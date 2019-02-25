namespace Towel.DataStructures
{
	/// <summary>A self-sorting binary tree based on grouping nodes together at the same height.</summary>
	/// <typeparam name="T">The generic type of this data structure.</typeparam>
	public interface BTree<T> : Structure<T>,
		// Structure Properties
		Structure.Clearable<T>,
		Structure.Addable<T>,
		Structure.Countable<T>,
		Structure.Removable<T>,
		Structure.Auditable<T>
	{
		#region Methods

		/// <summary>Determines if the structure contains an item based on a given key.</summary>
		/// <typeparam name="Key">The type of the key.</typeparam>
		/// <param name="key">The key.</param>
		/// <param name="comparison">The comparison technique (must synchronize with the sorting of this data structure).</param>
		/// <returns>True if contained, False if not.</returns>
		bool Contains<Key>(Key key, Compare<T, Key> comparison);
		/// <summary>Gets an item based on a given key.</summary>
		/// <typeparam name="Key">The type of the key.</typeparam>
		/// <param name="get">The key.</param>
		/// <param name="compare">The comparison technique (must synchronize with the sorting of this data structure).</param>
		/// <returns>The found item.</returns>
		T Get<Key>(Key get, Compare<T, Key> compare);
		/// <summary>Removes an item based on a given key.</summary>
		/// <typeparam name="Key">The type of the key.</typeparam>
		/// <param name="removal">The key.</param>
		/// <param name="compare">The comparison technique (must synchronize with the sorting of this data structure).</param>
		void Remove<Key>(Key removal, Compare<T, Key> compare);
		/// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
		/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
		void StepperReverse(Step<T> step_delegate);
		/// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
		/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		StepStatus StepperReverse(StepBreak<T> step_delegate);
		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <param name="step_function">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		void Stepper(Step<T> step_function, T minimum, T maximum);
		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <param name="step_function">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The result status of the stepper function.</returns>
		StepStatus Stepper(StepBreak<T> step_function, T minimum, T maximum);
		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <param name="step_function">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		void StepperReverse(Step<T> step_function, T minimum, T maximum);
		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <param name="step_function">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The result status of the stepper function.</returns>
		StepStatus StepperReverse(StepBreak<T> step_function, T minimum, T maximum);

		#endregion
	}

	/// <summary>Contains extensions methods for the BTree<T> interface.</summary>
	public static class BTree
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
		public static bool TryGet<T, Key>(this BTree<T> structure, Key get, Compare<T, Key> comparison, out T item)
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
		public static bool TryRemove<T, Key>(this BTree<T> structure, Key removal, Compare<T, Key> comparison)
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

	/// <summary>A self-sorting binary tree based on grouping nodes together at the same height.</summary>
	/// <typeparam name="T">The generic type of this data structure.</typeparam>
	[System.Serializable]
	public class BTreeLinkedArray<T> : BTree<T>
	{
		// Fields
		Node _root;
		int _count;
		int _height;
		int _node_size;
		Compare<T, T> _compare;

		#region Nested Types

		/// <summary>A BTree node can contain multiple items and children.</summary>
		public class Node
		{
			private T[] _items;
			private Node[] _children;
			private int _item_count;
			private int _child_count;

			internal T[] Items { get { return this._items; } set { this._items = value; } }
			internal Node[] Children { get { return this._children; } set { this._children = value; } }
			internal int ItemCount { get { return this._item_count; } set { this._item_count = value; } }
			internal int ChildCount { get { return this._child_count; } set { this._child_count = value; } }

			internal Node(int size)
			{
				this._items = new T[size * 2];
				this._children = new Node[size * 2 + 1];
				this._item_count = 0;
				this._child_count = 0;
			}
		}

		#endregion

		#region Constructors

		public BTreeLinkedArray(Compare<T> compare, int node_size)
		{
			if (node_size < 2)
				throw new System.ArgumentOutOfRangeException("node_size");

			this._node_size = node_size;
			this._root = new Node(this._node_size);
			this._compare = new Compare<T,T>(compare);
			this._count = 0;
			this._height = 1;
		}

		#endregion

		#region Properties

		/// <summary>The number of items in this structure.</summary>
		public int Count { get { return this._count; } }

		#endregion

		#region Methods

		// methods (public)
		#region public bool Contains(T item)
		/// <summary>Determines if this structure contains a given item.</summary>
		/// <param name="item">The item to be removed.</param>
		/// <returns>True if found, False if not.</returns>
		public bool Contains(T item)
		{
			return this.Contains(item, this._compare);
		}
		#endregion
		#region public bool Contains<K>(K key, Compare<T, K> compare)
		/// <summary>Determines if this structure contains a given item based on a given key.</summary>
		/// <typeparam name="K">The type of the key.</typeparam>
		/// <param name="key">The key.</param>
		/// <param name="compare">The sorting technique (must synchronize with the sorting of this structure).</param>
		/// <returns>True if found, False if not.</returns>
		public bool Contains<K>(K key, Compare<T, K> compare)
		{
			return this.Contains(this._root, key, compare);
		}
		#endregion
		#region public T Get<K>(K key, Compare<T, K> compare)
		/// <summary>Gets an item from this structure based on a given key.</summary>
		/// <typeparam name="K">The type of the key.</typeparam>
		/// <param name="key">The key.</param>
		/// <param name="compare">The comparison techmique (must synchronize with this structure's sorting).</param>
		/// <returns>Item is found.</returns>
		public T Get<K>(K key, Compare<T, K> compare)
		{
			return this.Get(this._root, key, compare);
		}
		#endregion
		#region public void Add(T addition)
		/// <summary>Adds an item to the this structure.</summary>
		/// <param name="addition">The item to be added.</param>
		public void Add(T addition)
		{
			if (!(this._root.ItemCount == this._node_size))//(2 * this._node_size) - 1))
			{
				this.Add_NonFull(this._root, addition);
				this._count++;
				return;
			}

			Node oldRoot = this._root;
			this._root = new Node(this._node_size);
			this._root.Children[this._root.ChildCount++] = oldRoot;
			this.Add_SplitChild(this._root, 0, oldRoot);
			this.Add_NonFull(this._root, addition);

			this._height++;
			this._count++;
		}
		#endregion
		#region public void Remove(T removal)
		/// <summary>Removes an item from this structure.</summary>
		/// <param name="removal">The item to be removed.</param>
		public void Remove(T removal)
		{
			this.Remove(removal, this._compare);
			this._count--;
		}
		#endregion
		#region public void Remove<K>(K key, Compare<T, K> compare)
		/// <summary>Removes an item from the structure based on a key.</summary>
		/// <typeparam name="K">The type of the key.</typeparam>
		/// <param name="key">The key.</param>
		/// <param name="compare">The compareison technique (must synchronize with this structure's sorting).</param>
		public void Remove<K>(K key, Compare<T, K> compare)
		{
			this.Remove(this._root, key, compare);

			if (this._root.ItemCount == 0 && !(this._root.ChildCount == 0))
			{
				if (this._root.ChildCount != 1)
					throw new System.InvalidOperationException("this._root.ChildCount != 1");
				this._root = this._root.Children[0];
				this._height--;
			}

			this._count--;
		}
		#endregion
		#region public void StepperReverse(Step<T> step_delegate)
		/// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
		/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
		public void StepperReverse(Step<T> step_delegate)
		{
			this.StepperReverse(step_delegate, this._root);
		}
		#endregion
		#region public void StepperReverse(StepRef<T> step_delegate)
		/// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
		/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
		public void StepperReverse(StepRef<T> step_delegate)
		{
			this.StepperReverse(step_delegate, this._root);
		}
		#endregion
		#region public StepStatus StepperReverse(StepBreak<T> step_delegate)
		/// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
		/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus StepperReverse(StepBreak<T> step_delegate)
		{
			return this.StepperReverse(step_delegate, this._root);
		}
		#endregion
		#region public StepStatus StepperReverse(StepRefBreak<T> step_delegate)
		/// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
		/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus StepperReverse(StepRefBreak<T> step_delegate)
		{
			return this.StepperReverse(step_delegate, this._root);
		}
		#endregion
		#region public void Stepper(Step<T> step_function, T minimum, T maximum)
		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <param name="step_function">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		public void Stepper(Step<T> step_function, T minimum, T maximum)
		{
			throw new System.NotImplementedException();
		}
		#endregion
		#region public void Stepper(StepRef<T> step_function, T minimum, T maximum)
		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <param name="step_function">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		public void Stepper(StepRef<T> step_function, T minimum, T maximum)
		{
			throw new System.NotImplementedException();
		}
		#endregion
		#region public StepStatus Stepper(StepBreak<T> step_function, T minimum, T maximum)
		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <param name="step_function">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The result status of the stepper function.</returns>
		public StepStatus Stepper(StepBreak<T> step_function, T minimum, T maximum)
		{
			throw new System.NotImplementedException();
		}
		#endregion
		#region public StepStatus Stepper(StepRefBreak<T> step_function, T minimum, T maximum)
		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <param name="step_function">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The result status of the stepper function.</returns>
		public StepStatus Stepper(StepRefBreak<T> step_function, T minimum, T maximum)
		{
			throw new System.NotImplementedException();
		}
		#endregion
		#region public void StepperReverse(Step<T> step_function, T minimum, T maximum)
		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <param name="step_function">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		public void StepperReverse(Step<T> step_function, T minimum, T maximum)
		{
			throw new System.NotImplementedException();
		}
		#endregion
		#region public void StepperReverse(StepRef<T> step_function, T minimum, T maximum)
		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <param name="step_function">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		public void StepperReverse(StepRef<T> step_function, T minimum, T maximum)
		{
			throw new System.NotImplementedException();
		}
		#endregion
		#region public StepStatus StepperReverse(StepBreak<T> step_function, T minimum, T maximum)
		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <param name="step_function">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The result status of the stepper function.</returns>
		public StepStatus StepperReverse(StepBreak<T> step_function, T minimum, T maximum)
		{
			throw new System.NotImplementedException();
		}
		#endregion
		#region public StepStatus StepperReverse(StepRefBreak<T> step_function, T minimum, T maximum)
		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <param name="step_function">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The result status of the stepper function.</returns>
		public StepStatus StepperReverse(StepRefBreak<T> step_function, T minimum, T maximum)
		{
			throw new System.NotImplementedException();
		}
		#endregion
		#region public void Clear()
		/// <summary>Returns the structure to an empty state.</summary>
		public void Clear()
		{
			this._root = new Node(this._node_size);
			this._count = 0;
		}
		#endregion
		#region public void Stepper(Step<T> step_delegate)
		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Step<T> step_delegate)
		{
			Stepper(step_delegate, this._root);
		}
		#endregion
		#region public void Stepper(StepRef<T> step_delegate)
		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
		public void Stepper(StepRef<T> step_delegate)
		{
			Stepper(step_delegate, this._root);
		}
		#endregion
		#region public StepStatus Stepper(StepBreak<T> step_delegate)
		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepBreak<T> step_delegate)
		{
			return Stepper(step_delegate, this._root);
		}
		#endregion
		#region public StepStatus Stepper(StepRefBreak<T> step_delegate)
		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepRefBreak<T> step_delegate)
		{
			return Stepper(step_delegate, this._root);
		}
		#endregion
		#region System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		/// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
		System.Collections.IEnumerator
			System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
		#endregion
		#region public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		/// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			Stack<Link<Node, int, bool>> forks = new StackLinked<Link<Node, int, bool>>();
			forks.Push(new Link<Node, int, bool>(this._root, 0, false));
			while (forks.Count > 0)
			{
				Link<Node, int, bool> link = forks.Pop();
				if (link._1 == null)
					continue;
				else if (!link._3)
				{
					link._3 = true;
					forks.Push(link);
					Node child = link._1.Children[link._2];
					forks.Push(new Link<Node, int, bool>(child, 0, false));
				}
				else if (link._2 < link._1.ItemCount)
				{
					yield return link._1.Items[link._2];
					link._2++;
					link._3 = false;
					forks.Push(link);
				}
			}
		}
		#endregion
		// methods (internal)
		#region private bool Contains<K>(Node node, K key, Compare<T, K> compare)
		private bool Contains<K>(Node node, K key, Compare<T, K> compare)
		{
			int loc = 0;
			for (int i = 0; i < node.ItemCount; i++)
				switch (compare(node.Items[i], key))
				{
					case Comparison.Less:
						loc++;
						continue;
					case Comparison.Equal:
						goto break_for;
					case Comparison.Greater:
						goto break_for;
					default:
						throw new System.NotImplementedException();
				}
		break_for:
			if (loc < node.ItemCount && compare(node.Items[loc], key) == Comparison.Equal)
				return true;
			if (node.ChildCount == 0)
				return false;
			return this.Contains(node.Children[loc], key, compare);
		}
		#endregion
		#region private T Get<K>(Node node, K key, Compare<T, K> compare)
		private T Get<K>(Node node, K key, Compare<T, K> compare)
		{
			int loc = 0;
			for (int i = 0; i < node.ItemCount; i++)
				switch (compare(node.Items[i], key))
				{
					case Comparison.Less:
						loc++;
						continue;
					case Comparison.Equal:
						goto break_for;
					case Comparison.Greater:
						goto break_for;
					default:
						throw new System.NotImplementedException();
				}
		break_for:
			if (loc < node.ItemCount && compare(node.Items[loc], key) == Comparison.Equal)
				return node.Items[loc];
			if (node.ChildCount == 0)
				throw new System.InvalidOperationException("getting a non-existing item");
			return this.Get(node.Children[loc], key, compare);
		}
		#endregion
		#region private void Remove<K>(Node node, K key, Compare<T, K> compare)
		private void Remove<K>(Node node, K key, Compare<T, K> compare)
		{
			int loc = 0;
			for (int i = 0; i < node.ItemCount; i++)
				switch (compare(node.Items[i], key))
				{
					case Comparison.Less:
						loc++;
						continue;
					case Comparison.Equal:
						goto break_for;
					case Comparison.Greater:
						goto break_for;
					default:
						throw new System.NotImplementedException();
				}
		break_for:
			if (loc < node.ItemCount && compare(node.Items[loc], key) == Comparison.Equal)
			{
				this.RemoveKeyFromNode(node, key, compare, loc);
				return;
			}
			if (!(node.ChildCount == 0))
			{
				this.RemoveKeyFromSubtree(node, key, compare, loc);
			}
		}
		#endregion
		#region private void RemoveKeyFromSubtree<K>(Node parentNode, K key, Compare<T, K> compare, int subtreeIndexInNode)
		private void RemoveKeyFromSubtree<K>(Node parentNode, K key, Compare<T, K> compare, int subtreeIndexInNode)
		{
			Node childNode = parentNode.Children[subtreeIndexInNode];

			if (childNode.ItemCount == this._node_size - 1)
			{
				int leftIndex = subtreeIndexInNode - 1;
				Node leftSibling = subtreeIndexInNode > 0 ? parentNode.Children[leftIndex] : null;

				int rightIndex = subtreeIndexInNode + 1;
				Node rightSibling = subtreeIndexInNode < parentNode.ChildCount - 1 ? parentNode.Children[rightIndex] : null;

				if (leftSibling != null && leftSibling.ItemCount > this._node_size - 1)
				{
					for (int i = childNode.ChildCount; i > -1; i--)
						childNode.Items[i] = childNode.Items[i - 1];
					childNode.Items[0] = parentNode.Items[subtreeIndexInNode];
					childNode.ItemCount++;
					parentNode.Items[subtreeIndexInNode] = leftSibling.Items[leftSibling.ItemCount - 1];
					leftSibling.ItemCount--;

					if (!(leftSibling.ChildCount == 0))
					{
						for (int i = childNode.ChildCount; i > -1; i--)
							childNode.Children[i] = childNode.Children[i - 1];
						childNode.Children[0] = leftSibling.Children[leftSibling.ChildCount - 1];
						leftSibling.ChildCount--;
					}
				}
				else if (rightSibling != null && rightSibling.ItemCount > this._node_size - 1)
				{
					childNode.Items[childNode.ItemCount] = parentNode.Items[subtreeIndexInNode];
					childNode.ItemCount++;
					parentNode.Items[subtreeIndexInNode] = rightSibling.Items[0];
					for (int i = 0; i < rightSibling.ItemCount; i++)
						rightSibling.Items[i] = rightSibling.Items[i + 1];
					rightSibling.ItemCount--;

					if (!(rightSibling.ChildCount == 0))
					{
						childNode.Children[childNode.ChildCount - 1] = rightSibling.Children[0];
						childNode.ChildCount++;
						for (int i = 0; i < rightSibling.ChildCount; i++)
							rightSibling.Items[i] = rightSibling.Items[i + 1];
						rightSibling.ChildCount--;
					}
				}
				else
				{
					if (leftSibling != null)
					{
						for (int i = childNode.ItemCount; i > 0; i--)
							childNode.Items[i] = childNode.Items[i - 1];
						childNode.Items[0] = parentNode.Items[subtreeIndexInNode];
						childNode.ItemCount++;

						T[] oldEntries = childNode.Items;
						int oldEntries_count = childNode.ItemCount;
						childNode.Items = leftSibling.Items;
						childNode.ItemCount = leftSibling.ItemCount;
						for (int i = 0; i < oldEntries_count; i++)
						{
							childNode.Items[childNode.ItemCount - 1] = oldEntries[i];
							childNode.ItemCount++;
						}

						if (!(leftSibling.ChildCount == 0))
						{
							Node[] oldChildren = childNode.Children;
							int oldCildren_count = childNode.ChildCount;
							childNode.Children = leftSibling.Children;
							childNode.ChildCount = leftSibling.ChildCount;
							for (int i = 0; i < oldCildren_count; i++)
							{
								childNode.Children[childNode.ItemCount - 1] = oldChildren[i];
								childNode.ChildCount++;
							}
						}

						for (int i = leftIndex; i < parentNode.ChildCount; i++)
							parentNode.Children[i] = parentNode.Children[i + 1];
						parentNode.ChildCount--;

						for (int i = subtreeIndexInNode; i < parentNode.ItemCount - 1; i++)
							parentNode.Items[i] = parentNode.Items[i + 1];
						parentNode.ItemCount--;
					}
					else
					{
						childNode.Items[childNode.ItemCount] = parentNode.Items[subtreeIndexInNode];
						childNode.ItemCount++;

						for (int i = 0; i < rightSibling.ItemCount; i++)
						{
							childNode.Items[childNode.ItemCount] = rightSibling.Items[i];
							childNode.ItemCount++;
						}

						if (!(rightSibling.ChildCount == 0))
						{
							for (int i = 0; i < rightSibling.ChildCount; i++)
							{
								childNode.Children[childNode.ChildCount] = rightSibling.Children[i];
								childNode.ChildCount++;
							}
						}

						for (int i = rightIndex; i < parentNode.ChildCount; i++)
							parentNode.Children[i] = parentNode.Children[i + 1];
						parentNode.ChildCount--;

						for (int i = subtreeIndexInNode; i < parentNode.ItemCount; i++)
							parentNode.Items[i] = parentNode.Items[i + 1];
						parentNode.ItemCount--;
					}
				}
			}

			this.Remove(childNode, key, compare);
		}
		#endregion
		#region private void RemoveKeyFromNode<K>(Node node, K keyToDelete, Compare<T, K> compare, int keyIndexInNode)
		private void RemoveKeyFromNode<K>(Node node, K keyToDelete, Compare<T, K> compare, int keyIndexInNode)
		{
			if (node.ChildCount == 0)
			{
				for (int i = keyIndexInNode; i < node.ItemCount; i++)
					node.Items[i] = node.Items[i + 1];
				node.ItemCount--;
				return;
			}

			Node predecessorChild = node.Children[keyIndexInNode];
			if (predecessorChild.ItemCount >= this._node_size)
			{
				T predecessor = this.RemovePredecessor(predecessorChild);
				node.Items[keyIndexInNode] = predecessor;
			}
			else
			{
				Node successorChild = node.Children[keyIndexInNode + 1];
				if (successorChild.ItemCount >= this._node_size)
				{
					T successor = this.RemoveSuccessor(predecessorChild);
					node.Items[keyIndexInNode] = successor;
				}
				else
				{
					predecessorChild.Items[predecessorChild.ItemCount++] = node.Items[keyIndexInNode];
					for (int i = 0; i < successorChild.ItemCount; i++)
						predecessorChild.Items[predecessorChild.ItemCount++] = successorChild.Items[i];
					for (int i = 0; i < successorChild.ChildCount; i++)
						predecessorChild.Items[predecessorChild.ChildCount++] = successorChild.Items[i];
					for (int i = keyIndexInNode; i < node.ItemCount; i++)
						node.Items[i] = node.Items[i + 1];
					node.ItemCount--;
					for (int i = keyIndexInNode + 1; i < node.ChildCount; i++)
						node.Children[i] = node.Children[i + 1];
					node.ChildCount--;
					this.Remove(predecessorChild, keyToDelete, compare);
				}
			}
		}
		#endregion
		#region private T RemovePredecessor(Node node)
		private T RemovePredecessor(Node node)
		{
			if (node.ChildCount == 0)
			{
				T result = node.Items[node.ItemCount - 1];
				for (int i = node.ItemCount - 1; i < node.ItemCount; i++)
					node.Items[i] = node.Items[i + 1];
				node.ItemCount--;
				return result;
			}
			return this.RemovePredecessor(node.Children[node.ChildCount - 1]);
		}
		private T RemoveSuccessor(Node node)
		{
			if (node.ChildCount == 0)
			{
				T result = node.Items[0];
				for (int i = 0; i < node.ItemCount; i++)
					node.Items[i] = node.Items[i + 1];
				node.ItemCount--;
				return result;
			}
			return this.RemovePredecessor(node.Children[0]);
		}
		#endregion
		#region private void Add_SplitChild(Node parentNode, int nodeToBeSplitIndex, Node nodeToBeSplit)
		private void Add_SplitChild(Node parentNode, int nodeToBeSplitIndex, Node nodeToBeSplit)
		{
			Node newNode = new Node(this._node_size);
			for (int i = nodeToBeSplitIndex; i < parentNode.ItemCount; i++)
				parentNode.Items[i] = parentNode.Items[i + 1];
			parentNode.Items[nodeToBeSplitIndex] = nodeToBeSplit.Items[this._node_size - 1];
			for (int i = nodeToBeSplitIndex + 1; i < parentNode.ChildCount; i++)
				parentNode.Children[i] = parentNode.Children[i + 1];
			parentNode.Children[nodeToBeSplitIndex] = newNode;
			//int end_range = this._node_size + this._node_size - 1;
			//for (int i = this._node_size; i < end_range; i++)
			//	newNode.Items[newNode.ItemCount++] = nodeToBeSplit.Items[i];

			int end_range = this._node_size;
			for (int i = 0; i < end_range; i++)
				newNode.Items[newNode.ItemCount++] = nodeToBeSplit.Items[i];

			nodeToBeSplit.ItemCount -= this._node_size;
			if (!(nodeToBeSplit.ChildCount == 0))
			{
				int end_range_2 = this._node_size * 2;
				for (int i = this._node_size; i < end_range_2; i++)
					newNode.Children[newNode.ChildCount++] = nodeToBeSplit.Children[i];
				nodeToBeSplit.ChildCount -= this._node_size;
			}
		}
		#endregion
		#region private void Add_NonFull(Node node, T addition)
		private void Add_NonFull(Node node, T addition)
		{
			int positionToInsert = 0;
			for (int i = 0; i < node.ItemCount; i++)
				switch (this._compare(addition, node.Items[i]))
				{
					case Comparison.Less:
						goto break_for;
					case Comparison.Equal:
						positionToInsert++;
						continue;
					case Comparison.Greater:
						positionToInsert++;
						continue;
					default:
						throw new System.NotImplementedException();
				}
		break_for:
			if (node.ChildCount == 0) // leaf node
			{
				for (int i = positionToInsert; i < node.ItemCount; i++)
					node.Items[i] = node.Items[i + 1];
				node.Items[positionToInsert] = addition;
				node.ItemCount++;
				return;
			}
			Node child = node.Children[positionToInsert];
			if (child.ItemCount == this._node_size)// (2 * this._node_size) - 1) // non-leaf
			{
				this.Add_SplitChild(node, positionToInsert, child);
				if (this._compare(addition, node.Items[positionToInsert]) == Comparison.Greater)
				{
					positionToInsert++;
				}
			}
			//if (node.Children[positionToInsert] == null)
			//	node.Children[positionToInsert] = new Node(this._node_size);
			this.Add_NonFull(node.Children[positionToInsert], addition);
		}
		#endregion
		#region private void StepperReverse(Step<T> step_delegate, Node node)
		private void StepperReverse(Step<T> step_delegate, Node node)
		{
			if (node == null)
				return;
			Stepper(step_delegate, node.Children[node.ItemCount]);
			for (int i = node.ItemCount - 1; i > -1; i--)
			{
				step_delegate(node.Items[i]);
				Stepper(step_delegate, node.Children[i]);
			}
		}
		#endregion
		#region private void StepperReverse(StepRef<T> step_delegate, Node node)
		private void StepperReverse(StepRef<T> step_delegate, Node node)
		{
			if (node == null)
				return;
			Stepper(step_delegate, node.Children[node.ItemCount]);
			for (int i = node.ItemCount - 1; i > -1; i--)
			{
				step_delegate(ref node.Items[i]);
				Stepper(step_delegate, node.Children[i]);
			}
		}
		#endregion
		#region private StepStatus StepperReverse(StepBreak<T> step_delegate, Node node)
		private StepStatus StepperReverse(StepBreak<T> step_delegate, Node node)
		{
			if (node == null)
				return StepStatus.Continue;
			switch (Stepper(step_delegate, node.Children[node.ItemCount]))
			{
				case StepStatus.Break:
					return StepStatus.Break;
				case StepStatus.Continue:
					break;
				default:
					throw new System.NotImplementedException();
			}
			for (int i = node.ItemCount - 1; i > -1; i--)
			{
				switch (step_delegate(node.Items[i]))
				{
					case StepStatus.Break:
						return StepStatus.Break;
					case StepStatus.Continue:
						break;
					default:
						throw new System.NotImplementedException();
				}
				switch (Stepper(step_delegate, node.Children[i]))
				{
					case StepStatus.Break:
						return StepStatus.Break;
					case StepStatus.Continue:
						break;
					default:
						throw new System.NotImplementedException();
				}
			}
			return StepStatus.Continue;
		}
		#endregion
		#region private StepStatus StepperReverse(StepRefBreak<T> step_delegate, Node node)
		private StepStatus StepperReverse(StepRefBreak<T> step_delegate, Node node)
		{
			if (node == null)
				return StepStatus.Continue;
			switch (Stepper(step_delegate, node.Children[node.ItemCount]))
			{
				case StepStatus.Break:
					return StepStatus.Break;
				case StepStatus.Continue:
					break;
				default:
					throw new System.NotImplementedException();
			}
			for (int i = node.ItemCount - 1; i > -1; i--)
			{
				switch (step_delegate(ref node.Items[i]))
				{
					case StepStatus.Break:
						return StepStatus.Break;
					case StepStatus.Continue:
						break;
					default:
						throw new System.NotImplementedException();
				}
				switch (Stepper(step_delegate, node.Children[i]))
				{
					case StepStatus.Break:
						return StepStatus.Break;
					case StepStatus.Continue:
						break;
					default:
						throw new System.NotImplementedException();
				}
			}
			return StepStatus.Continue;
		}
		#endregion
		#region private void Stepper(Step<T> step_delegate, Node node)
		private void Stepper(Step<T> step_delegate, Node node)
		{
			if (node == null)
				return;
			for (int i = 0; i < this._node_size && i < node.ItemCount; i++)
			{
				Stepper(step_delegate, node.Children[i]);
				step_delegate(node.Items[i]);
			}
			Stepper(step_delegate, node.Children[node.ItemCount]);
		}
		#endregion
		#region private void Stepper(StepRef<T> step_delegate, Node node)
		private void Stepper(StepRef<T> step_delegate, Node node)
		{
			if (node == null)
				return;
			for (int i = 0; i < this._node_size && i < node.ItemCount; i++)
			{
				Stepper(step_delegate, node.Children[i]);
				step_delegate(ref node.Items[i]);
			}
			Stepper(step_delegate, node.Children[node.ItemCount]);
		}
		#endregion
		#region private StepStatus Stepper(StepBreak<T> step_delegate, Node node)
		private StepStatus Stepper(StepBreak<T> step_delegate, Node node)
		{
			if (node == null)
				return StepStatus.Continue;
			for (int i = 0; i < this._node_size && i < node.ItemCount; i++)
			{
				switch (Stepper(step_delegate, node.Children[i]))
				{
					case StepStatus.Break:
						return StepStatus.Break;
					case StepStatus.Continue:
						break;
					default:
						throw new System.NotImplementedException();
				}
				switch (step_delegate(node.Items[i]))
				{
					case StepStatus.Continue:
						continue;
					case StepStatus.Break:
						return StepStatus.Break;
					default:
						throw new System.NotImplementedException();
				}
			}
			return Stepper(step_delegate, node.Children[node.ItemCount]);
		}
		#endregion
		#region private StepStatus Stepper(StepRefBreak<T> step_delegate, Node node)
		private StepStatus Stepper(StepRefBreak<T> step_delegate, Node node)
		{
			if (node == null)
				return StepStatus.Continue;
			for (int i = 0; i < this._node_size && i < node.ItemCount; i++)
			{
				switch (Stepper(step_delegate, node.Children[i]))
				{
					case StepStatus.Break:
						return StepStatus.Break;
					case StepStatus.Continue:
						break;
					default:
						throw new System.NotImplementedException();
				}
				switch (step_delegate(ref node.Items[i]))
				{
					case StepStatus.Continue:
						continue;
					case StepStatus.Break:
						return StepStatus.Break;
					default:
						throw new System.NotImplementedException();
				}
			}
			return Stepper(step_delegate, node.Children[node.ItemCount]);
		}
		#endregion

		#endregion
	}

	#region In Development

	///// <summary>A self-sorting binary tree based on grouping nodes together at the same height.</summary>
	///// <typeparam name="T">The generic type of this data structure.</typeparam>
	//public class BTree_LinkedArray<T> : BTree<T>
	//{
	//	#region Node

	//	/// <summary>A BTree node can contain multiple items and children.</summary>
	//	public class Node
	//	{
	//		private T[] _items;
	//		private Node[] _children;
	//		private Node _parent;
	//		private int _count;

	//		internal T[] Items { get { return this._items; } set { this._items = value; } }
	//		internal Node[] Children { get { return this._children; } set { this._children = value; } }
	//		internal Node Parent { get { return this._parent; } set { this._parent = value; } }
	//		internal int Count { get { return this._count; } set { this._count = value; } }

	//		internal Node(Node parent, int size)
	//		{
	//			this._items = new T[size];
	//			this._children = new Node[size + 1];
	//			this._parent = parent;
	//			this._count = 0;
	//		}
	//	}

	//	#endregion

	//	#region BTree_LinkedArray<T>

	//	#region field

	//	Node _root;
	//	int _count;
	//	int _node_size;
	//	Compare<T> _compare;

	//	#endregion

	//	#region construct

	//	public BTree_LinkedArray(Compare<T> compare, int node_size)
	//	{
	//		this._node_size = node_size;
	//		this._root = new Node(null, this._node_size);
	//		this._compare = compare;
	//		this._count = 0;
	//	}

	//	#endregion

	//	#region method

	//	#endregion

	//	#endregion

	//	#region BTree<T>

	//	/// <summary>The current number of items in this structure.</summary>
	//	public int Count { get { return _count; } }

	//	/// <summary>Adds and item to this structure.</summary>
	//	/// <param name="addition">The item to be added.</param>
	//	public void Add(T addition)
	//	{
	//		this.Add(addition, this._root);
	//	}
	//	private void Add(T addition, Node node)
	//	{
	//		int Index = 0;
	//		while (true)
	//		{
	//			Index = 0;
	//			int NoOfOccupiedSlots = node.Count;
	//			if (NoOfOccupiedSlots > 1)
	//			{
	//				Index = BinarySearch(node.Items, 0, NoOfOccupiedSlots, addition, this._compare);
	//				if (Index < 0)
	//					Index = ~Index;
	//			}
	//			else if (NoOfOccupiedSlots == 1)
	//			{
	//				if (this._compare(node.Items[0], addition) == Comparison.Less)
	//					Index = 1;
	//			}
	//			if (node.Children != null)	// if not an outermost node let next lower level node do the 'Add'.
	//				node = node.Children[Index];
	//			else
	//				break;
	//		}
	//		this.Add(node, addition, Index);
	//		node = null;
	//	}

	//	private static int BinarySearch<T>(T[] array, int index, int length, T value, Compare<T> compare)
	//	{
	//		int low = index;
	//		int hi = index + length - 1;
	//		while (low <= hi)
	//		{
	//			int median = low + (hi - low >> 1);
	//			switch (compare(array[median], value))
	//			{
	//				case Comparison.Equal:
	//					return median;
	//				case Comparison.Greater:
	//					hi = median - 1;
	//					break;
	//				case Comparison.Less:
	//					low = median + 1;
	//					break;
	//				default:
	//					throw new System.NotImplementedException();
	//			}
	//		}
	//		return ~low;
	//	}

	//	private void Add(Node node, T addition, int index)
	//	{
	//		if (node.Count < this._node_size)
	//		{
	//			if (index < node.Count)
	//				for (int i = node.Count; i >= index; i--)
	//					node.Items[i] = node.Items[i - 1];
	//			node.Items[index] = addition;
	//			node.Count++;
	//			return;
	//		}
	//		else
	//		{
	//			T[] temp_array = new T[this._node_size];
	//			for (int i = 0; i < index; i++)
	//				temp_array[i] = node.Items[i];
	//			temp_array[index] = addition;
	//			for (int i = this._node_size; i > index; i--)
	//				temp_array[i] = node.Items[i - 1];

	//			int half_node_size = this._node_size >> 1;
	//			if (node.Parent != null)
	//			{
	//				bool bIsUnBalanced = false;
	//				int iIsThereVacantSlot = 0;
	//				if (IsThereVacantSlotInLeft(node, ref bIsUnBalanced))
	//					iIsThereVacantSlot = 1;
	//				else if (IsThereVacantSlotInRight(node, ref bIsUnBalanced))
	//					iIsThereVacantSlot = 2;
	//				if (iIsThereVacantSlot > 0)
	//				{
	//					// copy temp buffer contents to the actual slots.
	//					byte b = (byte)(iIsThereVacantSlot == 1 ? 0 : 1);
	//					CopyArrayElements(node.TempSlots, b, Slots, 0, node.SlotLength);
	//					if (iIsThereVacantSlot == 1)
	//						// Vacant in left, "skud over" the leftmost node's item to parent and left.
	//						DistributeToLeft(node, node.TempSlots[node.SlotLength]);
	//					else if (iIsThereVacantSlot == 2)
	//						// Vacant in right, move the rightmost node item into the vacant slot in right.
	//						DistributeToRight(node, node.TempSlots[0]);
	//					return;
	//				}
	//				else if (bIsUnBalanced)
	//				{	// if this branch is unbalanced..
	//					// _BreakNode
	//					// Description :
	//					// -copy the left half of the slots
	//					// -copy the right half of the slots
	//					// -zero out the current slot.
	//					// -copy the middle slot
	//					// -allocate memory for children node *s
	//					// -assign the new children nodes.
	//					TreeNode LeftNode;
	//					TreeNode RightNode;
	//					try
	//					{
	//						// Initialize should throw an exception if in error.
	//						RightNode = node.GetRecycleNode(this);
	//						LeftNode = node.GetRecycleNode(this);
	//						CopyArrayElements(node.TempSlots, 0, LeftNode.Slots, 0, half_node_size);
	//						LeftNode.count = half_node_size;
	//						CopyArrayElements(node.TempSlots, (ushort)(half_node_size + 1), RightNode.Slots, 0, half_node_size);
	//						RightNode.count = half_node_size;
	//						ResetArray(Slots, null);
	//						count = 1;
	//						Slots[0] = node.TempSlots[half_node_size];
	//						Children = new TreeNode[node.half_node_size + 1];

	//						ResetArray(Children, null);
	//						Children[(int)Sop.Collections.BTree.ChildNodes.LeftChild] = LeftNode;
	//						Children[(int)Sop.Collections.BTree.ChildNodes.RightChild] = RightNode;
	//						//SUCCESSFUL!
	//						return;
	//					}
	//					catch (Exception)
	//					{
	//						Children = null;
	//						LeftNode = null;
	//						RightNode = null;
	//						throw;
	//					}
	//				}
	//				else
	//				{
	//					// All slots are occupied in this and other siblings' nodes..
	//					TreeNode RightNode;
	//					try
	//					{
	//						// prepare this and the right node sibling and promote the temporary parent node(pTempSlot).
	//						RightNode = node.GetRecycleNode(Parent);
	//						// zero out the current slot.
	//						ResetArray(Slots, null);
	//						// copy the left half of the slots to left sibling
	//						CopyArrayElements(node.TempSlots, 0, Slots, 0, SlotsHalf);
	//						count = SlotsHalf;
	//						// copy the right half of the slots to right sibling
	//						CopyArrayElements(node.TempSlots, (ushort)(SlotsHalf + 1), RightNode.Slots, 0, SlotsHalf);
	//						RightNode.count = SlotsHalf;

	//						// copy the middle slot to temp parent slot.
	//						node.TempParent = node.TempSlots[SlotsHalf];

	//						// assign the new children nodes.
	//						node.TempParentChildren[(int)Sop.Collections.BTree.ChildNodes.LeftChild] = this;
	//						node.TempParentChildren[(int)Sop.Collections.BTree.ChildNodes.RightChild] = RightNode;

	//						node.PromoteParent = (TreeNode)Parent;
	//						node.PromoteIndexOfNode = GetIndexOfNode(node);
	//						return;
	//					}
	//					catch
	//					{
	//						RightNode = null;
	//						throw;
	//					}
	//				}
	//			}
	//			else
	//			{
	//				// _BreakNode
	//				// Description :
	//				// -copy the left half of the temp slots
	//				// -copy the right half of the temp slots
	//				// -zero out the current slot.
	//				// -copy the middle of temp slot to 1st elem of current slot
	//				// -allocate memory for children node *s
	//				// -assign the new children nodes.
	//				Node LeftNode;
	//				Node RightNode;
	//				try
	//				{
	//					RightNode = node.GetRecycleNode(this);
	//					LeftNode = node.GetRecycleNode(this);
	//					CopyArrayElements(node.TempSlots, 0, LeftNode.Slots, 0, SlotsHalf);
	//					LeftNode.count = SlotsHalf;
	//					CopyArrayElements(node.TempSlots, (ushort)(SlotsHalf + 1), RightNode.Slots, 0, SlotsHalf);
	//					RightNode.count = SlotsHalf;
	//					ResetArray(Slots, null);
	//					Slots[0] = node.TempSlots[SlotsHalf];
	//					count = 1;
	//					Children = new TreeNode[node.SlotLength + 1];
	//					Children[(int)Sop.Collections.BTree.ChildNodes.LeftChild] = LeftNode;
	//					Children[(int)Sop.Collections.BTree.ChildNodes.RightChild] = RightNode;
	//					return;	// successful
	//				}
	//				catch (Exception)
	//				{
	//					// falling through here and further down means mem alloc error so delete the allocated ones.
	//					LeftNode = null;
	//					RightNode = null;
	//					RightNode = null;
	//					throw;
	//				}
	//			}
	//		}
	//	}

	//	private bool IsThereVacantSlotInLeft(Node node, ref bool IsUnBalanced)
	//	{
	//		IsUnBalanced = false;
	//		while ((node = node.GetLeftSibling(ParentBTree)) != null)
	//		{
	//			if (node.Children != null)
	//			{
	//				IsUnBalanced = true;
	//				return false;
	//			}
	//			else if (!node.IsFull(ParentBTree.SlotLength))
	//				return true;
	//		}
	//		return false;
	//	}

	//	/// <summary>Removes an item from this structure.</summary>
	//	/// <param name="removal">The item to be removed.</param>
	//	public void Remove(T removal)
	//	{
	//		throw new System.NotImplementedException();
	//	}

	//	/// <summary>Determines if the structure contains an item based on a given key.</summary>
	//	/// <typeparam name="Key">The type of the key.</typeparam>
	//	/// <param name="key">The key.</param>
	//	/// <param name="comparison">The comparison technique (must synchronize with the sorting of this data structure).</param>
	//	/// <returns>True if contained, False if not.</returns>
	//	public bool Contains<Key>(Key key, Compare<T, Key> comparison)
	//	{
	//		throw new System.NotImplementedException();
	//	}

	//	/// <summary>Gets an item based on a given key.</summary>
	//	/// <typeparam name="Key">The type of the key.</typeparam>
	//	/// <param name="key">The key.</param>
	//	/// <param name="compare">The comparison technique (must synchronize with the sorting of this data structure).</param>
	//	/// <returns>The found item.</returns>
	//	public T Get<Key>(Key key, Compare<T, Key> compare)
	//	{
	//		return Get<Key>(key, compare, this._root);
	//	}
	//	private T Get<K>(K key, Compare<T, K> compare, Node node)
	//	{
	//		if (node == null)
	//			throw new System.Exception("getting a non-existing value from BTree");
	//		for (int i = 0; i < node.Items.Length; i++)
	//		{
	//			switch (compare(node.Items[i], key))
	//			{
	//				case Comparison.Equal:
	//					return node.Items[i];
	//				case Comparison.Greater:
	//					return Get<K>(key, compare, node.Children[i]);
	//				case Comparison.Less:
	//					break;
	//				default:
	//					throw new System.NotImplementedException();
	//			}
	//		}
	//		return Get<K>(key, compare, node.Children[node.Items.Length]);
	//	}

	//	/// <summary>Removes an item based on a given key.</summary>
	//	/// <typeparam name="Key">The type of the key.</typeparam>
	//	/// <param name="removal">The key.</param>
	//	/// <param name="compare">The comparison technique (must synchronize with the sorting of this data structure).</param>
	//	public void Remove<Key>(Key removal, Compare<T, Key> compare)
	//	{
	//		throw new System.NotImplementedException();
	//	}

	//	/// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
	//	/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
	//	public void StepperReverse(Step<T> step_delegate)
	//	{
	//		this.StepperReverse(step_delegate, this._root);
	//	}
	//	private void StepperReverse(Step<T> step_delegate, Node node)
	//	{
	//		if (node == null)
	//			return;
	//		Stepper(step_delegate, node.Children[node.Count]);
	//		for (int i = node.Count - 1; i > -1; i--)
	//		{
	//			step_delegate(node.Items[i]);
	//			Stepper(step_delegate, node.Children[i]);
	//		}
	//	}

	//	/// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
	//	/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
	//	public void StepperReverse(StepRef<T> step_delegate)
	//	{
	//		this.StepperReverse(step_delegate, this._root);
	//	}
	//	private void StepperReverse(StepRef<T> step_delegate, Node node)
	//	{
	//		if (node == null)
	//			return;
	//		Stepper(step_delegate, node.Children[node.Count]);
	//		for (int i = node.Count - 1; i > -1; i--)
	//		{
	//			step_delegate(ref node.Items[i]);
	//			Stepper(step_delegate, node.Children[i]);
	//		}
	//	}

	//	/// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
	//	/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
	//	/// <returns>The resulting status of the iteration.</returns>
	//	public StepStatus StepperReverse(StepBreak<T> step_delegate)
	//	{
	//		return this.StepperReverse(step_delegate, this._root);
	//	}
	//	private StepStatus StepperReverse(StepBreak<T> step_delegate, Node node)
	//	{
	//		if (node == null)
	//			return StepStatus.Continue;
	//		switch (Stepper(step_delegate, node.Children[node.Count]))
	//		{
	//			case StepStatus.Break:
	//				return StepStatus.Break;
	//			case StepStatus.Continue:
	//				break;
	//			default:
	//				throw new System.NotImplementedException();
	//		}
	//		for (int i = node.Count - 1; i > -1; i--)
	//		{
	//			switch (step_delegate(node.Items[i]))
	//			{
	//				case StepStatus.Break:
	//					return StepStatus.Break;
	//				case StepStatus.Continue:
	//					break;
	//				default:
	//					throw new System.NotImplementedException();
	//			}
	//			switch (Stepper(step_delegate, node.Children[i]))
	//			{
	//				case StepStatus.Break:
	//					return StepStatus.Break;
	//				case StepStatus.Continue:
	//					break;
	//				default:
	//					throw new System.NotImplementedException();
	//			}
	//		}
	//		return StepStatus.Continue;
	//	}

	//	/// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
	//	/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
	//	/// <returns>The resulting status of the iteration.</returns>
	//	public StepStatus StepperReverse(StepRefBreak<T> step_delegate)
	//	{
	//		return this.StepperReverse(step_delegate, this._root);
	//	}
	//	private StepStatus StepperReverse(StepRefBreak<T> step_delegate, Node node)
	//	{
	//		if (node == null)
	//			return StepStatus.Continue;
	//		switch (Stepper(step_delegate, node.Children[node.Count]))
	//		{
	//			case StepStatus.Break:
	//				return StepStatus.Break;
	//			case StepStatus.Continue:
	//				break;
	//			default:
	//				throw new System.NotImplementedException();
	//		}
	//		for (int i = node.Count - 1; i > -1; i--)
	//		{
	//			switch (step_delegate(ref node.Items[i]))
	//			{
	//				case StepStatus.Break:
	//					return StepStatus.Break;
	//				case StepStatus.Continue:
	//					break;
	//				default:
	//					throw new System.NotImplementedException();
	//			}
	//			switch (Stepper(step_delegate, node.Children[i]))
	//			{
	//				case StepStatus.Break:
	//					return StepStatus.Break;
	//				case StepStatus.Continue:
	//					break;
	//				default:
	//					throw new System.NotImplementedException();
	//			}
	//		}
	//		return StepStatus.Continue;
	//	}

	//	/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
	//	/// <param name="step_function">The step function.</param>
	//	/// <param name="minimum">The minimum step value.</param>
	//	/// <param name="maximum">The maximum step value.</param>
	//	public void Stepper(Step<T> step_function, T minimum, T maximum)
	//	{
	//		throw new System.NotImplementedException();
	//	}

	//	/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
	//	/// <param name="step_function">The step function.</param>
	//	/// <param name="minimum">The minimum step value.</param>
	//	/// <param name="maximum">The maximum step value.</param>
	//	public void Stepper(StepRef<T> step_function, T minimum, T maximum)
	//	{
	//		throw new System.NotImplementedException();
	//	}

	//	/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
	//	/// <param name="step_function">The step function.</param>
	//	/// <param name="minimum">The minimum step value.</param>
	//	/// <param name="maximum">The maximum step value.</param>
	//	/// <returns>The result status of the stepper function.</returns>
	//	public StepStatus Stepper(StepBreak<T> step_function, T minimum, T maximum)
	//	{
	//		throw new System.NotImplementedException();
	//	}

	//	/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
	//	/// <param name="step_function">The step function.</param>
	//	/// <param name="minimum">The minimum step value.</param>
	//	/// <param name="maximum">The maximum step value.</param>
	//	/// <returns>The result status of the stepper function.</returns>
	//	public StepStatus Stepper(StepRefBreak<T> step_function, T minimum, T maximum)
	//	{
	//		throw new System.NotImplementedException();
	//	}

	//	/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
	//	/// <param name="step_function">The step function.</param>
	//	/// <param name="minimum">The minimum step value.</param>
	//	/// <param name="maximum">The maximum step value.</param>
	//	public void StepperReverse(Step<T> step_function, T minimum, T maximum)
	//	{
	//		throw new System.NotImplementedException();
	//	}

	//	/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
	//	/// <param name="step_function">The step function.</param>
	//	/// <param name="minimum">The minimum step value.</param>
	//	/// <param name="maximum">The maximum step value.</param>
	//	public void StepperReverse(StepRef<T> step_function, T minimum, T maximum)
	//	{
	//		throw new System.NotImplementedException();
	//	}

	//	/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
	//	/// <param name="step_function">The step function.</param>
	//	/// <param name="minimum">The minimum step value.</param>
	//	/// <param name="maximum">The maximum step value.</param>
	//	/// <returns>The result status of the stepper function.</returns>
	//	public StepStatus StepperReverse(StepBreak<T> step_function, T minimum, T maximum)
	//	{
	//		throw new System.NotImplementedException();
	//	}

	//	/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
	//	/// <param name="step_function">The step function.</param>
	//	/// <param name="minimum">The minimum step value.</param>
	//	/// <param name="maximum">The maximum step value.</param>
	//	/// <returns>The result status of the stepper function.</returns>
	//	public StepStatus StepperReverse(StepRefBreak<T> step_function, T minimum, T maximum)
	//	{
	//		throw new System.NotImplementedException();
	//	}

	//	/// <summary>Returns the structure to an empty state.</summary>
	//	public void Clear()
	//	{
	//		this._root = new Node(null, this._node_size);
	//		this._count = 0;
	//	}

	//	#endregion

	//	#region Structure<T>

	//	/// <summary>Invokes a delegate for each entry in the data structure.</summary>
	//	/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
	//	public void Stepper(Step<T> step_delegate)
	//	{
	//		Stepper(step_delegate, this._root);
	//	}
	//	private void Stepper(Step<T> step_delegate, Node node)
	//	{
	//		if (node == null)
	//			return;
	//		for (int i = 0; i < this._node_size && i < node.Count; i++)
	//		{
	//			Stepper(step_delegate, node.Children[i]);
	//			step_delegate(node.Items[i]);
	//		}
	//		Stepper(step_delegate, node.Children[node.Count]);
	//	}

	//	/// <summary>Invokes a delegate for each entry in the data structure.</summary>
	//	/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
	//	public void Stepper(StepRef<T> step_delegate)
	//	{
	//		Stepper(step_delegate, this._root);
	//	}
	//	private void Stepper(StepRef<T> step_delegate, Node node)
	//	{
	//		if (node == null)
	//			return;
	//		for (int i = 0; i < this._node_size && i < node.Count; i++)
	//		{
	//			Stepper(step_delegate, node.Children[i]);
	//			step_delegate(ref node.Items[i]);
	//		}
	//		Stepper(step_delegate, node.Children[node.Count]);
	//	}

	//	/// <summary>Invokes a delegate for each entry in the data structure.</summary>
	//	/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
	//	/// <returns>The resulting status of the iteration.</returns>
	//	public StepStatus Stepper(StepBreak<T> step_delegate)
	//	{
	//		return Stepper(step_delegate, this._root);
	//	}
	//	private StepStatus Stepper(StepBreak<T> step_delegate, Node node)
	//	{
	//		if (node == null)
	//			return StepStatus.Continue;
	//		for (int i = 0; i < this._node_size && i < node.Count; i++)
	//		{
	//			switch (Stepper(step_delegate, node.Children[i]))
	//			{
	//				case StepStatus.Break:
	//					return StepStatus.Break;
	//				case StepStatus.Continue:
	//					break;
	//				default:
	//					throw new System.NotImplementedException();
	//			}
	//			switch (step_delegate(node.Items[i]))
	//			{
	//				case StepStatus.Continue:
	//					continue;
	//				case StepStatus.Break:
	//					return StepStatus.Break;
	//				default:
	//					throw new System.NotImplementedException();
	//			}
	//		}
	//		return Stepper(step_delegate, node.Children[node.Count]);
	//	}

	//	/// <summary>Invokes a delegate for each entry in the data structure.</summary>
	//	/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
	//	/// <returns>The resulting status of the iteration.</returns>
	//	public StepStatus Stepper(StepRefBreak<T> step_delegate)
	//	{
	//		return Stepper(step_delegate, this._root);
	//	}
	//	private StepStatus Stepper(StepRefBreak<T> step_delegate, Node node)
	//	{
	//		if (node == null)
	//			return StepStatus.Continue;
	//		for (int i = 0; i < this._node_size && i < node.Count; i++)
	//		{
	//			switch (Stepper(step_delegate, node.Children[i]))
	//			{
	//				case StepStatus.Break:
	//					return StepStatus.Break;
	//				case StepStatus.Continue:
	//					break;
	//				default:
	//					throw new System.NotImplementedException();
	//			}
	//			switch (step_delegate(ref node.Items[i]))
	//			{
	//				case StepStatus.Continue:
	//					continue;
	//				case StepStatus.Break:
	//					return StepStatus.Break;
	//				default:
	//					throw new System.NotImplementedException();
	//			}
	//		}
	//		return Stepper(step_delegate, node.Children[node.Count]);
	//	}

	//	#endregion

	//	#region IEnumerable<T>

	//	/// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
	//	System.Collections.IEnumerator
	//		System.Collections.IEnumerable.GetEnumerator()
	//	{
	//		Stack<Link<Node, int, bool>> forks = new Stack_Linked<Link<Node, int, bool>>();
	//		forks.Push(new Link<Node, int, bool>(this._root, 0, false));
	//		while (forks.Count > 0)
	//		{
	//			Link<Node, int, bool> link = forks.Pop();
	//			if (link.One == null)
	//				continue;
	//			else if (!link.Three)
	//			{
	//				link.Three = true;
	//				forks.Push(link);
	//				Node child = link.One.Children[link.Two];
	//				forks.Push(new Link<Node, int, bool>(child, 0, false));
	//			}
	//			else if (link.Two < link.One.Count)
	//			{
	//				yield return link.One.Items[link.Two];
	//				link.Two++;
	//				link.Three = false;
	//				forks.Push(link);
	//			}
	//		}
	//	}

	//	/// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
	//	System.Collections.Generic.IEnumerator<T>
	//		System.Collections.Generic.IEnumerable<T>.GetEnumerator()
	//	{
	//		Stack<Link<Node, int, bool>> forks = new Stack_Linked<Link<Node, int, bool>>();
	//		forks.Push(new Link<Node, int, bool>(this._root, 0, false));
	//		while (forks.Count > 0)
	//		{
	//			Link<Node, int, bool> link = forks.Pop();
	//			if (link.One == null)
	//				continue;
	//			else if (!link.Three)
	//			{
	//				link.Three = true;
	//				forks.Push(link);
	//				Node child = link.One.Children[link.Two];
	//				forks.Push(new Link<Node, int, bool>(child, 0, false));
	//			}
	//			else if (link.Two < link.One.Count)
	//			{
	//				yield return link.One.Items[link.Two];
	//				link.Two++;
	//				link.Three = false;
	//				forks.Push(link);
	//			}
	//		}
	//	}

	//	#endregion
	//}

	#endregion
}
