using System;

namespace Towel.DataStructures
{
	/// <summary>A self-sorting binary tree based on the heights of each node.</summary>
	/// <typeparam name="T">The generic type of this data structure.</typeparam>
	public interface AvlTree<T> : DataStructure<T>,
		// Structure Properties
		DataStructure.Addable<T>,
		DataStructure.Removable<T>,
		DataStructure.Countable<T>,
		DataStructure.Clearable<T>,
		DataStructure.Comparing<T>,
		DataStructure.Auditable<T>
	{
		#region Properties

		/// <summary>Gets the current least item in the avl tree.</summary>
		T CurrentLeast { get; }
		/// <summary>Gets the current greated item in the avl tree.</summary>
		T CurrentGreatest { get; }

		#endregion

		#region Methods

		/// <summary>Determines if this structure contains a given item.</summary>
		/// <typeparam name="Key">The type of the key.</typeparam>
		/// <param name="key">The key of the item to check.</param>
		/// <param name="comparison">Comparison technique (must match the sorting technique of the structure).</param>
		/// <returns>True if contained, False if not.</returns>
		bool Contains<Key>(Key key, Compare<T, Key> comparison);
		/// <summary>Gets an item based on a given key.</summary>
		/// <typeparam name="Key">The type of the key.</typeparam>
		/// <param name="get">The key of the item to get.</param>
		/// <param name="comparison">Comparison technique (must match the sorting technique of the structure).</param>
		/// <returns>The found item.</returns>
		T Get<Key>(Key get, Compare<T, Key> comparison);
		/// <summary>Removes and item based on a given key.</summary>
		/// <typeparam name="Key">The type of the key.</typeparam>
		/// <param name="removal">The key of the item to be removed.</param>
		/// <param name="comparison">Comparison technique (must match the sorting technique of the structure).</param>
		void Remove<Key>(Key removal, Compare<T, Key> comparison);
        /// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        void StepperReverse(Step<T> step);
        /// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        void StepperReverse(StepRef<T> step);
        /// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        StepStatus StepperReverse(StepBreak<T> step);
        /// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        StepStatus StepperReverse(StepRefBreak<T> step);
        /// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
        /// <param name="step">The step function.</param>
        /// <param name="minimum">The minimum step value.</param>
        /// <param name="maximum">The maximum step value.</param>
        void Stepper(Step<T> step, T minimum, T maximum);
        /// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
        /// <param name="step">The step function.</param>
        /// <param name="minimum">The minimum step value.</param>
        /// <param name="maximum">The maximum step value.</param>
        void Stepper(StepRef<T> step, T minimum, T maximum);
        /// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
        /// <param name="step">The step function.</param>
        /// <param name="minimum">The minimum step value.</param>
        /// <param name="maximum">The maximum step value.</param>
        /// <returns>The result status of the stepper function.</returns>
        StepStatus Stepper(StepBreak<T> step, T minimum, T maximum);
        /// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
        /// <param name="step">The step function.</param>
        /// <param name="minimum">The minimum step value.</param>
        /// <param name="maximum">The maximum step value.</param>
        /// <returns>The result status of the stepper function.</returns>
        StepStatus Stepper(StepRefBreak<T> step, T minimum, T maximum);
        /// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
        /// <param name="step">The step function.</param>
        /// <param name="minimum">The minimum step value.</param>
        /// <param name="maximum">The maximum step value.</param>
        void StepperReverse(Step<T> step, T minimum, T maximum);
        /// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
        /// <param name="step">The step function.</param>
        /// <param name="minimum">The minimum step value.</param>
        /// <param name="maximum">The maximum step value.</param>
        void StepperReverse(StepRef<T> step, T minimum, T maximum);
        /// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
        /// <param name="step">The step function.</param>
        /// <param name="minimum">The minimum step value.</param>
        /// <param name="maximum">The maximum step value.</param>
        /// <returns>The result status of the stepper function.</returns>
        StepStatus StepperReverse(StepBreak<T> step, T minimum, T maximum);
        /// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
        /// <param name="step">The step function.</param>
        /// <param name="minimum">The minimum step value.</param>
        /// <param name="maximum">The maximum step value.</param>
        /// <returns>The result status of the stepper function.</returns>
        StepStatus StepperReverse(StepRefBreak<T> step, T minimum, T maximum);

		#endregion
	}

	/// <summary>Contains extensions methods for the AvlTree interface.</summary>
	public static class AvlTree
	{
		#region Extensions

