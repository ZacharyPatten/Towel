using System;
using Towel.DataStructures;
using static Towel.Syntax;

namespace Towel
{
	/// <summary>Static class taht constains algorithms for searching.</summary>
	public static class Search
	{
		#region Binary

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning disable CS1734 // XML comment has a paramref tag, but there is no parameter by that name
#pragma warning disable CS1735 // XML comment has a typeparamref tag, but there is no type parameter by that name
		/// <summary>Performs a binary search on sorted indexed data.</summary>
		/// <typeparam name="T">The type of elements to search through.</typeparam>
		/// <typeparam name="Get">The function for getting an element at an index.</typeparam>
		/// <typeparam name="Sift">The function for sifting through the elements.</typeparam>
		/// <typeparam name="Compare">The compare function.</typeparam>
		/// <param name="index">The starting index of the binary search.</param>
		/// <param name="length">The number of elements to be searched after the starting <paramref name="index"/>.</param>
		/// <param name="get">The function for getting an element at an index.</param>
		/// <param name="sift">The function for comparing the the elements to th desired target.</param>
		/// <param name="array">The array search.</param>
		/// <param name="element">The element to search for.</param>
		/// <param name="compare">The compare function.</param>
		/// <param name="span">The span of the binary search.</param>
		/// <returns>
		/// (<see cref="bool"/> Success, <see cref="int"/> Index, <typeparamref name="T"/> Value)
		/// <para>- <see cref="bool"/> Success: True if a match was found; False if not.</para>
		/// <para>- <see cref="int"/> Index: The resulting index of the search that will always be &lt;= the desired match.</para>
		/// <para>- <typeparamref name="T"/> Value: The resulting value of the binary search if a match was found or default if not.</para>
		/// </returns>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Binary_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1735 // XML comment has a typeparamref tag, but there is no type parameter by that name
#pragma warning restore CS1734 // XML comment has a paramref tag, but there is no parameter by that name
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="Binary_XML"/>
		public static (bool Success, int Index, T Value) Binary<T>(T[] array, T element, Compare<T> compare = default)
		{
			_ = array ?? throw new ArgumentNullException(nameof(array));
			return Binary<T, GetIndexArray<T>, SiftFromCompareAndValue<T, CompareRuntime<T>>>(0, array.Length, array, new SiftFromCompareAndValue<T, CompareRuntime<T>>(element, compare ?? Compare.Default));
		}

		/// <inheritdoc cref="Binary_XML"/>
		public static (bool Success, int Index, T Value) Binary<T, Compare>(T[] array, T element, Compare compare = default)
			where Compare : IFunc<T, T, CompareResult>
		{
			_ = array ?? throw new ArgumentNullException(nameof(array));
			return Binary<T, GetIndexArray<T>, SiftFromCompareAndValue<T, Compare>>(0, array.Length, array, new SiftFromCompareAndValue<T, Compare>(element, compare));
		}

		/// <inheritdoc cref="Binary_XML"/>
		public static (bool Success, int Index, T Value) Binary<T>(T[] array, Sift<T> sift)
		{
			_ = array ?? throw new ArgumentNullException(nameof(array));
			return Binary<T, GetIndexArray<T>, SiftRuntime<T>>(0, array.Length, array, sift);
		}

		/// <inheritdoc cref="Binary_XML"/>
		public static (bool Success, int Index, T Value) Binary<T, Sift>(T[] array, Sift sift = default)
			where Sift : ISift<T>
		{
			_ = array ?? throw new ArgumentNullException(nameof(array));
			return Binary<T, GetIndexArray<T>, Sift>(0, array.Length, array, sift);
		}

		/// <inheritdoc cref="Binary_XML"/>
		public static (bool Success, int Index, T Value) Binary<T>(int length, GetIndex<T> get, Sift<T> sift)
		{
			_ = get ?? throw new ArgumentNullException(nameof(get));
			_ = sift ?? throw new ArgumentNullException(nameof(sift));
			return Binary<T, GetIndexRuntime<T>, SiftRuntime<T>>(0, length, get, sift);
		}

