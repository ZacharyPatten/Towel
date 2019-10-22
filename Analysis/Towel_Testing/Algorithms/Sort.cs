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

		public static void TestAlgorithm(Action<int[]> algorithm)
		{
			Random random = new Random(randomSeed);
			int[] array = new int[size];
			Stepper.Iterate(size, i => array[i] = i);
			Sort.Shuffle(random, array);
			Assert.IsFalse(IsLeastToGreatest(array)); // make sure the data is randomized before algorithm
			algorithm(array);
			Assert.IsTrue(IsLeastToGreatest(array));
		}

		[TestMethod] public void Shuffle_Testing()
		{
			Random random = new Random(randomSeed);
			int[] array = new int[size];
			Stepper.Iterate(size, i => array[i] = i);
			Sort.Shuffle(random, array);
			Assert.IsFalse(IsLeastToGreatest(array));
		}

		[TestMethod] public void Bubble_Testing() => TestAlgorithm(Sort.Bubble);

		[TestMethod] public void Insertion_Testing() => TestAlgorithm(Sort.Insertion);

		[TestMethod] public void Selection_Testing() => TestAlgorithm(Sort.Selection);

		[TestMethod] public void Merge_Testing() => TestAlgorithm(Sort.Merge);

		[TestMethod] public void Quick_Testing() => TestAlgorithm(Sort.Quick);

		[TestMethod] public void Heap_Testing() => TestAlgorithm(Sort.Heap);
	}
}
