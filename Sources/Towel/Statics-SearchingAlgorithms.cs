namespace Towel;

/// <summary>Root type of the static functional methods in Towel.</summary>
public static partial class Statics
{
	#region SearchBinary

#pragma warning disable CS1711, CS1572, CS1734, CS1735, SA1617

	/// <summary>Performs a binary search on sorted indexed data.</summary>
	/// <typeparam name="T">The type of values to search through.</typeparam>
	/// <typeparam name="TGet">The type of function for getting an element at an index.</typeparam>
	/// <typeparam name="TSift">The type of function for sifting through the values.</typeparam>
	/// <typeparam name="TCompare">The type of compare function.</typeparam>
	/// <param name="index">The starting index of the binary search.</param>
	/// <param name="length">The number of values to be searched after the starting <paramref name="index"/>.</param>
	/// <param name="get">The function for getting an element at an index.</param>
	/// <param name="sift">The function for comparing the the values to th desired target.</param>
	/// <param name="array">The array search.</param>
	/// <param name="element">The element to search for.</param>
	/// <param name="compare">The compare function.</param>
	/// <param name="span">The span of the binary search.</param>
	/// <returns>
	/// - <see cref="bool"/> Found: True if a match was found; False if not.<br/>
	/// - <see cref="int"/> Index: The resulting index of the search that will always be &lt;= the desired match.<br/>
	/// - <typeparamref name="T"/> Value: The resulting value of the binary search if a match was found or default if not.
	/// </returns>
	[Obsolete(NotIntended, true)]
	public static void XML_SearchBinary() => throw new DocumentationMethodException();

#pragma warning restore CS1711, CS1572, CS1734, CS1735, SA1617

	/// <inheritdoc cref="XML_SearchBinary"/>
	public static (bool Found, int Index, T? Value) SearchBinary<T>(int length, Func<int, T> get, Func<T, CompareResult> sift)
	{
		if (get is null) throw new ArgumentNullException(nameof(get));
		if (sift is null) throw new ArgumentNullException(nameof(sift));
		return SearchBinary<T, SFunc<int, T>, SFunc<T, CompareResult>>(0, length, get, sift);
	}

	/// <inheritdoc cref="XML_SearchBinary"/>
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

	/// <inheritdoc cref="XML_SearchBinary"/>
	public static (bool Found, int Index, T? Value) SearchBinary<T>(ReadOnlySpan<T> span, T element, Func<T, T, CompareResult>? compare = default) =>
		SearchBinary<T, SiftFromCompareAndValue<T, SFunc<T, T, CompareResult>>>(span, new SiftFromCompareAndValue<T, SFunc<T, T, CompareResult>>(element, compare ?? Compare));

	/// <inheritdoc cref="XML_SearchBinary"/>
	public static (bool Found, int Index, T? Value) SearchBinary<T>(ReadOnlySpan<T> span, Func<T, CompareResult> sift)
	{
		if (sift is null) throw new ArgumentNullException(nameof(sift));
		return SearchBinary<T, SFunc<T, CompareResult>>(span, sift);
	}

	/// <inheritdoc cref="XML_SearchBinary"/>
	public static (bool Found, int Index, T? Value) SearchBinary<T, TCompare>(ReadOnlySpan<T> span, T element, TCompare compare = default)
		where TCompare : struct, IFunc<T, T, CompareResult> =>
		SearchBinary<T, SiftFromCompareAndValue<T, TCompare>>(span, new SiftFromCompareAndValue<T, TCompare>(element, compare));

