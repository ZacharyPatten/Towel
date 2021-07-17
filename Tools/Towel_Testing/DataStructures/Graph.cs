using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel;
using Towel.DataStructures;
using static Towel.Statics;

namespace Towel_Testing.DataStructures
{
	[TestClass]
	public class GraphMap_Testing
	{
		#region Methods

		[TestMethod]
		public void AdjacentTest()
		{
			IGraph<int> graph = GraphMap.New<int>();
			for (int i = 1; i <= 5; i++) graph.Add(i);
			graph.Add(1, 5);
			graph.Add(2, 5);
			graph.Add(3, 5);
			graph.Add(4, 5);
			Assert.IsTrue(graph.Adjacent(1, 5));
		}

		[TestMethod]
		public void NeighborsTest()
		{
			IGraph<int> graph = GraphMap.New<int>();
			for (int i = 1; i <= 5; i++) graph.Add(i);
			graph.Add(1, 5);
			graph.Add(2, 5);
			graph.Add(3, 5);
			graph.Add(4, 5);
			graph.Add(5, 2);
			graph.Add(5, 3);
			int sum = 0;
			graph.Neighbors(5, (x) => sum += x);
			Assert.IsTrue(sum is 5);
		}

		[TestMethod]
		public void RemoveTest()
		{
			IGraph<int> graph = GraphMap.New<int>();
			for (int i = 1; i <= 5; i++) graph.Add(i);
			graph.Add(1, 5);
			graph.Add(2, 5);
			graph.Add(3, 5);
			graph.Add(4, 5);
			graph.Remove(3);
			ListArray<(int, int)> RemainingEdges = new();
			graph.Edges(edge => RemainingEdges.Add((edge.Item1, edge.Item2)));
			Assert.IsTrue(SetEquals<(int, int)>(new[] { (1, 5), (2, 5), (4, 5) }, RemainingEdges.ToArray()));
		}

		[TestMethod]
		public void AddPropertyTest()
		{
			IGraph<int> graph = GraphMap.New<int>();
			for (int i = 1; i <= 5; i++) graph.Add(i);
			graph.Add(1, 5);
			graph.Add(2, 5);
			graph.Add(3, 5);
			graph.Add(4, 5);
			graph.Add(1, 2);
			graph.Add(2, 3);
			graph.Add(3, 4);
			Assert.IsTrue(graph.EdgeCount * graph.NodeCount is 35);
		}

		[TestMethod]
		public void IncorrectAddTest()
		{
			Assert.ThrowsException<ArgumentException>(
				() =>
				{
					IGraph<int> graph = GraphMap.New<int>();
					for (int i = 1; i <= 5; i++) graph.Add(i);
					graph.Add(1, 5);
					graph.Add(2, 5);
					graph.Add(3, 5);
					graph.Add(4, 5);
					graph.Add(3, 6);
				});
		}

		#endregion
	}

	[TestClass]
	public class GraphSetOmnitree_Testing
	{
		#region Methods

		[TestMethod]
		public void AdjacentTest()
		{
			IGraph<int> graph = GraphSetOmnitree.New<int>();
			for (int i = 1; i <= 5; i++) graph.Add(i);
			graph.Add(1, 5);
			graph.Add(2, 5);
			graph.Add(3, 5);
			graph.Add(4, 5);
			Assert.IsTrue(graph.Adjacent(1, 5));
		}

		[TestMethod]
		public void NeighborsTest()
		{
			IGraph<int> graph = GraphSetOmnitree.New<int>();
			for (int i = 1; i <= 5; i++) graph.Add(i);
			graph.Add(1, 5);
			graph.Add(2, 5);
			graph.Add(3, 5);
			graph.Add(4, 5);
			graph.Add(5, 2);
			graph.Add(5, 3);
			int sum = 0;
			graph.Neighbors(5, (x) => sum += x);
			Assert.IsTrue(sum is 5);
		}

