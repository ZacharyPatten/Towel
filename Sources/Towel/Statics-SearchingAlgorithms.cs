using System;
using Towel.DataStructures;

namespace Towel
{
	/// <summary>Root type of the static functional methods in Towel.</summary>
	public static partial class Statics
	{
		#region SearchBinary

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning disable CS1734 // XML comment has a paramref tag, but there is no parameter by that name
#pragma warning disable CS1735 // XML comment has a typeparamref tag, but there is no type parameter by that name
		/// <summary>Performs a binary search on sorted indexed data.</summary>
		/// <typeparam name="T">The type of values to search through.</typeparam>
		/// <typeparam name="Get">The function for getting an element at an index.</typeparam>
		/// <typeparam name="Sift">The function for sifting through the values.</typeparam>
		/// <typeparam name="Compare">The compare function.</typeparam>
		/// <param name="index">The starting index of the binary search.</param>
		/// <param name="length">The number of values to be searched after the starting <paramref name="index"/>.</param>
		/// <param name="get">The function for getting an element at an index.</param>
		/// <param name="sift">The function for comparing the the values to th desired target.</param>
		/// <param name="array">The array search.</param>
		/// <param name="element">The element to search for.</param>
		/// <param name="compare">The compare function.</param>
		/// <param name="span">The span of the binary search.</param>
		/// <returns>
		/// (<see cref="bool"/> Found, <see cref="int"/> Index, <typeparamref name="T"/> Value)
		/// <para>- <see cref="bool"/> Found: True if a match was found; False if not.</para>
		/// <para>- <see cref="int"/> Index: The resulting index of the search that will always be &lt;= the desired match.</para>
		/// <para>- <typeparamref name="T"/> Value: The resulting value of the binary search if a match was found or default if not.</para>
		/// </returns>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SearchBinary_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1735 // XML comment has a typeparamref tag, but there is no type parameter by that name
#pragma warning restore CS1734 // XML comment has a paramref tag, but there is no parameter by that name
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="SearchBinary_XML"/>
		public static (bool Found, int Index, T? Value) SearchBinary<T>(int length, Func<int, T> get, Func<T, CompareResult> sift)
		{
			_ = get ?? throw new ArgumentNullException(nameof(get));
			_ = sift ?? throw new ArgumentNullException(nameof(sift));
			return SearchBinary<T, SFunc<int, T>, SFunc<T, CompareResult>>(0, length, get, sift);
		}

