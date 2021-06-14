#if false

using System;
using static Towel.Syntax;

namespace Towel.DataStructures
{
	public interface IDoubleEndedQueue<T> : IDataStructure<T>,
		DataStructure.ICountable,
		DataStructure.IClearable
	{
		#region Methods

		void EnqueueFront(T enqueue);
		void EnqueueBack(T enqueue);
		T DequeueFront();
		T DequeueBack();
		T PeekFront();
		T PeekBack();

		#endregion
	}

	public class DoubleEndedQueueLinked<T> : IDoubleEndedQueue<T>
	{
		internal Node _head;
		internal Node _tail;
		internal int _count;

		#region Nested Types

		/// <summary>This class just holds the data for each individual node of the list.</summary>
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

		/// <summary>
		/// Creates an instance of a queue.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public DoubleEndedQueueLinked()
		{
			_head = _tail = null;
			_count = 0;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns the number of items in the queue.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public int Count => _count;

		#endregion

		#region Methods

		/// <summary>
		/// Adds an item to the back of the queue.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <param name="enqueue">The item to add to the queue.</param>
		public void EnqueueBack(T enqueue)
		{
			if (_tail is null)
			{
				_head = _tail = new Node(enqueue);
			}
			else
			{
				_tail = _tail.Next = new Node(enqueue);
			}
			_count++;
		}

		/// <summary>
		/// Removes the oldest item in the queue.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public T DequeueFront()
		{
			if (_head is null)
			{
				throw new InvalidOperationException("Attempting to remove a non-existing id value.");
			}
			T value = _head.Value;
			if (_head == _tail)
			{
				_tail = null;
			}
			_head = null;
			_count--;
			return value;
		}

		/// <summary>Looks at the front-most value.</summary>
		/// <returns>The front-most value.</returns>
		public T PeekFront()
		{
			if (_head is null)
			{
				throw new InvalidOperationException("Attempting to remove a non-existing id value.");
			}
			T returnValue = _head.Value;
			return returnValue;
		}

		/// <summary>
		/// Resets the queue to an empty state.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public void Clear()
		{
			_head = _tail = null;
			_count = 0;
		}

		/// <summary>
		/// Converts the list into a standard array.
		/// <para>Runtime: O(n)</para>
		/// </summary>
		/// <returns>A standard array of all the items.</returns>
		public T[] ToArray()
		{
			if (_count == 0)
			{
				return null;
			}
			T[] array = new T[_count];
			Node node = _head;
			for (int i = 0; i < _count; i++)
			{
				array[i] = node.Value;
				node = node.Next;
			}
			return array;
		}

		#region Stepper and IEnumerable

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Action<T> step)
		{
			for (Node current = _head; current is not null; current = current.Next)
			{
				step(current.Value);
			}
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(StepRef<T> step)
		{
			for (Node current = _head; current is not null; current = current.Next)
			{
				step(ref current.Value);
			}
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(Func<T, StepStatus> step)
		{
			for (Node current = _head; current is not null; current = current.Next)
			{
				if (step(current.Value) is Break)
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
			for (Node current = _head; current is not null; current = current.Next)
			{
				if (step(ref current.Value) is Break)
				{
					return Break;
				}
			}
			return Continue;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Gets the enumerator for this dequeue.</summary>
		/// <returns>The enumerator for this dequeue.</returns>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			for (Node current = _head; current is not null; current = current.Next)
			{
				yield return current.Value;
			}
		}

		#endregion

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public IDataStructure<T> Clone()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}

#endif