	/// <inheritdoc cref="XML_SearchBinary"/>
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
	/// <typeparam name="TNode">The node type of the graph being searched.</typeparam>
	/// <param name="current">The node to step through all the neighbors of.</param>
	/// <param name="neighbors">Step function to perform on all neighbors.</param>
	public delegate void SearchNeighbors<TNode>(TNode current, Action<TNode> neighbors);
	/// <summary>Computes the heuristic value of a given node in a graph (smaller values mean closer to goal node).</summary>
	/// <typeparam name="TNode">The node type of the graph being searched.</typeparam>
	/// <typeparam name="TNumeric">The numeric to use when performing calculations.</typeparam>
	/// <param name="node">The node to compute the heuristic value of.</param>
	/// <returns>The computed heuristic value for this node.</returns>
	public delegate TNumeric SearchHeuristic<TNode, TNumeric>(TNode node);
	/// <summary>Computes the cost of moving from the current node to a specific neighbor.</summary>
	/// <typeparam name="TNode">The node type of the graph being searched.</typeparam>
	/// <typeparam name="TNumeric">The numeric to use when performing calculations.</typeparam>
	/// <param name="current">The current (starting) node.</param>
	/// <param name="neighbor">The node to compute the cost of movign to.</param>
	/// <returns>The computed cost value of movign from current to neighbor.</returns>
	public delegate TNumeric SearchCost<TNode, TNumeric>(TNode current, TNode neighbor);
	/// <summary>Predicate for determining if we have reached the goal node.</summary>
	/// <typeparam name="TNode">The node type of the graph being searched.</typeparam>
	/// <param name="current">The current node.</param>
	/// <returns>True if the current node is a/the goal node; False if not.</returns>
	public delegate bool SearchGoal<TNode>(TNode current);
	/// <summary>Checks the status of a graph search.</summary>
	/// <typeparam name="TNode">The node type of the search.</typeparam>
	/// <param name="current">The current node of the search.</param>
	/// <returns>The status of the search.</returns>
	public delegate GraphSyntax.GraphSearchStatusStruct SearchCheck<TNode>(TNode current);

	#endregion

	#region Internals

	internal abstract class BaseAlgorithmNode<TAlgorithmNode, TNode>
		where TAlgorithmNode : BaseAlgorithmNode<TAlgorithmNode, TNode>
	{
		internal TAlgorithmNode? Previous;
		internal TNode Value;

		internal BaseAlgorithmNode(TNode value, TAlgorithmNode? previous = null)
		{
			Value = value;
			Previous = previous;
		}
	}

	internal class BreadthFirstSearch<TNode> : BaseAlgorithmNode<BreadthFirstSearch<TNode>, TNode>
	{
		internal BreadthFirstSearch(TNode value, BreadthFirstSearch<TNode>? previous = null)
			: base(value: value, previous: previous) { }
	}

	internal class DijkstraNode<TNode, TNumeric> : BaseAlgorithmNode<DijkstraNode<TNode, TNumeric>, TNode>
	{
		internal TNumeric Priority;

		internal DijkstraNode(TNode value, TNumeric priority, DijkstraNode<TNode, TNumeric>? previous = null)
			: base(value: value, previous: previous)
		{
			Priority = priority;
		}
	}

	internal class AstarNode<TNode, TNumeric> : BaseAlgorithmNode<AstarNode<TNode, TNumeric>, TNode>
	{
		internal TNumeric Priority;
		internal TNumeric Cost;

		internal AstarNode(TNode value, TNumeric priority, TNumeric cost, AstarNode<TNode, TNumeric>? previous = null)
			: base(value: value, previous: previous)
		{
			Priority = priority;
			Cost = cost;
		}
	}

	internal class PathNode<TNode>
	{
		internal TNode Value;
		internal PathNode<TNode>? Next;

		internal PathNode(TNode value, PathNode<TNode>? next = null)
		{
			Value = value;
			Next = next;
		}
	}

	internal static Action<Action<TNode>> BuildPath<TAlgorithmNode, TNode>(BaseAlgorithmNode<TAlgorithmNode, TNode> node)
		where TAlgorithmNode : BaseAlgorithmNode<TAlgorithmNode, TNode>
	{
		PathNode<TNode>? start = null;
		for (BaseAlgorithmNode<TAlgorithmNode, TNode>? current = node; current is not null; current = current.Previous)
		{
			PathNode<TNode>? temp = start;
			start = new PathNode<TNode>(
				value: current.Value,
				next: temp);
		}
		return step =>
		{
			PathNode<TNode>? current = start;
			while (current is not null)
			{
				step(current.Value);
				current = current.Next;
			}
		};
	}

