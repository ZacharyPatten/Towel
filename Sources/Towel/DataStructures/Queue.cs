using System;
using static Towel.Syntax;

namespace Towel.DataStructures
{
	/// <summary>Implements First-In-First-Out queue data structure.</summary>
	/// <typeparam name="T">The generic type within the structure.</typeparam>
	public interface IQueue<T> : IDataStructure<T>,
		// Structure Properties
		DataStructure.ICountable,
		DataStructure.IClearable
	{
		#region Properties

		/// <summary>The newest item currently in the queue.</summary>
		T Newest { get; }
		/// <summary>The oldest item currently in the queue.</summary>
		T Oldest { get; }

		#endregion

		#region Methods

		/// <summary>Adds an item to the back of the queue.</summary>
		/// <param name="enqueue">The item to add to the queue.</param>
		void Enqueue(T enqueue);
		/// <summary>Gets the next item in the queue without removing it.</summary>
		/// <returns>The next item in the queue.</returns>
		T Peek();
		/// <summary>Removes the oldest item in the queue.</summary>
		/// <returns>The next item in the queue.</returns>
		T Dequeue();

		#endregion
	}

	/// <summary>Implements First-In-First-Out queue data structure using a linked list.</summary>
	/// <typeparam name="T">The generic type within the structure.</typeparam>
	[Serializable]
	public class QueueLinked<T> : IQueue<T>
	{
		internal Node _head;
		internal Node _tail;
		internal int _count;

		#region Node

		[Serializable]
		internal class Node
		{
			internal T Value;
			internal Node Next;

			internal Node(T data)
			{
				Value = data;
			}
		}

		#endregion

		#region Constructors

		/// <summary>Creates an instance of a queue.</summary>
		/// <runtime>O(1)</runtime>
		public QueueLinked()
		{
			_head = _tail = null;
			_count = 0;
		}

		#endregion

		#region Properties

		/// <summary>The newest item currently in the queue.</summary>
		/// <runtime>O(1)</runtime>
		public T Newest =>
			_count > 0
			? _tail.Value
			: throw new InvalidOperationException("attempting to get the newest item in an empty queue");

		/// <summary>The oldest item currently in the queue.</summary>
		/// <runtime>O(1)</runtime>
		public T Oldest =>
			_count > 0
			? _head.Value
			: throw new InvalidOperationException("attempting to get the oldet item in an empty queue");

		/// <summary>Returns the number of items in the queue.</summary>
		/// <runtime>O(1)</runtime>
		public int Count => _count;

		#endregion

		#region Methods

		#region ToArray

		/// <summary>Converts the list into a standard array.</summary>
		/// <returns>A standard array of all the items.</returns>
		/// <runtime>O(n)</runtime>
		public T[] ToArray()
		{
			T[] array = new T[_count];
			int index = 0;
			for (Node current = _head; current != null; current = current.Next)
			{
				array[index++] = current.Value;
			}
			return array;
		}

		#endregion

		#region Clone

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public QueueLinked<T> Clone()
		{
			Node head = new Node(_head.Value);
			Node current = _head.Next;
			Node current_clone = head;
			while (current != null)
			{
				current_clone.Next = new Node(current.Value);
				current_clone = current_clone.Next;
				current = current.Next;
			}
			QueueLinked<T> clone = new QueueLinked<T>
			{
				_head = head,
				_tail = current_clone,
				_count = _count
			};
			return clone;
		}

		#endregion

		#region Enqueue

		/// <summary>Adds an item to the back of the queue.</summary>
		/// <param name="enqueue">The item to add to the queue.</param>
		/// <remarks>Runtime: O(1).</remarks>
		public void Enqueue(T enqueue)
		{
			if (_tail == null)
			{
				_head = _tail = new Node(enqueue);
			}
			else
			{
				_tail = _tail.Next = new Node(enqueue);
			}
			_count++;
		}

