using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Towel;
using Towel.DataStructures;

namespace Towel_Testing.DataStructures
{
	[TestClass] public class RedBlackTreeLinked_Testing
	{
		[TestMethod] public void TryAdd_Testing()
		{
			// adding duplicate values should return false
			{
				IRedBlackTree<int> tree = RedBlackTreeLinked.New<int>();
				Assert.IsTrue(tree.TryAdd(1));
				Assert.IsFalse(tree.TryAdd(1));
			}
			// normal add checking
			{
				IRedBlackTree<int> tree = RedBlackTreeLinked.New<int>();
				Assert.IsTrue(tree.TryAdd(1));
				Assert.IsTrue(tree.TryAdd(2));
				Assert.IsTrue(tree.TryAdd(3));
				Assert.IsTrue(tree.Count == 3);
				Assert.IsTrue(tree.Contains(1));
				Assert.IsTrue(tree.Contains(2));
				Assert.IsTrue(tree.Contains(3));
			}
			{
				IRedBlackTree<int> tree = RedBlackTreeLinked.New<int>();
				Extensions.Iterate(100, i => tree.TryAdd(i));
				Assert.IsTrue(tree.Count == 100);
				Extensions.Iterate(100, i => Assert.IsTrue(tree.Contains(i)));
			}
		}

		[TestMethod] public void Add_Testing()
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
				Extensions.Iterate(100, i => tree.Add(i));
				Assert.IsTrue(tree.Count == 100);
				Extensions.Iterate(100, i => Assert.IsTrue(tree.Contains(i)));
			}
		}

		[TestMethod] public void TryRemove_Testing()
		{
			// removing a non-existing value should return false
			{
				IRedBlackTree<int> tree = RedBlackTreeLinked.New<int>();
				tree.Add(1);
				tree.Add(3);
				Assert.IsFalse(tree.TryRemove(2));
			}
			// normal remove checking
			{
				IRedBlackTree<int> tree = RedBlackTreeLinked.New<int>();
				tree.Add(1);
				tree.Add(2);
				tree.Add(3);
				Assert.IsTrue(tree.Count == 3);
				Assert.IsTrue(tree.TryRemove(1));
				Assert.IsFalse(tree.Contains(1));
				Assert.IsTrue(tree.Count == 2);
				Assert.IsTrue(tree.TryRemove(2));
				Assert.IsFalse(tree.Contains(2));
				Assert.IsTrue(tree.Count == 1);
				Assert.IsTrue(tree.TryRemove(3));
				Assert.IsFalse(tree.Contains(3));
				Assert.IsTrue(tree.Count == 0);
			}
			{
				IRedBlackTree<int> tree = RedBlackTreeLinked.New<int>();
				Extensions.Iterate(100, i => tree.Add(i));
				Assert.IsTrue(tree.Count == 100);
				Extensions.Iterate(100, i => tree.TryRemove(i));
				Assert.IsTrue(tree.Count == 0);
			}
		}

		[TestMethod] public void Remove_Testing()
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
				Assert.IsTrue(tree.Count == 3);
				tree.Remove(1);
				Assert.IsFalse(tree.Contains(1));
				Assert.IsTrue(tree.Count == 2);
				tree.Remove(2);
				Assert.IsFalse(tree.Contains(2));
				Assert.IsTrue(tree.Count == 1);
				tree.Remove(3);
				Assert.IsFalse(tree.Contains(3));
				Assert.IsTrue(tree.Count == 0);
			}
			{
				IRedBlackTree<int> tree = RedBlackTreeLinked.New<int>();
				Extensions.Iterate(100, i => tree.Add(i));
				Assert.IsTrue(tree.Count == 100);
				Extensions.Iterate(100, i => tree.Remove(i));
				Assert.IsTrue(tree.Count == 0);
			}
		}
	}
}
