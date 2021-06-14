#if false

using System;
using static Towel.Syntax;

namespace Towel.DataStructures
{
	/// <summary>A self-sorting binary tree based on grouping nodes together at the same height.</summary>
	/// <typeparam name="T">The generic type of this data structure.</typeparam>
	public interface IBTree<T> : IDataStructure<T>,
		DataStructure.IClearable,
		//DataStructure.IAddable<T>,
		DataStructure.ICountable,
		//DataStructure.IRemovable<T>,
		DataStructure.IAuditable<T>
	{
#region Methods

		/// <summary>Determines if this structure contains a given item.</summary>
		/// <param name="compare">Comparison technique (must match the sorting technique of the structure).</param>
		/// <returns>True if contained, False if not.</returns>
		bool Contains(CompareToKnownValue<T> compare);
		/// <summary>Gets an item based on a given key.</summary>
		/// <param name="compare">Comparison technique (must match the sorting technique of the structure).</param>
		/// <returns>The found item.</returns>
		T Get(CompareToKnownValue<T> compare);
		/// <summary>Removes and item based on a given key.</summary>
		/// <param name="compare">Comparison technique (must match the sorting technique of the structure).</param>
		void Remove(CompareToKnownValue<T> compare);
		/// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
		/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
		void StepperReverse(Action<T> step_delegate);
		/// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
		/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		StepStatus StepperReverse(Func<T, StepStatus> step_delegate);
		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		void Stepper(Action<T> step, T minimum, T maximum);
		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The result status of the stepper function.</returns>
		StepStatus Stepper(Func<T, StepStatus> step, T minimum, T maximum);
		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		void StepperReverse(Action<T> step, T minimum, T maximum);
		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The result status of the stepper function.</returns>
		StepStatus StepperReverse(Func<T, StepStatus> step, T minimum, T maximum);

#endregion
	}

	/// <summary>Contains extensions methods for the BTree interface.</summary>
	public static class BTree
	{
#region Extensions

		///// <summary>Wrapper for the get function to handle exceptions.</summary>
		///// <typeparam name="T">The generic type of this data structure.</typeparam>
		///// <typeparam name="Key">The type of the key.</typeparam>
		///// <param name="structure">This structure.</param>
		///// <param name="get">The key to get.</param>
		///// <param name="comparison">The sorting technique (must synchronize with this structure's sorting).</param>
		///// <param name="item">The item if found.</param>
		///// <returns>True if successful, False if not.</returns>
		//public static bool TryGet<T, Key>(this IBTree<T> structure, Key get, Compare<T, Key> comparison, out T item)
		//{
		//    try
		//    {
		//        item = structure.Get<Key>(get, comparison);
		//        return true;
		//    }
		//    catch
		//    {
		//        item = default(T);
		//        return false;
		//    }
		//}

		///// <summary>Wrapper for the remove function to handle exceptions.</summary>
		///// <typeparam name="T">The generic type of this data structure.</typeparam>
		///// <typeparam name="Key">The type of the key.</typeparam>
		///// <param name="structure">This structure.</param>
		///// <param name="removal">The key.</param>
		///// <param name="comparison">The sorting technique (must synchronize with this structure's sorting).</param>
		///// <returns>True if successful, False if not.</returns>
		//public static bool TryRemove<T, Key>(this IBTree<T> structure, Key removal, Compare<T, Key> comparison)
		//{
		//    try
		//    {
		//        structure.Remove(removal, comparison);
		//        return true;
		//    }
		//    catch
		//    {
		//        return false;
		//    }
		//}

#endregion
	}

	/// <summary>A self-sorting binary tree based on grouping nodes together at the same height.</summary>
	/// <typeparam name="T">The generic type of this data structure.</typeparam>
	public class BTreeLinkedArray<T> : IBTree<T>
	{
		internal Node _root;
		internal int _count;
		internal int _height;
		internal int _node_size;
		internal Compare<T, T> _compare;

#region Nested Types

