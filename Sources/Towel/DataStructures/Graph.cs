using System;
using static Towel.Statics;

namespace Towel.DataStructures
{
	/// <summary>Static helpers for <see cref="IGraph{T}"/>.</summary>
	public static class Graph
	{
		#region Extension Methods

		/// <summary>Adds an edge to a graph.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <param name="graph">The data structure to add the value to.</param>
		/// <param name="a">The start of the edge.</param>
		/// <param name="b">The end of the edge.</param>
		public static void Add<T>(this IGraph<T> graph, T a, T b)
		{
			var (success, exception) = graph.TryAdd(a, b);
			if (!success)
			{
				throw exception ?? new ArgumentException($"{nameof(Add)} failed but the {nameof(exception)} is null");
			}
		}

		/// <summary>Removes a value from a graph.</summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <param name="graph">The data structure to remove the value from.</param>
		/// <param name="value">The value to be removed.</param>
		public static void Remove<T>(this IGraph<T> graph, T value)
		{
			var (success, exception) = graph.TryRemove(value);
			if (!success)
			{
				throw exception ?? new ArgumentException($"{nameof(Remove)} failed but the {nameof(exception)} is null");
			}
		}

		/// <summary>Removes an edge from a graph.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <param name="graph">The data structure to remove the value from.</param>
		/// <param name="a">The start of the edge.</param>
		/// <param name="b">The end of the edge.</param>
		public static void Remove<T>(this IGraph<T> graph, T a, T b)
		{
			var (success, exception) = graph.TryRemove(a, b);
			if (!success)
			{
				throw exception ?? new ArgumentException($"{nameof(Remove)} failed but the {nameof(exception)} is null");
			}
		}

		/// <summary>Invokes a function on every edge in the graph.</summary>
		/// <typeparam name="T">The type of values stored in this graph.</typeparam>
		/// <param name="graph">The graph to traverse the edges of.</param>
		/// <param name="step">The function on every edge in the graph.</param>
		public static void Edges<T>(this IGraph<T> graph, Action<(T, T)> step)
		{
			if (step is null) throw new ArgumentNullException(nameof(step));
			graph.Edges<T, SAction<(T, T)>>(step);
		}

		/// <summary>Invokes a function on every edge in the graph.</summary>
		/// <typeparam name="T">The type of values stored in this graph.</typeparam>
		/// <typeparam name="TStep">The type of the step function.</typeparam>
		/// <param name="graph">The graph to traverse the edges of.</param>
		/// <param name="step">The function on every edge in the graph.</param>
		public static void Edges<T, TStep>(this IGraph<T> graph, TStep step = default)
			where TStep : struct, IAction<(T, T)> =>
			graph.EdgesBreak<StepBreakFromAction<(T, T), TStep>>(step);

		/// <summary>Invokes a function on every edge in the graph.</summary>
		/// <typeparam name="T">The type of values stored in this graph.</typeparam>
		/// <param name="graph">The graph to traverse the edges of.</param>
		/// <param name="step">The function on every edge in the graph.</param>
		/// <returns>The status of iteration.</returns>
		public static StepStatus EdgesBreak<T>(this IGraph<T> graph, Func<(T, T), StepStatus> step)
		{
			if (step is null) throw new ArgumentNullException(nameof(step));
			return graph.EdgesBreak<SFunc<(T, T), StepStatus>>(step);
		}

		#endregion
	}

	/// <summary>A graph data structure that stores nodes and edges.</summary>
	/// <typeparam name="T">The generic node type to store in the graph.</typeparam>
	public interface IGraph<T> : IDataStructure<T>,
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

		/// <summary>Checks if an edge exists from <paramref name="a"/> to <paramref name="b"/>.</summary>
		/// <param name="a">The start of the edge.</param>
		/// <param name="b">The end of the edge.</param>
		/// <returns>True if an edge exists from <paramref name="a"/> to <paramref name="b"/>; False if not</returns>
		bool Adjacent(T a, T b);

