using System;

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
	[Serializable]
	public class StackLinked<T> : IStack<T>
	{
		internal Node _top;
		internal int _count;

		#region Node

		[Serializable]
		internal class Node
		{
			internal T Value;
			internal Node Down;

			internal Node(T data, Node down)
			{
				Value = data;
				Down = down;
			}
		}

		#endregion

		#region Constructors

		/// <summary>Creates an instance of a stack.</summary>
		/// <runtime>θ(1)</runtime>
		public StackLinked()
		{
			_top = null;
			_count = 0;
		}

		#endregion

		#region Properties

		/// <summary>Returns the number of items in the stack.</summary>
		/// <runtime>θ(1)</runtime>
		public int Count
		{
			get
			{
				return _count;
			}
		}

		#endregion

		#region Methods

		#region Clone

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public StackLinked<T> Clone()
		{
			StackLinked<T> clone = new StackLinked<T>();
			if (_count == 0)
			{
				return clone;
			}
			Node copying = _top;
			Node cloneTop = new Node(_top.Value, null);
			Node cloning = cloneTop;
			while (copying != null)
			{
				copying = copying.Down;
				cloning.Down = new Node(copying.Value, null);
				cloning = cloning.Down;
			}
			clone._top = cloneTop;
			return clone;
		}

		#endregion

		#region ToArray

		/// <summary>Converts the structure into an array.</summary>
		/// <returns>An array containing all the item in the structure.</returns>
		/// <remarks>Runtime: Towel(n).</remarks>
		public T[] ToArray()
		{
			if (_count == 0)
			{
				return null;
			}
			T[] array = new T[_count];
			Node looper = _top;
			for (int i = 0; i < _count; i++)
			{
				array[i] = looper.Value;
				looper = looper.Down;
			}
			return array;
		}

		#endregion

		#region Push

		/// <summary>Adds an item to the top of the stack.</summary>
		/// <param name="addition">The item to add to the stack.</param>
		/// <runtime>O(1)</runtime>
		public void Push(T addition)
		{
			_top = new Node(addition, _top);
			_count++;
		}

		#endregion

		#region Peek

		/// <summary>Returns the most recent addition to the stack.</summary>
		/// <returns>The most recent addition to the stack.</returns>
		/// <remarks>Runtime: O(1).</remarks>
		public T Peek()
		{
			if (_top == null)
				throw new System.InvalidOperationException("Attempting to remove from an empty queue.");
			T peek = _top.Value;
			return peek;
		}

		#endregion

		#region Pop

		/// <summary>Removes and returns the most recent addition to the stack.</summary>
		/// <returns>The most recent addition to the stack.</returns>
		/// <remarks>Runtime: O(1).</remarks>
		public T Pop()
		{
			T x = _top.Value;
			_top = _top.Down;
			_count--;
			return x;
		}

		#endregion

		#region Clear

		/// <summary>Clears the stack to an empty state.</summary>
		/// <remarks>Runtime: O(1). Note: causes considerable garbage collection.</remarks>
		public void Clear()
		{
			_top = null;
			_count = 0;
		}

		#endregion

		#region Stepper and IEnumerable

		#region Stepper

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Step<T> step)
		{
			for (Node looper = _top; looper != null; looper = looper.Down)
			{
				step(looper.Value);
			}
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(StepRef<T> step)
		{
			for (Node looper = _top; looper != null; looper = looper.Down)
			{
				step(ref looper.Value);
			}
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepBreak<T> step)
		{
			for (Node looper = _top; looper != null; looper = looper.Down)
			{
				if (step(looper.Value) == StepStatus.Break)
				{
					return StepStatus.Break;
				}
			}
			return StepStatus.Continue;
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepRefBreak<T> step)
		{
			for (Node looper = _top; looper != null; looper = looper.Down)
			{
				if (step(ref looper.Value) == StepStatus.Break)
				{
					return StepStatus.Break;
				}
			}
			return StepStatus.Continue;
		}

		#endregion

		#region IEnumerable

		System.Collections.IEnumerator
			System.Collections.IEnumerable.GetEnumerator()
		{
			for (Node looper = _top; looper != null; looper = looper.Down)
			{
				yield return looper.Value;
			}
		}

		System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
		{
			for (Node looper = _top; looper != null; looper = looper.Down)
			{
				yield return looper.Value;
			}
		}

		#endregion

		#endregion

		#endregion
	}

	/// <summary>Implements a First-In-Last-Out stack data structure using an array.</summary>
	/// <typeparam name="T">The generic type within the structure.</typeparam>
	[Serializable]
	public class StackArray<T> : IStack<T>
	{
		private T[] _array;
		private int _count;
		private int _minimumCapacity;

		#region Constructors

		/// <summary>Creates an instance of a ListArray, and sets it's minimum capacity.</summary>
		/// <remarks>Runtime: O(1).</remarks>
		public StackArray()
		{
			_array = new T[1];
			_count = 0;
			_minimumCapacity = 1;
		}

		/// <summary>Creates an instance of a ListArray, and sets it's minimum capacity.</summary>
		/// <param name="minimumCapacity">The initial and smallest array size allowed by this list.</param>
		/// <remarks>Runtime: O(1).</remarks>
		public StackArray(int minimumCapacity)
		{
			_array = new T[minimumCapacity];
			_count = 0;
			_minimumCapacity = minimumCapacity;
		}

		#endregion

		#region Properties

		/// <summary>Gets the current capacity of the list.</summary>
		/// <remarks>Runtime: O(1).</remarks>
		public int CurrentCapacity
		{
			get
			{
				return _array.Length;
			}
		}

		/// <summary>Allows you to adjust the minimum capacity of this list.</summary>
		/// <remarks>Runtime: O(n), Omega(1).</remarks>
		public int MinimumCapacity
		{
			get
			{
				int returnValue = _minimumCapacity;
				return returnValue;
			}
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
				else
				{
					_minimumCapacity = value;
				}
			}
		}

		/// <summary>Gets the number of items in the list.</summary>
		/// <remarks>Runtime: O(1).</remarks>
		public int Count
		{
			get
			{
				return _count;
			}
		}

		#endregion

		#region Methods

		#region Clone

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public StackArray<T> Clone()
		{
			StackArray<T> clone = new StackArray<T>();
			clone._array = new T[this._array.Length];
			for (int i = 0; i < this._count; i++)
				clone._array[i] = this._array[i];
			clone._minimumCapacity = this._minimumCapacity;
			clone._count = this._count;
			return clone;
		}

		#endregion

		#region ToArray

		/// <summary>Converts the list array into a standard array.</summary>
		/// <returns>A standard array of all the elements.</returns>
		public T[] ToArray()
		{
			T[] array = new T[this._count];
			for (int i = 0; i < this._count; i++)
				array[i] = this._array[i];
			return array;
		}

		#endregion

		#region Push

		/// <summary>Adds an item to the end of the list.</summary>
		/// <param name="addition">The item to be added.</param>
		/// <remarks>Runtime: O(n), EstAvg(1). </remarks>
		public void Push(T addition)
		{
			if (_count == _array.Length)
			{
				if (_array.Length > int.MaxValue / 2)
					throw new System.InvalidOperationException("your queue is so large that it can no longer double itself (Int32.MaxValue barrier reached).");
				T[] newStack = new T[_array.Length * 2];
				for (int i = 0; i < _count; i++)
					newStack[i] = _array[i];
				_array = newStack;
			}
			_array[_count++] = addition;
		}

		#endregion

		#region Pop

		/// <summary>Removes the item at a specific index.</summary>
		/// <remarks>Runtime: Towel(n - index).</remarks>
		public T Pop()
		{
			if (_count == 0)
				throw new System.InvalidOperationException("attempting to dequeue from an empty queue.");
			if (_count < _array.Length / 4 && _array.Length / 2 > _minimumCapacity)
			{
				T[] newQueue = new T[_array.Length / 2];
				for (int i = 0; i < _count; i++)
					newQueue[i] = _array[i];
				_array = newQueue;
			}
			T returnValue = _array[--_count];
			return returnValue;
		}

		#endregion

		#region Peek

		/// <summary>Returns the most recent addition to the stack.</summary>
		/// <returns>The most recent addition to the stack.</returns>
		/// <remarks>Runtime: O(1).</remarks>
		public T Peek()
		{
			T returnValue = _array[_count - 1];
			return returnValue;
		}

		#endregion

		#region Clear

		/// <summary>Empties the list back and reduces it back to its original capacity.</summary>
		/// <remarks>Runtime: O(1).</remarks>
		public void Clear()
		{
			_array = new T[_minimumCapacity];
			_count = 0;
		}

		#endregion

		#region Stepper And IEnumerabe

		#region Stepper

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Step<T> step)
		{
			for (int i = 0; i < this._count; i++)
			{
				step(_array[i]);
			}
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(StepRef<T> step)
		{
			for (int i = 0; i < this._count; i++)
			{
				step(ref _array[i]);
			}
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepBreak<T> step)
		{
			for (int i = 0; i < _count; i++)
			{
				if (step(_array[i]) == StepStatus.Break)
				{
					return StepStatus.Break;
				}
			}
			return StepStatus.Continue;
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepRefBreak<T> step)
		{
			for (int i = 0; i < this._count; i++)
			{
				if (step(ref _array[i]) == StepStatus.Break)
				{
					return StepStatus.Break;
				}
			}
			return StepStatus.Continue;
		}

		#endregion

		#region IEnumerable

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			for (int i = 0; i < _count; i++)
			{
				yield return _array[i];
			}
		}

		System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
		{
			for (int i = 0; i < _count; i++)
			{
				yield return _array[i];
			}
		}

		#endregion

		#endregion

		#endregion
	}
}