	internal struct AStarPriorityCompare<TNode, TNumeric> : IFunc<AstarNode<TNode, TNumeric>, AstarNode<TNode, TNumeric>, CompareResult>
	{
		// NOTE: Typical A* implementations prioritize smaller values
		public CompareResult Invoke(AstarNode<TNode, TNumeric> a, AstarNode<TNode, TNumeric> b) =>
			Compare(b.Priority, a.Priority);
	}

	internal struct DijkstraPriorityCompare<TNode, TNumeric> : IFunc<DijkstraNode<TNode, TNumeric>, DijkstraNode<TNode, TNumeric>, CompareResult>
	{
		// NOTE: Typical A* implementations prioritize smaller values
		public CompareResult Invoke(DijkstraNode<TNode, TNumeric> a, DijkstraNode<TNode, TNumeric> b) =>
			Compare(b.Priority, a.Priority);
	}

	#endregion

	#region Graph (Shared)

#pragma warning disable CS1711, CS1572, SA1604, SA1617

	/// <typeparam name="TNode">The node type of the graph being searched.</typeparam>
	/// <typeparam name="TNumeric">The numeric to use when performing calculations.</typeparam>
	/// <param name="start">The node to start at.</param>
	/// <param name="neighbors">Step function for all neigbors of a given node.</param>
	/// <param name="heuristic">Computes the heuristic value of a given node in a graph.</param>
	/// <param name="cost">Computes the cost of moving from the current node to a specific neighbor.</param>
	/// <param name="check">Checks the status of the search.</param>
	/// <param name="totalCost">The total cost of the path if a path was found.</param>
	/// <param name="goal">The goal of the search.</param>
	/// <param name="graph">The graph to perform the search on.</param>
	/// <returns>Stepper of the shortest path or null if no path exists.</returns>
	[Obsolete(NotIntended, true)]
	public static void XML_SearchGraph() => throw new DocumentationMethodException();

#pragma warning restore CS1711, CS1572, SA1604, SA1617

	#endregion

	#region A* Algorithm

	/// <summary>Runs the A* search algorithm on a graph.</summary>
	/// <inheritdoc cref="XML_SearchGraph"/>
	[Obsolete(NotIntended, true)]
	public static void XML_SearchGraph_Astar() => throw new DocumentationMethodException();

	/// <inheritdoc cref="XML_SearchGraph_Astar"/>
	public static Action<Action<TNode>>? SearchGraph<TNode, TNumeric>(TNode start, SearchNeighbors<TNode> neighbors, SearchHeuristic<TNode, TNumeric> heuristic, SearchCost<TNode, TNumeric> cost, SearchGoal<TNode> goal, out TNumeric? totalCost) =>
		SearchGraph(start, neighbors, heuristic, cost, node => goal(node) ? GraphSearchStatus.Goal : GraphSearchStatus.Continue, out totalCost);

	/// <inheritdoc cref="XML_SearchGraph_Astar"/>
	public static Action<Action<TNode>>? SearchGraph<TNode, TNumeric>(TNode start, IGraph<TNode> graph, SearchHeuristic<TNode, TNumeric> heuristic, SearchCost<TNode, TNumeric> cost, SearchGoal<TNode> goal, out TNumeric? totalCost) =>
		SearchGraph(start, graph.Neighbors, heuristic, cost, goal, out totalCost);

	/// <inheritdoc cref="XML_SearchGraph_Astar"/>
	public static Action<Action<TNode>>? SearchGraph<TNode, TNumeric>(TNode start, SearchNeighbors<TNode> neighbors, SearchHeuristic<TNode, TNumeric> heuristic, SearchCost<TNode, TNumeric> cost, TNode goal, out TNumeric? totalCost) =>
		SearchGraph(start, neighbors, heuristic, cost, goal, Equate, out totalCost);

