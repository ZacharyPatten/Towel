using Towel.DataStructures;
using Towel.Mathematics;

namespace Towel.Algorithms
{
	/// <summary>Static class taht constains algorithms for searching.</summary>
	/// <typeparam name="T">The type of items being searched for/against.</typeparam>
	public static class Search<T>
	{
		/// <summary>Performs a binary search to find the index where a specific value fits in indexed, sorted items.</summary>
		/// <param name="get">Indexer delegate.</param>
		/// <param name="length">The number of indexed items.</param>
		/// <param name="compare">Comparison delegate.</param>
		/// <returns>The index where the specific value fits into the index, sorted items.</returns>
        public static int BinarySearch(Get<T> get, int length, CompareToExpectedValue<T> compare)
		{
			if (get == null)
				throw new System.ArgumentNullException("get");
			if (compare == null)
				throw new System.ArgumentNullException("compare");
			return BinarySearch(get, 0, length, compare);
		}

        private static int BinarySearch(Get<T> get, int index, int length, CompareToExpectedValue<T> compare)
		{
			int low = index;
			int hi = index + length - 1;
			while (low <= hi)
			{
				int median = low + (hi - low >> 1);
				switch (compare(get(median)))
				{
					case Comparison.Equal:
						return median;
					case Comparison.Less:
						low = median + 1;
						break;
					case Comparison.Greater:
						hi = median - 1;
						break;
					default:
						throw new System.NotImplementedException();
				}
			}
			return ~low;
		}

		/// <summary>Static class taht constains algorithms for searching graphs.</summary>
		/// <typeparam name="Math">The numeric typs used for computations.</typeparam>
		public static class Graph<Math>
		{
			/// <summary>Step function for all neigbors of a given node.</summary>
			/// <param name="current">The node to step through all the neighbors of.</param>
			/// <param name="neighbors">Step function to perform on all neighbors.</param>
			public delegate void Neighbors(T current, Step<T> neighbors);
			/// <summary>Computes the heuristic value of a given node in a graph (smaller values mean closer to goal node).</summary>
			/// <param name="node">The node to compute the heuristic value of.</param>
			/// <returns>The computed heuristic value for this node.</returns>
			public delegate Math Heuristic(T node);
			/// <summary>Computes the cost of moving from the current node to a specific neighbor.</summary>
			/// <param name="current">The current (starting) node.</param>
			/// <param name="neighbor">The node to compute the cost of movign to.</param>
			/// <returns>The computed cost value of movign from current to neighbor.</returns>
			public delegate Math Cost(T current, T neighbor);
			/// <summary>Predicate for determining if we have reached the goal node.</summary>
			/// <param name="current">The current node.</param>
			/// <returns>True if the current node is a/the goal node; False if not.</returns>
			public delegate bool Goal(T current);

			/// <summary>Runs the A* search algorithm algorithm on a graph.</summary>
			/// <param name="start">The node to start at.</param>
			/// <param name="neighbors">Step function for all neigbors of a given node.</param>
			/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
			/// <param name="cost">Computes the cost of moving from the current node to a specific neighbor.</param>
			/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
			/// <returns>Stepper of the shortest path or null if no path exists.</returns>
			public static Stepper<T> Astar(T start, Neighbors neighbors, Heuristic heuristic, Cost cost, Goal goal)
			{
				// using a heap (aka priority queue) to store nodes based on their computed A* f(n) value
				Heap<Astar_Node> fringe = new HeapArray<Astar_Node>(
					// NOTE: Typical A* implementations prioritize smaller values
					(Astar_Node left, Astar_Node right) =>
                    {
                        Comparison comparison = Compute.Compare<Math>(right.Priority, left.Priority);
                        return comparison;
                    });

				// using a map (aka dictionary) to store costs from start to current nodes
				Map<Math, Astar_Node> computed_costs = new MapHashArray<Math, Astar_Node>();

				// construct the f(n) for this A* execution
				Astar_function function = (T node, Astar_Node previous) =>
				{
                    Math previousCost = computed_costs.Get(previous);
                    Math currentCost = cost(previous.Value, node);
                    Math costFromStart = Compute.Add<Math>(previousCost, currentCost);
                    Math hueristic = heuristic(node);
                    return Compute.Add<Math>(costFromStart, hueristic);
				};

				// push starting node
				Astar_Node start_node = new Astar_Node(null, start, default(Math));
				fringe.Enqueue(start_node);
				computed_costs.Add(start_node, default(Math));

				// run the algorithm
				while (fringe.Count != 0)
				{
					Astar_Node current = fringe.Dequeue();
					if (goal(current.Value))
					{
						return Astar_BuildPath(current);
					}
					else
					{
						neighbors(current.Value,
							(T neighbor) =>
							{
								Astar_Node newNode = new Astar_Node(current, neighbor, function(neighbor, current));
								Math costValue = Compute.Add<Math>(computed_costs.Get(current), cost(current.Value, neighbor));
								computed_costs.Add(newNode, costValue);
								fringe.Enqueue(newNode);
							});
					}
				}
				return null; // goal node was not reached (no path exists)
			}

