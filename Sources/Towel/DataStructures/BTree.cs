using System;
using System.Collections;
using System.Collections.Generic;

namespace Towel.DataStructures
{
	/// <summary>A single node of BTree</summary>
	/// <typeparam name="T">Data type of tree</typeparam>
	public class BTreeNode<T> : ISteppable<T>
	{
		/// <summary>The array of items</summary>
		internal T[] Items;
		/// <summary> The array of references (pointers to child nodes)</summary>
		internal BTreeNode<T>?[] Children;
		/// <summary> The reference to the parent node.
		/// It is null only for the Root node</summary>
		internal BTreeNode<T>? Parent;
		/// <summary> The index of the child reference
		/// in the `Children` array of its parent</summary>
		internal byte ParentIndex;
		/// <summary> The items present in this node</summary>
		public byte Count { get; internal set; }
		/// <summary> The fixed maximum degree a node can have</summary>
		public int MaxDegree => Children.Length;
		/// <summary>
		/// Creates a Node for BTree of given maximum degree
		/// </summary>
		/// <param name="maxdegree">The maximum degree a node can have</param>
		public BTreeNode(uint maxdegree)
		{
			Items = new T[maxdegree - 1];
			Children = new BTreeNode<T>?[maxdegree];
		}
		/// <summary>Denotes whether the node is a leaf node or not</summary>
		public bool IsLeaf => Children[0] == null; //A node has degree either 0 (which impilies it is leaf) or maxsize
		/// <summary>Denotes whether the node is at full capacity or not</summary>
		public bool IsFull => Count + 1 == MaxDegree;
		/// <summary>
		/// Returns the leftmost node of the subtree rooted at this node
		/// </summary>
		/// <param name="someNode">The node whose leftmost node is needed</param>
		/// <returns>The leftmost node of the B-Tree</returns>
		public static BTreeNode<T> LeftmostNode(BTreeNode<T> someNode)
		{
			BTreeNode<T>? lc = someNode.Children[0];
			while (lc != null)
			{
				someNode = lc;
				lc = someNode.Children[0];
			}
			return someNode;
		}
		/// <summary>
		/// Given a item's index in a node, returns the node and index of the item which is next to the given item.
		/// </summary>
		/// <param name="node">The node in which the current item is contained</param>
		/// <param name="lastpos">The index of the item within the node</param>
		/// <returns>The node and index of the next item</returns>
		public static (BTreeNode<T>?, int) NextNode(BTreeNode<T> node, int lastpos)
		{
			lastpos++;
			BTreeNode<T>? p = node.Children[lastpos];
			if (lastpos >= node.Count && p == null)
			{
				lastpos = node.ParentIndex;
				p = node.Parent;
				while (p != null && lastpos == p.Count)
				{
					node = p;
					p = p.Parent;
					lastpos = node.ParentIndex;
				}
				return (p, lastpos);
			}
			else if (p != null)
			{
				return (LeftmostNode(p), 0);
			}
			else
			{
				return (node, lastpos);
			}
		}
		/// <inheritdoc/>
		public StepStatus StepperBreak<TStep>(TStep step = default) where TStep : struct, IFunc<T, StepStatus>
		{
			int pos = 0;
			BTreeNode<T> node;
			BTreeNode<T>? nextnode = BTreeNode<T>.LeftmostNode(this);
			do
			{
				node = nextnode;
				if (step.Invoke(node.Items[pos]) == StepStatus.Break) return StepStatus.Break;
				(nextnode, pos) = BTreeNode<T>.NextNode(node, pos);
			} while (nextnode != null);
			return StepStatus.Break;
		}
	}
	/// <summary>
	/// B-Tree Data structure
	/// /// </summary>
	/// <typeparam name="T">The type to store</typeparam>
	public class BTree<T> : IDataStructure<T>
	where T : IComparable<T>
	{
		/// <summary> The number of elements within this tree</summary>
		public int Count { get; private set; }
		internal BTreeNode<T> Top;
		/// <summary> The fixed size of a node within this tree</summary>
		public readonly byte MaxNodeDegree;
		/// <summary>
		/// Creates a B-Tree having nodes of given maximum size. Maximum size must be even
		/// </summary>
		/// <param name="maxdegree">The maximum degree/children a node can have. Must be even</param>
		public BTree(byte maxdegree)
		{
			if (maxdegree < 2)
				throw new ArgumentException("Maximum degree should be at least 2", nameof(maxdegree));
			else if (maxdegree % 2 != 0)
				throw new ArgumentException("Maximum degree should be an even number", nameof(maxdegree));
			Top = new(maxdegree);
			Count = 0;
			MaxNodeDegree = maxdegree;
		}
		/// <summary>
		/// Searches for an item in the tree and provides the
		/// node it is contained in and its index in the array (as `out` variables)
		/// if it exists. Otherwise provides the leaf node where the item shold be inserted
		/// </summary>
		/// <param name="item">The item to search</param>
		/// <param name="node">The node found</param>
		/// <param name="index">The index at which it is found</param>
		/// <returns>Returns true if item exists in the tree, otherwise false</returns>
		protected bool Search(T item, out BTreeNode<T> node, out int index)
		{
			index = -1;
			BTreeNode<T>? next = Top;
			do
			{
				node = next;
				next = node.Children[node.Count];
				for (int i = 0; i < node.Count; i++)
				{
					int c = node.Items[i].CompareTo(item);
					if (c == 0)
					{
						index = i;
						return true;
					}
					else if (c > 0)
					{
						next = node.Children[i];
						break;
					}
				}
			} while (next != null);
			return false;
		}
		/// <summary>
		/// Searches for an item in the tree
		/// </summary>
		/// <param name="item">The item to search</param>
		/// <returns>Returns true if item is present in tree, otherwise false</returns>
		public bool Search(T item)
		{
			return Search(item, out _, out _);
		}
		/// <summary>
		/// Finds the best index at which the insertion can be done and
		/// inserts the item at the given index, displacing the items as necessary <br/> <br/>
		/// Before using this method, ensure that the node is <br/>
		/// 1. A leaf node <br/>
		/// 2. Can support adding a value in it, i.e Count is at most MaxDegree-1 after insertion
		/// </summary>
		/// <param name="item">Item to add</param>
		/// <param name="node">The node in which the item is to be added</param>
		/// <returns>returns true if addition was successful. If a duplicate is found, addition fails and the method returns false</returns>
		protected bool Add(T item, BTreeNode<T> node)
		{
			int index = 0;
			if (node.Count > 0)
			{
				for (; index < node.Count; index++)
				{
					int c = node.Items[index].CompareTo(item);
					if (c == 0) return false;
					else if (c > 0) break;
				}
				for (int i = node.Count - 1; i >= index; i--)
				{
					node.Items[i + 1] = node.Items[i];
				}
			}
			node.Items[index] = item;
			node.Count++;
			Count++;
			return true;
		}
		/// <summary>
		/// Splits the full node into a node having two child nodes<br/><br/>
		/// Before calling this method, ensure that <br/>
		/// 1. the node is full <br/>
		/// 2. the parent node is Not full (Not applicable for the Root node)
		///  </summary>
		/// <param name="fullNode">The node to split</param>
		protected void Split(BTreeNode<T> fullNode)
		{
			BTreeNode<T> right = new(MaxNodeDegree);
			BTreeNode<T>? parent = fullNode.Parent, x;
			int l = MaxNodeDegree - 1, MedianIndex = l >> 1, i, j;
			for (i = MedianIndex + 1, j = 0; i < l; i++, j++)
			{
				right.Items[j] = fullNode.Items[i];
			}
			right.Count = (byte)j;
			if (!fullNode.IsLeaf)
			{
				for (i = MedianIndex + 1, j = 0; i <= l; i++, j++)
				{
					x = fullNode.Children[i];
					right.Children[j] = x;
					if (x != null)
					{
						x.Parent = right;
						x.ParentIndex = (byte)j;
					}
				}
			}
			fullNode.Count = (byte)MedianIndex;
			if (parent == null)
			{
				Top = new(MaxNodeDegree);
				Top.Items[0] = fullNode.Items[MedianIndex];
				Top.Count = 1;
				Top.Children[0] = fullNode;
				fullNode.ParentIndex = 0;
				Top.Children[1] = right;
				right.ParentIndex = 1;
				fullNode.Parent = right.Parent = Top;
			}
			else
			{
				for (i = parent.Count + 1, j = i--; i > fullNode.ParentIndex; j--, i--)
				{
					x = parent.Children[i];
					parent.Children[j] = x;
					if (x != null)
					{
						x.ParentIndex = (byte)j;
					}

				}
				for (i = parent.Count, j = i--; i >= fullNode.ParentIndex; j--, i--)
				{
					parent.Items[j] = parent.Items[i];
				}
				parent.Items[j] = fullNode.Items[MedianIndex];
				parent.Children[j] = fullNode;
				fullNode.ParentIndex = (byte)j;
				parent.Children[j + 1] = right;
				right.ParentIndex = (byte)(j + 1);
				right.Parent = parent;
				parent.Count++;
			}
		}
		/// <summary>
		/// Adds a unique element to the tree. <br/> Top down insertion
		/// </summary>
		/// <param name="item">The element to add</param>
		/// <returns>if element is already present in tree,
		/// additon fails and returns false. Otherwise returns true</returns>
		public bool Add(T item)
		{
			if (Top.IsFull)
				Split(Top);
			BTreeNode<T> node = Top;
			BTreeNode<T>? child;
			while (!node.IsLeaf)
			{
				int i = 0, c;
				while (i < node.Count && (c = node.Items[i].CompareTo(item)) <= 0)
				{
					if (c == 0) return false;
					i++;
				}
				child = node.Children[i];
				if (child != null && child.IsFull)
				{
					Split(child);
					c = node.Items[i].CompareTo(item);
					if (c == 0) return false;
					else if (c > 0) child = node.Children[i];
					else child = node.Children[i + 1];
				}
				node = child ?? throw new Exception("Expected a non-null internal node, but found a null node");
			}
			return Add(item, node);
		}
		/// <summary>
		/// Searches and removes the item in the tree if it exists. <br/> Top down deletion
		/// </summary>
		/// <param name="item">item to remove</param>
		/// <returns>true on successful deletion, otherwise false</returns>
		public bool Remove(T item)
		{
			if (Count == 0) return false;
			BTreeNode<T> node = Top;
			BTreeNode<T>? child;
			int MinCount = (MaxNodeDegree >> 1) - 1;
			while (!node.IsLeaf)
			{
				int i = 0, c;
				while (i < node.Count && (c = node.Items[i].CompareTo(item)) <= 0)
				{
					if (c == 0)
					{
						// TODO: Case 2 a/b/c
					}
					i++;
				}
				child = node.Children[i];
				if (child != null && child.Count < MinCount)
				{
					// TODO: Case 3 a/b
				}
				node = child ?? throw new Exception("Expected a non-null internal node, but found a null node");
			}
			{
				// TODO: Case 1 (Remove item from leaf node)
			}
			Count--;
			return true;
		}
		/// <summary>
		/// Struct for Stepper function outputting to array
		/// </summary>
		protected struct StepperToArray : IFunc<T, StepStatus>
		{
			/// <summary>The resultant array</summary>
			public T[] array;
			private int i;
			/// <summary>
			/// Returns the array of elements
			/// </summary>
			/// <param name="size">Size of the array</param>
			public StepperToArray(int size)
			{
				array = new T[size];
				i = 0;
			}
			/// <inheritdoc/>
			public StepStatus Invoke(T arg1)
			{
				array[i++] = arg1;
				return StepStatus.Continue;
			}
		}
		/// <inheritdoc/>
		public T[] ToArray()
		{
			if (Count == 0) return Array.Empty<T>();
			StepperToArray itr = new(Count);
			StepperBreak(itr);
			return itr.array;
		}

		/// <summary>
		/// Prepares an enumerator to enumerate every item in the tree
		/// </summary>
		/// <returns>Enumerator to enumerate the tree</returns>
		public IEnumerator<T> GetEnumerator()
		{
			if (Top != null)
			{
				int pos = 0;
				BTreeNode<T> node;
				BTreeNode<T>? nextnode = BTreeNode<T>.LeftmostNode(Top);
				do
				{
					node = nextnode;
					yield return node.Items[pos];
					(nextnode, pos) = BTreeNode<T>.NextNode(node, pos);
				} while (nextnode != null);
			}
			yield break;
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		/// <summary>
		/// Checks wheter the given node is the parent of the other or not
		/// </summary>
		/// <param name="node">The node to check if it is the child</param>
		/// <param name="parent">The node to check if it is the parent</param>
		/// <returns>true if the node is child of the other node, otherwise false</returns>
		public bool IsChildOf(BTreeNode<T> node, BTreeNode<T> parent) => node.Parent == parent;
		/// <summary>
		/// Returns the parent node of the given node. (Can be null for the root node)
		/// </summary>
		/// <param name="child">The node whose parent is to be found</param>
		/// <returns>The parent node of the current node, if it exists</returns>
		public BTreeNode<T>? Parent(BTreeNode<T> child) => child.Parent;
		public (bool Success, Exception? Exception) TryAdd(BTreeNode<T> addition, BTreeNode<T> parent)
		{
			throw new NotImplementedException();
		}
		public (bool Success, Exception? Exception) TryRemove(BTreeNode<T> value)
		{
			throw new NotImplementedException();
		}
		/// <inheritdoc/>
		public StepStatus StepperBreak<TStep>(TStep step = default) where TStep : struct, IFunc<T, StepStatus>
		{
			if (Top != null) return Top.StepperBreak(step);
			else return StepStatus.Break;
		}
	}
}