		#endregion

		#region Dequeue

		/// <summary>Removes the oldest item in the queue.</summary>
		/// <returns>The next item in the queue.</returns>
		/// <remarks>Runtime: O(1).</remarks>
		public T Dequeue()
		{
			if (_head == null)
			{
				throw new InvalidOperationException("deque from an empty queue");
			}
			T value = _head.Value;
			if (_head == _tail)
			{
				_tail = null;
			}
			_head = _head.Next;
			_count--;
			return value;
		}

		#endregion

		#region Peek

		/// <summary>Gets the next item in the queue without removing it.</summary>
		/// <returns>The next item in the queue.</returns>
		public T Peek()
		{
			if (_head == null)
			{
				throw new InvalidOperationException("peek from an empty queue");
			}
			T returnValue = _head.Value;
			return returnValue;
		}

		#endregion

		#region Clear

		/// <summary>Resets the queue to an empty state.</summary>
		/// <runtime>O(1)</runtime>
		public void Clear()
		{
			_head = _tail = null;
			_count = 0;
		}

		#endregion

		#region Stepper And IEnumerable

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Step<T> step)
		{
			for (Node current = _head; current != null; current = current.Next)
			{
				step(current.Value);
			}
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(StepRef<T> step)
		{
			for (Node current = _head; current != null; current = current.Next)
			{
				step(ref current.Value);
			}
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepBreak<T> step)
		{
			for (Node current = _head; current != null; current = current.Next)
			{
				if (step(current.Value) == Break)
				{
					return Break;
				}
			}
			return Continue;
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepRefBreak<T> step)
		{
			for (Node current = _head; current != null; current = current.Next)
			{
				if (step(ref current.Value) == Break)
				{
					return Break;
				}
			}
			return Continue;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Gets the enumerator for this queue.</summary>
		/// <returns>The enumerator for the queue.</returns>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			for (Node current = _head; current != null; current = current.Next)
			{
				yield return current.Value;
			}
		}

		#endregion

		#endregion
	}

	/// <summary>Implements First-In-First-Out queue data structure using an array.</summary>
	/// <typeparam name="T">The generic type within the structure.</typeparam>
	[Serializable]
	public class QueueArray<T> : IQueue<T>
	{
		internal T[] _queue;
		internal int _start;
		internal int _count;
		internal int _minimumCapacity;

		#region Constructors

		/// <summary>Creates an instance of a ListArray, and sets it's minimum capacity.</summary>
		/// <runtime>θ(1)</runtime>
		public QueueArray()
		{
			_queue = new T[1];
			_count = 0;
			_minimumCapacity = 1;
		}

		/// <summary>Creates an instance of a ListArray, and sets it's minimum capacity.</summary>
		/// <param name="minimumCapacity">The initial and smallest array size allowed by this list.</param>
		/// <runtime>θ(1)</runtime>
		public QueueArray(int minimumCapacity)
		{
			_queue = new T[minimumCapacity];
			_count = 0;
			_minimumCapacity = minimumCapacity;
		}

		#endregion

		#region Properties

		/// <summary>Gets the number of items in the list.</summary>
		/// <remarks>Runtime: O(1).</remarks>
		public int Count
		{
			get
			{
				return _count;
			}
		}

		/// <summary>Gets the current capacity of the list.</summary>
		/// <remarks>Runtime: O(1).</remarks>
		public int CurrentCapacity
		{
			get
			{
				return _queue.Length;
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
				else if (value > _queue.Length)
				{
					T[] newList = new T[value];
					_queue.CopyTo(newList, 0);
					_queue = newList;
				}
				else
					_minimumCapacity = value;
			}
		}

		/// <summary>The newest item currently in the queue.</summary>
		public T Newest
		{
			get
			{
				if (this._count > 0)
					return this._queue[(this._start + this._count) % this._queue.Length];
				else
					throw new System.InvalidOperationException("attempting to get the newest item in an empty queue");
			}
		}