		[TestMethod]
		public void RemoveTest()
		{
			IGraph<int> graph = GraphSetOmnitree.New<int>();
			for (int i = 1; i <= 5; i++) graph.Add(i);
			graph.Add(1, 5);
			graph.Add(2, 5);
			graph.Add(3, 5);
			graph.Add(4, 5);
			graph.Remove(3);
			ListArray<(int, int)> RemainingEdges = new();
			graph.Edges(edge => RemainingEdges.Add((edge.Item1, edge.Item2)));
			Assert.IsTrue(SetEquals<(int, int)>(new[] { (1, 5), (2, 5), (4, 5) }, RemainingEdges.ToArray()));
		}

		[TestMethod]
		public void AddPropertyTest()
		{
			IGraph<int> graph = GraphSetOmnitree.New<int>();
			for (int i = 1; i <= 5; i++) graph.Add(i);
			graph.Add(1, 5);
			graph.Add(2, 5);
			graph.Add(3, 5);
			graph.Add(4, 5);
			graph.Add(1, 2);
			graph.Add(2, 3);
			graph.Add(3, 4);
			Assert.IsTrue(graph.NodeCount * graph.EdgeCount is 35);
		}

		[TestMethod]
		public void IncorrectAddTest()
		{
			Assert.ThrowsException<ArgumentException>(
				() =>
				{
					IGraph<int> graph = GraphSetOmnitree.New<int>();
					for (int i = 1; i <= 5; i++) graph.Add(i);
					graph.Add(1, 5);
					graph.Add(2, 5);
					graph.Add(3, 5);
					graph.Add(4, 5);
					graph.Add(3, 6);
				});
		}

		#endregion
	}

	[TestClass]
	public class GraphWeightedMap_Testing
	{
		#region Methods

		[TestMethod]
		public void AdjacentTest()
		{
			IGraphWeighted<byte, int> graph = GraphWeightedMap.New<byte, int>();
			for (byte i = 1; i <= 5; i++) graph.Add(i);
			graph.Add((byte)1, (byte)5, 1);
			graph.Add((byte)2, (byte)5, 1);
			graph.Add((byte)3, (byte)5, 1);
			graph.Add((byte)4, (byte)5, 1);
			Assert.IsTrue(graph.Adjacent(1, 5));
		}

		[TestMethod]
		public void NeighborsTest()
		{
			IGraphWeighted<byte, int> graph = GraphWeightedMap.New<byte, int>();
			for (byte i = 1; i <= 5; i++) graph.Add(i);
			graph.Add((byte)1, (byte)5, 1);
			graph.Add((byte)2, (byte)5, 1);
			graph.Add((byte)3, (byte)5, 1);
			graph.Add((byte)4, (byte)5, 1);
			graph.Add((byte)5, (byte)2, 1);
			graph.Add((byte)5, (byte)3, 1);
			int sum = 0;
			graph.Neighbors(5, x => sum += x);
			Assert.IsTrue(sum is 5);
		}

		[TestMethod]
		public void RemoveTest()
		{
			IGraphWeighted<byte, int> graph = GraphWeightedMap.New<byte, int>();
			for (byte i = 1; i <= 5; i++) graph.Add(i);
			graph.Add((byte)1, (byte)5, 1);
			graph.Add((byte)2, (byte)5, 1);
			graph.Add((byte)3, (byte)5, 1);
			graph.Add((byte)4, (byte)5, 1);
			graph.Remove<byte>(3);
			ListArray<(int, int)> RemainingEdges = new();
			graph.Edges(edge => RemainingEdges.Add((edge.Item1, edge.Item2)));
			Assert.IsTrue(SetEquals<(int, int)>(new[] { (1, 5), (2, 5), (4, 5) }, RemainingEdges.ToArray()));
		}

		[TestMethod]
		public void AddPropertyTest()
		{
			IGraphWeighted<byte, int> graph = GraphWeightedMap.New<byte, int>();
			for (byte i = 1; i <= 5; i++) graph.Add(i);
			graph.Add((byte)1, (byte)5, 1);
			graph.Add((byte)2, (byte)5, 1);
			graph.Add((byte)3, (byte)5, 1);
			graph.Add((byte)4, (byte)5, 1);
			graph.Add((byte)1, (byte)2, 1);
			graph.Add((byte)2, (byte)3, 1);
			graph.Add((byte)3, (byte)4, 1);
			Assert.IsTrue(graph.NodeCount * graph.EdgeCount is 35);
		}

