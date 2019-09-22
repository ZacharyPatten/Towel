using System;

namespace Towel.DataStructures
{
	/// <summary>A graph data structure that stores nodes and edges.</summary>
	/// <typeparam name="T">The generic node type to store in the graph.</typeparam>
	public interface IGraph<T> : IDataStructure<T>,
		// Structure Properties
		DataStructure.IAddable<T>,
		DataStructure.IRemovable<T>,
		DataStructure.IClearable
	{
		#region Properties

		/// <summary>The number of edges in the graph.</summary>
		int EdgeCount { get; }
		/// <summary>The number of nodes in the graph.</summary>
		int NodeCount { get; }

		#endregion

		#region Methods

		/// <summary>Checks if b is adjacent to a.</summary>
		/// <param name="a">The starting point of the edge to check.</param>
		/// <param name="b">The ending point of the edge to check.</param>
		/// <returns>True if b is adjacent to a; False if not</returns>
		bool Adjacent(T a, T b);
		/// <summary>Gets all the nodes adjacent to a and performs the provided delegate on each.</summary>
		/// <param name="a">The node to find all the adjacent node to.</param>
		/// <param name="function">The delegate to perform on each adjacent node to a.</param>
		void Neighbors(T a, Step<T> function);
		/// <summary>Adds an edge to the graph starting at a and ending at b.</summary>
		/// <param name="start">The stating point of the edge to add.</param>
		/// <param name="end">The ending point of the edge to add.</param>
		void Add(T start, T end);
		/// <summary>Removes an edge from the graph.</summary>
		/// <param name="start">The starting point of the edge to remove.</param>
		/// <param name="end">The ending point of the edge to remove.</param>
		void Remove(T start, T end);
		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		void Stepper(Step<T, T> step);
		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		StepStatus Stepper(StepBreak<T, T> step);

		#endregion
	}

	/// <summary>Stores the graph as a set-hash of nodes and quadtree of edges.</summary>
	/// <typeparam name="T">The generic type of this data structure.</typeparam>
	public class GraphSetOmnitree<T> : IGraph<T>
	{
		internal SetHashLinked<T> _nodes;
		internal OmnitreePointsLinked<Edge, T, T> _edges;

		#region Nested Types

		/// <summary>Represents an edge in a graph.</summary>
		public class Edge
		{
			/// <summary>The starting node of the edge.</summary>
			public readonly T Start;
			/// <summary>The ending node of hte edge.</summary>
			public readonly T End;

			/// <summary>Constructs a new Edge.</summary>
			/// <param name="start">The starting point of the Edge.</param>
			/// <param name="end">The ending point of the Edge.</param>
			public Edge(T start, T end)
			{
				Start = start;
				End = end;
			}
		}

		#endregion

		#region Constructor

		/// <summary>Constructs a new GraphSetOmnitree by cloning an existing instance.</summary>
		/// <param name="graph">The graph to construct a clone of.</param>
		internal GraphSetOmnitree(GraphSetOmnitree<T> graph)
		{
			_edges = graph._edges.Clone() as OmnitreePointsLinked<Edge, T, T>;
			_nodes = graph._nodes.Clone() as SetHashLinked<T>;
		}

		/// <summary>Constructs a new GraphSetOmnitree.</summary>
		/// <param name="equate">The equate delegate for the data structure to use.</param>
		/// <param name="compare">The compare delegate for the data structure to use.</param>
		/// <param name="hash">The hash delegate for the datastructure to use.</param>
		public GraphSetOmnitree(Equate<T> equate, Compare<T> compare, Hash<T> hash)
		{
			_nodes = new SetHashLinked<T>(equate, hash);
			_edges = new OmnitreePointsLinked<Edge, T, T>(
				(Edge a, out T start, out T end) =>
				{
					start = a.Start;
					end = a.End;
				},
				compare, compare);
		}

		/// <summary>Constructs a new GraphSetOmnitree.</summary>
		public GraphSetOmnitree() :
			this(Equate.Default, Compare.Default, Hash.Default)
		{ }

		#endregion

		#region Properties

		/// <summary>Gets the number of edges in the graph.</summary>
		public int EdgeCount => _edges.Count;

		/// <summary>Gets the number of nodes in the graph.</summary>
		public int NodeCount => _nodes.Count;

		#endregion

		#region Methods

		/// <summary>Adds a node to the graph.</summary>
		/// <param name="node">The node to add to the graph.</param>
		public bool TryAdd(T node, out Exception exception)
		{
			if (_nodes.Contains(node))
			{
				exception = new InvalidOperationException("Adding an already-existing node to a graph");
				return false;
			}
			_nodes.Add(node);
			exception = null;
			return true;
		}

		/// <summary>Adds an edge to the graph.</summary>
		/// <param name="start">The starting point of the edge.</param>
		/// <param name="end">The ending point of the edge.</param>
		public void Add(T start, T end)
		{
			if (!_nodes.Contains(start))
			{
				throw new InvalidOperationException("Adding an edge to a graph from a node that does not exists");
			}
			if (!_nodes.Contains(end))
			{
				throw new InvalidOperationException("Adding an edge to a graph to a node that does not exists");
			}
			_edges.Stepper(
				(Edge e) => throw new InvalidOperationException("Adding an edge to a graph that already exists"),
				start, start, end, end);

			_edges.Add(new Edge(start, end));
		}

