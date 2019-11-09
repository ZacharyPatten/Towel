using System;
using Towel.DataStructures;
using static Towel.Syntax;

namespace Towel
{
	/// <summary>Static class taht constains algorithms for searching.</summary>
	public static class Search
	{
		#region Binary

		/// <summary>Performs a binary search to find the index where a specific value fits in indexed, sorted items.</summary>
		/// <typeparam name="T">The generic type of the set of values.</typeparam>
		/// <param name="array">The array to binary search on.</param>
		/// <param name="compare">Comparison delegate.</param>
		/// <returns>The index where the specific value fits into the index, sorted items.</returns>
		public static int Binary<T>(T[] array, CompareToKnownValue<T> compare) =>
			Binary(array.WrapGetIndex(), array.Length, compare);

		/// <summary>Performs a binary search to find the index where a specific value fits in indexed, sorted items.</summary>
		/// <typeparam name="T">The generic type of the set of values.</typeparam>
		/// <param name="get">Indexer delegate.</param>
		/// <param name="length">The number of indexed items.</param>
		/// <param name="compare">Comparison delegate.</param>
		/// <returns>The index where the specific value fits into the index, sorted items.</returns>
		public static int Binary<T>(GetIndex<T> get, int length, CompareToKnownValue<T> compare)
		{
			_ = get ?? throw new ArgumentNullException(nameof(get));
			_ = compare ?? throw new ArgumentNullException(nameof(compare));
			if (length <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(length), length, "!(" + nameof(length) + " > 0)");
			}
			return Binary(get, 0, length, compare);
		}

		internal static int Binary<T>(GetIndex<T> get, int index, int length, CompareToKnownValue<T> compare)
		{
			int low = index;
			int hi = index + length - 1;
			while (low <= hi)
			{
				int median = low + (hi - low >> 1);
				switch (compare(get(median)))
				{
					case Equal:
						return median;
					case Less:
						low = median + 1;
						break;
					case Greater:
						hi = median - 1;
						break;
					default:
						throw new NotImplementedException();
				}
			}
			return ~low;
		}

		#endregion

		#region Graph Algorithms

		#region Publics

		/// <summary>The status of a graph search algorithm.</summary>
		public enum GraphSearchStatus
		{
			#region Members

			/// <summary>Graph search was not broken.</summary>
			Continue = 0,
			/// <summary>Graph search was broken.</summary>
			Break = 1,
			/// <summary>Graph search found the goal.</summary>
			Goal = 2,

			#endregion
		}

		/// <summary>Syntax sugar hacks.</summary>
		public static class Syntax
		{
			/// <summary>This is a syntax sugar hack to allow implicit conversions from StepStatus to GraphSearchStatus.</summary>
			public struct GraphSearchStatus
			{
				internal readonly Search.GraphSearchStatus Value;
				/// <summary>Constructs a new graph search status.</summary>
				/// <param name="value">The status of the graph search.</param>
				public GraphSearchStatus(Search.GraphSearchStatus value) => Value = value;
				/// <summary>Converts a <see cref="Search.GraphSearchStatus"/> into a <see cref="GraphSearchStatus"/>.</summary>
				/// <param name="value">The <see cref="Search.GraphSearchStatus"/> to convert.</param>
				public static implicit operator GraphSearchStatus(Search.GraphSearchStatus value) => new GraphSearchStatus(value);
				/// <summary>Converts a <see cref="StepStatus"/> into a <see cref="GraphSearchStatus"/>.</summary>
				/// <param name="value">The <see cref="StepStatus"/> to convert.</param>
				public static implicit operator GraphSearchStatus(StepStatus value) => new GraphSearchStatus((Search.GraphSearchStatus)value);
				/// <summary>Converts a <see cref="GraphSearchStatus"/> into a <see cref="Search.GraphSearchStatus"/>.</summary>
				/// <param name="value">The <see cref="GraphSearchStatus"/> to convert.</param>
				public static implicit operator Search.GraphSearchStatus(GraphSearchStatus value) => value.Value;
			}
		}

		/// <summary>Step function for all neigbors of a given node.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <param name="current">The node to step through all the neighbors of.</param>
		/// <param name="neighbors">Step function to perform on all neighbors.</param>
		public delegate void Neighbors<Node>(Node current, Step<Node> neighbors);
		/// <summary>Computes the heuristic value of a given node in a graph (smaller values mean closer to goal node).</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="node">The node to compute the heuristic value of.</param>
		/// <returns>The computed heuristic value for this node.</returns>
		public delegate Numeric Heuristic<Node, Numeric>(Node node);
		/// <summary>Computes the cost of moving from the current node to a specific neighbor.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="current">The current (starting) node.</param>
		/// <param name="neighbor">The node to compute the cost of movign to.</param>
		/// <returns>The computed cost value of movign from current to neighbor.</returns>
		public delegate Numeric Cost<Node, Numeric>(Node current, Node neighbor);
		/// <summary>Predicate for determining if we have reached the goal node.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <param name="current">The current node.</param>
		/// <returns>True if the current node is a/the goal node; False if not.</returns>
		public delegate bool Goal<Node>(Node current);
		/// <summary>Checks the status of a graph search.</summary>
		/// <typeparam name="Node">The node type of the search.</typeparam>
		/// <param name="current">The current node of the search.</param>
		/// <returns>The status of the search.</returns>
		public delegate Syntax.GraphSearchStatus Check<Node>(Node current);