	/// <inheritdoc cref="XML_SearchGraph_Astar"/>
	public static Action<Action<TNode>>? SearchGraph<TNode, TNumeric>(TNode start, SearchNeighbors<TNode> neighbors, SearchHeuristic<TNode, TNumeric> heuristic, SearchCost<TNode, TNumeric> cost, TNode goal, Func<TNode, TNode, bool> equate, out TNumeric? totalCost) =>
		SearchGraph(start, neighbors, heuristic, cost, node => equate(node, goal), out totalCost);

	/// <inheritdoc cref="XML_SearchGraph_Astar"/>
	public static Action<Action<TNode>>? SearchGraph<TNode, TNumeric>(TNode start, IGraph<TNode> graph, SearchHeuristic<TNode, TNumeric> heuristic, SearchCost<TNode, TNumeric> cost, TNode goal, out TNumeric? totalCost) =>
		SearchGraph(start, graph, heuristic, cost, goal, Equate, out totalCost);

	/// <inheritdoc cref="XML_SearchGraph_Astar"/>
	public static Action<Action<TNode>>? SearchGraph<TNode, TNumeric>(TNode start, IGraph<TNode> graph, SearchHeuristic<TNode, TNumeric> heuristic, SearchCost<TNode, TNumeric> cost, TNode goal, Func<TNode, TNode, bool> equate, out TNumeric? totalCost) =>
		SearchGraph(start, graph.Neighbors, heuristic, cost, node => equate(node, goal), out totalCost);

