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
		(bool Success, Exception? Exception) TryAdd(T addition, T parent);

		#endregion
	}

	/// <summary>Static helpers.</summary>
	public static class TreeMap
	{
		/// <summary>Constructs a new <see cref="TreeMap{T, TEquate, THash}"/>.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <returns>The new constructed <see cref="TreeMap{T, TEquate, THash}"/>.</returns>
		public static TreeMap<T, SFunc<T, T, bool>, SFunc<T, int>> New<T>(
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
		internal T _top;
		internal MapHashLinked<Node, T, TEquate, THash> _map;

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

		public TreeMap(T top, TEquate equate = default, THash hash = default)
		{
			_top = top;
			_map = new(equate, hash)
			{
				{ _top, new Node(default, new(equate, hash)) }
			};
		}

		/// <summary>This constructor is for cloning purposes.</summary>
		/// <param name="tree">The tree to clone.</param>
		internal TreeMap(TreeMap<T, TEquate, THash> tree)
		{
			_top = tree._top;
			_map = tree._map.Clone();
		}

		#endregion

		#region Properties

		/// <summary>The head of the tree.</summary>
		public T Head => _top;

		/// <summary>The hash function being used (was passed into the constructor).</summary>
		public THash Hash => _map.Hash;

		/// <summary>The equate function being used (was passed into the constructor).</summary>
		public TEquate Equate => _map.Equate;

		/// <summary>The number of nodes in this tree.</summary>
		public int Count => _map.Count;

		#endregion

		#region Methods

		/// <summary>Determines if a node is the child of another node.</summary>
		/// <param name="node">The child to check the parent of.</param>
		/// <param name="parent">The parent to check the child of.</param>
		/// <returns>True if the node is a child of the parent; False if not.</returns>
		public bool IsChildOf(T node, T parent)
		{
			if (!_map.Contains(node))
			{
				throw new ArgumentException(paramName: nameof(node), message: "Check for a parent-child relationship of non-existing node.");
			}
			var (success, value,exception) = _map.TryGet(parent);
			if (!success)
			{
				throw new ArgumentException(paramName: nameof(parent), message: "Check for a parent-child relationship of non-existing node.", innerException: exception);
			}
			return value!.Children.Contains(node);
		}

		/// <summary>Gets the parent of a given node.</summary>
		/// <param name="child">The child to get the parent of.</param>
		/// <returns>The parent of the given child.</returns>
		public T Parent(T child)
		{
			if (Equate.Invoke(child, _top))
			{
				throw new InvalidOperationException("Attempting to get the parent of the top of the tree.");
			}
			var (success, value, exception) = _map.TryGet(child);
			if (!success)
			{
				throw new InvalidOperationException("Attempting to get the parent of a non-existing node.", innerException: exception);
			}
			return value!.Parent!;
		}

		/// <summary>Stepper function for the children of a given node.</summary>
		/// <param name="parent">The node to step through the children of.</param>
		/// <param name="step">The step function.</param>
		public void Children(T parent, Action<T> step)
		{
			var (success, value, exception) = _map.TryGet(parent);
			if (!success)
			{
				throw new ArgumentException(paramName: nameof(parent), message: "Attepting to step through the children of a none-existing parent.", innerException: exception);
			}
			value!.Children.Stepper(step);
		}

		/// <summary>Adds a node to the tree.</summary>
		/// <param name="node">The node to be added.</param>
		/// <param name="parent">The parent of the node to be added.</param>
		public (bool Success, Exception? Exception) TryAdd(T node, T parent)
		{
			if (_map.Contains(node))
			{
				return (false, new ArgumentException(paramName: nameof(node), message: "Adding an already-existing node to a tree."));
			}
			var (success, value, exception) = _map.TryGet(parent);
			if (!success)
			{
				return (false, new ArgumentException(paramName: nameof(parent), message: "Adding a node to a non-existant parent in a tree.", innerException: exception));
			}
			_map.Add(node, new Node(parent, new(Equate, Hash)));
			value!.Children.Add(node);
			return (true, null);
		}


		/// <summary>Removes a node from the tree and all the child nodes.</summary>
		/// <param name="node">The node to be removed.</param>
		public (bool Success, Exception? Exception) TryRemove(T node)
		{
			if (Equate.Invoke(node, _top))
			{
				return (false, new ArgumentException(paramName: nameof(node), message: "Attempting to remove the top of the tree."));
			}

			var (success, value, exception) = _map.TryGet(node);
			if (!success)
			{
				return (false, new InvalidOperationException("Attempting to remove a non-existing node", exception));
			}
			_map[value!.Parent!].Children.Remove(node);
			RemoveRecursive(node);
			return (true, null);
		}

		/// <inheritdoc/>
		public StepStatus StepperBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<T, StepStatus> =>
			_map.KeysBreak(step);

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public TreeMap<T, TEquate, THash> Clone() => new(this);

		internal void RemoveRecursive(T current)
		{
			_map[current].Children.Stepper(child => RemoveRecursive(child));
			_map.Remove(current);
		}

		#endregion
	}
}
