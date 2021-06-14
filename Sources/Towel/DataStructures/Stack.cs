using System;
using static Towel.Statics;

namespace Towel.DataStructures
{
	/// <summary>Implements a First-In-Last-Out stack data structure.</summary>
	/// <typeparam name="T">The generic type within the structure.</typeparam>
	public interface IStack<T> : IDataStructure<T>,
		DataStructure.ICountable,
		DataStructure.IClearable
	{
		#region Methods

		/// <summary>Adds an item to the top of the stack.</summary>
		/// <param name="push">The item to add to the stack.</param>
		void Push(T push);
		/// <summary>Returns the most recent addition to the stack.</summary>
		/// <returns>The most recent addition to the stack.</returns>
		T Peek();
		/// <summary>Removes and returns the most recent addition to the stack.</summary>
		/// <returns>The most recent addition to the stack.</returns>
		T Pop();

		#endregion
	}

	/// <summary>Implements a First-In-Last-Out stack data structure using a linked list.</summary>
	/// <typeparam name="T">The generic type within the structure.</typeparam>
	public class StackLinked<T> : IStack<T>, ICloneable<StackLinked<T>>
	{
		internal Node? _top;
		internal int _count;

		#region Nested Types

		internal class Node
		{
			internal T Value;
			internal Node? Down;

			internal Node(T value, Node? down = null)
			{
				Value = value;
				Down = down;
			}
		}

		#endregion

		#region Constructors

		/// <summary>Constructs a new stack.</summary>
		public StackLinked()
		{
			_top = null;
			_count = 0;
		}

		/// <summary>This constructor is for cloning purposes.</summary>
		/// <param name="stack">The stack to clone.</param>
		internal StackLinked(StackLinked<T> stack)
		{
			if (stack._top is not null)
			{
				_top = new Node(value: stack._top.Value);
				Node? a = stack._top.Down;
				Node? b = _top;
				while (a is not null)
				{
					b.Down = new Node(value: a.Value);
					b = b.Down;
					a = a.Down;
				}
				_count = stack._count;
			}
		}

		#endregion

		#region Properties

		/// <inheritdoc/>
		public int Count => _count;

		#endregion

		#region Methods

		/// <inheritdoc/>
		public StackLinked<T> Clone() => new(this);

		/// <inheritdoc/>
		public T[] ToArray()
		{
			if (_count is 0)
			{
				return Array.Empty<T>();
			}
			T[] array = new T[_count];
			for (var (i, node) = (0, _top); node is not null; node = node.Down, i++)
			{
				array[i] = node.Value;
			}
			return array;
		}

		/// <inheritdoc/>
		public void Push(T addition)
		{
			_top = new Node(value: addition, down: _top);
			_count++;
		}

		/// <inheritdoc/>
		public T Peek()
		{
			if (_top is null)
			{
				throw new InvalidOperationException("Attempting to remove from an empty queue.");
			}
			T peek = _top.Value;
			return peek;
		}

		/// <inheritdoc/>
		public T Pop()
		{
			if (_count == 0)
			{
				throw new InvalidOperationException("attempting to pop from an empty stack.");
			}
			if (_top is null)
			{
				throw new TowelBugException($"{nameof(Count)} is greater than 0 but {nameof(_top)} is null");
			}
			T pop = _top.Value;
			_top = _top.Down;
			_count--;
			return pop;
		}

		/// <inheritdoc/>
		public void Clear()
		{
			_top = null;
			_count = 0;
		}

		/// <inheritdoc/>
		public StepStatus StepperBreak<Step>(Step step = default)
			where Step : struct, IFunc<T, StepStatus>
		{
			for (Node? node = _top; node is not null; node = node.Down)
			{
				if (step.Invoke(node.Value) is Break)
				{
					return Break;
				}
			}
			return Continue;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <inheritdoc/>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			for (Node? node = _top; node is not null; node = node.Down)
			{
				yield return node.Value;
			}
		}