		[TestMethod]
		public void IncorrectAddTest()
		{
			Assert.ThrowsException<ArgumentException>(
				() =>
				{
					IGraphWeighted<byte, int> graph = GraphWeightedMap.New<byte, int>();
					for (byte i = 1; i <= 5; i++) graph.Add(i);
					graph.Add((byte)1, (byte)5, 1);
					graph.Add((byte)2, (byte)5, 1);
					graph.Add((byte)3, (byte)5, 1);
					graph.Add((byte)4, (byte)5, 1);
					graph.Add((byte)3, (byte)6, 1);
				});
		}

		[TestMethod]
		public void CloneTest()
		{
			var g1 = GraphWeightedMap.New<byte, int>();
			g1.Add((byte)7);
			g1.Add((byte)5);
			g1.Add((byte)7, (byte)5, 13);
			var g2 = g1.Clone();
			g2.Adjacent(7, 5, out var f);
			Assert.IsTrue(f is 13);
		}

		[TestMethod]
		public void ToArrayTest()
		{
			var graph = GraphWeightedMap.New<byte, int>();
			for (byte i = 1; i <= 5; i++) graph.Add(i);
			var array = graph.ToArray();
			CollectionAssert.AreEquivalent(new byte[] { 1, 2, 3, 4, 5 }, array);
		}

		[TestMethod]
		public void EdgesToArrayTest()
		{
			IGraphWeighted<byte, int> graph = GraphWeightedMap.New<byte, int>();
			for (byte i = 1; i <= 5; i++) graph.Add(i);
			graph.Add((byte)1, (byte)5, 1);
			graph.Add((byte)2, (byte)5, 1);
			graph.Add((byte)3, (byte)5, 1);
			graph.Add((byte)4, (byte)5, 1);
			graph.Add((byte)1, (byte)2, 1);
			graph.Add((byte)2, (byte)3, 1);
			graph.Add((byte)3, (byte)4, 1);
			var array = graph.EdgesToArray();
			CollectionAssert.AreEquivalent(new (byte, byte)[] { (1, 5), (2, 5), (3, 5), (4, 5), (1, 2), (2, 3), (3, 4) }, array);
		}

		[TestMethod]
		public void EdgesAndWeightsToArrayTest()
		{
			IGraphWeighted<byte, int> graph = GraphWeightedMap.New<byte, int>();
			for (byte i = 1; i <= 5; i++) graph.Add(i);
			graph.Add((byte)1, (byte)5, 1);
			graph.Add((byte)2, (byte)5, 1);
			graph.Add((byte)3, (byte)5, 1);
			graph.Add((byte)4, (byte)5, 1);
			graph.Add((byte)1, (byte)2, 1);
			graph.Add((byte)2, (byte)3, 1);
			graph.Add((byte)3, (byte)4, 1);
			var array = graph.EdgesAndWeightsToArray();
			CollectionAssert.AreEquivalent(new (byte, byte, int)[] { (1, 5, 1), (2, 5, 1), (3, 5, 1), (4, 5, 1), (1, 2, 1), (2, 3, 1), (3, 4, 1) }, array);
		}

		[TestMethod]
		public void GetNeighboursTest()
		{
			IGraphWeighted<byte, int> graph = GraphWeightedMap.New<byte, int>();
			for (byte i = 1; i <= 5; i++) graph.Add(i);
			graph.Add((byte)1, (byte)5, 1);
			graph.Add((byte)2, (byte)5, 1);
			graph.Add((byte)3, (byte)5, 1);
			graph.Add((byte)4, (byte)5, 1);
			graph.Add((byte)1, (byte)2, 1);
			graph.Add((byte)2, (byte)3, 1);
			graph.Add((byte)3, (byte)4, 1);
			((IGraph<byte>)graph).GetNeighbours(4);
		}