		/// <inheritdoc cref="Binary_XML"/>
		public static (bool Success, int Index, T Value) Binary<T, Get, Sift>(int index, int length, Get get = default, Sift sift = default)
			where Get : IFunc<int, T>
			where Sift : ISift<T>
		{
			if (length <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(length), length, "!(" + nameof(length) + " > 0)");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(index), index, "!(" + nameof(index) + " > 0)");
			}
			if (length > int.MaxValue / 2 && index > int.MaxValue / 2)
			{
				throw new ArgumentOutOfRangeException($"{nameof(length)} > {int.MaxValue / 2} && {nameof(index)} / {int.MaxValue / 2}", default(Exception));
			}
			int low = index;
			int hi = index + length - 1;
			while (low <= hi)
			{
				int median = low + (hi - low) / 2;
				T value = get.Do(median);
				CompareResult compareResult = sift.Do(value);
				switch (compareResult)
				{
					case Less:    low = median + 1; break;
					case Greater: hi  = median - 1; break;
					case Equal:   return (true, median, value);
					default:
						throw compareResult.IsDefined()
						? (Exception)new TowelBugException($"Unhandled {nameof(CompareResult)} value: {compareResult}.")
						: new ArgumentException($"Invalid {nameof(Sift)} function; an undefined {nameof(CompareResult)} was returned.", nameof(sift));
				}
			}
			return (false, Math.Min(low, hi), default);
		}

		/// <inheritdoc cref="Binary_XML"/>
		public static (bool Success, int Index, T Value) Binary<T, Sift>(ReadOnlySpan<T> span, Sift sift = default)
			where Sift : ISift<T>
		{
			int low = 0;
			int hi = span.Length - 1;
			while (low <= hi)
			{
				int median = low + (hi - low) / 2;
				T value = span[median];
				CompareResult compareResult = sift.Do(value);
				switch (compareResult)
				{
					case Less: low = median + 1; break;
					case Greater: hi = median - 1; break;
					case Equal: return (true, median, value);
					default:
						throw compareResult.IsDefined()
						? (Exception)new TowelBugException($"Unhandled {nameof(CompareResult)} value: {compareResult}.")
						: new ArgumentException($"Invalid {nameof(Sift)} function; an undefined {nameof(CompareResult)} was returned.", nameof(sift));
				}
			}
			return (false, Math.Min(low, hi), default);
		}

		#endregion

		#region Graph Algorithms

		#region Publics

		/// <summary>The status of a graph search algorithm.</summary>
		public enum GraphSearchStatus
		{
			#region Members

			/// <summary>Graph search was not broken.</summary>
			Continue = StepStatus.Continue,
			/// <summary>Graph search was broken.</summary>
			Break = StepStatus.Break,
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

		internal struct AStarPriorityCompare<Node, Numeric> : IFunc<AstarNode<Node, Numeric>, AstarNode<Node, Numeric>, CompareResult>
		{
			// NOTE: Typical A* implementations prioritize smaller values
			public CompareResult Do(AstarNode<Node, Numeric> a, AstarNode<Node, Numeric> b) =>
				Comparison(b.Priority, a.Priority);
		}

		internal struct DijkstraPriorityCompare<Node, Numeric> : IFunc<DijkstraNode<Node, Numeric>, DijkstraNode<Node, Numeric>, CompareResult>
		{
			// NOTE: Typical A* implementations prioritize smaller values
			public CompareResult Do(DijkstraNode<Node, Numeric> a, DijkstraNode<Node, Numeric> b) =>
				Comparison(b.Priority, a.Priority);
		}

		#endregion

		#region Graph (Shared)

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="start">The node to start at.</param>
		/// <param name="neighbors">Step function for all neigbors of a given node.</param>
		/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
		/// <param name="cost">Computes the cost of moving from the current node to a specific neighbor.</param>
		/// <param name="check">Checks the status of the search.</param>
		/// <param name="totalCost">The total cost of the path if a path was found.</param>
		/// <param name="goal">The goal of the search.</param>
		/// <param name="graph">The graph to perform the search on.</param>
		/// <returns>Stepper of the shortest path or null if no path exists.</returns>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Graph_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		#endregion

		#region A* Algorithm

		/// <summary>Runs the A* search algorithm on a graph.</summary>
		/// <inheritdoc cref="Graph_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Graph_Astar_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="Graph_Astar_XML"/>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, Neighbors<Node> neighbors, Heuristic<Node, Numeric> heuristic, Cost<Node, Numeric> cost, Goal<Node> goal, out Numeric totalCost) =>
			Graph(start, neighbors, heuristic, cost, node => goal(node) ? GraphSearchStatus.Goal : GraphSearchStatus.Continue, out totalCost);

		/// <inheritdoc cref="Graph_Astar_XML"/>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, IGraph<Node> graph, Heuristic<Node, Numeric> heuristic, Cost<Node, Numeric> cost, Goal<Node> goal, out Numeric totalCost) =>
			Graph(start, graph.Neighbors, heuristic, cost, goal, out totalCost);

		/// <inheritdoc cref="Graph_Astar_XML"/>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, Neighbors<Node> neighbors, Heuristic<Node, Numeric> heuristic, Cost<Node, Numeric> cost, Node goal, out Numeric totalCost) =>
			Graph(start, neighbors, heuristic, cost, goal, Equate.Default, out totalCost);

		/// <inheritdoc cref="Graph_Astar_XML"/>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, Neighbors<Node> neighbors, Heuristic<Node, Numeric> heuristic, Cost<Node, Numeric> cost, Node goal, Equate<Node> equate, out Numeric totalCost) =>
			Graph(start, neighbors, heuristic, cost, node => equate(node, goal), out totalCost);

		/// <inheritdoc cref="Graph_Astar_XML"/>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, IGraph<Node> graph, Heuristic<Node, Numeric> heuristic, Cost<Node, Numeric> cost, Node goal, out Numeric totalCost) =>
			Graph(start, graph, heuristic, cost, goal, Equate.Default, out totalCost);

		/// <inheritdoc cref="Graph_Astar_XML"/>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, IGraph<Node> graph, Heuristic<Node, Numeric> heuristic, Cost<Node, Numeric> cost, Node goal, Equate<Node> equate, out Numeric totalCost) =>
			Graph(start, graph.Neighbors, heuristic, cost, node => equate(node, goal), out totalCost);

		/// <inheritdoc cref="Graph_Astar_XML"/>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, Neighbors<Node> neighbors, Heuristic<Node, Numeric> heuristic, Cost<Node, Numeric> cost, Check<Node> check, out Numeric totalCost)
		{
			// using a heap (aka priority queue) to store nodes based on their computed A* f(n) value
			IHeap<AstarNode<Node, Numeric>> fringe = new HeapArray<AstarNode<Node, Numeric>, AStarPriorityCompare<Node, Numeric>>();

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
				if (status is GraphSearchStatus.Break)
				{
					break;
				}
				else if (status is GraphSearchStatus.Goal)
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

		#endregion

		#region Dijkstra Algorithm

		/// <summary>Runs the Dijkstra search algorithm on a graph.</summary>
		/// <inheritdoc cref="Graph_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Graph_Dijkstra_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="Graph_Dijkstra_XML"/>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, Neighbors<Node> neighbors, Heuristic<Node, Numeric> heuristic, Goal<Node> goal) =>
			Graph(start, neighbors, heuristic, node => goal(node) ? GraphSearchStatus.Goal : GraphSearchStatus.Continue);

		/// <inheritdoc cref="Graph_Dijkstra_XML"/>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, Neighbors<Node> neighbors, Heuristic<Node, Numeric> heuristic, Node goal) =>
			Graph(start, neighbors, heuristic, goal, Equate.Default);

		/// <inheritdoc cref="Graph_Dijkstra_XML"/>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, Neighbors<Node> neighbors, Heuristic<Node, Numeric> heuristic, Node goal, Equate<Node> equate) =>
			Graph(start, neighbors, heuristic, node => equate(node, goal));

		/// <inheritdoc cref="Graph_Dijkstra_XML"/>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, IGraph<Node> graph, Heuristic<Node, Numeric> heuristic, Node goal) =>
			Graph(start, graph, heuristic, goal, Equate.Default);

		/// <inheritdoc cref="Graph_Dijkstra_XML"/>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, IGraph<Node> graph, Heuristic<Node, Numeric> heuristic, Node goal, Equate<Node> equate) =>
			Graph(start, graph.Neighbors, heuristic, node => equate(node, goal));

		/// <inheritdoc cref="Graph_Dijkstra_XML"/>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, IGraph<Node> graph, Heuristic<Node, Numeric> heuristic, Goal<Node> goal) =>
			Graph(start, graph.Neighbors, heuristic, goal);

		/// <inheritdoc cref="Graph_Dijkstra_XML"/>
		public static Stepper<Node> Graph<Node, Numeric>(Node start, Neighbors<Node> neighbors, Heuristic<Node, Numeric> heuristic, Check<Node> check)
		{
			// using a heap (aka priority queue) to store nodes based on their computed heuristic value
			IHeap<DijkstraNode<Node, Numeric>> fringe = new HeapArray<DijkstraNode<Node, Numeric>, DijkstraPriorityCompare<Node, Numeric>>();

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
				if (status is GraphSearchStatus.Break)
				{
					break;
				}
				if (status is GraphSearchStatus.Goal)
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

		#endregion

		#region BreadthFirstSearch Algorithm

		/// <summary>Runs the Breadth-First-Search search algorithm on a graph.</summary>
		/// <inheritdoc cref="Graph_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Graph_BreadthFirstSearch_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="Graph_BreadthFirstSearch_XML"/>
		public static Stepper<Node> Graph<Node>(Node start, Neighbors<Node> neighbors, Goal<Node> goal) =>
			Graph(start, neighbors, node => goal(node) ? GraphSearchStatus.Goal : GraphSearchStatus.Continue);

		/// <inheritdoc cref="Graph_BreadthFirstSearch_XML"/>
		public static Stepper<Node> Graph<Node>(Node start, Neighbors<Node> neighbors, Node goal) =>
			Graph(start, neighbors, goal, Equate.Default);

		/// <inheritdoc cref="Graph_BreadthFirstSearch_XML"/>
		public static Stepper<Node> Graph<Node>(Node start, Neighbors<Node> neighbors, Node goal, Equate<Node> equate) =>
			Graph(start, neighbors, node => equate(node, goal));

		/// <inheritdoc cref="Graph_BreadthFirstSearch_XML"/>
		public static Stepper<Node> Graph<Node>(Node start, IGraph<Node> graph, Node goal) =>
			Graph(start, graph, goal, Equate.Default);

		/// <inheritdoc cref="Graph_BreadthFirstSearch_XML"/>
		public static Stepper<Node> Graph<Node>(Node start, IGraph<Node> graph, Node goal, Equate<Node> equate) =>
			Graph(start, graph.Neighbors, node => equate(node, goal));

		/// <inheritdoc cref="Graph_BreadthFirstSearch_XML"/>
		public static Stepper<Node> Graph<Node>(Node start, IGraph<Node> graph, Goal<Node> goal) =>
			Graph(start, graph.Neighbors, goal);

		/// <inheritdoc cref="Graph_BreadthFirstSearch_XML"/>
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
				if (status is GraphSearchStatus.Break)
				{
					break;
				}
				if (status is GraphSearchStatus.Goal)
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

		#endregion

		#endregion
	}
}
