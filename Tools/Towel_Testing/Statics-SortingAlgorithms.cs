using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Towel;
using static Towel.Statics;
using System.Linq;

namespace Towel_Testing
{
	// [TestClass]
	public partial class Statics_Testing
	{
		#region Reverse

		[TestMethod]
		public void Reverse_Testing()
		{
			// start < end
			for (int size = 1; size < 11; size++)
			{
				int[] array = (1..(size + 1)).ToArray();
				Reverse(0, array.Length - 1, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(Equate<int>(array, (size..0).ToArray()));
			}
			// partial reverse size 3
			{
				int[] array = (1..4).ToArray();
				Reverse(0, 1, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(Equate<int>(array, new[] { 2, 1, 3 }));
			}
			{
				int[] array = (1..4).ToArray();
				Reverse(1, 2, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(Equate<int>(array, new[] { 1, 3, 2 }));
			}
			// partial reverse size 4
			{
				int[] array = (1..5).ToArray();
				Reverse(0, 1, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(Equate<int>(array, new[] { 2, 1, 3, 4 }));
			}
			{
				int[] array = (1..5).ToArray();
				Reverse(1, 2, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(Equate<int>(array, new[] { 1, 3, 2, 4 }));
			}
			{
				int[] array = (1..5).ToArray();
				Reverse(2, 3, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(Equate<int>(array, new[] { 1, 2, 4, 3 }));
			}
			{
				int[] array = (1..5).ToArray();
				Reverse(0, 2, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(Equate<int>(array, new[] { 3, 2, 1, 4 }));
			}
			{
				int[] array = (1..5).ToArray();
				Reverse(1, 3, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(Equate<int>(array, new[] { 1, 4, 3, 2 }));
			}

			// start > end
			for (int size = 1; size < 11; size++)
			{
				int[] array = (1..(size + 1)).ToArray();
				Reverse(array.Length - 1, 0, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(Equate<int>(array, (size..0).ToArray()));
			}

			// partial reverse size 3
			{
				int[] array = (1..4).ToArray();
				Reverse(1, 0, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(Equate<int>(array, new[] { 2, 1, 3 }));
			}
			{
				int[] array = (1..4).ToArray();
				Reverse(2, 1, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(Equate<int>(array, new[] { 1, 3, 2 }));
			}
			// partial reverse size 4
			{
				int[] array = (1..5).ToArray();
				Reverse(1, 0, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(Equate<int>(array, new[] { 2, 1, 3, 4 }));
			}
			{
				int[] array = (1..5).ToArray();
				Reverse(2, 1, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(Equate<int>(array, new[] { 1, 3, 2, 4 }));
			}
			{
				int[] array = (1..5).ToArray();
				Reverse(3, 2, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(Equate<int>(array, new[] { 1, 2, 4, 3 }));
			}
			{
				int[] array = (1..5).ToArray();
				Reverse(2, 0, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(Equate<int>(array, new[] { 3, 2, 1, 4 }));
			}
			{
				int[] array = (1..5).ToArray();
				Reverse(3, 1, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(Equate<int>(array, new[] { 1, 4, 3, 2 }));
			}
		}

		#endregion

		#region Shuffle

		[TestMethod]
		public void Shuffle_Testing()
		{
			Random random = new(7);
			int[] array = (..10).ToArray();
			Shuffle<int>(array, random);
			Assert.IsFalse(IsOrdered<int>(array));
		}

		#endregion

		#region Sorting Algorithms

		#region TGet + TSet

		internal static void TestSortAlgorithm(
			Action<int, int, Func<int, int>, Action<int, int>, Func<int, int, CompareResult>> algorithm,
			int? maxSize = null)
		{
			// Test sorting the full array (0...length - 1)
			foreach (int size in (0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 100, 101, 1000, 1001))
			{
				if (maxSize is null || maxSize.Value >= size)
				{
					Random random = new(7);
					int[] array = (size..0).ToArray();
					Shuffle<int>(array, random);
					if (IsOrdered<int>(array))
					{
						array = (size..0).ToArray();
					}
					algorithm(0, array.Length - 1, i => array[i], (i, v) => array[i] = v, Compare);
					Assert.IsTrue(IsOrdered<int>(array));
					Assert.IsTrue(Equate<int>(array, (1..(size + 1)).ToArray()));
				}
			}

			// Test partial sorting of the array (start > 0 && end < length - 1)
			if (maxSize is null || maxSize.Value >= 5)
			{
				int[] array = { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
				algorithm(3, 7, i => array[i], (i, v) => array[i] = v, Compare);
				int[] expected = { 9, 8, 7, /**/ 2, 3, 4, 5, 6, /**/ 1, 0 };
				Assert.IsTrue(Equate<int>(array, expected));
			}
			if (maxSize is null || maxSize.Value >= 5)
			{
				int[] array = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
				algorithm(4, 8, i => array[i], (i, v) => array[i] = v, Compare);
				int[] expected = { 10, 9, 8, 7, /**/ 2, 3, 4, 5, 6, /**/ 1, 0 };
				Assert.IsTrue(Equate<int>(array, expected));
			}
			if (maxSize is null || maxSize.Value >= 980)
			{
				Random random = new(7);
				int[] array = (1000..0).ToArray();
				Shuffle<int>(array.AsSpan(10..990), random);
				algorithm(10, 990, i => array[i], (i, v) => array[i] = v, Compare);
				int[] expected = (1000..990).ToArray().Concat((10..991).ToArray()).Concat((9..0).ToArray()).ToArray();
				Assert.IsTrue(Equate<int>(array, expected));
			}
		}

		[TestMethod]
		public void Bubble_Testing() => TestSortAlgorithm(SortBubble);

		[TestMethod]
		public void Insertion_Testing() => TestSortAlgorithm(SortInsertion);

		[TestMethod]
		public void Selection_Testing() => TestSortAlgorithm(SortSelection);

		[TestMethod]
		public void Merge_Testing() => TestSortAlgorithm(SortMerge);

		[TestMethod]
		public void Quick_Testing() => TestSortAlgorithm(SortQuick);

		[TestMethod]
		public void Heap_Testing() => TestSortAlgorithm(SortHeap);

		[TestMethod]
		public void OddEven_Testing() => TestSortAlgorithm(SortOddEven);

		[TestMethod]
		public void Slow_Testing() => TestSortAlgorithm(SortSlow, 10);

		[TestMethod]
		public void Gnome_Testing() => TestSortAlgorithm(SortGnome);

		[TestMethod]
		public void Comb_Testing() => TestSortAlgorithm(SortComb);

		[TestMethod]
		public void Shell_Testing() => TestSortAlgorithm(SortShell);

		[TestMethod]
		public void Cocktail_Testing() => TestSortAlgorithm(SortCocktail);

		[TestMethod]
		public void Cycle_Testing() => TestSortAlgorithm(SortCycle);

		[TestMethod]
		public void Pancake_Testing() => TestSortAlgorithm(SortPancake);

		[TestMethod]
		public void Stooge_Testing() => TestSortAlgorithm(SortStooge, 100);

		[TestMethod]
		public void Bogo_Testing() => TestSortAlgorithm((start, end, get, set, compare) => SortBogo(start, end, get, set, compare), 6);

		[TestMethod]
		public void Tim_Testing() => TestSortAlgorithm(SortTim);

		#endregion

		#region Span<T>

		public delegate void SortSpan<T>(Span<T> span);

		public static void TestSortAlgorithmSpan(
			SortSpan<int> algorithm,
			int? sizeOverride = null)
		{
			if (sizeOverride is null || sizeOverride.Value > 10)
			{
				Span<int> span = new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
				algorithm(span);
				Assert.IsTrue(IsOrdered<int>(span));
			}
			if (sizeOverride is null || sizeOverride.Value > 9)
			{
				Span<int> span = new[] { 8, 7, 6, 5, 4, 3, 2, 1, 0 };
				algorithm(span);
				Assert.IsTrue(IsOrdered<int>(span));
			}
			if (sizeOverride is null || sizeOverride.Value > 1000)
			{
				Random random = new(7);
				var array = (..1000).ToArray();
				Shuffle<int>(array, random);
				algorithm(array);
				Assert.IsTrue(IsOrdered<int>(array));
			}
		}

		[TestMethod]
		public void BubbleSpan_Test() => TestSortAlgorithmSpan(x => SortBubble<int, Int32Compare>(x));

		[TestMethod]
		public void InsertionSpan_Test() => TestSortAlgorithmSpan(x => SortInsertion<int, Int32Compare>(x));

		[TestMethod]
		public void SelectionSpan_Test() => TestSortAlgorithmSpan(x => SortSelection<int, Int32Compare>(x));

		[TestMethod]
		public void MergeSpan_Test() => TestSortAlgorithmSpan(x => SortMerge<int, Int32Compare>(x));

		[TestMethod]
		public void QuickSpan_Test() => TestSortAlgorithmSpan(x => SortQuick<int, Int32Compare>(x));

		[TestMethod]
		public void HeapSpan_Test() => TestSortAlgorithmSpan(x => SortHeap<int, Int32Compare>(x));

		[TestMethod]
		public void OddEvenSpan_Test() => TestSortAlgorithmSpan(x => SortOddEven<int, Int32Compare>(x));

		[TestMethod]
		public void SlowSpan_Test() => TestSortAlgorithmSpan(x => SortSlow<int, Int32Compare>(x), 11);

		[TestMethod]
		public void GnomeSpan_Test() => TestSortAlgorithmSpan(x => SortGnome<int, Int32Compare>(x));

		[TestMethod]
		public void CombSpan_Test() => TestSortAlgorithmSpan(x => SortComb<int, Int32Compare>(x));

		[TestMethod]
		public void ShellSpan_Test() => TestSortAlgorithmSpan(x => SortShell<int, Int32Compare>(x));

		[TestMethod]
		public void CocktailSpan_Test() => TestSortAlgorithmSpan(x => SortCocktail<int, Int32Compare>(x));

		[TestMethod]
		public void CycleSpan_Test() => TestSortAlgorithmSpan(x => SortCycle<int, Int32Compare>(x));

		[TestMethod]
		public void BogoSpan_Test() => TestSortAlgorithmSpan(x => SortBogo<int, Int32Compare>(x), 11);

		[TestMethod]
		public void TimSpan_Test() => TestSortAlgorithmSpan(x => SortTim<int, Int32Compare>(x));

		[TestMethod]
		public void PancakeSpan_Test() => TestSortAlgorithmSpan(x => SortPancake<int, Int32Compare>(x));

		[TestMethod]
		public void StoogeSpan_Test() => TestSortAlgorithmSpan(x => SortStooge<int, Int32Compare>(x));

		#endregion

		#endregion
	}
}