	/// <inheritdoc cref="XML_SearchGraph_Astar"/>
	public static Action<Action<TNode>>? SearchGraph<TNode, TNumeric>(TNode start, SearchNeighbors<TNode> neighbors, SearchHeuristic<TNode, TNumeric> heuristic, SearchCost<TNode, TNumeric> cost, SearchCheck<TNode> check, out TNumeric? totalCost)
	{
		// using a heap (aka priority queue) to store nodes based on their computed A* f(n) value
		HeapArray<AstarNode<TNode, TNumeric>, AStarPriorityCompare<TNode, TNumeric>> fringe = new();

		// push starting node
		fringe.Enqueue(
			new AstarNode<TNode, TNumeric>(
				value: start,
				priority: Constant<TNumeric>.Zero,
				cost: Constant<TNumeric>.Zero,
				previous: null));

		// run the algorithm
		while (fringe.Count != 0)
		{
			AstarNode<TNode, TNumeric> current = fringe.Dequeue();
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
						TNumeric costValue = Addition(current.Cost, cost(current.Value, neighbor));
						fringe.Enqueue(
							new AstarNode<TNode, TNumeric>(
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

	/// <summary>Perfoms A* Search on the graph</summary>
	/// <param name="graph">The weighted graph object</param>
	/// <param name="Source">The node to begin search from</param>
	/// <param name="Destination">The node to search</param>
	/// <param name="CustomHeuristic">The heuristic function of the node</param>
	/// <param name="TotalWeight">The total cost or weight incurred from source to destination</param>
	/// <typeparam name="TNode">The Node type of the graph</typeparam>
	/// <typeparam name="TNumeric">The Numeric type of the weighted graph</typeparam>
	/// <returns>IEnumerable of the ordered sequence of nodes from source to destination</returns>
	public static System.Collections.Generic.IEnumerable<TNode> AStarSearch<TNode, TNumeric>(this IGraphWeighted<TNode, TNumeric> graph, TNode Source, TNode Destination, SearchHeuristic<TNode, TNumeric> CustomHeuristic, out TNumeric TotalWeight)
	{
		ListArray<TNode> path = new();
		var graphPath = SearchGraph<TNode, TNumeric>(start: Source, graph: graph, heuristic: CustomHeuristic, cost: graph.GetWeight, goal: Destination, out TNumeric? totalWeight);
		TotalWeight = totalWeight ?? Constant<TNumeric>.Zero;
		graphPath?.Invoke(n => path.Add(n));
		return path;
	}

	/// <summary>Perfoms A* Search on the graph and executes action on every node in path</summary>
	/// <param name="graph">The weighted graph object</param>
	/// <param name="Source">The node to begin search from</param>
	/// <param name="Destination">The node to search</param>
	/// <param name="CustomHeuristic">The heuristic function of the node</param>
	/// <param name="action">The action to perform for every node on path</param>
	/// <param name="TotalWeight">The total cost or weight incurred from source to destination</param>
	/// <typeparam name="TNode">The Node type of the graph</typeparam>
	/// <typeparam name="TNumeric">The Numeric type of the weighted graph</typeparam>
	public static void AStarSearch<TNode, TNumeric>(this IGraphWeighted<TNode, TNumeric> graph, TNode Source, TNode Destination, SearchHeuristic<TNode, TNumeric> CustomHeuristic, Action<TNode> action, out TNumeric TotalWeight)
	{
		var graphPath = SearchGraph<TNode, TNumeric>(start: Source, graph: graph, heuristic: CustomHeuristic, cost: graph.GetWeight, goal: Destination, out TNumeric? totalWeight);
		TotalWeight = totalWeight ?? Constant<TNumeric>.Zero;
		if (graphPath != null && action != null) graphPath(n => action(n));
	}

	#endregion

	#region Dijkstra Algorithm

	/// <summary>Runs the Dijkstra search algorithm on a graph.</summary>
	/// <inheritdoc cref="XML_SearchGraph"/>
	[Obsolete(NotIntended, true)]
	public static void XML_SearchGraph_Dijkstra() => throw new DocumentationMethodException();

	/// <inheritdoc cref="XML_SearchGraph_Dijkstra"/>
	public static Action<Action<TNode>>? SearchGraph<TNode, TNumeric>(TNode start, SearchNeighbors<TNode> neighbors, SearchHeuristic<TNode, TNumeric> heuristic, SearchGoal<TNode> goal) =>
		SearchGraph(start, neighbors, heuristic, node => goal(node) ? GraphSearchStatus.Goal : GraphSearchStatus.Continue);

	/// <inheritdoc cref="XML_SearchGraph_Dijkstra"/>
	public static Action<Action<TNode>>? SearchGraph<TNode, TNumeric>(TNode start, SearchNeighbors<TNode> neighbors, SearchHeuristic<TNode, TNumeric> heuristic, TNode goal) =>
		SearchGraph(start, neighbors, heuristic, goal, Equate);

	/// <inheritdoc cref="XML_SearchGraph_Dijkstra"/>
	public static Action<Action<TNode>>? SearchGraph<TNode, TNumeric>(TNode start, SearchNeighbors<TNode> neighbors, SearchHeuristic<TNode, TNumeric> heuristic, TNode goal, Func<TNode, TNode, bool> equate) =>
		SearchGraph(start, neighbors, heuristic, node => equate(node, goal));

	/// <inheritdoc cref="XML_SearchGraph_Dijkstra"/>
	public static Action<Action<TNode>>? SearchGraph<TNode, TNumeric>(TNode start, IGraph<TNode> graph, SearchHeuristic<TNode, TNumeric> heuristic, TNode goal) =>
		SearchGraph(start, graph, heuristic, goal, Equate);

	/// <inheritdoc cref="XML_SearchGraph_Dijkstra"/>
	public static Action<Action<TNode>>? SearchGraph<TNode, TNumeric>(TNode start, IGraph<TNode> graph, SearchHeuristic<TNode, TNumeric> heuristic, TNode goal, Func<TNode, TNode, bool> equate) =>
		SearchGraph(start, graph.Neighbors, heuristic, node => equate(node, goal));

	/// <inheritdoc cref="XML_SearchGraph_Dijkstra"/>
	public static Action<Action<TNode>>? SearchGraph<TNode, TNumeric>(TNode start, IGraph<TNode> graph, SearchHeuristic<TNode, TNumeric> heuristic, SearchGoal<TNode> goal) =>
		SearchGraph(start, graph.Neighbors, heuristic, goal);

	/// <inheritdoc cref="XML_SearchGraph_Dijkstra"/>
	public static Action<Action<TNode>>? SearchGraph<TNode, TNumeric>(TNode start, SearchNeighbors<TNode> neighbors, SearchHeuristic<TNode, TNumeric> heuristic, SearchCheck<TNode> check)
	{
		// using a heap (aka priority queue) to store nodes based on their computed heuristic value
		HeapArray<DijkstraNode<TNode, TNumeric>, DijkstraPriorityCompare<TNode, TNumeric>> fringe = new();

		// push starting node
		fringe.Enqueue(
			new DijkstraNode<TNode, TNumeric>(
				value: start,
				priority: Constant<TNumeric>.Zero,
				previous: null));

		// run the algorithm
		while (fringe.Count != 0)
		{
			DijkstraNode<TNode, TNumeric> current = fringe.Dequeue();
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
							new DijkstraNode<TNode, TNumeric>(
								value: neighbor,
								priority: heuristic(neighbor),
								previous: current));
					});
			}
		}
		return null; // goal node was not reached (no path exists)
	}

