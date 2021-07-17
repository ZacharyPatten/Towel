using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel;
using static Towel.Statics;

namespace Towel_Testing
{
	// [TestClass]
	public partial class Statics_Testing
	{
		#region NextUniqueRollTracking

		[TestMethod]
		public void NextUniqueRollTracking_Testing()
		{
			// count > sqrt(max - min)
			{
				Func<int, int, int> next = (_, max) => 0; // 0, 0, 0, 0, 0
				int[] output = NextUniqueRollTracking<SFunc<int, int, int>>(5, 0, 5, next);
				Assert.IsTrue(SetEquals<int>(output, new[] { 0, 1, 2, 3, 4, }));
			}
			{
				Func<int, int, int> next = (_, max) => -5; // 0, 0, 0, 0, 0
				int[] output = NextUniqueRollTracking<SFunc<int, int, int>>(5, -5, 0, next);
				Assert.IsTrue(SetEquals<int>(output, new[] { -5, -4, -3, -2, -1, }));
			}
			{
				Func<int, int, int> next = (_, max) => max - 1; // 4, 3, 2, 1, 0
				int[] output = NextUniqueRollTracking<SFunc<int, int, int>>(5, 0, 5, next);
				Assert.IsTrue(SetEquals<int>(output, new[] { 0, 1, 2, 3, 4, }));
			}
			// count > sqrt(max - min)
			{
				Func<int, int, int> next = (_, max) => 0; // 0, 0, 0, 0, 0
				int[] output = NextUniqueRollTracking<SFunc<int, int, int>>(5, 0, 500, next);
				Assert.IsTrue(output.Length is 5);
				Assert.IsTrue(!Any<int>(output, i => i < 0 || i >= 500));
				Assert.IsTrue(!ContainsDuplicates<int>(output));
			}
			{
				Func<int, int, int> next = (_, max) => -500; // 0, 0, 0, 0, 0
				int[] output = NextUniqueRollTracking<SFunc<int, int, int>>(5, -500, 0, next);
				Assert.IsTrue(output.Length is 5);
				Assert.IsTrue(!Any<int>(output, i => i < -500 || i >= 0));
				Assert.IsTrue(!ContainsDuplicates<int>(output));
			}
			{
				Func<int, int, int> next = (_, max) => max - 1; // 4, 3, 2, 1, 0
				int[] output = NextUniqueRollTracking<SFunc<int, int, int>>(5, 0, 500, next);
				Assert.IsTrue(output.Length is 5);
				Assert.IsTrue(!Any<int>(output, i => i < 0 || i >= 500));
				Assert.IsTrue(!ContainsDuplicates<int>(output));
			}
			// exceptions
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => NextUniqueRollTracking<SFunc<int, int, int>>(-1, 0, 1, new Func<int, int, int>((_, _) => default)));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => NextUniqueRollTracking<SFunc<int, int, int>>(1, 0, -1, new Func<int, int, int>((_, _) => default)));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => NextUniqueRollTracking<SFunc<int, int, int>>(2, 0, 1, new Func<int, int, int>((_, _) => default)));
		}

		#endregion

		#region NextUniquePoolTracking (with exclusions)

		[TestMethod]
		public void NextUniquePoolTracking_exclusions_Testing()
		{
			// count > sqrt(max - min)
			{
				Func<int, int, int> next = (_, max) => 0; // 0, 0, 0, 0, 0
				int[] output = NextUniquePoolTracking<SFunc<int, int, int>>(
					count: 5,
					minValue: 0,
					maxValue: 10,
					excluded: new[] { 1, 3, 5, 7, 9, },
					random: next);
				Assert.IsTrue(SetEquals<int>(output, new[] { 0, 2, 4, 6, 8, }));
			}
			{
				Func<int, int, int> next = (_, max) => 0; // 0, 0, 0, 0, 0
				int[] output = NextUniquePoolTracking<SFunc<int, int, int>>(
					count: 5,
					minValue: 0,
					maxValue: 10,
					excluded: new[] { 0, 2, 4, 6, 8, },
					random: next);
				Assert.IsTrue(SetEquals<int>(output, new[] { 1, 3, 5, 7, 9, }));
			}
			{
				Random random = new();
				const int minValue = 0;
				const int maxValue = 1000;
				const int count = 6;
				for (int i = 0; i < 1000; i++)
				{
					const int excludeCount = 100;
					int[] excludes = new int[excludeCount];
					for (int j = 0; j < excludeCount; j++)
					{
						excludes[j] = random.Next(minValue, maxValue);
					}
					int[] output = random.NextUniquePoolTracking(
						count: count,
						minValue: minValue,
						maxValue: maxValue,
						excluded: excludes);
					Assert.IsTrue(output.Length is count);
					Assert.IsTrue(!ContainsDuplicates<int>(output));
					Assert.IsTrue(!Any<int>(output, value => value < minValue));
					Assert.IsTrue(!Any<int>(output, value => value >= maxValue));
					Assert.IsTrue(!Any<int>(output, value => Contains(excludes, value)));
				}
			}
			// exceptions
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => NextUniqueRollTracking<SFunc<int, int, int>>(-1, 0, 1, excluded: new[] { -2 }, new Func<int, int, int>((_, _) => default)));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => NextUniqueRollTracking<SFunc<int, int, int>>(1, 0, -1, excluded: new[] { -2 }, new Func<int, int, int>((_, _) => default)));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => NextUniqueRollTracking<SFunc<int, int, int>>(2, 0, 1, excluded: new[] { -2 }, new Func<int, int, int>((_, _) => default)));
			Assert.ThrowsException<ArgumentException>(() => NextUniqueRollTracking<SFunc<int, int, int>>(2, 0, 2, excluded: new[] { 0 }, new Func<int, int, int>((_, _) => default)));
		}

		#endregion

		#region NextUniquePoolTracking (with exclusions)

		[TestMethod]
		public void NextUniqueRollTracking_exclusions_Testing()
		{
			// count > sqrt(max - min)
			{
				Func<int, int, int> next = (_, max) => 0; // 0, 0, 0, 0, 0
				int[] output = NextUniqueRollTracking<SFunc<int, int, int>>(
					count: 5,
					minValue: 0,
					maxValue: 10,
					excluded: new[] { 1, 3, 5, 7, 9, },
					random: next);
				Assert.IsTrue(SetEquals<int>(output, new[] { 0, 2, 4, 6, 8, }));
			}
			{
				Func<int, int, int> next = (_, max) => 0; // 0, 0, 0, 0, 0
				int[] output = NextUniqueRollTracking<SFunc<int, int, int>>(
					count: 5,
					minValue: 0,
					maxValue: 10,
					excluded: new[] { 0, 2, 4, 6, 8, },
					random: next);
				Assert.IsTrue(SetEquals<int>(output, new[] { 1, 3, 5, 7, 9, }));
			}
			{
				Random random = new();
				const int minValue = 0;
				const int maxValue = 1000;
				const int count = 6;
				for (int i = 0; i < 1000; i++)
				{
					const int excludeCount = 100;
					int[] excludes = new int[excludeCount];
					for (int j = 0; j < excludeCount; j++)
					{
						excludes[j] = random.Next(minValue, maxValue);
					}
					int[] output = random.NextUniqueRollTracking(
						count: count,
						minValue: minValue,
						maxValue: maxValue,
						excluded: excludes);
					Assert.IsTrue(output.Length is count);
					Assert.IsTrue(!ContainsDuplicates<int>(output));
					Assert.IsTrue(!Any<int>(output, value => value < minValue));
					Assert.IsTrue(!Any<int>(output, value => value >= maxValue));
					Assert.IsTrue(!Any<int>(output, value => Contains(excludes, value)));
				}
			}
			// exceptions
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => NextUniqueRollTracking<SFunc<int, int, int>>(-1, 0, 1, excluded: new[] { -2 }, new Func<int, int, int>((_, _) => default)));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => NextUniqueRollTracking<SFunc<int, int, int>>(1, 0, -1, excluded: new[] { -2 }, new Func<int, int, int>((_, _) => default)));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => NextUniqueRollTracking<SFunc<int, int, int>>(2, 0, 1, excluded: new[] { -2 }, new Func<int, int, int>((_, _) => default)));
			Assert.ThrowsException<ArgumentException>(() => NextUniqueRollTracking<SFunc<int, int, int>>(2, 0, 2, excluded: new[] { 0 }, new Func<int, int, int>((_, _) => default)));
		}

		#endregion

		#region NextUniquePoolTracking

		[TestMethod]
		public void NextUniquePoolTracking_Testing()
		{
			// count > sqrt(max - min)
			{
				Func<int, int, int> next = (_, max) => 0; // 0, 0, 0, 0, 0
				int[] output = NextUniquePoolTracking<SFunc<int, int, int>>(5, 0, 5, next);
				Assert.IsTrue(SetEquals<int>(output, new[] { 0, 1, 2, 3, 4, }));
			}
			{
				Func<int, int, int> next = (_, max) => 0; // 0, 0, 0, 0, 0
				int[] output = NextUniquePoolTracking<SFunc<int, int, int>>(5, -5, 0, next);
				Assert.IsTrue(SetEquals<int>(output, new[] { -5, -4, -3, -2, -1, }));
			}
			{
				Func<int, int, int> next = (_, max) => max - 1; // 4, 3, 2, 1, 0
				int[] output = NextUniquePoolTracking<SFunc<int, int, int>>(5, 0, 5, next);
				Assert.IsTrue(SetEquals<int>(output, new[] { 0, 1, 2, 3, 4, }));
			}
			// count > sqrt(max - min)
			{
				Func<int, int, int> next = (_, max) => 0; // 0, 0, 0, 0, 0
				int[] output = NextUniquePoolTracking<SFunc<int, int, int>>(5, 0, 500, next);
				Assert.IsTrue(output.Length is 5);
				Assert.IsTrue(!Any<int>(output, i => i < 0 || i >= 500));
				Assert.IsTrue(!ContainsDuplicates<int>(output));
			}
			{
				Func<int, int, int> next = (_, max) => 0; // 0, 0, 0, 0, 0
				int[] output = NextUniquePoolTracking<SFunc<int, int, int>>(5, -500, 0, next);
				Assert.IsTrue(output.Length is 5);
				Assert.IsTrue(!Any<int>(output, i => i < -500 || i >= 0));
				Assert.IsTrue(!ContainsDuplicates<int>(output));
			}
			{
				Func<int, int, int> next = (_, max) => max - 1; // 4, 3, 2, 1, 0
				int[] output = NextUniquePoolTracking<SFunc<int, int, int>>(5, 0, 500, next);
				Assert.IsTrue(output.Length is 5);
				Assert.IsTrue(!Any<int>(output, i => i < 0 || i >= 500));
				Assert.IsTrue(!ContainsDuplicates<int>(output));
			}
			// exceptions
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => NextUniquePoolTracking<SFunc<int, int, int>>(-1, 0, 1, new Func<int, int, int>((_, _) => default)));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => NextUniquePoolTracking<SFunc<int, int, int>>(1, 0, -1, new Func<int, int, int>((_, _) => default)));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => NextUniquePoolTracking<SFunc<int, int, int>>(2, 0, 1, new Func<int, int, int>((_, _) => default)));
		}

		#endregion
	}
}
