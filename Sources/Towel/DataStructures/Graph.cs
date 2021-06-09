using System;
using static Towel.Statics;

namespace Towel.DataStructures
{
	/// <summary>Static helpers for <see cref="IGraph{T}"/>.</summary>
	public static class Graph
	{
		/// <summary>Tries to removes a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <param name="structure">The structure to remove the value from.</param>
		/// <param name="value">The value to be removed.</param>
		/// <returns>True if the remove was successful or false if not.</returns>
		public static bool TryRemove<T>(this IGraph<T> structure, T value) =>
			structure.TryRemove(value, out _);

		/// <summary>Removes a value.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <param name="structure">The structure to remove the value from.</param>
		/// <param name="value">The value to be removed.</param>
		public static void Remove<T>(this IGraph<T> structure, T value)
		{
			if (!structure.TryRemove(value, out Exception? exception))
			{
				throw exception ?? new ArgumentException($"{nameof(Remove)} failed but the {nameof(exception)} is null"); ;
			}
		}

		public static void Remove<T>(this IGraph<T> structure, T a, T b)
		{
			if (!structure.TryRemove(a, b, out Exception? exception))
			{
				throw exception ?? new ArgumentException($"{nameof(Remove)} failed but the {nameof(exception)} is null"); ;
			}
		}
	}

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
		void Neighbors(T a, Action<T> function);

		/// <summary>Adds an edge to the graph starting at a and ending at b.</summary>
		/// <param name="start">The stating point of the edge to add.</param>
		/// <param name="end">The ending point of the edge to add.</param>
		void Add(T start, T end);

		/// <summary>Removes an edge from the graph.</summary>
		/// <param name="start">The starting point of the edge to remove.</param>
		/// <param name="end">The ending point of the edge to remove.</param>
		bool TryRemove(T start, T end, out Exception? exception);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		void Edges(Action<(T?, T?)> step);

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		StepStatus EdgesBreak(Func<(T, T), StepStatus> step);