		/// <inheritdoc cref="SearchBinary_XML"/>
		public static (bool Found, int Index, T? Value) SearchBinary<T, TGet, TSift>(int index, int length, TGet get = default, TSift sift = default)
			where TGet : struct, IFunc<int, T>
			where TSift : struct, IFunc<T, CompareResult>
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
				T value = get.Invoke(median);
				CompareResult compareResult = sift.Invoke(value);
				switch (compareResult)
				{
					case Less: low = median + 1; break;
					case Greater: hi = median - 1; break;
					case Equal: return (true, median, value);
					default:
						throw compareResult.IsDefined()
						? (Exception)new TowelBugException($"Unhandled {nameof(CompareResult)} value: {compareResult}.")
						: new ArgumentException($"Invalid {nameof(TSift)} function; an undefined {nameof(CompareResult)} was returned.", nameof(sift));
				}
			}
			return (false, Math.Min(low, hi), default);
		}

		/// <inheritdoc cref="SearchBinary_XML"/>
		public static (bool Found, int Index, T? Value) SearchBinary<T>(ReadOnlySpan<T> span, T element, Func<T, T, CompareResult>? compare = default) =>
			SearchBinary<T, SiftFromCompareAndValue<T, SFunc<T, T, CompareResult>>>(span, new SiftFromCompareAndValue<T, SFunc<T, T, CompareResult>>(element, compare ?? Compare));

		/// <inheritdoc cref="SearchBinary_XML"/>
		public static (bool Found, int Index, T? Value) SearchBinary<T>(ReadOnlySpan<T> span, Func<T, CompareResult> sift)
		{
			_ = sift ?? throw new ArgumentNullException(nameof(sift));
			return SearchBinary<T, SFunc<T, CompareResult>>(span, sift);
		}

		/// <inheritdoc cref="SearchBinary_XML"/>
		public static (bool Found, int Index, T? Value) SearchBinary<T, TCompare>(ReadOnlySpan<T> span, T element, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult> =>
			SearchBinary<T, SiftFromCompareAndValue<T, TCompare>>(span, new SiftFromCompareAndValue<T, TCompare>(element, compare));

		/// <inheritdoc cref="SearchBinary_XML"/>
		public static (bool Found, int Index, T? Value) SearchBinary<T, TSift>(ReadOnlySpan<T> span, TSift sift = default)
			where TSift : struct, IFunc<T, CompareResult>
		{
			if (span.IsEmpty)
			{
				throw new ArgumentException($@"{nameof(span)}.{nameof(span.IsEmpty)}", nameof(span));
			}
			int low = 0;
			int hi = span.Length - 1;
			while (low <= hi)
			{
				int median = low + (hi - low) / 2;
				T value = span[median];
				CompareResult compareResult = sift.Invoke(value);
				switch (compareResult)
				{
					case Less: low = median + 1; break;
					case Greater: hi = median - 1; break;
					case Equal: return (true, median, value);
					default:
						throw compareResult.IsDefined()
						? (Exception)new TowelBugException($"Unhandled {nameof(CompareResult)} value: {compareResult}.")
						: new ArgumentException($"Invalid {nameof(TSift)} function; an undefined {nameof(CompareResult)} was returned.", nameof(sift));
				}
			}
			return (false, Math.Min(low, hi), default);
		}

		#endregion

		#region Search Graph Algorithms

		#region Publics

		/// <summary>The status of a graph search algorithm.</summary>
		public enum GraphSearchStatus
		{
			/// <summary>Graph search was not broken.</summary>
			Continue = StepStatus.Continue,
			/// <summary>Graph search was broken.</summary>
			Break = StepStatus.Break,
			/// <summary>Graph search found the goal.</summary>
			Goal = 2,
		}

		/// <summary>Syntax sugar hacks.</summary>
		public static class GraphSyntax
		{
			/// <summary>This is a syntax sugar hack to allow implicit conversions from StepStatus to GraphSearchStatus.</summary>
			public struct GraphSearchStatusStruct
			{
				internal readonly GraphSearchStatus Value;
				/// <summary>Constructs a new graph search status.</summary>
				/// <param name="value">The status of the graph search.</param>
				public GraphSearchStatusStruct(GraphSearchStatus value) => Value = value;
				/// <summary>Converts a <see cref="GraphSearchStatus"/> into a <see cref="GraphSearchStatusStruct"/>.</summary>
				/// <param name="value">The <see cref="GraphSearchStatus"/> to convert.</param>
				public static implicit operator GraphSearchStatusStruct(GraphSearchStatus value) => new(value);
				/// <summary>Converts a <see cref="StepStatus"/> into a <see cref="GraphSearchStatusStruct"/>.</summary>
				/// <param name="value">The <see cref="StepStatus"/> to convert.</param>
				public static implicit operator GraphSearchStatusStruct(StepStatus value) => new((GraphSearchStatus)value);
				/// <summary>Converts a <see cref="GraphSearchStatusStruct"/> into a <see cref="GraphSearchStatus"/>.</summary>
				/// <param name="value">The <see cref="GraphSearchStatusStruct"/> to convert.</param>
				public static implicit operator GraphSearchStatus(GraphSearchStatusStruct value) => value.Value;
			}
		}

		/// <summary>Step function for all neigbors of a given node.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <param name="current">The node to step through all the neighbors of.</param>
		/// <param name="neighbors">Step function to perform on all neighbors.</param>
		public delegate void SearchNeighbors<Node>(Node current, Action<Node> neighbors);
		/// <summary>Computes the heuristic value of a given node in a graph (smaller values mean closer to goal node).</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="node">The node to compute the heuristic value of.</param>
		/// <returns>The computed heuristic value for this node.</returns>
		public delegate Numeric SearchHeuristic<Node, Numeric>(Node node);
		/// <summary>Computes the cost of moving from the current node to a specific neighbor.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <typeparam name="Numeric">The numeric to use when performing calculations.</typeparam>
		/// <param name="current">The current (starting) node.</param>
		/// <param name="neighbor">The node to compute the cost of movign to.</param>
		/// <returns>The computed cost value of movign from current to neighbor.</returns>
		public delegate Numeric SearchCost<Node, Numeric>(Node current, Node neighbor);
		/// <summary>Predicate for determining if we have reached the goal node.</summary>
		/// <typeparam name="Node">The node type of the graph being searched.</typeparam>
		/// <param name="current">The current node.</param>
		/// <returns>True if the current node is a/the goal node; False if not.</returns>
		public delegate bool SearchGoal<Node>(Node current);
		/// <summary>Checks the status of a graph search.</summary>
		/// <typeparam name="Node">The node type of the search.</typeparam>
		/// <param name="current">The current node of the search.</param>
		/// <returns>The status of the search.</returns>
		public delegate GraphSyntax.GraphSearchStatusStruct SearchCheck<Node>(Node current);

		#endregion

		#region Internals

		internal abstract class BaseAlgorithmNode<AlgorithmNode, Node>
			where AlgorithmNode : BaseAlgorithmNode<AlgorithmNode, Node>
		{
			internal AlgorithmNode? Previous;
			internal Node Value;

			internal BaseAlgorithmNode(Node value, AlgorithmNode? previous = null)
			{
				Value = value;
				Previous = previous;
			}
		}

		internal class BreadthFirstSearch<Node> : BaseAlgorithmNode<BreadthFirstSearch<Node>, Node>
		{
			internal BreadthFirstSearch(Node value, BreadthFirstSearch<Node>? previous = null)
				: base(value: value, previous: previous) { }
		}

		internal class DijkstraNode<Node, Numeric> : BaseAlgorithmNode<DijkstraNode<Node, Numeric>, Node>
		{
			internal Numeric Priority;

			internal DijkstraNode(Node value, Numeric priority, DijkstraNode<Node, Numeric>? previous = null)
				: base(value: value, previous: previous)
			{
				Priority = priority;
			}
		}

		internal class AstarNode<Node, Numeric> : BaseAlgorithmNode<AstarNode<Node, Numeric>, Node>
		{
			internal Numeric Priority;
			internal Numeric Cost;

			internal AstarNode(Node value, Numeric priority, Numeric cost, AstarNode<Node, Numeric>? previous = null)
				: base(value: value, previous: previous)
			{
				Priority = priority;
				Cost = cost;
			}
		}

		internal class PathNode<Node>
		{
			internal Node Value;
			internal PathNode<Node>? Next;

			internal PathNode(Node value, PathNode<Node>? next = null)
			{
				Value = value;
				Next = next;
			}
		}

		internal static Action<Action<Node>> BuildPath<AlgorithmNode, Node>(BaseAlgorithmNode<AlgorithmNode, Node> node)
			where AlgorithmNode : BaseAlgorithmNode<AlgorithmNode, Node>
		{
			PathNode<Node>? start = null;
			for (BaseAlgorithmNode<AlgorithmNode, Node>? current = node; current is not null; current = current.Previous)
			{
				PathNode<Node>? temp = start;
				start = new PathNode<Node>(
					value: current.Value,
					next: temp);
			}
			return step =>
			{
				PathNode<Node>? current = start;
				while (current is not null)
				{
					step(current.Value);
					current = current.Next;
				}
			};
		}

		internal struct AStarPriorityCompare<Node, Numeric> : IFunc<AstarNode<Node, Numeric>, AstarNode<Node, Numeric>, CompareResult>
		{
			// NOTE: Typical A* implementations prioritize smaller values
			public CompareResult Invoke(AstarNode<Node, Numeric> a, AstarNode<Node, Numeric> b) =>
				Compare(b.Priority, a.Priority);
		}

		internal struct DijkstraPriorityCompare<Node, Numeric> : IFunc<DijkstraNode<Node, Numeric>, DijkstraNode<Node, Numeric>, CompareResult>
		{
			// NOTE: Typical A* implementations prioritize smaller values
			public CompareResult Invoke(DijkstraNode<Node, Numeric> a, DijkstraNode<Node, Numeric> b) =>
				Compare(b.Priority, a.Priority);
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
		internal static void SearchGraph_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		#endregion

		#region A* Algorithm

		/// <summary>Runs the A* search algorithm on a graph.</summary>
		/// <inheritdoc cref="SearchGraph_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Graph_Astar_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="Graph_Astar_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, SearchNeighbors<Node> neighbors, SearchHeuristic<Node, Numeric> heuristic, SearchCost<Node, Numeric> cost, SearchGoal<Node> goal, out Numeric? totalCost) =>
			SearchGraph(start, neighbors, heuristic, cost, node => goal(node) ? GraphSearchStatus.Goal : GraphSearchStatus.Continue, out totalCost);

		/// <inheritdoc cref="Graph_Astar_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, IGraph<Node> graph, SearchHeuristic<Node, Numeric> heuristic, SearchCost<Node, Numeric> cost, SearchGoal<Node> goal, out Numeric? totalCost) =>
			SearchGraph(start, graph.Neighbors, heuristic, cost, goal, out totalCost);

		/// <inheritdoc cref="Graph_Astar_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, SearchNeighbors<Node> neighbors, SearchHeuristic<Node, Numeric> heuristic, SearchCost<Node, Numeric> cost, Node goal, out Numeric? totalCost) =>
			SearchGraph(start, neighbors, heuristic, cost, goal, Equate, out totalCost);

		/// <inheritdoc cref="Graph_Astar_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, SearchNeighbors<Node> neighbors, SearchHeuristic<Node, Numeric> heuristic, SearchCost<Node, Numeric> cost, Node goal, Func<Node, Node, bool> equate, out Numeric? totalCost) =>
			SearchGraph(start, neighbors, heuristic, cost, node => equate(node, goal), out totalCost);

		/// <inheritdoc cref="Graph_Astar_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, IGraph<Node> graph, SearchHeuristic<Node, Numeric> heuristic, SearchCost<Node, Numeric> cost, Node goal, out Numeric? totalCost) =>
			SearchGraph(start, graph, heuristic, cost, goal, Equate, out totalCost);

		/// <inheritdoc cref="Graph_Astar_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, IGraph<Node> graph, SearchHeuristic<Node, Numeric> heuristic, SearchCost<Node, Numeric> cost, Node goal, Func<Node, Node, bool> equate, out Numeric? totalCost) =>
			SearchGraph(start, graph.Neighbors, heuristic, cost, node => equate(node, goal), out totalCost);

		/// <inheritdoc cref="Graph_Astar_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, SearchNeighbors<Node> neighbors, SearchHeuristic<Node, Numeric> heuristic, SearchCost<Node, Numeric> cost, SearchCheck<Node> check, out Numeric? totalCost)
		{
			// using a heap (aka priority queue) to store nodes based on their computed A* f(n) value
			HeapArray<AstarNode<Node, Numeric>, AStarPriorityCompare<Node, Numeric>> fringe = new();

			// push starting node
			fringe.Enqueue(
				new AstarNode<Node, Numeric>(
					value: start,
					priority: Constant<Numeric>.Zero,
					cost: Constant<Numeric>.Zero,
					previous: null));

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
								new AstarNode<Node, Numeric>(
									value: neighbor,
									priority: Addition(heuristic(neighbor), costValue),
									cost: costValue,
									previous: current));
						});
				}
			}
			totalCost = default;
			return null; // goal node was not reached (no path exists)
		}

		#endregion

		#region Dijkstra Algorithm

		/// <summary>Runs the Dijkstra search algorithm on a graph.</summary>
		/// <inheritdoc cref="SearchGraph_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Graph_Dijkstra_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="Graph_Dijkstra_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, SearchNeighbors<Node> neighbors, SearchHeuristic<Node, Numeric> heuristic, SearchGoal<Node> goal) =>
			SearchGraph(start, neighbors, heuristic, node => goal(node) ? GraphSearchStatus.Goal : GraphSearchStatus.Continue);

		/// <inheritdoc cref="Graph_Dijkstra_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, SearchNeighbors<Node> neighbors, SearchHeuristic<Node, Numeric> heuristic, Node goal) =>
			SearchGraph(start, neighbors, heuristic, goal, Equate);

		/// <inheritdoc cref="Graph_Dijkstra_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, SearchNeighbors<Node> neighbors, SearchHeuristic<Node, Numeric> heuristic, Node goal, Func<Node, Node, bool> equate) =>
			SearchGraph(start, neighbors, heuristic, node => equate(node, goal));

		/// <inheritdoc cref="Graph_Dijkstra_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, IGraph<Node> graph, SearchHeuristic<Node, Numeric> heuristic, Node goal) =>
			SearchGraph(start, graph, heuristic, goal, Equate);

		/// <inheritdoc cref="Graph_Dijkstra_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, IGraph<Node> graph, SearchHeuristic<Node, Numeric> heuristic, Node goal, Func<Node, Node, bool> equate) =>
			SearchGraph(start, graph.Neighbors, heuristic, node => equate(node, goal));

		/// <inheritdoc cref="Graph_Dijkstra_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, IGraph<Node> graph, SearchHeuristic<Node, Numeric> heuristic, SearchGoal<Node> goal) =>
			SearchGraph(start, graph.Neighbors, heuristic, goal);

		/// <inheritdoc cref="Graph_Dijkstra_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node, Numeric>(Node start, SearchNeighbors<Node> neighbors, SearchHeuristic<Node, Numeric> heuristic, SearchCheck<Node> check)
		{
			// using a heap (aka priority queue) to store nodes based on their computed heuristic value
			HeapArray<DijkstraNode<Node, Numeric>, DijkstraPriorityCompare<Node, Numeric>> fringe = new();

			// push starting node
			fringe.Enqueue(
				new DijkstraNode<Node, Numeric>(
					value: start,
					priority: Constant<Numeric>.Zero,
					previous: null));

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
								new DijkstraNode<Node, Numeric>(
									value: neighbor,
									priority: heuristic(neighbor),
									previous: current));
						});
				}
			}
			return null; // goal node was not reached (no path exists)
		}

		#endregion

		#region BreadthFirstSearch Algorithm

		/// <summary>Runs the Breadth-First-Search search algorithm on a graph.</summary>
		/// <inheritdoc cref="SearchGraph_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Graph_BreadthFirstSearch_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="Graph_BreadthFirstSearch_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node>(Node start, SearchNeighbors<Node> neighbors, SearchGoal<Node> goal) =>
			SearchGraph(start, neighbors, node => goal(node) ? GraphSearchStatus.Goal : GraphSearchStatus.Continue);

		/// <inheritdoc cref="Graph_BreadthFirstSearch_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node>(Node start, SearchNeighbors<Node> neighbors, Node goal) =>
			SearchGraph(start, neighbors, goal, Equate);

		/// <inheritdoc cref="Graph_BreadthFirstSearch_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node>(Node start, SearchNeighbors<Node> neighbors, Node goal, Func<Node, Node, bool> equate) =>
			SearchGraph(start, neighbors, node => equate(node, goal));

		/// <inheritdoc cref="Graph_BreadthFirstSearch_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node>(Node start, IGraph<Node> graph, Node goal) =>
			SearchGraph(start, graph, goal, Equate);

		/// <inheritdoc cref="Graph_BreadthFirstSearch_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node>(Node start, IGraph<Node> graph, Node goal, Func<Node, Node, bool> equate) =>
			SearchGraph(start, graph.Neighbors, node => equate(node, goal));

		/// <inheritdoc cref="Graph_BreadthFirstSearch_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node>(Node start, IGraph<Node> graph, SearchGoal<Node> goal) =>
			SearchGraph(start, graph.Neighbors, goal);

		/// <inheritdoc cref="Graph_BreadthFirstSearch_XML"/>
		public static Action<Action<Node>>? SearchGraph<Node>(Node start, SearchNeighbors<Node> neighbors, SearchCheck<Node> check)
		{
			IQueue<BreadthFirstSearch<Node>> fringe = new QueueLinked<BreadthFirstSearch<Node>>();

			// push starting node
			fringe.Enqueue(
				new BreadthFirstSearch<Node>(
					value: start,
					previous: null));

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
								new BreadthFirstSearch<Node>(
									value: neighbor,
									previous: current));
						});
				}
			}
			return null; // goal node was not reached (no path exists)
		}

		#endregion

		#endregion
	}
}
