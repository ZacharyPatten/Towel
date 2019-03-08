using Towel;

namespace Towel.DataStructures
{
	/// <summary>Stores items based on priorities and allows access to the highest priority item.</summary>
	/// <typeparam name="T">The generic type to be stored within the heap.</typeparam>
	public interface Heap<T> : DataStructure<T>,
		// Structure Properties
		DataStructure.Countable<T>,
		DataStructure.Clearable<T>,
		DataStructure.Comparing<T>
	{
		#region Methods

		/// <summary>Enqueues an item into the heap.</summary>
		/// <param name="addition"></param>
		void Enqueue(T addition);
		/// <summary>Removes and returns the highest priority item.</summary>
		/// <returns>The highest priority item from the queue.</returns>
		T Dequeue();
		/// <summary>Returns the highest priority item.</summary>
		/// <returns>The highest priority item in the queue.</returns>
		T Peek();

		#endregion
	}
	
	/// <summary>Implements a priority heap with static priorities using an array.</summary>
	/// <typeparam name="T">The type of item to be stored in this priority heap.</typeparam>
	/// <remarks>The runtimes of each public member are included in the "remarks" xml tags.</remarks>
	/// <citation>
	/// This heap imlpementation was originally developed by 
	/// Rodney Howell of Kansas State University. However, it has 
	/// been modified since its addition into the Towel framework.
	/// </citation>
	[System.Serializable]
	public class HeapArray<T> : Heap<T>
	{
        // Fields
        private Compare<T> _compare;
        private T[] _heap;
        private int _minimumCapacity;
        private int _count;
        private const int _root = 1; // The root index of the heap.

        #region Constructors

        /// <summary>Generates a priority queue with a capacity of the parameter. Runtime O(1).</summary>
        /// <param name="compare">Delegate determining the comparison technique used for sorting.</param>
        /// <remarks>Runtime: Theta(capacity).</remarks>
        public HeapArray(Compare<T> compare)
        {
            this._compare = compare;
            this._heap = new T[2];
            this._minimumCapacity = 1;
            this._count = 0;
        }

        /// <summary>Generates a priority queue with a capacity of the parameter. Runtime O(1).</summary>
        /// <param name="compare">Delegate determining the comparison technique used for sorting.</param>
        /// <param name="minimumCapacity">The capacity you want this priority queue to have.</param>
        /// <remarks>Runtime: Theta(capacity).</remarks>
        public HeapArray(Compare<T> compare, int minimumCapacity)
        {
            this._compare = compare;
            this._heap = new T[minimumCapacity + 1];
            this._minimumCapacity = minimumCapacity;
            this._count = 0;
        }

        #endregion

        #region Properties

        /// <summary>Delegate determining the comparison technique used for sorting.</summary>
        public Compare<T> Compare { get { return this._compare; } }
        /// <summary>The maximum items the queue can hold.</summary>
        /// <remarks>Runtime: O(1).</remarks>
        public int CurrentCapacity { get { return this._heap.Length - 1; } }
        /// <summary>The minumum capacity of this queue to limit low-level resizing.</summary>
        public int MinimumCapacity { get { return this._minimumCapacity; } }
        /// <summary>The number of items in the queue.</summary
        /// <remarks>Runtime: O(1).</remarks>
        public int Count { get { return this._count; } }

        #endregion

        #region Methods

        /// <summary>Gets the index of the left child of the provided item.</summary>
        /// <param name="parent">The item to find the left child of.</param>
        /// <returns>The index of the left child of the provided item.</returns>
        private static int LeftChild(int parent)
        {
            return parent * 2;
        }

        /// <summary>Gets the index of the right child of the provided item.</summary>
        /// <param name="parent">The item to find the right child of.</param>
        /// <returns>The index of the right child of the provided item.</returns>
        private static int RightChild(int parent)
        {
            return parent * 2 + 1;
        }

        /// <summary>Gets the index of the parent of the provided item.</summary>
        /// <param name="child">The item to find the parent of.</param>
        /// <returns>The index of the parent of the provided item.</returns>
        private static int Parent(int child)
        {
            return child / 2;
        }

        /// <summary>Enqueue an item into the priority queue and let it works its magic.</summary>
        /// <param name="addition">The item to be added.</param>
        /// <param name="priority">The priority of the addition. (LARGER PRIORITY -> HIGHER PRIORITY)</param>
        /// <remarks>Runtime: O(ln(n)), Omega(1), EstAvg(ln(n)).</remarks>
        public void Enqueue(T addition)
        {
            if (!(_count + 1 < _heap.Length))
            {
                if (_heap.Length * 2 > int.MaxValue)
                    throw new System.InvalidOperationException("this heap has become too large");
                T[] _newHeap = new T[_heap.Length * 2];
                for (int i = 1; i <= _count; i++)
                    _newHeap[i] = this._heap[i];
                this._heap = _newHeap;
            }
            this._count++;
            this._heap[this._count] = addition;
            this.ShiftUp(this._count);
        }

        /// <summary>Dequeues the item with the highest priority.</summary>
        /// <returns>The item of the highest priority.</returns>
        /// <remarks>Runtime: Theta(ln(n)).</remarks>
        public T Dequeue()
        {
            if (_count > 0)
            {
                T removal = this._heap[_root];
                this.ArraySwap(_root, this._count);
                this._count--;
                this.ShiftDown(_root);
                return removal;
            }
            throw new System.InvalidOperationException("Attempting to remove from an empty priority queue.");
        }

        /// <summary>Requeues an item after a change has occured.</summary>
        /// <param name="item">The item to requeue.</param>
        /// <remarks>Runtime: O(n).</remarks>
        public void Requeue(T item)
        {
            int i;
            for (i = 1; i <= this._count; i++)
                if (this._compare(item, this._heap[i]) == Comparison.Equal)
                    break;
            if (i > this._count)
                throw new System.InvalidOperationException("Attempting to re-queue an item that is not in the heap.");
            ShiftUp(i);
            ShiftDown(i);
        }

        /// <summary>This lets you peek at the top priority WITHOUT REMOVING it.</summary>
        /// <remarks>Runtime: O(1).</remarks>
        public T Peek()
        {
            if (this._count > 0)
                return this._heap[_root];
            throw new System.InvalidOperationException("Attempting to peek at an empty priority queue.");
        }

        /// <summary>Standard priority queue algorithm for up sifting.</summary>
        /// <param name="index">The index to be up sifted.</param>
        /// <remarks>Runtime: O(ln(n)), Omega(1).</remarks>
        private void ShiftUp(int index)
        {
            int parent;
            while (
                (parent = Parent(index)) > 0 &&
                this._compare(_heap[index], _heap[parent]) == Comparison.Greater)
            {
                this.ArraySwap(index, parent);
                index = parent;
            }
        }

