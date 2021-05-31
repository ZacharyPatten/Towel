using System;
using System.Collections;
using System.Collections.Generic;
using static Towel.Statics;

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
		void Neighbors(T a, Action<T> function);
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
		void Stepper(Action<T?, T?> step);
		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		StepStatus Stepper(Func<T, T, StepStatus> step);

		#endregion
	}

	/// <summary>Stores the graph as a set-hash of nodes and quadtree of edges.</summary>
	/// <typeparam name="T">The generic type of this data structure.</typeparam>
	public class GraphSetOmnitree<T> : IGraph<T>
	{
		internal SetHashLinked<T> _nodes;
		internal OmnitreePointsLinked<Edge, T, T> _edges;

		#region Edge

		/// <summary>Represents an edge in a graph.</summary>
		public class Edge
		{
			/// <summary>The starting node of the edge.</summary>
			public T Start { get; internal set; }
			/// <summary>The ending node of the edge.</summary>
			public T End { get; internal set; }
		}

		#endregion

		#region Constructors

		/// <summary>This constructor is for cloning purposes.</summary>
		/// <param name="graph">The graph to construct a clone of.</param>
		internal GraphSetOmnitree(GraphSetOmnitree<T> graph)
		{
			_edges = graph._edges.Clone();
			_nodes = graph._nodes.Clone();
		}

		/// <summary>Constructs a new GraphSetOmnitree.</summary>
		/// <param name="equate">The equate delegate for the data structure to use.</param>
		/// <param name="compare">The compare delegate for the data structure to use.</param>
		/// <param name="hash">The hash delegate for the datastructure to use.</param>
		public GraphSetOmnitree(
			Func<T, T, bool>? equate = null,
			Func<T, T, CompareResult>? compare = null,
			Func<T, int>? hash = null)
		{
			equate ??= Equate;
			compare ??= Compare;
			hash ??= DefaultHash;

			_nodes = new SetHashLinked<T>(equate, hash);
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

		/// <summary>Gets the number of edges in the graph.</summary>
		public int EdgeCount => _edges.Count;

		/// <summary>Gets the number of nodes in the graph.</summary>
		public int NodeCount => _nodes.Count;

		#endregion

		#region Methods

		/// <summary>Tries to add a node to the graph.</summary>
		/// <param name="node">The node to add to the graph.</param>
		/// <param name="exception">The exception that occurred if the add failed.</param>
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
		/// <param name="exception">The exception that occurred if the remove failed.</param>
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
		public void Remove(T start, T end)
		{
			if (!_nodes.Contains(start))
			{
				throw new ArgumentException("Removing an edge to a graph from a node that does not exists");
			}
			if (!_nodes.Contains(end))
			{
				throw new ArgumentException("Removing an edge to a graph to a node that does not exists");
			}
			_edges.Stepper(
				(Edge e) => throw new ArgumentException("Removing a non-existing edge in a graph"),
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

		/// <summary>Clones this data structure.</summary>
		/// <returns>A clone of this data structure.</returns>
		public IDataStructure<T> Clone() => new GraphSetOmnitree<T>(this);

		/// <summary>Returns this data structure to an empty state.</summary>
		public void Clear()
		{
			_nodes.Clear();
			_edges.Clear();
		}

		/// <summary>Steps through all the nodes in the graph.</summary>
		/// <param name="step">The action to perform on all the nodes in the graph.</param>
		public void Stepper(Action<T> step) => _nodes.Stepper(step);

		/// <summary>Steps through all the nodes in the graph.</summary>
		/// <param name="step">The action to perform on all the nodes in the graph.</param>
		/// <returns>The status of the stepper operation.</returns>
		public StepStatus Stepper(Func<T, StepStatus> step) => _nodes.Stepper(step);

		/// <summary>Steps through all the edges in the graph.</summary>
		/// <param name="step">The action to perform on all the edges in the graph.</param>
		public void Stepper(Action<T, T> step) => _edges.Stepper(edge => step(edge.Start, edge.End));

		/// <summary>Steps through all the edges in the graph.</summary>
		/// <param name="step">The action to perform on all the edges in the graph.</param>
		/// <returns>The status of the stepper operation.</returns>
		public StepStatus Stepper(Func<T, T, StepStatus> step) => _edges.Stepper(edge => step(edge.Start, edge.End));

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Gets the enumerator for the nodes in the graph.</summary>
		/// <returns>The enumerator for the nodes in the graph.</returns>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator() => _nodes.GetEnumerator();

		#endregion
	}

	/// <summary>Stores a graph as a map and nested map (adjacency matrix).</summary>
	/// <typeparam name="T">The generic node type of this graph.</typeparam>
	public class GraphMap<T> : IGraph<T>
	{
		internal MapHashLinked<(SetHashLinked<T> Incoming, SetHashLinked<T> Outgoing), T> _map;
		internal int _edges;

		#region Constructors

		/// <summary>Constructs a new GraphMap.</summary>
		public GraphMap() : this(Equate, DefaultHash) { }

		/// <summary>Constructs a new GraphMap.</summary>
		/// <param name="equate">The equate delegate for the data structure to use.</param>
		/// <param name="hash">The hash function for the data structure to use.</param>
		public GraphMap(Func<T, T, bool> equate, Func<T, int> hash)
		{
			_edges = 0;
			_map = new(equate, hash);
		}

		/// <summary>Constructor for cloning purposes.</summary>
		/// <param name="graphToClone">The graph to clone.</param>
		internal GraphMap(GraphMap<T> graphToClone)
		{
			_edges = graphToClone._edges;
			_map = new(graphToClone._map.Equate, graphToClone._map.Hash);
			Stepper((T node) => this.Add(node));
			Stepper((T a, T b) => Add(a, b));
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
		public bool TryAdd(T node, out Exception? exception)
		{

			return _map.TryAdd(node, (new SetHashLinked<T>(_map.Equate, _map.Hash), new SetHashLinked<T>(_map.Equate, _map.Hash)), out exception);
		}

		/// <summary>Adds an edge to the graph.</summary>
		/// <param name="start">The starting point of the edge.</param>
		/// <param name="end">The ending point of the edge.</param>
		public void Add(T start, T end)
		{
			if (!_map.Contains(start))
				throw new ArgumentException(message: "Adding an edge to a non-existing starting node.");
			if (!_map.Contains(end))
				throw new ArgumentException(message: "Adding an edge to a non-existing ending node.");
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
		public bool TryRemove(T node, out Exception exception)
		{
			exception = new();
			try
			{
				foreach (var item in _map[node].Outgoing) Remove(node, item);
				foreach (var item in _map[node].Incoming) Remove(item, node);
				_map.Remove(node);
				return true;
			}
			catch (Exception e)
			{
				exception = e;
				return false;
			}
		}

		/// <summary>Removes an edge from the graph.</summary>
		/// <param name="start">The starting point of the edge to remove.</param>
		/// <param name="end">The ending point of the edge to remove.</param>
		public void Remove(T start, T end)
		{
			if(_map.Contains(start) && _map.Contains(end) && _map[start].Outgoing.Contains(end))
			{
				_map[start].Outgoing.Remove(end);
				_map[end].Incoming.Remove(start);
				_edges--;
			}
			else throw new InvalidOperationException("Removing a non-existing edge from the graph.");
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
			foreach(var node in _map[a].Outgoing)step(node);
		}
		/// <summary>Steps through all the nodes in the <see cref="GraphMap{T}"/></summary>
		/// <param name="step">The action to perform on every node in the graph.</param>
		public void Stepper(Action<T> step) =>
			_map.Keys(step);

		/// <summary>Steps through all the nodes in the <see cref="GraphMap{T}"/></summary>
		/// <param name="step">The action to perform on every node in the graph.</param>
		/// <returns>The status of the iteration.</returns>
		public StepStatus Stepper(Func<T, StepStatus> step) =>
			_map.Keys(step);

		/// <summary>Steps through all the edges in the <see cref="GraphMap{T}"/></summary>
		/// <param name="step">The action to perform on every edge in the graph.</param>
		public void Stepper(Action<T?, T?> step) =>
			_map.Stepper((edges, a) =>
				edges.Outgoing.Stepper(b => step(a, b)));

		/// <summary>Steps through all the edges in the <see cref="GraphMap{T}"/></summary>
		/// <param name="step">The action to perform on every edge in the graph.</param>
		/// <returns>The status of the iteration.</returns>
		public StepStatus Stepper(Func<T?, T?, StepStatus> step) =>
			_map.Stepper((edges, a) =>
				edges.Outgoing.Stepper(b => step(a, b)));

		/// <summary>Makes a clone of this <see cref="GraphMap{T}"/>.</summary>
		/// <returns></returns>
		public GraphMap<T> Clone() =>
			new(this);

		/// <summary>Gets an with all the nodes of th graph in it.</summary>
		/// <returns>An array with all the nodes of th graph in it.</returns>
		public T[] ToArray()
		{
			T[] nodes = new T[_map.Count];
			int i = 0;
			_map.Keys(key => nodes[i++] = key);
			return nodes;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()=>GetEnumerator();
		/// <summary>
		/// Enumerates through the nodes of the graph
		/// </summary>
		/// <returns>Enumerator object</returns>
		public System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			foreach(var pairs in _map.GetEnumeratorPairs) yield return pairs.Key;
		}

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

		#endregion
	}

	/// <summary>
	/// Implements a weighted graph. Implements a Dictionary of Nodes and edges
	/// </summary>
	/// <typeparam name="V">The generic node type of this graph.</typeparam>
	/// <typeparam name="W">The generic weight type of this graph.</typeparam>
	public class GraphWeighted<V, W> : IGraphWeighted<V, W>
	{
		/// <summary>A cheap way to create entire structure of a graph</summary>
		protected MapHashLinked<(MapHashLinked<W?, V> OutgoingEdges, SetHashLinked<V> IncomingNodes), V> Structure;

		#region Constructors

		/// <summary>Default constructor for this Type</summary>
		public GraphWeighted()
		{
			Structure = new();
		}

		[Obsolete("Not Implemented")]
		internal GraphWeighted(GraphWeighted<V, W> graph)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region Methods

		/// <summary>Number of edges present in the graph</summary>
		/// <value>int value of edges in graph</value>
		public int EdgeCount
		{
			get
			{
				int count = 0;
				Structure.Keys(key => count += Structure[key].IncomingNodes._count);
				return count;
			}
		}

		/// <summary>Number of nodes present in graph</summary>
		public int NodeCount => Structure.Keys().Count();

		/// <summary>
		/// Tries to add a node. Returns false on success.
		/// </summary>
		/// <param name="value">The node to add</param>
		/// <param name="exception">The inner exception, in case of failure</param>
		/// <returns>boolean value indicating the success of process</returns>
		public bool TryAdd(V value, out Exception? exception)
		{
			// TODO: change this so that it does not allocate the new collections until necessary
			if (!Structure.TryAdd(value, (new(), new()), out Exception? childException))
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
			if (!Structure.TryGet(start, out var outgoing)) throw new ArgumentException(message: "start Vertex must be present in graph before adding edge", paramName: nameof(start));
			if (!Structure.TryGet(end, out var incoming)) throw new ArgumentException(message: "end Vertex must be present in graph before adding edge", paramName: nameof(end));
			outgoing.OutgoingEdges.Add(end, weight);
			incoming.IncomingNodes.Add(start);
		}

		/// <summary>Checks if b is adjacent to a.</summary>
		/// <param name="a">The starting point of the edge to check.</param>
		/// <param name="b">The ending point of the edge to check.</param>
		/// <param name="weight">The weight of the edge, if it exists.</param>
		/// <returns>True if b is adjacent to a; False if not</returns>
		public bool Adjacent(V a, V b, out W? weight)
		{
			if (!Structure.TryGet(a, out var outgoing)) throw new ArgumentException(message: "Vertex must be present in graph", paramName: nameof(a));
			if (!Structure.Contains(b)) throw new ArgumentException(message: "Vertex must be present in graph", paramName: nameof(b));
			if (outgoing.OutgoingEdges.Contains(b))
			{
				weight = Structure[a].OutgoingEdges[b];
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
		public void Clear() => Structure.Clear();

		/// <summary> Performs action via a delegate for every neighbour of a node </summary>
		/// <param name="a">The node to scan neighbours for</param>
		/// <param name="function">The delegate function to act on the node</param>
		public void Neighbors(V a, Action<V> function) => Structure[a].OutgoingEdges.Keys(function);

		/// <summary>
		/// Renoves any edge between the given nodes
		/// </summary>
		/// <param name="start">The node on the start of the edge</param>
		/// <param name="end">The node on the end of the edge</param>
		public void Remove(V start, V end)
		{
			if (!Structure.Contains(start)) throw new ArgumentException(message: "Vertex must be present in graph", paramName: nameof(start));
			if (!Structure.Contains(end)) throw new ArgumentException(message: "Vertex must be present in graph", paramName: nameof(end));
			Structure[start].OutgoingEdges.Remove(end);
			Structure[end].IncomingNodes.Remove(start);
		}

		#region Stepper and IEnumerable

		/// <summary>Invokes a delegate for each edge in the graph.</summary>
		/// <param name="step">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Action<V?, V?> step)
		{
			foreach(var pair in Structure.GetEnumeratorPairs)
			{
				foreach(var (Key, _) in pair.Value.OutgoingEdges.GetEnumeratorPairs)
				{
					step(pair.Key, Key);
				}
			}
		}

		/// <summary>
		/// Performs action for every edge
		/// </summary>
		/// <param name="step">Action to perform</param>
		/// <returns>Status of Action</returns>
		public StepStatus Stepper(Func<V, V, StepStatus> step) =>
			Structure.Stepper((edge, a) =>
			edge.OutgoingEdges.Keys(b => step(a, b)));

		#region Stepper (nodes/verteces)

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void Stepper<Step>(Step step = default)
			where Step : struct, IAction<V> =>
			StepperRef<StepToStepRef<V, Step>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void Stepper(Action<V> step) =>
			Stepper<ActionRuntime<V>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void StepperRef<Step>(Step step = default)
			where Step : struct, IStepRef<V> =>
			StepperRefBreak<StepRefBreakFromStepRef<V, Step>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public void Stepper(StepRef<V> step) =>
			StepperRef<StepRefRuntime<V>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public StepStatus StepperBreak<Step>(Step step = default)
			where Step : struct, IFunc<V, StepStatus> =>
			StepperRefBreak<StepRefBreakFromStepBreak<V, Step>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public StepStatus Stepper(Func<V, StepStatus> step) =>
			StepperBreak<StepBreakRuntime<V>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public StepStatus Stepper(StepRefBreak<V> step) =>
			StepperRefBreak<StepRefBreakRuntime<V>>(step);

		/// <inheritdoc cref="DataStructure.Stepper_O_n_step_XML"/>
		public StepStatus StepperRefBreak<Step>(Step step = default)
			where Step : struct, IStepRefBreak<V> =>
			Structure.KeysRefBreak<Step>(step);

		#endregion

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>Enumerates through all the added nodes in the graph</summary>
		/// <returns></returns>
		public IEnumerator<V> GetEnumerator() => Structure.Keys().ToArray().GetEnumerator() as IEnumerator<V>;

		#endregion

		/// <summary>
		/// Tries to remove a node. Returns true on success.
		/// </summary>
		/// <param name="node">The node to remove from the graph</param>
		/// <param name="exception">The inner exception in case of failure</param>
		/// <returns>boolean value indicating success of process</returns>
		public bool TryRemove(V node, out Exception? exception)
		{
			if (!Structure.Contains(node))
			{
				exception = new ArgumentException("Node does not exist in graph", nameof(node));
				return false;
			}
			//Remove all edges e, where e=(x, deleteNode)
			foreach (var item in Structure[node].IncomingNodes)
			{
				Remove(node, item);
			}
			//Remove all edges e, where e=(deleteNode, x)
			foreach (var item in Structure[node].OutgoingEdges.Keys().ToArray())
			{
				Remove(node, item);
			}
			Structure.Remove(node);
			exception = null;
			return true;
		}

		

		/// <summary>Clones the graph.</summary>
		/// <returns>A clone of the graph.</returns>
		[Obsolete("Not Implemented")]
		public GraphWeighted<V, W> Clone() => new(this);

		#endregion
	}
}