		/// <summary>Gets all the nodes adjacent to a and performs the provided delegate on each.</summary>
		/// <param name="a">The node to find all the adjacent node to.</param>
		/// <param name="function">The delegate to perform on each adjacent node to a.</param>
		void Neighbors(T a, Action<T> function);
		/// <summary>Gets enumerator of all neighbours of a node</summary>
		/// <param name="node">The node to get neighbours of</param>
		/// <returns>IEnumerable of all neighbours of the given node</returns>
		System.Collections.Generic.IEnumerable<T> GetNeighbours(T node);

		/// <summary>Adds an edge to the graph starting at a and ending at b.</summary>
		/// <param name="start">The stating point of the edge to add.</param>
		/// <param name="end">The ending point of the edge to add.</param>
		/// <returns>
		/// <para>- Success: true if the edge was added or false if not</para>
		/// <para>- Exception: the exception that occured if the add failed</para>
		/// </returns>
		(bool Success, Exception? Exception) TryAdd(T start, T end);

		/// <summary>Removes an edge from the graph.</summary>
		/// <param name="start">The starting point of the edge to remove.</param>
		/// <param name="end">The ending point of the edge to remove.</param>
		/// <returns>
		/// <para>- Success: true if the edge was removed or false if not</para>
		/// <para>- Exception: the exception that occured if the remove failed</para>
		/// </returns>
		(bool Success, Exception? Exception) TryRemove(T start, T end);

		/// <summary>Invokes a function on every edge in the graph.</summary>
		/// <typeparam name="TStep">The type of the step function.</typeparam>
		/// <param name="step">The function to perform on every edge in the graph.</param>
		/// <returns>The status of the traversal.</returns>
		StepStatus EdgesBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<(T, T), StepStatus>;