		#endregion
	}

	/// <summary>Implements a First-In-Last-Out stack data structure using an array.</summary>
	/// <typeparam name="T">The generic type within the structure.</typeparam>
	public class StackArray<T> : IStack<T>, ICloneable<StackArray<T>>
	{
		const int DefaultMinimumCapacity = 1;

		internal T[] _array;
		internal int _count;
		internal int? _minimumCapacity;

		#region Constructors

		/// <summary>Constructs a new stack.</summary>
		public StackArray()
		{
			_array = new T[DefaultMinimumCapacity];
			_count = 0;
		}

		/// <summary>Constructs a new stack.</summary>
		/// <param name="minimumCapacity">The minimum capacity of the stack.</param>
		public StackArray(int minimumCapacity)
		{
			if (minimumCapacity <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(minimumCapacity), minimumCapacity, $"{nameof(minimumCapacity)} <= 0");
			}
			_array = new T[minimumCapacity];
			_count = 0;
			_minimumCapacity = minimumCapacity;
		}

		internal StackArray(T[] array, int count, int? minimumCapacity)
		{
			_array = array;
			_count = count;
			_minimumCapacity = minimumCapacity;
		}

		/// <summary>This constructor is for cloning purposes.</summary>
		/// <param name="stack">The stack to clone.</param>
		internal StackArray(StackArray<T> stack)
		{
			_array = (T[])stack._array.Clone();
			_count = stack._count;
			_minimumCapacity = stack._minimumCapacity;
		}

		#endregion

		#region Properties

		/// <summary>Gets the current capacity of the list.</summary>
		public int CurrentCapacity => _array.Length;

		/// <summary>Allows you to adjust the minimum capacity of this list.</summary>
		public int? MinimumCapacity
		{
			get => _minimumCapacity;
			set
			{
				if (!value.HasValue)
				{
					_minimumCapacity = value;
				}
				else if (value < 1)
				{
					throw new InvalidOperationException("Attempting to set a minimum capacity to a negative or zero value.");
				}
				else if (value > _array.Length)
				{
					Array.Resize<T>(ref _array, value.Value);
				}
				_minimumCapacity = value;
			}
		}

		/// <inheritdoc/>
		public int Count => _count;

		#endregion

		#region Methods

		/// <inheritdoc/>
		public StackArray<T> Clone() => new(this);

		/// <inheritdoc/>
		public T[] ToArray() => _count is 0 ? Array.Empty<T>() : _array[.._count];

		/// <inheritdoc/>
		public void Push(T addition)
		{
			if (_count == _array.Length)
			{
				if (_array.Length > int.MaxValue / 2)
				{
					throw new InvalidOperationException("your queue is so large that it can no longer double itself (Int32.MaxValue barrier reached).");
				}
				Array.Resize<T>(ref _array, _array.Length * 2);
			}
			_array[_count++] = addition;
		}

		/// <inheritdoc/>
		public T Pop()
		{
			if (_count == 0)
			{
				throw new InvalidOperationException("attempting to pop from an empty stack.");
			}
			if (_count < _array.Length / 4 && _array.Length / 2 > _minimumCapacity)
			{
				Array.Resize<T>(ref _array, _array.Length / 2);
			}
			T returnValue = _array[--_count];
			return returnValue;
		}

		/// <inheritdoc/>
		public T Peek() => _array[_count - 1];

		/// <inheritdoc/>
		public void Clear()
		{
			_array = new T[_minimumCapacity ?? DefaultMinimumCapacity];
			_count = 0;
		}

		/// <inheritdoc/>
		public StepStatus StepperBreak<Step>(Step step = default)
			where Step : struct, IFunc<T, StepStatus> =>
			_array.StepperBreak(0, _count, step);

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <inheritdoc/>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			for (int i = 0; i < _count; i++)
			{
				yield return _array[i];
			}
		}

		#endregion
	}
}