        /// <summary>Standard priority queue algorithm for sifting down.</summary>
        /// <param name="index">The index to be down sifted.</param>
        /// <remarks>Runtime: O(ln(n)), Omega(1).</remarks>
        private void ShiftDown(int index)
        {
            int leftChild, rightChild;
            while ((leftChild = LeftChild(index)) <= this._count)
            {
                int down = leftChild;
                if ((rightChild = RightChild(index)) <= this._count &&
                    this._compare(this._heap[rightChild], this._heap[leftChild]) == Comparison.Greater)
                    down = rightChild;
                if (this._compare(this._heap[down], this._heap[index]) == Comparison.Less)
                    break;
                this.ArraySwap(index, down);
                index = down;
            }
        }

        /// <summary>Standard array swap method.</summary>
        /// <param name="indexOne">The first index of the swap.</param>
        /// <param name="indexTwo">The second index of the swap.</param>
        /// <remarks>Runtime: O(1).</remarks>
        private void ArraySwap(int indexOne, int indexTwo)
        {
            T temp = this._heap[indexTwo];
            this._heap[indexTwo] = this._heap[indexOne];
            this._heap[indexOne] = temp;
        }

        /// <summary>Returns this queue to an empty state.</summary>
        /// <remarks>Runtime: O(1).</remarks>
        public void Clear() { this._count = 0; }

        /// <summary>Converts the heap into an array using pre-order traversal (WARNING: items are not ordered).</summary>
        /// <returns>The array of priority-sorted items.</returns>
        public T[] ToArray()
        {
            T[] array = new T[this._count];
            for (int i = 1; i <= this._count; i++)
                array[i] = this._heap[i];
            return array;
        }

        /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
        System.Collections.IEnumerator
            System.Collections.IEnumerable.GetEnumerator()
        {
            for (int i = 0; i <= _count; i++)
                yield return this._heap[i];
        }

        /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
        System.Collections.Generic.IEnumerator<T>
            System.Collections.Generic.IEnumerable<T>.GetEnumerator()
        {
            for (int i = 0; i <= _count; i++)
                yield return this._heap[i];
        }

        /// <summary>Checks to see if a given object is in this data structure.</summary>
        /// <param name="item">The item to check for.</param>
        /// <param name="compare">Delegate representing comparison technique.</param>
        /// <returns>true if the item is in this structure; false if not.</returns>
        public bool Contains(T item, Compare<T> compare)
        {
            for (int i = 1; i <= this._count; i++)
                if (compare(this._heap[i], item) == Comparison.Equal)
                    return true;
            return false;
        }

