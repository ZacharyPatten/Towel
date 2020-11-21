using System;
using Towel;
using static Towel.Statics;

namespace Towel.DataStructures
{
	/// <summary>Implements a First-In-Last-Out stack data structure.</summary>
	/// <typeparam name="T">The generic type within the structure.</typeparam>
	public interface IStack<T> : IDataStructure<T>,
		// Structure Properties
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
	public class StackLinked<T> : IStack<T>
	{
		internal Node? _top;
		internal int _count;

		#region Node

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

		/// <summary>
		/// Creates an instance of a stack.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public StackLinked()
		{
			_top = null;
			_count = 0;
		}

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

		/// <summary>
		/// Returns the number of items in the stack.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public int Count => _count;

		#endregion

		#region Methods

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public StackLinked<T> Clone() => new StackLinked<T>(this);

		/// <summary>
		/// Converts the structure into an array.
		/// <para>Runtime: Θ(n)</para>
		/// </summary>
		/// <returns>An array containing all the item in the structure.</returns>
		public T?[] ToArray()
		{
			if (_count == 0)
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

		/// <summary>
		/// Adds an item to the top of the stack.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <param name="addition">The item to add to the stack.</param>
		public void Push(T addition)
		{
			_top = new Node(value: addition, down: _top);
			_count++;
		}

		/// <summary>
		/// Returns the most recent addition to the stack.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <returns>The most recent addition to the stack.</returns>
		public T Peek()
		{
			if (_top is null)
			{
				throw new InvalidOperationException("Attempting to remove from an empty queue.");
			}
			T peek = _top.Value;
			return peek;
		}

		/// <summary>
		/// Removes and returns the most recent addition to the stack.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <returns>The most recent addition to the stack.</returns>
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

		/// <summary>
		/// Clears the stack to an empty state.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public void Clear()
		{
			_top = null;
			_count = 0;
		}

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void Stepper<Step>(Step step = default)
			where Step : struct, IAction<T> =>
			StepperRef<StepToStepRef<T, Step>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void Stepper(Action<T> step) =>
			Stepper<ActionRuntime<T>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void StepperRef<Step>(Step step = default)
			where Step : struct, IStepRef<T> =>
			StepperRefBreak<StepRefBreakFromStepRef<T, Step>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void Stepper(StepRef<T> step) =>
			StepperRef<StepRefRuntime<T>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public StepStatus StepperBreak<Step>(Step step = default)
			where Step : struct, IFunc<T, StepStatus> =>
			StepperRefBreak<StepRefBreakFromStepBreak<T, Step>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public StepStatus Stepper(Func<T, StepStatus> step) =>
			StepperBreak<StepBreakRuntime<T>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public StepStatus Stepper(StepRefBreak<T> step) =>
			StepperRefBreak<StepRefBreakRuntime<T>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public StepStatus StepperRefBreak<Step>(Step step = default)
			where Step : struct, IStepRefBreak<T>
		{
			for (Node? node = _top; node is not null; node = node.Down)
			{
				if (step.Do(ref node.Value) is Break)
				{
					return Break;
				}
			}
			return Continue;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Gets the enumerator for this stack.</summary>
		/// <returns>The enumerator for this stack.</returns>
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
	public class StackArray<T> : IStack<T>
	{
		const int DefaultMinimumCapacity = 1;

		internal T[] _array;
		internal int _count;
		internal int _minimumCapacity;

		#region Constructors

		/// <summary>
		/// Creates an instance of a ListArray, and sets it's minimum capacity.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public StackArray() : this(DefaultMinimumCapacity) { }

		/// <summary>
		/// Creates an instance of a ListArray, and sets it's minimum capacity.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <param name="minimumCapacity">The initial and smallest array size allowed by this list.</param>
		public StackArray(int minimumCapacity)
		{
			_array = new T[minimumCapacity];
			_count = 0;
			_minimumCapacity = minimumCapacity;
		}

		internal StackArray(T[] array, int count, int minimumCapacity = DefaultMinimumCapacity)
		{
			_array = array;
			_count = count;
			_minimumCapacity = minimumCapacity;
		}

		internal StackArray(StackArray<T> stack)
		{
			_array = (T[])stack._array.Clone();
			_count = stack._count;
			_minimumCapacity = stack._minimumCapacity;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the current capacity of the list.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public int CurrentCapacity => _array.Length;

		/// <summary>
		/// Allows you to adjust the minimum capacity of this list.
		/// <para>Runtime (get): O(1)</para>
		/// <para>Runtime (set): O(n), Ω(1)</para>
		/// </summary>
		public int MinimumCapacity
		{
			get => _minimumCapacity;
			set
			{
				if (value < 1)
				{
					throw new InvalidOperationException("Attempting to set a minimum capacity to a negative or zero value.");
				}
				else if (value > _array.Length)
				{
					T[] newList = new T[value];
					_array.CopyTo(newList, 0);
					_array = newList;
				}
				_minimumCapacity = value;
			}
		}

		/// <summary>
		/// Gets the number of items in the list.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public int Count => _count;

		#endregion

		#region Methods

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public StackArray<T> Clone() => new StackArray<T>(this);

		/// <summary>Converts the list array into a standard array.</summary>
		/// <returns>A standard array of all the elements.</returns>
		public T[] ToArray() => _array.AsSpan(0, _count).ToArray();

		/// <summary>
		/// Adds an item to the end of the list.
		/// <para>Runtime: O(n), Ω(1), ε(1)</para>
		/// </summary>
		/// <param name="addition">The item to be added.</param>
		public void Push(T addition)
		{
			if (_count == _array.Length)
			{
				if (_array.Length > int.MaxValue / 2)
				{
					throw new InvalidOperationException("your queue is so large that it can no longer double itself (Int32.MaxValue barrier reached).");
				}
				T[] newStack = new T[_array.Length * 2];
				for (int i = 0; i < _count; i++)
				{
					newStack[i] = _array[i];
				}
				_array = newStack;
			}
			_array[_count++] = addition;
		}

		/// <summary>
		/// Removes the item at a specific index.
		/// <para>Runtime: O(Count), Ω(1), ε(1)</para>
		/// </summary>
		public T Pop()
		{
			if (_count == 0)
			{
				throw new InvalidOperationException("attempting to pop from an empty stack.");
			}
			if (_count < _array.Length / 4 && _array.Length / 2 > _minimumCapacity)
			{
				T[] newQueue = new T[_array.Length / 2];
				for (int i = 0; i < _count; i++)
				{
					newQueue[i] = _array[i];
				}
				_array = newQueue;
			}
			T returnValue = _array[--_count];
			return returnValue;
		}

		/// <summary>
		/// Returns the most recent addition to the stack.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <returns>The most recent addition to the stack.</returns>
		public T Peek() => _array[_count - 1];

		/// <summary>
		/// Empties the list back and reduces it back to its original capacity.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public void Clear()
		{
			_array = new T[_minimumCapacity];
			_count = 0;
		}

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void Stepper<Step>(Step step = default)
			where Step : struct, IAction<T> =>
			StepperRef<StepToStepRef<T, Step>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void Stepper(Action<T> step) =>
			Stepper<ActionRuntime<T>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void StepperRef<Step>(Step step = default)
			where Step : struct, IStepRef<T> =>
			StepperRefBreak<StepRefBreakFromStepRef<T, Step>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void Stepper(StepRef<T> step) =>
			StepperRef<StepRefRuntime<T>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public StepStatus StepperBreak<Step>(Step step = default)
			where Step : struct, IFunc<T, StepStatus> =>
			StepperRefBreak<StepRefBreakFromStepBreak<T, Step>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public StepStatus Stepper(Func<T, StepStatus> step) => StepperBreak<StepBreakRuntime<T>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public StepStatus Stepper(StepRefBreak<T> step) => StepperRefBreak<StepRefBreakRuntime<T>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public StepStatus StepperRefBreak<Step>(Step step = default)
			where Step : struct, IStepRefBreak<T> =>
			_array.StepperRefBreak(0, _count, step);

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Gets the enumerator for this stack.</summary>
		/// <returns>The enumerator for this stack.</returns>
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