            #region Astar Overloads

            /// <summary>Runs the A* search algorithm algorithm on a graph.</summary>
            /// <param name="start">The node to start at.</param>
            /// <param name="graph">The graph to perform the search on.</param>
            /// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
            /// <param name="cost">Computes the cost of moving from the current node to a specific neighbor.</param>
            /// <param name="goal">Predicate for determining if we have reached the goal node.</param>
            /// <returns>Stepper of the shortest path or null if no path exists.</returns>
            public static Stepper<T> Astar(T start, Towel.DataStructures.Graph<T> graph, Heuristic heuristic, Cost cost, Goal goal)
			{
				return Astar(start, graph.Neighbors, heuristic, cost, goal);
			}

			/// <summary>Runs the A* search algorithm algorithm on a graph.</summary>
			/// <param name="start">The node to start at.</param>
			/// <param name="neighbors">Delegate for gettign the neighbors of a node.</param>
			/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
			/// <param name="cost">Computes the cost of moving from the current node to a specific neighbor.</param>
			/// <param name="goal">The goal node.</param>
			/// <returns>Stepper of the shortest path or null if no path exists.</returns>
			public static Stepper<T> Astar(T start, Neighbors neighbors, Heuristic heuristic, Cost cost, T goal)
			{
				return Astar(start, neighbors, heuristic, cost, goal, Equate.Default);
			}

			/// <summary>Runs the A* search algorithm algorithm on a graph.</summary>
			/// <param name="start">The node to start at.</param>
			/// <param name="neighbors">Delegate for gettign the neighbors of a node.</param>
			/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
			/// <param name="cost">Computes the cost of moving from the current node to a specific neighbor.</param>
			/// <param name="goal">The goal node.</param>
			/// <param name="equate">A delegate for checking for equality between two nodes.</param>
			/// <returns>Stepper of the shortest path or null if no path exists.</returns>
			public static Stepper<T> Astar(T start, Neighbors neighbors, Heuristic heuristic, Cost cost, T goal, Equate<T> equate)
			{
				return Astar(start, neighbors, heuristic, cost, (T node) => { return equate(node, goal); });
			}

			/// <summary>Runs the A* search algorithm algorithm on a graph.</summary>
			/// <param name="start">The node to start at.</param>
			/// <param name="graph">The graph to perform the search on.</param>
			/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
			/// <param name="cost">Computes the cost of moving from the current node to a specific neighbor.</param>
			/// <param name="goal">The goal node.</param>
			/// <returns>Stepper of the shortest path or null if no path exists.</returns>
            public static Stepper<T> Astar(T start, Towel.DataStructures.Graph<T> graph, Heuristic heuristic, Cost cost, T goal)
			{
				return Astar(start, graph, heuristic, cost, goal, Equate.Default);
			}

			/// <summary>Runs the A* search algorithm algorithm on a graph.</summary>
			/// <param name="start">The node to start at.</param>
			/// <param name="graph">The graph to perform the search on.</param>
			/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
			/// <param name="cost">Computes the cost of moving from the current node to a specific neighbor.</param>
			/// <param name="goal">The goal node.</param>
			/// <param name="equate">A delegate for checking for equality between two nodes.</param>
			/// <returns>Stepper of the shortest path or null if no path exists.</returns>
            public static Stepper<T> Astar(T start, Towel.DataStructures.Graph<T> graph, Heuristic heuristic, Cost cost, T goal, Equate<T> equate)
			{
				return Astar(start, graph.Neighbors, heuristic, cost, (T node) => { return equate(node, goal); });
			}

			#endregion

			#region Astar privates

			// nested types
			#region private class Node
			/// <summary>Wraps a T node to include path and priority for this algorithm.</summary>
			private class Astar_Node
			{
				private Astar_Node _previous;
				private T _value;
				private Math _priority;

				public Astar_Node Previous { get { return this._previous; } }
				public T Value { get { return this._value; } }
				public Math Priority { get { return this._priority; } }