        /// <summary>Checks to see if a given object is in this data structure.</summary>
        /// <typeparam name="Key">The type of the key to check for.</typeparam>
        /// <param name="key">The key to check for.</param>
        /// <param name="compare">Delegate representing comparison technique.</param>
        /// <returns>true if the item is in this structure; false if not.</returns>
        public bool Contains<Key>(Key key, Compare<T, Key> compare)
        {
            for (int i = 1; i <= this._count; i++)
                if (compare(this._heap[i], key) == Comparison.Equal)
                    return true;
            return false;
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        public void Stepper(Step<T> function)
        {
            for (int i = 1; i <= this._count; i++)
                function(this._heap[i]);
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        public void Stepper(StepRef<T> function)
        {
            for (int i = 1; i <= this._count; i++)
                function(ref this._heap[i]);
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Stepper(StepBreak<T> function)
        {
            for (int i = 1; i <= this._count; i++)
                if (function(this._heap[i]) == StepStatus.Break)
                    return StepStatus.Break;
            return StepStatus.Continue;
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="function">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        public StepStatus Stepper(StepRefBreak<T> function)
        {
            for (int i = 1; i <= this._count; i++)
                if (function(ref this._heap[i]) == StepStatus.Break)
                    return StepStatus.Break;
            return StepStatus.Continue;
        }

        /// <summary>Creates a shallow clone of this data structure.</summary>
        /// <returns>A shallow clone of this data structure.</returns>
        public DataStructure<T> Clone()
        {
            HeapArray<T> clone =
                new HeapArray<T>(this._compare);
            T[] cloneItems = new T[this._heap.Length];
            for (int i = 1; i <= this._count; i++)
                cloneItems[i] = _heap[i];
            clone._heap = cloneItems;
            clone._count = this._count;
            clone._minimumCapacity = this._minimumCapacity;
            return clone;
        }

        #endregion

        #region needs to be reviewed

        ///// <summary>Traversal function for a heap. Following a pre-order traversal.</summary>
        ///// <param name="traversalFunction">The function to perform per iteration.</param>
        ///// <returns>A determining a break in the traversal. (true = continue, false = break)</returns>
        //public bool TraverseBreakable(Func<Type, bool> traversalFunction) { return TraversalPreOrderBreakable(traversalFunction); }

        ///// <summary>Traversal function for a heap. Following a pre-order traversal.</summary>
        ///// <param name="traversalFunction">The function to perform per iteration.</param>
        //public void Traverse(Action<T> traversalFunction) { TraversalPreOrder(traversalFunction); }

        ///// <summary>Implements an imperative traversal of the structure.</summary>
        ///// <param name="traversalFunction">The function to perform per node in the traversal.</param>
        ///// <remarks>Runtime: O(n * traversalFunction).</remarks>
        //public bool TraversalPreOrderBreakable(Func<Type, bool> traversalFunction)
        //{
        //	for (int i = 0; i < _count; i++)
        //		if (!traversalFunction(_heapArray[i].Value))
        //			return false;
        //	return true;
        //}

        ///// <summary>Implements an imperative traversal of the structure.</summary>
        ///// <param name="traversalAction">The action to perform per node in the traversal.</param>
        ///// <remarks>Runtime: O(n * traversalAction).</remarks>
        //public void TraversalPreOrder(Action<T> traversalAction)
        //{
        //	for (int i = 0; i < _count; i++)
        //		traversalAction(_heapArray[i].Value);
        //}

        /// <summary>Pulls out all the values in the structure that are equivalent to the key.</summary>
        /// <typeparam name="Key">The type of the key to check for.</typeparam>
        /// <param name="key">The key to check for.</param>
        /// <param name="compare">Delegate representing comparison technique.</param>
        /// <returns>An array containing all the values matching the key or null if non were found.</returns>
        //Type[] GetValues<Key>(Key key, Compare<Type, Key> compare);

        /// <summary>Pulls out all the values in the structure that are equivalent to the key.</summary>
        /// <typeparam name="Key">The type of the key to check for.</typeparam>
        /// <param name="key">The key to check for.</param>
        /// <param name="compare">Delegate representing comparison technique.</param>
        /// <returns>An array containing all the values matching the key or null if non were found.</returns>
        /// <param name="values">The values that matched the given key.</param>
        /// <returns>true if 1 or more values were found; false if no values were found.</returns>
        //bool TryGetValues<Key>(Key key, Compare<Type, Key> compare, out Type[] values);

        #endregion
    }

    #region In Development

    #region HeapLinkedStatic
    /*
	/// <summary>Implements a mutable priority heap with static priorities using an array.</summary>
	/// <typeparam name="Type">The type of item to be stored in this priority heap.</typeparam>
	/// <remarks>The runtimes of each public member are included in the "remarks" xml tags.</remarks>
	public class HeapLinkedStatic<T> : Heap<T>
	{
		#region HeapLinkedStaticNode

		/// <summary>This is just a storage class, it stores an entry in the priority heap and its priority.</summary>
		private class HeapLinkedStaticNode
		{
			private HeapLinkedStaticNode _parent;
			private HeapLinkedStaticNode _leftChild;
			private HeapLinkedStaticNode _rightChild;
			private int _priority;
			private Type _value;

			internal HeapLinkedStaticNode Parent { get { return _parent; } set { _parent = value; } }
			internal HeapLinkedStaticNode LeftChild { get { return _leftChild; } set { _leftChild = value; } }
			internal HeapLinkedStaticNode RightChild { get { return _rightChild; } set { _rightChild = value; } }
			internal int Priority { get { return _priority; } set { _priority = value; } }
			internal Type Value { get { return _value; } set { _value = value; } }

			internal HeapLinkedStaticNode(HeapLinkedStaticNode parent, int left, Type right)
			{
				_parent = parent;
				_priority = left;
				_value = right;
			}
		}

		#endregion

		private HeapLinkedStaticNode[] _heapArray;
		private int _count;

		private object _lock;
		private int _readers;
		private int _writers;

		/// <summary>The maximum items the queue can hold.</summary>
		/// <remarks>Runtime: O(1).</remarks>
		public int Capacity { get { return _heapArray.Length - 1; } }

		/// <summary>The number of items in the queue.</summary
		/// <remarks>Runtime: O(1).</remarks>
		public int Count { get { return _count; } }

		/// <summary>True if full, false if there is still room.</summary>
		/// <remarks>Runtime: O(1).</remarks>
		public bool IsFull { get { return _count == _heapArray.Length - 1; } }

		/// <summary>Generates a priority queue with a capacity of the parameter. Runtime O(1).</summary>
		/// <param name="capacity">The capacity you want this priority queue to have.</param>
		/// <remarks>Runtime: Towel(capacity).</remarks>
		public HeapLinkedStatic(int capacity)
		{
			_heapArray = new HeapLinkedStaticNode[capacity + 1];
			_heapArray[0] = new HeapLinkedStaticNode(int.MaxValue, default(Type));
			for (int i = 1; i < capacity; i++)
				_heapArray[i] = new HeapLinkedStaticNode(int.MinValue, default(Type));
			_count = 0;
			_lock = new object();
		}

		/// <summary>Enqueue an item into the priority queue and let it works its magic.</summary>
		/// <param name="addition">The item to be added.</param>
		/// <param name="priority">The priority of the addition. (LARGER PRIORITY -> HIGHER PRIORITY)</param>
		/// <remarks>Runtime: O(ln(n)), Omega(1), EstAvg(ln(n)).</remarks>
		public void Enqueue(Type addition, int priority)
		{
			WriterLock();
			if (!(_count < _heapArray.Length - 1))
			{
				WriterUnlock();
				throw new HeapArrayStaticException("Attempting to add to a full priority queue.");
			}
			_count++;
			// Functionality Note: imutable or mutable (next three lines)
			//_queueArray[_count + 1] = new Link<int, Type>(priority, addition);
			_heapArray[_count].Priority = priority;
			_heapArray[_count].Value = addition;
			ShiftUp(_count);
			WriterUnlock();
		}

		/// <summary>Dequeues the item with the highest priority.</summary>
		/// <returns>The item of the highest priority.</returns>
		/// <remarks>Runtime: Towel(ln(n)).</remarks>
		public Type Dequeue()
		{
			WriterLock();
			if (_count > 0)
			{
				Type removal = _heapArray[1].Value;
				ArraySwap(1, _count);
				_count--;
				ShiftDown(1);
				WriterUnlock();
				return removal;
			}
			WriterUnlock();
			throw new HeapArrayStaticException("Attempting to remove from an empty priority queue.");
		}

		/// <summary>This lets you peek at the top priority WITHOUT REMOVING it.</summary>
		/// <remarks>Runtime: O(1).</remarks>
		public Type Peek()
		{
			ReaderLock();
			if (_count > 0)
			{
				ReaderUnlock();
				return _heapArray[1].Value;
			}
			ReaderUnlock();
			throw new HeapArrayStaticException("Attempting to peek at an empty priority queue.");
		}

		/// <summary>Standard priority queue algorithm for up sifting.</summary>
		/// <param name="index">The index to be up sifted.</param>
		/// <remarks>Runtime: O(ln(n)), Omega(1).</remarks>
		private void ShiftUp(int index)
		{
			// NOTE: "index * 2" is the index of the leftchild of the item at location "index"
			while (_heapArray[index].Priority > _heapArray[index / 2].Priority)
			{
				ArraySwap(index, index / 2);
				index = index / 2;
			}
		}

		/// <summary>Standard priority queue algorithm for sifting down.</summary>
		/// <param name="index">The index to be down sifted.</param>
		/// <remarks>Runtime: O(ln(n)), Omega(1).</remarks>
		private void ShiftDown(int index)
		{
			// NOTE: "index * 2" is the index of the leftchild of the item at location "index"
			while ((index * 2) <= _count)
			{
				int index2 = index * 2;
				if (((index * 2) + 1) <= _count && _heapArray[(index * 2) + 1].Priority < _heapArray[index].Priority) index2++;
				// NOTE: "(index * 2) + 1" is the index of the rightchild of the item at location "index"
				if (_heapArray[index].Priority >= _heapArray[index2].Priority) break;
				ArraySwap(index, index2);
				index = index2;
			}
		}

		/// <summary>Standard array swap method.</summary>
		/// <param name="indexOne">The first index of the swap.</param>
		/// <param name="indexTwo">The second index of the swap.</param>
		/// <remarks>Runtime: O(1).</remarks>
		private void ArraySwap(int indexOne, int indexTwo)
		{
			HeapLinkedStaticNode swapStorage = _heapArray[indexTwo];
			_heapArray[indexTwo] = _heapArray[indexOne];
			_heapArray[indexOne] = swapStorage;
		}

		/// <summary>Returns this queue to an empty state.</summary>
		/// <remarks>Runtime: O(1).</remarks>
		public void Clear() { WriterLock(); _count = 0; WriterUnlock(); }

		/// <summary>Traversal function for a heap. Following a pre-order traversal.</summary>
		/// <param name="traversalFunction">The function to perform per iteration.</param>
		/// <returns>A determining a break in the traversal. (true = continue, false = break)</returns>
		public bool TraverseBreakable(Func<Type, bool> traversalFunction) { return TraversalPreOrderBreakable(traversalFunction); }

		/// <summary>Traversal function for a heap. Following a pre-order traversal.</summary>
		/// <param name="traversalFunction">The function to perform per iteration.</param>
		public void Traverse(Action<T> traversalFunction) { TraversalPreOrder(traversalFunction); }

		/// <summary>Implements an imperative traversal of the structure.</summary>
		/// <param name="traversalFunction">The function to perform per node in the traversal.</param>
		/// <remarks>Runtime: O(n * traversalFunction).</remarks>
		public bool TraversalPreOrderBreakable(Func<Type, bool> traversalFunction)
		{
			ReaderLock();
			for (int i = 0; i < _count; i++)
			{
				if (!traversalFunction(_heapArray[i].Value))
				{
					ReaderUnlock();
					return false;
				}
			}
			ReaderUnlock();
			return true;
		}

		/// <summary>Implements an imperative traversal of the structure.</summary>
		/// <param name="traversalAction">The action to perform per node in the traversal.</param>
		/// <remarks>Runtime: O(n * traversalAction).</remarks>
		public void TraversalPreOrder(Action<T> traversalAction)
		{
			ReaderLock();
			for (int i = 0; i < _count; i++) traversalAction(_heapArray[i].Value);
			ReaderUnlock();
		}

		/// <summary>Converts the heap into an array using pre-order traversal (WARNING: items are not ordered).</summary>
		/// <returns>The array of priority-sorted items.</returns>
		public Type[] ToArray()
		{
			ReaderLock();
			Type[] array = new Type[_count];
			for (int i = 0; i < _count; i++) { array[i] = _heapArray[i].Value; }
			ReaderUnlock();
			return array;
		}

		/// <summary>Thread safe enterance for readers.</summary>
		private void ReaderLock() { lock (_lock) { while (!(_writers == 0)) Monitor.Wait(_lock); _readers++; } }
		/// <summary>Thread safe exit for readers.</summary>
		private void ReaderUnlock() { lock (_lock) { _readers--; Monitor.Pulse(_lock); } }
		/// <summary>Thread safe enterance for writers.</summary>
		private void WriterLock() { lock (_lock) { while (!(_writers == 0) && !(_readers == 0)) Monitor.Wait(_lock); _writers++; } }
		/// <summary>Thread safe exit for readers.</summary>
		private void WriterUnlock() { lock (_lock) { _writers--; Monitor.PulseAll(_lock); } }

		/// <summary>This is used for throwing imutable priority queue exceptions only to make debugging faster.</summary>
		private class HeapArrayStaticException : Exception { public HeapArrayStaticException(string message) : base(message) { } }
	}

	*/
    #endregion

    #region HeapArrayStatic

    ///// <summary>Implements a mutable priority heap with static priorities using an array.</summary>
    ///// <typeparam name="Type">The type of item to be stored in this priority heap.</typeparam>
    ///// <remarks>The runtimes of each public member are included in the "remarks" xml tags.</remarks>
    //[Serializable]
    //public class HeapArrayStatic<T> //: Heap<T>
    //{
    //	#region HeapArrayLink

    //	/// <summary>This is just a storage class, it stores an entry in the priority heap and its priority.</summary>
    //	private class HeapArrayLink
    //	{
    //		private int _priority;
    //		private Type _value;

    //		internal int Priority { get { return _priority; } set { _priority = value; } }
    //		internal Type Value { get { return _value; } set { _value = value; } }

    //		internal HeapArrayLink(int left, Type right)
    //		{
    //			_priority = left;
    //			_value = right;
    //		}
    //	}

    //	#endregion

    //	private HeapArrayLink[] _heapArray;
    //	private int _count;

    //	private object _lock;
    //	private int _readers;
    //	private int _writers;

    //	/// <summary>The maximum items the queue can hold.</summary>
    //	/// <remarks>Runtime: O(1).</remarks>
    //	public int Capacity { get { return _heapArray.Length - 1; } }

    //	/// <summary>The number of items in the queue.</summary
    //	/// <remarks>Runtime: O(1).</remarks>
    //	public int Count { get { return _count; } }

    //	/// <summary>True if full, false if there is still room.</summary>
    //	/// <remarks>Runtime: O(1).</remarks>
    //	public bool IsFull { get { return _count == _heapArray.Length - 1; } }

    //	/// <summary>Generates a priority queue with a capacity of the parameter. Runtime O(1).</summary>
    //	/// <param name="capacity">The capacity you want this priority queue to have.</param>
    //	/// <remarks>Runtime: Towel(capacity).</remarks>
    //	public HeapArrayStatic(int capacity)
    //	{
    //		_heapArray = new HeapArrayLink[capacity + 1];
    //		_heapArray[0] = new HeapArrayLink(int.MaxValue, default(Type));
    //		for (int i = 1; i < capacity + 1; i++)
    //			_heapArray[i] = new HeapArrayLink(int.MinValue, default(Type));
    //		_count = 0;
    //		_lock = new object();
    //	}

    //	/// <summary>Enqueue an item into the priority queue and let it works its magic.</summary>
    //	/// <param name="addition">The item to be added.</param>
    //	/// <param name="priority">The priority of the addition. (LARGER PRIORITY -> HIGHER PRIORITY)</param>
    //	/// <remarks>Runtime: O(ln(n)), Omega(1), EstAvg(ln(n)).</remarks>
    //	public void Enqueue(Type addition, int priority)
    //	{
    //		WriterLock();
    //		if (!(_count < _heapArray.Length - 1))
    //		{
    //			WriterUnlock();
    //			throw new HeapArrayStaticException("Attempting to add to a full priority queue.");
    //		}
    //		_count++;
    //		// Functionality Note: imutable or mutable (next three lines)
    //		//_queueArray[_count + 1] = new Link<int, Type>(priority, addition);
    //		_heapArray[_count].Priority = priority;
    //		_heapArray[_count].Value = addition;
    //		ShiftUp(_count);
    //		WriterUnlock();
    //	}

    //	/// <summary>Dequeues the item with the highest priority.</summary>
    //	/// <returns>The item of the highest priority.</returns>
    //	/// <remarks>Runtime: Towel(ln(n)).</remarks>
    //	public Type Dequeue()
    //	{
    //		WriterLock();
    //		if (_count > 0)
    //		{
    //			Type removal = _heapArray[1].Value;
    //			ArraySwap(1, _count);
    //			_count--;
    //			ShiftDown(1);
    //			WriterUnlock();
    //			return removal;
    //		}
    //		WriterUnlock();
    //		throw new HeapArrayStaticException("Attempting to remove from an empty priority queue.");
    //	}

    //	/// <summary>This lets you peek at the top priority WITHOUT REMOVING it.</summary>
    //	/// <remarks>Runtime: O(1).</remarks>
    //	public Type Peek()
    //	{
    //		ReaderLock();
    //		if (_count > 0)
    //		{
    //			ReaderUnlock();
    //			return _heapArray[1].Value;
    //		}
    //		ReaderUnlock();
    //		throw new HeapArrayStaticException("Attempting to peek at an empty priority queue.");
    //	}

    //	/// <summary>Standard priority queue algorithm for up sifting.</summary>
    //	/// <param name="index">The index to be up sifted.</param>
    //	/// <remarks>Runtime: O(ln(n)), Omega(1).</remarks>
    //	private void ShiftUp(int index)
    //	{
    //		// NOTE: "index * 2" is the index of the leftchild of the item at location "index"
    //		while (_heapArray[index].Priority > _heapArray[index / 2].Priority)
    //		{
    //			ArraySwap(index, index / 2);
    //			index = index / 2;
    //		}
    //	}

    //	/// <summary>Standard priority queue algorithm for sifting down.</summary>
    //	/// <param name="index">The index to be down sifted.</param>
    //	/// <remarks>Runtime: O(ln(n)), Omega(1).</remarks>
    //	private void ShiftDown(int index)
    //	{
    //		// NOTE: "index * 2" is the index of the leftchild of the item at location "index"
    //		while ((index * 2) <= _count)
    //		{
    //			int index2 = index * 2;
    //			if (((index * 2) + 1) <= _count && _heapArray[(index * 2) + 1].Priority < _heapArray[index].Priority) index2++;
    //			// NOTE: "(index * 2) + 1" is the index of the rightchild of the item at location "index"
    //			if (_heapArray[index].Priority >= _heapArray[index2].Priority) break;
    //			ArraySwap(index, index2);
    //			index = index2;
    //		}
    //	}

    //	/// <summary>Standard array swap method.</summary>
    //	/// <param name="indexOne">The first index of the swap.</param>
    //	/// <param name="indexTwo">The second index of the swap.</param>
    //	/// <remarks>Runtime: O(1).</remarks>
    //	private void ArraySwap(int indexOne, int indexTwo)
    //	{
    //		HeapArrayLink swapStorage = _heapArray[indexTwo];
    //		_heapArray[indexTwo] = _heapArray[indexOne];
    //		_heapArray[indexOne] = swapStorage;
    //	}

    //	/// <summary>Returns this queue to an empty state.</summary>
    //	/// <remarks>Runtime: O(1).</remarks>
    //	public void Clear() { WriterLock(); _count = 0; WriterUnlock(); }

    //	/// <summary>Traversal function for a heap. Following a pre-order traversal.</summary>
    //	/// <param name="traversalFunction">The function to perform per iteration.</param>
    //	/// <returns>A determining a break in the traversal. (true = continue, false = break)</returns>
    //	public bool TraverseBreakable(Func<Type, bool> traversalFunction) { return TraversalPreOrderBreakable(traversalFunction); }

    //	/// <summary>Traversal function for a heap. Following a pre-order traversal.</summary>
    //	/// <param name="traversalFunction">The function to perform per iteration.</param>
    //	public void Traverse(Action<T> traversalFunction) { TraversalPreOrder(traversalFunction); }

    //	/// <summary>Implements an imperative traversal of the structure.</summary>
    //	/// <param name="traversalFunction">The function to perform per node in the traversal.</param>
    //	/// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //	public bool TraversalPreOrderBreakable(Func<Type, bool> traversalFunction)
    //	{
    //		ReaderLock();
    //		for (int i = 0; i < _count; i++)
    //		{
    //			if (!traversalFunction(_heapArray[i].Value))
    //			{
    //				ReaderUnlock();
    //				return false;
    //			}
    //		}
    //		ReaderUnlock();
    //		return true;
    //	}

    //	/// <summary>Implements an imperative traversal of the structure.</summary>
    //	/// <param name="traversalAction">The action to perform per node in the traversal.</param>
    //	/// <remarks>Runtime: O(n * traversalAction).</remarks>
    //	public void TraversalPreOrder(Action<T> traversalAction)
    //	{
    //		ReaderLock();
    //		for (int i = 0; i < _count; i++) traversalAction(_heapArray[i].Value);
    //		ReaderUnlock();
    //	}

    //	/// <summary>Converts the heap into an array using pre-order traversal (WARNING: items are not ordered).</summary>
    //	/// <returns>The array of priority-sorted items.</returns>
    //	public Type[] ToArray()
    //	{
    //		ReaderLock();
    //		Type[] array = new Type[_count];
    //		for (int i = 0; i < _count; i++) { array[i] = _heapArray[i].Value; }
    //		ReaderUnlock();
    //		return array;
    //	}

    //	/// <summary>Thread safe enterance for readers.</summary>
    //	private void ReaderLock() { lock (_lock) { while (!(_writers == 0)) Monitor.Wait(_lock); _readers++; } }
    //	/// <summary>Thread safe exit for readers.</summary>
    //	private void ReaderUnlock() { lock (_lock) { _readers--; Monitor.Pulse(_lock); } }
    //	/// <summary>Thread safe enterance for writers.</summary>
    //	private void WriterLock() { lock (_lock) { while (!(_writers == 0) && !(_readers == 0)) Monitor.Wait(_lock); _writers++; } }
    //	/// <summary>Thread safe exit for readers.</summary>
    //	private void WriterUnlock() { lock (_lock) { _writers--; Monitor.PulseAll(_lock); } }

    //	#region Structure<T>

    //	#region .Net Framework Compatibility

    //	///// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    //	//IEnumerator IEnumerable.GetEnumerator()
    //	//{
    //	//	throw new NotImplementedException();
    //	//}

    //	///// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    //	//IEnumerator<T> IEnumerable<T>.GetEnumerator()
    //	//{
    //	//	throw new NotImplementedException();
    //	//}

    //	#endregion

    //	/// <summary>Pulls out all the values in the structure that are equivalent to the key.</summary>
    //	/// <typeparam name="Key">The type of the key to check for.</typeparam>
    //	/// <param name="key">The key to check for.</param>
    //	/// <param name="compare">Delegate representing comparison technique.</param>
    //	/// <returns>An array containing all the values matching the key or null if non were found.</returns>
    //	//Type[] GetValues<Key>(Key key, Compare<Type, Key> compare);

    //	/// <summary>Pulls out all the values in the structure that are equivalent to the key.</summary>
    //	/// <typeparam name="Key">The type of the key to check for.</typeparam>
    //	/// <param name="key">The key to check for.</param>
    //	/// <param name="compare">Delegate representing comparison technique.</param>
    //	/// <returns>An array containing all the values matching the key or null if non were found.</returns>
    //	/// <param name="values">The values that matched the given key.</param>
    //	/// <returns>true if 1 or more values were found; false if no values were found.</returns>
    //	//bool TryGetValues<Key>(Key key, Compare<Type, Key> compare, out Type[] values);

    //	/// <summary>Checks to see if a given object is in this data structure.</summary>
    //	/// <param name="item">The item to check for.</param>
    //	/// <param name="compare">Delegate representing comparison technique.</param>
    //	/// <returns>true if the item is in this structure; false if not.</returns>
    //	public bool Contains(Type item, Compare<T> compare)
    //	{
    //		throw new NotImplementedException();
    //	}

    //	/// <summary>Checks to see if a given object is in this data structure.</summary>
    //	/// <typeparam name="Key">The type of the key to check for.</typeparam>
    //	/// <param name="key">The key to check for.</param>
    //	/// <param name="compare">Delegate representing comparison technique.</param>
    //	/// <returns>true if the item is in this structure; false if not.</returns>
    //	public bool Contains<Key>(Key key, Compare<Type, Key> compare)
    //	{
    //		throw new NotImplementedException();
    //	}

    //	/// <summary>Invokes a delegate for each entry in the data structure.</summary>
    //	/// <param name="function">The delegate to invoke on each item in the structure.</param>
    //	public void Stepper(Step<T> function)
    //	{
    //		throw new NotImplementedException();
    //	}

    //	/// <summary>Invokes a delegate for each entry in the data structure.</summary>
    //	/// <param name="function">The delegate to invoke on each item in the structure.</param>
    //	public void Stepper(StepRef<T> function)
    //	{
    //		throw new NotImplementedException();
    //	}

    //	/// <summary>Invokes a delegate for each entry in the data structure.</summary>
    //	/// <param name="function">The delegate to invoke on each item in the structure.</param>
    //	/// <returns>The resulting status of the iteration.</returns>
    //	public StepStatus Stepper(StepBreak<T> function)
    //	{
    //		throw new NotImplementedException();
    //	}

    //	/// <summary>Invokes a delegate for each entry in the data structure.</summary>
    //	/// <param name="function">The delegate to invoke on each item in the structure.</param>
    //	/// <returns>The resulting status of the iteration.</returns>
    //	public StepStatus Stepper(StepRefBreak<T> function)
    //	{
    //		throw new NotImplementedException();
    //	}

    //	/// <summary>Creates a shallow clone of this data structure.</summary>
    //	/// <returns>A shallow clone of this data structure.</returns>
    //	public Structure<T> Clone()
    //	{
    //		throw new NotImplementedException();
    //	}

    //	///// <summary>Converts the structure into an array.</summary>
    //	///// <returns>An array containing all the item in the structure.</returns>
    //	//public Type[] ToArray()
    //	//{
    //	//	throw new NotImplementedException();
    //	//}

    //	#endregion

    //	/// <summary>This is used for throwing imutable priority queue exceptions only to make debugging faster.</summary>
    //	private class HeapArrayStaticException : System.Exception
    //	{
    //		public HeapArrayStaticException(string message) : base(message) { }
    //	}
    //}

    #endregion

    #region HeapArrayDynamic

    ///// <summary>Implements a mutable priority heap with dynamic priorities using an array and a hash table.</summary>
    ///// <typeparam name="Type">The type of item to be stored in this priority heap.</typeparam>
    ///// <remarks>The runtimes of each public member are included in the "remarks" xml tags.</remarks>
    //[Serializable]
    //public class HeapArrayDynamic<T> : Heap<T>
    //{
    //	#region HeapArrayDynamicLink

    //	/// <summary>This is just a storage class, it stores an entry in the priority heap and its priority.</summary>
    //	private class HeapArrayDynamicLink
    //	{
    //		private int _priority;
    //		private Type _value;

    //		internal int Priority { get { return _priority; } set { _priority = value; } }
    //		internal Type Value { get { return _value; } set { _value = value; } }

    //		internal HeapArrayDynamicLink(int left, Type right)
    //		{
    //			_priority = left;
    //			_value = right;
    //		}
    //	}

    //	#endregion

    //	private int _count;
    //	private HeapArrayDynamicLink[] _heapArray;
    //	private HashTableLinked<int, Type> _indexingReference;

    //	private object _lock;
    //	private int _readers;
    //	private int _writers;

    //	/// <summary>The maximum items the queue can hold.</summary>
    //	/// <remarks>Runtime: O(1).</remarks>
    //	public int Capacity { get { ReaderLock(); int capacity = _heapArray.Length - 1; ReaderUnlock(); return capacity; } }

    //	/// <summary>The number of items in the queue.</summary
    //	/// <remarks>Runtime: O(1).</remarks>
    //	public int Count { get { ReaderLock(); int count = _count; ReaderUnlock(); return count; } }

    //	/// <summary>True if full, false if there is still room.</summary>
    //	/// <remarks>Runtime: O(1).</remarks>
    //	public bool IsFull { get { ReaderLock(); bool isFull = _count == _heapArray.Length - 1; ReaderUnlock(); return isFull; } }

    //	/// <summary>Generates a priority queue with a capacity of the parameter.</summary>
    //	/// <param name="capacity">The capacity you want this priority queue to have.</param>
    //	/// <remarks>Runtime: Towel(capacity).</remarks>
    //	public HeapArrayDynamic(int capacity)
    //	{
    //		_indexingReference = new HashTableLinked<int, Type>();
    //		_heapArray = new HeapArrayDynamicLink[capacity + 1];
    //		_heapArray[0] = new HeapArrayDynamicLink(int.MaxValue, default(Type));
    //		for (int i = 1; i < capacity; i++)
    //			_heapArray[0] = new HeapArrayDynamicLink(int.MinValue, default(Type));
    //		_count = 0;
    //		_lock = new object();
    //	}

    //	/// <summary>Enqueue an item into the priority queue and let it works its magic.</summary>
    //	/// <param name="addition">The item to be added.</param>
    //	/// <param name="priority">The priority of the addition (LARGER PRIORITY -> HIGHER PRIORITY).</param>
    //	/// <remarks>Runtime: O(n), Omega(1), EstAvg(ln(n)).</remarks>
    //	public void Enqueue(Type addition, int priority)
    //	{
    //		WriterLock();
    //		if (!(_count < _heapArray.Length - 1))
    //		{
    //			WriterUnlock();
    //			throw new HeapArrayDynamicException("Attempting to add to a full priority queue.");
    //		}
    //		_count++;
    //		// Functionality Note: imutable or mutable (next three lines)
    //		//_queueArray[_count + 1] = new Link<int, Type>(priority, addition);
    //		_heapArray[_count].Priority = priority;
    //		_heapArray[_count].Value = addition;
    //		// Runtime Note: O(n) cause by hash table addition
    //		_indexingReference.Add(addition, _count);
    //		ShiftUp(_count);
    //		WriterUnlock();
    //	}

    //	/// <summary>This lets you peek at the top priority WITHOUT REMOVING it.</summary>
    //	/// <remarks>Runtime: O(1).</remarks>
    //	public Type Peek()
    //	{
    //		ReaderLock();
    //		if (_count > 0)
    //		{
    //			Type peek = _heapArray[1].Value;
    //			ReaderUnlock();
    //			return peek;
    //		}
    //		ReaderUnlock();
    //		throw new HeapArrayDynamicException("Attempting to peek at an empty priority queue.");
    //	}

    //	/// <summary>Dequeues the item with the highest priority.</summary>
    //	/// <returns>The item of the highest priority.</returns>
    //	/// <remarks>Runtime: O(n), Omega(ln(n)), EstAvg(ln(n)).</remarks>
    //	public Type Dequeue()
    //	{
    //		WriterLock();
    //		if (_count > 0)
    //		{
    //			Type removal = _heapArray[1].Value;
    //			ArraySwap(1, _count);
    //			_count--;
    //			// Runtime Note: O(n) caused by has table removal
    //			_indexingReference.Remove(removal);
    //			ShiftDown(1);
    //			WriterUnlock();
    //			return removal;
    //		}
    //		WriterUnlock();
    //		throw new HeapArrayDynamicException("Attempting to dequeue from an empty priority queue.");
    //	}

    //	/// <summary>Increases the priority of an item in the queue.</summary>
    //	/// <param name="item">The item to have its priority increased.</param>
    //	/// <param name="priority">The ammount to increase the priority by (LARGER INT -> HIGHER PRIORITY).</param>
    //	/// <remarks>Runtime: O(n), Omega(1), EstAvg(ln(n)).</remarks>
    //	public void IncreasePriority(Type item, int priority)
    //	{
    //		WriterLock();
    //		// Runtime Note: O(n) caused by hash table look-up.
    //		int index = _indexingReference[item];
    //		// Functionality Note: imutable or mutable (next two lines)
    //		//_queueArray[index] = new Link<int, Type>(_queueArray[index].Left + priority, item);
    //		_heapArray[index].Priority += priority;
    //		ShiftUp(index);
    //		WriterUnlock();
    //	}

    //	/// <summary>Decreases the priority of an item in the queue.</summary>
    //	/// <param name="item">The item to have its priority decreased.</param>
    //	/// <param name="priority">The ammount to decrease the priority by (LARGER INT -> HIGHER PRIORITY).</param>
    //	/// <remarks>Runtime: O(n), Omega(1), EstAvg(ln(n)).</remarks>
    //	public void DecreasePriority(Type item, int priority)
    //	{
    //		WriterLock();
    //		// Runtime Note: O(n) caused by hash table look-up.
    //		int index = _indexingReference[item];
    //		// Functionality Note: imutable or mutable (next two lines)
    //		//_queueArray[index] = new Link<int, Type>(_queueArray[index].Left - priority, item);
    //		_heapArray[index].Priority -= priority;
    //		ShiftDown(index);
    //		WriterUnlock();
    //	}

    //	/// <summary>Standard priority queue algorithm for up sifting.</summary>
    //	/// <param name="index">The index to be up sifted.</param>
    //	/// <remarks>Runtime: O(ln(n)), Omega(1).</remarks>
    //	private void ShiftUp(int index)
    //	{
    //		// NOTE: "index / 2" is the index of the parent of the item at location "index"
    //		while (_heapArray[index].Priority > _heapArray[index / 2].Priority)
    //		{
    //			ArraySwap(index, index / 2);
    //			index = index / 2;
    //		}
    //	}

    //	/// <summary>Standard priority queue algorithm for sifting down.</summary>
    //	/// <param name="index">The index to be down sifted.</param>
    //	/// <remarks>Runtime: O(ln(n)), Omega(1).</remarks>
    //	private void ShiftDown(int index)
    //	{
    //		// NOTE: "index * 2" is the index of the leftchild of the item at location "index"
    //		while ((index * 2) <= _count)
    //		{
    //			int index2 = index * 2;
    //			if (((index * 2) + 1) <= _count && _heapArray[(index * 2) + 1].Priority < _heapArray[index].Priority) index2++;
    //			// NOTE: "(index * 2) + 1" is the index of the rightchild of the item at location "index"
    //			if (_heapArray[index].Priority >= _heapArray[index2].Priority) break;
    //			ArraySwap(index, index2);
    //			index = index2;
    //		}
    //	}

    //	/// <summary>Standard array swap method.</summary>
    //	/// <param name="indexOne">The first index of the swap.</param>
    //	/// <param name="indexTwo">The second index of the swap.</param>
    //	/// <remarks>Runtime: O(1).</remarks>
    //	private void ArraySwap(int indexOne, int indexTwo)
    //	{
    //		HeapArrayDynamicLink swapStorage = _heapArray[indexTwo];
    //		_heapArray[indexTwo] = _heapArray[indexOne];
    //		_heapArray[indexOne] = swapStorage;
    //		_indexingReference[_heapArray[indexOne].Value] = indexOne;
    //		_indexingReference[_heapArray[indexTwo].Value] = indexTwo;
    //	}

    //	/// <summary>Returns this queue to an empty state.</summary>
    //	/// <remarks>Runtime: O(1).</remarks>
    //	public void Clear() { WriterLock(); _indexingReference.Clear(); _count = 0; WriterUnlock(); }

    //	/// <summary>Traversal function for a heap. Following a pre-order traversal.</summary>
    //	/// <param name="traversalFunction">The function to perform per iteration.</param>
    //	/// <returns>A determining a break in the traversal. (true = continue, false = break)</returns>
    //	public bool TraverseBreakable(Func<Type, bool> traversalFunction) { return TraversalPreOrderBreakable(traversalFunction); }

    //	/// <summary>Traversal function for a heap. Following a pre-order traversal.</summary>
    //	/// <param name="traversalFunction">The function to perform per iteration.</param>
    //	public void Traverse(Action<T> traversalFunction) { TraversalPreOrder(traversalFunction); }

    //	/// <summary>Implements an imperative traversal of the structure.</summary>
    //	/// <param name="traversalFunction">The function to perform per node in the traversal.</param>
    //	/// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //	public bool TraversalPreOrderBreakable(Func<Type, bool> traversalFunction)
    //	{
    //		ReaderLock();
    //		for (int i = 0; i < _count; i++)
    //		{
    //			if (!traversalFunction(_heapArray[i].Value))
    //			{
    //				ReaderUnlock();
    //				return false;
    //			}
    //		}
    //		ReaderUnlock();
    //		return true;
    //	}

    //	/// <summary>Implements an imperative traversal of the structure.</summary>
    //	/// <param name="traversalAction">The action to perform per node in the traversal.</param>
    //	/// <remarks>Runtime: O(n * traversalAction).</remarks>
    //	public void TraversalPreOrder(Action<T> traversalAction)
    //	{
    //		ReaderLock();
    //		for (int i = 0; i < _count; i++) traversalAction(_heapArray[i].Value);
    //		ReaderUnlock();
    //	}

    //	/// <summary>Gets all the items in the heap. WARNING: the return items are NOT ordered.</summary>
    //	/// <returns>The items in the heap in random order.</returns>
    //	public Type[] ToArray()
    //	{
    //		ReaderLock();
    //		Type[] array = new Type[_count];
    //		for (int i = 0; i < _count; i++)
    //			array[i] = _heapArray[i].Value;
    //		ReaderUnlock();
    //		return array;
    //	}

    //	/// <summary>Thread safe enterance for readers.</summary>
    //	private void ReaderLock() { lock (_lock) { while (!(_writers == 0)) Monitor.Wait(_lock); _readers++; } }
    //	/// <summary>Thread safe exit for readers.</summary>
    //	private void ReaderUnlock() { lock (_lock) { _readers--; Monitor.Pulse(_lock); } }
    //	/// <summary>Thread safe enterance for writers.</summary>
    //	private void WriterLock() { lock (_lock) { while (!(_writers == 0) && !(_readers == 0)) Monitor.Wait(_lock); _writers++; } }
    //	/// <summary>Thread safe exit for readers.</summary>
    //	private void WriterUnlock() { lock (_lock) { _writers--; Monitor.PulseAll(_lock); } }

    //	#region Structure<T>

    //	#region .Net Framework Compatibility

    //	/// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    //	IEnumerator IEnumerable.GetEnumerator()
    //	{
    //		throw new NotImplementedException();
    //	}

    //	/// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
    //	IEnumerator<T> IEnumerable<T>.GetEnumerator()
    //	{
    //		throw new NotImplementedException();
    //	}

    //	#endregion

    //	/// <summary>Pulls out all the values in the structure that are equivalent to the key.</summary>
    //	/// <typeparam name="Key">The type of the key to check for.</typeparam>
    //	/// <param name="key">The key to check for.</param>
    //	/// <param name="compare">Delegate representing comparison technique.</param>
    //	/// <returns>An array containing all the values matching the key or null if non were found.</returns>
    //	//Type[] GetValues<Key>(Key key, Compare<Type, Key> compare);

    //	/// <summary>Pulls out all the values in the structure that are equivalent to the key.</summary>
    //	/// <typeparam name="Key">The type of the key to check for.</typeparam>
    //	/// <param name="key">The key to check for.</param>
    //	/// <param name="compare">Delegate representing comparison technique.</param>
    //	/// <returns>An array containing all the values matching the key or null if non were found.</returns>
    //	/// <param name="values">The values that matched the given key.</param>
    //	/// <returns>true if 1 or more values were found; false if no values were found.</returns>
    //	//bool TryGetValues<Key>(Key key, Compare<Type, Key> compare, out Type[] values);

    //	/// <summary>Checks to see if a given object is in this data structure.</summary>
    //	/// <param name="item">The item to check for.</param>
    //	/// <param name="compare">Delegate representing comparison technique.</param>
    //	/// <returns>true if the item is in this structure; false if not.</returns>
    //	public bool Contains(Type item, Compare<T> compare)
    //	{
    //		throw new NotImplementedException();
    //	}

    //	/// <summary>Checks to see if a given object is in this data structure.</summary>
    //	/// <typeparam name="Key">The type of the key to check for.</typeparam>
    //	/// <param name="key">The key to check for.</param>
    //	/// <param name="compare">Delegate representing comparison technique.</param>
    //	/// <returns>true if the item is in this structure; false if not.</returns>
    //	public bool Contains<Key>(Key key, Compare<Type, Key> compare)
    //	{
    //		throw new NotImplementedException();
    //	}

    //	/// <summary>Invokes a delegate for each entry in the data structure.</summary>
    //	/// <param name="function">The delegate to invoke on each item in the structure.</param>
    //	public void Stepper(Step<T> function)
    //	{
    //		throw new NotImplementedException();
    //	}

    //	/// <summary>Invokes a delegate for each entry in the data structure.</summary>
    //	/// <param name="function">The delegate to invoke on each item in the structure.</param>
    //	public void Stepper(StepRef<T> function)
    //	{
    //		throw new NotImplementedException();
    //	}

    //	/// <summary>Invokes a delegate for each entry in the data structure.</summary>
    //	/// <param name="function">The delegate to invoke on each item in the structure.</param>
    //	/// <returns>The resulting status of the iteration.</returns>
    //	public StepStatus Stepper(StepBreak<T> function)
    //	{
    //		throw new NotImplementedException();
    //	}

    //	/// <summary>Invokes a delegate for each entry in the data structure.</summary>
    //	/// <param name="function">The delegate to invoke on each item in the structure.</param>
    //	/// <returns>The resulting status of the iteration.</returns>
    //	public StepStatus Stepper(StepRefBreak<T> function)
    //	{
    //		throw new NotImplementedException();
    //	}

    //	/// <summary>Creates a shallow clone of this data structure.</summary>
    //	/// <returns>A shallow clone of this data structure.</returns>
    //	public Structure<T> Clone()
    //	{
    //		throw new NotImplementedException();
    //	}

    //	///// <summary>Converts the structure into an array.</summary>
    //	///// <returns>An array containing all the item in the structure.</returns>
    //	//public Type[] ToArray()
    //	//{
    //	//	throw new NotImplementedException();
    //	//}

    //	#endregion

    //	/// <summary>This is used for throwing mutable priority queue exceptions only to make debugging faster.</summary>
    //	private class HeapArrayDynamicException : System.Exception
    //	{
    //		public HeapArrayDynamicException(string message) : base(message) { }
    //	}
    //}

    #endregion

    #endregion
}