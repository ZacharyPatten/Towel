using System;
using static Towel.Statics;

namespace Towel.DataStructures
{
	/// <summary>An indexed fixed-sized data structure.</summary>
	/// <typeparam name="T">The generic type within the structure.</typeparam>
	/// <typeparam name="Index">The generic type of the indexing.</typeparam>
	public interface IArray<T, Index> : IDataStructure<T>
	{
		#region Properties

		/// <summary>Allows indexed access of the array.</summary>
		/// <param name="index">The index of the array to get/set.</param>
		/// <returns>The value at the desired index.</returns>
		T this[Index index] { get; set; }
		/// <summary>The length of the array.</summary>
		Index Length { get; }

		#endregion
	}

	/// <summary>An indexed fixed-sized data structure.</summary>
	/// <typeparam name="T">The generic type within the structure.</typeparam>
	public interface IArray<T> : IArray<T, int>
	{

	}

	/// <summary>Contiguous fixed-sized data structure.</summary>
	/// <typeparam name="T">The generic type within the structure.</typeparam>
	public class Array<T> : IArray<T>, ICloneable<Array<T>>
	{
		internal T[] _array;

		#region Constructors

		/// <summary>Constructs an array that implements a traversal delegate function 
		/// which is an optimized "foreach" implementation.</summary>
		/// <param name="size">The length of the array in memory.</param>
		public Array(int size)
		{
			if (size < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(size), "size of the array must be at least 1.");
			}
			_array = new T[size];
		}

		/// <summary>Constructs by wrapping an existing array.</summary>
		/// <param name="array">The array to be wrapped.</param>
		public Array(params T[] array) => _array = array;

		#endregion

		#region Properties

		/// <inheritdoc/>
		public T this[int index]
		{
			get
			{
				if (!(0 <= index || index < _array.Length))
				{
					throw new ArgumentOutOfRangeException(nameof(index), $"!(0 <= {nameof(index)} < this.{nameof(_array.Length)})");
				}
				return _array[index];
			}
			set
			{
				if (!(0 <= index || index < _array.Length))
				{
					throw new ArgumentOutOfRangeException(nameof(index), $"!(0 <= {nameof(index)} < this.{nameof(_array.Length)})");
				}
				_array[index] = value;
			}
		}

		/// <summary>The length of the array.</summary>
		public int Length => _array.Length;

		#endregion

		#region Operators

		/// <summary>Implicitly converts a C# System array into a Towel array.</summary>
		/// <param name="array">The array to be represented as a Towel array.</param>
		public static implicit operator Array<T>(T[] array) => new(array);

		/// <summary>Implicitly converts a Towel array into a C# System array.</summary>
		/// <param name="array">The array to be represented as a C# System array.</param>
		public static implicit operator T[](Array<T> array) => array.ToArray();

		#endregion

		#region Methods

		/// <inheritdoc/>
		public Array<T> Clone() => (T[])_array.Clone();

		/// <inheritdoc/>
		public StepStatus StepperBreak<TStep>(TStep step)
			where TStep : struct, IFunc<T, StepStatus> =>
			_array.StepperBreak(step);

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <inheritdoc/>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator() => ((System.Collections.Generic.IEnumerable<T>)_array).GetEnumerator();

		/// <inheritdoc/>
		public T[] ToArray() => Length is 0 ? Array.Empty<T>() : _array[..];

