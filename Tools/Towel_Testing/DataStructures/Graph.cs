using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Towel.Statics;
using Towel;
using Towel.DataStructures;
using System;

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
			var array=graph.ToArray();
			CollectionAssert.AreEquivalent(new byte[]{1,2,3,4,5}, array);
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
			CollectionAssert.AreEquivalent(new (byte, byte)[]{(1,5),(2,5),(3,5),(4,5),(1,2),(2,3),(3,4)}, array);
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
			CollectionAssert.AreEquivalent(new (byte, byte, int)[]{(1,5,1),(2,5,1),(3,5,1),(4,5,1),(1,2,1),(2,3,1),(3,4,1)}, array);
		}
		#endregion
	}
}
