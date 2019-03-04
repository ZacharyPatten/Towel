namespace Towel.DataStructures
{
	public interface Graph<T> : DataStructure<T>,
		// Structure Properties
		Structure.Addable<T>,
		Structure.Removable<T>,
		Structure.Clearable<T>
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

		#endregion
	}

	/// <summary>Stores the graph as a set-hash of nodes and quadtree of edges.</summary>
	/// <typeparam name="T">The generic type of this data structure.</typeparam>
	[System.Serializable]
	public class GraphSetOmnitree<T> : Graph<T>
	{
		// Fields
		public SetHashList<T> _nodes;
        public OmnitreePointsLinked<Edge, T, T> _edges;

		#region Nested Types

		/// <summary>Represents an edge in a graph.</summary>
		public class Edge
		{
			private T _start;
			private T _end;

			public T Start { get { return this._start; } set { this._start = value; } }
			public T End { get { return this._end; } set { this._end = value; } }

			public Edge(T start, T end)
			{
				this._start = start;
				this._end = end;
			}
		}

		#endregion

		#region Constructor

		private GraphSetOmnitree(GraphSetOmnitree<T> graph)
		{
            this._edges = graph._edges.Clone() as OmnitreePointsLinked<Edge, T, T>;
			this._nodes = graph._nodes.Clone() as SetHashList<T>;
		}

        public GraphSetOmnitree(Equate<T> equate, Compare<T> compare, Hash<T> hash)
		{
			this._nodes = new SetHashList<T>(equate, hash);
            this._edges = new OmnitreePointsLinked<Edge, T, T>((Edge a, out T start, out T end) => { start = a.Start; end = a.End; });
		}

        public GraphSetOmnitree(Compare<T> compare, Hash<T> hash)
		{
			this._nodes = new SetHashList<T>((T a, T b) => { return compare(a, b) == Comparison.Equal; }, hash);
            this._edges = new OmnitreePointsLinked<Edge, T, T>((Edge a, out T start, out T end) => { start = a.Start; end = a.End; });
		}

		#endregion

		#region Properties

		public int EdgeCount { get { return this._edges.Count; } }
		public int NodeCount { get { return this._nodes.Count; } }

		#endregion

		#region Methods

		public DataStructure<T> Clone()
		{
			return new GraphSetOmnitree<T>(this);
		}

		public T[] ToArray()
		{
			throw new System.NotImplementedException();
		}

		public void Clear()
		{
			this._nodes.Clear();
			this._edges.Clear();
		}

		public bool Adjacent(T a, T b)
		{
			bool exists = false;
			this._edges.Stepper((Edge edge) => { exists = true; }, a, b, a, b);
			return exists;
		}

		public void Neighbors(T a, Step<T> function)
		{
			if (!this._nodes.Contains(a))
				throw new System.InvalidOperationException("Attempting to look up the neighbors of a node that does not belong to a graph");

			this._edges.Stepper((Edge e) => { function(e.End); }, a, a, Omnitree.Bound<T>.None, Omnitree.Bound<T>.None);
		}

		public void Add(T start, T end)
		{
			if (!this._nodes.Contains(start))
				throw new System.InvalidOperationException("Adding an edge to a graph from a node that does not exists");
			if (!this._nodes.Contains(end))
				throw new System.InvalidOperationException("Adding an edge to a graph to a node that does not exists");
			this._edges.Stepper(
					(Edge e) => { throw new System.InvalidOperationException("Adding an edge to a graph that already exists"); },
					start, start, end, end);

			this._edges.Add(new Edge(start, end));
		}

		public void Remove(T start, T end)
		{
			if (!this._nodes.Contains(start))
				throw new System.InvalidOperationException("Removing an edge to a graph from a node that does not exists");
			if (!this._nodes.Contains(end))
				throw new System.InvalidOperationException("Removing an edge to a graph to a node that does not exists");

			bool exists = false;
			this._edges.Stepper(
					(Edge e) => { exists = true; },
					start, end, start, end);
			if (!exists)
				throw new System.InvalidOperationException("Removing a non-existing edge in a graph");

			this._edges.Remove(start, end, start, end);
		}

		public void Add(T node)
		{
			if (this._nodes.Contains(node))
				throw new System.InvalidOperationException("Adding an already-existing node to a graph");

			this._nodes.Add(node);
		}

		public void Remove(T node)
		{
			if (this._nodes.Contains(node))
				throw new System.InvalidOperationException("Removing non-existing node from a graph");

            this._edges.Remove(node, node, Omnitree.Bound<T>.None, Omnitree.Bound<T>.None);
            this._edges.Remove(Omnitree.Bound<T>.None, Omnitree.Bound<T>.None, node, node);

			this._nodes.Add(node);
		}

		public void Stepper(Step<T> function)
		{
			this._nodes.Stepper(function);
		}

		public StepStatus Stepper(StepBreak<T> function)
		{
			return this._nodes.Stepper(function);
		}

		public void Stepper(Step<Edge> function)
		{
			this._edges.Stepper(function);
		}

		public StepStatus Stepper(StepBreak<Edge> function)
		{
			return this._edges.Stepper(function);
		}

		System.Collections.IEnumerator
			System.Collections.IEnumerable.GetEnumerator()
		{
			throw new System.NotImplementedException();
		}

		System.Collections.Generic.IEnumerator<T>
			System.Collections.Generic.IEnumerable<T>.GetEnumerator()
		{
			throw new System.NotImplementedException();
		}

		#endregion
	}

	/// <summary>Stores a graph as a map and nested map (adjacency matrix).</summary>
	/// <typeparam name="T">The generic node type of this graph.</typeparam>
	[System.Serializable]
	public class GraphMap<T> : Graph<T>
	{
		// Fields
		public MapHashLinked<MapHashLinked<bool, T>, T> _map;
		int _edges;

		#region Nested Types

		/// <summary>Represents an edge in a graph.</summary>
		public class Edge
		{
			public T _start;
			public T _end;

			public T Start { get { return this._start; } set { this._start = value; } }
			public T End { get { return this._end; } set { this._end = value; } }

			public Edge(T start, T end)
			{
				this._start = start;
				this._end = end;
			}
		}

		#endregion

		#region Constructors

		private GraphMap(Equate<T> equate, Hash<T> hash, GraphSetOmnitree<T> graph)
		{
			this._edges = 0;
			this._map = new MapHashLinked<MapHashLinked<bool, T>, T>(equate, hash);
		}

		#endregion

		#region Properties

		public int EdgeCount { get { return this._edges; } }
		public int NodeCount { get { return this._map.Count; } }

		#endregion

		#region Methods

		public bool Adjacent(T a, T b)
		{
			bool temp_bool;
			MapHashLinked<bool, T> temp_map;
			if (this._map.TryGet(a, out temp_map))
				if (temp_map.TryGet(a, out temp_bool))
					return temp_bool;
			return false;
		}

		public void Neighbors(T a, Step<T> function)
		{
			MapHashLinked<bool, T> temp_map;
			if (this._map.TryGet(a, out temp_map))
				temp_map.Keys(function);
		}

		/// <summary>Adds an edge to the graph.</summary>
		/// <param name="start">The starting point of the edge.</param>
		/// <param name="end">The ending point of the edge.</param>
		public void Add(T start, T end)
		{
			if (!this._map.Contains(start))
				throw new System.InvalidOperationException("Adding an edge to a non-existing starting node.");
			if (!this._map[start].Contains(end))
				throw new System.InvalidOperationException("Adding an edge to a non-existing ending node.");
			if (this._map[start] == null)
				this._map[start] = new MapHashLinked<bool, T>(this._map.Equate, this._map.Hash);

			this._map[start].Add(end, true);
		}

		/// <summary>Removes an edge from the graph.</summary>
		/// <param name="start">The starting point of the edge to remove.</param>
		/// <param name="end">The ending point of the edge to remove.</param>
		public void Remove(T start, T end)
		{
			if (this._map[start] == null)
				throw new System.InvalidOperationException("Removing a non-existing edge from the graph.");
			if (!this._map[start].Contains(end))
				throw new System.InvalidOperationException("Removing a non-existing edge from the graph.");
			this._map[start].Remove(end);
		}

		/// <summary>Adds a node to the graph.</summary>
		/// <param name="node">The node to add to the graph.</param>
		public void Add(T node)
		{
			if (this._map.Count == int.MaxValue)
				throw new System.InvalidOperationException("This graph has reach its node capacity.");
			this._map.Add(node, new MapHashLinked<bool, T>(this._map.Equate, this._map.Hash));
		}

		public void Remove(T node)
		{
			throw new System.NotImplementedException();
		}

		public void Stepper(Step<T> function)
		{
			throw new System.NotImplementedException();
		}

		public void Stepper(StepRef<T> function)
		{
			throw new System.NotImplementedException();
		}

		public StepStatus Stepper(StepBreak<T> function)
		{
			throw new System.NotImplementedException();
		}

		public StepStatus Stepper(StepRefBreak<T> function)
		{
			throw new System.NotImplementedException();
		}

		public void Stepper(Step<Edge> function)
		{
			throw new System.NotImplementedException();
		}

		public void Stepper(StepRef<Edge> function)
		{
			throw new System.NotImplementedException();
		}

		public StepStatus Stepper(StepBreak<Edge> function)
		{
			throw new System.NotImplementedException();
		}

		public StepStatus Stepper(StepRefBreak<Edge> function)
		{
			throw new System.NotImplementedException();
		}

		public DataStructure<T> Clone()
		{
			throw new System.NotImplementedException();
		}

		public T[] ToArray()
		{
			throw new System.NotImplementedException();
		}

		System.Collections.IEnumerator
			System.Collections.IEnumerable.GetEnumerator()
		{
			throw new System.NotImplementedException();
		}

		System.Collections.Generic.IEnumerator<T>
			System.Collections.Generic.IEnumerable<T>.GetEnumerator()
		{
			throw new System.NotImplementedException();
		}

		public void Clear()
		{
			this._edges = 0;
			this._map = new MapHashLinked<MapHashLinked<bool, T>, T>(this._map.Equate, this._map.Hash);
		}

		#endregion
	}
}
