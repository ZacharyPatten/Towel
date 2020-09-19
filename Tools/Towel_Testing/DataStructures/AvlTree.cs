using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Towel;
using Towel.DataStructures;
using static Towel.Statics;

namespace Towel_Testing.DataStructures
{
	[TestClass] public class AvleTreeLinked_Testing
	{
		[TestMethod] public void TryAdd_Testing()
		{
			// adding duplicate values should return false
			{
				IAvlTree<int> tree = new AvlTreeLinked<int>();
				Assert.IsTrue(tree.TryAdd(1));
				Assert.IsFalse(tree.TryAdd(1));
			}
			// normal add checking
			{
				IAvlTree<int> tree = new AvlTreeLinked<int>();
				Assert.IsTrue(tree.TryAdd(1));
				Assert.IsTrue(tree.TryAdd(2));
				Assert.IsTrue(tree.TryAdd(3));
				Assert.IsTrue(tree.Count == 3);
				Assert.IsTrue(tree.Contains(1));
				Assert.IsTrue(tree.Contains(2));
				Assert.IsTrue(tree.Contains(3));
			}
			{
				IAvlTree<int> tree = new AvlTreeLinked<int>();
				Extensions.Iterate(100, i => tree.TryAdd(i));
				Assert.IsTrue(tree.Count == 100);
				Extensions.Iterate(100, i => Assert.IsTrue(tree.Contains(i)));
			}
		}

		[TestMethod] public void Add_Testing()
		{
			// adding duplicate values should throw exceptions
			{
				IAvlTree<int> tree = new AvlTreeLinked<int>() { 1, };
				Assert.ThrowsException<ArgumentException>(() => tree.Add(1));
			}
			// normal add checking
			{
				IAvlTree<int> tree = new AvlTreeLinked<int>() { 1, 2, 3, };
				Assert.IsTrue(tree.Count == 3);
				Assert.IsTrue(tree.Contains(1));
				Assert.IsTrue(tree.Contains(2));
				Assert.IsTrue(tree.Contains(3));
			}
			{
				IAvlTree<int> tree = new AvlTreeLinked<int>();
				Extensions.Iterate(100, i => tree.Add(i));
				Assert.IsTrue(tree.Count == 100);
				Extensions.Iterate(100, i => Assert.IsTrue(tree.Contains(i)));
			}
		}

		[TestMethod] public void TryRemove_Testing()
		{
			// removing a non-existing value should return false
			{
				IAvlTree<int> tree = new AvlTreeLinked<int>() { 1, 3, };
				Assert.IsFalse(tree.TryRemove(2));
			}
			// normal remove checking
			{
				IAvlTree<int> tree = new AvlTreeLinked<int>() { 1, 2, 3, };
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
				IAvlTree<int> tree = new AvlTreeLinked<int>();
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
				IAvlTree<int> tree = new AvlTreeLinked<int>() { 1, 3, };
				Assert.ThrowsException<ArgumentException>(() => tree.Remove(2));
			}
			// normal remove checking
			{
				IAvlTree<int> tree = new AvlTreeLinked<int>() { 1, 2, 3, };
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
				IAvlTree<int> tree = new AvlTreeLinked<int>();
				Extensions.Iterate(100, i => tree.Add(i));
				Assert.IsTrue(tree.Count == 100);
				Extensions.Iterate(100, i => tree.Remove(i));
				Assert.IsTrue(tree.Count == 0);
			}
			// large randomized data set
			{
				const int count = 1000;
				IAvlTree<int> tree = new AvlTreeLinked<int>();
				for (int i = 0; i < count; i++)
				{
					Assert.IsTrue(tree.Count == i);
					tree.Add(i);
					Assert.IsTrue(tree.Count == i + 1);
					Assert.IsTrue(tree.Contains(i));

					int stepperCount = 0;
					tree.Stepper(x => stepperCount++);
					Assert.IsTrue(stepperCount == tree.Count);
				}
				int[] values = new int[count];
				for (int i = 0; i < count; i++)
				{
					values[i] = i;
				}
				Random random = new Random(7);
				Shuffle<int>(values, random);
				for (int i = 0; i < count; i++)
				{
					Assert.IsTrue(tree.Count == count - i);
					tree.Remove(values[i]);
					Assert.IsTrue(tree.Count == count - i - 1);
					Assert.IsFalse(tree.Contains(values[i]));

					int stepperCount = 0;
					tree.Stepper(x => stepperCount++);
					Assert.IsTrue(stepperCount == tree.Count);
				}
			}
		}
	}
}
