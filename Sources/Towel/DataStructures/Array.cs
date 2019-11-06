using System;
using System.Collections;
using System.Collections.Generic;
using static Towel.Syntax;

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
	[Serializable]
	public class Array<T> : IArray<T>
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
				throw new ArgumentOutOfRangeException("size of the array must be at least 1.");
			}
			_array = new T[size];
		}

		/// <summary>Constructs by wrapping an existing array.</summary>
		/// <param name="array">The array to be wrapped.</param>
		public Array(params T[] array) => _array = array;

		#endregion

		#region Properties

		/// <summary>Allows indexed access of the array.</summary>
		/// <param name="index">The index of the array to get/set.</param>
		/// <returns>The value at the desired index.</returns>
		public T this[int index]
		{
			get
			{
				if (!(0 <= index || index < _array.Length))
				{
					throw new ArgumentOutOfRangeException("!(0 <= " + nameof(index) + " < this." + nameof(_array.Length) + ")");
				}
				return _array[index];
			}
			set
			{
				if (!(0 <= index || index < _array.Length))
				{
					throw new ArgumentOutOfRangeException("!(0 <= " + nameof(index) + " < this." + nameof(_array.Length) + ")");
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
		public static implicit operator Array<T>(T[] array) => new Array<T>(array);

		/// <summary>Implicitly converts a Towel array into a C# System array.</summary>
		/// <param name="array">The array to be represented as a C# System array.</param>
		public static implicit operator T[](Array<T> array) => array.ToArray();

		#endregion

		#region Methods

		#region Clone

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public Array<T> Clone() => (T[])_array.Clone();

		#endregion

		#region Stepper And IEnumerable

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Step<T> step) => _array.Stepper(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(StepRef<T> step) => _array.Stepper(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepBreak<T> step) => _array.Stepper(step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepRefBreak<T> step) => _array.Stepper(step);

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Gets the enumerator for the array.</summary>
		/// <returns>The enumerator for the array.</returns>
		public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)_array).GetEnumerator();

		#endregion

		#region ToArray

		/// <summary>Converts the structure into an array.</summary>
		/// <returns>An array containing all the item in the structure.</returns>
		public T[] ToArray() => _array;

		#endregion

		#endregion
	}

	/// <summary>An array implemented as a jagged array to allow for a number of elements > Int.MaxValue.</summary>
	/// <typeparam name="T">The generic type of value to store in the array.</typeparam>
	public class ArrayJagged<T> : IArray<T, ulong>
	{
		// BLOCK_SIZE must be a power of 2, and we want it to be big enough that we allocate
		// blocks in the large object heap so that they don’t move.
		internal const int BLOCK_SIZE = 524288;
		internal const int BLOCK_SIZE_LOG2 = 19;

		// Don’t use a multi-dimensional array here because then we can’t right size the last
		// block and we have to do range checking on our own and since there will then be 
		// exception throwing in our code there is a good chance that the JIT won’t inline.
		internal T[][] _elements;
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
			_elements = new T[numBlocks][];
			for (int i = 0; i < numBlocks; i++)
			{
				_elements[i] = bigArray._elements[i].Clone() as T[];
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
			if (size == 0)
				return;

			int numBlocks = (int)(size / BLOCK_SIZE);
			if ((ulong)(numBlocks * BLOCK_SIZE) < size)
			{
				numBlocks += 1;
			}


			_length = size;
			_elements = new T[numBlocks][];
			for (int i = 0; i < (numBlocks - 1); i++)
			{
				_elements[i] = new T[BLOCK_SIZE];
			}
			// by making sure to make the last block right sized then we get the range checks 
			// for free with the normal array range checks and don’t have to add our own
			_elements[numBlocks - 1] = new T[size % (ulong)BLOCK_SIZE];
		}

		#endregion

		#region Properties

		/// <summary>The length of the array.</summary>
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

		/// <summary>Gets and sets the value at a particual index.</summary>
		/// <param name="index">The index of the value to get or set.</param>
		/// <returns>The value at the provided index.</returns>
		public T this[ulong index]
		{
			// these must be _very_ simple in order to ensure that they get inlined into
			// their caller 
			get
			{
				int blockNum = (int)(index >> BLOCK_SIZE_LOG2);
				int elementNumberInBlock = (int)(index & (BLOCK_SIZE - 1));
				return _elements[blockNum][elementNumberInBlock];
			}
			set
			{
				int blockNum = (int)(index >> BLOCK_SIZE_LOG2);
				int elementNumberInBlock = (int)(index & (BLOCK_SIZE - 1));
				_elements[blockNum][elementNumberInBlock] = value;
			}
		}

		#endregion

		#region Methods

		/// <summary>Clones this array.</summary>
		/// <returns>A clone of the array.</returns>
		public ArrayJagged<T> Clone() =>
			new ArrayJagged<T>(this);

		#region Stepper And IEnumerable

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Step<T> step)
		{
			for (int i = 0; i < _elements.Length; i++)
			{
				T[] array = _elements[i];
				int arrayLength = array.Length;
				for (int j = 0; j < arrayLength; j++)
				{
					step(array[j]);
				}
			}
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(StepRef<T> step)
		{
			for (int i = 0; i < _elements.Length; i++)
			{
				T[] array = _elements[i];
				int arrayLength = array.Length;
				for (int j = 0; j < arrayLength; j++)
				{
					step(ref array[j]);
				}
			}
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepBreak<T> step)
		{
			for (int i = 0; i < _elements.Length; i++)
			{
				T[] array = _elements[i];
				int arrayLength = array.Length;
				for (int j = 0; j < arrayLength; j++)
				{
					if (step(array[i]) == Break)
					{
						return Break;
					}
				}
			}
			return Continue;
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepRefBreak<T> step)
		{
			for (int i = 0; i < _elements.Length; i++)
			{
				T[] array = _elements[i];
				int arrayLength = array.Length;
				for (int j = 0; j < arrayLength; j++)
				{
					if (step(ref array[i]) == Break)
					{
						return Break;
					}
				}
			}
			return Continue;
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Gets the enumerator for this array.</summary>
		/// <returns>The enumerator for this array.</returns>
		public IEnumerator<T> GetEnumerator()
		{
			foreach (T[] array in _elements)
			{
				foreach (T value in array)
				{
					yield return value;
				}
			}
		}

		#endregion

		#endregion
	}
}
