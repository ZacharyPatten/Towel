using System;
using static Towel.Statics;

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

	/// <summary>Static helpers.</summary>
	public static class TreeMap
	{
		/// <summary>Constructs a new <see cref="TreeMap{T, TEquate, THash}"/>.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <returns>The new constructed <see cref="TreeMap{T, TEquate, THash}"/>.</returns>
		public static TreeMap<T, FuncRuntime<T, T, bool>, FuncRuntime<T, int>> New<T>(
			T head,
			Func<T, T, bool>? equate = null,
			Func<T, int>? hash = null) =>
			new(head, equate ?? Equate, hash ?? DefaultHash);
	}

	/// <summary>A generic tree data structure using a dictionary to store node data.</summary>
	/// <typeparam name="T">The generic type stored in this data structure.</typeparam>
	public class TreeMap<T, TEquate, THash> : ITree<T>,
		// Structure Properties
		DataStructure.IHashing<T, THash>,
		DataStructure.IEquating<T, TEquate>
		where TEquate : struct, IFunc<T, T, bool>
		where THash : struct, IFunc<T, int>
	{
		internal T _head;
		internal MapHashLinked<Node, T, TEquate, THash> _tree;

		#region Node

		internal class Node
		{
			internal T? Parent;
			internal SetHashLinked<T, TEquate, THash> Children;

			public Node(T? parent, SetHashLinked<T, TEquate, THash> children)
			{
				Parent = parent;
				Children = children;
			}
		}

		#endregion

		#region Constructors

		public TreeMap(T head, TEquate equate = default, THash hash = default)
		{
			_head = head;
			_tree = new(equate, hash)
			{
				{ _head, new Node(default, new(equate, hash)) }
			};
		}

		/// <summary>This constructor is for cloning purposes.</summary>
		/// <param name="tree">The tree to clone.</param>
		internal TreeMap(TreeMap<T, TEquate, THash> tree)
		{
			_head = tree._head;
			_tree = tree._tree.Clone();
		}

		#endregion

		#region Properties

		/// <summary>The head of the tree.</summary>
		public T Head => _head;

		/// <summary>The hash function being used (was passed into the constructor).</summary>
		public THash Hash => _tree.Hash;

		/// <summary>The equate function being used (was passed into the constructor).</summary>
		public TEquate Equate => _tree.Equate;

		/// <summary>The number of nodes in this tree.</summary>
		public int Count => _tree.Count;

		#endregion

		#region Methods

		/// <summary>Determines if a node is the child of another node.</summary>
		/// <param name="node">The child to check the parent of.</param>
		/// <param name="parent">The parent to check the child of.</param>
		/// <returns>True if the node is a child of the parent; False if not.</returns>
		public bool IsChildOf(T node, T parent)
		{
			if (_tree.TryGet(parent, out Node? nodeData))
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
			if (_tree.TryGet(child, out Node? node))
			{
				return node.Parent;
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
			if (_tree.TryGet(parent, out Node? node))
			{
				node.Children.Stepper(step);
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
			if (_tree.TryGet(parent, out Node? node))
			{
				_tree.Add(addition, new Node(parent, new(Equate, Hash)));
				node.Children.Add(addition);
			}
			else
			{
				throw new InvalidOperationException("Attempting to add a node to a non-existing parent");
			}
		}


		/// <summary>Removes a node from the tree and all the child nodes.</summary>
		/// <param name="removal">The node to be removed.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		public bool TryRemove(T removal, out Exception? exception)
		{
			if (_tree.TryGet(removal, out Node? node))
			{
				_tree[node.Parent].Children.Remove(removal);
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

		/// <inheritdoc/>
		public StepStatus StepperBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<T, StepStatus> =>
			_tree.KeysBreak(step);

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public TreeMap<T, TEquate, THash> Clone() => new(this);

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
