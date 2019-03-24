using System;

namespace Towel.DataStructures
{
	/// <summary>A self sorting binary tree using the red-black tree algorithms.</summary>
	/// <typeparam name="T">The generic type of the structure.</typeparam>
	public interface RedBlackTree<T> : DataStructure<T>,
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

		#endregion
	}

	/// <summary>Contains extension methods for the RedBlackTree interface.</summary>
	public static class RedBlackTree
	{
		#region Extensions

		/// <summary>Wrapper for the "Add" method to help with exceptions.</summary>
		/// <typeparam name="T">The generic type of the structure.</typeparam>
		/// <typeparam name="Key">The type of the key.</typeparam>
		/// <param name="redBlackTree">The structure.</param>
		/// <param name="get">The key of the item to get.</param>
		/// <param name="comparison">Comparison technique (must match the sorting technique of the structure).</param>
		/// <param name="item">The item if it was found.</param>
		/// <returns>True if successful, False if not.</returns>
		public static bool TryGet<T, Key>(this RedBlackTree<T> redBlackTree, Key get, Compare<T, Key> comparison, out T item)
		{
			try
			{
				item = redBlackTree.Get(get, comparison);
				return true;
			}
			catch
			{
				item = default(T);
				return false;
			}
		}

		/// <summary>Wrapper for the "Remove" method to help with exceptions.</summary>
		/// <typeparam name="T">The generic type of the structure.</typeparam>
		/// <typeparam name="Key">The type of the key.</typeparam>
		/// <param name="redBlackTree">The structure.</param>
		/// <param name="removal">The key of the item to remove.</param>
		/// <param name="comparison">Comparison technique (must match the sorting technique of the structure).</param>
		/// <returns>True if successful, False if not.</returns>
		public static bool TryRemove<T, Key>(this RedBlackTree<T> redBlackTree, Key removal, Compare<T, Key> comparison)
		{
			try
			{
				redBlackTree.Remove(removal, comparison);
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion
	}

	/// <summary>A self sorting binary tree using the red-black tree algorithms.</summary>
	/// <typeparam name="T">The generic type of the structure.</typeparam>
	/// <citation>
	/// This Red-Black Tree imlpementation was originally developed by 
	/// Roy Clem and hosted as an open source project on 
	/// CodeProject.com. However, it has been modified since its
	/// addition into the Towel framework.
	/// http://www.codeproject.com/Articles/8287/Red-Black-Trees-in-C
	/// </citation>
	[Serializable]
	public class RedBlackTreeLinked<T> : RedBlackTree<T>
	{
		// Fields
		internal const bool Red = true;
		internal const bool Black = false;
		private static Node _sentinelNode;

		internal Compare<T, T> _compare_function;
		internal int _count;
		private Node _root;

        #region Nested Types

        [Serializable]
        private class Node
		{
			private bool _color;
			private T _value;
			private Node _leftChild;
			private Node _rightChild;
			private Node _parent;

			internal bool Color { get { return _color; } set { _color = value; } }
			internal T Value { get { return _value; } set { _value = value; } }
			internal Node LeftChild { get { return _leftChild; } set { _leftChild = value; } }
			internal Node RightChild { get { return _rightChild; } set { _rightChild = value; } }
			internal Node Parent { get { return _parent; } set { _parent = value; } }

			internal Node()
			{
				_color = Red;
			}
		}

		#endregion

		#region Constructors

		public RedBlackTreeLinked() : this(Towel.Compare.Default) { } 

		public RedBlackTreeLinked(Compare<T> compare_function)
		{
			_sentinelNode = new Node();
			_sentinelNode.Color = Black;
			this._root = _sentinelNode;
			this._compare_function = new Compare<T, T>(compare_function);
		}

		#endregion

		#region Properties

		/// <summary>Gets the current least item in the avl tree.</summary>
		public T CurrentLeast
		{
			get
			{
				RedBlackTreeLinked<T>.Node treeNode = _root;
				if (treeNode == null || treeNode == _sentinelNode)
					throw new System.InvalidOperationException("attempting to get the minimum value from an empty tree.");
				while (treeNode.LeftChild != _sentinelNode)
					treeNode = treeNode.LeftChild;
				T returnValue = treeNode.Value;
				return returnValue;
			}
		}

		/// <summary>Gets the current greated item in the avl tree.</summary>
		public T CurrentGreatest
		{
			get
			{
				RedBlackTreeLinked<T>.Node treeNode = _root;
				if (treeNode == null || treeNode == _sentinelNode)
					throw new System.InvalidOperationException("attempting to get the maximum value from an empty tree.");
				while (treeNode.RightChild != _sentinelNode)
					treeNode = treeNode.RightChild;
				T returnValue = treeNode.Value;
				return returnValue;
			}
		}

		/// <summary>The number of items in this data structure.</summary>
		public int Count { get { return _count; } }

		/// <summary>The sorting technique.</summary>
		public Compare<T> Compare { get { return new Compare<T>(this._compare_function); } }

		#endregion

		#region Methods

		#region Add

		public void Add(T data)
		{
			RedBlackTreeLinked<T>.Node addition = new RedBlackTreeLinked<T>.Node();
			RedBlackTreeLinked<T>.Node temp = _root;
			while (temp != _sentinelNode)
			{
				addition.Parent = temp;

				switch (this._compare_function(data, temp.Value))
				{
					case Comparison.Equal:
						throw (new System.InvalidOperationException("A Node with the same key already exists"));
					case Comparison.Greater:
						temp = temp.RightChild;
						break;
					case Comparison.Less:
						temp = temp.LeftChild;
						break;
					default:
						throw new System.NotImplementedException();
				}
			}
			addition.Value = data;
			addition.LeftChild = _sentinelNode;
			addition.RightChild = _sentinelNode;
			if (addition.Parent != null)
			{
				switch (this._compare_function(addition.Value, addition.Parent.Value))
				{
					case Comparison.Greater:
						addition.Parent.RightChild = addition;
						break;
					case Comparison.Less:
						addition.Parent.LeftChild = addition;
						break;
					default:
						throw new System.NotImplementedException();
				}
			}
			else
				_root = addition;
			BalanceAddition(addition);
			_count = _count + 1;
		}

		#endregion

		#region Clear

		public void Clear()
		{
			_root = _sentinelNode;
			_count = 0;
		}

		#endregion

		#region Clone

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public RedBlackTreeLinked<T> Clone()
		{
			throw new System.NotImplementedException();
		}

		#endregion

		#region Contains

		public bool Contains(T item)
		{
			return Contains<T>(item, this._compare_function);
		}
		
		public bool Contains<Key>(Key key, Compare<T, Key> comparison)
		{
			RedBlackTreeLinked<T>.Node treeNode = _root;
			while (treeNode != _sentinelNode)
			{
				switch (comparison(treeNode.Value, key))
				{
					case Comparison.Equal:
						return true;
					case Comparison.Greater:
						treeNode = treeNode.LeftChild;
						break;
					case Comparison.Less:
						treeNode = treeNode.RightChild;
						break;
					default:
						throw new System.NotImplementedException();
				}
			}
			return false;
		}
		
		#endregion

		#region Get

		public T Get<Key>(Key key, Compare<T, Key> comparison)
		{
			RedBlackTreeLinked<T>.Node treeNode = _root;
			while (treeNode != _sentinelNode)
			{
				switch (comparison(treeNode.Value, key))
				{
					case Comparison.Equal:
						return treeNode.Value;
					case Comparison.Greater:
						treeNode = treeNode.LeftChild;
						break;
					case Comparison.Less:
						treeNode = treeNode.RightChild;
						break;
					default:
						throw new System.NotImplementedException();
				}
			}
			throw new System.InvalidOperationException("attempting to get a non-existing value.");
		}

		#endregion

		#region Remove

		public void Remove(T value)
		{
			this.Remove<T>(value, this._compare_function);
		}

		public void Remove<Key>(Key key, Compare<T, Key> function)
		{
			RedBlackTreeLinked<T>.Node node;
			node = _root;
			while (node != _sentinelNode)
			{
				switch (function(node.Value, key))
				{
					case Comparison.Equal:
						if (node == _sentinelNode)
							return;
						this.Remove(node);
						this._count = _count - 1;
						return;
					case Comparison.Greater:
						node = node.LeftChild;
						break;
					case Comparison.Less:
						node = node.RightChild;
						break;
					default:
						throw new System.NotImplementedException();
				}
			}
		}

		private void Remove(Node removal)
		{
			Node x = new Node();
			Node temp;
			if (removal.LeftChild == _sentinelNode || removal.RightChild == _sentinelNode)
				temp = removal;
			else
			{
				temp = removal.RightChild;
				while (temp.LeftChild != _sentinelNode)
					temp = temp.LeftChild;
			}
			if (temp.LeftChild != _sentinelNode)
				x = temp.LeftChild;
			else
				x = temp.RightChild;
			x.Parent = temp.Parent;
			if (temp.Parent != null)
				if (temp == temp.Parent.LeftChild)
					temp.Parent.LeftChild = x;
				else
					temp.Parent.RightChild = x;
			else
				_root = x;
			if (temp != removal)
				removal.Value = temp.Value;
			if (temp.Color == Black) BalanceRemoval(x);
		}
		
		#endregion

		#region Stepper And IEnumerable

		/// <summary>Invokes a delegate for each entry in the data structure (least to greatest).</summary>
		/// <param name="function">The delegate to invoke on each item in the structure.</param>
		public void StepperInOrder(Step<T> function)
		{
			StepperInOrder(function, _root);
		}
		
		/// <summary>Invokes a delegate for each entry in the data structure (least to greatest).</summary>
		/// <param name="function">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Step<T> function)
		{
			StepperInOrder(function);
		}
		
		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="function">The delegate to invoke on each item in the structure.</param>
		public void Stepper(StepRef<T> function)
		{
			StepperInOrder(function, this._root);
		}
		
		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="function">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepBreak<T> function)
		{
			return StepperInOrder(function, this._root);
		}
		
		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="function">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepRefBreak<T> function)
		{
			return StepperInOrder(function, this._root);
		}
		
		/// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
		System.Collections.IEnumerator
			System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
		
		/// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			FirstInLastOut<RedBlackTreeLinked<T>.Node> forks = new FirstInLastOutLinked<RedBlackTreeLinked<T>.Node>();
			RedBlackTreeLinked<T>.Node current = _root;
			while (current != null && current.LeftChild != null && current.RightChild != null || forks.Count > 0)
			{
				if (current != null)
				{
					forks.Push(current);
					current = current.LeftChild;
				}
				else if (forks.Count > 0)
				{
					current = forks.Pop();
					if (current.LeftChild != null && current.RightChild != null)
						yield return current.Value;
					current = current.RightChild;
				}
			}
		}

		private void StepperInOrder(Step<T> function, Node node)
		{
			if (node != null && node.LeftChild != null && node.RightChild != null)
			{
				StepperInOrder(function, node.LeftChild);
				function(node.Value);
				StepperInOrder(function, node.RightChild);
			}
		}

		private void StepperInOrder(StepRef<T> function, Node node)
		{
			if (node != null && node.LeftChild != null && node.RightChild != null)
			{
				StepperInOrder(function, node.LeftChild);
				T temp = node.Value;
				function(ref temp);
				node.Value = temp;
				StepperInOrder(function, node.RightChild);
			}
		}

		private StepStatus StepperInOrder(StepBreak<T> function, Node node)
		{
			if (node != null && node.LeftChild != null && node.RightChild != null)
			{
				if (StepperInOrder(function, node.LeftChild) == StepStatus.Break)
					return StepStatus.Break;
				if (function(node.Value) == StepStatus.Break)
					return StepStatus.Break;
				if (StepperInOrder(function, node.RightChild) == StepStatus.Break)
					return StepStatus.Break;
			}
			return StepStatus.Continue;
		}

		private StepStatus StepperInOrder(StepRefBreak<T> function, Node node)
		{
			if (node != null && node.LeftChild != null && node.RightChild != null)
			{
				if (StepperInOrder(function, node.LeftChild) == StepStatus.Break)
					return StepStatus.Break;
				T temp = node.Value;
				StepStatus status = function(ref temp);
				node.Value = temp;
				if (status == StepStatus.Break)
					return StepStatus.Break;
				if (StepperInOrder(function, node.RightChild) == StepStatus.Break)
					return StepStatus.Break;
			}
			return StepStatus.Continue;
		}

		#endregion

		#region Helpers

		private void BalanceAddition(Node balancing)
		{
			Node temp;
			while (balancing != _root && balancing.Parent.Color == Red)
			{
				if (balancing.Parent == balancing.Parent.Parent.LeftChild)
				{
					temp = balancing.Parent.Parent.RightChild;
					if (temp != null && temp.Color == Red)
					{
						balancing.Parent.Color = Black;
						temp.Color = Black;
						balancing.Parent.Parent.Color = Red;
						balancing = balancing.Parent.Parent;
					}
					else
					{
						if (balancing == balancing.Parent.RightChild)
						{
							balancing = balancing.Parent;
							RotateLeft(balancing);
						}
						balancing.Parent.Color = Black;
						balancing.Parent.Parent.Color = Red;
						RotateRight(balancing.Parent.Parent);
					}
				}
				else
				{
					temp = balancing.Parent.Parent.LeftChild;
					if (temp != null && temp.Color == Red)
					{
						balancing.Parent.Color = Black;
						temp.Color = Black;
						balancing.Parent.Parent.Color = Red;
						balancing = balancing.Parent.Parent;
					}
					else
					{
						if (balancing == balancing.Parent.LeftChild)
						{
							balancing = balancing.Parent;
							RotateRight(balancing);
						}
						balancing.Parent.Color = Black;
						balancing.Parent.Parent.Color = Red;
						RotateLeft(balancing.Parent.Parent);
					}
				}
			}
			_root.Color = Black;
		}
		
		private void RotateLeft(Node redBlackTree)
		{
			Node temp = redBlackTree.RightChild;
			redBlackTree.RightChild = temp.LeftChild;
			if (temp.LeftChild != _sentinelNode)
				temp.LeftChild.Parent = redBlackTree;
			if (temp != _sentinelNode)
				temp.Parent = redBlackTree.Parent;
			if (redBlackTree.Parent != null)
			{
				if (redBlackTree == redBlackTree.Parent.LeftChild)
					redBlackTree.Parent.LeftChild = temp;
				else
					redBlackTree.Parent.RightChild = temp;
			}
			else
				_root = temp;
			temp.LeftChild = redBlackTree;
			if (redBlackTree != _sentinelNode)
				redBlackTree.Parent = temp;
		}
		
		private void RotateRight(Node redBlacktree)
		{
			Node temp = redBlacktree.LeftChild;
			redBlacktree.LeftChild = temp.RightChild;
			if (temp.RightChild != _sentinelNode)
				temp.RightChild.Parent = redBlacktree;
			if (temp != _sentinelNode)
				temp.Parent = redBlacktree.Parent;
			if (redBlacktree.Parent != null)
			{
				if (redBlacktree == redBlacktree.Parent.RightChild)
					redBlacktree.Parent.RightChild = temp;
				else
					redBlacktree.Parent.LeftChild = temp;
			}
			else
				_root = temp;
			temp.RightChild = redBlacktree;
			if (redBlacktree != _sentinelNode)
				redBlacktree.Parent = temp;
		}
		
		private void BalanceRemoval(Node balancing)
		{
			Node temp;
			while (balancing != _root && balancing.Color == Black)
			{
				if (balancing == balancing.Parent.LeftChild)
				{
					temp = balancing.Parent.RightChild;
					if (temp.Color == Red)
					{
						temp.Color = Black;
						balancing.Parent.Color = Red;
						RotateLeft(balancing.Parent);
						temp = balancing.Parent.RightChild;
					}
					if (temp.LeftChild.Color == Black && temp.RightChild.Color == Black)
					{
						temp.Color = Red;
						balancing = balancing.Parent;
					}
					else
					{
						if (temp.RightChild.Color == Black)
						{
							temp.LeftChild.Color = Black;
							temp.Color = Red;
							RotateRight(temp);
							temp = balancing.Parent.RightChild;
						}
						temp.Color = balancing.Parent.Color;
						balancing.Parent.Color = Black;
						temp.RightChild.Color = Black;
						RotateLeft(balancing.Parent);
						balancing = _root;
					}
				}
				else
				{
					temp = balancing.Parent.LeftChild;
					if (temp.Color == Red)
					{
						temp.Color = Black;
						balancing.Parent.Color = Red;
						RotateRight(balancing.Parent);
						temp = balancing.Parent.LeftChild;
					}
					if (temp.RightChild.Color == Black && temp.LeftChild.Color == Black)
					{
						temp.Color = Red;
						balancing = balancing.Parent;
					}
					else
					{
						if (temp.LeftChild.Color == Black)
						{
							temp.RightChild.Color = Black;
							temp.Color = Red;
							RotateLeft(temp);
							temp = balancing.Parent.LeftChild;
						}
						temp.Color = balancing.Parent.Color;
						balancing.Parent.Color = Black;
						temp.LeftChild.Color = Black;
						RotateRight(balancing.Parent);
						balancing = _root;
					}
				}
			}
			balancing.Color = Black;
		}

		#endregion

		#endregion
	}
}