		#endregion
	}

	/// <summary>Static helpers for <see cref="GraphSetOmnitree{T, TEquate, THash}"/>.</summary>
	public static class GraphSetOmnitree
	{
		#region Extension Methods

		/// <summary>Constructs a new <see cref="GraphSetOmnitree{T, TEquate, THash}"/>.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <returns>The new constructed <see cref="GraphSetOmnitree{T, TEquate, THash}"/>.</returns>
		public static GraphSetOmnitree<T, SFunc<T, T, bool>, SFunc<T, int>> New<T>(
			Func<T, T, bool>? equate = null,
			Func<T, int>? hash = null) =>
			new(equate ?? Equate, hash ?? DefaultHash);

		#endregion
	}

	/// <summary>Stores the graph as a set-hash of nodes and quadtree of edges.</summary>
	/// <typeparam name="T">The generic type of this data structure.</typeparam>
	/// <typeparam name="TEquate">The type of function for quality checking <typeparamref name="T"/> values.</typeparam>
	/// <typeparam name="THash">The type of function for hashing <typeparamref name="T"/> values.</typeparam>
	public class GraphSetOmnitree<T, TEquate, THash> : IGraph<T>,
		ICloneable<GraphSetOmnitree<T, TEquate, THash>>,
		DataStructure.IEquating<T, TEquate>,
		DataStructure.IHashing<T, THash>
		where TEquate : struct, IFunc<T, T, bool>
		where THash : struct, IFunc<T, int>
	{
		internal SetHashLinked<T, TEquate, THash> _nodes;
		internal OmnitreePointsLinked<Edge, T, T> _edges;

		#region Nested Types

		/// <summary>Represents an edge in a graph.</summary>
		internal class Edge
		{
			/// <summary>The starting node of the edge.</summary>
			internal T Start { get; set; }
			/// <summary>The ending node of the edge.</summary>
			internal T End { get; set; }

			internal Edge(T start, T end)
			{
				Start = start;
				End = end;
			}
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

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryAdd(T node)
		{
			var (success, exception) = _nodes.TryAdd(node);
			if (!success)
			{
				return (false, new ArgumentException(paramName: nameof(node), message: "Attempting to add an already existing node to a graph.", innerException: exception));
			}
			return (true, null);
		}

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryAdd(T start, T end)
		{
			if (!_nodes.Contains(start))
			{
				return (false, new ArgumentException("Adding an edge to a graph from a node that does not exists"));
			}
			if (!_nodes.Contains(end))
			{
				return (false, new ArgumentException("Adding an edge to a graph to a node that does not exists"));
			}
			Exception? exception = null;
			_edges.Stepper(
				e => { exception = new ArgumentException("Adding an edge to a graph that already exists"); return Break; },
				start, start, end, end);
			if (exception is not null)
			{
				return (false, exception);
			}
			_edges.Add(new Edge(start, end));
			return (true, null);
		}

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryRemove(T node)
		{
			var (success, exception) = _nodes.TryRemove(node);
			if (!success)
			{
				return (false, new ArgumentException("Removing a node from a graph from a node that does not exists", innerException: exception));
			}
			_edges.Remove(node, node, None, None);
			_edges.Remove(None, None, node, node);
			return (true, null);
		}

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryRemove(T start, T end)
		{
			if (!_nodes.Contains(start))
			{
				return (false, new ArgumentException("Removing an edge from a graph from a node that does not exists"));
			}
			if (!_nodes.Contains(end))
			{
				return (false, new ArgumentException("Removing an edge from a graph to a node that does not exists"));
			}
			#warning Add the number of values removed in Omnitree Remove method (then you can use the return count rather than capturing the exeption)
			Exception? exception = null;
			_edges.Stepper(
				edge => { exception = new ArgumentException("Removing a non-existing edge in a graph"); return Break; },
				start, start, end, end);
			if (exception is not null)
			{
				return (false, exception);
			}
			_edges.Remove(start, start, end, end);
			return (true, null);
		}

		/// <inheritdoc/>
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

		/// <inheritdoc/>
		public void Neighbors(T node, Action<T> step)
		{
			if (!_nodes.Contains(node))
			{
				throw new InvalidOperationException("Attempting to look up the neighbors of a node that does not belong to a graph");
			}
			_edges.Stepper(e => step(e.End),
				node, node,
				None, None);
		}

		/// <inheritdoc/>
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

		/// <inheritdoc/>
		public StepStatus EdgesBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<(T, T), StepStatus> =>
			#warning TODO: optimize this (structs vs delegate)
			_edges.StepperBreak(edge => step.Invoke((edge.Start, edge.End)));

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <inheritdoc/>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator() => _nodes.GetEnumerator();
		/// <summary>Gets enumerator of all neighbours of a node</summary>
		/// <param name="node">The node to get neighbours of</param>
		/// <returns>IEnumerable of all neighbours of the given node</returns>
		public System.Collections.Generic.IEnumerable<T> GetNeighbours(T node)
		{
			ListArray<T> NodeList = new();
			Neighbors(node, n => NodeList.Add(n));
			return NodeList;
		}

		/// <inheritdoc/>
		public T[] ToArray() => _nodes.ToArray();

		#endregion
	}

	/// <summary>Static helpers for <see cref="GraphMap{T, TEquate, THash}"/>.</summary>
	public static class GraphMap
	{
		#region Extension Methods

		/// <summary>Constructs a new <see cref="GraphMap{T, TEquate, THash}"/>.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <returns>The new constructed <see cref="GraphMap{T, TEquate, THash}"/>.</returns>
		public static GraphMap<T, SFunc<T, T, bool>, SFunc<T, int>> New<T>(
			Func<T, T, bool>? equate = null,
			Func<T, int>? hash = null) =>
			new(equate ?? Equate, hash ?? DefaultHash);

		#endregion
	}

	/// <summary>Stores a graph as a map and nested map (adjacency matrix).</summary>
	/// <typeparam name="T">The generic node type of this graph.</typeparam>
	/// <typeparam name="TEquate">The type of the equate function.</typeparam>
	/// <typeparam name="THash">The type of the hash function.</typeparam>
	public class GraphMap<T, TEquate, THash> : IGraph<T>,
		ICloneable<GraphMap<T, TEquate, THash>>,
		DataStructure.IEquating<T, TEquate>,
		DataStructure.IHashing<T, THash>
		where TEquate : struct, IFunc<T, T, bool>
		where THash : struct, IFunc<T, int>
	{
		internal MapHashLinked<(SetHashLinked<T, TEquate, THash> Incoming, SetHashLinked<T, TEquate, THash> Outgoing), T, TEquate, THash> _map;
		internal int _edgeCount;

		#region Constructors

		/// <summary>Constructs a new GraphMap.</summary>
		/// <param name="equate">The equate delegate for the data structure to use.</param>
		/// <param name="hash">The hash function for the data structure to use.</param>
		public GraphMap(TEquate equate = default, THash hash = default)
		{
			_edgeCount = 0;
			_map = new(equate, hash);
		}

		/// <summary>Constructor for cloning purposes.</summary>
		/// <param name="graph">The graph to clone.</param>
		internal GraphMap(GraphMap<T, TEquate, THash> graph)
		{
			_edgeCount = graph._edgeCount;
			_map = graph._map.Clone();
		}

		#endregion

		#region Properties

		/// <inheritdoc/>
		public TEquate Equate => _map.Equate;

		/// <inheritdoc/>
		public THash Hash => _map.Hash;

		/// <inheritdoc/>
		public int EdgeCount => _edgeCount;

		/// <inheritdoc/>
		public int NodeCount => _map.Count;

		#endregion

		#region Methods

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryAdd(T node)
		{
			var (success, exception) = _map.TryAdd(node, (new(Equate, Hash), new(Equate, Hash)));
			if (!success)
			{
				return (false, new ArgumentException(paramName: nameof(node), message: "Adding an allready existing node to a graph.", innerException: exception));
			}
			return (true, null);
		}

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryAdd(T start, T end)
		{
			if (!_map.Contains(start))
			{
				return (false, new ArgumentException(message: "Adding an edge to a non-existing starting node."));
			}
			if (!_map.Contains(end))
			{
				return (false, new ArgumentException(message: "Adding an edge to a non-existing ending node."));
			}

			//Commenting out the case below, as it should not ouccer
			// if (_map[start].Outgoing is null)
			// _map[start].Outgoing = new(_map.Equate, _map.Hash);

			_map[start].Outgoing.Add(end);
			_map[end].Incoming.Add(start);
			_edgeCount++;
			return (true, null);
		}

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryRemove(T node)
		{
			if (!_map.Contains(node))
			{
				return (false, new ArgumentException(paramName: nameof(node), message: "Removing a non-existing node from a graph."));
			}
			foreach (var item in _map[node].Outgoing)
			{
				this.Remove(node, item);
			}
			foreach (var item in _map[node].Incoming)
			{
				this.Remove(node, item);
			}
			_map.Remove(node);
			return (true, null);
		}

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryRemove(T start, T end)
		{
			if (_map.Contains(start) && _map.Contains(end) && _map[start].Outgoing.Contains(end))
			{
				_map[start].Outgoing.Remove(end);
				_map[end].Incoming.Remove(start);
				_edgeCount--;
				return (true, null);
			}
			return (false, new InvalidOperationException("Removing a non-existing edge from the graph."));
		}

		/// <inheritdoc/>
		public bool Adjacent(T a, T b)
		{
			if (_map.Contains(a) && _map.Contains(b)) return _map[a].Outgoing.Contains(b);
			else return false;
		}

		/// <inheritdoc/>
		public void Neighbors(T a, Action<T> step)
		{
			foreach (var node in _map[a].Outgoing) step(node);
		}

		/// <inheritdoc/>
		public StepStatus StepperBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<T, StepStatus> =>
			_map.KeysBreak(step);

		/// <inheritdoc/>
		public StepStatus EdgesBreak<TStep>(TStep step)
			where TStep : struct, IFunc<(T, T), StepStatus> =>
			_map.PairsBreak(pair =>
				#warning TODO: use structs instead of delegates
				pair.Value.Outgoing.StepperBreak(b => step.Invoke((pair.Key, b))));

		/// <inheritdoc/>
		public GraphMap<T, TEquate, THash> Clone() => new(this);

		/// <inheritdoc/>
		public T[] ToArray()
		{
			#warning TODO: Make a "K[] KeysToArray()" method on MapHashLinked
			T[] nodes = new T[_map.Count];
			int i = 0;
			_map.Keys(key => nodes[i++] = key);
			return nodes;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <inheritdoc/>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator() => _map.GetKeys().GetEnumerator();

		/// <inheritdoc/>
		public System.Collections.Generic.IEnumerable<T> GetNeighbours(T node) => _map[node].Outgoing;

		/// <inheritdoc/>
		public void Clear()
		{
			_edgeCount = 0;
			_map = new(_map.Equate, _map.Hash);
		}

		#endregion
	}

	/// <summary>A weighted graph data structure that stores nodes, edges with their corresponding weights.</summary>
	/// <typeparam name="T">The generic node type of this graph.</typeparam>
	/// <typeparam name="W">The generic weight type of this graph.</typeparam>
	public interface IGraphWeighted<T, W> : IGraph<T>
	{
		#region Methods

		(bool Success, Exception? Exception) IGraph<T>.TryAdd(T start, T end) => TryAdd(start, end, default);

		/// <summary>Adds a weighted edge to the graph </summary>
		/// <param name="start">The starting point of the edge to add</param>
		/// <param name="end">The ending point of the edge to add</param>
		/// <param name="weight">The weight of the edge</param>
		(bool Success, Exception? Exception) TryAdd(T start, T end, W? weight);

		/// <summary>Checks if b is adjacent to a.</summary>
		/// <param name="a">The starting point of the edge to check.</param>
		/// <param name="b">The ending point of the edge to check.</param>
		/// <param name="weight">The weight of the edge, if it exists.</param>
		/// <returns>True if b is adjacent to a; False if not</returns>
		bool Adjacent(T a, T b, out W? weight);

		/// <summary>Gets the weight of an edge.</summary>
		/// <param name="a">The starting node of the edge.</param>
		/// <param name="b">The ending node of the edge.</param>
		/// <returns>The weight of the edge.</returns>
		W GetWeight(T a, T b);

		/// <summary>Enumerates and returns all edges present in the graph</summary>
		/// <returns>Array of Tuple of nodes that represent an edge</returns>
		(T, T)[] EdgesToArray();

		/// <summary>Enumerates and returns all edges present in the graph</summary>
		/// <returns>Array of Tuple of nodes and weight that represent an edge</returns>
		(T, T, W?)[] EdgesAndWeightsToArray();

		#endregion
	}

	/// <summary>Static helpers for <see cref="IGraphWeighted{T, W}"/>.</summary>
	public static class GraphWeighted
	{
		#region Extension Methods

		/// <summary>Adds an edge to a weighted graph</summary>
		/// <typeparam name="T">The generic node type of this graph.</typeparam>
		/// <typeparam name="W">The generic weight type of this graph.</typeparam>
		/// <param name="graph">The data structure to add the value to.</param>
		/// <param name="start">The starting point of the edge.</param>
		/// <param name="end">The ending point of the edge.</param>
		/// <param name="weight">The weight of the edge.</param>
		public static void Add<T, W>(this IGraphWeighted<T, W> graph, T start, T end, W? weight)
		{
			var (success, exception) = graph.TryAdd(start, end, weight);
			if (!success)
			{
				throw exception ?? new ArgumentException(message: $"{nameof(Add)} failed but the {nameof(exception)} is null", innerException: exception);
			}
		}

		#endregion
	}

	/// <summary>Static helpers for <see cref="GraphWeightedMap{T, W, TEquate, THash}"/>.</summary>
	public static class GraphWeightedMap
	{
		#region Extension Methods

		/// <summary>Constructs a new <see cref="GraphWeightedMap{V, W, TEquate, THash}"/>.</summary>
		/// <typeparam name="T">The type of values stored in this data structure.</typeparam>
		/// <typeparam name="W">The type of weight stored in the edges in this data structure.</typeparam>
		/// <returns>The new constructed <see cref="GraphWeightedMap{V, W, TEquate, THash}"/>.</returns>
		public static GraphWeightedMap<T, W, SFunc<T, T, bool>, SFunc<T, int>> New<T, W>(
			Func<T, T, bool>? equate = null,
			Func<T, int>? hash = null) =>
			new(equate ?? Equate, hash ?? DefaultHash);

		#endregion
	}

	/// <summary>Implements a weighted graph. Implements a Dictionary of Nodes and edges.</summary>
	/// <typeparam name="T">The generic node type of this graph.</typeparam>
	/// <typeparam name="W">The generic weight type of this graph.</typeparam>
	/// <typeparam name="TEquate">The type of function for quality checking <typeparamref name="T"/> values.</typeparam>
	/// <typeparam name="THash">The type of function for hashing <typeparamref name="T"/> values.</typeparam>
	public class GraphWeightedMap<T, W, TEquate, THash> : IGraphWeighted<T, W>,
		ICloneable<GraphWeightedMap<T, W, TEquate, THash>>,
		DataStructure.IEquating<T, TEquate>,
		DataStructure.IHashing<T, THash>
		where TEquate : struct, IFunc<T, T, bool>
		where THash : struct, IFunc<T, int>
	{
		internal MapHashLinked<(MapHashLinked<W?, T, TEquate, THash> OutgoingEdges, SetHashLinked<T, TEquate, THash> IncomingNodes), T, TEquate, THash> _map;
		internal int _edgeCount;

		#region Constructors

		/// <summary>Constructs a new graph.</summary>
		/// <param name="equate">The function for equality checking <typeparamref name="T"/> values.</param>
		/// <param name="hash">The function for hashing <typeparamref name="T"/> values.</param>
		public GraphWeightedMap(TEquate equate = default, THash hash = default)
		{
			_map = new(equate, hash);
			_edgeCount = 0;
		}

		internal GraphWeightedMap(GraphWeightedMap<T, W, TEquate, THash> graph)
		{
			_edgeCount = graph._edgeCount;
			_map = graph._map.Clone();
		}

		#endregion

		#region Properties

		/// <inheritdoc/>
		public TEquate Equate => _map.Equate;

		/// <inheritdoc/>
		public THash Hash => _map.Hash;

		/// <inheritdoc/>
		public int EdgeCount => _edgeCount;

		/// <inheritdoc/>
		public int NodeCount => _map.Count;

		#endregion

		#region Methods

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryAdd(T value)
		{
			#warning TODO: change this so that it does not allocate the new collections until necessary
			var (success, exception) = _map.TryAdd(value, (new(Equate, Hash), new(Equate, Hash)));
			if (!success)
			{
				return (false, new ArgumentException(message: "Queried value already exists in graph", paramName: nameof(value), exception));
			}
			else
			{
				return (true, null);
			}
		}

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryAdd(T start, T end, W? weight)
		{
			var x = _map.TryGet(start);
			if (!x.Success)
			{
				return (false, new ArgumentException(message: "start Vertex must be present in graph before adding edge", paramName: nameof(start)));
			}
			var y = _map.TryGet(end);
			if (!y.Success)
			{
				return (false, new ArgumentException(message: "end Vertex must be present in graph before adding edge", paramName: nameof(end)));
			}
			x.Value.OutgoingEdges.Add(end, weight);
			y.Value.IncomingNodes.Add(start);
			_edgeCount++;
			return (true, null);
		}

		/// <inheritdoc/>
		public bool Adjacent(T a, T b, out W? weight)
		{
			var (success, exception, value) = _map.TryGet(a);
			if (!success)
			{
				throw new ArgumentException(message: "Vertex must be present in graph", paramName: nameof(a), innerException: exception);
			}
			if (!_map.Contains(b)) throw new ArgumentException(message: "Vertex must be present in graph", paramName: nameof(b));
			if (value.OutgoingEdges.Contains(b))
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

		/// <inheritdoc/>
		public bool Adjacent(T a, T b) => Adjacent(a, b, out var _);

		/// <inheritdoc/>
		public void Clear() => _map.Clear();

		/// <inheritdoc/>
		public void Neighbors(T a, Action<T> function) => _map[a].OutgoingEdges.Keys(function);

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryRemove(T start, T end)
		{
			if (!_map.Contains(start)) return (false, new ArgumentException(message: "Vertex must be present in graph", paramName: nameof(start)));
			if (!_map.Contains(end)) return (false, new ArgumentException(message: "Vertex must be present in graph", paramName: nameof(end)));
			_map[start].OutgoingEdges.Remove(end);
			_map[end].IncomingNodes.Remove(start);
			_edgeCount--;
			return (true, null);
		}

		/// <inheritdoc/>
		public StepStatus StepperBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<T, StepStatus> =>
			_map.KeysBreak<TStep>(step);

		/// <inheritdoc/>
		public StepStatus EdgesBreak<TStep>(TStep step = default)
			where TStep : struct, IFunc<(T, T), StepStatus>
		{
			EdgesStep<TStep> step2 = new() { _step = step, };
			return _map.PairsBreak<EdgesStep<TStep>>(step2);
		}

		internal struct EdgesStep<TStep> : IFunc<((MapHashLinked<W?, T, TEquate, THash> OutgoingEdges, SetHashLinked<T, TEquate, THash> IncomingNodes), T), StepStatus>
			where TStep : struct, IFunc<(T, T), StepStatus>
		{
			internal TStep _step;

			public StepStatus Invoke(((MapHashLinked<W?, T, TEquate, THash> OutgoingEdges, SetHashLinked<T, TEquate, THash> IncomingNodes), T) a)
			{
				EdgesStep2<TStep> step2 = new() { _a = a.Item2, _step = _step, };
				return a.Item1.OutgoingEdges.PairsBreak<EdgesStep2<TStep>>(step2);
			}
		}

		internal struct EdgesStep2<TStep> : IFunc<(W?, T), StepStatus>
			where TStep : struct, IFunc<(T, T), StepStatus>
		{
			internal TStep _step;
			internal T _a;

			public StepStatus Invoke((W?, T) a)
			{
				var edge = (_a, a.Item2);
				return _step.Invoke(edge);
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <inheritdoc/>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator() =>
			_map.GetKeys().GetEnumerator();

		/// <inheritdoc/>
		public (bool Success, Exception? Exception) TryRemove(T node)
		{
			if (!_map.Contains(node))
			{
				return (false, new ArgumentException("Node does not exist in graph", nameof(node)));
			}
			foreach (var item in _map[node].IncomingNodes)
			{
				this.Remove(node, item);
			}
			foreach (var item in _map[node].OutgoingEdges.GetKeys())
			{
				this.Remove(node, item);
			}
			_map.Remove(node);
			return (true, null);
		}

		/// <inheritdoc/>
		public GraphWeightedMap<T, W, TEquate, THash> Clone() => new(this);

		/// <inheritdoc/>
		public T[] ToArray() => _map.KeysToArray();

		/// <inheritdoc/>
		public (T, T)[] EdgesToArray()
		{
			(T, T)[] array = new(T, T)[_edgeCount];
			int i = 0;
			foreach(var (adjacencies, start) in _map.GetPairs())
			{
				foreach(var (_, end) in adjacencies.OutgoingEdges.GetPairs())
				{
					array[i++] = (start, end);
				}
			}
			return array;
		}

		/// <inheritdoc/>
		public (T, T, W?)[] EdgesAndWeightsToArray()
		{
			(T, T, W?)[] array = new (T, T, W?)[_edgeCount];
			int i = 0;
			foreach(var (adjacencies, start) in _map.GetPairs())
			{
				foreach(var (weight, end) in adjacencies.OutgoingEdges.GetPairs())
				{
					array[i++] = (start, end, weight);
				}
			}
			return array;
		}

		/// <inheritdoc/>
		public System.Collections.Generic.IEnumerable<T> GetNeighbours(T node) => _map[node].OutgoingEdges.GetKeys();

		/// <inheritdoc/>
		public W GetWeight(T a, T b) => _map[a].OutgoingEdges[b] ?? throw new ArgumentException("No edge found between the queired nodes");

		#endregion
	}
}