		/// <summary>Wrapper for the get function to handle exceptions.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <typeparam name="Key">The type of the key.</typeparam>
		/// <param name="structure">This structure.</param>
		/// <param name="get">The key to get.</param>
		/// <param name="comparison">The sorting technique (must synchronize with this structure's sorting).</param>
		/// <param name="item">The item if found.</param>
		/// <returns>True if successful, False if not.</returns>
		public static bool TryGet<T, Key>(this AvlTree<T> structure, Key get, Compare<T, Key> comparison, out T item)
		{
			try
			{
				item = structure.Get<Key>(get, comparison);
				return true;
			}
			catch
			{
				item = default(T);
				return false;
			}
		}

		/// <summary>Wrapper for the remove function to handle exceptions.</summary>
		/// <typeparam name="T">The generic type of this data structure.</typeparam>
		/// <typeparam name="Key">The type of the key.</typeparam>
		/// <param name="structure">This structure.</param>
		/// <param name="removal">The key.</param>
		/// <param name="comparison">The sorting technique (must synchronize with this structure's sorting).</param>
		/// <returns>True if successful, False if not.</returns>
		public static bool TryRemove<T, Key>(this AvlTree<T> structure, Key removal, Compare<T, Key> comparison)
		{
			try
			{
				structure.Remove(removal, comparison);
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion
	}

    /// <summary>A self-sorting binary tree based on the heights of each node.</summary>
    /// <citation>
    /// This AVL tree imlpementation was originally developed by 
    /// Rodney Howell of Kansas State University. However, it has 
    /// been modified since its addition into the Towel framework.
    /// </citation>
    [Serializable]
    public class AvlTreeLinked<T> : AvlTree<T>
	{
		internal Node _root;
        internal int _count;
        internal Compare<T> _compare;

		#region Node

		/// <summary>This class just holds the data for each individual node of the tree.</summary>
		[Serializable]
		internal class Node
		{
			internal T Value;
			internal Node LeftChild;
			internal Node RightChild;
			internal int Height;
            
			internal Node(T value)
			{
				this.Value = value;
				this.LeftChild = null;
				this.RightChild = null;
				this.Height = 0;
			}
		}

        #endregion

        #region Constructors

        /// <summary>Constructs an AVL Tree.</summary>
        /// <param name="compare">The comparison function for sorting the items.</param>
        /// <runtime>θ(1)</runtime>
        public AvlTreeLinked(Compare<T> compare)
		{
			this._root = null;
			this._count = 0;
			this._compare = compare;
		}

        /// <summary>Constructs an AVL Tree.</summary>
        /// <runtime>θ(1)</runtime>
        public AvlTreeLinked() : this(Towel.Compare.Default) { }

        #endregion

        #region Properties

        /// <summary>Gets the current least item in the avl tree.</summary>
        /// <runtime>θ(ln(Count))</runtime>
        public T CurrentLeast
		{
			get
			{
				Node node = this._root;
                while (node.LeftChild != null)
                {
                    node = node.LeftChild;
                }
				return node.Value;
			}
		}

        /// <summary>Gets the current greated item in the avl tree.</summary>
        /// <runtime>θ(ln(Count))</runtime>
        public T CurrentGreatest
		{
			get
			{
				Node node = this._root;
                while (node.RightChild != null)
                {
                    node = node.RightChild;
                }
				return node.Value;
			}
		}

        /// <summary>The comparison function being utilized by this structure.</summary>
        /// <runtime>θ(1)</runtime>
        public Compare<T> Compare
        {
            get
            {
                return this._compare;
            }
        }

        /// <summary>Gets the number of elements in the collection.</summary>
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

		#region Add

		/// <summary>Adds an object to the AVL Tree.</summary>
		/// <param name="addition">The object to add.</param>
		/// <runtime>O(ln(n))</runtime>
		public void Add(T addition)
		{
            Node ADD(T ADDITION, Node NODE)
            {
                if (NODE is null)
                {
                    return new Node(ADDITION);
                }
                Comparison comparison = _compare(NODE.Value, ADDITION);
                if (comparison == Comparison.Equal)
                {
                    throw new InvalidOperationException("Adding an item that already exists.");
                }
                else if (comparison == Comparison.Greater)
                {
                    NODE.LeftChild = ADD(ADDITION, NODE.LeftChild);
                }
                else // (compareResult == Comparison.Less)
                {
                    NODE.RightChild = ADD(ADDITION, NODE.RightChild);
                }
                return Balance(NODE);
            }
            
            _root = ADD(addition, _root);
			_count++;
        }

		#endregion

		#region Clear

		/// <summary>Returns the tree to an iterative state.</summary>
		/// <runtime>θ(1)</runtime>
        public void Clear()
		{
			_root = null;
			_count = 0;
		}

        #endregion

        #region Clone

        /// <summary>Clones the AVL tree.</summary>
        /// <returns>A clone of the AVL tree.</returns>
        /// <runtime>θ(n)</runtime>
        public AvlTreeLinked<T> Clone()
		{
            // Note: this has room for optimization
			AvlTreeLinked<T> clone = new AvlTreeLinked<T>(this._compare);
            Stepper(x => clone.Add(x));
			return clone;
		}

        #endregion

        #region Contains

        /// <summary>Determines if the AVL tree contains a value.</summary>
        /// <param name="value">The value to look for.</param>
        /// <returns>Whether or not the AVL tree contains the value.</returns>
        /// <runtime>O(ln(Count)) Ω(1)</runtime>
        public bool Contains(T value)
		{
            bool CONTAINS(T VALUE, Node NODE)
            {
                if (NODE is null)
                {
                    return false;
                }
                Comparison comparison = _compare(VALUE, NODE.Value);
                if (comparison == Comparison.Equal)
                {
                    return true;
                }
                else if (comparison == Comparison.Less)
                {
                    return CONTAINS(VALUE, NODE.LeftChild);
                }
                else // else if (comparison == Comparison.Greater)
                {
                    return CONTAINS(VALUE, NODE.RightChild);
                }
            }

            return CONTAINS(value, this._root);
		}

        /// <summary>Determines if this structure contains an item by a given key.</summary>
        /// <typeparam name="Key">The type of the key.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="comparison">The sorting technique (must synchronize with this structure's sorting).</param>
        /// <returns>True of contained, False if not.</returns>
        /// <runtime>O(ln(Count)) Ω(1)</runtime>
        public bool Contains<Key>(Key key, Compare<T, Key> comparison)
		{
			Node node = _root;
			while (node != null)
			{
				Comparison compareResult = comparison(node.Value, key);
                if (compareResult == Comparison.Equal)
                {
                    return true;
                }
                else if (compareResult == Comparison.Greater)
                {
                    node = node.LeftChild;
                }
                else // (compareResult == Copmarison.Less)
                {
                    node = node.RightChild;
                }
			}
			return false;
		}

        #endregion

        #region Get (Key)

        /// <summary>Gets the item with the designated by the string.</summary>
        /// <param name="key">The string ID to look for.</param>
        /// <param name="compare">The sorting technique (must synchronize with this structure's sorting).</param>
        /// <returns>The object with the desired string ID if it exists.</returns>
        /// <runtime>O(ln(Count)) Ω(1)</runtime>
        public T Get<Key>(Key key, Compare<T, Key> compare)
		{
			Node node = _root;
			while (node != null)
			{
				Comparison comparison = compare(node.Value, key);
                if (comparison == Comparison.Equal)
                {
                    return node.Value;
                }
                else if (comparison == Comparison.Greater)
                {
                    node = node.LeftChild;
                }
                else // (compareResult == Copmarison.Less)
                {
                    node = node.RightChild;
                }
			}
			throw new InvalidOperationException("Attempting to get a non-existing item.");
		}

		#endregion

		#region Remove

		/// <summary>Removes an item from this structure.</summary>
		/// <param name="removal">The item to remove.</param>
		/// <runtime>O(ln(n))</runtime>
		public void Remove(T removal)
		{
            Node REMOVE(T REMOVAL, Node NODE)
            {
                if (NODE != null)
                {
                    Comparison compareResult = _compare(NODE.Value, REMOVAL);
                    if (compareResult == Comparison.Equal)
                    {
                        if (NODE.RightChild != null)
                        {
                            NODE.RightChild = RemoveLeftMost(NODE.RightChild, out Node leftMostOfRight);
                            leftMostOfRight.RightChild = NODE.RightChild;
                            leftMostOfRight.LeftChild = NODE.LeftChild;
                            NODE = leftMostOfRight;
                        }
                        else if (NODE.LeftChild != null)
                        {
                            NODE.LeftChild = RemoveRightMost(NODE.LeftChild, out Node rightMostOfLeft);
                            rightMostOfLeft.RightChild = NODE.RightChild;
                            rightMostOfLeft.LeftChild = NODE.LeftChild;
                            NODE = rightMostOfLeft;
                        }
                        else
                        {
                            return null;
                        }
                        SetHeight(NODE);
                        return Balance(NODE);
                    }
                    else if (compareResult == Comparison.Greater)
                    {
                        NODE.LeftChild = REMOVE(REMOVAL, NODE.LeftChild);
                    }
                    else // (compareResult == Comparison.Less)
                    {
                        NODE.RightChild = REMOVE(REMOVAL, NODE.RightChild);
                    }
                    SetHeight(NODE);
                    return Balance(NODE);
                }
                throw new InvalidOperationException("Attempting to remove a non-existing entry.");
            }

            _root = REMOVE(removal, _root);
			_count--;
		}

        #endregion

        #region Remove (Key)

        /// <summary>Removes an item from this structure by a given key.</summary>
        /// <typeparam name="Key">The type of the key.</typeparam>
        /// <param name="removal">The key.</param>
        /// <param name="comparison">The sorting technique (must synchronize with the structure's sorting).</param>
        /// <runtime>O(ln(n))</runtime>
        public void Remove<Key>(Key removal, Compare<T, Key> comparison)
		{
            Node REMOVE(Key REMOVAL, Compare<T, Key> COMPARE, Node NODE)
            {
                if (NODE != null)
                {
                    Comparison compareResult = COMPARE(NODE.Value, REMOVAL);
                    if (compareResult == Comparison.Equal)
                    {
                        if (NODE.RightChild != null)
                        {
                            NODE.RightChild = RemoveLeftMost(NODE.RightChild, out Node leftMostOfRight);
                            leftMostOfRight.RightChild = NODE.RightChild;
                            leftMostOfRight.LeftChild = NODE.LeftChild;
                            NODE = leftMostOfRight;
                        }
                        else if (NODE.LeftChild != null)
                        {
                            NODE.LeftChild = RemoveRightMost(NODE.LeftChild, out Node rightMostOfLeft);
                            rightMostOfLeft.RightChild = NODE.RightChild;
                            rightMostOfLeft.LeftChild = NODE.LeftChild;
                            NODE = rightMostOfLeft;
                        }
                        else
                        {
                            return null;
                        }
                        SetHeight(NODE);
                        return Balance(NODE);
                    }
                    else if (compareResult == Comparison.Greater)
                    {
                        NODE.LeftChild = REMOVE(REMOVAL, COMPARE, NODE.LeftChild);
                    }
                    else // (compareResult == Comparison.Less)
                    {
                        NODE.RightChild = REMOVE(REMOVAL, COMPARE, NODE.RightChild);
                    }
                    SetHeight(NODE);
                    return Balance(NODE);
                }
                throw new InvalidOperationException("Attempting to remove a non-existing entry.");
            }

            _root = REMOVE(removal, comparison, _root);
			_count--;
		}

		#endregion

		#region Stepper And IEnumerable

        #region Stepper

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        /// <remarks>Runtime: O(n * step).</remarks>
        public void Stepper(Step<T> step)
        {
            Stepper(step, this._root);
        }
        internal static bool Stepper(Step<T> step, Node node)
        {
            if (node != null)
            {
                Stepper(step, node.LeftChild);
                step(node.Value);
                Stepper(step, node.RightChild);
            }
            return true;
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <remarks>Runtime: O(n * step).</remarks>
		public void Stepper(StepRef<T> step)
        {
            Stepper(step, this._root);
        }
        internal static void Stepper(StepRef<T> step, Node node)
        {
            if (node != null)
            {
                Stepper(step, node.LeftChild);
                step(ref node.Value);
                Stepper(step, node.RightChild);
            }
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		/// <remarks>Runtime: O(n * step).</remarks>
		public StepStatus Stepper(StepBreak<T> step)
        {
            return Stepper(step, this._root);
        }
        internal static StepStatus Stepper(StepBreak<T> step, Node avltreeNode)
        {
            if (avltreeNode != null)
            {
                if (Stepper(step, avltreeNode.LeftChild) == StepStatus.Break)
                {
                    return StepStatus.Break;
                }
                if (step(avltreeNode.Value) == StepStatus.Break)
                {
                    return StepStatus.Break;
                }
                if (Stepper(step, avltreeNode.RightChild) == StepStatus.Break)
                {
                    return StepStatus.Break;
                }
            }
            return StepStatus.Continue;
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		/// <remarks>Runtime: O(n * step).</remarks>
		public StepStatus Stepper(StepRefBreak<T> step)
        {
            return Stepper(step, this._root);
        }
        internal static StepStatus Stepper(StepRefBreak<T> step, Node node)
        {
            if (node != null)
            {
                if (Stepper(step, node.LeftChild) == StepStatus.Break)
                {
                    return StepStatus.Break;
                }
                if (step(ref node.Value) == StepStatus.Break)
                {
                    return StepStatus.Break;
                }
                if (Stepper(step, node.RightChild) == StepStatus.Break)
                {
                    return StepStatus.Break;
                }
            }
            return StepStatus.Continue;
        }

        #endregion

        #region Stepper (ranged)

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <remarks>Runtime: O(n * step).</remarks>
		public virtual void Stepper(Step<T> step, T minimum, T maximum)
        {
            if (_compare(minimum, maximum) == Comparison.Greater)
            {
                throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
            }
            Stepper(step, this._root, minimum, maximum);
        }
        internal void Stepper(Step<T> step, Node node, T minimum, T maximum)
        {
            if (node != null)
            {
                if (_compare(node.Value, maximum) == Comparison.Greater)
                {
                    Stepper(step, node.LeftChild, minimum, maximum);
                }
                else if (_compare(node.Value, minimum) == Comparison.Less)
                {
                    Stepper(step, node.RightChild, minimum, maximum);
                }
                else
                {
                    Stepper(step, node.LeftChild, minimum, maximum);
                    step(node.Value);
                    Stepper(step, node.RightChild, minimum, maximum);
                }
            }
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <remarks>Runtime: O(n * step).</remarks>
		public virtual void Stepper(StepRef<T> step, T minimum, T maximum)
        {
            if (_compare(minimum, maximum) == Comparison.Greater)
            {
                throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
            }
            Stepper(step, _root, minimum, maximum);
        }
        internal void Stepper(StepRef<T> step, Node node, T minimum, T maximum)
        {
            if (node != null)
            {
                if (_compare(node.Value, minimum) == Comparison.Less)
                {
                    Stepper(step, node.RightChild, minimum, maximum);
                }
                else if (_compare(node.Value, maximum) == Comparison.Greater)
                {
                    Stepper(step, node.LeftChild, minimum, maximum);
                }
                else
                {
                    Stepper(step, node.LeftChild, minimum, maximum);
                    step(ref node.Value);
                    Stepper(step, node.RightChild, minimum, maximum);
                }
            }
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_function">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <remarks>Runtime: O(n * step).</remarks>
		public virtual StepStatus Stepper(StepBreak<T> step, T minimum, T maximum)
        {
            if (this._compare(minimum, maximum) == Comparison.Greater)
            {
                throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
            }
            return Stepper(step, _root, minimum, maximum);
        }
        internal StepStatus Stepper(StepBreak<T> step, Node node, T minimum, T maximum)
        {
            if (node != null)
            {
                if (_compare(node.Value, minimum) == Comparison.Less)
                {
                    return Stepper(step, node.RightChild, minimum, maximum);
                }
                else if (_compare(node.Value, maximum) == Comparison.Greater)
                {
                    return Stepper(step, node.LeftChild, minimum, maximum);
                }
                else
                {
                    if (Stepper(step, node.LeftChild, minimum, maximum) == StepStatus.Break)
                    {
                        return StepStatus.Break;
                    }
                    if (step(node.Value) == StepStatus.Break)
                    {
                        return StepStatus.Break;
                    }
                    if (this.Stepper(step, node.RightChild, minimum, maximum) == StepStatus.Break)
                    {
                        return StepStatus.Break;
                    }
                }
            }
            return StepStatus.Continue;
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <param name="minimum">The minimum value of the optimized stepper function.</param>
		/// <param name="maximum">The maximum value of the optimized stepper function.</param>
		/// <remarks>Runtime: O(n * step).</remarks>
		public virtual StepStatus Stepper(StepRefBreak<T> step, T minimum, T maximum)
        {
            if (_compare(minimum, maximum) == Comparison.Greater)
            {
                throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
            }
            return Stepper(step, _root, minimum, maximum);
        }
        internal StepStatus Stepper(StepRefBreak<T> step, Node node, T minimum, T maximum)
        {
            if (node != null)
            {
                if (_compare(node.Value, minimum) == Comparison.Less)
                {
                    return Stepper(step, node.RightChild, minimum, maximum);
                }
                else if (_compare(node.Value, maximum) == Comparison.Greater)
                {
                    return Stepper(step, node.LeftChild, minimum, maximum);
                }
                else
                {
                    if (Stepper(step, node.LeftChild, minimum, maximum) == StepStatus.Break)
                    {
                        return StepStatus.Break;
                    }
                    if (step(ref node.Value) == StepStatus.Break)
                    {
                        return StepStatus.Break;
                    }
                    if (Stepper(step, node.RightChild, minimum, maximum) == StepStatus.Break)
                    {
                        return StepStatus.Break;
                    }
                }
            }
            return StepStatus.Continue;
        }

        #endregion

        #region StepperReverse

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        /// <remarks>Runtime: O(n * traversalFunction).</remarks>
        public void StepperReverse(Step<T> step)
        {
            StepperReverse(step);
        }
        internal static bool StepperReverse(Step<T> step, Node node)
        {
            if (node != null)
            {
                StepperReverse(step, node.RightChild);
                step(node.Value);
                StepperReverse(step, node.LeftChild);
            }
            return true;
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        /// <remarks>Runtime: O(n * traversalFunction).</remarks>
        public void StepperReverse(StepRef<T> step)
        {
            StepperReverse(step);
        }
        internal static bool StepperReverse(StepRef<T> step, Node node)
        {
            if (node != null)
            {
                StepperReverse(step, node.RightChild);
                step(ref node.Value);
                StepperReverse(step, node.LeftChild);
            }
            return true;
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        /// <remarks>Runtime: O(n * traversalFunction).</remarks>
        public StepStatus StepperReverse(StepBreak<T> step)
        {
            return StepperReverse(step);
        }
        internal static StepStatus StepperReverse(StepBreak<T> step, Node avltreeNode)
        {
            if (avltreeNode != null)
            {
                if (StepperReverse(step, avltreeNode.RightChild) == StepStatus.Break)
                {
                    return StepStatus.Break;
                }
                if (step(avltreeNode.Value) == StepStatus.Break)
                {
                    return StepStatus.Break;
                }
                if (StepperReverse(step, avltreeNode.LeftChild) == StepStatus.Break)
                {
                    return StepStatus.Break;
                }
            }
            return StepStatus.Continue;
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        /// <returns>The resulting status of the iteration.</returns>
        /// <remarks>Runtime: O(n * traversalFunction).</remarks>
        public StepStatus StepperReverse(StepRefBreak<T> step)
        {
            return StepperReverse(step);
        }
        internal static StepStatus StepperReverse(StepRefBreak<T> step, Node node)
        {
            if (node != null)
            {
                if (StepperReverse(step, node.RightChild) == StepStatus.Break)
                {
                    return StepStatus.Break;
                }
                if (step(ref node.Value) == StepStatus.Break)
                {
                    return StepStatus.Break;
                }
                if (StepperReverse(step, node.LeftChild) == StepStatus.Break)
                {
                    return StepStatus.Break;
                }
            }
            return StepStatus.Continue;
        }

        #endregion

        #region StepperReverse (ranged)

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        /// <param name="minimum">The minimum value of the optimized stepper function.</param>
        /// <param name="maximum">The maximum value of the optimized stepper function.</param>
        /// <remarks>Runtime: O(n * traversalFunction).</remarks>
        public virtual void StepperReverse(Step<T> step, T minimum, T maximum)
        {
            if (_compare(minimum, maximum) == Comparison.Greater)
            {
                throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
            }
            StepperReverse(step, _root);
        }
        internal void StepperReverse(Step<T> step, Node node, T minimum, T maximum)
        {
            if (node != null)
            {
                if (_compare(node.Value, maximum) == Comparison.Greater)
                {
                    StepperReverse(step, node.LeftChild, minimum, maximum);
                }
                else if (_compare(node.Value, minimum) == Comparison.Less)
                {
                    StepperReverse(step, node.RightChild, minimum, maximum);
                }
                else
                {
                    StepperReverse(step, node.RightChild, minimum, maximum);
                    step(node.Value);
                    StepperReverse(step, node.LeftChild, minimum, maximum);
                }
            }
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        /// <param name="minimum">The minimum value of the optimized stepper function.</param>
        /// <param name="maximum">The maximum value of the optimized stepper function.</param>
        /// <remarks>Runtime: O(n * traversalFunction).</remarks>
        public virtual void StepperReverse(StepRef<T> step, T minimum, T maximum)
        {
            if (_compare(minimum, maximum) == Comparison.Greater)
            {
                throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
            }
            StepperReverse(step, _root, minimum, maximum);
        }
        internal void StepperReverse(StepRef<T> step, Node node, T minimum, T maximum)
        {
            if (node != null)
            {
                if (_compare(node.Value, minimum) == Comparison.Less)
                {
                    StepperReverse(step, node.RightChild, minimum, maximum);
                }
                else if (_compare(node.Value, maximum) == Comparison.Greater)
                {
                    StepperReverse(step, node.LeftChild, minimum, maximum);
                }
                else
                {
                    StepperReverse(step, node.RightChild, minimum, maximum);
                    step(ref node.Value);
                    StepperReverse(step, node.LeftChild, minimum, maximum);
                }
            }
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step_function">The delegate to invoke on each item in the structure.</param>
        /// <param name="minimum">The minimum value of the optimized stepper function.</param>
        /// <param name="maximum">The maximum value of the optimized stepper function.</param>
        /// <remarks>Runtime: O(n * traversalFunction).</remarks>
        public virtual StepStatus StepperReverse(StepBreak<T> step_function, T minimum, T maximum)
        {
            if (_compare(minimum, maximum) == Comparison.Greater)
            {
                throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
            }
            return StepperReverse(step_function, _root, minimum, maximum);
        }
        internal StepStatus StepperReverse(StepBreak<T> step, Node node, T minimum, T maximum)
        {
            if (node != null)
            {
                if (_compare(node.Value, minimum) == Comparison.Less)
                {
                    return StepperReverse(step, node.RightChild, minimum, maximum);
                }
                else if (_compare(node.Value, maximum) == Comparison.Greater)
                {
                    return StepperReverse(step, node.LeftChild, minimum, maximum);
                }
                else
                {
                    if (StepperReverse(step, node.RightChild, minimum, maximum) == StepStatus.Break)
                    {
                        return StepStatus.Break;
                    }
                    if (step(node.Value) == StepStatus.Break)
                    {
                        return StepStatus.Break;
                    }
                    if (StepperReverse(step, node.LeftChild, minimum, maximum) == StepStatus.Break)
                    {
                        return StepStatus.Break;
                    }
                }
            }
            return StepStatus.Continue;
        }

        /// <summary>Invokes a delegate for each entry in the data structure.</summary>
        /// <param name="step">The delegate to invoke on each item in the structure.</param>
        /// <param name="minimum">The minimum value of the optimized stepper function.</param>
        /// <param name="maximum">The maximum value of the optimized stepper function.</param>
        /// <remarks>Runtime: O(n * step_function).</remarks>
        public virtual StepStatus StepperReverse(StepRefBreak<T> step, T minimum, T maximum)
        {
            if (_compare(minimum, maximum) == Comparison.Greater)
            {
                throw new InvalidOperationException("!(" + nameof(minimum) + " <= " + nameof(maximum) + ")");
            }
            return StepperReverse(step, _root, minimum, maximum);
        }
        internal StepStatus StepperReverse(StepRefBreak<T> step, Node node, T minimum, T maximum)
        {
            if (node != null)
            {
                if (_compare(node.Value, minimum) == Comparison.Less)
                {
                    return StepperReverse(step, node.RightChild, minimum, maximum);
                }
                else if (_compare(node.Value, maximum) == Comparison.Greater)
                {
                    return StepperReverse(step, node.LeftChild, minimum, maximum);
                }
                else
                {
                    if (StepperReverse(step, node.RightChild, minimum, maximum) == StepStatus.Break)
                    {
                        return StepStatus.Break;
                    }
                    if (step(ref node.Value) == StepStatus.Break)
                    {
                        return StepStatus.Break;
                    }
                    if (StepperReverse(step, node.LeftChild, minimum, maximum) == StepStatus.Break)
                    {
                        return StepStatus.Break;
                    }
                }
            }
            return StepStatus.Continue;
        }

        #endregion

        #region IEnumerable

        /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
        [Obsolete("AVL Trees should be enumerated using the Stepper functions.")]
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

        /// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
        [Obsolete("AVL Trees should be enumerated using the Stepper functions.")]
        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			FirstInLastOut<Node> forks = new FirstInLastOutLinked<Node>();
			Node current = _root;
			while (current != null || forks.Count > 0)
			{
				if (current != null)
				{
					forks.Push(current);
					current = current.LeftChild;
				}
				else if (forks.Count > 0)
				{
					current = forks.Pop();
					yield return current.Value;
					current = current.RightChild;
				}
			}
		}

        #endregion

        #endregion

        #region Helpers

        /// <summary>Standard balancing algorithm for an AVL Tree.</summary>
        /// <param name="node">The tree to check the balancing of.</param>
        /// <returns>The result of the possible balancing.</returns>
        /// <runtime>θ(1)</runtime>
        private Node Balance(Node node)
		{
            Node RotateSingleLeft(Node NODE)
            {
                Node temp = NODE.RightChild;
                NODE.RightChild = temp.LeftChild;
                temp.LeftChild = NODE;
                SetHeight(NODE);
                SetHeight(temp);
                return temp;
            }

            Node RotateDoubleLeft(Node NODE)
            {
                Node temp = NODE.RightChild.LeftChild;
                NODE.RightChild.LeftChild = temp.RightChild;
                temp.RightChild = NODE.RightChild;
                NODE.RightChild = temp.LeftChild;
                temp.LeftChild = NODE;
                SetHeight(temp.LeftChild);
                SetHeight(temp.RightChild);
                SetHeight(temp);
                return temp;
            }

            Node RotateSingleRight(Node NODE)
            {
                Node temp = NODE.LeftChild;
                NODE.LeftChild = temp.RightChild;
                temp.RightChild = NODE;
                SetHeight(NODE);
                SetHeight(temp);
                return temp;
            }

            Node RotateDoubleRight(Node NODE)
            {
                Node temp = NODE.LeftChild.RightChild;
                NODE.LeftChild.RightChild = temp.LeftChild;
                temp.LeftChild = NODE.LeftChild;
                NODE.LeftChild = temp.RightChild;
                temp.RightChild = NODE;
                SetHeight(temp.RightChild);
                SetHeight(temp.LeftChild);
                SetHeight(temp);
                return temp;
            }

            if (Height(node.LeftChild) == Height(node.RightChild) + 2)
			{
                if (Height(node.LeftChild.LeftChild) > Height(node.RightChild))
                {
                    return RotateSingleRight(node);
                }
                else
                {
                    return RotateDoubleRight(node);
                }
			}
			else if (Height(node.RightChild) == Height(node.LeftChild) + 2)
			{
                if (Height(node.RightChild.RightChild) > Height(node.LeftChild))
                {
                    return RotateSingleLeft(node);
                }
                else
                {
                    return RotateDoubleLeft(node);
                }
			}
			SetHeight(node);
			return node;
		}

        /// <summary>This is just a protection against the null valued leaf nodes, which have a height of "-1".</summary>
        /// <param name="node">The node to find the hight of.</param>
        /// <returns>Returns "-1" if null (leaf) or the height property of the node.</returns>
        /// <runtime>θ(1)</runtime>
        private int Height(Node node)
		{
            if (node is null)
            {
                return -1;
            }
            else
            {
                return node.Height;
            }
		}

        /// <summary>Removes the left-most child of an AVL Tree node and returns it 
        /// through the out parameter.</summary>
        /// <param name="node">The tree to remove the left-most child from.</param>
        /// <param name="leftMost">The left-most child of this AVL tree.</param>
        /// <returns>The updated tree with the removal.</returns>
        private Node RemoveLeftMost(Node node, out Node leftMost)
		{
			if (node.LeftChild is null)
            {
                leftMost = node;
                return null;
            }
			node.LeftChild = RemoveLeftMost(node.LeftChild, out leftMost);
			SetHeight(node);
			return Balance(node);
		}

        /// <summary>Removes the right-most child of an AVL Tree node and returns it 
        /// through the out parameter.</summary>
        /// <param name="node">The tree to remove the right-most child from.</param>
        /// <param name="leftMost">The right-most child of this AVL tree.</param>
        /// <returns>The updated tree with the removal.</returns>
        private Node RemoveRightMost(Node node, out Node rightMost)
		{
			if (node.RightChild is null)
            {
                rightMost = node;
                return null;
            }
			node.LeftChild = RemoveRightMost(node.RightChild, out rightMost);
			SetHeight(node);
			return Balance(node);
		}
        
		/// <summary>Sets the height of a tree based on its children's heights.</summary>
		/// <param name="node">The tree to have its height adjusted.</param>
		/// <remarks>Runtime: O(1).</remarks>
		private void SetHeight(Node node)
		{
            if (node.LeftChild is null && node.RightChild is null)
            {
                node.Height = 0;
            }
            else if (Height(node.LeftChild) < Height(node.RightChild))
            {
                node.Height = Math.Max(Height(node.LeftChild), Height(node.RightChild)) + 1;
            }
		}

        #endregion

        #endregion
    }
}