		#endregion

		#region Internals

		internal abstract class BaseAlgorithmNode<AlgorithmNode, Node>
			where AlgorithmNode : BaseAlgorithmNode<AlgorithmNode, Node>
		{
			internal AlgorithmNode Previous;
			internal Node Value;
		}

		internal class BreadthFirstSearch<Node> : BaseAlgorithmNode<BreadthFirstSearch<Node>, Node> { }

		internal class DijkstraNode<Node, Numeric> : BaseAlgorithmNode<DijkstraNode<Node, Numeric>, Node>
		{
			internal Numeric Priority;
		}

		internal class AstarNode<Node, Numeric> : BaseAlgorithmNode<AstarNode<Node, Numeric>, Node>
		{
			internal Numeric Priority;
			internal Numeric Cost;
		}

		internal class PathNode<Node>
		{
			internal Node Value;
			internal PathNode<Node> Next;
		}

		internal static Stepper<Node> BuildPath<AlgorithmNode, Node>(BaseAlgorithmNode<AlgorithmNode, Node> node)
			where AlgorithmNode : BaseAlgorithmNode<AlgorithmNode, Node>
		{
			PathNode<Node> start = null;
			for (BaseAlgorithmNode<AlgorithmNode, Node> current = node; !(current is null); current = current.Previous)
			{
				PathNode<Node> temp = start;
				start = new PathNode<Node>()
				{
					Value = current.Value,
					Next = temp,
				};
			}
			return step =>
			{
				PathNode<Node> current = start;
				while (!(current is null))
				{
					step(current.Value);
					current = current.Next;
				}
			};
		}

		#endregion

		#region A* Algorithm

		/// <summary>Runs the A* search algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="neighbors">Step function for all neigbors of a given node.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="cost">Computes the cost of moving from the current node to a specific neighbor.</param>
		/// <param name="check">Checks the status of the search.</param>
		/// <param name="totalCost">The total cost of the path if a path was found.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, Neighbors<Node> neighbors, Heuristic<Node, Numeric> heuristic, Cost<Node, Numeric> cost, Check<Node> check, out Numeric totalCost)
		{
			// using a heap (aka priority queue) to store nodes based on their computed A* f(n) value
			IHeap<AstarNode<Node, Numeric>> fringe = new HeapArray<AstarNode<Node, Numeric>>(
				// NOTE: Typical A* implementations prioritize smaller values
				(a, b) => Comparison(b.Priority, a.Priority));

			// push starting node
			fringe.Enqueue(
				new AstarNode<Node, Numeric>()
				{
					Previous = null,
					Value = start,
					Priority = default,
					Cost = Constant<Numeric>.Zero,
				});

			// run the algorithm
			while (fringe.Count != 0)
			{
				AstarNode<Node, Numeric> current = fringe.Dequeue();
				GraphSearchStatus status = check(current.Value);
				if (status == GraphSearchStatus.Break)
				{
					break;
				}
				else if (status == GraphSearchStatus.Goal)
				{
					totalCost = current.Cost;
					return BuildPath(current);
				}
				else
				{
					neighbors(current.Value,
						neighbor =>
						{
							Numeric costValue = Addition(current.Cost, cost(current.Value, neighbor));
							fringe.Enqueue(
								new AstarNode<Node, Numeric>()
								{
									Previous = current,
									Value = neighbor,
									Priority = Addition(heuristic(neighbor), costValue),
									Cost = costValue,
								});
						});
				}
			}
			totalCost = default;
			return null; // goal node was not reached (no path exists)
		}

		#region A* overloads

		/// <summary>Runs the A* search algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="neighbors">Step function for all neigbors of a given node.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="cost">Computes the cost of moving from the current node to a specific neighbor.</param>
		/// <param name="goal">The predicate to determine if the goal has been reached.</param>
		/// <param name="totalCost">The total cost of the path if a path was found.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, Neighbors<Node> neighbors, Heuristic<Node, Numeric> heuristic, Cost<Node, Numeric> cost, Goal<Node> goal, out Numeric totalCost) =>
			Graph(start, neighbors, heuristic, cost, node => goal(node) ? GraphSearchStatus.Goal : GraphSearchStatus.Continue, out totalCost);

