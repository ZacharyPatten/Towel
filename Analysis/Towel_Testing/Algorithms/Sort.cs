using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Towel;
using static Towel.Syntax;
using Towel.Algorithms;

namespace Towel_Testing.Algorithms
{
	[TestClass] public class Sort_Testing
	{
		public const int size = 10;
		public const int randomSeed = 7;

		public static bool IsLeastToGreatest<T>(T[] array)
		{
			for (int i = 0; i < array.Length - 1; i++)
			{
				if (Comparison(array[i], array[i + 1]) == Greater)
				{
					return false;
				}
			}
			return true;
		}

		public static void TestAlgorithm(Action<int[]> algorithm, int? sizeOverride = null)
		{
			int sizeAdjusted = sizeOverride ?? size;
			Random random = new Random(randomSeed);
			int[] array = new int[sizeAdjusted];
			Stepper.Iterate(sizeAdjusted, i => array[i] = i);
			Sort.Shuffle(array, random);
			Assert.IsFalse(IsLeastToGreatest(array), "Test failed (invalid randomization).");
			algorithm(array);
			Assert.IsTrue(IsLeastToGreatest(array), "Sorting algorithm failed.");
		}

		[TestMethod] public void Shuffle_Testing()
		{
			Random random = new Random(randomSeed);
			int[] array = new int[size];
			Stepper.Iterate(size, i => array[i] = i);
			Sort.Shuffle(array, random);
			Assert.IsFalse(IsLeastToGreatest(array));
		}

		[TestMethod] public void Bubble_Testing() => TestAlgorithm(Sort.Bubble);

		[TestMethod] public void Insertion_Testing() => TestAlgorithm(Sort.Insertion);

		[TestMethod] public void Selection_Testing() => TestAlgorithm(Sort.Selection);

		[TestMethod] public void Merge_Testing() => TestAlgorithm(Sort.Merge);

		[TestMethod] public void Quick_Testing() => TestAlgorithm(Sort.Quick);

		[TestMethod] public void Heap_Testing() => TestAlgorithm(Sort.Heap);

		[TestMethod] public void OddEven_Testing() => TestAlgorithm(Sort.OddEven);

		[TestMethod] public void Slow_Testing() => TestAlgorithm(Sort.Slow);

		[TestMethod] public void Bogo_Testing() => TestAlgorithm(array => Sort.Bogo(array), 5);
	}
}