				public Astar_Node(Astar_Node previous, T value, Math priority)
				{
					this._previous = previous;
					this._value = value;
					this._priority = priority;
				}
			}
			#endregion
			#region private class PathNode
			/// <summary>Needed for ordering computed path after the goal node is found.</summary>
			private class Astar_PathNode
			{
				private T _value;
				private Astar_PathNode _next;

				public T Value { get { return this._value; } }
				public Astar_PathNode Next { get { return this._next; } set { this._next = value; } }

				public Astar_PathNode(T value)
				{
					this._value = value;
				}
			}
			#endregion
			// delegates
			#region private delegate Math function(T node, Node previous);
			/// <summary>Computes "f(n) = g(n) + h(n)" where g(n) is the known cost of getting from the initial node to n,
			/// h(n) is a heuristic estimate of the cost to get from n to any goal node, and f(n) is the node priority.
			/// Smaller f(n) means higher node priority.</summary>
			/// <param name="node">The node to compute the A* function of.</param>
			/// <param name="previous">The previous node (needed to compute the cost).</param>
			/// <returns>The A* computed value for searching.</returns>
			private delegate Math Astar_function(T node, Astar_Node previous);
			#endregion
			// methods
			#region private static Stepper<T> BuildPath(Node node)
			/// <summary>Builds the path from resulting from the A* algorithm.</summary>
			/// <param name="node">The resulting final node fromt he A* algorithm.</param>
			/// <returns>A stepper function of the computed path frmo the A* algorithm.</returns>
			private static Stepper<T> Astar_BuildPath(Astar_Node node)
			{
				Astar_PathNode end;
				Astar_PathNode start = Astar_BuildPath(node, out end);
				return (Step<T> step) =>
				{
					Astar_PathNode current = start;
					while (current != null)
					{
						step(current.Value);
						current = current.Next;
					}
				};
			}
			#endregion
			#region private static PathNode BuildPath(Node currentNode, out PathNode currentPathNode)
			private static Astar_PathNode Astar_BuildPath(Astar_Node currentNode, out Astar_PathNode currentPathNode)
			{
				if (currentNode.Previous == null)
				{
					Astar_PathNode start = new Astar_PathNode(currentNode.Value);
					currentPathNode = start;
					return start;
				}
				else
				{
					Astar_PathNode previous;
					Astar_PathNode start = Astar_BuildPath(currentNode.Previous, out previous);
					currentPathNode = new Astar_PathNode(currentNode.Value);
					previous.Next = currentPathNode;
					return start;
				}
			}
			#endregion

			#endregion

			/// <summary>Runs the Greedy search algorithm algorithm on a graph.</summary>
			/// <param name="start">The node to start at.</param>
			/// <param name="neighbors">Step function for all neigbors of a given node.</param>
			/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
			/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
			/// <returns>Stepper of the shortest path or null if no path exists.</returns>
			public static Stepper<T> Greedy(T start, Neighbors neighbors, Heuristic heuristic, Goal goal)
			{
				// using a heap (aka priority queue) to store nodes based on their computed heuristic value
				Heap<Greedy_Node> fringe = new HeapArray<Greedy_Node>(
					// NOTE: I just reversed the order of left and right because smaller values are higher priority
					(Greedy_Node left, Greedy_Node right) => { return Compute.Compare(right.Priority, left.Priority); });

				// push starting node
				Greedy_Node start_node = new Greedy_Node(null, start, default(Math));
				fringe.Enqueue(start_node);

				// run the algorithm
				while (fringe.Count != 0)
				{
					Greedy_Node current = fringe.Dequeue();
					if (goal(current.Value))
					{
						return Greedy_BuildPath(current);
					}
					else
					{
						neighbors(current.Value,
							(T neighbor) =>
							{
								Greedy_Node newNode = new Greedy_Node(current, neighbor, heuristic(neighbor));
								fringe.Enqueue(newNode);
							});
					}
				}
				return null; // goal node was not reached (no path exists)
			}

			#region Greedy Overloads

			/// <summary>Runs the Greedy search algorithm algorithm on a graph.</summary>
			/// <param name="start">The node to start at.</param>
			/// <param name="neighbors">Step function for all neigbors of a given node.</param>
			/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
			/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
			/// <returns>Stepper of the shortest path or null if no path exists.</returns>
			public static Stepper<T> Greedy(T start, Neighbors neighbors, Heuristic heuristic, T goal)
			{
				return Greedy(start, neighbors, heuristic, goal, Equate.Default);
			}

