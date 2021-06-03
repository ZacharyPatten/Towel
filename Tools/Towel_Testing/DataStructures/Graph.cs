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
			GraphWeightedMap<int, float> g1=new();
			g1.Add(7);
			g1.Add(5);
			g1.Add(7, 5, 13);
			GraphWeightedMap<int, float> g2=new(g1);
			g2.Adjacent(7, 5, out var f);
			Assert.IsTrue(f is 13f);
		}
		[TestMethod]
		public void ToArrayTest()
		{
			GraphWeightedMap<byte, int> graph = new();
			for (byte i = 1; i <= 5; i++) graph.Add(i);
			var array=graph.ToArray();
			CollectionAssert.AreEquivalent(new byte[]{1,2,3,4,5}, array);
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
			var array=graph.EdgesToArray();
			CollectionAssert.AreEquivalent(new (byte, byte)[]{(1,5),(2,5),(3,5),(4,5),(1,2),(2,3),(3,4)}, array);
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
			var array=graph.EdgesAndWeightsToArray();
			CollectionAssert.AreEquivalent(new (byte, byte, int)[]{(1,5,1),(2,5,1),(3,5,1),(4,5,1),(1,2,1),(2,3,1),(3,4,1)}, array);
		}
		#endregion
	}
}