		/// <summary>Removes a node from the graph and all attached edges.</summary>
		/// <param name="node">The edge to remove from the graph.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the remove succeeded or false if not.</returns>
		public bool TryRemove(T node, out Exception exception)
		{
			if (_nodes.Contains(node))
			{
				exception = new InvalidOperationException("Removing non-existing node from a graph");
				return false;
			}
			_edges.Remove(node, node, Omnitree.Bound<T>.None, Omnitree.Bound<T>.None);
			_edges.Remove(Omnitree.Bound<T>.None, Omnitree.Bound<T>.None, node, node);
			_nodes.Add(node);
			exception = null;
			return true;
		}

		/// <summary>Removes an edge from the graph.</summary>
		/// <param name="start">The starting point of the edge.</param>
		/// <param name="end">The ending point of the edge.</param>
		public void Remove(T start, T end)
		{
			if (!_nodes.Contains(start))
			{
				throw new InvalidOperationException("Removing an edge to a graph from a node that does not exists");
			}
			if (!_nodes.Contains(end))
			{
				throw new InvalidOperationException("Removing an edge to a graph to a node that does not exists");
			}
			_edges.Stepper(
				(Edge e) => throw new InvalidOperationException("Removing a non-existing edge in a graph"),
				start, start, end, end);
			_edges.Remove(start, start, end, end);
		}

		/// <summary>Checks two nodes for adjacency (connected by an edge).</summary>
		/// <param name="a">The first node of the adjacency check.</param>
		/// <param name="b">The second node of the adjacency check.</param>
		/// <returns>True if adjacent. False if not.</returns>
		public bool Adjacent(T a, T b)
		{
			bool exists = false;
			_edges.Stepper(edge =>
			{
				exists = true;
				return StepStatus.Break;
			}, a, a, b, b);
			return exists;
		}

		/// <summary>Gets the neighbors of a node.</summary>
		/// <param name="node">The node to get the neighbors of.</param>
		/// <param name="step">The step to perform on all the neighbors.</param>
		public void Neighbors(T node, Step<T> step)
		{
			if (!_nodes.Contains(node))
			{
				throw new InvalidOperationException("Attempting to look up the neighbors of a node that does not belong to a graph");
			}
			_edges.Stepper(e => step(e.End),
				node, node,
				Omnitree.Bound<T>.None, Omnitree.Bound<T>.None);
		}

		/// <summary>Clones this data structure.</summary>
		/// <returns>A clone of this data structure.</returns>
		public IDataStructure<T> Clone()
		{
			return new GraphSetOmnitree<T>(this);
		}

		/// <summary>Returns this data structure to an empty state.</summary>
		public void Clear()
		{
			_nodes.Clear();
			_edges.Clear();
		}

		/// <summary>Steps through all the nodes in the graph.</summary>
		/// <param name="step">The action to perform on all the nodes in the graph.</param>
		public void Stepper(Step<T> step)
		{
			_nodes.Stepper(step);
		}

		/// <summary>Steps through all the nodes in the graph.</summary>
		/// <param name="step">The action to perform on all the nodes in the graph.</param>
		/// <returns>The status of the stepper operation.</returns>
		public StepStatus Stepper(StepBreak<T> step)
		{
			return _nodes.Stepper(step);
		}

		/// <summary>Steps through all the edges in the graph.</summary>
		/// <param name="step">The action to perform on all the edges in the graph.</param>
		public void Stepper(Step<T, T> step)
		{
			_edges.Stepper(edge => step(edge.Start, edge.End));
		}

		/// <summary>Steps through all the edges in the graph.</summary>
		/// <param name="step">The action to perform on all the edges in the graph.</param>
		/// <returns>The status of the stepper operation.</returns>
		public StepStatus Stepper(StepBreak<T, T> step)
		{
			return _edges.Stepper(edge => step(edge.Start, edge.End));
		}

		System.Collections.IEnumerator
			System.Collections.IEnumerable.GetEnumerator()
		{
			return (_nodes as System.Collections.Generic.IEnumerable<T>).GetEnumerator();
		}

		System.Collections.Generic.IEnumerator<T>
			System.Collections.Generic.IEnumerable<T>.GetEnumerator()
		{
			return (_nodes as System.Collections.Generic.IEnumerable<T>).GetEnumerator();
		}

		#endregion
	}

	/// <summary>Stores a graph as a map and nested map (adjacency matrix).</summary>
	/// <typeparam name="T">The generic node type of this graph.</typeparam>
	public class GraphMap<T> : IGraph<T>
	{
		internal MapHashLinked<MapHashLinked<bool, T>, T> _map;
		internal int _edges;

		#region Edge

