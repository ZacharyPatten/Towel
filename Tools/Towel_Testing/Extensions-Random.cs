using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel;
using Towel.DataStructures;

namespace Towel_Testing
{
	// [TestClass]
	public partial class Extensions_Testing
	{
		[TestMethod]
		public void Random_UniqueNext()
		{
			#region count < Math.Sqrt(maxValue - minValue)

			{ // test shifting from maxValue
				int i = 999;
				TestingRandom random = new(() => i--);
				ISet<int> set = SetHashLinked.New<int>();
				random.NextUnique(5, 0, 1000, j => { Assert.IsFalse(set.Contains(j)); set.Add(j); });
				Assert.IsTrue(set.Count == 5);
			}
			{ // test shifting from 0
				TestingRandom random = new(() => 0);
				ISet<int> set = SetHashLinked.New<int>();
				random.NextUnique(5, 0, 1000, i => { Assert.IsFalse(set.Contains(i)); set.Add(i); });
				Assert.IsTrue(set.Count == 5);
			}
			{ // test shifting from inner value
				TestingRandom random = new(() => 7);
				ISet<int> set = SetHashLinked.New<int>();
				random.NextUnique(5, 0, 1000, i => { Assert.IsFalse(set.Contains(i)); set.Add(i); });
				Assert.IsTrue(set.Count == 5);
			}
			{ // test
				Random random = new();
				for (int i = 0; i < 10000; i++)
				{
					ISet<int> set = SetHashLinked.New<int>();
					random.NextUnique(5, 0, 1000, j => { Assert.IsFalse(set.Contains(j)); set.Add(j); });
					Assert.IsTrue(set.Count == 5);
				}
			}
			{ // test invalid random argument exception
				TestingRandom random = new(() => 1000);
				Assert.ThrowsException<ArgumentException>(() => random.NextUnique(5, 0, 1000, i => { }));
			}
			{ // test invalid random argument exception
				TestingRandom random = new(() => -1);
				Assert.ThrowsException<ArgumentException>(() => random.NextUnique(5, 0, 1000, i => { }));
			}

			#endregion

			#region count >= Math.Sqrt(maxValue - minValue)

			{ // test shifting from maxValue
				int i = 999;
				TestingRandom random = new(() => i--);
				ISet<int> set = SetHashLinked.New<int>();
				random.NextUnique(100, 0, 1000, j => { Assert.IsFalse(set.Contains(j)); set.Add(j); });
				Assert.IsTrue(set.Count == 100);
			}
			{ // test shifting from 0
				TestingRandom random = new(() => 0);
				ISet<int> set = SetHashLinked.New<int>();
				random.NextUnique(100, 0, 1000, i => { Assert.IsFalse(set.Contains(i)); set.Add(i); });
				Assert.IsTrue(set.Count == 100);
			}
			{ // test shifting from inner value
				TestingRandom random = new(() => 7);
				ISet<int> set = SetHashLinked.New<int>();
				random.NextUnique(100, 0, 1000, i => { Assert.IsFalse(set.Contains(i)); set.Add(i); });
				Assert.IsTrue(set.Count == 100);
			}
			{ // test
				Random random = new();
				for (int i = 0; i < 10000; i++)
				{
					ISet<int> set = SetHashLinked.New<int>();
					random.NextUnique(100, 0, 1000, j => { Assert.IsFalse(set.Contains(j)); set.Add(j); });
					Assert.IsTrue(set.Count == 100);
				}
			}
			{ // test invalid random argument exception
				TestingRandom random = new(() => 1000);
				Assert.ThrowsException<ArgumentException>(() => random.NextUnique(100, 0, 1000, i => { }));
			}
			{ // test invalid random argument exception
				TestingRandom random = new(() => -1);
				Assert.ThrowsException<ArgumentException>(() => random.NextUnique(100, 0, 1000, i => { }));
			}

			#endregion
		}

		[TestMethod]
		public void Random_NextDecimal()
		{
			Random random = new(7);
			_ = random.NextDecimal();
			{
				const decimal min = 0;
				const decimal max = 100000;
				decimal a = random.NextDecimal(min, max);
				Assert.IsTrue(min <= a && a <= max);
				decimal b = random.NextDecimal(min, max);
				Assert.IsTrue(min <= b && b <= max);
				decimal c = random.NextDecimal(min, max);
				Assert.IsTrue(min <= c && c <= max);
			}
			{
				const decimal min = -100000;
				const decimal max = 0;
				decimal a = random.NextDecimal(min, max);
				Assert.IsTrue(min <= a && a <= max);
				decimal b = random.NextDecimal(min, max);
				Assert.IsTrue(min <= b && b <= max);
				decimal c = random.NextDecimal(min, max);
				Assert.IsTrue(min <= c && c <= max);
			}
			{
				const decimal min = -100000;
				const decimal max = 100000;
				decimal a = random.NextDecimal(min, max);
				Assert.IsTrue(min <= a && a <= max);
				decimal b = random.NextDecimal(min, max);
				Assert.IsTrue(min <= b && b <= max);
				decimal c = random.NextDecimal(min, max);
				Assert.IsTrue(min <= c && c <= max);
			}
			{
				const decimal min = decimal.MinValue;
				const decimal max = decimal.MaxValue;
				Assert.ThrowsException<ArgumentOutOfRangeException>(() => random.NextDecimal(min, max));
			}
		}
	}
}