			/// <summary>Runs the Greedy search algorithm algorithm on a graph.</summary>
			/// <param name="start">The node to start at.</param>
			/// <param name="neighbors">Step function for all neigbors of a given node.</param>
			/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
			/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
			/// <param name="equate">Delegate for checking for equality between two nodes.</param>
			/// <returns>Stepper of the shortest path or null if no path exists.</returns>
			public static Stepper<T> Greedy(T start, Neighbors neighbors, Heuristic heuristic, T goal, Equate<T> equate)
			{
				return Greedy(start, neighbors, heuristic, (T node) => { return equate(node, goal); });
			}

			/// <summary>Runs the Greedy search algorithm algorithm on a graph.</summary>
			/// <param name="start">The node to start at.</param>
			/// <param name="graph">The graph to search against.</param>
			/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
			/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
			/// <returns>Stepper of the shortest path or null if no path exists.</returns>
            public static Stepper<T> Greedy(T start, Towel.DataStructures.Graph<T> graph, Heuristic heuristic, T goal)
			{
				return Greedy(start, graph, heuristic, goal, Equate.Default);
			}

			/// <summary>Runs the Greedy search algorithm algorithm on a graph.</summary>
			/// <param name="start">The node to start at.</param>
			/// <param name="graph">The graph to search against.</param>
			/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
			/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
			/// <param name="equate">Delegate for checking for equality between two nodes.</param>
			/// <returns>Stepper of the shortest path or null if no path exists.</returns>
            public static Stepper<T> Greedy(T start, Towel.DataStructures.Graph<T> graph, Heuristic heuristic, T goal, Equate<T> equate)
			{
				return Greedy(start, graph.Neighbors, heuristic, (T node) => { return equate(node, goal); });
			}

			/// <summary>Runs the Greedy search algorithm algorithm on a graph.</summary>
			/// <param name="start">The node to start at.</param>
			/// <param name="graph">The graph to search against.</param>
			/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
			/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
			/// <returns>Stepper of the shortest path or null if no path exists.</returns>
            public static Stepper<T> Greedy(T start, Towel.DataStructures.Graph<T> graph, Heuristic heuristic, Goal goal)
			{
				return Greedy(start, graph.Neighbors, heuristic, goal);
			}

			#endregion

			#region Greedy privates

			// nested types
			#region private class Node
			/// <summary>Wraps a T node to include path and priority for this algorithm.</summary>
			private class Greedy_Node
			{
				private Greedy_Node _previous;
				private T _value;
				private Math _priority;

				public Greedy_Node Previous { get { return this._previous; } }
				public T Value { get { return this._value; } }
				public Math Priority { get { return this._priority; } }

				public Greedy_Node(Greedy_Node previous, T value, Math priority)
				{
					this._previous = previous;
					this._value = value;
					this._priority = priority;
				}
			}
			#endregion
			#region private class PathNode
			/// <summary>Needed for ordering computed path after the goal node is found.</summary>
			private class Greedy_PathNode
			{
				private T _value;
				private Greedy_PathNode _next;

				public T Value { get { return this._value; } }
				public Greedy_PathNode Next { get { return this._next; } set { this._next = value; } }

				public Greedy_PathNode(T value)
				{
					this._value = value;
				}
			}
			#endregion
			// methods
			#region private static Stepper<T> Greedy_BuildPath(Greedy_Node node)
			/// <summary>Builds the path from resulting from the A* algorithm.</summary>
			/// <param name="node">The resulting final node fromt he A* algorithm.</param>
			/// <returns>A stepper function of the computed path frmo the A* algorithm.</returns>
			private static Stepper<T> Greedy_BuildPath(Greedy_Node node)
			{
				Greedy_PathNode end;
				Greedy_PathNode start = Greedy_BuildPath(node, out end);
				return (Step<T> step) =>
				{
					Greedy_PathNode current = start;
					while (current != null)
					{
						step(current.Value);
						current = current.Next;
					}
				};
			}
			#endregion
			#region private static Greedy_PathNode Greedy_BuildPath(Greedy_Node currentNode, out Greedy_PathNode currentPathNode)
			private static Greedy_PathNode Greedy_BuildPath(Greedy_Node currentNode, out Greedy_PathNode currentPathNode)
			{
				if (currentNode.Previous == null)
				{
					Greedy_PathNode start = new Greedy_PathNode(currentNode.Value);
					currentPathNode = start;
					return start;
				}
				else
				{
					Greedy_PathNode previous;
					Greedy_PathNode start = Greedy_BuildPath(currentNode.Previous, out previous);
					currentPathNode = new Greedy_PathNode(currentNode.Value);
					previous.Next = currentPathNode;
					return start;
				}
			}

			#endregion

			#endregion
		}
	}
}
