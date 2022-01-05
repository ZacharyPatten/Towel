using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel.DataStructures;
namespace Towel_Testing.DataStructures
{
	[TestClass]
	public class BTree_Testing
	{
		[TestMethod]
		public void EnumeratorTest()
		{
			BTree<int> tree = new(8);
			foreach (int _ in tree)
			{
				Assert.Fail();
			}
		}
		[TestMethod]
		public void ClearTest()
		{
			BTree<int> tree = new(8);
			for (int i = 0; i < 10000; i++) tree.Add(i);
			tree.Clear();
			Assert.IsTrue(tree.Count == 0);
			int[] arr = new int[100];
			for (int i = 0; i < 100; i++)
			{
				arr[i] = 300 - i;
				tree.Add(arr[i]);
			}
			Array.Sort(arr);
			CollectionAssert.AreEqual(arr, tree.ToArray());
		}
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
			Random r = new();
			int size = 1000;
			size *= 3;
			int[] arr = new int[size];
			for (int i = 0, j; i < size; i++)
			{
				do
				{
					j = r.Next(size * size);
				} while (Array.IndexOf(arr, j, 0, i) >= 0);
				arr[i] = j;
			}
			for (int i = 0; i < size; i++)
			{
				Assert.IsTrue(tree.Add(arr[i]));
			}
			for (int i = 0; i < size; i += 3)
			{
				Assert.IsTrue(tree.Remove(arr[i + 1]));
				Assert.IsTrue(tree.Remove(arr[i + 2]));
			}
			for (int i = 0; i < size; i += 3)
			{
				Assert.IsTrue(tree.Search(arr[i]));
				Assert.IsFalse(tree.Search(arr[i + 1]));
				Assert.IsFalse(tree.Search(arr[i + 2]));
			}
		}
		[TestMethod]
		public void RemoveAll_1()
		{
			Random r = new();
			int size = 14, max = size * size;
			BTree<int> tree = new(4);
			int[] arr = new int[size];
			for (int i = 0, j; i < size; i++)
			{
				do
				{
					j = max + r.Next(max);
				} while (Array.IndexOf(arr, j, 0, i) >= 0);
				arr[i] = j;
			}
			for (int i = 0; i < size; i++)
				Assert.IsTrue(tree.Add(arr[i]));
			for (int i = 0, j; i < size; i++)
			{
				Assert.IsTrue(tree.Remove(arr[i]));
				do
				{
					j = r.Next(max);
				} while (Array.IndexOf(arr, j, 0, i) >= 0);
			}
			Assert.IsTrue(tree.Count == 0);
			for (int i = 0; i < size; i++) Assert.IsTrue(tree.Add(arr[i]));
			Array.Sort(arr);
			CollectionAssert.AreEqual(arr, tree.ToArray());
		}
		[TestMethod]
		public void RemoveAll_2()
		{
			Random r = new();
			int size = 400, max = size * size;
			BTree<int> tree = new(8);
			int[] arr = new int[size];
			for (int i = 0, j; i < size; i++)
			{
				do
				{
					j = max + r.Next(max);
				} while (Array.IndexOf(arr, j, 0, i) >= 0);
				arr[i] = j;
			}
			for (int i = 0; i < size; i++)
				Assert.IsTrue(tree.Add(arr[i]));
			for (int i = 0, j; i < size; i++)
			{
				Assert.IsTrue(tree.Remove(arr[i]));
				do
				{
					j = r.Next(max);
				} while (Array.IndexOf(arr, j, 0, i) >= 0);
			}
			Assert.IsTrue(tree.Count == 0);
			for (int i = 0; i < size; i++) Assert.IsTrue(tree.Add(arr[i]));
			Array.Sort(arr);
			CollectionAssert.AreEqual(arr, tree.ToArray());
		}
	}
}