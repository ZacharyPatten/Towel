using System;
using static Towel.Syntax;

namespace Towel.DataStructures
{
	/// <summary>A generic tree data structure.</summary>
	/// <typeparam name="T">The generic type stored in this data structure.</typeparam>
	public interface ITree<T> : IDataStructure<T>,
		// Structure Properties
		DataStructure.ICountable,
		DataStructure.IRemovable<T>
	{
		#region Properties

		/// <summary>The head of the tree.</summary>
		T Head { get; }

		#endregion

		#region Methods

		/// <summary>Determines if a node is the child of another node.</summary>
		/// <param name="node">The child to check the parent of.</param>
		/// <param name="parent">The parent to check the child of.</param>
		/// <returns>True if the node is a child of the parent; False if not.</returns>
		bool IsChildOf(T node, T parent);

		/// <summary>Stepper function for the children of a given node.</summary>
		/// <param name="parent">The node to step through the children of.</param>
		/// <param name="step">The step function.</param>
		void Children(T parent, Action<T> step);

		/// <summary>Adds a node to the tree.</summary>
		/// <param name="addition">The node to be added.</param>
		/// <param name="parent">The parent of the node to be added.</param>
		void Add(T addition, T parent);

		#endregion
	}

	public static class Tree
	{

	}

	/// <summary>A generic tree data structure using a dictionary to store node data.</summary>
	/// <typeparam name="T">The generic type stored in this data structure.</typeparam>
	public class TreeMap<T> : ITree<T>,
		// Structure Properties
		DataStructure.IHashing<T>,
		DataStructure.IEquating<T>
	{
		internal Func<T, T, bool> _equate;
		internal Func<T, int> _hash;
		internal T _head;
		internal MapHashLinked<Node, T> _tree;

		#region Node

		internal class Node
		{
			internal T Parent;
			internal SetHashLinked<T> Children;

			public Node(T parent, SetHashLinked<T> children)
			{
				Parent = parent;
				Children = children;
			}
		}

		#endregion

		#region Constructors

		public TreeMap(T head) : this(head, DefaultEquals, DefaultHash) { }

		public TreeMap(T head, Func<T, T, bool> equate, Func<T, int> hash)
		{
			_equate = equate;
			_hash = hash;
			_head = head;
			_tree = new MapHashLinked<Node, T>(_equate, _hash)
			{
				{ _head, new Node(default, new SetHashLinked<T>(_equate, _hash)) }
			};
		}

		#endregion

		#region Properties

		/// <summary>The head of the tree.</summary>
		public T Head { get { return _head; } }

		/// <summary>The hash function being used (was passed into the constructor).</summary>
		public Func<T, int> Hash { get { return _hash; } }

		/// <summary>The equate function being used (was passed into the constructor).</summary>
		public Func<T, T, bool> Equate { get { return _equate; } }

		/// <summary>The number of nodes in this tree.</summary>
		public int Count { get { return _tree.Count; } }

		#endregion

		#region Methods

		/// <summary>Determines if a node is the child of another node.</summary>
		/// <param name="node">The child to check the parent of.</param>
		/// <param name="parent">The parent to check the child of.</param>
		/// <returns>True if the node is a child of the parent; False if not.</returns>
		public bool IsChildOf(T node, T parent)
		{
			if (_tree.TryGet(parent, out Node nodeData))
			{
				return nodeData.Children.Contains(node);
			}
			else
			{
				throw new InvalidOperationException("Attempting to get the children of a non-existing node");
			}
		}

		/// <summary>Gets the parent of a given node.</summary>
		/// <param name="child">The child to get the parent of.</param>
		/// <returns>The parent of the given child.</returns>
		public T Parent(T child)
		{
			if (_tree.TryGet(child, out Node nodeData))
			{
				return nodeData.Parent;
			}
			else
			{
				throw new InvalidOperationException("Attempting to get the parent of a non-existing node");
			}
		}

		/// <summary>Stepper function for the children of a given node.</summary>
		/// <param name="parent">The node to step through the children of.</param>
		/// <param name="step">The step function.</param>
		public void Children(T parent, Action<T> step)
		{
			if (_tree.TryGet(parent, out Node nodeData))
			{
				nodeData.Children.Stepper(step);
			}
			else
			{
				throw new InvalidOperationException("Attempting to get the children of a non-existing node");
			}
		}

		/// <summary>Adds a node to the tree.</summary>
		/// <param name="addition">The node to be added.</param>
		/// <param name="parent">The parent of the node to be added.</param>
		public void Add(T addition, T parent)
		{
			if (_tree.TryGet(parent, out Node nodeData))
			{
				_tree.Add(addition, new Node(parent, new SetHashLinked<T>(_equate, _hash)));
				nodeData.Children.Add(addition);
			}
			else
			{
				throw new InvalidOperationException("Attempting to add a node to a non-existing parent");
			}
		}


		/// <summary>Removes a node from the tree and all the child nodes.</summary>
		/// <param name="removal">The node to be removed.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		public bool TryRemove(T removal, out Exception exception)
		{
			if (_tree.TryGet(removal, out Node nodeData))
			{
				_tree[nodeData.Parent].Children.Remove(removal);
				RemoveRecursive(removal);
				exception = null;
				return true;
			}
			else
			{
				exception = new InvalidOperationException("Attempting to remove a non-existing node");
				return false;
			}
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Action<T> step)
		{
			_tree.Keys(step);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(Func<T, StepStatus> step)
		{
			return _tree.Keys(step);
		}

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public TreeMap<T> Clone()
		{
			throw new NotImplementedException();
		}

		System.Collections.IEnumerator
			System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		System.Collections.Generic.IEnumerator<T>
			System.Collections.Generic.IEnumerable<T>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		internal void RemoveRecursive(T current)
		{
			_tree[current].Children.Stepper(child => RemoveRecursive(child));
			_tree.Remove(current);
		}

		#endregion
	}
}