		/// <summary>A BTree node can contain multiple items and children.</summary>
		public class Node
		{
			internal T[] _items;
			internal Node[] _children;
			internal int _item_count;
			internal int _child_count;

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

		public BTreeLinkedArray(Func<T, T, CompareResult> compare, int node_size)
		{
			if (node_size < 2)
				throw new System.ArgumentOutOfRangeException("node_size");

			this._node_size = node_size;
			this._root = new Node(this._node_size);
			this._compare = new Compare<T, T>(compare);
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
			return Contains(x => _compare(x, item));
		}
#endregion
#region public bool Contains<K>(K key, Compare<T, K> compare)
		/// <summary>Determines if this structure contains a given item based on a given key.</summary>
		/// <param name="compare">The sorting technique (must synchronize with the sorting of this structure).</param>
		/// <returns>True if found, False if not.</returns>
		public bool Contains(CompareToKnownValue<T> compare)
		{
			return Contains(_root, compare);
		}
#endregion
#region public T Get<K>(K key, Compare<T, K> compare)
		/// <summary>Gets an item from this structure based on a given key.</summary>
		/// <param name="compare">The comparison techmique (must synchronize with this structure's sorting).</param>
		/// <returns>Item is found.</returns>
		public T Get(CompareToKnownValue<T> compare)
		{
			return Get(_root, compare);
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
			Remove(x => _compare(x, removal));
			_count--;
		}
#endregion
#region public void Remove<K>(K key, Compare<T, K> compare)
		/// <summary>Removes an item from the structure based on a key.</summary>
		/// <param name="compare">The compareison technique (must synchronize with this structure's sorting).</param>
		public void Remove(CompareToKnownValue<T> compare)
		{
			this.Remove(_root, compare);

			if (_root.ItemCount == 0 && !(_root.ChildCount == 0))
			{
				if (_root.ChildCount != 1)
					throw new System.InvalidOperationException("this._root.ChildCount != 1");
				_root = _root.Children[0];
				_height--;
			}

			_count--;
		}
#endregion
#region public void StepperReverse(Action<T> step_delegate)
		/// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
		/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
		public void StepperReverse(Action<T> step_delegate)
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
#region public StepStatus StepperReverse(Func<T, StepStatus> step_delegate)
		/// <summary>Invokes a delegate for each entry in the data structure (right to left).</summary>
		/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus StepperReverse(Func<T, StepStatus> step_delegate)
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
#region public void Stepper(Action<T> step, T minimum, T maximum)
		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		public void Stepper(Action<T> step, T minimum, T maximum)
		{
			throw new System.NotImplementedException();
		}
#endregion
#region public void Stepper(StepRef<T> step, T minimum, T maximum)
		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		public void Stepper(StepRef<T> step, T minimum, T maximum)
		{
			throw new System.NotImplementedException();
		}
#endregion
#region public StepStatus Stepper(Func<T, StepStatus> step, T minimum, T maximum)
		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The result status of the stepper function.</returns>
		public StepStatus Stepper(Func<T, StepStatus> step, T minimum, T maximum)
		{
			throw new System.NotImplementedException();
		}
#endregion
#region public StepStatus Stepper(StepRefBreak<T> step, T minimum, T maximum)
		/// <summary>Does an optimized step function (left to right) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The result status of the stepper function.</returns>
		public StepStatus Stepper(StepRefBreak<T> step, T minimum, T maximum)
		{
			throw new System.NotImplementedException();
		}
#endregion
#region public void StepperReverse(Action<T> step, T minimum, T maximum)
		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		public void StepperReverse(Action<T> step, T minimum, T maximum)
		{
			throw new System.NotImplementedException();
		}
#endregion
#region public void StepperReverse(StepRef<T> step, T minimum, T maximum)
		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		public void StepperReverse(StepRef<T> step, T minimum, T maximum)
		{
			throw new System.NotImplementedException();
		}
#endregion
#region public StepStatus StepperReverse(Func<T, StepStatus> step, T minimum, T maximum)
		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The result status of the stepper function.</returns>
		public StepStatus StepperReverse(Func<T, StepStatus> step, T minimum, T maximum)
		{
			throw new System.NotImplementedException();
		}
#endregion
#region public StepStatus StepperReverse(StepRefBreak<T> step, T minimum, T maximum)
		/// <summary>Does an optimized step function (right to left) for sorted binary search trees.</summary>
		/// <param name="step">The step function.</param>
		/// <param name="minimum">The minimum step value.</param>
		/// <param name="maximum">The maximum step value.</param>
		/// <returns>The result status of the stepper function.</returns>
		public StepStatus StepperReverse(StepRefBreak<T> step, T minimum, T maximum)
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
#region public void Stepper(Action<T> step_delegate)
		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Action<T> step_delegate)
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
#region public StepStatus Stepper(Func<T, StepStatus> step_delegate)
		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_delegate">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(Func<T, StepStatus> step_delegate)
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
			IStack<Link<Node, int, bool>> forks = new StackLinked<Link<Node, int, bool>>();
			forks.Push(new Link<Node, int, bool>(this._root, 0, false));
			while (forks.Count > 0)
			{
				Link<Node, int, bool> link = forks.Pop();
				if (link._1 is null)
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
#region internal bool Contains<K>(Node node, K key, Compare<T, K> compare)
		internal bool Contains(Node node, CompareToKnownValue<T> compare)
		{
			int loc = 0;
			for (int i = 0; i < node.ItemCount; i++)
				switch (compare(node.Items[i]))
				{
					case Less:
						loc++;
						continue;
					case Equal:
						goto break_for;
					case Greater:
						goto break_for;
					default:
						throw new System.NotImplementedException();
				}
			break_for:
			if (loc < node.ItemCount && compare(node.Items[loc]) is Equal)
				return true;
			if (node.ChildCount == 0)
				return false;
			return this.Contains(node.Children[loc], compare);
		}
#endregion
#region internal T Get<K>(Node node, K key, Compare<T, K> compare)
		internal T Get(Node node, CompareToKnownValue<T> compare)
		{
			int loc = 0;
			for (int i = 0; i < node.ItemCount; i++)
				switch (compare(node.Items[i]))
				{
					case Less:
						loc++;
						continue;
					case Equal:
						goto break_for;
					case Greater:
						goto break_for;
					default:
						throw new NotImplementedException();
				}
			break_for:
			if (loc < node.ItemCount && compare(node.Items[loc]) is Equal)
				return node.Items[loc];
			if (node.ChildCount == 0)
				throw new InvalidOperationException("getting a non-existing item");
			return this.Get(node.Children[loc], compare);
		}
#endregion
#region internal void Remove<K>(Node node, K key, Compare<T, K> compare)
		internal void Remove(Node node, CompareToKnownValue<T> compare)
		{
			int loc = 0;
			for (int i = 0; i < node.ItemCount; i++)
				switch (compare(node.Items[i]))
				{
					case Less:
						loc++;
						continue;
					case Equal:
						goto break_for;
					case Greater:
						goto break_for;
					default:
						throw new System.NotImplementedException();
				}
			break_for:
			if (loc < node.ItemCount && compare(node.Items[loc]) is Equal)
			{
				RemoveKeyFromNode(node, compare, loc);
				return;
			}
			if (!(node.ChildCount == 0))
			{
				RemoveKeyFromSubtree(node, compare, loc);
			}
		}
#endregion
#region internal void RemoveKeyFromSubtree<K>(Node parentNode, K key, Compare<T, K> compare, int subtreeIndexInNode)
		internal void RemoveKeyFromSubtree(Node parentNode, CompareToKnownValue<T> compare, int subtreeIndexInNode)
		{
			Node childNode = parentNode.Children[subtreeIndexInNode];

			if (childNode.ItemCount == _node_size - 1)
			{
				int leftIndex = subtreeIndexInNode - 1;
				Node leftSibling = subtreeIndexInNode > 0 ? parentNode.Children[leftIndex] : null;

				int rightIndex = subtreeIndexInNode + 1;
				Node rightSibling = subtreeIndexInNode < parentNode.ChildCount - 1 ? parentNode.Children[rightIndex] : null;

				if (leftSibling is not null && leftSibling.ItemCount > _node_size - 1)
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
				else if (rightSibling is not null && rightSibling.ItemCount > _node_size - 1)
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
					if (leftSibling is not null)
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

			this.Remove(childNode, compare);
		}
#endregion
#region internal void RemoveKeyFromNode<K>(Node node, K keyToDelete, Compare<T, K> compare, int keyIndexInNode)
		internal void RemoveKeyFromNode(Node node, CompareToKnownValue<T> compare, int keyIndexInNode)
		{
			if (node.ChildCount == 0)
			{
				for (int i = keyIndexInNode; i < node.ItemCount; i++)
					node.Items[i] = node.Items[i + 1];
				node.ItemCount--;
				return;
			}

			Node predecessorChild = node.Children[keyIndexInNode];
			if (predecessorChild.ItemCount >= _node_size)
			{
				T predecessor = RemovePredecessor(predecessorChild);
				node.Items[keyIndexInNode] = predecessor;
			}
			else
			{
				Node successorChild = node.Children[keyIndexInNode + 1];
				if (successorChild.ItemCount >= _node_size)
				{
					T successor = RemoveSuccessor(predecessorChild);
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
					Remove(predecessorChild, compare);
				}
			}
		}
#endregion
#region internal T RemovePredecessor(Node node)
		internal T RemovePredecessor(Node node)
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
		internal T RemoveSuccessor(Node node)
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
#region internal void Add_SplitChild(Node parentNode, int nodeToBeSplitIndex, Node nodeToBeSplit)
		internal void Add_SplitChild(Node parentNode, int nodeToBeSplitIndex, Node nodeToBeSplit)
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
#region internal void Add_NonFull(Node node, T addition)
		internal void Add_NonFull(Node node, T addition)
		{
			int positionToInsert = 0;
			for (int i = 0; i < node.ItemCount; i++)
				switch (this._compare(addition, node.Items[i]))
				{
					case Less:
						goto break_for;
					case Equal:
						positionToInsert++;
						continue;
					case Greater:
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
				if (this._compare(addition, node.Items[positionToInsert]) is Greater)
				{
					positionToInsert++;
				}
			}
			//if (node.Children[positionToInsert] is null)
			//	node.Children[positionToInsert] = new Node(this._node_size);
			this.Add_NonFull(node.Children[positionToInsert], addition);
		}
#endregion
#region internal void StepperReverse(Action<T> step_delegate, Node node)
		internal void StepperReverse(Action<T> step_delegate, Node node)
		{
			if (node is null)
				return;
			Stepper(step_delegate, node.Children[node.ItemCount]);
			for (int i = node.ItemCount - 1; i > -1; i--)
			{
				step_delegate(node.Items[i]);
				Stepper(step_delegate, node.Children[i]);
			}
		}
#endregion
#region internal void StepperReverse(StepRef<T> step_delegate, Node node)
		internal void StepperReverse(StepRef<T> step_delegate, Node node)
		{
			if (node is null)
				return;
			Stepper(step_delegate, node.Children[node.ItemCount]);
			for (int i = node.ItemCount - 1; i > -1; i--)
			{
				step_delegate(ref node.Items[i]);
				Stepper(step_delegate, node.Children[i]);
			}
		}
#endregion
#region internal StepStatus StepperReverse(Func<T, StepStatus> step_delegate, Node node)
		internal StepStatus StepperReverse(Func<T, StepStatus> step_delegate, Node node)
		{
			if (node is null)
				return Continue;
			switch (Stepper(step_delegate, node.Children[node.ItemCount]))
			{
				case Break:
					return Break;
				case Continue:
					break;
				default:
					throw new System.NotImplementedException();
			}
			for (int i = node.ItemCount - 1; i > -1; i--)
			{
				switch (step_delegate(node.Items[i]))
				{
					case Break:
						return Break;
					case Continue:
						break;
					default:
						throw new System.NotImplementedException();
				}
				switch (Stepper(step_delegate, node.Children[i]))
				{
					case Break:
						return Break;
					case Continue:
						break;
					default:
						throw new System.NotImplementedException();
				}
			}
			return Continue;
		}
#endregion
#region internal StepStatus StepperReverse(StepRefBreak<T> step_delegate, Node node)
		internal StepStatus StepperReverse(StepRefBreak<T> step_delegate, Node node)
		{
			if (node is null)
				return Continue;
			switch (Stepper(step_delegate, node.Children[node.ItemCount]))
			{
				case Break:
					return Break;
				case Continue:
					break;
				default:
					throw new System.NotImplementedException();
			}
			for (int i = node.ItemCount - 1; i > -1; i--)
			{
				switch (step_delegate(ref node.Items[i]))
				{
					case Break:
						return Break;
					case Continue:
						break;
					default:
						throw new System.NotImplementedException();
				}
				switch (Stepper(step_delegate, node.Children[i]))
				{
					case Break:
						return Break;
					case Continue:
						break;
					default:
						throw new System.NotImplementedException();
				}
			}
			return Continue;
		}
#endregion
#region internal void Stepper(Action<T> step_delegate, Node node)
		internal void Stepper(Action<T> step_delegate, Node node)
		{
			if (node is null)
				return;
			for (int i = 0; i < this._node_size && i < node.ItemCount; i++)
			{
				Stepper(step_delegate, node.Children[i]);
				step_delegate(node.Items[i]);
			}
			Stepper(step_delegate, node.Children[node.ItemCount]);
		}
#endregion
#region internal void Stepper(StepRef<T> step_delegate, Node node)
		internal void Stepper(StepRef<T> step_delegate, Node node)
		{
			if (node is null)
				return;
			for (int i = 0; i < this._node_size && i < node.ItemCount; i++)
			{
				Stepper(step_delegate, node.Children[i]);
				step_delegate(ref node.Items[i]);
			}
			Stepper(step_delegate, node.Children[node.ItemCount]);
		}
#endregion
#region internal StepStatus Stepper(Func<T, StepStatus> step_delegate, Node node)
		internal StepStatus Stepper(Func<T, StepStatus> step_delegate, Node node)
		{
			if (node is null)
				return Continue;
			for (int i = 0; i < this._node_size && i < node.ItemCount; i++)
			{
				switch (Stepper(step_delegate, node.Children[i]))
				{
					case Break:
						return Break;
					case Continue:
						break;
					default:
						throw new System.NotImplementedException();
				}
				switch (step_delegate(node.Items[i]))
				{
					case Continue:
						continue;
					case Break:
						return Break;
					default:
						throw new System.NotImplementedException();
				}
			}
			return Stepper(step_delegate, node.Children[node.ItemCount]);
		}
#endregion
#region internal StepStatus Stepper(StepRefBreak<T> step_delegate, Node node)
		internal StepStatus Stepper(StepRefBreak<T> step_delegate, Node node)
		{
			if (node is null)
				return Continue;
			for (int i = 0; i < this._node_size && i < node.ItemCount; i++)
			{
				switch (Stepper(step_delegate, node.Children[i]))
				{
					case Break:
						return Break;
					case Continue:
						break;
					default:
						throw new System.NotImplementedException();
				}
				switch (step_delegate(ref node.Items[i]))
				{
					case Continue:
						continue;
					case Break:
						return Break;
					default:
						throw new System.NotImplementedException();
				}
			}
			return Stepper(step_delegate, node.Children[node.ItemCount]);
		}
#endregion

#endregion
	}
}

#endif