		/// <summary>Runs the A* search algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="graph">The graph to perform the search on.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="cost">Computes the cost of moving from the current node to a specific neighbor.</param>
		/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
		/// <param name="totalCost">The total cost of the path if a path was found.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, IGraph<Node> graph, Heuristic<Node, Numeric> heuristic, Cost<Node, Numeric> cost, Goal<Node> goal, out Numeric totalCost) =>
			Graph(start, graph.Neighbors, heuristic, cost, goal, out totalCost);

		/// <summary>Runs the A* search algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="neighbors">Delegate for gettign the neighbors of a node.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="cost">Computes the cost of moving from the current node to a specific neighbor.</param>
		/// <param name="goal">The goal node.</param>
		/// <param name="totalCost">The total cost of the path if a path was found.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, Neighbors<Node> neighbors, Heuristic<Node, Numeric> heuristic, Cost<Node, Numeric> cost, Node goal, out Numeric totalCost) =>
			Graph(start, neighbors, heuristic, cost, goal, Equate.Default, out totalCost);

		/// <summary>Runs the A* search algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="neighbors">Delegate for gettign the neighbors of a node.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="cost">Computes the cost of moving from the current node to a specific neighbor.</param>
		/// <param name="goal">The goal node.</param>
		/// <param name="equate">A delegate for checking for equality between two nodes.</param>
		/// <param name="totalCost">The total cost of the path if a path was found.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, Neighbors<Node> neighbors, Heuristic<Node, Numeric> heuristic, Cost<Node, Numeric> cost, Node goal, Equate<Node> equate, out Numeric totalCost) =>
			Graph(start, neighbors, heuristic, cost, node => equate(node, goal), out totalCost);

		/// <summary>Runs the A* search algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="graph">The graph to perform the search on.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="cost">Computes the cost of moving from the current node to a specific neighbor.</param>
		/// <param name="goal">The goal node.</param>
		/// <param name="totalCost">The total cost of the path if a path was found.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, IGraph<Node> graph, Heuristic<Node, Numeric> heuristic, Cost<Node, Numeric> cost, Node goal, out Numeric totalCost) =>
			Graph(start, graph, heuristic, cost, goal, Equate.Default, out totalCost);

		/// <summary>Runs the A* search algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="graph">The graph to perform the search on.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="cost">Computes the cost of moving from the current node to a specific neighbor.</param>
		/// <param name="goal">The goal node.</param>
		/// <param name="equate">A delegate for checking for equality between two nodes.</param>
		/// <param name="totalCost">The total cost of the path if a path was found.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, IGraph<Node> graph, Heuristic<Node, Numeric> heuristic, Cost<Node, Numeric> cost, Node goal, Equate<Node> equate, out Numeric totalCost) =>
			Graph(start, graph.Neighbors, heuristic, cost, node => equate(node, goal), out totalCost);

		#endregion

		#endregion

		#region Dijkstra Algorithm

		/// <summary>Runs the Dijkstra search algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="neighbors">Step function for all neigbors of a given node.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="check">Checks the status of the search.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, Neighbors<Node> neighbors, Heuristic<Node, Numeric> heuristic, Check<Node> check)
		{
			// using a heap (aka priority queue) to store nodes based on their computed heuristic value
			IHeap<DijkstraNode<Node, Numeric>> fringe = new HeapArray<DijkstraNode<Node, Numeric>>(
				// NOTE: Typical graph search implementations prioritize smaller values
				(a, b) => Comparison(b.Priority, a.Priority));

			// push starting node
			fringe.Enqueue(
				new DijkstraNode<Node, Numeric>()
				{
					Previous = null,
					Value = start,
					Priority = default,
				});

			// run the algorithm
			while (fringe.Count != 0)
			{
				DijkstraNode<Node, Numeric> current = fringe.Dequeue();
				GraphSearchStatus status = check(current.Value);
				if (status == GraphSearchStatus.Break)
				{
					break;
				}
				if (status == GraphSearchStatus.Goal)
				{
					return BuildPath(current);
				}
				else
				{
					neighbors(current.Value,
						neighbor =>
						{
							fringe.Enqueue(
								new DijkstraNode<Node, Numeric>()
								{
									Previous = current,
									Value = neighbor,
									Priority = heuristic(neighbor),
								});
						});
				}
			}
			return null; // goal node was not reached (no path exists)
		}

		#region Dijkstra Overloads

		/// <summary>Runs the Dijkstra search algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="neighbors">Step function for all neigbors of a given node.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, Neighbors<Node> neighbors, Heuristic<Node, Numeric> heuristic, Goal<Node> goal) =>
			Graph(start, neighbors, heuristic, node => goal(node) ? GraphSearchStatus.Goal : GraphSearchStatus.Continue);