		/// <summary>Represents an edge in a graph.</summary>
		public class Edge
		{
			/// <summary>The starting point of the edge.</summary>
			public readonly T Start;
			/// <summary>The ending point of the edge.</summary>
			public readonly T End;

			/// <summary>Constructs a new Edge.</summary>
			/// <param name="start">The starting point of the edge.</param>
			/// <param name="end">The ending point of the edge.</param>
			public Edge(T start, T end)
			{
				Start = start;
				End = end;
			}
		}

		#endregion

		#region Constructors

		/// <summary>Constructs a new GraphMap.</summary>
		public GraphMap() : this(Equate.Default, Hash.Default) { }

		/// <summary>Constructs a new GraphMap.</summary>
		/// <param name="equate">The equate delegate for the data structure to use.</param>
		/// <param name="hash">The hash function for the data structure to use.</param>
		public GraphMap(Equate<T> equate, Hash<T> hash)
		{
			_edges = 0;
			_map = new MapHashLinked<MapHashLinked<bool, T>, T>(equate, hash);
		}

		#endregion

		#region Properties

		/// <summary>Gets the number of edges in the graph.</summary>
		public int EdgeCount => _edges;

		/// <summary>Gets the number of nodes in the graph.</summary>
		public int NodeCount => _map.Count;

		#endregion

		#region Methods

		/// <summary>Adds a node to the graph.</summary>
		/// <param name="node">The node to add to the graph.</param>
		/// <param name="exception">The exception that occurred if the add failed.</param>
		public bool TryAdd(T node, out Exception exception)
		{
			if (_map.Count == int.MaxValue)
			{
				exception = new InvalidOperationException("This graph has reach its node capacity.");
				return false;
			}
			_map.Add(node, new MapHashLinked<bool, T>(_map.Equate, _map.Hash));
			exception = null;
			return true;
		}

		/// <summary>Adds an edge to the graph.</summary>
		/// <param name="start">The starting point of the edge.</param>
		/// <param name="end">The ending point of the edge.</param>
		public void Add(T start, T end)
		{
			if (!_map.Contains(start))
				throw new InvalidOperationException("Adding an edge to a non-existing starting node.");
			if (!_map[start].Contains(end))
				throw new InvalidOperationException("Adding an edge to a non-existing ending node.");
			if (_map[start] == null)
				_map[start] = new MapHashLinked<bool, T>(_map.Equate, _map.Hash);

			_map[start].Add(end, true);
		}

		/// <summary>Removes a node from the graph.</summary>
		/// <param name="node">The node to remove from the graph.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the remove succeeded or false if not.</returns>
		public bool TryRemove(T node, out Exception exception)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes an edge from the graph.</summary>
		/// <param name="start">The starting point of the edge to remove.</param>
		/// <param name="end">The ending point of the edge to remove.</param>
		public void Remove(T start, T end)
		{
			if (_map.TryGet(start, out MapHashLinked<bool, T> foundValue) && foundValue.TryRemove(end))
			{
				return;
			}
			throw new InvalidOperationException("Removing a non-existing edge from the graph.");
		}

		/// <summary>Checks for adjacency between two nodes.</summary>
		/// <param name="a">The first node of the adjacency check.</param>
		/// <param name="b">The second node fo the adjacency check.</param>
		/// <returns>True if ajacent. False if not.</returns>
		public bool Adjacent(T a, T b)
		{
			if (_map.TryGet(a, out MapHashLinked<bool, T> map))
			{
				if (map.TryGet(a, out bool boolean))
				{
					return boolean;
				}
			}
			return false;
		}

		/// <summary>Steps through all the neighbors of a node.</summary>
		/// <param name="a">The node to step through the children of.</param>
		/// <param name="step">The action to perform on all the neighbors of the provided node.</param>
		public void Neighbors(T a, Step<T> step)
		{
			if (_map.TryGet(a, out MapHashLinked<bool, T> map))
			{
				map.Keys(step);
			}
		}

		public void Stepper(Step<T> step)
		{
			throw new NotImplementedException();
		}

		public void Stepper(StepRef<T> step)
		{
			throw new NotImplementedException();
		}

		public StepStatus Stepper(StepBreak<T> step)
		{
			throw new NotImplementedException();
		}

		public StepStatus Stepper(StepRefBreak<T> step)
		{
			throw new NotImplementedException();
		}

		public void Stepper(Step<T, T> step)
		{
			throw new NotImplementedException();
		}

		public void Stepper(StepRef<T, T> step)
		{
			throw new NotImplementedException();
		}

		public StepStatus Stepper(StepBreak<T, T> step)
		{
			throw new NotImplementedException();
		}

		public StepStatus Stepper(StepRefBreak<T, T> step)
		{
			throw new NotImplementedException();
		}

		public IDataStructure<T> Clone()
		{
			throw new NotImplementedException();
		}

		public T[] ToArray()
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

		public void Clear()
		{
			_edges = 0;
			_map = new MapHashLinked<MapHashLinked<bool, T>, T>(_map.Equate, _map.Hash);
		}

		#endregion
	}
}