		#endregion
	}

	/// <summary>An array implemented as a jagged array to allow for a number of values greater than Int.MaxValue.</summary>
	/// <typeparam name="T">The generic type of value to store in the array.</typeparam>
	public class ArrayJagged<T> : IArray<T, ulong>, ICloneable<ArrayJagged<T>>
	{
		// BLOCK_SIZE must be a power of 2, and we want it to be big enough that we allocate
		// blocks in the large object heap so that they don’t move.
		internal const int BLOCK_SIZE = 524288;
		internal const int BLOCK_SIZE_LOG2 = 19;

		// Don’t use a multi-dimensional array here because then we can’t right size the last
		// block and we have to do range checking on our own and since there will then be 
		// exception throwing in our code there is a good chance that the JIT won’t inline.
		internal T[][] _blocks;
		internal ulong _length;

		#region Constructors

		internal ArrayJagged(ArrayJagged<T> bigArray)
		{
			int numBlocks = (int)(bigArray.Length / BLOCK_SIZE);
			if ((ulong)(numBlocks * BLOCK_SIZE) < bigArray.Length)
			{
				numBlocks += 1;
			}
			_length = bigArray.Length;
			_blocks = new T[numBlocks][];
			for (int i = 0; i < numBlocks; i++)
			{
				_blocks[i] = (T[])bigArray._blocks[i].Clone();
			}
		}

		/// <summary>Constructs a ArrayJagged.</summary>
		/// <param name="size">The length of the ArrayJagged to construct.</param>
		public ArrayJagged(int size)
			: this((ulong)size)
		{ }

		/// <summary>Constructs a ArrayJagged.</summary>
		/// <param name="size">The length of the ArrayJagged to construct.</param>
		// maximum BigArray size = BLOCK_SIZE * Int.MaxValue
		public ArrayJagged(ulong size)
		{
			if (size is 0)
				return;

			int numBlocks = (int)(size / BLOCK_SIZE);
			if ((ulong)(numBlocks * BLOCK_SIZE) < size)
			{
				numBlocks += 1;
			}


			_length = size;
			_blocks = new T[numBlocks][];
			for (int i = 0; i < (numBlocks - 1); i++)
			{
				_blocks[i] = new T[BLOCK_SIZE];
			}
			// by making sure to make the last block right sized then we get the range checks 
			// for free with the normal array range checks and don’t have to add our own
			_blocks[numBlocks - 1] = new T[size % (ulong)BLOCK_SIZE];
		}

		#endregion

		#region Properties

		/// <inheritdoc/>
		public ulong Length => _length;

		/// <summary>Gets and sets the value at a particual index.</summary>
		/// <param name="index">The index of the value to get or set.</param>
		/// <returns>The value at the provided index.</returns>
		public T this[int index]
		{
			get
			{
				return this[(ulong)index];
			}
			set
			{
				this[(ulong)index] = value;
			}
		}

		/// <inheritdoc/>
		public T this[ulong index]
		{
			// these must be _very_ simple in order to ensure that they get inlined into
			// their caller 
			get
			{
				int blockNum = (int)(index >> BLOCK_SIZE_LOG2);
				int elementNumberInBlock = (int)(index & (BLOCK_SIZE - 1));
				return _blocks[blockNum][elementNumberInBlock];
			}
			set
			{
				int blockNum = (int)(index >> BLOCK_SIZE_LOG2);
				int elementNumberInBlock = (int)(index & (BLOCK_SIZE - 1));
				_blocks[blockNum][elementNumberInBlock] = value;
			}
		}

		#endregion

		#region Methods

		/// <inheritdoc/>
		public ArrayJagged<T> Clone() => new(this);

		/// <inheritdoc/>
		public StepStatus StepperBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<T, StepStatus>
		{
			for (int i = 0; i < _blocks.Length; i++)
			{
				T[] array = _blocks[i];
				int arrayLength = array.Length;
				for (int j = 0; j < arrayLength; j++)
				{
					if (step.Invoke(array[i]) is Break)
					{
						return Break;
					}
				}
			}
			return Continue;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <inheritdoc/>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			foreach (T[] array in _blocks)
			{
				foreach (T value in array)
				{
					yield return value;
				}
			}
		}

		/// <inheritdoc/>
		public T[] ToArray()
		{
			if (_length > int.MaxValue)
			{
				throw new InvalidOperationException("Can't convert ArrayJagged<T> to array. Length > int.MaxValue.");
			}
			T[] array = new T[_length];
			int i = 0;
			foreach (T[] segment in _blocks)
			{
				foreach (T value in array)
				{
					array[i++] = value;
				}
			}
			return array;
		}

		#endregion
	}
}
