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
			graph.Stepper((p, q) => RemainingEdges.Add((p, q)));
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
			graph.Stepper((p, q) => RemainingEdges.Add((p, q)));
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
	public class WeightedGraph_Testing
	{
		#region Methods

		[TestMethod]
		public void AdjacentTest()
		{
			GraphWeighted<byte, int> graph = new();
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
			GraphWeighted<byte, int> graph = new();
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
			GraphWeighted<byte, int> graph = new();
			for (byte i = 1; i <= 5; i++) graph.Add(i);
			graph.Add(1, 5, 1);
			graph.Add(2, 5, 1);
			graph.Add(3, 5, 1);
			graph.Add(4, 5, 1);
			graph.Remove<byte>(3);
			ListArray<(int, int)> RemainingEdges = new();
			graph.Stepper((p, q) => RemainingEdges.Add((p, q)));
			Assert.IsTrue(SetEquals<(int, int)>(new[] { (1, 5), (2, 5), (4, 5) }, RemainingEdges.ToArray()));
		}

		[TestMethod]
		public void AddPropertyTest()
		{
			GraphWeighted<byte, int> graph = new();
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
					GraphWeighted<byte, int> graph = new();
					for (byte i = 1; i <= 5; i++) graph.Add(i);
					graph.Add(1, 5, 1);
					graph.Add(2, 5, 1);
					graph.Add(3, 5, 1);
					graph.Add(4, 5, 1);
					graph.Add(3, 6, 1);
				});
		}

		#endregion
	}
}
