using System;

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
        private Node _head;
        private Node _tail;
        private int _count;

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
        /// <runtime>θ(1)</runtime>
        public QueueLinked()
        {
            _head = _tail = null;
            _count = 0;
        }

        #endregion

        #region Properties

        /// <summary>The newest item currently in the queue.</summary>
        /// <runtime>θ(1)</runtime>
        public T Newest
        {
            get
            {
                if (_count > 0)
                {
                    return _tail.Value;
                }
                else
                {
                    throw new InvalidOperationException("attempting to get the newest item in an empty queue");
                }
            }
        }

        /// <summary>The oldest item currently in the queue.</summary>
        /// <runtime>θ(1)</runtime>
        public T Oldest
        {
            get
            {
                if (_count > 0)
                {
                    return _head.Value;
                }
                else
                {
                    throw new InvalidOperationException("attempting to get the oldet item in an empty queue");
                }
            }
        }

        /// <summary>Returns the number of items in the queue.</summary>
        /// <runtime>θ(1)</runtime>
        public int Count { get { return _count; } }

        #endregion

        #region Methods

        #region ToArray

        /// <summary>Converts the list into a standard array.</summary>
        /// <returns>A standard array of all the items.</returns>
        /// /// <remarks>Runtime: Towel(n).</remarks>
        public T[] ToArray()
        {
            if (_count == 0)
                return null;
            T[] array = new T[_count];
            Node looper = _head;
            for (int i = 0; i < _count; i++)
            {
                array[i] = looper.Value;
                looper = looper.Next;
            }
            return array;
        }

        #endregion

        #region Clone

        /// <summary>Creates a shallow clone of this data structure.</summary>
        /// <returns>A shallow clone of this data structure.</returns>
        public QueueLinked<T> Clone()
        {
            Node head = new Node(this._head.Value);
            Node current = this._head.Next;
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
                _count = this._count
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
                _head = _tail = new Node(enqueue);
            else
                _tail = _tail.Next = new Node(enqueue);
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
                throw new System.InvalidOperationException("peek from an empty queue");
            T returnValue = _head.Value;
            return returnValue;
        }

        #endregion

        #region Clear

        /// <summary>Resets the queue to an empty state.</summary>
        /// <remarks>Runtime: O(1).</remarks>
        public void Clear()
        {
            _head = _tail = null;
            _count = 0;
        }

        #endregion

        #region Stepper And IEnumerable

        #region public void Stepper(Step<T> function)
        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        public void Stepper(Step<T> function)
        {
            Node current = this._head;
            while (current != null)
            {
                function(current.Value);
                current = current.Next;
            }
        }
        #endregion
        #region public void Stepper(StepRef<T> function)
        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        public void Stepper(StepRef<T> function)
        {
            Node current = this._head;
            while (current != null)
            {
                T temp = current.Value;
                function(ref temp);
                current.Value = temp;
                current = current.Next;
            }
        }
        #endregion
        #region public StepStatus Stepper(StepBreak<T> function)
        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Stepper(StepBreak<T> function)
        {
            Node current = this._head;
            while (current != null)
            {
                switch (function(current.Value))
                {
                    case StepStatus.Break:
                        current = current.Next;
                        return StepStatus.Break;
                    case StepStatus.Continue:
                        current = current.Next;
                        continue;
                    default:
                        current = current.Next;
                        throw new System.NotImplementedException();
                }
            }
            return StepStatus.Continue;
        }
        #endregion
        #region public StepStatus Stepper(StepRefBreak<T> function)
        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Stepper(StepRefBreak<T> function)
        {
            Node current = this._head;
            while (current != null)
            {
                T temp = current.Value;
                switch (function(ref temp))
                {
                    case StepStatus.Break:
                        current.Value = temp;
                        current = current.Next;
                        return StepStatus.Break;
                    case StepStatus.Continue:
                        current.Value = temp;
                        current = current.Next;
                        continue;
                    default:
                        current.Value = temp;
                        current = current.Next;
                        throw new System.NotImplementedException();
                }
            }
            return StepStatus.Continue;
        }
        #endregion
        #region System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
        System.Collections.IEnumerator
            System.Collections.IEnumerable.GetEnumerator()
        {
            Node current = this._head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }
        #endregion
        #region System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
        /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
        System.Collections.Generic.IEnumerator<T>
            System.Collections.Generic.IEnumerable<T>.GetEnumerator()
        {
            Node current = this._head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }
        #endregion

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
        /// <param name="minimumCapacity">The initial and smallest array size allowed by this list.</param>
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
        /// <remarks>Runtime: Towel(n - index).</remarks>
        public T Dequeue()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("attempting to dequeue from an empty queue.");
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

        public T Peek()
        {
            T returnValue = _queue[_start];
            return returnValue;
        }

        #endregion

        #region Clear

        /// <summary>Empties the list back and reduces it back to its original capacity.</summary>
        /// <remarks>Runtime: O(1).</remarks>
        public void Clear()
        {
            _queue = new T[_minimumCapacity];
            _count = 0;
        }

        #endregion

        #region Stepper And IEnumerable

        #region Stepper

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step_function">The delegate to invoke on each item in the structure.</param>
        public void Stepper(Step<T> step_function)
        {
            for (int i = 0; i < _count; i++)
            {
                step_function(_queue[i]);
            }
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step_function">The delegate to invoke on each item in the structure.</param>
        public void Stepper(StepRef<T> step_function)
        {
            for (int i = 0; i < _count; i++)
            {
                step_function(ref _queue[i]);
            }
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step_function">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Stepper(StepBreak<T> step)
        {
            for (int i = 0; i < _count; i++)
            {
                if (step(_queue[i]) == StepStatus.Break)
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
            for (int i = 0; i < _count; i++)
            {
                if (step(ref _queue[i]) == StepStatus.Break)
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
                yield return _queue[i];
            }
        }

        System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
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