	/// <summary>Performs Dijkstra search on graph</summary>
	/// <param name="graph">The weighted graph object to search</param>
	/// <param name="Source">The node to begin searching from</param>
	/// <param name="Destination">The node to search</param>
	/// <param name="TotalWeight">The total cost or weight incurred from the Soruce to the Destination node</param>
	/// <typeparam name="TNode">The Node Type of the graph</typeparam>
	/// <typeparam name="TNumeric">The Numeric Type of the graph</typeparam>
	/// <returns>IEnumerable of the ordered nodes in path from source to destination</returns>
	public static System.Collections.Generic.IEnumerable<TNode> DijkstraSearch<TNode, TNumeric>(this IGraphWeighted<TNode, TNumeric> graph, TNode Source, TNode Destination, out TNumeric TotalWeight)
	{
		ListArray<TNode> path = new();
		var graphPath = SearchGraph<TNode, TNumeric>(start: Source, graph: graph, heuristic: (x) => Constant<TNumeric>.Zero, cost: graph.GetWeight, goal: Destination, out TNumeric? totalWeight);
		TotalWeight = totalWeight ?? Constant<TNumeric>.Zero;
		graphPath?.Invoke(n => path.Add(n));
		return path;
	}

	/// <summary>Performs Dijkstra Search and executes action on every node in path</summary>
	/// <param name="graph">The weighted graph object to search</param>
	/// <param name="Source">The node to begin searching from</param>
	/// <param name="Destination">The destination node to search</param>
	/// <param name="action">The action to execute for every node on the path</param>
	/// <param name="TotalWeight">The Total cost or weight incurred from the Source to the Destination node</param>
	/// <typeparam name="TNode">The Node Type of the graph</typeparam>
	/// <typeparam name="TNumeric">The Numeric Type of the graph</typeparam>
	public static void DijkstraSearch<TNode, TNumeric>(this IGraphWeighted<TNode, TNumeric> graph, TNode Source, TNode Destination, Action<TNode> action, out TNumeric TotalWeight)
	{
		var graphPath = SearchGraph<TNode, TNumeric>(start: Source, graph: graph, heuristic: (x) => Constant<TNumeric>.Zero, cost: graph.GetWeight, goal: Destination, out TNumeric? totalWeight);
		TotalWeight = totalWeight ?? Constant<TNumeric>.Zero;
		if (action == null) return;
		graphPath?.Invoke(n => action(n));
	}

	#endregion

	#region BreadthFirstSearch Algorithm

	/// <summary>Runs the Breadth-First-Search search algorithm on a graph.</summary>
	/// <inheritdoc cref="XML_SearchGraph"/>
	[Obsolete(NotIntended, true)]
	public static void XML_SearchGraph_BreadthFirst() => throw new DocumentationMethodException();

	/// <inheritdoc cref="XML_SearchGraph_BreadthFirst"/>
	public static Action<Action<TNode>>? SearchGraph<TNode>(TNode start, SearchNeighbors<TNode> neighbors, SearchGoal<TNode> goal) =>
		SearchGraph(start, neighbors, node => goal(node) ? GraphSearchStatus.Goal : GraphSearchStatus.Continue);