		/// <summary>The oldest item currently in the queue.</summary>
		public T Oldest
		{
			get
			{
				if (_count > 0)
				{
					return _queue[_start];
				}
				else
				{
					throw new InvalidOperationException("attempting to get the oldet item in an empty queue");
				}
			}
		}

		#endregion

		#region Methods

		#region ToArray

		/// <summary>Converts the list array into a standard array.</summary>
		/// <returns>A standard array of all the elements.</returns>
		public T[] ToArray()
		{
			T[] array = new T[_count];
			for (int i = 0; i < _count; i++)
			{
				array[i] = _queue[i];
			}
			return array;
		}

		#endregion

		#region Clone

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public IDataStructure<T> Clone()
		{
			QueueArray<T> clone = new QueueArray<T>
			{
				_queue = new T[_queue.Length],
				_count = this._count,
				_start = this._start,
				_minimumCapacity = this._minimumCapacity,
			};
			T[] array = clone._queue;
			for (int i = 0; i < _count; i++)
			{
				array[i] = _queue[i];
			}
			return clone;
		}

		#endregion

		#region Enqueue

		/// <summary>Adds an item to the end of the list.</summary>
		/// <param name="addition">The item to be added.</param>
		/// <remarks>Runtime: O(n), EstAvg(1). </remarks>
		public void Enqueue(T addition)
		{
			if (_count == _queue.Length)
			{
				if (_queue.Length > int.MaxValue / 2)
				{
					throw new InvalidOperationException("your queue is so large that it can no longer double itself (Int32.MaxValue barrier reached).");
				}
				T[] newQueue = new T[_queue.Length * 2];
				for (int i = 0; i < _count; i++)
				{
					newQueue[i] = _queue[(i + _start) % _queue.Length];
				}
				_start = 0;
				_queue = newQueue;
			}
			_queue[(_start + _count++) % _queue.Length] = addition;
		}

		#endregion

		#region Dequeue

		/// <summary>Removes the item at a specific index.</summary>
		/// <runtime>O(n - index)</runtime>
		public T Dequeue()
		{
			if (_count == 0)
			{
				throw new InvalidOperationException("attempting to queue from an empty queue.");
			}
			if (_count < _queue.Length / 4 && _queue.Length / 2 > _minimumCapacity)
			{
				T[] newQueue = new T[_queue.Length / 2];
				for (int i = 0; i < _count; i++)
				{
					newQueue[i] = _queue[(i + _start) % _queue.Length];
				}
				_start = 0;
				_queue = newQueue;
			}
			T returnValue = _queue[_start++];
			_count--;
			if (_count == 0)
			{
				_start = 0;
			}
			return returnValue;
		}

		#endregion

		#region Peek

		/// <summary>Gets the next item in the queue without removing it.</summary>
		/// <returns>The next item in the queue.</returns>
		public T Peek()
		{
			T returnValue = _queue[_start];
			return returnValue;
		}

		#endregion

		#region Clear

		/// <summary>Empties the list back and reduces it back to its original capacity.</summary>
		/// <runtime>O(1)</runtime>
		public void Clear()
		{
			_queue = new T[_minimumCapacity];
			_count = 0;
		}

		#endregion

		#region Stepper And IEnumerable

		#region Stepper

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Step<T> step) => _queue.Stepper(step, 0, _count);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(StepRef<T> step) => _queue.Stepper(step, 0, _count);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepBreak<T> step) => _queue.Stepper(step, 0, _count);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepRefBreak<T> step) => _queue.Stepper(step, 0, _count);

		#endregion

		#region IEnumerable

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Gets the enumerator for this queue.</summary>
		/// <returns>The enumerator for this queue.</returns>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			for (int i = 0; i < _count; i++)
			{
				yield return _queue[i];
			}
		}

		#endregion

		#endregion

		#endregion
	}
}
