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
    /// <remarks>The runtimes of each public member are included in the "remarks" xml tags.</remarks>
    /// <citation>
    /// This AVL tree imlpementation was originally developed by 
    /// Rodney Howell of Kansas State University. However, it has 
    /// been modified since its addition into the Towel framework.
    /// </citation>
    [Serializable]
    public class AvlTreeLinked<T> : AvlTree<T>
	{
		// Fields
		internal Node _root;
        internal int _count;
        internal Compare<T> _compare;

		#region Nested Types

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

		public AvlTreeLinked() : this(Towel.Compare.Default) { }

		/// <summary>Constructs an AVL Tree.</summary>
		/// <param name="compare">The comparison function for sorting the items.</param>
		/// <remarks>Runtime: O(1).</remarks>
		public AvlTreeLinked(Compare<T> compare)
		{
			this._root = null;
			this._count = 0;
			this._compare = compare;
		}
		
		#endregion

		#region Properties

		/// <summary>Gets the current least item in the avl tree.</summary>
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
		public Compare<T> Compare
        {
            get
            {
                return this._compare;
            }
        }

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <runtime>O(1)</runtime>
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
		/// <runtime>Towel(ln(n))</runtime>
		public void Add(T addition)
		{
			this._root = Add(addition, this._root);
			this._count++;
		}

		private Node Add(T addition, Node avlTree)
		{
			if (avlTree == null) return new Node(addition);
			Comparison comparison = this._compare(avlTree.Value, addition);
            if (comparison == Comparison.Equal)
            {
                throw new InvalidOperationException("Adding an item that already exists.");
            }
            else if (comparison == Comparison.Greater)
            {
                avlTree.LeftChild = Add(addition, avlTree.LeftChild);
            }
            else // (compareResult == Comparison.Less)
            {
                avlTree.RightChild = Add(addition, avlTree.RightChild);
            }
			return Balance(avlTree);
		}

		#endregion

		#region Clear

		/// <summary>Returns the tree to an iterative state.</summary>
		public void Clear()
		{
			_root = null;
			_count = 0;
		}

		#endregion

		#region Clone

		public AvlTreeLinked<T> Clone()
		{
			AvlTreeLinked<T> clone = new AvlTreeLinked<T>(this._compare);
            Stepper(x => clone.Add(x));
			return clone;
		}

		#endregion

		#region Contains

		public bool Contains(T value)
		{
			return this.Contains(value, this._root);
		}

		/// <summary>Determines if this structure contains an item by a given key.</summary>
		/// <typeparam name="Key">The type of the key.</typeparam>
		/// <param name="key">The key.</param>
		/// <param name="comparison">The sorting technique (must synchronize with this structure's sorting).</param>
		/// <returns>True of contained, False if not.</returns>
		/// <runtime>O(ln(n)), Omega(1)</runtime>
		public bool Contains<Key>(Key key, Compare<T, Key> comparison)
		{
			// THIS THIS THE ITERATIVE VERSION OF THIS FUNCTION. THERE IS A RECURSIVE
			// VERSION IN THE "RECURSIVE" REGION SHOULD YOU WISH TO SEE IT.
			Node _current = _root;
			while (_current != null)
			{
				Comparison compareResult = comparison(_current.Value, key);
				if (compareResult == Comparison.Equal)
					return true;
				else if (compareResult == Comparison.Greater)
					_current = _current.LeftChild;
				else // (compareResult == Copmarison.Less)
					_current = _current.RightChild;
			}
			return false;
		}

		#endregion

		#region Get

		/// <summary>Gets the item with the designated by the string.</summary>
		/// <param name="key">The string ID to look for.</param>
		/// <param name="compare">The sorting technique (must synchronize with this structure's sorting).</param>
		/// <returns>The object with the desired string ID if it exists.</returns>
		/// <runtime>O(ln(n)), Omega(1)</runtime>
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
			this._root = Remove(removal, _root);
			this._count--;
		}

		/// <summary>Removes an item from this structure by a given key.</summary>
		/// <typeparam name="Key">The type of the key.</typeparam>
		/// <param name="removal">The key.</param>
		/// <param name="comparison">The sorting technique (must synchronize with the structure's sorting).</param>
		/// <runtime>O(ln(n))</runtime>
		public void Remove<Key>(Key removal, Compare<T, Key> comparison)
		{
			this._root = Remove(removal, comparison, _root);
			this._count--;
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
        /// <param name="avlTree">The tree to check the balancing of.</param>
        /// <returns>The result of the possible balancing.</returns>
        /// <remarks>Runtime: O(1).</remarks>
        private Node Balance(Node avlTree)
		{
			if (Height(avlTree.LeftChild) == Height(avlTree.RightChild) + 2)
			{
                if (Height(avlTree.LeftChild.LeftChild) > Height(avlTree.RightChild))
                {
                    return RotateSingleRight(avlTree);
                }
                else
                {
                    return RotateDoubleRight(avlTree);
                }
			}
			else if (Height(avlTree.RightChild) == Height(avlTree.LeftChild) + 2)
			{
                if (Height(avlTree.RightChild.RightChild) > Height(avlTree.LeftChild))
                {
                    return RotateSingleLeft(avlTree);
                }
                else
                {
                    return RotateDoubleLeft(avlTree);
                }
			}
			SetHeight(avlTree);
			return avlTree;
		}

		private bool Contains(T value, AvlTreeLinked<T>.Node node)
		{
            if (node is null)
            {
                return false;
            }
			Comparison comparison = _compare(value, node.Value);
            if (comparison == Comparison.Equal)
            {
                return true;
            }
            else if (comparison == Comparison.Less)
            {
                return Contains(value, node.LeftChild);
            }
            else // else if (comparison == Comparison.Greater)
            {
                return Contains(value, node.RightChild);
            }
		}

		/// <summary>This is just a protection against the null valued leaf nodes, 
		/// which have a height of "-1".</summary>
		/// <param name="avlTree">The AVL Tree to find the hight of.</param>
		/// <returns>Returns "-1" if null (leaf) or the height property of the node.</returns>
		/// <remarks>Runtime: O(1).</remarks>
		private int Height(Node avlTree)
		{
            if (avlTree == null)
            {
                return -1;
            }
            else
            {
                return avlTree.Height;
            }
		}

		/// <summary>Removes an object from the AVL Tree.</summary>
		/// <param name="removal">The string ID of the object to remove.</param>
		/// <param name="avlTree">The binary tree to remove from.</param>
		/// <returns>The resulting tree after the removal.</returns>
		/// <remarks>Runtime: Towel(ln(n)).</remarks>
		private Node Remove(T removal, Node avlTree)
		{
			if (avlTree != null)
			{
				Comparison compareResult = _compare(avlTree.Value, removal);
                if (compareResult == Comparison.Equal)
                {
                    if (avlTree.RightChild != null)
                    {
                        avlTree.RightChild = RemoveLeftMost(avlTree.RightChild, out Node leftMostOfRight);
                        leftMostOfRight.RightChild = avlTree.RightChild;
                        leftMostOfRight.LeftChild = avlTree.LeftChild;
                        avlTree = leftMostOfRight;
                    }
                    else if (avlTree.LeftChild != null)
                    {
                        avlTree.LeftChild = RemoveRightMost(avlTree.LeftChild, out Node rightMostOfLeft);
                        rightMostOfLeft.RightChild = avlTree.RightChild;
                        rightMostOfLeft.LeftChild = avlTree.LeftChild;
                        avlTree = rightMostOfLeft;
                    }
                    else return null;
                    SetHeight(avlTree);
                    return Balance(avlTree);
                }
                else if (compareResult == Comparison.Greater)
                {
                    avlTree.LeftChild = Remove(removal, avlTree.LeftChild);
                }
                else // (compareResult == Comparison.Less)
                {
                    avlTree.RightChild = Remove(removal, avlTree.RightChild);
                }
				SetHeight(avlTree);
				return Balance(avlTree);
			}
			throw new System.InvalidOperationException("Attempting to remove a non-existing entry.");
		}

		/// <summary>Removes an object from the AVL Tree.</summary>
		/// <param name="removal">The string ID of the object to remove.</param>
		/// <param name="avlTree">The binary tree to remove from.</param>
		/// <returns>The resulting tree after the removal.</returns>
		/// <remarks>Runtime: Towel(ln(n)).</remarks>
		private Node Remove<Key>(Key removal, Compare<T, Key> comparison, Node avlTree)
		{
			if (avlTree != null)
			{
				Comparison compareResult = comparison(avlTree.Value, removal);
				if (compareResult == Comparison.Equal)
				{
					if (avlTree.RightChild != null)
					{
                        avlTree.RightChild = RemoveLeftMost(avlTree.RightChild, out Node leftMostOfRight);
                        leftMostOfRight.RightChild = avlTree.RightChild;
						leftMostOfRight.LeftChild = avlTree.LeftChild;
						avlTree = leftMostOfRight;
					}
					else if (avlTree.LeftChild != null)
					{
                        avlTree.LeftChild = RemoveRightMost(avlTree.LeftChild, out Node rightMostOfLeft);
                        rightMostOfLeft.RightChild = avlTree.RightChild;
						rightMostOfLeft.LeftChild = avlTree.LeftChild;
						avlTree = rightMostOfLeft;
					}
					else return null;
					SetHeight(avlTree);
					return Balance(avlTree);
				}
				else if (compareResult == Comparison.Greater)
					avlTree.LeftChild = Remove<Key>(removal, comparison, avlTree.LeftChild);
				else // (compareResult == Comparison.Less)
					avlTree.RightChild = Remove<Key>(removal, comparison, avlTree.RightChild);
				SetHeight(avlTree);
				return Balance(avlTree);
			}
			throw new InvalidOperationException("Attempting to remove a non-existing entry.");
		}

		/// <summary>Removes the left-most child of an AVL Tree node and returns it 
		/// through the out parameter.</summary>
		/// <param name="avlTree">The tree to remove the left-most child from.</param>
		/// <param name="leftMost">The left-most child of this AVL tree.</param>
		/// <returns>The updated tree with the removal.</returns>
		/// <remarks>Runtime: Towel(ln(n)).</remarks>
		private Node RemoveLeftMost(Node avlTree, out Node leftMost)
		{
			if (avlTree.LeftChild is null)
            {
                leftMost = avlTree;
                return null;
            }
			avlTree.LeftChild = RemoveLeftMost(avlTree.LeftChild, out leftMost);
			SetHeight(avlTree);
			return Balance(avlTree);
		}

		/// <summary>Removes the right-most child of an AVL Tree node and returns it 
		/// through the out parameter.</summary>
		/// <param name="avlTree">The tree to remove the right-most child from.</param>
		/// <param name="leftMost">The right-most child of this AVL tree.</param>
		/// <returns>The updated tree with the removal.</returns>
		/// <remarks>Runtime: Towel(ln(n)).</remarks>
		private Node RemoveRightMost(Node avlTree, out Node rightMost)
		{
			if (avlTree.RightChild is null)
            {
                rightMost = avlTree;
                return null;
            }
			avlTree.LeftChild = RemoveLeftMost(avlTree.RightChild, out rightMost);
			SetHeight(avlTree);
			return Balance(avlTree);
		}

		/// <summary>Standard single rotation (to the right) algorithm for an AVL Tree.</summary>
		/// <param name="avlTree">The tree to single rotate right.</param>
		/// <returns>The resulting tree.</returns>
		/// <remarks>Runtime: O(1).</remarks>
		private Node RotateSingleRight(Node avlTree)
		{
			Node temp = avlTree.LeftChild;
			avlTree.LeftChild = temp.RightChild;
			temp.RightChild = avlTree;
			SetHeight(avlTree);
			SetHeight(temp);
			return temp;
		}

		/// <summary>Standard single rotation (to the left) algorithm for an AVL Tree.</summary>
		/// <param name="avlTree">The tree to single rotate left.</param>
		/// <returns>The resulting tree.</returns>
		/// <remarks>Runtime: O(1).</remarks>
		private Node RotateSingleLeft(Node avlTree)
		{
			Node temp = avlTree.RightChild;
			avlTree.RightChild = temp.LeftChild;
			temp.LeftChild = avlTree;
			SetHeight(avlTree);
			SetHeight(temp);
			return temp;
		}

		/// <summary>Standard double rotation (to the right) algorithm for an AVL Tree.</summary>
		/// <param name="avlTree">The tree to float rotate right.</param>
		/// <returns>The resulting tree.</returns>
		/// <remarks>Runtime: O(1).</remarks>
		private Node RotateDoubleRight(Node avlTree)
		{
			Node temp = avlTree.LeftChild.RightChild;
			avlTree.LeftChild.RightChild = temp.LeftChild;
			temp.LeftChild = avlTree.LeftChild;
			avlTree.LeftChild = temp.RightChild;
			temp.RightChild = avlTree;
			SetHeight(temp.LeftChild);
			SetHeight(temp.RightChild);
			SetHeight(temp);
			return temp;
		}

		/// <summary>Standard double rotation (to the left) algorithm for an AVL Tree.</summary>
		/// <param name="avlTree">The tree to float rotate left.</param>
		/// <returns>The resulting tree.</returns>
		/// <remarks>Runtime: O(1).</remarks>
		private Node RotateDoubleLeft(Node avlTree)
		{
			Node temp = avlTree.RightChild.LeftChild;
			avlTree.RightChild.LeftChild = temp.RightChild;
			temp.RightChild = avlTree.RightChild;
			avlTree.RightChild = temp.LeftChild;
			temp.LeftChild = avlTree;
			SetHeight(temp.LeftChild);
			SetHeight(temp.RightChild);
			SetHeight(temp);
			return temp;
		}

		/// <summary>Sets the height of a tree based on its children's heights.</summary>
		/// <param name="avlTree">The tree to have its height adjusted.</param>
		/// <remarks>Runtime: O(1).</remarks>
		private void SetHeight(Node avlTree)
		{
            if (Height(avlTree.LeftChild) < Height(avlTree.RightChild))
            {
                avlTree.Height = System.Math.Max(Height(avlTree.LeftChild), Height(avlTree.RightChild)) + 1;
            }
		}

        #endregion

        #endregion
    }

    #region In Development
    ///// <summary>Implements an AVL Tree where the items are sorted by string id values.</summary>
    ///// <remarks>The runtimes of each public member are included in the "remarks" xml tags.</remarks>
    //[System.Serializable]
    //internal class AvlTree_Array<T>// : AvlTree<T>
    //{
    //	[System.Serializable]
    //	public struct Node
    //	{
    //		public bool _occupied;
    //		public T _value;
    //	}

    //	internal Link<bool, T>[] _avlTree;
    //	private int _count;

    //	private Compare<T> _compare;

    //	/// <summary>Constructs an AVL Tree.</summary>
    //	/// <param name="compare">The comparison function for sorting the items.</param>
    //	/// <remarks>Runtime: O(1).</remarks>
    //	internal AvlTree_Array(Compare<T> compare, int maximumSize)
    //	{
    //		throw new System.Exception("still in development");

    //		if (maximumSize < 1)
    //			throw new System.Exception("");
    //		this._avlTree = new Link<bool, T>[maximumSize + 1];
    //		this._count = 0;
    //		this._compare = compare;
    //	}

    //	/// <summary>Gets the number of elements in the collection.</summary>
    //	/// <remarks>Runtime: O(1).</remarks>
    //	public int Count { get { return _count; } }

    //	private int Height(int index)
    //	{
    //		return (index - 1) / 2;
    //	}

    //	public virtual bool Contains<Key>(Key key, Compare<T, Key> comparison)
    //	{
    //		// THIS THIS THE ITERATIVE VERSION OF THIS FUNCTION. THERE IS A RECURSIVE
    //		// VERSION IN THE "RECURSIVE" REGION SHOULD YOU WISH TO SEE IT.
    //		int current = 1;
    //		while (_avlTree[current].One != false)
    //		{
    //			Comparison compareResult = comparison(_avlTree[current].Two, key);
    //			if (compareResult == Comparison.Equal)
    //				return true;
    //			else if (compareResult == Comparison.Greater)
    //				current = current * 2; // LeftChild
    //			else // (compareResult == Copmarison.Less)
    //				current = current * 2 + 1; // RightChild
    //		}
    //		return false;
    //	}

    //	///// <summary>Gets the item with the designated by the string.</summary>
    //	///// <param name="id">The string ID to look for.</param>
    //	///// <returns>The object with the desired string ID if it exists.</returns>
    //	///// <remarks>Runtime: O(ln(n)), Omega(1).</remarks>
    //	//public virtual Type Get<Key>(Key get, Compare<Type, Key> comparison)
    //	//{
    //	//	int current = 1;
    //	//	while (_avlTree[current] != null)
    //	//	{
    //	//		Comparison compareResult = comparison(_avlTree[current], get);
    //	//		if (compareResult == Comparison.Equal)
    //	//			return _avlTree[current];
    //	//		else if (compareResult == Comparison.Greater)
    //	//			current = current * 2; // LeftChild
    //	//		else // (compareResult == Copmarison.Less)
    //	//			current = current * 2 + 1; // RightChild
    //	//	}
    //	//	throw new AvlTree_Array<T>.Exception
    //	//		("Attempting to get a non-existing value: " + get.ToString() + ".");
    //	//}

    //	///// <summary>Gets the item with the designated by the string.</summary>
    //	///// <param name="id">The string ID to look for.</param>
    //	///// <returns>The object with the desired string ID if it exists.</returns>
    //	///// <remarks>Runtime: O(ln(n)), Omega(1).</remarks>
    //	//public virtual bool TryGet<Key>(Key get, Compare<Type, Key> comparison, out Type result)
    //	//{
    //	//	int current = 1;
    //	//	while (_avlTree[current] != null)
    //	//	{
    //	//		Comparison compareResult = comparison(_avlTree[current], get);
    //	//		if (compareResult == Comparison.Equal)
    //	//		{
    //	//			result = _avlTree[current];
    //	//			return true;
    //	//		}
    //	//		else if (compareResult == Comparison.Greater)
    //	//			current = current * 2; // LeftChild
    //	//		else // (compareResult == Comparison.Less)
    //	//			current = current * 2 + 1; // RightChild
    //	//	}
    //	//	result = default(Type);
    //	//	return false;
    //	//}

    //	///// <summary>Adds an object to the AVL Tree.</summary>
    //	///// <param name="addition">The object to add.</param>
    //	///// <remarks>Runtime: Towel(ln(n)).</remarks>
    //	//public virtual void Add(Type addition)
    //	//{
    //	//	_avlTree = Add(addition, 1);
    //	//	_count++;
    //	//}

    //	//private virtual void Add(Type addition, int index)
    //	//{
    //	//	if (index > _avlTree.Length)
    //	//		throw new Exception("maximum tree size reached");

    //	//	if (_avlTree[index] == null)
    //	//	{
    //	//		_avlTree[index] = addition;
    //	//		return
    //	//	}
    //	//	Comparison compareResult = _compare(avlTree.Value, addition);
    //	//	if (compareResult == Comparison.Equal)
    //	//		throw new AvlTreeLinkedException("Attempting to add an already existing id exists.");
    //	//	else if (compareResult == Comparison.Greater)
    //	//		avlTree.LeftChild = Add(addition, avlTree.LeftChild);
    //	//	else // (compareResult == Comparison.Less)
    //	//		avlTree.RightChild = Add(addition, avlTree.RightChild);
    //	//	return Balance(avlTree);
    //	//}

    //	///// <summary>Adds an object to the AVL Tree.</summary>
    //	///// <param name="addition">The object to add.</param>
    //	///// <remarks>Runtime: Towel(ln(n)).</remarks>
    //	//public virtual bool TryAdd(Type addition)
    //	//{
    //	//	bool added;
    //	//	_avlTree = TryAdd(addition, _avlTree, out added);
    //	//	_count++;
    //	//	return added;
    //	//}

    //	//private Node TryAdd(Type addition, Node avlTree, out bool added)
    //	//{
    //	//	if (avlTree == null)
    //	//	{
    //	//		added = true;
    //	//		return new Node(addition);
    //	//	}
    //	//	Comparison compareResult = _compare(avlTree.Value, addition);
    //	//	if (compareResult == Comparison.Equal)
    //	//	{
    //	//		added = false;
    //	//		return avlTree;
    //	//	}
    //	//	else if (compareResult == Comparison.Greater)
    //	//		avlTree.LeftChild = TryAdd(addition, avlTree.LeftChild, out added);
    //	//	else // (compareResult == Comparison.Less)
    //	//		avlTree.RightChild = TryAdd(addition, avlTree.RightChild, out added);
    //	//	return Balance(avlTree);
    //	//}

    //	///// <summary>Removes an object from the AVL Tree.</summary>
    //	///// <param name="removal">The string ID of the object to remove.</param>
    //	///// <remarks>Runtime: Towel(ln(n)).</remarks>
    //	//public virtual void Remove(Type removal)
    //	//{
    //	//	_avlTree = Remove(removal, _avlTree);
    //	//	_count--;
    //	//}

    //	///// <summary>Removes an object from the AVL Tree.</summary>
    //	///// <param name="removal">The string ID of the object to remove.</param>
    //	///// <param name="avlTree">The binary tree to remove from.</param>
    //	///// <returns>The resulting tree after the removal.</returns>
    //	///// <remarks>Runtime: Towel(ln(n)).</remarks>
    //	//private Node Remove(Type removal, Node avlTree)
    //	//{
    //	//	if (avlTree != null)
    //	//	{
    //	//		Comparison compareResult = _compare(avlTree.Value, removal);
    //	//		if (compareResult == Comparison.Equal)
    //	//		{
    //	//			if (avlTree.RightChild != null)
    //	//			{
    //	//				Node leftMostOfRight;
    //	//				avlTree.RightChild = RemoveLeftMost(avlTree.RightChild, out leftMostOfRight);
    //	//				leftMostOfRight.RightChild = avlTree.RightChild;
    //	//				leftMostOfRight.LeftChild = avlTree.LeftChild;
    //	//				avlTree = leftMostOfRight;
    //	//			}
    //	//			else if (avlTree.LeftChild != null)
    //	//			{
    //	//				Node rightMostOfLeft;
    //	//				avlTree.LeftChild = RemoveRightMost(avlTree.LeftChild, out rightMostOfLeft);
    //	//				rightMostOfLeft.RightChild = avlTree.RightChild;
    //	//				rightMostOfLeft.LeftChild = avlTree.LeftChild;
    //	//				avlTree = rightMostOfLeft;
    //	//			}
    //	//			else return null;
    //	//			SetHeight(avlTree);
    //	//			return Balance(avlTree);
    //	//		}
    //	//		else if (compareResult == Comparison.Greater)
    //	//			avlTree.LeftChild = Remove(removal, avlTree.LeftChild);
    //	//		else // (compareResult == Comparison.Less)
    //	//			avlTree.RightChild = Remove(removal, avlTree.RightChild);
    //	//		SetHeight(avlTree);
    //	//		return Balance(avlTree);
    //	//	}
    //	//	throw new AvlTreeLinkedException("Attempting to remove a non-existing entry.");
    //	//}

    //	///// <summary>Removes an object from the AVL Tree.</summary>
    //	///// <param name="removal">The string ID of the object to remove.</param>
    //	///// <remarks>Runtime: Towel(ln(n)).</remarks>
    //	//public virtual bool TryRemove(Type removal)
    //	//{
    //	//	try
    //	//	{
    //	//		_avlTree = Remove(removal, _avlTree);
    //	//		_count--;
    //	//		return true;
    //	//	}
    //	//	catch
    //	//	{
    //	//		return false;
    //	//	}
    //	//}

    //	///// <summary>Removes an object from the AVL Tree.</summary>
    //	///// <param name="removal">The string ID of the object to remove.</param>
    //	///// <remarks>Runtime: Towel(ln(n)).</remarks>
    //	//public virtual void Remove<Key>(Key removal, Compare<Type, Key> comparison)
    //	//{
    //	//	_avlTree = Remove(removal, comparison, _avlTree);
    //	//	_count--;
    //	//}

    //	///// <summary>Removes an object from the AVL Tree.</summary>
    //	///// <param name="removal">The string ID of the object to remove.</param>
    //	///// <param name="avlTree">The binary tree to remove from.</param>
    //	///// <returns>The resulting tree after the removal.</returns>
    //	///// <remarks>Runtime: Towel(ln(n)).</remarks>
    //	//private Node Remove<Key>(Key removal, Compare<Type, Key> comparison, Node avlTree)
    //	//{
    //	//	if (avlTree != null)
    //	//	{
    //	//		Comparison compareResult = comparison(avlTree.Value, removal);
    //	//		if (compareResult == Comparison.Equal)
    //	//		{
    //	//			if (avlTree.RightChild != null)
    //	//			{
    //	//				Node leftMostOfRight;
    //	//				avlTree.RightChild = RemoveLeftMost(avlTree.RightChild, out leftMostOfRight);
    //	//				leftMostOfRight.RightChild = avlTree.RightChild;
    //	//				leftMostOfRight.LeftChild = avlTree.LeftChild;
    //	//				avlTree = leftMostOfRight;
    //	//			}
    //	//			else if (avlTree.LeftChild != null)
    //	//			{
    //	//				Node rightMostOfLeft;
    //	//				avlTree.LeftChild = RemoveRightMost(avlTree.LeftChild, out rightMostOfLeft);
    //	//				rightMostOfLeft.RightChild = avlTree.RightChild;
    //	//				rightMostOfLeft.LeftChild = avlTree.LeftChild;
    //	//				avlTree = rightMostOfLeft;
    //	//			}
    //	//			else return null;
    //	//			SetHeight(avlTree);
    //	//			return Balance(avlTree);
    //	//		}
    //	//		else if (compareResult == Comparison.Greater)
    //	//			avlTree.LeftChild = Remove<Key>(removal, comparison, avlTree.LeftChild);
    //	//		else // (compareResult == Comparison.Less)
    //	//			avlTree.RightChild = Remove<Key>(removal, comparison, avlTree.RightChild);
    //	//		SetHeight(avlTree);
    //	//		return Balance(avlTree);
    //	//	}
    //	//	throw new AvlTreeLinkedException("Attempting to remove a non-existing entry.");
    //	//}

    //	///// <summary>Removes an object from the AVL Tree.</summary>
    //	///// <param name="removal">The string ID of the object to remove.</param>
    //	///// <remarks>Runtime: Towel(ln(n)).</remarks>
    //	//public virtual bool TryRemove<Key>(Key removal, Compare<Type, Key> comparison)
    //	//{
    //	//	try
    //	//	{
    //	//		_avlTree = Remove(removal, comparison, _avlTree);
    //	//		_count--;
    //	//		return true;
    //	//	}
    //	//	catch
    //	//	{
    //	//		return false;
    //	//	}
    //	//}

    //	///// <summary>Removes the left-most child of an AVL Tree node and returns it 
    //	///// through the out parameter.</summary>
    //	///// <param name="avlTree">The tree to remove the left-most child from.</param>
    //	///// <param name="leftMost">The left-most child of this AVL tree.</param>
    //	///// <returns>The updated tree with the removal.</returns>
    //	///// <remarks>Runtime: Towel(ln(n)).</remarks>
    //	//private Node RemoveLeftMost(Node avlTree, out Node leftMost)
    //	//{
    //	//	if (avlTree.LeftChild == null) { leftMost = avlTree; return null; }
    //	//	avlTree.LeftChild = RemoveLeftMost(avlTree.LeftChild, out leftMost);
    //	//	SetHeight(avlTree);
    //	//	return Balance(avlTree);
    //	//}

    //	///// <summary>Removes the right-most child of an AVL Tree node and returns it 
    //	///// through the out parameter.</summary>
    //	///// <param name="avlTree">The tree to remove the right-most child from.</param>
    //	///// <param name="leftMost">The right-most child of this AVL tree.</param>
    //	///// <returns>The updated tree with the removal.</returns>
    //	///// <remarks>Runtime: Towel(ln(n)).</remarks>
    //	//private Node RemoveRightMost(Node avlTree, out Node rightMost)
    //	//{
    //	//	if (avlTree.RightChild == null) { rightMost = avlTree; return null; }
    //	//	avlTree.LeftChild = RemoveLeftMost(avlTree.RightChild, out rightMost);
    //	//	SetHeight(avlTree);
    //	//	return Balance(avlTree);
    //	//}

    //	///// <summary>This is just a protection against the null valued leaf nodes, 
    //	///// which have a height of "-1".</summary>
    //	///// <param name="avlTree">The AVL Tree to find the hight of.</param>
    //	///// <returns>Returns "-1" if null (leaf) or the height property of the node.</returns>
    //	///// <remarks>Runtime: O(1).</remarks>
    //	//private int Height(Node avlTree)
    //	//{
    //	//	if (avlTree == null) return -1;
    //	//	else return avlTree.Height;
    //	//}

    //	///// <summary>Sets the height of a tree based on its children's heights.</summary>
    //	///// <param name="avlTree">The tree to have its height adjusted.</param>
    //	///// <remarks>Runtime: O(1).</remarks>
    //	//private void SetHeight(Node avlTree)
    //	//{
    //	//	if (Height(avlTree.LeftChild) < Height(avlTree.RightChild))
    //	//		avlTree.Height = Math.Max(Height(avlTree.LeftChild), Height(avlTree.RightChild)) + 1;
    //	//}

    //	///// <summary>Standard balancing algorithm for an AVL Tree.</summary>
    //	///// <param name="avlTree">The tree to check the balancing of.</param>
    //	///// <returns>The result of the possible balancing.</returns>
    //	///// <remarks>Runtime: O(1).</remarks>
    //	//private Node Balance(Node avlTree)
    //	//{
    //	//	if (Height(avlTree.LeftChild) == Height(avlTree.RightChild) + 2)
    //	//	{
    //	//		if (Height(avlTree.LeftChild.LeftChild) > Height(avlTree.RightChild))
    //	//			return SingleRotateRight(avlTree);
    //	//		else return DoubleRotateRight(avlTree);
    //	//	}
    //	//	else if (Height(avlTree.RightChild) == Height(avlTree.LeftChild) + 2)
    //	//	{
    //	//		if (Height(avlTree.RightChild.RightChild) > Height(avlTree.LeftChild))
    //	//			return SingleRotateLeft(avlTree);
    //	//		else return DoubleRotateLeft(avlTree);
    //	//	}
    //	//	SetHeight(avlTree);
    //	//	return avlTree;
    //	//}

    //	///// <summary>Standard single rotation (to the right) algorithm for an AVL Tree.</summary>
    //	///// <param name="avlTree">The tree to single rotate right.</param>
    //	///// <returns>The resulting tree.</returns>
    //	///// <remarks>Runtime: O(1).</remarks>
    //	//private Node SingleRotateRight(Node avlTree)
    //	//{
    //	//	Node temp = avlTree.LeftChild;
    //	//	avlTree.LeftChild = temp.RightChild;
    //	//	temp.RightChild = avlTree;
    //	//	SetHeight(avlTree);
    //	//	SetHeight(temp);
    //	//	return temp;
    //	//}

    //	///// <summary>Standard single rotation (to the left) algorithm for an AVL Tree.</summary>
    //	///// <param name="avlTree">The tree to single rotate left.</param>
    //	///// <returns>The resulting tree.</returns>
    //	///// <remarks>Runtime: O(1).</remarks>
    //	//private Node SingleRotateLeft(Node avlTree)
    //	//{
    //	//	Node temp = avlTree.RightChild;
    //	//	avlTree.RightChild = temp.LeftChild;
    //	//	temp.LeftChild = avlTree;
    //	//	SetHeight(avlTree);
    //	//	SetHeight(temp);
    //	//	return temp;
    //	//}

    //	///// <summary>Standard double rotation (to the right) algorithm for an AVL Tree.</summary>
    //	///// <param name="avlTree">The tree to float rotate right.</param>
    //	///// <returns>The resulting tree.</returns>
    //	///// <remarks>Runtime: O(1).</remarks>
    //	//private Node DoubleRotateRight(Node avlTree)
    //	//{
    //	//	Node temp = avlTree.LeftChild.RightChild;
    //	//	avlTree.LeftChild.RightChild = temp.LeftChild;
    //	//	temp.LeftChild = avlTree.LeftChild;
    //	//	avlTree.LeftChild = temp.RightChild;
    //	//	temp.RightChild = avlTree;
    //	//	SetHeight(temp.LeftChild);
    //	//	SetHeight(temp.RightChild);
    //	//	SetHeight(temp);
    //	//	return temp;
    //	//}

    //	///// <summary>Standard double rotation (to the left) algorithm for an AVL Tree.</summary>
    //	///// <param name="avlTree">The tree to float rotate left.</param>
    //	///// <returns>The resulting tree.</returns>
    //	///// <remarks>Runtime: O(1).</remarks>
    //	//private Node DoubleRotateLeft(Node avlTree)
    //	//{
    //	//	Node temp = avlTree.RightChild.LeftChild;
    //	//	avlTree.RightChild.LeftChild = temp.RightChild;
    //	//	temp.RightChild = avlTree.RightChild;
    //	//	avlTree.RightChild = temp.LeftChild;
    //	//	temp.LeftChild = avlTree;
    //	//	SetHeight(temp.LeftChild);
    //	//	SetHeight(temp.RightChild);
    //	//	SetHeight(temp);
    //	//	return temp;
    //	//}

    //	///// <summary>Returns the tree to an iterative state.</summary>
    //	//public virtual void Clear() { _avlTree = null; _count = 0; }

    //	///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    //	///// <param name="function">The delegate to invoke on each item in the structure.</param>
    //	///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //	//public void Stepper(Step<T> function)
    //	//{
    //	//	this.StepperInOrder(function);
    //	//}

    //	///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    //	///// <param name="function">The delegate to invoke on each item in the structure.</param>
    //	///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //	//public void Stepper(StepRef<T> function)
    //	//{
    //	//	this.StepperInOrder(function);
    //	//}

    //	///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    //	///// <param name="function">The delegate to invoke on each item in the structure.</param>
    //	///// <returns>The resulting status of the iteration.</returns>
    //	///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //	//public StepStatus Stepper(StepBreak<T> function)
    //	//{
    //	//	return this.StepperInOrder(function);
    //	//}

    //	///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    //	///// <param name="function">The delegate to invoke on each item in the structure.</param>
    //	///// <returns>The resulting status of the iteration.</returns>
    //	///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //	//public StepStatus Stepper(StepRefBreak<T> function)
    //	//{
    //	//	return this.StepperInOrder(function);
    //	//}

    //	//public IEnumerator<T> GetEnumerator()
    //	//{
    //	//	return AvlTree_Linked<T>.GetEnumerator(this._avlTree);
    //	//}
    //	//private static IEnumerator<T> GetEnumerator(Node avltreeNode)
    //	//{
    //	//	if (avltreeNode != null)
    //	//	{
    //	//		AvlTree_Linked<T>.GetEnumerator(avltreeNode.LeftChild);
    //	//		yield return avltreeNode.Value;
    //	//		AvlTree_Linked<T>.GetEnumerator(avltreeNode.RightChild);
    //	//	}
    //	//}

    //	///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    //	///// <param name="function">The delegate to invoke on each item in the structure.</param>
    //	///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //	//public virtual void StepperInOrder(Step<T> function)
    //	//{
    //	//	AvlTree_Linked<T>.TraversalInOrder(function, _avlTree);
    //	//}
    //	//private static bool TraversalInOrder(Step<T> function, Node avltreeNode)
    //	//{
    //	//	if (avltreeNode != null)
    //	//	{
    //	//		AvlTree_Linked<T>.TraversalInOrder(function, avltreeNode.LeftChild);
    //	//		function(avltreeNode.Value);
    //	//		AvlTree_Linked<T>.TraversalInOrder(function, avltreeNode.RightChild);
    //	//	}
    //	//	return true;
    //	//}

    //	///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    //	///// <param name="function">The delegate to invoke on each item in the structure.</param>
    //	///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //	//public virtual void StepperInOrder(StepRef<T> function)
    //	//{
    //	//	AvlTree_Linked<T>.TraversalInOrder(function, _avlTree);
    //	//}
    //	//private static bool TraversalInOrder(StepRef<T> function, Node avltreeNode)
    //	//{
    //	//	if (avltreeNode != null)
    //	//	{
    //	//		AvlTree_Linked<T>.TraversalInOrder(function, avltreeNode.LeftChild);
    //	//		Type value = avltreeNode.Value;
    //	//		function(ref value);
    //	//		avltreeNode.Value = value;
    //	//		AvlTree_Linked<T>.TraversalInOrder(function, avltreeNode.RightChild);
    //	//	}
    //	//	return true;
    //	//}

    //	///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    //	///// <param name="function">The delegate to invoke on each item in the structure.</param>
    //	///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //	//public virtual StepStatus StepperInOrder(StepBreak<T> function)
    //	//{
    //	//	return AvlTree_Linked<T>.TraversalInOrder(function, _avlTree);
    //	//}
    //	//private static StepStatus TraversalInOrder(StepBreak<T> function, Node avltreeNode)
    //	//{
    //	//	if (avltreeNode != null)
    //	//	{
    //	//		if (AvlTree_Linked<T>.TraversalInOrder(function, avltreeNode.LeftChild) == StepStatus.Break)
    //	//			return StepStatus.Break;
    //	//		Type value = avltreeNode.Value;
    //	//		StepStatus status = function(value);
    //	//		avltreeNode.Value = value;
    //	//		if (status == StepStatus.Break)
    //	//			return StepStatus.Break;
    //	//		if (AvlTree_Linked<T>.TraversalInOrder(function, avltreeNode.RightChild) == StepStatus.Break)
    //	//			return StepStatus.Break;
    //	//	}
    //	//	return StepStatus.Continue;
    //	//}

    //	///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    //	///// <param name="function">The delegate to invoke on each item in the structure.</param>
    //	///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //	//public virtual StepStatus StepperInOrder(StepRefBreak<T> function)
    //	//{
    //	//	return AvlTree_Linked<T>.TraversalInOrder(function, _avlTree);
    //	//}
    //	//private static StepStatus TraversalInOrder(StepRefBreak<T> function, Node avltreeNode)
    //	//{
    //	//	if (avltreeNode != null)
    //	//	{
    //	//		if (AvlTree_Linked<T>.TraversalInOrder(function, avltreeNode.LeftChild) == StepStatus.Break)
    //	//			return StepStatus.Break;
    //	//		Type value = avltreeNode.Value;
    //	//		StepStatus status = function(ref value);
    //	//		avltreeNode.Value = value;
    //	//		if (status == StepStatus.Break)
    //	//			return StepStatus.Break;
    //	//		if (AvlTree_Linked<T>.TraversalInOrder(function, avltreeNode.RightChild) == StepStatus.Break)
    //	//			return StepStatus.Break;
    //	//	}
    //	//	return StepStatus.Continue;
    //	//}

    //	///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    //	///// <param name="function">The delegate to invoke on each item in the structure.</param>
    //	///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //	//public virtual void StepperPreOrder(Step<T> function)
    //	//{
    //	//	AvlTree_Linked<T>.TraversalPreOrder(function, _avlTree);
    //	//}
    //	//private static bool TraversalPreOrder(Step<T> function, Node avltreeNode)
    //	//{
    //	//	if (avltreeNode != null)
    //	//	{
    //	//		function(avltreeNode.Value);
    //	//		AvlTree_Linked<T>.TraversalPreOrder(function, avltreeNode.LeftChild);
    //	//		AvlTree_Linked<T>.TraversalPreOrder(function, avltreeNode.RightChild);
    //	//	}
    //	//	return true;
    //	//}

    //	///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    //	///// <param name="function">The delegate to invoke on each item in the structure.</param>
    //	///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //	//public virtual void StepperPreOrder(StepRef<T> function)
    //	//{
    //	//	AvlTree_Linked<T>.TraversalPreOrder(function, _avlTree);
    //	//}
    //	//private static bool TraversalPreOrder(StepRef<T> function, Node avltreeNode)
    //	//{
    //	//	if (avltreeNode != null)
    //	//	{
    //	//		Type value = avltreeNode.Value;
    //	//		function(ref value);
    //	//		avltreeNode.Value = value;
    //	//		AvlTree_Linked<T>.TraversalPreOrder(function, avltreeNode.LeftChild);
    //	//		AvlTree_Linked<T>.TraversalPreOrder(function, avltreeNode.RightChild);
    //	//	}
    //	//	return true;
    //	//}

    //	///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    //	///// <param name="function">The delegate to invoke on each item in the structure.</param>
    //	///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //	//public virtual StepStatus StepperPreOrder(StepBreak<T> function)
    //	//{
    //	//	return AvlTree_Linked<T>.TraversalPreOrder(function, _avlTree);
    //	//}
    //	//private static StepStatus TraversalPreOrder(StepBreak<T> function, Node avltreeNode)
    //	//{
    //	//	if (avltreeNode != null)
    //	//	{
    //	//		Type value = avltreeNode.Value;
    //	//		StepStatus status = function(value);
    //	//		avltreeNode.Value = value;
    //	//		if (status == StepStatus.Break)
    //	//			return StepStatus.Break;
    //	//		if (AvlTree_Linked<T>.TraversalPreOrder(function, avltreeNode.LeftChild) == StepStatus.Break)
    //	//			return StepStatus.Break;
    //	//		if (AvlTree_Linked<T>.TraversalPreOrder(function, avltreeNode.RightChild) == StepStatus.Break)
    //	//			return StepStatus.Break;
    //	//	}
    //	//	return StepStatus.Continue;
    //	//}

    //	///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    //	///// <param name="function">The delegate to invoke on each item in the structure.</param>
    //	///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //	//public virtual StepStatus StepperPreOrder(StepRefBreak<T> function)
    //	//{
    //	//	return AvlTree_Linked<T>.TraversalPreOrder(function, _avlTree);
    //	//}
    //	//private static StepStatus TraversalPreOrder(StepRefBreak<T> function, Node avltreeNode)
    //	//{
    //	//	if (avltreeNode != null)
    //	//	{
    //	//		Type value = avltreeNode.Value;
    //	//		StepStatus status = function(ref value);
    //	//		avltreeNode.Value = value;
    //	//		if (status == StepStatus.Break)
    //	//			return StepStatus.Break;
    //	//		if (AvlTree_Linked<T>.TraversalPreOrder(function, avltreeNode.LeftChild) == StepStatus.Break)
    //	//			return StepStatus.Break;
    //	//		if (AvlTree_Linked<T>.TraversalPreOrder(function, avltreeNode.RightChild) == StepStatus.Break)
    //	//			return StepStatus.Break;
    //	//	}
    //	//	return StepStatus.Continue;
    //	//}

    //	///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    //	///// <param name="function">The delegate to invoke on each item in the structure.</param>
    //	///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //	//public virtual void StepperPostOrder(Step<T> function)
    //	//{
    //	//	AvlTree_Linked<T>.TraversalPostOrder(function, _avlTree);
    //	//}
    //	//private static bool TraversalPostOrder(Step<T> function, Node avltreeNode)
    //	//{
    //	//	if (avltreeNode != null)
    //	//	{
    //	//		AvlTree_Linked<T>.TraversalPostOrder(function, avltreeNode.RightChild);
    //	//		function(avltreeNode.Value);
    //	//		AvlTree_Linked<T>.TraversalPostOrder(function, avltreeNode.LeftChild);
    //	//	}
    //	//	return true;
    //	//}

    //	///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    //	///// <param name="function">The delegate to invoke on each item in the structure.</param>
    //	///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //	//public virtual void StepperPostOrder(StepRef<T> function)
    //	//{
    //	//	AvlTree_Linked<T>.TraversalPostOrder(function, _avlTree);
    //	//}
    //	//private static bool TraversalPostOrder(StepRef<T> function, Node avltreeNode)
    //	//{
    //	//	if (avltreeNode != null)
    //	//	{
    //	//		AvlTree_Linked<T>.TraversalPostOrder(function, avltreeNode.RightChild);
    //	//		Type value = avltreeNode.Value;
    //	//		function(ref value);
    //	//		avltreeNode.Value = value;
    //	//		AvlTree_Linked<T>.TraversalPostOrder(function, avltreeNode.LeftChild);
    //	//	}
    //	//	return true;
    //	//}

    //	///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    //	///// <param name="function">The delegate to invoke on each item in the structure.</param>
    //	///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //	//public virtual StepStatus StepperPostOrder(StepBreak<T> function)
    //	//{
    //	//	return AvlTree_Linked<T>.TraversalPostOrder(function, _avlTree);
    //	//}
    //	//private static StepStatus TraversalPostOrder(StepBreak<T> function, Node avltreeNode)
    //	//{
    //	//	if (avltreeNode != null)
    //	//	{
    //	//		if (AvlTree_Linked<T>.TraversalPostOrder(function, avltreeNode.RightChild) == StepStatus.Break)
    //	//			return StepStatus.Break;
    //	//		Type value = avltreeNode.Value;
    //	//		StepStatus status = function(value);
    //	//		avltreeNode.Value = value;
    //	//		if (status == StepStatus.Break)
    //	//			return StepStatus.Break;
    //	//		if (AvlTree_Linked<T>.TraversalPostOrder(function, avltreeNode.LeftChild) == StepStatus.Break)
    //	//			return StepStatus.Break;
    //	//	}
    //	//	return StepStatus.Continue;
    //	//}

    //	///// <summary>Invokes a delegate for each entry in the data structure.</summary>
    //	///// <param name="function">The delegate to invoke on each item in the structure.</param>
    //	///// <remarks>Runtime: O(n * traversalFunction).</remarks>
    //	//public virtual StepStatus StepperPostOrder(StepRefBreak<T> function)
    //	//{
    //	//	return AvlTree_Linked<T>.TraversalPostOrder(function, _avlTree);
    //	//}
    //	//private static StepStatus TraversalPostOrder(StepRefBreak<T> function, Node avltreeNode)
    //	//{
    //	//	if (avltreeNode != null)
    //	//	{
    //	//		if (AvlTree_Linked<T>.TraversalPostOrder(function, avltreeNode.RightChild) == StepStatus.Break)
    //	//			return StepStatus.Break;
    //	//		Type value = avltreeNode.Value;
    //	//		StepStatus status = function(ref value);
    //	//		avltreeNode.Value = value;
    //	//		if (status == StepStatus.Break)
    //	//			return StepStatus.Break;
    //	//		if (AvlTree_Linked<T>.TraversalPostOrder(function, avltreeNode.LeftChild) == StepStatus.Break)
    //	//			return StepStatus.Break;
    //	//	}
    //	//	return StepStatus.Continue;
    //	//}

    //	///// <summary>Creates an array out of the values in this structure.</summary>
    //	///// <returns>An array containing the values in this structure.</returns>
    //	///// <remarks>Runtime: Towel(n).</remarks>
    //	//public virtual Type[] ToArray()
    //	//{
    //	//	Type[] array = new Type[_count];
    //	//	ToArray(array, _avlTree, 0);
    //	//	return array;
    //	//}
    //	//private void ToArray(Type[] array, Node avltreeNode, int position)
    //	//{
    //	//	if (avltreeNode != null)
    //	//	{
    //	//		ToArray(array, avltreeNode.LeftChild, position);
    //	//		array[position++] = avltreeNode.Value;
    //	//		ToArray(array, avltreeNode.RightChild, position);
    //	//	}
    //	//}
    //}
    #endregion
}