	/// <inheritdoc cref="XML_SearchGraph_BreadthFirst"/>
	public static Action<Action<TNode>>? SearchGraph<TNode>(TNode start, SearchNeighbors<TNode> neighbors, TNode goal) =>
		SearchGraph(start, neighbors, goal, Equate);

	/// <inheritdoc cref="XML_SearchGraph_BreadthFirst"/>
	public static Action<Action<TNode>>? SearchGraph<TNode>(TNode start, SearchNeighbors<TNode> neighbors, TNode goal, Func<TNode, TNode, bool> equate) =>
		SearchGraph(start, neighbors, node => equate(node, goal));

	/// <inheritdoc cref="XML_SearchGraph_BreadthFirst"/>
	public static Action<Action<TNode>>? SearchGraph<TNode>(TNode start, IGraph<TNode> graph, TNode goal) =>
		SearchGraph(start, graph, goal, Equate);

	/// <inheritdoc cref="XML_SearchGraph_BreadthFirst"/>
	public static Action<Action<TNode>>? SearchGraph<TNode>(TNode start, IGraph<TNode> graph, TNode goal, Func<TNode, TNode, bool> equate) =>
		SearchGraph(start, graph.Neighbors, node => equate(node, goal));

	/// <inheritdoc cref="XML_SearchGraph_BreadthFirst"/>
	public static Action<Action<TNode>>? SearchGraph<TNode>(TNode start, IGraph<TNode> graph, SearchGoal<TNode> goal) =>
		SearchGraph(start, graph.Neighbors, goal);

	/// <inheritdoc cref="XML_SearchGraph_BreadthFirst"/>
	public static Action<Action<TNode>>? SearchGraph<TNode>(TNode start, SearchNeighbors<TNode> neighbors, SearchCheck<TNode> check)
	{
		IQueue<BreadthFirstSearch<TNode>> fringe = new QueueLinked<BreadthFirstSearch<TNode>>();

		// push starting node
		fringe.Enqueue(
			new BreadthFirstSearch<TNode>(
				value: start,
				previous: null));

		// run the algorithm
		while (fringe.Count != 0)
		{
			BreadthFirstSearch<TNode> current = fringe.Dequeue();
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
							new BreadthFirstSearch<TNode>(
								value: neighbor,
								previous: current));
					});
			}
		}
		return null; // goal node was not reached (no path exists)
	}

	/// <summary>Performs Breadth First Search and returns the path as ordered sequence of nodes</summary>
	/// <param name="graph">The IGraph object</param>
	/// <param name="Source">The node to begin the Breadth First Search from</param>
	/// <param name="Destination">The node to search</param>
	/// <typeparam name="TNode">The Node Type of the Graph</typeparam>
	/// <returns>IEnumerable of the Nodes found along the path</returns>
	public static System.Collections.Generic.IEnumerable<TNode> PerformBredthFirstSearch<TNode>(this IGraph<TNode> graph, TNode Source, TNode Destination)
	{
		ListArray<TNode> path = new();
		var graphPath = SearchGraph<TNode>(start: Source, graph: graph, goal: Destination);
		graphPath?.Invoke(n => path.Add(n));
		return path;
	}

	/// <summary>Performs Breadth First Search and executes the specified action on every ordered node to in the path searched.</summary>
	/// <param name="graph">The IGraph object</param>
	/// <param name="Source">The node to begin Breadth First Search from</param>
	/// <param name="Destination">The node to search</param>
	/// <param name="action">The action to perform</param>
	/// <typeparam name="TNode">The Node Type of the graph</typeparam>
	public static void PerformBredthFirstSearch<TNode>(this IGraph<TNode> graph, TNode Source, TNode Destination, Action<TNode> action)
	{
		if (action != null)
		{
			var graphPath = SearchGraph<TNode>(start: Source, graph: graph, goal: Destination);
			if (action != null && graphPath != null) graphPath(n => action(n));
		}
	}

	#endregion

	#endregion
}
