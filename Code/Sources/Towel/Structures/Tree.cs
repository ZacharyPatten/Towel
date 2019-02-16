namespace Towel.Structures
{
	/// <summary>A generic tree data structure.</summary>
	/// <typeparam name="T">The generic type stored in this data structure.</typeparam>
	public interface Tree<T> : Structure<T>,
		// Structure Properties
		Structure.Countable<T>,
		Structure.Removable<T>
	{
		#region T Head
		/// <summary>The head of the tree.</summary>
		T Head { get; }
		#endregion
		#region bool IsChildOf(T node, T parent);
		/// <summary>Determines if a node is the child of another node.</summary>
		/// <param name="node">The child to check the parent of.</param>
		/// <param name="parent">The parent to check the child of.</param>
		/// <returns>True if the node is a child of the parent; False if not.</returns>
		bool IsChildOf(T node, T parent);
		#endregion
		#region void Children(T parent, Step<T> step_function);
		/// <summary>Stepper function for the children of a given node.</summary>
		/// <param name="parent">The node to step through the children of.</param>
		/// <param name="step_function">The step function.</param>
		void Children(T parent, Step<T> step_function);
		#endregion
		#region void Add(T addition, T parent);
		/// <summary>Adds a node to the tree.</summary>
		/// <param name="addition">The node to be added.</param>
		/// <param name="parent">The parent of the node to be added.</param>
		void Add(T addition, T parent);
		#endregion
	}

	/// <summary>A generic tree data structure using a dictionary to store node data.</summary>
	/// <typeparam name="T">The generic type stored in this data structure.</typeparam>
	[System.Serializable]
	public class TreeMap<T> : Tree<T>,
		// Structure Properties
		Structure.Hashing<T>,
		Structure.Equating<T>
	{
		// fields
		private Equate<T> _equate;
		private Hash<T> _hash;
		private T _head;
		private MapHashLinked<NodeData, T> _tree;
		// nested types
		#region private class NodeData
		[System.Serializable]
		private class NodeData
		{
			private T _parent;
			private SetHashList<T> _children;

			public T Parent { get { return this._parent; } set { this._parent = value; } }
			public SetHashList<T> Children { get { return this._children; } set { this._children = value; } }

			public NodeData(T parent, SetHashList<T> children)
			{
				this._parent = parent;
				this._children = children;
			}
		}

		#endregion
		// constructors
		#region public TreeMap(T head)
		public TreeMap(T head) : this(head, Towel.Equate.Default, Towel.Hash.Default) { }
		#endregion
		#region public TreeMap(T head, Equate<T> equate, Hash<T> hash)
		public TreeMap(T head, Equate<T> equate, Hash<T> hash)
		{
			this._equate = equate;
			this._hash = hash;
			this._head = head;
			this._tree = new MapHashLinked<NodeData, T>(this._equate, this._hash);
			this._tree.Add(this._head, new NodeData(default(T), new SetHashList<T>(this._equate, this._hash)));
		}
		#endregion
		// properties
		#region public T Head
		/// <summary>The head of the tree.</summary>
		public T Head { get { return this._head; } }
		#endregion
		#region public Hash<T> Hash
		/// <summary>The hash function being used (was passed into the constructor).</summary>
		public Hash<T> Hash { get { return this._hash; } }
		#endregion
		#region public Equate<T> Equate
		/// <summary>The equate function being used (was passed into the constructor).</summary>
		public Equate<T> Equate { get { return this._equate; } }
		#endregion
		#region public int Count
		/// <summary>The number of nodes in this tree.</summary>
		public int Count { get { return this._tree.Count; } }
		#endregion
		// methods
		#region public bool IsChildOf(T node, T parent)
		/// <summary>Determines if a node is the child of another node.</summary>
		/// <param name="node">The child to check the parent of.</param>
		/// <param name="parent">The parent to check the child of.</param>
		/// <returns>True if the node is a child of the parent; False if not.</returns>
		public bool IsChildOf(T node, T parent)
		{
			NodeData nodeData;
			if (this._tree.TryGet(parent, out nodeData))
				return nodeData.Children.Contains(node);
			else
				throw new System.InvalidOperationException("Attempting to get the children of a non-existing node");
		}
		#endregion
		#region public T Parent(T child)
		/// <summary>Gets the parent of a given node.</summary>
		/// <param name="child">The child to get the parent of.</param>
		/// <returns>The parent of the given child.</returns>
		public T Parent(T child)
		{
			NodeData nodeData;
			if (this._tree.TryGet(child, out nodeData))
				return nodeData.Parent;
			else
				throw new System.InvalidOperationException("Attempting to get the parent of a non-existing node");
		}
		#endregion
		#region public void Children(T parent, Step<T> step_function)
		/// <summary>Stepper function for the children of a given node.</summary>
		/// <param name="parent">The node to step through the children of.</param>
		/// <param name="step_function">The step function.</param>
		public void Children(T parent, Step<T> step_function)
		{
			NodeData nodeData;
			if (this._tree.TryGet(parent, out nodeData))
				nodeData.Children.Stepper(step_function);
			else
				throw new System.InvalidOperationException("Attempting to get the children of a non-existing node");
		}
		#endregion
		#region public void Add(T addition, T parent)
		/// <summary>Adds a node to the tree.</summary>
		/// <param name="addition">The node to be added.</param>
		/// <param name="parent">The parent of the node to be added.</param>
		public void Add(T addition, T parent)
		{
			NodeData nodeData;
			if (this._tree.TryGet(parent, out nodeData))
			{
				this._tree.Add(addition, new NodeData(parent, new SetHashList<T>(this._equate, this._hash)));
				nodeData.Children.Add(addition);
			}
			else
				throw new System.InvalidOperationException("Attempting to add a node to a non-existing parent");
		}
		#endregion
		#region public void Remove(T removal)
		/// <summary>Removes a node from the tree and all the child nodes.</summary>
		/// <param name="removal">The node to be removed.</param>
		public void Remove(T removal)
		{
			NodeData nodeData;
			if (this._tree.TryGet(removal, out nodeData))
			{
				this._tree[nodeData.Parent].Children.Remove(removal);
				RemoveRecursive(removal);
			}
			else
				throw new System.InvalidOperationException("Attempting to remove a non-existing node");
		}
		#endregion
		#region public void Stepper(Step<T> step_function)
		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_function">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Step<T> step_function)
		{
			this._tree.Keys(step_function);
		}
		#endregion
		#region public StepStatus Stepper(StepBreak<T> step_function)
		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_function">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepBreak<T> step_function)
		{
			return this._tree.Keys(step_function);
		}
		#endregion
		#region public TreeMap<T> Clone()
		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public TreeMap<T> Clone()
		{
			throw new System.NotImplementedException();
		}
		#endregion
		#region System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		System.Collections.IEnumerator
			System.Collections.IEnumerable.GetEnumerator()
		{
			throw new System.NotImplementedException();
		}
		#endregion
		#region System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
		System.Collections.Generic.IEnumerator<T>
			System.Collections.Generic.IEnumerable<T>.GetEnumerator()
		{
			throw new System.NotImplementedException();
		}
		#endregion
		// methods (private)
		#region private void RemoveRecursive(T current)
		private void RemoveRecursive(T current)
		{
			this._tree[current].Children.Stepper(
				(T child) =>
				{
					RemoveRecursive(child);
				});
			this._tree.Remove(current);
		}
		#endregion
	}
}