		/// <summary>Runs the Dijkstra search algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="neighbors">Step function for all neigbors of a given node.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, Neighbors<Node> neighbors, Heuristic<Node, Numeric> heuristic, Node goal) =>
			Graph(start, neighbors, heuristic, goal, Equate.Default);

		/// <summary>Runs the Dijkstra search algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="neighbors">Step function for all neigbors of a given node.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
		/// <param name="equate">Delegate for checking for equality between two nodes.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, Neighbors<Node> neighbors, Heuristic<Node, Numeric> heuristic, Node goal, Equate<Node> equate) =>
			Graph(start, neighbors, heuristic, node => equate(node, goal));

		/// <summary>Runs the Dijkstra search algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="graph">The graph to search against.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, IGraph<Node> graph, Heuristic<Node, Numeric> heuristic, Node goal) =>
			Graph(start, graph, heuristic, goal, Equate.Default);

		/// <summary>Runs the Dijkstra search algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="graph">The graph to search against.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
		/// <param name="equate">Delegate for checking for equality between two nodes.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, IGraph<Node> graph, Heuristic<Node, Numeric> heuristic, Node goal, Equate<Node> equate) =>
			Graph(start, graph.Neighbors, heuristic, node => equate(node, goal));

		/// <summary>Runs the Dijkstra search algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="graph">The graph to search against.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, IGraph<Node> graph, Heuristic<Node, Numeric> heuristic, Goal<Node> goal) =>
			Graph(start, graph.Neighbors, heuristic, goal);

		#endregion

		#endregion

		#region BreadthFirstSearch Algorithm

		/// <summary>Runs the Breadth-First-Search search algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="neighbors">Step function for all neigbors of a given node.</param>
		/// <param name="check">Checks the status of the search.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node>(Node start, Neighbors<Node> neighbors, Check<Node> check)
		{
			IQueue<BreadthFirstSearch<Node>> fringe = new QueueLinked<BreadthFirstSearch<Node>>();

			// push starting node
			fringe.Enqueue(
				new BreadthFirstSearch<Node>()
				{
					Previous = null,
					Value = start,
				});

			// run the algorithm
			while (fringe.Count != 0)
			{
				BreadthFirstSearch<Node> current = fringe.Dequeue();
				GraphSearchStatus status = check(current.Value);
				if (status == GraphSearchStatus.Break)
				{
					break;
				}
				if (status == GraphSearchStatus.Goal)
				{
					return BuildPath(current);
				}
				else
				{
					neighbors(current.Value,
						neighbor =>
						{
							fringe.Enqueue(
								new BreadthFirstSearch<Node>()
								{
									Previous = current,
									Value = neighbor,
								});
						});
				}
			}
			return null; // goal node was not reached (no path exists)
		}

		#region BreadthFirstSearch Overloads

		/// <summary>Runs the Breadth-First-Search search algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="neighbors">Step function for all neigbors of a given node.</param>
		/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node>(Node start, Neighbors<Node> neighbors, Goal<Node> goal) =>
			Graph(start, neighbors, node => goal(node) ? GraphSearchStatus.Goal : GraphSearchStatus.Continue);

		/// <summary>Runs the BreadthFirstSearch search algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="neighbors">Step function for all neigbors of a given node.</param>
		/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node>(Node start, Neighbors<Node> neighbors, Node goal) =>
			Graph(start, neighbors, goal, Equate.Default);

		/// <summary>Runs the BreadthFirstSearch search algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="neighbors">Step function for all neigbors of a given node.</param>
		/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
		/// <param name="equate">Delegate for checking for equality between two nodes.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node>(Node start, Neighbors<Node> neighbors, Node goal, Equate<Node> equate) =>
			Graph(start, neighbors, node => equate(node, goal));

		/// <summary>Runs the BreadthFirstSearch search algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="graph">The graph to search against.</param>
		/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node>(Node start, IGraph<Node> graph, Node goal) =>
			Graph(start, graph, goal, Equate.Default);

		/// <summary>Runs the BreadthFirstSearch search algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="graph">The graph to search against.</param>
		/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
		/// <param name="equate">Delegate for checking for equality between two nodes.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node>(Node start, IGraph<Node> graph, Node goal, Equate<Node> equate) =>
			Graph(start, graph.Neighbors, node => equate(node, goal));

		/// <summary>Runs the BreadthFirstSearch search algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="graph">The graph to search against.</param>
		/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node>(Node start, IGraph<Node> graph, Goal<Node> goal) =>
			Graph(start, graph.Neighbors, goal);

		#endregion

		#endregion

		#endregion
	}
}
