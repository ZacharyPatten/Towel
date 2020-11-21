using System;
using System.Security;
using static Towel.Statics;

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

		/// <summary>The current newest element in the queue.</summary>
		T Newest { get; }
		/// <summary>The current oldest element in the queue.</summary>
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
	public class QueueLinked<T> : IQueue<T>
	{
		internal Node? _head;
		internal Node? _tail;
		internal int _count;

		#region Node

		internal class Node
		{
			internal T Value;
			internal Node? Next;

			internal Node(T value, Node? next = null)
			{
				Value = value;
				Next = next;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates an instance of a queue.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public QueueLinked()
		{
			_head = _tail = null;
			_count = 0;
		}

		internal QueueLinked(QueueLinked<T> queue)
		{
			if (queue._head is not null)
			{
				_head = new Node(value: queue._head.Value);
				Node? a = queue._head.Next;
				Node? b = _head;
				while (a is not null)
				{
					b.Next = new Node(value: a.Value);
					b = b.Next;
					a = a.Next;
				}
				_tail = b;
				_count = queue._count;
			}
		}

		#endregion

		#region Properties

		/// <summary>
		/// The current newest element in the queue.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public T Newest =>
			_count <= 0 ? throw new InvalidOperationException("attempting to get the newest item in an empty queue") :
			_tail is null ? throw new TowelBugException($"{nameof(Count)} is greater than 0 but {nameof(_tail)} is null") :
			_tail.Value;

		/// <summary>
		/// The current oldest element in the queue.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public T Oldest =>
			_count <= 0 ? throw new InvalidOperationException("attempting to get the oldet item in an empty queue") :
			_head is null ? throw new TowelBugException($"{nameof(Count)} is greater than 0 but {nameof(_head)} is null") :
			_head.Value;

		/// <summary>
		/// Returns the number of items in the queue.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public int Count => _count;

		#endregion

		#region Methods

		/// <summary>
		/// Converts the list into a standard array.
		/// <para>Runtime: O(n)</para>
		/// </summary>
		/// <returns>A standard array of all the items.</returns>
		public T[] ToArray()
		{
			if (_count == 0)
			{
				return Array.Empty<T>();
			}
			T[] array = new T[_count];
			for (var (i, node) = (0, _head); node is not null; node = node.Next, i++)
			{
				array[i] = node.Value;
			}
			return array;
		}

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public QueueLinked<T> Clone() => new QueueLinked<T>(this);

		/// <summary>
		/// Adds an item to the back of the queue.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <param name="enqueue">The item to add to the queue.</param>
		public void Enqueue(T enqueue)
		{
			if (_tail is null)
			{
				_head = _tail = new Node(value: enqueue);
			}
			else
			{
				_tail = _tail.Next = new Node(value: enqueue);
			}
			_count++;
		}

		/// <summary>
		/// Removes the oldest item in the queue.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <returns>The next item in the queue.</returns>
		public T Dequeue()
		{
			if (_head is null)
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

		/// <summary>
		/// Gets the next item in the queue without removing it.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <returns>The next item in the queue.</returns>
		public T Peek() =>
			_count <= 0 ? throw new InvalidOperationException("attempting to peek an empty queue") :
			_head is null ? throw new TowelBugException($"{nameof(Count)} is greater than 0 but {nameof(_head)} is null") :
			_head.Value;

		/// <summary>
		/// Resets the queue to an empty state.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public void Clear()
		{
			_head = null;
			_tail = null;
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
			for (Node? current = _head; current is not null; current = current.Next)
			{
				if (step.Do(ref current.Value) is Break)
				{
					return Break;
				}
			}
			return Continue;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>
		/// Gets the enumerator for this queue.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <returns>The enumerator for the queue.</returns>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			for (Node? current = _head; current is not null; current = current.Next)
			{
				yield return current.Value;
			}
		}

		#endregion
	}

	/// <summary>Implements First-In-First-Out queue data structure using an array.</summary>
	/// <typeparam name="T">The generic type within the structure.</typeparam>
	public class QueueArray<T> : IQueue<T>
	{
		internal T[] _array;
		internal int _start;
		internal int _count;
		internal int _minimumCapacity;

		#region Constructors

		/// <summary>
		/// Constructs a new queue.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public QueueArray() : this(1) { }

		/// <summary>
		/// Constructs a new queue.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <param name="minimumCapacity">The initial and smallest array size allowed by this list.</param>
		public QueueArray(int minimumCapacity)
		{
			_array = new T[minimumCapacity];
			_count = 0;
			_minimumCapacity = minimumCapacity;
		}

		internal QueueArray(QueueArray<T> queue)
		{
			_minimumCapacity = queue._minimumCapacity;
			_start = queue._start;
			_count = queue._count;
			_array = (T[])queue._array.Clone();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the number of items in the list.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public int Count => _count;

		/// <summary>
		/// Gets the current capacity of the list.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public int CurrentCapacity => _array.Length;

		/// <summary>
		/// Allows you to adjust the minimum capacity of this list.
		/// <para>Runtime (Get): O(1)</para>
		/// <para>Runtime (Set): O(n), Ω(1)</para>
		/// </summary>
		public int MinimumCapacity
		{
			get => _minimumCapacity;
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(value)} < 1");
				}
				if (value > _array.Length)
				{
					T[] array = new T[value];
					_array.CopyTo(array, 0);
					_array = array;
				}
				_minimumCapacity = value;
			}
		}

		/// <summary>
		/// The current newest <typeparamref name="T"/> in the queue.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <exception cref="InvalidOperationException">Thrown when: _count &lt;= 0</exception>
		public T Newest =>
			_count <= 0 ? throw new InvalidOperationException("attempting to get the newest item in an empty queue") :
			_array[(_start + _count) % _array.Length];

		/// <summary>
		/// The current newest <typeparamref name="T"/> in the queue.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <exception cref="InvalidOperationException">Thrown when: _count &lt;= 0</exception>
		public T Oldest =>
			_count <= 0 ? throw new InvalidOperationException("attempting to get the oldet item in an empty queue") :
			_array[_start];

		/// <summary>
		/// Gets or sets the <typeparamref name="T"/> at an index in the queue.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		/// <param name="index">The index of the <typeparamref name="T"/> to get or set.</param>
		/// <returns>The element at the provided index.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when: index &lt;= 0 || index &gt; _count</exception>
		public T this[int index]
		{
			get =>
				index <= 0 || index > _count ? throw new ArgumentOutOfRangeException(nameof(index), index, $"!(0 <= {nameof(index)} < {nameof(Count)})") :
				_array[(_start + index) % _array.Length];
			set
			{
				if (index <= 0 || index > _count) throw new ArgumentOutOfRangeException(nameof(index), index, $"!(0 <= {nameof(index)} < {nameof(Count)})");
				_array[(_start + index) % _array.Length] = value;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Converts the list array into a standard array.
		/// <para>Runtime: O(<see cref="Count"/>)</para>
		/// </summary>
		/// <returns>A standard array of all the elements.</returns>
		public T[] ToArray()
		{
			if (_count <= 0)
			{
				return Array.Empty<T>();
			}
			T[] array = new T[_count];
			for (int i = 0, index = _start; i < _count; i++, index = ++index >= _array.Length ? 0 : index)
			{
				array[i] = _array[index];
			}
			return array;
		}

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public QueueArray<T> Clone() => new QueueArray<T>(this);

		/// <summary>
		/// Adds an item to the end of the list.
		/// <para>Runtime: O(n), ε(1)</para>
		/// </summary>
		/// <param name="addition">The item to be added.</param>
		public void Enqueue(T addition)
		{
			if (_count + 1 >= _array.Length)
			{
				if (_array.Length > int.MaxValue / 2)
				{
					throw new InvalidOperationException("your queue is so large that it can no longer double itself (Int32.MaxValue barrier reached).");
				}
				T[] array = new T[_array.Length * 2];
				for (int i = 0, index = _start; i < _count; i++, index = ++index >= _array.Length ? 0 : index)
				{
					array[i] = _array[index];
				}
				_start = 0;
				_array = array;
			}
			int end = _start + _count;
			if (end >= _array.Length)
			{
				end %= _array.Length;
			}
			_array[end] = addition;
			_count++;
		}

		/// <summary>
		/// Removes the item at a specific index.
		/// <para>Runtime: O(n), ε(1)</para>
		/// </summary>
		public T Dequeue()
		{
			if (_count == 0)
			{
				throw new InvalidOperationException("attempting to queue from an empty queue.");
			}
			if (_count < _array.Length / 4 && _array.Length > _minimumCapacity)
			{
				int length = Math.Max(_minimumCapacity, _array.Length / 2);
				T[] array = new T[length];
				for (int i = 0, index = _start; i < _count; i++, index = ++index >= _array.Length ? 0 : index)
				{
					array[i] = _array[index];
				}
				_start = 0;
				_array = array;
			}
			T element = _array[_start++];
			if (_start >= _array.Length)
			{
				_start = 0;
			}
			_count--;
			return element;
		}

		/// <summary>Gets the next item in the queue without removing it.</summary>
		/// <returns>The next item in the queue.</returns>
		public T Peek() =>
			_count <= 0 ? throw new InvalidOperationException("attempting to peek an empty queue") :
			_array[_start];

		/// <summary>
		/// Returns the queue to an empty state.
		/// <para>Runtime: O(1)</para>
		/// </summary>
		public void Clear()
		{
			_count = 0;
			_start = 0;
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
			for (int i = 0, index = _start; i < _count; i++, index = ++index >= _array.Length ? 0 : index)
			{
				if (step.Do(ref _array[index]) is Break)
				{
					return Break;
				}
			}
			return Continue;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Gets the enumerator for this queue.</summary>
		/// <returns>The enumerator for this queue.</returns>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			for (int i = 0, index = _start; i < _count; i++, index++)
			{
				if (index == _array.Length)
				{
					index = 0;
				}
				yield return _array[index];
			}
		}

		#endregion
	}
}
