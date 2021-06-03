using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Towel.Statics;
using Towel;
using Towel.DataStructures;
using System;
using System.Linq;

namespace Towel_Testing.DataStructures
{
	[TestClass]
	public class GraphMap_Testing
	{
		#region Methods

		[TestMethod]
		public void AdjacentTest()
		{
			GraphMap<int> graph = new();
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
			GraphMap<int> graph = new();
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
			GraphMap<int> graph = new();
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
			GraphMap<int> graph = new();
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
					GraphMap<int> graph = new();
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
			GraphSetOmnitree<int> graph = new();
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
			GraphSetOmnitree<int> graph = new();
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
			GraphSetOmnitree<int> graph = new();
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
			GraphSetOmnitree<int> graph = new();
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
					GraphSetOmnitree<int> graph = new();
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
			GraphWeightedMap<byte, int> graph = new();
			for (byte i = 1; i <= 5; i++) graph.Add(i);
			graph.Add(1, 5, 1);
			graph.Add(2, 5, 1);
			graph.Add(3, 5, 1);
			graph.Add(4, 5, 1);
			Assert.IsTrue(graph.Adjacent(1, 5));
		}

		[TestMethod]
		public void NeighborsTest()
		{
			GraphWeightedMap<byte, int> graph = new();
			for (byte i = 1; i <= 5; i++) graph.Add(i);
			graph.Add(1, 5, 1);
			graph.Add(2, 5, 1);
			graph.Add(3, 5, 1);
			graph.Add(4, 5, 1);
			graph.Add(5, 2, 1);
			graph.Add(5, 3, 1);
			int sum = 0;
			graph.Neighbors(5, x => sum += x);
			Assert.IsTrue(sum is 5);
		}

		[TestMethod]
		public void RemoveTest()
		{
			GraphWeightedMap<byte, int> graph = new();
			for (byte i = 1; i <= 5; i++) graph.Add(i);
			graph.Add(1, 5, 1);
			graph.Add(2, 5, 1);
			graph.Add(3, 5, 1);
			graph.Add(4, 5, 1);
			graph.Remove<byte>(3);
			ListArray<(int, int)> RemainingEdges = new();
			graph.Edges(edge => RemainingEdges.Add((edge.Item1, edge.Item2)));
			Assert.IsTrue(SetEquals<(int, int)>(new[] { (1, 5), (2, 5), (4, 5) }, RemainingEdges.ToArray()));
		}

		[TestMethod]
		public void AddPropertyTest()
		{
			GraphWeightedMap<byte, int> graph = new();
			for (byte i = 1; i <= 5; i++) graph.Add(i);
			graph.Add(1, 5, 1);
			graph.Add(2, 5, 1);
			graph.Add(3, 5, 1);
			graph.Add(4, 5, 1);
			graph.Add(1, 2, 1);
			graph.Add(2, 3, 1);
			graph.Add(3, 4, 1);
			Assert.IsTrue(graph.NodeCount * graph.EdgeCount is 35);
		}

		[TestMethod]
		public void IncorrectAddTest()
		{
			Assert.ThrowsException<ArgumentException>(
				() =>
				{
					GraphWeightedMap<byte, int> graph = new();
					for (byte i = 1; i <= 5; i++) graph.Add(i);
					graph.Add(1, 5, 1);
					graph.Add(2, 5, 1);
					graph.Add(3, 5, 1);
					graph.Add(4, 5, 1);
					graph.Add(3, 6, 1);
				});
		}

