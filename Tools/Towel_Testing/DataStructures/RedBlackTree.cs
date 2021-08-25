using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel;
using Towel.DataStructures;
using static Towel.Statics;

namespace Towel_Testing.DataStructures
{
	[TestClass]
	public class RedBlackTreeLinked_Testing
	{
		[TestMethod]
		public void TryAdd_Testing()
		{
			// adding duplicate values should return false
			{
				IRedBlackTree<int> tree = RedBlackTreeLinked.New<int>();
				Assert.IsTrue(tree.TryAdd(1).Success);
				Assert.IsFalse(tree.TryAdd(1).Success);
			}
			// normal add checking
			{
				IRedBlackTree<int> tree = RedBlackTreeLinked.New<int>();
				Assert.IsTrue(tree.TryAdd(1).Success);
				Assert.IsTrue(tree.TryAdd(2).Success);
				Assert.IsTrue(tree.TryAdd(3).Success);
				Assert.IsTrue(tree.Count is 3);
				Assert.IsTrue(tree.Contains(1));
				Assert.IsTrue(tree.Contains(2));
				Assert.IsTrue(tree.Contains(3));
			}
			{
				IRedBlackTree<int> tree = RedBlackTreeLinked.New<int>();
				Iterate(100, i => tree.TryAdd(i));
				Assert.IsTrue(tree.Count is 100);
				Iterate(100, i => Assert.IsTrue(tree.Contains(i)));
			}
		}

		[TestMethod]
		public void Add_Testing()
		{
			// adding duplicate values should throw exceptions
			{
				IRedBlackTree<int> tree = RedBlackTreeLinked.New<int>();
				tree.Add(1);
				Assert.ThrowsException<ArgumentException>(() => tree.Add(1));
			}
			// normal add checking
			{
				IRedBlackTree<int> tree = RedBlackTreeLinked.New<int>();
				tree.Add(1);
				tree.Add(2);
				tree.Add(3);
				Assert.IsTrue(tree.Count == 3);
				Assert.IsTrue(tree.Contains(1));
				Assert.IsTrue(tree.Contains(2));
				Assert.IsTrue(tree.Contains(3));
			}
			{
				IRedBlackTree<int> tree = RedBlackTreeLinked.New<int>();
				Iterate(100, i => tree.Add(i));
				Assert.IsTrue(tree.Count is 100);
				Iterate(100, i => Assert.IsTrue(tree.Contains(i)));
			}
		}

		[TestMethod]
		public void TryRemove_Testing()
		{
			// removing a non-existing value should return false
			{
				IRedBlackTree<int> tree = RedBlackTreeLinked.New<int>();
				tree.Add(1);
				tree.Add(3);
				Assert.IsFalse(tree.TryRemove(2).Success);
			}
			// normal remove checking
			{
				IRedBlackTree<int> tree = RedBlackTreeLinked.New<int>();
				tree.Add(1);
				tree.Add(2);
				tree.Add(3);
				Assert.IsTrue(tree.Count is 3);
				Assert.IsTrue(tree.TryRemove(1).Success);
				Assert.IsFalse(tree.Contains(1));
				Assert.IsTrue(tree.Count is 2);
				Assert.IsTrue(tree.TryRemove(2).Success);
				Assert.IsFalse(tree.Contains(2));
				Assert.IsTrue(tree.Count is 1);
				Assert.IsTrue(tree.TryRemove(3).Success);
				Assert.IsFalse(tree.Contains(3));
				Assert.IsTrue(tree.Count is 0);
			}
			{
				IRedBlackTree<int> tree = RedBlackTreeLinked.New<int>();
				Iterate(100, i => tree.Add(i));
				Assert.IsTrue(tree.Count is 100);
				Iterate(100, i => tree.Remove(i));
				Assert.IsTrue(tree.Count is 0);
			}
		}

		[TestMethod]
		public void Remove_Testing()
		{
			// removing a non-existing value should throw exceptions
			{
				IRedBlackTree<int> tree = RedBlackTreeLinked.New<int>();
				tree.Add(1);
				tree.Add(3);
				Assert.ThrowsException<ArgumentException>(() => tree.Remove(2));
			}
			// normal remove checking
			{
				IRedBlackTree<int> tree = RedBlackTreeLinked.New<int>();
				tree.Add(1);
				tree.Add(2);
				tree.Add(3);
				Assert.IsTrue(tree.Count is 3);
				tree.Remove(1);
				Assert.IsFalse(tree.Contains(1));
				Assert.IsTrue(tree.Count is 2);
				tree.Remove(2);
				Assert.IsFalse(tree.Contains(2));
				Assert.IsTrue(tree.Count is 1);
				tree.Remove(3);
				Assert.IsFalse(tree.Contains(3));
				Assert.IsTrue(tree.Count is 0);
			}
			{
				IRedBlackTree<int> tree = RedBlackTreeLinked.New<int>();
				Iterate(100, i => tree.Add(i));
				Assert.IsTrue(tree.Count is 100);
				Iterate(100, i => tree.Remove(i));
				Assert.IsTrue(tree.Count is 0);
			}
		}

		[TestMethod]
		public void GetEnumerable_Testing()
		{
			#pragma warning disable CA1829 // Use Length/Count property instead of Count() when available
			{
				IRedBlackTree<int> tree = RedBlackTreeLinked.New<int>();
				Assert.IsTrue(tree.Count() is 0);
			}
			{
				IRedBlackTree<int> tree = RedBlackTreeLinked.New<int>();
				Assert.IsTrue(tree.TryAdd(1).Success);
				Assert.IsTrue(tree.Count() is 1);
				Assert.IsTrue(tree.SequenceEqual(Ɐ(1)));
			}
			{
				IRedBlackTree<int> tree = RedBlackTreeLinked.New<int>();
				Assert.IsTrue(tree.TryAdd(1).Success);
				Assert.IsTrue(tree.TryAdd(2).Success);
				Assert.IsTrue(tree.TryAdd(3).Success);
				Assert.IsTrue(tree.Count() is 3);
				Assert.IsTrue(tree.SequenceEqual(Ɐ(1, 2, 3)));
			}
			{
				IRedBlackTree<int> tree = RedBlackTreeLinked.New<int>();
				Assert.IsTrue(tree.TryAdd(1).Success);
				Assert.IsTrue(tree.TryAdd(2).Success);
				Assert.IsTrue(tree.TryAdd(3).Success);
				Assert.IsTrue(tree.TryAdd(4).Success);
				Assert.IsTrue(tree.TryAdd(5).Success);
				Assert.IsTrue(tree.TryAdd(6).Success);
				Assert.IsTrue(tree.TryAdd(7).Success);
				Assert.IsTrue(tree.TryAdd(8).Success);
				Assert.IsTrue(tree.TryAdd(9).Success);
				Assert.IsTrue(tree.Count() is 9);
				Assert.IsTrue(tree.SequenceEqual(Ɐ(1, 2, 3, 4, 5, 6, 7, 8, 9)));
			}
			#pragma warning restore CA1829 // Use Length/Count property instead of Count() when available
		}
	}
}
