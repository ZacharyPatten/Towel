using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel.DataStructures;
namespace Towel_Testing.DataStructures
{
	[TestClass]
	public class SkipList_Testing
	{
		[TestMethod]
		public void AddTest_Random()
		{
			int size = 100;
			int[] arr = new int[size];
			int max = size * size;
			Random r = new();
			for (int i = 0; i < size; i++) arr[i] = r.Next(max);
			var list = SkipList.New<int>(12);
			foreach (var x in arr)
			{
				list.Add(x);
			}
			int[] act = list.ToArray();
			System.Array.Sort(arr);
			CollectionAssert.AreEqual(arr, act);
		}
		[TestMethod]
		public void IteratorTest()
		{
			int[] arr = new int[] { 54, 12, 21, 33, 43, 45, 54, 81, 92, 93, 54, 99, 54 };
			var list = SkipList.New<int>(5);
			foreach (var x in arr)
			{
				list.Add(x);
			}
			System.Array.Sort(arr);
			int[] act = list.ToArray();
			CollectionAssert.AreEqual(arr, act);
		}
		[TestMethod]
		public void False_Removal_Test()
		{
			var list = SkipList.New<int>(4);
			for (int i = 0; i < 10; i++)
				Assert.IsFalse(list.TryRemove(i).Success);
			for (int i = 100; i <= 200; i++)
				list.Add(i);
			for (int i = 0; i < 100; i++)
				Assert.IsFalse(list.TryRemove(i).Success);
		}
		[TestMethod]
		public void Add_Remove_Search_Test_1()
		{
			int size = 1000;
			int[] arr = new int[size];
			int max = size * size;
			Random r = new();
			for (int i = 0; i < size; i++) arr[i] = r.Next(max);
			var list = SkipList.New<int>(15);
			foreach (var x in arr)
			{
				list.Add(x);
			}
			System.Array.Sort(arr);
			for (int i = 0; i < max; i++)
			{
				if (Array.BinarySearch(arr, 0, size, i) >= 0)
					Assert.IsTrue(list.Contains(i));
				else
					Assert.IsFalse(list.Contains(i));
			}
			foreach (var x in arr)
			{
				Assert.IsTrue(list.Contains(x));
				Assert.IsTrue(list.TryRemove(x).Success);
			}
			Assert.IsTrue(list.Count == 0);
		}
		[TestMethod]
		public void Add_Remove_Search_Test_2()
		{
			int size = 4000;
			int[] arr = new int[size];
			int max = size * size;
			Random r = new();
			for (int i = 0; i < size; i++) arr[i] = r.Next(max);
			var list = SkipList.New<int>(20);
			foreach (var x in arr)
			{
				list.Add(x);
			}
			System.Array.Sort(arr);
			for (int i = 0; i < max; i++)
			{
				if (Array.BinarySearch(arr, 0, size, i) >= 0)
					Assert.IsTrue(list.Contains(i));
				else
					Assert.IsFalse(list.Contains(i));
			}
			foreach (var x in arr)
			{
				Assert.IsTrue(list.TryRemove(x).Success);
			}
			Assert.IsTrue(list.Count == 0);
		}
		[TestMethod]
		public void Remove_Test()
		{
			int size = 4000;
			int[] arr = new int[size];
			int max = size * size;
			Random r = new();
			for (int i = 0; i < size; i++) arr[i] = r.Next(max);
			var list = SkipList.New<int>(20);
			for (int i = 0; i < size; i++) list.Add(max + r.Next(max));
			list.Clear();
			foreach (var x in arr) list.Add(x);
			Array.Sort(arr);
			CollectionAssert.AreEqual(arr, list.ToArray());
		}
		[TestMethod]
		public void ClearTest()
		{
			int size = 4000;
			int[] arr = new int[size];
			int max = size * size;
			Random r = new();
			for (int i = 0; i < size; i++) arr[i] = r.Next(max);
			var list = SkipList.New<int>(20);
			for (int i = 0; i < size; i++) list.Add(max + r.Next(max));
			list.Clear();
			foreach (var x in arr) list.Add(x);
			Array.Sort(arr);
			CollectionAssert.AreEqual(arr, list.ToArray());
		}
		[TestMethod]
		public void RemoveFirstTest()
		{
			int[] arr = new int[] { 90, 10, 23, 25, 37, 14, 38, 11, 24, 98, 77 };
			var list = SkipList.New<int>(3);
			foreach (var x in arr)
			{
				list.Add(x);
			}
			list.Add(190);
			list.Add(102);
			list.Add(204);
			Assert.IsTrue(list.TryRemoveFirst(x => x > 100));
			Assert.IsTrue(list.TryRemoveFirst(x => x > 100));
			Assert.IsTrue(list.TryRemoveFirst(x => x > 100));
			Assert.IsFalse(list.TryRemoveFirst(x => x > 100));
			System.Array.Sort(arr);
			int[] actual = list.ToArray();
			CollectionAssert.AreEqual(arr, actual);
		}
		[TestMethod]
		public void RemoveAllTest_1()
		{
			int size = 50;
			int[] arr = new int[size];
			int max = 500;
			Random r = new();
			for (int i = 0; i < size; i++) arr[i] = r.Next(max);
			var list = SkipList.New<int>(7);
			foreach (var x in arr) list.Add(x);
			for (int i = 0; i < size; i++) list.Add(max + r.Next(max));
			list.RemoveAll(x => x > max);
			System.Array.Sort(arr);
			int[] actual = list.ToArray();
			CollectionAssert.AreEqual(arr, actual);
		}
		[TestMethod]
		public void RemoveAllTest_2()
		{
			int size = 500;
			int[] arr = new int[size];
			int max = 5000;
			Random r = new();
			for (int i = 0; i < size; i++) arr[i] = r.Next(max) * 2; // Even numbers
			var list = SkipList.New<int>(7);
			foreach (var x in arr) list.Add(x);
			for (int i = 0; i < size; i++) list.Add(1 + (2 * r.Next(max))); // Odd numbers
			list.RemoveAll(x => x % 2 != 0);
			System.Array.Sort(arr);
			int[] actual = list.ToArray();
			CollectionAssert.AreEqual(arr, actual);
		}
		[TestMethod]
		public void EmptyIterationTest_1()
		{
			var list = SkipList.New<int>(5);
			CollectionAssert.AreEqual(Array.Empty<int>(), list.ToArray());
		}
		[TestMethod]
		public void EmptyIterationTest_2()
		{
			var list = SkipList.New<int>(5);
			foreach (var _ in list)
			{
				Assert.Fail();
			}
		}
		[TestMethod]
		public void EmptyIterationTest_3()
		{
			var list = SkipList.New<int>(5);
			int[] x = RandomIntArray(100, 1000000);
			foreach (var item in x)
			{
				list.Add(item);
			}
			foreach (var item in x)
			{
				Assert.IsTrue(list.TryRemove(item).Success);
			}
			CollectionAssert.AreEqual(Array.Empty<int>(), list.ToArray());
		}
		[TestMethod]
		public void EmptyIterationTest_4()
		{
			var list = SkipList.New<int>(5);
			int[] x = RandomIntArray(100, 1000000);
			foreach (var item in x)
			{
				list.Add(item);
			}
			foreach (var item in x)
			{
				Assert.IsTrue(list.TryRemove(item).Success);
			}
			foreach (var _ in list)
			{
				Assert.Fail();
			}
		}
		public static int[] RandomIntArray(int size, int max)
		{
			int[] array = new int[size];
			Random r = new();
			for (int i = 0; i < size; i++)
			{
				array[i] = r.Next(max);
			}
			return array;
		}
	}
}