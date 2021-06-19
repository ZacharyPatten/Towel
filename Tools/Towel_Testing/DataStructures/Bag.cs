using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Towel;
using static Towel.Statics;
using Towel.DataStructures;

namespace Towel_Testing.DataStructures
{
	[TestClass]
	public class BagMap_Testing
	{
#pragma warning disable CA1829 // Use Length/Count property instead of Count() when available
		[TestMethod]
		public void A()
		{
			IBag<int> bag = BagMap.New<int>();
			const int size = 1000;
			Assert.IsTrue(bag.Count is 0);
			for (int i = 0; i < size; i++)
			{
				Assert.IsTrue(bag[i] is 0);
				Assert.IsTrue(bag.Get(i) is 0);
				Assert.IsTrue(bag.TryGet(i) is (true, null, 0));
				Assert.IsTrue(!bag.Contains(i));
			}
			for (int i = 0; i < size; i++)
			{
				Assert.IsTrue(bag.TryAdd(i) is (true, null));
				Assert.IsTrue(bag[i] is 1);
				Assert.IsTrue(bag.Get(i) is 1);
				Assert.IsTrue(bag.TryGet(i) is (true, null, 1));
				Assert.IsTrue(bag.Count == i + 1);
				Assert.IsTrue(bag.Contains(i));
			}
			Assert.IsTrue(bag.Count() is size);
			Assert.IsTrue(bag.GetCounts().Count() is size);
			Assert.IsTrue(bag.GetCounts().All(x => x.Count is 1));
			for (int i = 0; i < size; i++)
			{
				Assert.IsTrue(bag.TryAdd(i) is (true, null));
				Assert.IsTrue(bag[i] is 2);
				Assert.IsTrue(bag.Get(i) is 2);
				Assert.IsTrue(bag.TryGet(i) is (true, null, 2));
				Assert.IsTrue(bag.Count == size + i + 1);
				Assert.IsTrue(bag.Contains(i));
			}
			Assert.IsTrue(bag.Count() is 2 * size);
			Assert.IsTrue(bag.GetCounts().Count() is size);
			Assert.IsTrue(bag.GetCounts().All(x => x.Count is 2));
			for (int i = 0; i < size; i++)
			{
				bag.Add(i);
				Assert.IsTrue(bag[i] is 3);
				Assert.IsTrue(bag.Get(i) is 3);
				Assert.IsTrue(bag.TryGet(i) is (true, null, 3));
				Assert.IsTrue(bag.Count == size * 2 + i + 1);
				Assert.IsTrue(bag.Contains(i));
			}
			Assert.IsTrue(bag.Count() is 3 * size);
			Assert.IsTrue(bag.GetCounts().Count() is size);
			Assert.IsTrue(bag.GetCounts().All(x => x.Count is 3));
			for (int i = 0; i < size; i++)
			{
				Assert.IsTrue(bag.TryRemove(i) is (true, null, 3, 2));
				Assert.IsTrue(bag[i] is 2);
				Assert.IsTrue(bag.Get(i) is 2);
				Assert.IsTrue(bag.TryGet(i) is (true, null, 2));
				Assert.IsTrue(bag.Count == size * 3 - i - 1);
				Assert.IsTrue(bag.Contains(i));
			}
			Assert.IsTrue(bag.Count() is 2 * size);
			Assert.IsTrue(bag.GetCounts().Count() is size);
			Assert.IsTrue(bag.GetCounts().All(x => x.Count is 2));
			for (int i = 0; i < size; i++)
			{
				bag.Remove(i);
				Assert.IsTrue(bag[i] is 1);
				Assert.IsTrue(bag.Get(i) is 1);
				Assert.IsTrue(bag.TryGet(i) is (true, null, 1));
				Assert.IsTrue(bag.Count == size * 2 - i - 1);
				Assert.IsTrue(bag.Contains(i));
			}
			Assert.IsTrue(bag.Count() is size);
			Assert.IsTrue(bag.GetCounts().Count() is size);
			Assert.IsTrue(bag.GetCounts().All(x => x.Count is 1));
		}
#pragma warning restore CA1829 // Use Length/Count property instead of Count() when available
	}
}