		[TestMethod]
		public void CloneTest()
		{
			GraphWeightedMap<int, float> g1 = new();
			g1.Add(7);
			g1.Add(5);
			g1.Add(7, 5, 13);
			GraphWeightedMap<int, float> g2 = new(g1);
			g2.Adjacent(7, 5, out var f);
			Assert.IsTrue(f is 13f);
		}
		[TestMethod]
		public void ToArrayTest()
		{
			GraphWeightedMap<byte, int> graph = new();
			for (byte i = 1; i <= 5; i++) graph.Add(i);
			var array = graph.ToArray();
			CollectionAssert.AreEquivalent(new byte[] { 1, 2, 3, 4, 5 }, array);
		}
		[TestMethod]
		public void EdgesToArrayTest()
		{
			GraphWeightedMap<byte, int> graph = new();
			for (byte i = 1; i <= 5; i++) graph.Add(i);
			graph.Add(1, 5, 1);
			graph.Add(2, 5, 1);
			graph.Add(3, 5, 1);
			graph.Add(4, 5, 1);
			graph.Add(1, 2, 1);
			graph.Add(2, 3, 1);
			graph.Add(3, 4, 1);
			var array = graph.EdgesToArray();
			CollectionAssert.AreEquivalent(new (byte, byte)[] { (1, 5), (2, 5), (3, 5), (4, 5), (1, 2), (2, 3), (3, 4) }, array);
		}
		[TestMethod]
		public void EdgesAndWeightsToArrayTest()
		{
			GraphWeightedMap<byte, int> graph = new();
			for (byte i = 1; i <= 5; i++) graph.Add(i);
			graph.Add(1, 5, 1);
			graph.Add(2, 5, 1);
			graph.Add(3, 5, 1);
			graph.Add(4, 5, 1);
			graph.Add(1, 2, 1);
			graph.Add(2, 3, 1);
			graph.Add(3, 4, 1);
			var array = graph.EdgesAndWeightsToArray();
			CollectionAssert.AreEquivalent(new (byte, byte, int)[] { (1, 5, 1), (2, 5, 1), (3, 5, 1), (4, 5, 1), (1, 2, 1), (2, 3, 1), (3, 4, 1) }, array);
		}
		[TestMethod]
		public void GetNeighboursTest()
		{
			GraphWeightedMap<byte, int> graph = new();
			for (byte i = 1; i <= 5; i++) graph.Add(i);
			graph.Add(1, 5, 1);
			graph.Add(2, 5, 1);
			graph.Add(3, 5, 1);
			graph.Add(4, 5, 1);
			graph.Add(1, 2, 1);
			graph.Add(2, 3, 1);
			graph.Add(3, 4, 1);
			((IGraph<byte>)graph).GetNeighbours(4);
		}
		[TestMethod]
		public void BFSFindingTest()
		{
			GraphWeightedMap<char, int> graph = new();
			for (char ch = 'A'; ch <= 'I'; ch++) graph.Add(ch);//Add Nodes from A to I
			graph.Add('A', 'D', 5); //
			graph.Add('A', 'B', 10);//	[A]--10--[B]--15--[C]
			graph.Add('B', 'C', 15);//	 |        |        |
			graph.Add('B', 'E', 20);//	 5        20       20
			graph.Add('C', 'F', 20);//	 |        |        |
			graph.Add('D', 'E', 35);//	[D]--35--[E]--10--[F]
			graph.Add('D', 'G', 55);//	 |        |        |
			graph.Add('E', 'H', 50);//	 55       30       60
			graph.Add('E', 'F', 10);//	 |        |        |
			graph.Add('F', 'I', 60);//	[G]--40--[H]--40--[I]
			graph.Add('G', 'H', 40);//
			graph.Add('H', 'I', 40);//
			CollectionAssert.AreEqual(new char[] { 'A', 'B', 'C' }, graph.PerformBredthFirstSearch('A', 'C').ToArray());
		}
		[TestMethod]
		public void DijkstraFindingTest()
		{
			GraphWeightedMap<char, int> graph = new();
			for (char ch = 'A'; ch <= 'I'; ch++) graph.Add(ch);//Add Nodes from A to I
			graph.Add('A', 'D', 5); //
			graph.Add('A', 'B', 10);//	[A]--10--[B]--15--[C]
			graph.Add('B', 'C', 15);//	 |        |        |
			graph.Add('B', 'E', 20);//	 5        20       20
			graph.Add('C', 'F', 20);//	 |        |        |
			graph.Add('D', 'E', 35);//	[D]--35--[E]--10--[F]
			graph.Add('D', 'G', 55);//	 |        |        |
			graph.Add('E', 'H', 30);//	 55       30       65
			graph.Add('E', 'F', 10);//	 |        |        |
			graph.Add('F', 'I', 65);//	[G]--40--[H]--40--[I]
			graph.Add('G', 'H', 40);//
			graph.Add('H', 'I', 40);//
			CollectionAssert.AreEqual(new char[] { 'A', 'B', 'E', 'H', 'I' }, graph.DijkstraSearch('A', 'I', out int totalcost).ToArray());//Test Path
			Assert.IsTrue(totalcost == 100);
		}
		[TestMethod]
		public void AStarTest()
		{
			GraphWeightedMap<(int, int), float> graph = new();
			for (int i = 1; i <= 3; i++)
			{
				for (int j = 1; j <= 3; j++) graph.Add((i, j));
			}
			graph.Add((1, 1), (2, 1), 10);//	[1, 1]--10--[2, 1]--15--[3, 1]
			graph.Add((1, 1), (1, 2), 05);//	   |           |           |
			graph.Add((2, 1), (3, 1), 15);//	   5           20          20
			graph.Add((2, 1), (2, 2), 20);//	   |           |           |
			graph.Add((3, 1), (3, 2), 20);//	[1, 2]--35--[2, 2]--10--[3, 2]
			graph.Add((1, 2), (2, 2), 35);//	   |           |           |
			graph.Add((1, 2), (1, 3), 55);//	  55           30          65
			graph.Add((2, 2), (3, 2), 10);//	   |           |           |
			graph.Add((2, 2), (2, 3), 30);//	[1, 3]--40--[2, 3]--40--[3, 3]
			graph.Add((3, 2), (3, 3), 65);
			graph.Add((1, 3), (2, 3), 40);
			graph.Add((2, 3), (3, 3), 40);
			float ManhattanDistanceFromCorner((int, int) node) => (3 - node.Item1) + (3 - node.Item2);//Fixed heuristic from any node (i, j) to node (3, 3).
			CollectionAssert.AreEqual(new (int, int)[] { (1, 1), (2, 1), (2, 2), (2, 3), (3, 3) }, graph.AStarSearch((1, 1), (3, 3), ManhattanDistanceFromCorner, out float totalcost).ToArray());
		}
		#endregion
	}
}