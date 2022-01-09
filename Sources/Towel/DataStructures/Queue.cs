namespace Towel.DataStructures;

/// <summary>Implements First-In-First-Out queue data structure.</summary>
/// <typeparam name="T">The generic type within the structure.</typeparam>
public interface IQueue<T> : IDataStructure<T>,
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
public class QueueLinked<T> : IQueue<T>, ICloneable<QueueLinked<T>>
{
	internal Node? _head;
	internal Node? _tail;
	internal int _count;

	#region Nested Types

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
	/// Creates an instance of a queue.<br/>
	/// Runtime: O(1)
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

	/// <inheritdoc/>
	public T Newest =>
		_count <= 0 ? throw new InvalidOperationException("attempting to get the newest item in an empty queue") :
		_tail is null ? throw new TowelBugException($"{nameof(Count)} is greater than 0 but {nameof(_tail)} is null") :
		_tail.Value;

	/// <inheritdoc/>
	public T Oldest =>
		_count <= 0 ? throw new InvalidOperationException("attempting to get the oldet item in an empty queue") :
		_head is null ? throw new TowelBugException($"{nameof(Count)} is greater than 0 but {nameof(_head)} is null") :
		_head.Value;

	/// <inheritdoc/>
	public int Count => _count;

	#endregion

	#region Methods

	/// <inheritdoc/>
	public T[] ToArray()
	{
		if (_count is 0)
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

	/// <inheritdoc/>
	public QueueLinked<T> Clone() => new(this);

	/// <inheritdoc/>
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

	/// <inheritdoc/>
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

	/// <inheritdoc/>
	public T Peek() =>
		_count <= 0 ? throw new InvalidOperationException("attempting to peek an empty queue") :
		_head is null ? throw new TowelBugException($"{nameof(Count)} is greater than 0 but {nameof(_head)} is null") :
		_head.Value;

	/// <inheritdoc/>
	public void Clear()
	{
		_head = null;
		_tail = null;
		_count = 0;
	}

	/// <inheritdoc/>
	public StepStatus StepperBreak<TStep>(TStep step = default)
		where TStep : struct, IFunc<T, StepStatus>
	{
		for (Node? node = _head; node is not null; node = node.Next)
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
		for (Node? node = _head; node is not null; node = node.Next)
		{
			yield return node.Value;
		}
	}

	#endregion
}

/// <summary>Implements First-In-First-Out queue data structure using an array.</summary>
/// <typeparam name="T">The generic type within the structure.</typeparam>
public class QueueArray<T> : IQueue<T>, ICloneable<QueueArray<T>>
{
	internal const int DefaultMinimumCapacity = 1;

	internal T[] _array;
	internal int _start;
	internal int _count;
	internal int? _minimumCapacity;

	#region Constructors

	/// <summary>Constructs a new queue.</summary>
	public QueueArray()
	{
		_array = new T[DefaultMinimumCapacity];
		_count = 0;
	}

	/// <summary>Constructs a new queue.</summary>
	/// <param name="minimumCapacity">The initial and smallest array size allowed by this list.</param>
	public QueueArray(int minimumCapacity)
	{
		if (minimumCapacity <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(minimumCapacity), minimumCapacity, $"{nameof(minimumCapacity)} <= 0");
		}
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

	/// <inheritdoc/>
	public int Count => _count;

	/// <summary>Gets the current capacity of the list.</summary>
	public int CurrentCapacity => _array.Length;

	/// <summary>
	/// Allows you to adjust the minimum capacity of this list.<br/>
	/// Runtime (Get): O(1)<br/>
	/// Runtime (Set): O(n), Ω(1)
	/// </summary>
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
				throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(value)} < 1");
			}
			if (value > _array.Length)
			{
				T[] array = new T[_array.Length * 2];
				for (int i = 0, index = _start; i < _count; i++, index = ++index >= _array.Length ? 0 : index)
				{
					array[i] = _array[index];
				}
				_start = 0;
				_array = array;
			}
			_minimumCapacity = value;
		}
	}

	/// <inheritdoc/>
	public T Newest =>
		_count <= 0 ? throw new InvalidOperationException("attempting to get the newest item in an empty queue") :
		_array[(_start + _count - 1) % _array.Length];

	/// <inheritdoc/>
	public T Oldest =>
		_count <= 0 ? throw new InvalidOperationException("attempting to get the oldet item in an empty queue") :
		_array[_start];

	/// <summary>
	/// Gets or sets the <typeparamref name="T"/> at an index in the queue.<br/>
	/// Runtime: O(1)
	/// </summary>
	/// <param name="index">The index of the <typeparamref name="T"/> to get or set.</param>
	/// <returns>The element at the provided index.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Thrown when: index &lt;= 0 || index &gt; _count</exception>
	public T this[int index]
	{
		get =>
			index < 0 || index >= _count ? throw new ArgumentOutOfRangeException(nameof(index), index, $"!(0 <= {nameof(index)} < {nameof(Count)})") :
			_array[(_start + index) % _array.Length];
		set
		{
			if (index < 0 || index >= _count) throw new ArgumentOutOfRangeException(nameof(index), index, $"!(0 <= {nameof(index)} < {nameof(Count)})");
			_array[(_start + index) % _array.Length] = value;
		}
	}

	#endregion

	#region Methods

	/// <inheritdoc/>
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

	/// <inheritdoc/>
	public QueueArray<T> Clone() => new(this);

	/// <inheritdoc/>
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

	/// <inheritdoc/>
	public T Dequeue()
	{
		if (_count is 0)
		{
			throw new InvalidOperationException("attempting to queue from an empty queue.");
		}
		if (_count < _array.Length / 4 && _array.Length > _minimumCapacity)
		{
			int length = Math.Max(_minimumCapacity ?? DefaultMinimumCapacity, _array.Length / 2);
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

	/// <inheritdoc/>
	public T Peek() =>
		_count <= 0 ? throw new InvalidOperationException("attempting to peek an empty queue") :
		_array[_start];

	/// <inheritdoc/>
	public void Clear()
	{
		_count = 0;
		_start = 0;
	}

	/// <inheritdoc/>
	public StepStatus StepperBreak<TStep>(TStep step = default)
		where TStep : struct, IFunc<T, StepStatus>
	{
		for (int i = 0, index = _start; i < _count; i++, index = ++index >= _array.Length ? 0 : index)
		{
			if (step.Invoke(_array[index]) is Break)
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
