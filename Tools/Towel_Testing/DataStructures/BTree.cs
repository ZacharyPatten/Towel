using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel.DataStructures;

namespace Towel_Testing.DataStructures
{
	[TestClass]
	public class BTree_Testing
	{
		[TestMethod]
		public void AddTest()
		{
			BTree<int> tree = new(8);
			Assert.IsTrue(tree.Top.IsLeaf);
			Assert.IsTrue(tree.Count == 0);
			for (int i = 100; i > 0; i--)
			{
				tree.Add(i);
			}
			int[] exp = new int[100];
			for (int i = 1; i <= 100; i++)
			{
				exp[i - 1] = i;
			}
			int[] act = tree.ToArray();
			CollectionAssert.AreEqual(exp, act);
		}
		[TestMethod]
		public void FalseAddTest_1()
		{
			BTree<int> tree = new(6);
			Assert.IsTrue(tree.Add(1));
			Assert.IsTrue(tree.Add(2));
			Assert.IsTrue(tree.Add(3));
			Assert.IsFalse(tree.Add(3));
			Assert.IsFalse(tree.Add(2));
		}
		[TestMethod]
		public void FalseAddTest_2()
		{
			BTree<int> tree = new(4);
			for (int i = 10; i < 1000; i++) Assert.IsTrue(tree.Add(i));
			Assert.IsTrue(tree.Add(1));
			Assert.IsTrue(tree.Add(2));
			Assert.IsTrue(tree.Add(3));
			Assert.IsFalse(tree.Add(3));
			Assert.IsFalse(tree.Add(2));
		}
		[TestMethod]
		public void FalseRemoveTest_1()
		{
			BTree<int> tree = new(6);
			Assert.IsTrue(tree.Add(1));
			Assert.IsTrue(tree.Add(2));
			Assert.IsTrue(tree.Add(3));
			Assert.IsFalse(tree.Add(3));
			Assert.IsFalse(tree.Add(2));
			Assert.IsTrue(tree.Remove(3));
		}
		[TestMethod]
		public void FalseRemoveTest_2()
		{
			BTree<int> tree = new(6);
			for (int i = 10; i < 2000; i++) Assert.IsTrue(tree.Add(i));
			for (int i = 0; i < 10; i++) Assert.IsFalse(tree.Remove(i));
		}
		[TestMethod]
		public void Add_SearchTest()
		{
			BTree<int> tree = new(10);
			for (int i = 0; i < 1000; i++)
			{
				Assert.IsTrue(tree.Add(i * 3));
			}
			for (int i = 0; i < 3000; i += 3)
			{
				Assert.IsTrue(tree.Search(i));
				Assert.IsFalse(tree.Search(i + 1));
				Assert.IsFalse(tree.Search(i + 2));
			}
		}
		[TestMethod]
		public void Add_Remove_SearchTest()
		{
			BTree<int> tree = new(10);
			for (int i = 0; i <= 3000; i++)
			{
				Assert.IsTrue(tree.Add(i));
			}
			for (int i = 0; i < 1000; i++)
			{
				Assert.IsTrue(tree.Remove((3 * i) + 1));
				Assert.IsTrue(tree.Remove((3 * i) + 2));
			}
			for (int i = 0; i < 3000; i += 3)
			{
				Assert.IsTrue(tree.Search(i));
				Assert.IsFalse(tree.Search(i + 1));
				Assert.IsFalse(tree.Search(i + 2));
			}
		}
	}
}