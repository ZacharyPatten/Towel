using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Towel;
using static Towel.Statics;

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
				Assert.IsTrue(EquateSequence<int>(array, (size..0).ToSpan()));
			}
			// partial reverse size 3
			{
				int[] array = (1..4).ToArray();
				Reverse(0, 1, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(EquateSequence<int>(array, stackalloc int[] { 2, 1, 3 }));
			}
			{
				int[] array = (1..4).ToArray();
				Reverse(1, 2, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(EquateSequence<int>(array, stackalloc int[] { 1, 3, 2 }));
			}
			// partial reverse size 4
			{
				int[] array = (1..5).ToArray();
				Reverse(0, 1, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(EquateSequence<int>(array, stackalloc int[] { 2, 1, 3, 4 }));
			}
			{
				int[] array = (1..5).ToArray();
				Reverse(1, 2, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(EquateSequence<int>(array, stackalloc int[] { 1, 3, 2, 4 }));
			}
			{
				int[] array = (1..5).ToArray();
				Reverse(2, 3, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(EquateSequence<int>(array, stackalloc int[] { 1, 2, 4, 3 }));
			}
			{
				int[] array = (1..5).ToArray();
				Reverse(0, 2, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(EquateSequence<int>(array, stackalloc int[] { 3, 2, 1, 4 }));
			}
			{
				int[] array = (1..5).ToArray();
				Reverse(1, 3, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(EquateSequence<int>(array, stackalloc int[] { 1, 4, 3, 2 }));
			}

			// start > end
			for (int size = 1; size < 11; size++)
			{
				int[] array = (1..(size + 1)).ToArray();
				Reverse(array.Length - 1, 0, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(EquateSequence<int>(array, (size..0).ToSpan()));
			}

			// partial reverse size 3
			{
				int[] array = (1..4).ToArray();
				Reverse(1, 0, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(EquateSequence<int>(array, stackalloc int[] { 2, 1, 3 }));
			}
			{
				int[] array = (1..4).ToArray();
				Reverse(2, 1, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(EquateSequence<int>(array, stackalloc int[] { 1, 3, 2 }));
			}
			// partial reverse size 4
			{
				int[] array = (1..5).ToArray();
				Reverse(1, 0, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(EquateSequence<int>(array, stackalloc int[] { 2, 1, 3, 4 }));
			}
			{
				int[] array = (1..5).ToArray();
				Reverse(2, 1, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(EquateSequence<int>(array, stackalloc int[] { 1, 3, 2, 4 }));
			}
			{
				int[] array = (1..5).ToArray();
				Reverse(3, 2, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(EquateSequence<int>(array, stackalloc int[] { 1, 2, 4, 3 }));
			}
			{
				int[] array = (1..5).ToArray();
				Reverse(2, 0, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(EquateSequence<int>(array, stackalloc int[] { 3, 2, 1, 4 }));
			}
			{
				int[] array = (1..5).ToArray();
				Reverse(3, 1, i => array[i], (i, v) => array[i] = v);
				Assert.IsTrue(EquateSequence<int>(array, stackalloc int[] { 1, 4, 3, 2 }));
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

		internal static void TestSortAlgorithm(
			Action<int, int, int[]> algorithm,
			int? maxSize = null,
			bool negatives = true)
		{
			// Test sorting the full array (0...length - 1)
			foreach (int size in (0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 100, 101, 1000, 1001))
			{
				if (maxSize is null || maxSize.Value >= size)
				{
					Random random = new(7);
					int[] array = (size..0).ToArray();
					if (size > 1)
					{
						Shuffle<int>(array, random);
						if (IsOrdered<int>(array))
						{
							Array.Reverse(array);
						}
					}
					algorithm(0, array.Length - 1, array);
					Assert.IsTrue(IsOrdered<int>(array));
					Assert.IsTrue(EquateSequence<int>(array, (1..(size + 1)).ToSpan()));
				}
			}

			// Test sorting the full array (0...length - 1) with negative values
			if (negatives)
			{
				foreach (int size in (0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 100, 101, 1000, 1001))
				{
					if (maxSize is null || maxSize.Value >= size)
					{
						Random random = new(7);
						int[] array = (size..0).ToArray(i => i - size);
						if (size > 1)
						{
							Shuffle<int>(array, random);
							if (IsOrdered<int>(array))
							{
								Array.Reverse(array);
							}
						}
						algorithm(0, array.Length - 1, array);
						Assert.IsTrue(IsOrdered<int>(array));
						var tmp = (0..size).ToSpan(i => i - size + 1);
						Assert.IsTrue(EquateSequence<int>(array, (0..size).ToSpan(i => i - size + 1)));
					}
				}
			}

			// Test sorting the full array (0...length - 1) with randomized values
			foreach (int size in (0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 100, 101, 1000, 1001))
			{
				if (maxSize is null || maxSize.Value >= size)
				{
					Random random = new(7);
					int[] array = (size..0).ToArray(i => random.Next(1, 100000));
					if (size > 1)
					{
						Shuffle<int>(array, random);
						if (IsOrdered<int>(array))
						{
							Array.Reverse(array);
						}
					}
					algorithm(0, array.Length - 1, array);
					Assert.IsTrue(IsOrdered<int>(array));
				}
			}

			// Test sorting the full array (0...length - 1) with randomized negative values
			if (negatives)
			{
				foreach (int size in (0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 100, 101, 1000, 1001))
				{
					if (maxSize is null || maxSize.Value >= size)
					{
						Random random = new(7);
						int[] array = (size..0).ToArray(i => random.Next(1, 100000) - 100000);
						if (size > 1)
						{
							Shuffle<int>(array, random);
							if (IsOrdered<int>(array))
							{
								Array.Reverse(array);
							}
						}
						algorithm(0, array.Length - 1, array);
						Assert.IsTrue(IsOrdered<int>(array));
					}
				}
			}

			// Test sorting the full array (0...length - 1) with randomized negative and positive values
			if (negatives)
			{
				foreach (int size in (0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 100, 101, 1000, 1001))
				{
					if (maxSize is null || maxSize.Value >= size)
					{
						Random random = new(7);
						int[] array = (size..0).ToArray(i => random.Next(1, 100000) - 50000);
						if (size > 1)
						{
							Shuffle<int>(array, random);
							if (IsOrdered<int>(array))
							{
								Array.Reverse(array);
							}
						}
						algorithm(0, array.Length - 1, array);
						Assert.IsTrue(IsOrdered<int>(array));
					}
				}
			}

			// Test partial sorting of the array (start > 0 && end < length - 1)
			if (maxSize is null || maxSize.Value >= 5)
			{
				int[] array = { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
				algorithm(3, 7, array);
				Span<int> expected = stackalloc int[] { 9, 8, 7, /*<*/ 2, 3, 4, 5, 6, /*>*/ 1, 0 };
				Assert.IsTrue(EquateSequence<int>(array, expected));
			}
			if (maxSize is null || maxSize.Value >= 5)
			{
				int[] array = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
				algorithm(4, 8, array);
				Span<int> expected = stackalloc int[] { 10, 9, 8, 7, /*<*/ 2, 3, 4, 5, 6, /*>*/ 1, 0 };
				Assert.IsTrue(EquateSequence<int>(array, expected));
			}
			if (maxSize is null || maxSize.Value >= 980)
			{
				Random random = new(7);
				int[] array = (1000..0).ToArray();
				Shuffle<int>(array.AsSpan(10..990), random);
				algorithm(10, 990, array);
				Span<int> expected = ArrayHelper.NewFromRanges(1000..990, 10..991, 9..0);
				Assert.IsTrue(EquateSequence<int>(array, expected));
			}
		}

		[TestMethod]
		public void Bubble_Testing() => TestSortAlgorithm((start, end, array) => SortBubble(start, end, i => array[i], (i, v) => array[i] = v));

		[TestMethod]
		public void Bubble_Span_Testing() => TestSortAlgorithm((start, end, array) => SortBubble(array.AsSpan()[start..(end + 1)]));

		[TestMethod]
		public void Insertion_Testing() => TestSortAlgorithm((start, end, array) => SortInsertion(start, end, i => array[i], (i, v) => array[i] = v));

		[TestMethod]
		public void Insertion_Span_Testing() => TestSortAlgorithm((start, end, array) => SortInsertion(array.AsSpan()[start..(end + 1)]));

		[TestMethod]
		public void Selection_Testing() => TestSortAlgorithm((start, end, array) => SortSelection(start, end, i => array[i], (i, v) => array[i] = v));

		[TestMethod]
		public void Selection_Span_Testing() => TestSortAlgorithm((start, end, array) => SortSelection(array.AsSpan()[start..(end + 1)]));

		[TestMethod]
		public void Merge_Testing() => TestSortAlgorithm((start, end, array) => SortMerge(start, end, i => array[i], (i, v) => array[i] = v));

		[TestMethod]
		public void Merge_Span_Testing() => TestSortAlgorithm((start, end, array) => SortMerge(array.AsSpan()[start..(end + 1)]));

		[TestMethod]
		public void Quick_Testing() => TestSortAlgorithm((start, end, array) => SortQuick(start, end, i => array[i], (i, v) => array[i] = v));

		[TestMethod]
		public void Quick_Span_Testing() => TestSortAlgorithm((start, end, array) => SortQuick(array.AsSpan()[start..(end + 1)]));

		[TestMethod]
		public void Heap_Testing() => TestSortAlgorithm((start, end, array) => SortHeap(start, end, i => array[i], (i, v) => array[i] = v));

		[TestMethod]
		public void Heap_Span_Testing() => TestSortAlgorithm((start, end, array) => SortHeap(array.AsSpan()[start..(end + 1)]));

		[TestMethod]
		public void OddEven_Testing() => TestSortAlgorithm((start, end, array) => SortOddEven(start, end, i => array[i], (i, v) => array[i] = v));

		[TestMethod]
		public void OddEven_Span_Testing() => TestSortAlgorithm((start, end, array) => SortOddEven(array.AsSpan()[start..(end + 1)]));

		[TestMethod]
		public void Slow_Testing() => TestSortAlgorithm((start, end, array) => SortSlow(start, end, i => array[i], (i, v) => array[i] = v), 11);

		[TestMethod]
		public void Slow_Span_Testing() => TestSortAlgorithm((start, end, array) => SortSlow(array.AsSpan()[start..(end + 1)]), 11);

		[TestMethod]
		public void Gnome_Testing() => TestSortAlgorithm((start, end, array) => SortGnome(start, end, i => array[i], (i, v) => array[i] = v));

		[TestMethod]
		public void Gnome_Span_Testing() => TestSortAlgorithm((start, end, array) => SortGnome(array.AsSpan()[start..(end + 1)]));

		[TestMethod]
		public void Comb_Testing() => TestSortAlgorithm((start, end, array) => SortComb(start, end, i => array[i], (i, v) => array[i] = v));

		[TestMethod]
		public void Comb_Span_Testing() => TestSortAlgorithm((start, end, array) => SortComb(array.AsSpan()[start..(end + 1)]));

		[TestMethod]
		public void Shell_Testing() => TestSortAlgorithm((start, end, array) => SortShell(start, end, i => array[i], (i, v) => array[i] = v));

		[TestMethod]
		public void Shell_Span_Testing() => TestSortAlgorithm((start, end, array) => SortShell(array.AsSpan()[start..(end + 1)]));

		[TestMethod]
		public void Cocktail_Testing() => TestSortAlgorithm((start, end, array) => SortCocktail(start, end, i => array[i], (i, v) => array[i] = v));

		[TestMethod]
		public void Cocktail_Span_Testing() => TestSortAlgorithm((start, end, array) => SortCocktail(array.AsSpan()[start..(end + 1)]));

		[TestMethod]
		public void Cycle_Testing() => TestSortAlgorithm((start, end, array) => SortCycle(start, end, i => array[i], (i, v) => array[i] = v));

		[TestMethod]
		public void Cycle_Span_Testing() => TestSortAlgorithm((start, end, array) => SortCycle(array.AsSpan()[start..(end + 1)]));

		[TestMethod]
		public void Pancake_Testing() => TestSortAlgorithm((start, end, array) => SortPancake(start, end, i => array[i], (i, v) => array[i] = v));

		[TestMethod]
		public void Pancake_Span_Testing() => TestSortAlgorithm((start, end, array) => SortPancake(array.AsSpan()[start..(end + 1)]));

		[TestMethod]
		public void Stooge_Testing() => TestSortAlgorithm((start, end, array) => SortStooge(start, end, i => array[i], (i, v) => array[i] = v), maxSize: 100);

		[TestMethod]
		public void Stooge_Span_Testing() => TestSortAlgorithm((start, end, array) => SortStooge(array.AsSpan()[start..(end + 1)]), maxSize: 100);

		[TestMethod]
		public void Bogo_Testing() => TestSortAlgorithm((start, end, array) => SortBogo(start, end, i => array[i], (i, v) => array[i] = v), maxSize: 6);

		[TestMethod]
		public void Bogo_Span_Testing() => TestSortAlgorithm((start, end, array) => SortBogo(array.AsSpan()[start..(end + 1)]), maxSize: 6);

		[TestMethod]
		public void Tim_Testing() => TestSortAlgorithm((start, end, array) => SortTim(start, end, i => array[i], (i, v) => array[i] = v));

		[TestMethod]
		public void Tim_Span_Testing() => TestSortAlgorithm((start, end, array) => SortTim(array.AsSpan()[start..(end + 1)]));

		[TestMethod]
		public void Counting_Testing() => TestSortAlgorithm((start, end, array) => SortCounting(start, end, i => (uint)i, i => array[i], (i, v) => array[i] = v), negatives: false);

		[TestMethod]
		public void Counting_Span_Testing()
		{
			TestSortAlgorithm((start, end, array) =>
			{
				Span<uint> unitSpan = array[start..(end + 1)].Select(i => (uint)i).ToArray();
				SortCounting(unitSpan);
				Span<int> intSpan = unitSpan.ToArray().Select(i => (int)i).ToArray();
				intSpan.CopyTo(array.AsSpan(start, end - start + 1));
			}, negatives: false);
		}

		[TestMethod]
		public void Radix_Testing() => TestSortAlgorithm((start, end, array) => SortRadix(start, end, i => (uint)i, i => array[i], (i, v) => array[i] = v), negatives: false);

		[TestMethod]
		public void Radix_Span_Testing()
		{
			TestSortAlgorithm((start, end, array) =>
			{
				Span<uint> unitSpan = array[start..(end + 1)].Select(i => (uint)i).ToArray();
				SortRadix(unitSpan);
				Span<int> intSpan = unitSpan.ToArray().Select(i => (int)i).ToArray();
				intSpan.CopyTo(array.AsSpan(start, end - start + 1));
			}, negatives: false);
		}

		[TestMethod]
		public void PidgeonHole_Testing() => TestSortAlgorithm((start, end, array) => SortPidgeonHole(start, end, i => array[i], (i, v) => array[i] = v));

		[TestMethod]
		public void PidgeonHole_Span_Testing() => TestSortAlgorithm((start, end, array) => SortPidgeonHole(array.AsSpan()[start..(end + 1)]));

		#endregion
	}
}