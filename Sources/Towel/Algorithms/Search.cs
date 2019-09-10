using System;
using Towel.DataStructures;
using Towel.Mathematics;

namespace Towel.Algorithms
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
			if (get is null)
			{
				throw new ArgumentNullException(nameof(get));
			}
			if (compare is null)
			{
				throw new ArgumentNullException(nameof(compare));
			}
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
					case CompareResult.Equal:
						return median;
					case CompareResult.Less:
						low = median + 1;
						break;
					case CompareResult.Greater:
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

		#region Delegates

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

		#endregion

		#region Classes

		internal abstract class BaseAlgorithmNode<AlgorithmNode, Node>
			where AlgorithmNode : BaseAlgorithmNode<AlgorithmNode, Node>
		{
			internal AlgorithmNode Previous;
			internal Node Value;
		}

		internal class GreedyNode<Node, Numeric> : BaseAlgorithmNode<GreedyNode<Node, Numeric>, Node>
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

		#endregion

		#region Internal Fuctions

		internal static Stepper<Node> BuildPath<AlgorithmNode, Node, Numeric>(BaseAlgorithmNode<AlgorithmNode, Node> node)
			where AlgorithmNode : BaseAlgorithmNode<AlgorithmNode, Node>
		{
			PathNode<Node> start = null;
			for (BaseAlgorithmNode<AlgorithmNode, Node> current = node; current != null; current = current.Previous)
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
				while (current != null)
				{
					step(current.Value);
					current = current.Next;
				}
			};
		}

		#endregion

		#region A* Algorithm

		/// <summary>Runs the A* search algorithm algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="neighbors">Step function for all neigbors of a given node.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="cost">Computes the cost of moving from the current node to a specific neighbor.</param>
		/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, Neighbors<Node> neighbors, Heuristic<Node, Numeric> heuristic, Cost<Node, Numeric> cost, Goal<Node> goal)
		{
			// using a heap (aka priority queue) to store nodes based on their computed A* f(n) value
			IHeap<AstarNode<Node, Numeric>> fringe = new HeapArray<AstarNode<Node, Numeric>>(
				// NOTE: Typical A* implementations prioritize smaller values
				(a, b) => Compute.Compare(b.Priority, a.Priority));

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
				if (goal(current.Value))
				{
					return BuildPath<AstarNode<Node, Numeric>, Node, Numeric>(current);
				}
				else
				{
					neighbors(current.Value,
						neighbor =>
						{
							Numeric costValue = Compute.Add(current.Cost, cost(current.Value, neighbor));
							fringe.Enqueue(
								new AstarNode<Node, Numeric>()
								{
									Previous = current,
									Value = neighbor,
									Priority = Compute.Add(heuristic(neighbor), costValue),
									Cost = costValue,
								});
						});
				}
			}
			return null; // goal node was not reached (no path exists)
		}

		#region A* overloads

		/// <summary>Runs the A* search algorithm algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="graph">The graph to perform the search on.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="cost">Computes the cost of moving from the current node to a specific neighbor.</param>
		/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, IGraph<Node> graph, Heuristic<Node, Numeric> heuristic, Cost<Node, Numeric> cost, Goal<Node> goal) =>
			Graph(start, graph.Neighbors, heuristic, cost, goal);

		/// <summary>Runs the A* search algorithm algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="neighbors">Delegate for gettign the neighbors of a node.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="cost">Computes the cost of moving from the current node to a specific neighbor.</param>
		/// <param name="goal">The goal node.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, Neighbors<Node> neighbors, Heuristic<Node, Numeric> heuristic, Cost<Node, Numeric> cost, Node goal) =>
			Graph(start, neighbors, heuristic, cost, goal, Equate.Default);

		/// <summary>Runs the A* search algorithm algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="neighbors">Delegate for gettign the neighbors of a node.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="cost">Computes the cost of moving from the current node to a specific neighbor.</param>
		/// <param name="goal">The goal node.</param>
		/// <param name="equate">A delegate for checking for equality between two nodes.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, Neighbors<Node> neighbors, Heuristic<Node, Numeric> heuristic, Cost<Node, Numeric> cost, Node goal, Equate<Node> equate) =>
			Graph(start, neighbors, heuristic, cost, node => equate(node, goal));

		/// <summary>Runs the A* search algorithm algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="graph">The graph to perform the search on.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="cost">Computes the cost of moving from the current node to a specific neighbor.</param>
		/// <param name="goal">The goal node.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, IGraph<Node> graph, Heuristic<Node, Numeric> heuristic, Cost<Node, Numeric> cost, Node goal) =>
			Graph(start, graph, heuristic, cost, goal, Equate.Default);

		/// <summary>Runs the A* search algorithm algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="graph">The graph to perform the search on.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="cost">Computes the cost of moving from the current node to a specific neighbor.</param>
		/// <param name="goal">The goal node.</param>
		/// <param name="equate">A delegate for checking for equality between two nodes.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, IGraph<Node> graph, Heuristic<Node, Numeric> heuristic, Cost<Node, Numeric> cost, Node goal, Equate<Node> equate) =>
			Graph(start, graph.Neighbors, heuristic, cost, (Node node) => { return equate(node, goal); });

		#endregion

		#endregion

		#region Greedy Algorithm

		/// <summary>Runs the Greedy search algorithm algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="neighbors">Step function for all neigbors of a given node.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, Neighbors<Node> neighbors, Heuristic<Node, Numeric> heuristic, Goal<Node> goal)
		{
			// using a heap (aka priority queue) to store nodes based on their computed heuristic value
			IHeap<GreedyNode<Node, Numeric>> fringe = new HeapArray<GreedyNode<Node, Numeric>>(
				// NOTE: Typical graph search implementations prioritize smaller values
				(a, b) => Compute.Compare(b.Priority, a.Priority));

			// push starting node
			fringe.Enqueue(
				new GreedyNode<Node, Numeric>()
				{
					Previous = null,
					Value = start,
					Priority = default,
				});

			// run the algorithm
			while (fringe.Count != 0)
			{
				GreedyNode<Node, Numeric> current = fringe.Dequeue();
				if (goal(current.Value))
				{
					return BuildPath<GreedyNode<Node, Numeric>, Node, Numeric>(current);
				}
				else
				{
					neighbors(current.Value,
						neighbor =>
						{
							fringe.Enqueue(
								new GreedyNode<Node, Numeric>()
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

		#region Greedy Overloads

		/// <summary>Runs the Greedy search algorithm algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="neighbors">Step function for all neigbors of a given node.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, Neighbors<Node> neighbors, Heuristic<Node, Numeric> heuristic, Node goal) =>
			Graph(start, neighbors, heuristic, goal, Equate.Default);

		/// <summary>Runs the Greedy search algorithm algorithm on a graph.</summary>
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

		/// <summary>Runs the Greedy search algorithm algorithm on a graph.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="graph">The graph to search against.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="goal">Predicate for determining if we have reached the goal node.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, IGraph<Node> graph, Heuristic<Node, Numeric> heuristic, Node goal) =>
			Graph(start, graph, heuristic, goal, Equate.Default);

		/// <summary>Runs the Greedy search algorithm algorithm on a graph.</summary>
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

		/// <summary>Runs the Greedy search algorithm algorithm on a graph.</summary>
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

		#endregion
	}
}