		#endregion
	}

	/// <summary>Static helpers for <see cref="GraphSetOmnitree{T, TEquate, THash}"/>.</summary>
	public static class GraphSetOmnitree
	{
		/// <summary>Constructs a new <see cref="GraphSetOmnitree{T, TEquate, THash}"/>.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <returns>The new constructed <see cref="GraphSetOmnitree{T, TEquate, THash}"/>.</returns>
		public static GraphSetOmnitree<T, SFunc<T, T, bool>, SFunc<T, int>> New<T>(
			Func<T, T, bool>? equate = null,
			Func<T, int>? hash = null) =>
			new(equate ?? Equate, hash ?? DefaultHash);
	}

	/// <summary>Stores the graph as a set-hash of nodes and quadtree of edges.</summary>
	/// <typeparam name="T">The generic type of this data structure.</typeparam>
	public class GraphSetOmnitree<T, TEquate, THash> : IGraph<T>,
		DataStructure.IEquating<T, TEquate>,
		DataStructure.IHashing<T, THash>
		where TEquate : struct, IFunc<T, T, bool>
		where THash : struct, IFunc<T, int>
	{
		internal SetHashLinked<T?, TEquate, THash> _nodes;
		internal OmnitreePointsLinked<Edge, T?, T?> _edges;

		#region Edge

		/// <summary>Represents an edge in a graph.</summary>
		public class Edge
		{
			/// <summary>The starting node of the edge.</summary>
			public T? Start { get; internal set; }
			/// <summary>The ending node of the edge.</summary>
			public T? End { get; internal set; }
		}

		#endregion

		#region Constructors

		/// <summary>This constructor is for cloning purposes.</summary>
		/// <param name="graph">The graph to construct a clone of.</param>
		internal GraphSetOmnitree(GraphSetOmnitree<T, TEquate, THash> graph)
		{
			_edges = graph._edges.Clone();
			_nodes = graph._nodes.Clone();
		}

		/// <summary>Constructs a new GraphSetOmnitree.</summary>
		/// <param name="equate">The equate delegate for the data structure to use.</param>
		/// <param name="compare">The compare delegate for the data structure to use.</param>
		/// <param name="hash">The hash delegate for the datastructure to use.</param>
		public GraphSetOmnitree(
			TEquate equate = default,
			THash hash = default,
			Func<T?, T?, CompareResult>? compare = null)
		{
			compare ??= Compare;
			_nodes = new(equate, hash);
			_edges = new OmnitreePointsLinked<Edge, T, T>(
				(Edge a, out T start, out T end) =>
				{
					start = a.Start;
					end = a.End;
				},
				compare, compare);
		}

		#endregion

		#region Properties

		/// <inheritdoc/>
		public TEquate Equate => _nodes.Equate;

		/// <inheritdoc/>
		public THash Hash => _nodes.Hash;

		/// <inheritdoc/>
		public int EdgeCount => _edges.Count;

		/// <inheritdoc/>
		public int NodeCount => _nodes.Count;

		#endregion

		#region Methods

		/// <summary>Tries to add a node to the graph.</summary>
		/// <param name="node">The node to add to the graph.</param>
		/// <param name="exception">The exception that occured if there was a failure.</param>
		public bool TryAdd(T node, out Exception? exception)
		{
			return _nodes.TryAdd(node, out exception);
		}

		/// <summary>Adds an edge to the graph.</summary>
		/// <param name="start">The starting point of the edge.</param>
		/// <param name="end">The ending point of the edge.</param>
		public void Add(T start, T end)
		{
			if (!_nodes.Contains(start))
			{
				throw new ArgumentException("Adding an edge to a graph from a node that does not exists");
			}
			if (!_nodes.Contains(end))
			{
				throw new ArgumentException("Adding an edge to a graph to a node that does not exists");
			}
			_edges.Stepper(
				e => throw new ArgumentException("Adding an edge to a graph that already exists"),
				start, start, end, end);

			_edges.Add(new Edge() { Start = start, End = end });
		}

		/// <summary>Removes a node from the graph and all attached edges.</summary>
		/// <param name="node">The edge to remove from the graph.</param>
		/// <param name="exception">The exception that occured if there was a failure.</param>
		/// <returns>True if the remove succeeded or false if not.</returns>
		public bool TryRemove(T node, out Exception? exception)
		{
			if (!_nodes.TryRemove(node, out exception))
			{
				return false;
			}
			_edges.Remove(node, node, None, None);
			_edges.Remove(None, None, node, node);
			exception = null;
			return true;
		}

		/// <summary>Removes an edge from the graph.</summary>
		/// <param name="start">The starting point of the edge.</param>
		/// <param name="end">The ending point of the edge.</param>
		/// <param name="exception">The exception that occured if there was a failure.</param>
		public bool TryRemove(T start, T end, out Exception? exception)
		{
			exception = null;
			if (!_nodes.Contains(start))
			{
				exception = new ArgumentException("Removing an edge to a graph from a node that does not exists");
			}
			if (!_nodes.Contains(end))
			{
				exception = new ArgumentException("Removing an edge to a graph to a node that does not exists");
			}

			#warning Add the number of values removed in Omnitree Remove method

			_edges.Stepper(
				(Edge e) => throw new ArgumentException("Removing a non-existing edge in a graph"),
				start, start, end, end);
			_edges.Remove(start, start, end, end);

			//int count = _edges.Remove(start, start, end, end);
			//if (count <= 0) exception = new ArgumentException("Removing a non-existing edge in a graph");

			return exception is not null;
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
				return Break;
			}, a, a, b, b);
			return exists;
		}

		/// <summary>Gets the neighbors of a node.</summary>
		/// <param name="node">The node to get the neighbors of.</param>
		/// <param name="step">The step to perform on all the neighbors.</param>
		public void Neighbors(T node, Action<T?> step)
		{
			if (!_nodes.Contains(node))
			{
				throw new InvalidOperationException("Attempting to look up the neighbors of a node that does not belong to a graph");
			}
			_edges.Stepper(e => step(e.End),
				node, node,
				None, None);
		}

		/// <summary>Clones this data structure.</summary>
		/// <returns>A clone of this data structure.</returns>
		public GraphSetOmnitree<T, TEquate, THash> Clone() => new(this);

		/// <inheritdoc/>
		public void Clear()
		{
			_nodes.Clear();
			_edges.Clear();
		}

		/// <inheritdoc/>
		public StepStatus StepperBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<T, StepStatus> =>
			_nodes.StepperBreak(step);

		/// <summary>Steps through all the edges in the graph.</summary>
		/// <param name="step">The action to perform on all the edges in the graph.</param>
		public void Edges(Action<(T, T)> step) => _edges.Stepper(edge => step((edge.Start, edge.End)));

		/// <summary>Steps through all the edges in the graph.</summary>
		/// <param name="step">The action to perform on all the edges in the graph.</param>
		/// <returns>The status of the stepper operation.</returns>
		public StepStatus EdgesBreak(Func<(T, T), StepStatus> step) => _edges.Stepper(edge => step((edge.Start, edge.End)));

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Gets the enumerator for the nodes in the graph.</summary>
		/// <returns>The enumerator for the nodes in the graph.</returns>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator() => _nodes.GetEnumerator();

		#endregion
	}

	/// <summary>Static helpers for <see cref="GraphMap{T, TEquate, THash}"/>.</summary>
	public static class GraphMap
	{
		/// <summary>Constructs a new <see cref="GraphMap{T, TEquate, THash}"/>.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <returns>The new constructed <see cref="GraphMap{T, TEquate, THash}"/>.</returns>
		public static GraphMap<T, SFunc<T, T, bool>, SFunc<T, int>> New<T>(
			Func<T, T, bool>? equate = null,
			Func<T, int>? hash = null) =>
			new(equate ?? Equate, hash ?? DefaultHash);
	}

	/// <summary>Stores a graph as a map and nested map (adjacency matrix).</summary>
	/// <typeparam name="T">The generic node type of this graph.</typeparam>
	public class GraphMap<T, TEquate, THash> : IGraph<T>
		where TEquate : struct, IFunc<T, T, bool>
		where THash : struct, IFunc<T, int>
	{
		internal MapHashLinked<(SetHashLinked<T, TEquate, THash> Incoming, SetHashLinked<T, TEquate, THash> Outgoing), T, TEquate, THash> _map;
		internal int _edges;

		#region Constructors

		/// <summary>Constructs a new GraphMap.</summary>
		/// <param name="equate">The equate delegate for the data structure to use.</param>
		/// <param name="hash">The hash function for the data structure to use.</param>
		public GraphMap(TEquate equate = default, THash hash = default)
		{
			_edges = 0;
			_map = new(equate, hash);
		}

		/// <summary>Constructor for cloning purposes.</summary>
		/// <param name="graph">The graph to clone.</param>
		internal GraphMap(GraphMap<T, TEquate, THash> graph)
		{
			_edges = graph._edges;
			_map = _map.Clone();
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
		public bool TryAdd(T node, out Exception? exception) =>
			_map.TryAdd(node, (new(_map.Equate, _map.Hash), new(_map.Equate, _map.Hash)), out exception);

		/// <summary>Adds an edge to the graph.</summary>
		/// <param name="start">The starting point of the edge.</param>
		/// <param name="end">The ending point of the edge.</param>
		public void Add(T start, T end)
		{
			if (!_map.Contains(start)) throw new ArgumentException(message: "Adding an edge to a non-existing starting node.");
			if (!_map.Contains(end)) throw new ArgumentException(message: "Adding an edge to a non-existing ending node.");

			//Commenting out the case below, as it should not ouccer
			// if (_map[start].Outgoing is null)
			// _map[start].Outgoing = new(_map.Equate, _map.Hash);

			_map[start].Outgoing.Add(end);
			_map[end].Incoming.Add(start);
			_edges++;
		}

		/// <summary>Removes a node from the graph.</summary>
		/// <param name="node">The node to remove from the graph.</param>
		/// <param name="exception">The exception that occurred if the remove failed.</param>
		/// <returns>True if the remove succeeded or false if not.</returns>
		public bool TryRemove(T node, out Exception? exception)
		{
			exception = null;
			foreach (var item in _map[node].Outgoing) TryRemove(node, item, out exception);
			foreach (var item in _map[node].Incoming) TryRemove(item, node, out exception);
			_map.TryRemove(node, out exception);
			return exception is null;
		}

		public bool TryRemove(T start, T end, out Exception? exception)
		{
			if (_map.Contains(start) && _map.Contains(end) && _map[start].Outgoing.Contains(end))
			{
				_map[start].Outgoing.Remove(end);
				_map[end].Incoming.Remove(start);
				_edges--;
				exception = null;
				return true;
			}
			exception = new InvalidOperationException("Removing a non-existing edge from the graph.");
			return false;
		}

		/// <summary>Checks for adjacency between two nodes.</summary>
		/// <param name="a">The first node of the adjacency check.</param>
		/// <param name="b">The second node fo the adjacency check.</param>
		/// <returns>True if ajacent. False if not.</returns>
		public bool Adjacent(T a, T b)
		{
			if(_map.Contains(a) && _map.Contains(b)) return _map[a].Outgoing.Contains(b);
			else return false;
		}
		/// <summary>Steps through all the **Outgoing** neighbors of a node.</summary>
		/// <param name="a">The node to step through the children of.</param>
		/// <param name="step">The action to perform on all the neighbors of the provided node.</param>
		public void Neighbors(T a, Action<T?> step)
		{
			foreach(var node in _map[a].Outgoing) step(node);
		}

		/// <inheritdoc/>
		public StepStatus StepperBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<T, StepStatus> =>
			_map.KeysBreak(step);

		/// <summary>Steps through all the edges in the <see cref="GraphMap{T}"/></summary>
		/// <param name="step">The action to perform on every edge in the graph.</param>
		public void Edges(Action<(T?, T?)> step) =>
			_map.Pairs(pair =>
				pair.Value.Outgoing.Stepper(b => step((pair.Key, b))));

		/// <summary>Steps through all the edges in the <see cref="GraphMap{T}"/></summary>
		/// <param name="step">The action to perform on every edge in the graph.</param>
		/// <returns>The status of the iteration.</returns>
		public StepStatus EdgesBreak(Func<(T?, T?), StepStatus> step) =>
			_map.PairsBreak(pair =>
				pair.Value.Outgoing.StepperBreak(b => step((pair.Key, b))));

		/// <summary>Makes a clone of this <see cref="GraphMap{T}"/>.</summary>
		/// <returns></returns>
		public GraphMap<T, TEquate, THash> Clone() => new(this);

		/// <summary>Gets an with all the nodes of th graph in it.</summary>
		/// <returns>An array with all the nodes of th graph in it.</returns>
		public T[] ToArray()
		{
			#warning Make a "K[] KeysToArray()" method on MapHashLinked
			T[] nodes = new T[_map.Count];
			int i = 0;
			_map.Keys(key => nodes[i++] = key);
			return nodes;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>
		/// Enumerates through the nodes of the graph
		/// </summary>
		/// <returns>Enumerator object</returns>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator() => _map.GetKeys().GetEnumerator();

		/// <summary>Clears this graph to an empty state.</summary>
		public void Clear()
		{
			_edges = 0;
			_map = new(_map.Equate, _map.Hash);
		}

		#endregion
	}

	/// <summary>
	/// A weighted graph data structure that stores nodes, edges with their corresponding weights. 
	/// </summary>
	/// <typeparam name="V">The generic node type of this graph.</typeparam>
	/// <typeparam name="W">The generic weight type of this graph.</typeparam>
	public interface IGraphWeighted<V, W> : IGraph<V>
	{
		#region Methods

		void IGraph<V>.Add(V start, V end) => Add(start, end, default);
		/// <summary>Adds a weighted edge to the graph </summary>
		/// <param name="start">The starting point of the edge to add</param>
		/// <param name="end">The ending point of the edge to add</param>
		/// <param name="weight">The weight of the edge</param>
		void Add(V start, V end, W? weight);
		/// <summary>Checks if b is adjacent to a.</summary>
		/// <param name="a">The starting point of the edge to check.</param>
		/// <param name="b">The ending point of the edge to check.</param>
		/// <param name="weight">The weight of the edge, if it exists.</param>
		/// <returns>True if b is adjacent to a; False if not</returns>
		bool Adjacent(V a, V b, out W? weight);

		/// <summary>Enumerates and returns all edges present in the graph</summary>
		/// <returns>Array of Tuple of nodes that represent an edge</returns>
		(V, V)[] EdgesToArray();

		/// <summary>Enumerates and returns all edges present in the graph</summary>
		/// <returns>Array of Tuple of nodes and weight that represent an edge</returns>
		(V, V, W)[] EdgesAndWeightsToArray();

		#endregion
	}

	/// <summary>Static helpers for <see cref="GraphWeightedMap{V, W, TEquate, THash}"/>.</summary>
	public static class GraphWeightedMap
	{
		/// <summary>Constructs a new <see cref="GraphWeightedMap{V, W, TEquate, THash}"/>.</summary>
		/// <typeparam name="V">The type of values stored in this data structure.</typeparam>
		/// <returns>The new constructed <see cref="GraphWeightedMap{V, W, TEquate, THash}"/>.</returns>
		public static GraphWeightedMap<V, W, SFunc<V, V, bool>, SFunc<V, int>> New<V, W>(
			Func<V, V, bool>? equate = null,
			Func<V, int>? hash = null) =>
			new(equate ?? Equate, hash ?? DefaultHash);
	}

	/// <summary>
	/// Implements a weighted graph. Implements a Dictionary of Nodes and edges
	/// </summary>
	/// <typeparam name="V">The generic node type of this graph.</typeparam>
	/// <typeparam name="W">The generic weight type of this graph.</typeparam>
	public class GraphWeightedMap<V, W, TEquate, THash> : IGraphWeighted<V, W>,
		DataStructure.IEquating<V, TEquate>,
		DataStructure.IHashing<V, THash>
		where TEquate : struct, IFunc<V, V, bool>
		where THash : struct, IFunc<V, int>
	{
		internal MapHashLinked<(MapHashLinked<W?, V, TEquate, THash> OutgoingEdges, SetHashLinked<V, TEquate, THash> IncomingNodes), V, TEquate, THash> _map;
		internal int _edges;

		#region Constructors

		public GraphWeightedMap(TEquate equate = default, THash hash = default)
		{
			_map = new(equate, hash);
			_edges = 0;
		}

		internal GraphWeightedMap(GraphWeightedMap<V, W, TEquate, THash> graph)
		{
			_edges = graph._edges;
			_map = graph._map.Clone();
		}

		#endregion

		#region Properties

		/// <inheritdoc/>
		public TEquate Equate => _map.Equate;

		/// <inheritdoc/>
		public THash Hash => _map.Hash;

		/// <inheritdoc/>
		public int EdgeCount => _edges;

		/// <inheritdoc/>
		public int NodeCount => _map.Count;

		#endregion

		#region Methods

		/// <summary>
		/// Tries to add a node. Returns false on success.
		/// </summary>
		/// <param name="value">The node to add</param>
		/// <param name="exception">The inner exception, in case of failure</param>
		/// <returns>boolean value indicating the success of process</returns>
		public bool TryAdd(V value, out Exception? exception)
		{
			#warning TODO: change this so that it does not allocate the new collections until necessary
			if (!_map.TryAdd(value, (new(Equate, Hash), new(Equate, Hash)), out Exception? childException))
			{
				exception = new ArgumentException(message: "Queried value already exists in graph", paramName: nameof(value), childException);
				return false;
			}
			else
			{
				exception = null;
				return true;
			}
		}

		/// <summary>Adds a weighted edge to the graph </summary>
		/// <param name="start">The starting point of the edge to add</param>
		/// <param name="end">The ending point of the edge to add</param>
		/// <param name="weight">The weight of the edge</param>
		public void Add(V start, V end, W? weight)
		{
			if (!_map.TryGet(start, out var outgoing)) throw new ArgumentException(message: "start Vertex must be present in graph before adding edge", paramName: nameof(start));
			if (!_map.TryGet(end, out var incoming)) throw new ArgumentException(message: "end Vertex must be present in graph before adding edge", paramName: nameof(end));
			outgoing.OutgoingEdges.Add(end, weight);
			incoming.IncomingNodes.Add(start);
			_edges++;
		}

		/// <summary>Checks if b is adjacent to a.</summary>
		/// <param name="a">The starting point of the edge to check.</param>
		/// <param name="b">The ending point of the edge to check.</param>
		/// <param name="weight">The weight of the edge, if it exists.</param>
		/// <returns>True if b is adjacent to a; False if not</returns>
		public bool Adjacent(V a, V b, out W? weight)
		{
			if (!_map.TryGet(a, out var outgoing)) throw new ArgumentException(message: "Vertex must be present in graph", paramName: nameof(a));
			if (!_map.Contains(b)) throw new ArgumentException(message: "Vertex must be present in graph", paramName: nameof(b));
			if (outgoing.OutgoingEdges.Contains(b))
			{
				weight = _map[a].OutgoingEdges[b];
				return true;
			}
			else
			{
				weight = default;
				return false;
			}
		}

		/// <summary>Checks if b is adjacent to a.</summary>
		/// <param name="a">The starting point of the edge to check.</param>
		/// <param name="b">The ending point of the edge to check.</param>
		/// <returns>True if b is adjacent to a; False if not</returns>
		public bool Adjacent(V a, V b) => Adjacent(a, b, out var _);

		/// <summary>Removes all edges and nodes</summary>
		public void Clear() => _map.Clear();

		/// <summary> Performs action via a delegate for every neighbour of a node </summary>
		/// <param name="a">The node to scan neighbours for</param>
		/// <param name="function">The delegate function to act on the node</param>
		public void Neighbors(V a, Action<V> function) => _map[a].OutgoingEdges.Keys(function);

		/// <summary>Removes any edge between the given nodes.</summary>
		/// <param name="start">The node on the start of the edge</param>
		/// <param name="end">The node on the end of the edge</param>
		public bool TryRemove(V start, V end, out Exception? exception)
		{
			exception = null;
			if (!_map.Contains(start)) exception = new ArgumentException(message: "Vertex must be present in graph", paramName: nameof(start));
			if (!_map.Contains(end)) exception = new ArgumentException(message: "Vertex must be present in graph", paramName: nameof(end));
			_map[start].OutgoingEdges.Remove(end);
			_map[end].IncomingNodes.Remove(start);
			_edges--;
			return exception is null;
		}

		#region Stepper and IEnumerable

		#region Stepper (nodes/verteces)

		/// <inheritdoc cref="DataStructure.Stepper_XML"/>
		public void Stepper<Step>(Step step = default)
			where Step : struct, IAction<V> =>
			StepperBreak<StepBreakFromAction<V, Step>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_XML"/>
		public void Stepper(Action<V> step) =>
			Stepper<SAction<V>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_XML"/>
		public StepStatus Stepper(Func<V, StepStatus> step) =>
			StepperBreak<StepBreakRuntime<V>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_XML"/>
		public StepStatus StepperBreak<Step>(Step step = default)
			where Step : struct, IFunc<V, StepStatus> =>
			_map.KeysBreak<Step>(step);

		#endregion

		#region Stepper (nodes/verteces)

		/// <inheritdoc cref="DataStructure.Stepper_XML"/>
		public void Edges<Step>(Step step = default)
			where Step : struct, IAction<(V, V)> =>
			EdgesBreak<StepBreakFromAction<(V, V), Step>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_XML"/>
		public void Edges(Action<(V, V)> step) =>
			Edges<SAction<(V, V)>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_XML"/>
		public StepStatus EdgesBreak(Func<(V, V), StepStatus> step) =>
			EdgesBreak<StepBreakRuntime<(V, V)>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_XML"/>
		public StepStatus EdgesBreak<Step>(Step step = default)
			where Step : struct, IFunc<(V, V), StepStatus>
		{
			EdgesStep<Step> step2 = new() { _step = step, };
			return _map.PairsBreak<EdgesStep<Step>>(step2);
		}

		internal struct EdgesStep<Step> : IFunc<((MapHashLinked<W?, V, TEquate, THash> OutgoingEdges, SetHashLinked<V, TEquate, THash> IncomingNodes), V), StepStatus>
			where Step : struct, IFunc<(V, V), StepStatus>
		{
			internal Step _step;

			public StepStatus Invoke(((MapHashLinked<W?, V, TEquate, THash> OutgoingEdges, SetHashLinked<V, TEquate, THash> IncomingNodes), V) a)
			{
				EdgesStep2<Step> step2 = new() { _a = a.Item2, _step = _step, };
				return a.Item1.OutgoingEdges.PairsBreak<EdgesStep2<Step>>(step2);
			}
		}

		internal struct EdgesStep2<Step> : IFunc<(W?, V), StepStatus>
			where Step : struct, IFunc<(V, V), StepStatus>
		{
			internal Step _step;
			internal V _a;

			public StepStatus Invoke((W?, V) a)
			{
				var edge = (_a, a.Item2);
				return _step.Invoke(edge);
			}
		}

		#endregion

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Enumerates through all the added nodes in the graph</summary>
		public System.Collections.Generic.IEnumerator<V> GetEnumerator() =>
			#warning TODO: optimize
			_map.Keys().ToArray().GetEnumerator() as System.Collections.Generic.IEnumerator<V>;

		#endregion

		/// <summary>
		/// Tries to remove a node. Returns true on success.
		/// </summary>
		/// <param name="node">The node to remove from the graph</param>
		/// <param name="exception">The inner exception in case of failure</param>
		/// <returns>boolean value indicating success of process</returns>
		public bool TryRemove(V node, out Exception? exception)
		{
			if (!_map.Contains(node))
			{
				exception = new ArgumentException("Node does not exist in graph", nameof(node));
				return false;
			}
			//Remove all edges e, where e=(x, deleteNode)
			foreach (var item in _map[node].IncomingNodes)
			{
				this.Remove(node, item);
			}
			//Remove all edges e, where e=(deleteNode, x)
			foreach (var item in _map[node].OutgoingEdges.Keys().ToArray())
			{
				this.Remove(node, item);
			}
			_map.Remove(node);
			exception = null;
			return true;
		}

		/// <summary>Clones the graph.</summary>
		/// <returns>A clone of the graph.</returns>
		public GraphWeightedMap<V, W, TEquate, THash> Clone() => new(this);

		/// <summary>Returns an array of nodes in this graph</summary>
		/// <returns>Array of nodes of this graph</returns>
		public V[] ToArray() => _map.Keys().ToArray();

		/// <inheritdoc/>
		public (V, V)[] EdgesToArray()
		{
			ListArray<(V, V)> edgelist=new();
			foreach(var (adjacencies, start) in _map.GetPairs())
			{
				foreach(var (_, end) in adjacencies.OutgoingEdges.GetPairs())
				{
					edgelist.Add((start, end));
				}
			}
			return edgelist.ToArray();
		}

		/// <inheritdoc/>
		public (V, V, W)[] EdgesAndWeightsToArray()
		{
			ListArray<(V, V, W)> edgelist = new();
			foreach(var (adjacencies, start) in _map.GetPairs())
			{
				foreach(var (weight, end) in adjacencies.OutgoingEdges.GetPairs())
				{
					edgelist.Add((start, end, weight));
				}
			}
			return edgelist.ToArray();
		}

		#endregion
	}
}