		[TestMethod]
		public void BFSFindingTest()
		{
			IGraphWeighted<char, int> graph = GraphWeightedMap.New<char, int>();
			for (char ch = 'A'; ch <= 'I'; ch++) graph.Add(ch);
			graph.Add('A', 'D', 05);
			graph.Add('A', 'B', 10);  // [A]--10--[B]--15--[C]
			graph.Add('B', 'C', 15);  //  |        |        |
			graph.Add('B', 'E', 20);  //  5        20       20
			graph.Add('C', 'F', 20);  //  |        |        |
			graph.Add('D', 'E', 35);  // [D]--35--[E]--10--[F]
			graph.Add('D', 'G', 55);  //  |        |        |
			graph.Add('E', 'H', 50);  //  55       30       60
			graph.Add('E', 'F', 10);  //  |        |        |
			graph.Add('F', 'I', 60);  // [G]--40--[H]--40--[I]
			graph.Add('G', 'H', 40);
			graph.Add('H', 'I', 40);
			CollectionAssert.AreEqual(new char[] { 'A', 'B', 'C' }, graph.PerformBredthFirstSearch('A', 'C').ToArray());
		}

		[TestMethod]
		public void DijkstraFindingTest()
		{
			IGraphWeighted<char, int> graph = GraphWeightedMap.New<char, int>();
			for (char ch = 'A'; ch <= 'I'; ch++) graph.Add(ch);
			graph.Add('A', 'D', 05);
			graph.Add('A', 'B', 10);  // [A]--10--[B]--15--[C]
			graph.Add('B', 'C', 15);  //  |        |        |
			graph.Add('B', 'E', 20);  //  5        20       20
			graph.Add('C', 'F', 20);  //  |        |        |
			graph.Add('D', 'E', 35);  // [D]--35--[E]--10--[F]
			graph.Add('D', 'G', 55);  //  |        |        |
			graph.Add('E', 'H', 30);  //  55       30       65
			graph.Add('E', 'F', 10);  //  |        |        |
			graph.Add('F', 'I', 65);  // [G]--40--[H]--40--[I]
			graph.Add('G', 'H', 40);
			graph.Add('H', 'I', 40);
			CollectionAssert.AreEqual(new char[] { 'A', 'B', 'E', 'H', 'I' }, graph.DijkstraSearch('A', 'I', out int totalcost).ToArray());
			Assert.IsTrue(totalcost is 100);
		}

		[TestMethod]
		public void AStarTest()
		{
			IGraphWeighted<(int, int), float> graph = GraphWeightedMap.New<(int, int), float>();
			for (int i = 1; i <= 3; i++)
			{
				for (int j = 1; j <= 3; j++) graph.Add((i, j));
			}
			graph.Add((1, 1), (2, 1), 10);
			graph.Add((1, 1), (1, 2), 05);  // [1, 1]--10--[2, 1]--15--[3, 1]
			graph.Add((2, 1), (3, 1), 15);  //    |           |           |
			graph.Add((2, 1), (2, 2), 20);  //    5           20          20
			graph.Add((3, 1), (3, 2), 20);  //    |           |           |
			graph.Add((1, 2), (2, 2), 35);  // [1, 2]--35--[2, 2]--10--[3, 2]
			graph.Add((1, 2), (1, 3), 55);  //    |           |           |
			graph.Add((2, 2), (3, 2), 10);  //   55           30          65
			graph.Add((2, 2), (2, 3), 30);  //    |           |           |
			graph.Add((3, 2), (3, 3), 65);  // [1, 3]--40--[2, 3]--40--[3, 3]
			graph.Add((1, 3), (2, 3), 40);
			graph.Add((2, 3), (3, 3), 40);
			// Fixed heuristic from any node (i, j) to node (3, 3).
			static float ManhattanDistanceFromCorner((int, int) node) => (3 - node.Item1) + (3 - node.Item2);
			CollectionAssert.AreEqual(new (int, int)[] { (1, 1), (2, 1), (2, 2), (2, 3), (3, 3) }, graph.AStarSearch((1, 1), (3, 3), ManhattanDistanceFromCorner, out float totalcost).ToArray());
		}

		#endregion
	}
}
