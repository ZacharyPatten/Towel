namespace Towel.DataStructures;

/// <summary>Static helpers for <see cref="BTreeLinked{T, TCompare}"/>.</summary>
public static class BTreeLinked
{
	#region Extension Methods

	/// <inheritdoc cref="BTreeLinked{T, TCompare}.BTreeLinked(byte, TCompare)" />
	/// <inheritdoc cref="BTreeLinked{T, TCompare}" />
	/// <returns>The new constructed <see cref="BTreeLinked{T, TCompare}"/>.</returns>
	public static BTreeLinked<T, SFunc<T, T, CompareResult>> New<T>(
		byte maxDegree,
		Func<T, T, CompareResult>? compare = null) =>
		new(maxDegree, compare ?? Compare);

	#endregion
}

/// <summary>B-Tree Data structure.</summary>
/// <typeparam name="T">The type to store</typeparam>
/// <typeparam name="TCompare">The type that is comparing <typeparamref name="T"/> values.</typeparam>
public class BTreeLinked<T, TCompare> : IDataStructure<T>,
	//DataStructure.IAddable<T>,
	//DataStructure.IRemovable<T>,
	DataStructure.ICountable,
	DataStructure.IClearable,
	DataStructure.IAuditable<T>,
	DataStructure.IComparing<T, TCompare>
	where TCompare : struct, IFunc<T, T, CompareResult>
{
	internal Node _root;
	internal int _count;
	internal TCompare _compare;
	internal byte _maxDegree;

	#region Nested Types

	internal class Node
	{
		internal T[] _items;
		internal Node?[] _children;
		internal Node? _parent;
		internal byte _parentIndex;
		internal byte _count;

		public Node(byte maxdegree)
		{
			_items = new T[maxdegree - 1];
			_children = new Node?[maxdegree];
		}

		public bool IsLeaf => _children[0] is null;

		public bool IsFull => _count + 1 == _children.Length;

		public static Node LeftmostNode(Node node)
		{
			Node? leftChild = node._children[0];
			while (leftChild != null)
			{
				node = leftChild;
				leftChild = node._children[0];
			}
			return node;
		}

		/// <summary>Given a item's index in a node, returns the node and index of the item which is next to the given item.</summary>
		/// <param name="node">The node in which the current item is contained</param>
		/// <param name="lastPosition">The index of the item within the node</param>
		/// <returns>The node and index of the next item</returns>
		public static (Node?, int) NextNode(Node node, int lastPosition)
		{
			lastPosition++;
			Node? p = node._children[lastPosition];
			if (lastPosition >= node._count && p is null)
			{
				lastPosition = node._parentIndex;
				p = node._parent;
				while (p != null && lastPosition == p._count)
				{
					node = p;
					p = p._parent;
					lastPosition = node._parentIndex;
				}
				return (p, lastPosition);
			}
			else if (p != null)
			{
				return (LeftmostNode(p), 0);
			}
			else
			{
				return (node, lastPosition);
			}
		}

		public StepStatus StepperBreak<TStep>(TStep step = default) where TStep : struct, IFunc<T, StepStatus>
		{
			int position = 0;
			Node? nextnode = LeftmostNode(this);
			do
			{
				Node node = nextnode;
				if (step.Invoke(node._items[position]) is Break)
				{
					return Break;
				}
				(nextnode, position) = NextNode(node, position);
			} while (nextnode != null);
			return Break;
		}
	}

	#endregion

	#region Constructors

	/// <summary>
	/// Creates a B-Tree having nodes of given maximum size. Maximum size must be even
	/// </summary>
	/// <param name="maxdegree">The maximum degree/children a node can have. Must be even</param>
	/// <param name="compare">The function for comparing <typeparamref name="T"/> values.</param>
	public BTreeLinked(byte maxdegree, TCompare compare = default)
	{
		if (maxdegree < 4) throw new ArgumentException("Maximum degree should be at least 4", nameof(maxdegree));
		else if (maxdegree % 2 is not 0) throw new ArgumentException("Maximum degree should be an even number", nameof(maxdegree));
		_root = new(maxdegree);
		_count = 0;
		_maxDegree = maxdegree;
		_compare = compare;
	}

	#endregion

	#region Properties

	/// <summary> The number of elements within this tree</summary>
	public int Count => _count;

	/// <inheritdoc cref="DataStructure.IComparing{T, TCompare}.Compare" />
	public TCompare Compare => _compare;

	/// <summary> The fixed size of a node within this tree</summary>
	public byte MaxDegree => _maxDegree;

	#endregion

	#region Methods

	/// <summary>
	/// Removes all elements from the B-Tree
	/// </summary>
	public void Clear()
	{
		_count = 0;
		_root._count = 0;
		for (int i = 0; i < MaxDegree; i++)
		{
			_root._children[i] = null;
		}
	}

	/// <summary>
	/// Searches for an item in the tree and provides the
	/// node it is contained in and its index in the array (as 'out' variables)
	/// if it exists. Otherwise provides the leaf node where the item shold be inserted
	/// </summary>
	/// <param name="item">The item to search</param>
	/// <param name="node">The node found</param>
	/// <param name="index">The index at which it is found</param>
	/// <returns>Returns true if item exists in the tree, otherwise false</returns>
	internal bool Search(T item, out Node node, out int index)
	{
		index = -1;
		Node? next = _root;
		do
		{
			node = next;
			next = node._children[node._count];
			for (int i = 0; i < node._count; i++)
			{
				CompareResult c = Compare.Invoke(node._items[i], item);
				if (c is Equal)
				{
					index = i;
					return true;
				}
				else if (c is Greater)
				{
					next = node._children[i];
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
	public bool Contains(T item)
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
	internal bool TryAdd(T item, Node node)
	{
		int index = 0;
		if (node._count > 0)
		{
			for (; index < node._count; index++)
			{
				CompareResult c = Compare.Invoke(node._items[index], item);
				if (c is Equal) return false;
				else if (c is Greater) break;
			}
			for (int i = node._count - 1; i >= index; i--)
			{
				node._items[i + 1] = node._items[i];
			}
		}
		node._items[index] = item;
		node._count++;
		_count++;
		return true;
	}

	/// <summary>
	/// Splits the full node into a node having two child nodes<br/><br/>
	/// Before calling this method, ensure that <br/>
	/// 1. the node is full <br/>
	/// 2. the parent node is Not full (Not applicable for the Root node)
	///  </summary>
	/// <param name="fullNode">The node to split</param>
	internal void Split(Node fullNode)
	{
		Node right = new(MaxDegree);
		Node? parent = fullNode._parent, x;
		int l = MaxDegree - 1, MedianIndex = l >> 1;
		int i;
		int j;
		for (i = MedianIndex + 1, j = 0; i < l; i++, j++)
		{
			right._items[j] = fullNode._items[i];
			fullNode._items[i] = default!;
		}
		right._count = (byte)j;
		if (!fullNode.IsLeaf)
		{
			for (i = MedianIndex + 1, j = 0; i <= l; i++, j++)
			{
				right._children[j] = x = fullNode._children[i];
				if (x != null)
				{
					x._parent = right;
					x._parentIndex = (byte)j;
				}
				fullNode._children[i] = null;
			}
		}
		fullNode._count = (byte)MedianIndex;
		if (parent is null) // 'fullnode' is Root
		{
			_root = new(MaxDegree);
			_root._items[0] = fullNode._items[MedianIndex];
			fullNode._items[MedianIndex] = default!;
			_root._count = 1;
			_root._children[0] = fullNode;
			fullNode._parentIndex = 0;
			_root._children[1] = right;
			right._parentIndex = 1;
			fullNode._parent = right._parent = _root;
		}
		else
		{
			for (i = parent._count + 1, j = i--; i > fullNode._parentIndex; j--, i--)
			{
				parent._children[j] = x = parent._children[i];
				if (x != null) x._parentIndex = (byte)j;
			}
			for (i = parent._count, j = i--; i >= fullNode._parentIndex; j--, i--)
			{
				parent._items[j] = parent._items[i];
			}
			parent._items[j] = fullNode._items[MedianIndex];
			fullNode._items[MedianIndex] = default!;
			parent._children[j] = fullNode;
			fullNode._parentIndex = (byte)j;
			parent._children[j + 1] = right;
			right._parentIndex = (byte)(j + 1);
			right._parent = parent;
			parent._count++;
		}
	}

	/// <summary>
	/// Adds a unique element to the tree. <br/> Top down insertion
	/// </summary>
	/// <param name="item">The element to add</param>
	/// <returns>if element is already present in tree,
	/// additon fails and returns false. Otherwise returns true</returns>
	public bool TryAdd(T item)
	{
		if (_root.IsFull)
		{
			Split(_root);
		}
		Node node = _root;
		Node? child;
		while (!node.IsLeaf)
		{
			int i = 0;
			CompareResult c;
			while (i < node._count && (c = Compare.Invoke(node._items[i], item)) is not Greater)
			{
				if (c is Equal)
				{
					return false;
				}
				i++;
			}
			child = node._children[i];
			if (child != null && child.IsFull)
			{
				Split(child);
				c = Compare.Invoke(node._items[i], item);
				switch (c)
				{
					case Greater: child = node._children[i];     break;
					case Less:    child = node._children[i + 1]; break;
					case Equal: return false;
				}
			}
			node = child ?? throw new Exception("Expected a non-null internal node, but found a null node");
		}
		return TryAdd(item, node);
	}
	/// <summary>
	/// Searches and removes the item in the tree if it exists. <br/> Top down deletion
	/// </summary>
	/// <param name="item">item to remove</param>
	/// <returns>true on successful deletion, otherwise false</returns>
	public bool Remove(T item)
	{
		if (_count is 0)
		{
			return false;
		}
		Node? node = _root;
		Node? child;
		int t = MaxDegree >> 1;
		// All nodes (except root) must contain at least this many items
		// [value of (t) in Cormen's "Introduction to Algorithms"]
		do
		{
			int i = 0;
			CompareResult c;
			while (i < node._count && (c = Compare.Invoke(node._items[i], item)) is not Greater)
			{
				if (c is Equal)
				{
					if (node.IsLeaf) // CASE 1
					{
						for (int j = i + 1; j < node._count; j++)
						{
							node._items[j - 1] = node._items[j];
						}
						node._items[--node._count] = default!;
						node = null;
					}
					else
					{
						Node? lc = node._children[i];
						Node? rc = node._children[i + 1];
						if (lc is null || rc is null) throw new Exception("Found null children of an internal node!");
						if (lc._count >= t) // CASE 2a
						{
							do
							{
								child = lc;
								lc = lc._children[lc._count];
							}
							while (lc != null);
							// At this point lc is null, child is the rightmost node of subtree preceeding 'item'
							item = node._items[i] = child._items[child._count - 1];
						}
						else if (rc._count >= t) // Case 2b
						{
							do
							{
								child = rc;
								rc = rc._children[0];
							} while (rc != null);
							// At this point rc is null, child is the leftmost node of subtree following 'item'
							item = node._items[i++] = child._items[0];
						}
						else // Case 2c
						{
							int p;
							int q;
							int r = lc._count;
							lc._items[lc._count++] = node._items[i];
							for (p = i, q = i + 1; q < node._count; p++, q++)
							{
								node._items[p] = node._items[q];
							}
							node._items[p] = default!;
							for (p = i + 1, q = p + 1; q <= node._count; p++, q++)
							{
								child = node._children[p] = node._children[q];
								if (child != null) child._parentIndex = (byte)p;
								else throw new Exception("Found null children of an internal node!");
							}
							node._children[p] = null;
							node._count--;
							for (p = lc._count, q = 0; q < rc._count; p++, q++, lc._count++)
							{
								lc._items[p] = rc._items[q];
								rc._items[q] = default!;
							}
							for (p = r + 1, q = 0; q <= rc._count; p++, q++)
							{
								child = lc._children[p] = rc._children[q];
								if (child != null)
								{
									child._parentIndex = (byte)p;
									child._parent = lc;
								}
								rc._children[q] = null;
							}
							rc._parent = null; // delete rc from memory ?
							if (node == _root && node._count is 0)
							{
								_root = lc;
								_root._parent = null;
								node._items = default!;
								node._children = default!; // delete node from memory ?
								node = lc;
								i = 0;
								continue;
							}
						}
					}
					break;
				}
				i++;
			}
			if (node is null)
			{
				break; // 'node' would be null after Case 1
			}
			else
			{
				child = node._children[i]; // 'child' will be null only if 'node' is a Leaf node
			}
			if (child is null)
			{
				return false; // Reached a leaf node, could not find item in the tree!
			}
			else if (child._count < t)
			{
				Node? lc = i > 0 ? node._children[i - 1] : null;
				Node? rc = i < node._count ? node._children[i + 1] : null;
				int j;
				if (lc != null && lc._count >= t) // Case 3a-Left
				{
					for (j = child._count; j > 0; j--)
					{
						child._items[j] = child._items[j - 1];
					}
					child._count++;
					for (j = child._count; j > 0; j--)
					{
						rc = child._children[j] = child._children[j - 1];
						if (rc != null) rc._parentIndex = (byte)j;
					}
					rc = lc._children[lc._count];
					if (rc != null)
					{
						child._children[0] = rc;
						rc._parent = child;
						rc._parentIndex = 0;
					}
					child._items[0] = node._items[i - 1];
					node._items[i - 1] = lc._items[--lc._count];
					lc._items[lc._count] = default!;
				}
				else if (rc != null && rc._count >= t) // Case 3a-Right
				{
					child._items[child._count++] = node._items[i];
					node._items[i] = rc._items[0];
					lc = rc._children[0];
					if (lc != null)
					{
						child._children[child._count] = lc;
						lc._parentIndex = (byte)child._count;
						lc._parent = child;
					}
					for (j = 1; j < rc._count; j++)
					{
						rc._items[j - 1] = rc._items[j];
					}
					for (j = 1; j <= rc._count; j++)
					{
						lc = rc._children[j - 1] = rc._children[j];
						if (lc != null) lc._parentIndex = (byte)(j - 1);
					}
					rc._children[rc._count--] = null;
					rc._items[rc._count] = default!;
				}
				else
				{
					int k;
					if (lc != null) // Case 3b-Left
					{
						lc._items[lc._count++] = node._items[i - 1];
						for (j = lc._count, k = 0; k <= child._count; j++, k++)
						{
							rc = lc._children[j] = child._children[k];
							if (rc != null)
							{
								rc._parentIndex = (byte)j;
								rc._parent = lc;
							}
						}
						for (k = 0; k < child._count; k++) lc._items[lc._count++] = child._items[k];
						child._items = default!;
						child._children = default!;
						child._parent = null;
						child = lc;
					}
					else if (rc != null) // Case 3b-Right || PS: can leave it at else {...} but this avoids Null reference warning
					{
						child._items[child._count++] = node._items[i];
						for (j = child._count, k = 0; k <= rc._count; j++, k++)
						{
							lc = child._children[j] = rc._children[k];
							if (lc != null)
							{
								lc._parentIndex = (byte)j;
								lc._parent = child;
							}
						}
						for (k = 0; k < rc._count; k++) child._items[child._count++] = rc._items[k];
						rc._items = default!;
						rc._children = default!;
						rc._parent = null;
					}
					else
					{
						throw new Exception("Found null children of an internal node!");
					}
					for (k = child._parentIndex + 1; k < node._count; k++)
					{
						node._items[k - 1] = node._items[k];
						lc = node._children[k] = node._children[k + 1];
						if (lc != null) lc._parentIndex = (byte)k;
					}
					node._children[node._count--] = null;
					node._items[node._count] = default!;
					if (node == _root && node._count is 0)
					{
						node._children = null!;
						_root = child;
						_root._parent = null;
						_root._parentIndex = 0;
					}
				}
			}
			node = child;
		}
		while (node != null);
		_count--;
		return true;
	}

	/// <inheritdoc/>
	public T[] ToArray()
	{
		if (_count is 0)
		{
			return Array.Empty<T>();
		}
		T[] array = new T[_count];
		FillArray<T> action = array;
		this.Stepper(action);
		return array;
	}

	/// <summary>Prepares an enumerator to enumerate every item in the tree.</summary>
	/// <returns>Enumerator to enumerate the tree</returns>
	public System.Collections.Generic.IEnumerator<T> GetEnumerator()
	{
		if (_count > 0)
		{
			int pos = 0;
			Node node;
			Node? nextnode = Node.LeftmostNode(_root);
			do
			{
				node = nextnode;
				yield return node._items[pos];
				(nextnode, pos) = Node.NextNode(node, pos);
			} while (nextnode != null);
		}
		yield break;
	}

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

	/// <inheritdoc/>
	public StepStatus StepperBreak<TStep>(TStep step = default) where TStep : struct, IFunc<T, StepStatus>
	{
		if (_count is 0)
		{
			return Continue;
		}
		return _root.StepperBreak(step);
	}

	#endregion
}