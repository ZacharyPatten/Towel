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
			Action<int[], Func<int, int, CompareResult>> algorithm,
			Action<int[], int, int, Func<int, int, CompareResult>> algorithmPartial,
			int? sizeOverride = null)
		{
			void Test(int sizeAdjusted)
			{
				Random random = new Random(randomSeed);
				int[] array = new int[sizeAdjusted];
				Extensions.Iterate(sizeAdjusted, i => array[i] = i);
				Shuffle(array, random);
				Assert.IsFalse(IsLeastToGreatest(array), "Test failed (invalid randomization).");
				algorithm(array, Comparison);
				Assert.IsTrue(IsLeastToGreatest(array), "Sorting algorithm failed.");
			}

			Test(sizeOverride ?? size); // Even Data Set
			Test((sizeOverride ?? size) + 1); // Odd Data Set
			if (sizeOverride is null) Test(1000); // Large(er) Data Set

			{ // Partial Array Sort
				int[] array = { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
				watchArray = array;
				algorithmPartial(array, 3, 7, Comparison);
				int[] expected = { 9, 8, 7, /*|*/ 2, 3, 4, 5, 6, /*|*/ 1, 0 };
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
			Extensions.Iterate(size, i => array[i] = i);
			Shuffle(array, random);
			Assert.IsFalse(IsLeastToGreatest(array));
		}

		[TestMethod] public void Bubble_Testing() => TestAlgorithm(SortBubble, SortBubble);

		[TestMethod] public void Insertion_Testing() => TestAlgorithm(SortInsertion, SortInsertion);

		[TestMethod] public void Selection_Testing() => TestAlgorithm(SortSelection, SortSelection);

		[TestMethod] public void Merge_Testing() => TestAlgorithm(SortMerge, SortMerge);

		[TestMethod] public void Quick_Testing() => TestAlgorithm(SortQuick, SortQuick);

		[TestMethod] public void Heap_Testing() => TestAlgorithm(SortHeap, SortHeap);

		[TestMethod] public void OddEven_Testing() => TestAlgorithm(SortOddEven, SortOddEven);

		[TestMethod] public void Slow_Testing() => TestAlgorithm(SortSlow, SortSlow, 10);

		[TestMethod] public void Gnome_Testing() => TestAlgorithm(SortGnome, SortGnome);

		[TestMethod] public void Comb_Testing() => TestAlgorithm(SortComb, SortComb);

		[TestMethod] public void Shell_Testing() => TestAlgorithm(SortShell, SortShell);

		[TestMethod] public void Cocktail_Testing() => TestAlgorithm(SortCocktail, SortCocktail);

		[TestMethod] public void Bogo_Testing() => TestAlgorithm(
			(array, compare) => SortBogo(array, compare),
			(array, start, end, compare) => SortBogo(array, start, end, compare),
			6);


		[TestMethod] public void BubbleSpan_Test()
		{
			Span<int> span = new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
			SortBubble<int, CompareInt>(span);
			for (int i = 1; i < span.Length; i++)
			{
				Assert.IsTrue(span[i - 1] <= span[i]);
			}
		}

		[TestMethod] public void InsertionSpan_Test()
		{
			Span<int> span = new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
			SortInsertion<int, CompareInt>(span);
			for (int i = 1; i < span.Length; i++)
			{
				Assert.IsTrue(span[i - 1] <= span[i]);
			}
		}

		[TestMethod] public void SelectionSpan_Test()
		{
			Span<int> span = new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
			SortInsertion<int, CompareInt>(span);
			for (int i = 1; i < span.Length; i++)
			{
				Assert.IsTrue(span[i - 1] <= span[i]);
			}
		}

		[TestMethod] public void MergeSpan_Test()
		{
			Span<int> span = new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
			SortMerge<int, CompareInt>(span);
			for (int i = 1; i < span.Length; i++)
			{
				Assert.IsTrue(span[i - 1] <= span[i]);
			}
		}

		[TestMethod] public void QuickSpan_Test()
		{
			Span<int> span = new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
			SortQuick<int, CompareInt>(span);
			for (int i = 1; i < span.Length; i++)
			{
				Assert.IsTrue(span[i - 1] <= span[i]);
			}
		}

		[TestMethod] public void HeapSpan_Test()
		{
			Span<int> span = new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
			SortHeap<int, CompareInt>(span);
			for (int i = 1; i < span.Length; i++)
			{
				Assert.IsTrue(span[i - 1] <= span[i]);
			}
		}

		[TestMethod] public void OddEvenSpan_Test()
		{
			Span<int> span = new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
			SortOddEven<int, CompareInt>(span);
			for (int i = 1; i < span.Length; i++)
			{
				Assert.IsTrue(span[i - 1] <= span[i]);
			}
		}

		[TestMethod] public void SlowSpan_Test()
		{
			Span<int> span = new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
			SortSlow<int, CompareInt>(span);
			for (int i = 1; i < span.Length; i++)
			{
				Assert.IsTrue(span[i - 1] <= span[i]);
			}
		}

		[TestMethod] public void GnomeSpan_Test()
		{
			Span<int> span = new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
			SortGnome<int, CompareInt>(span);
			for (int i = 1; i < span.Length; i++)
			{
				Assert.IsTrue(span[i - 1] <= span[i]);
			}
		}

		[TestMethod] public void CombSpan_Test()
		{
			Span<int> span = new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
			SortComb<int, CompareInt>(span);
			for (int i = 1; i < span.Length; i++)
			{
				Assert.IsTrue(span[i - 1] <= span[i]);
			}
		}

		[TestMethod] public void ShellSpan_Test()
		{
			Span<int> span = new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
			SortShell<int, CompareInt>(span);
			for (int i = 1; i < span.Length; i++)
			{
				Assert.IsTrue(span[i - 1] <= span[i]);
			}
		}

		[TestMethod] public void CocktailSpan_Test()
		{
			Span<int> span = new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
			SortCocktail<int, CompareInt>(span);
			for (int i = 1; i < span.Length; i++)
			{
				Assert.IsTrue(span[i - 1] <= span[i]);
			}
		}

		[TestMethod] public void BogoSpan_Test()
		{
			Span<int> span = new[] { 5, 4, 3, 2, 1, 0 };
			SortBogo<int, CompareInt>(span);
			for (int i = 1; i < span.Length; i++)
			{
				Assert.IsTrue(span[i - 1] <= span[i]);
			}
		}
	}
}
