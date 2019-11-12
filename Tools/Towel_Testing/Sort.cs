using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Towel;
using static Towel.Syntax;

namespace Towel_Testing
{
	[TestClass] public class Sort_Testing
	{
		public const int size = 10;
		public const int randomSeed = 7;

		public static bool IsLeastToGreatest<T>(T[] array)
		{
			for (int i = 0; i < array.Length - 1; i++)
			{
				if (Comparison(array[i], array[i + 1]) is Greater)
				{
					return false;
				}
			}
			return true;
		}

		public static int[] watchArray;

		public static void TestAlgorithm(
			Action<int[], Compare<int>> algorithm,
			Action<int[], int, int, Compare<int>> algorithmPartial,
			int? sizeOverride = null)
		{
			void Test(int sizeAdjusted)
			{
				Random random = new Random(randomSeed);
				int[] array = new int[sizeAdjusted];
				Stepper.Iterate(sizeAdjusted, i => array[i] = i);
				Sort.Shuffle(array, random);
				Assert.IsFalse(IsLeastToGreatest(array), "Test failed (invalid randomization).");
				algorithm(array, Compare.Default);
				Assert.IsTrue(IsLeastToGreatest(array), "Sorting algorithm failed.");
			}

			Test(sizeOverride ?? size); // Even Data Set
			Test((sizeOverride ?? size) + 1); // Odd Data Set
			if (sizeOverride is null) Test(1000); // Large(er) Data Set

			{ // Partial Array Sort
				int[] array = new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0, };
				watchArray = array;
				algorithmPartial(array, 3, 7, Compare.Default);
				int[] expected = new int[] { 9, 8, 7, /*|*/ 2, 3, 4, 5, 6, /*|*/ 1, 0, };
				for (int i = 0; i < size; i++)
				{
					Assert.IsTrue(array[i] == expected[i]);
				}
			}
		}

		[TestMethod] public void Shuffle_Testing()
		{
			Random random = new Random(randomSeed);
			int[] array = new int[size];
			Stepper.Iterate(size, i => array[i] = i);
			Sort.Shuffle(array, random);
			Assert.IsFalse(IsLeastToGreatest(array));
		}

		[TestMethod] public void Bubble_Testing() => TestAlgorithm(Sort.Bubble, Sort.Bubble);

		[TestMethod] public void Insertion_Testing() => TestAlgorithm(Sort.Insertion, Sort.Insertion);

		[TestMethod] public void Selection_Testing() => TestAlgorithm(Sort.Selection, Sort.Selection);

		[TestMethod] public void Merge_Testing() => TestAlgorithm(Sort.Merge, Sort.Merge);

		[TestMethod] public void Quick_Testing() => TestAlgorithm(Sort.Quick, Sort.Quick);

		[TestMethod] public void Heap_Testing() => TestAlgorithm(Sort.Heap, Sort.Heap);

		[TestMethod] public void OddEven_Testing() => TestAlgorithm(Sort.OddEven, Sort.OddEven);

		[TestMethod] public void Slow_Testing() => TestAlgorithm(Sort.Slow, Sort.Slow, 10);

		[TestMethod] public void Gnome_Testing() => TestAlgorithm(Sort.Gnome, Sort.Gnome);

		[TestMethod] public void Comb_Testing() => TestAlgorithm(Sort.Comb, Sort.Comb);

		[TestMethod] public void Shell_Testing() => TestAlgorithm(Sort.Shell, Sort.Shell);

		[TestMethod] public void Cocktail_Testing() => TestAlgorithm(Sort.Cocktail, Sort.Cocktail);

		[TestMethod] public void Bogo_Testing() => TestAlgorithm(
			(array, compare) => Sort.Bogo(array, compare),
			(array, start, end, compare) => Sort.Bogo(array, start, end, compare),
			6);
	}
}
