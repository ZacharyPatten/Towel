using System;
using static Towel.Syntax;

namespace Towel.Algorithms
{
	/// <summary>Contains static sorting algorithms.</summary>
	public static class Sort
	{
		#region Bubble

		/// <summary>Sorts an entire array in non-decreasing order using the bubble sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="array">The array to be sorted.</param>
		/// <runtime>Ω(n), ε(n^2), O(n^2)</runtime>
		/// <stability>True</stability>
		/// <memory>O(1)</memory>
		public static void Bubble<T>(T[] array) =>
			Bubble(array, Compare.Default);

		/// <summary>Sorts an entire array in non-decreasing order using the bubble sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="compare">The compare function.</param>
		/// <param name="array">The array to be sorted.</param>
		/// <runtime>Ω(n), ε(n^2), O(n^2)</runtime>
		/// <stability>True</stability>
		/// <memory>O(1)</memory>
		public static void Bubble<T>(T[] array, Compare<T> compare) =>
			Bubble(array, 0, array.Length - 1, compare);

		/// <summary>Sorts an entire array in non-decreasing order using the bubble sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <typeparam name="Compare">The compare function.</typeparam>
		/// <param name="array">The array to be sorted.</param>
		/// <param name="compare">The compare function.</param>
		/// <runtime>Ω(n), ε(n^2), O(n^2)</runtime>
		/// <stability>True</stability>
		/// <memory>O(1)</memory>
		public static void Bubble<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Bubble(array, 0, array.Length - 1, compare);

		/// <summary>Sorts an entire array in non-decreasing order using the bubble sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="compare">The compare function.</param>
		/// <param name="array">The array to be sorted.</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <runtime>Ω(n), ε(n^2), O(n^2)</runtime>
		/// <stability>True</stability>
		/// <memory>O(1)</memory>
		public static void Bubble<T>(T[] array, int start, int end, Compare<T> compare) =>
			Bubble<T, CompareRuntime<T>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <summary>Sorts an entire array in non-decreasing order using the bubble sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <typeparam name="Compare">The compare function.</typeparam>
		/// <param name="array">The array to be sorted.</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <param name="compare">The compare function.</param>
		public static void Bubble<T, Compare>(T[] array, int start, int end, Compare compare)
			where Compare : struct, ICompare<T> =>
			Bubble<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <summary>Sorts an entire array in non-decreasing order using the bubble sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="compare">The compare function.</param>
		/// <param name="get">Delegate for getting a value at a specified index.</param>
		/// <param name="set">Delegate for setting a value at a specified index.</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <runtime>Ω(n), ε(n^2), O(n^2)</runtime>
		/// <stability>True</stability>
		/// <memory>O(1)</memory>
		public static void Bubble<T>(int start, int end, Compare<T> compare, GetIndex<T> get, SetIndex<T> set) =>
			Bubble<T, CompareRuntime<T>, GetIndexRuntime<T>, SetIndexRuntime<T>>(start, end, compare, get, set);

		/// <summary>Sorts an entire array in non-decreasing order using the bubble sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <typeparam name="Compare">The compare function.</typeparam>
		/// <typeparam name="Get">The get function.</typeparam>
		/// <typeparam name="Set">The set function.</typeparam>
		/// <param name="compare">The compare function.</param>
		/// <param name="get">Delegate for getting a value at a specified index.</param>
		/// <param name="set">Delegate for setting a value at a specified index.</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <runtime>Ω(n), ε(n^2), O(n^2)</runtime>
		/// <stability>True</stability>
		/// <memory>O(1)</memory>
		public static void Bubble<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, ICompare<T>
			where Get     : struct, IGetIndex<T>
			where Set     : struct, ISetIndex<T>
		{
			for (int i = start; i <= end; i++)
			{
				for (int j = start; j <= end - 1; j++)
				{
					if (compare.Do(get.Do(j), get.Do(j + 1)) == Greater)
					{
						T temp = get.Do(j + 1);
						set.Do(j + 1, get.Do(j));
						set.Do(j, temp);
					}
				}
			}
		}

		#endregion

		#region Selection

		/// <summary>Sorts an entire array in non-decreasing order using the selection sort algoritm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="array">the array to be sorted</param>
		/// <runtime>Ω(n^2), ε(n^2), O(n^2)</runtime>
		/// <stability>False</stability>
		/// <memory>O(1)</memory>
		public static void Selection<T>(T[] array) =>
			Selection(Compare.Default, array);

		/// <summary>Sorts an entire array in non-decreasing order using the selection sort algoritm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="compare">Returns negative if the left is less than the right.</param>
		/// <param name="array">the array to be sorted</param>
		/// <runtime>Ω(n^2), ε(n^2), O(n^2)</runtime>
		/// <stability>False</stability>
		/// <memory>O(1)</memory>
		public static void Selection<T>(Compare<T> compare, T[] array) =>
			Selection(compare, array, 0, array.Length - 1);

		/// <summary>Sorts an entire array in non-decreasing order using the selection sort algoritm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="compare">Returns negative if the left is less than the right.</param>
		/// <param name="array">the array to be sorted</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <runtime>Ω(n^2), ε(n^2), O(n^2)</runtime>
		/// <stability>False</stability>
		/// <memory>O(1)</memory>
		public static void Selection<T>(Compare<T> compare, T[] array, int start, int end) =>
			Selection(compare, array.WrapGetIndex(), array.WrapSetIndex(), start, end);

		/// <summary>Sorts an entire array in non-decreasing order using the selection sort algoritm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="compare">Returns negative if the left is less than the right.</param>
		/// <param name="get">Delegate for getting a value at a specified index.</param>
		/// <param name="set">Delegate for setting a value at a specified index.</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <runtime>Ω(n^2), ε(n^2), O(n^2)</runtime>
		/// <stability>False</stability>
		/// <memory>O(1)</memory>
		public static void Selection<T>(Compare<T> compare, GetIndex<T> get, SetIndex<T> set, int start, int end)
		{
			for (int i = start; i <= end; i++)
			{
				int min_idx = i;
				for (int j = i + 1; j <= end; j++)
				{
					if (compare(get(j), get(min_idx)) == Less)
					{
						min_idx = j;
					}
				}
				T temp = get(min_idx);
				set(min_idx, get(i));
				set(i, temp);
			}
		}

		#endregion

		#region Insertion

		/// <summary>Sorts an entire array in non-decreasing order using the insertion sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="array">the array to be sorted</param>
		/// <runtime>Ω(n), ε(n^2), O(n^2)</runtime>
		/// <stability>True</stability>
		/// <memory>O(1)</memory>
		public static void Insertion<T>(T[] array) =>
			Insertion(Compare.Default, array);

		/// <summary>Sorts an entire array in non-decreasing order using the insertion sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="compare">Returns positive if left greater than right.</param>
		/// <param name="array">the array to be sorted</param>
		/// <runtime>Ω(n), ε(n^2), O(n^2)</runtime>
		/// <stability>True</stability>
		/// <memory>O(1)</memory>
		public static void Insertion<T>(Compare<T> compare, T[] array) =>
			Insertion(compare, array, 0, array.Length - 1);

		/// <summary>Sorts an entire array in non-decreasing order using the insertion sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="compare">Returns positive if left greater than right.</param>
		/// <param name="array">the array to be sorted</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <runtime>Ω(n), ε(n^2), O(n^2)</runtime>
		/// <stability>True</stability>
		/// <memory>O(1)</memory>
		public static void Insertion<T>(Compare<T> compare, T[] array, int start, int end) =>
			Insertion(compare, array.WrapGetIndex(), array.WrapSetIndex(), start, end);

		/// <summary>Sorts an entire array in non-decreasing order using the insertion sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="compare">Returns positive if left greater than right.</param>
		/// <param name="get">Delegate for getting a value at a specified index.</param>
		/// <param name="set">Delegate for setting a value at a specified index.</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <runtime>Ω(n), ε(n^2), O(n^2)</runtime>
		/// <stability>True</stability>
		/// <memory>O(1)</memory>
		public static void Insertion<T>(Compare<T> compare, GetIndex<T> get, SetIndex<T> set, int start, int end)
		{
			for (int i = start + 1; i <= end; i++)
			{
				T temp = get(i);
				int j = i;
				for (; j > start && compare(get(j - 1), temp) == Greater; j--)
				{
					set(j, get(j - 1));
				}
				set(j, temp);
			}
		}

		#endregion

		#region Quick

		/// <summary>Sorts an entire array in non-decreasing order using the quick sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="array">The array to be sorted</param>
		/// <runtime>Ω(n*ln(n)), ε(n*ln(n)), O(n^2)</runtime>
		/// <stability>False</stability>
		/// <memory>ln(n)</memory>
		public static void Quick<T>(T[] array) =>
			Quick(array, Compare.Default);

		/// <summary>Sorts an entire array in non-decreasing order using the quick sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="compare">The method of compare to be sorted by.</param>
		/// <param name="array">The array to be sorted</param>
		/// <runtime>Ω(n*ln(n)), ε(n*ln(n)), O(n^2)</runtime>
		/// <stability>False</stability>
		/// <memory>ln(n)</memory>
		public static void Quick<T>(T[] array, Compare<T> compare) =>
			Quick(array, 0, array.Length - 1, compare);

		/// <summary>Sorts an entire array in non-decreasing order using the quick sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <typeparam name="Compare">The method of compare to be sorted by.</typeparam>
		/// <param name="compare">The method of compare to be sorted by.</param>
		/// <param name="array">The array to be sorted</param>
		/// <runtime>Ω(n*ln(n)), ε(n*ln(n)), O(n^2)</runtime>
		/// <stability>False</stability>
		/// <memory>ln(n)</memory>
		public static void Quick<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Quick(array, 0, array.Length - 1, compare);

		/// <summary>Sorts an entire array in non-decreasing order using the quick sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="compare">The method of compare to be sorted by.</param>
		/// <param name="array">The array to be sorted</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <runtime>Ω(n*ln(n)), ε(n*ln(n)), O(n^2)</runtime>
		/// <stability>False</stability>
		/// <memory>ln(n)</memory>
		public static void Quick<T>(T[] array, int start, int end, Compare<T> compare) =>
			Quick<T, CompareRuntime<T>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <summary>Sorts an entire array in non-decreasing order using the quick sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <typeparam name="Compare">The method of compare to be sorted by.</typeparam>
		/// <param name="compare">The method of compare to be sorted by.</param>
		/// <param name="array">The array to be sorted</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <runtime>Ω(n*ln(n)), ε(n*ln(n)), O(n^2)</runtime>
		/// <stability>False</stability>
		/// <memory>ln(n)</memory>
		public static void Quick<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Quick<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <summary>Sorts an entire array in non-decreasing order using the quick sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="compare">The method of compare to be sorted by.</param>
		/// <param name="get">Delegate for getting a value at a specified index.</param>
		/// <param name="set">Delegate for setting a value at a specified index.</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <runtime>Ω(n*ln(n)), ε(n*ln(n)), O(n^2)</runtime>
		/// <stability>False</stability>
		/// <memory>ln(n)</memory>
		public static void Quick<T>(int start, int end, Compare<T> compare, GetIndex<T> get, SetIndex<T> set) =>
			Quick<T, CompareRuntime<T>, GetIndexRuntime<T>, SetIndexRuntime<T>>(start, end, compare, get, set);

		/// <summary>Sorts an entire array in non-decreasing order using the quick sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <typeparam name="Compare">Delegate for getting a value at a specified index.</typeparam>
		/// <typeparam name="Get">Delegate for getting a value at a specified index.</typeparam>
		/// <typeparam name="Set">Delegate for setting a value at a specified index.</typeparam>
		/// <param name="compare">The method of compare to be sorted by.</param>
		/// <param name="get">Delegate for getting a value at a specified index.</param>
		/// <param name="set">Delegate for setting a value at a specified index.</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <runtime>Ω(n*ln(n)), ε(n*ln(n)), O(n^2)</runtime>
		/// <stability>False</stability>
		/// <memory>ln(n)</memory>
		public static void Quick<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, ICompare<T>
			where Get : struct, IGetIndex<T>
			where Set : struct, ISetIndex<T> =>
			Quick_Recursive<T, Compare, Get, Set>(start, end - start + 1, compare, get, set);

		internal static void Quick_Recursive<T, Compare, Get, Set>(int start, int len, Compare compare, Get get, Set set)
			where Compare : struct, ICompare<T>
			where Get : struct, IGetIndex<T>
			where Set : struct, ISetIndex<T>
		{
			if (len > 1)
			{
				T pivot = get.Do(start);
				int i = start;
				int j = start + len - 1;
				int k = j;
				while (i <= j)
				{
					if (compare.Do(get.Do(j), pivot) == Less)
					{
						T temp = get.Do(i);
						set.Do(i++, get.Do(j));
						set.Do(j, temp);
					}
					else if (compare.Do(get.Do(j), pivot) == Equal)
					{
						j--;
					}
					else
					{
						T temp = get.Do(k);
						set.Do(k--, get.Do(j));
						set.Do(j--, temp);
					}
				}
				Quick_Recursive<T, Compare, Get, Set>(start, i - start, compare, get, set);
				Quick_Recursive<T, Compare, Get, Set>(k + 1, start + len - (k + 1), compare, get, set);
			}
		}

		#endregion

		#region Merge

		/// <summary>Sorts up to an array in non-decreasing order using the merge sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="array">The array to be sorted</param>
		/// <runtime>Ω(n*ln(n)), ε(n*ln(n)), O(n*ln(n))</runtime>
		/// <stability>True</stability>
		/// <memory>Θ(n)</memory>
		public static void Merge<T>(T[] array) =>
			Merge(Compare.Default, array, 0, array.Length);

		/// <summary>Sorts up to an array in non-decreasing order using the merge sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="compare">Returns zero or negative if the left is less than or equal to the right.</param>
		/// <param name="array">The array to be sorted</param>
		/// <runtime>Ω(n*ln(n)), ε(n*ln(n)), O(n*ln(n))</runtime>
		/// <stability>True</stability>
		/// <memory>Θ(n)</memory>
		public static void Merge<T>(Compare<T> compare, T[] array) =>
			Merge(compare, array, 0, array.Length);

		/// <summary>Sorts up to an array in non-decreasing order using the merge sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="compare">Returns zero or negative if the left is less than or equal to the right.</param>
		/// <param name="array">The array to be sorted</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <runtime>Ω(n*ln(n)), ε(n*ln(n)), O(n*ln(n))</runtime>
		/// <stability>True</stability>
		/// <memory>Θ(n)</memory>
		public static void Merge<T>(Compare<T> compare, T[] array, int start, int end) =>
			Merge(compare, array.WrapGetIndex(), array.WrapSetIndex(), start, end - start);

		/// <summary>Sorts up to an array in non-decreasing order using the merge sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="compare">Returns zero or negative if the left is less than or equal to the right.</param>
		/// <param name="get">Delegate for getting a value at a specified index.</param>
		/// <param name="set">Delegate for setting a value at a specified index.</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <runtime>Ω(n*ln(n)), ε(n*ln(n)), O(n*ln(n))</runtime>
		/// <stability>True</stability>
		/// <memory>Θ(n)</memory>
		public static void Merge<T>(Compare<T> compare, GetIndex<T> get, SetIndex<T> set, int start, int end) =>
			Merge_Recursive(compare, get, set, start, end - start);

		internal static void Merge_Recursive<T>(Compare<T> compare, GetIndex<T> get, SetIndex<T> set, int start, int len)
		{
			if (len > 1)
			{
				int half = len / 2;
				Merge_Recursive(compare, get, set, start, half);
				Merge_Recursive(compare, get, set, start + half, len - half);
				T[] sorted = new T[len];
				int i = start;
				int j = start + half;
				int k = 0;
				while (i < start + half && j < start + len)
				{
					if (compare(get(i), get(j)) == Greater)
					{
						sorted[k++] = get(j++);
					}
					else
					{
						sorted[k++] = get(i++);
					}
				}
				for (int h = 0; h < start + half - i; h++)
					sorted[k + h] = get(i + h);
				for (int h = 0; h < start + len - j; h++)
					sorted[k + h] = get(j + h);
				for (int h = 0; h < len; h++)
					set(start + h, sorted[0 + h]);
			}
		}

		#endregion

		#region Heap

		/// <summary>Sorts an entire array in non-decreasing order using the heap sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="array">The array to be sorted</param>
		/// <runtime>Ω(n*ln(n)), ε(n*ln(n)), O(n^2)</runtime>
		/// <stability>False</stability>
		/// <memory>O(1)</memory>
		public static void Heap<T>(T[] array) =>
			Heap(Compare.Default, array, 0, array.Length);

		/// <summary>Sorts an entire array in non-decreasing order using the heap sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="compare">The method of compare for the sort.</param>
		/// <param name="array">The array to be sorted</param>
		/// <runtime>Ω(n*ln(n)), ε(n*ln(n)), O(n^2)</runtime>
		/// <stability>False</stability>
		/// <memory>O(1)</memory>
		public static void Heap<T>(Compare<T> compare, T[] array) =>
			Heap(compare, array, 0, array.Length);

		/// <summary>Sorts an entire array in non-decreasing order using the heap sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="compare">The method of compare for the sort.</param>
		/// <param name="array">The array to be sorted</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <runtime>Ω(n*ln(n)), ε(n*ln(n)), O(n^2)</runtime>
		/// <stability>False</stability>
		/// <memory>O(1)</memory>
		public static void Heap<T>(Compare<T> compare, T[] array, int start, int end) =>
			Heap(compare, array.WrapGetIndex(), array.WrapSetIndex(), start, end);

		/// <summary>Sorts an entire array in non-decreasing order using the heap sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="compare">The method of compare for the sort.</param>
		/// <param name="get">Delegate for getting a value at a specified index.</param>
		/// <param name="set">Delegate for setting a value at a specified index.</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <runtime>Ω(n*ln(n)), ε(n*ln(n)), O(n^2)</runtime>
		/// <stability>False</stability>
		/// <memory>O(1)</memory>
		public static void Heap<T>(Compare<T> compare, GetIndex<T> get, SetIndex<T> set, int start, int end)
		{
			int heapSize = end - start;
			for (int i = (heapSize - 1) / 2; i >= 0; i--)
			{
				MaxHeapify(compare, get, set, heapSize, i);
			}
			for (int i = end - 1; i > start; i--)
			{
				T temp = get(0);
				set(0, get(i));
				set(i, temp);
				heapSize--;
				MaxHeapify(compare, get, set, heapSize, 0);
			}
		}

		internal static void MaxHeapify<T>(Compare<T> compare, GetIndex<T> get, SetIndex<T> set, int heapSize, int index)
		{
			int left = (index + 1) * 2 - 1;
			int right = (index + 1) * 2;
			int largest;
			if (left < heapSize && compare(get(left), get(index)) == Greater)
			{
				largest = left;
			}
			else
			{
				largest = index;
			}
			if (right < heapSize && compare(get(right), get(largest)) == Greater)
			{
				largest = right;
			}
			if (largest != index)
			{
				T temp = get(index);
				set(index, get(largest));
				set(largest, temp);
				MaxHeapify(compare, get, set, heapSize, largest);
			}
		}

		#endregion

		#region OddEven

		/// <summary>Sorts an entire array in non-decreasing order using the odd-even sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="array">The array to be sorted</param>
		/// <runtime>Ω(n), ε(n^2), O(n^2)</runtime>
		/// <stability>True</stability>
		/// <memory>O(1)</memory>
		public static void OddEven<T>(T[] array) =>
			OddEven(Compare.Default, array, 0, array.Length);

		/// <summary>Sorts an entire array in non-decreasing order using the odd-even sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="compare">The method of compare for the sort.</param>
		/// <param name="array">The array to be sorted</param>
		/// <runtime>Ω(n), ε(n^2), O(n^2)</runtime>
		/// <stability>True</stability>
		/// <memory>O(1)</memory>
		public static void OddEven<T>(Compare<T> compare, T[] array) =>
			OddEven(compare, array, 0, array.Length);

		/// <summary>Sorts an entire array in non-decreasing order using the odd-even sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="compare">The method of compare for the sort.</param>
		/// <param name="array">The array to be sorted</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <runtime>Ω(n), ε(n^2), O(n^2)</runtime>
		/// <stability>True</stability>
		/// <memory>O(1)</memory>
		public static void OddEven<T>(Compare<T> compare, T[] array, int start, int end) =>
			OddEven(compare, array.WrapGetIndex(), array.WrapSetIndex(), start, end);

		/// <summary>Sorts an entire array in non-decreasing order using the odd-even sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="compare">The method of compare for the sort.</param>
		/// <param name="get">Delegate for getting a value at a specified index.</param>
		/// <param name="set">Delegate for setting a value at a specified index.</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <runtime>Ω(n), ε(n^2), O(n^2)</runtime>
		/// <stability>True</stability>
		/// <memory>O(1)</memory>
		public static void OddEven<T>(Compare<T> compare, GetIndex<T> get, SetIndex<T> set, int start, int end)
		{
			var sorted = false;
			while (!sorted)
			{
				sorted = true;
				for (var i = start + 1; i < end - 1; i += 2)
				{
					if (compare(get(i), get(i + 1)) == Greater)
					{
						T temp = get(i);
						set(i, get(i + 1));
						set(i + 1, temp);
						sorted = false;
					}
				}
				for (var i = start; i < end - 1; i += 2)
				{
					if (compare(get(i), get(i + 1)) == Greater)
					{
						T temp = get(i);
						set(i, get(i + 1));
						set(i + 1, temp);
						sorted = false;
					}
				}
			}
		}

		#endregion

		#region Counting

#if false

		///// <summary>Method specifically for computing object keys in the Counting Sort algorithm.</summary>
		///// <typeparam name="T">The type of instances in the array to be sorted.</typeparam>
		///// <param name="instance">The instance to compute a counting key for.</param>
		///// <returns>The counting key computed from the provided instance.</returns>
		//public delegate int ComputeCountingKey(T instance);

		///// <summary>Sorts an entire array in non-decreasing order using the heap sort algorithm.</summary>
		///// <typeparam name="T">The type of objects stored within the array.</typeparam>
		///// <param name="computeCountingKey">Method specifically for computing object keys in the Counting Sort algorithm.</param>
		///// <param name="array">The array to be sorted</param>
		///// <runtime>Θ(Max(key)). Memory: Max(key). Stablity: yes</runtime>
		//public static void Counting(ComputeCountingKey computeCountingKey, T[] array)
		//{
		//	throw new System.NotImplementedException();

		//	// This code needs revision and conversion
		//	int[] count = new int[array.Length];
		//	int maxKey = 0;
		//	for (int i = 0; i < array.Length; i++)
		//	{
		//		int key = computeCountingKey(array[i]) / array.Length;
		//		count[key] += 1;
		//		if (key > maxKey)
		//			maxKey = key;
		//	}

		//	int total = 0;
		//	for (int i = 0; i < maxKey; i++)
		//	{
		//		int oldCount = count[i];
		//		count[i] = total;
		//		total += oldCount;
		//	}

		//	T[] output = new T[maxKey];
		//	for (int i = 0; i < array.Length; i++)
		//	{
		//		int key = computeCountingKey(array[i]);
		//		output[count[key]] = array[i];
		//		count[computeCountingKey(array[i])] += 1;
		//	}
		//}

		///// <summary>Sorts an entire array in non-decreasing order using the heap sort algorithm.</summary>
		///// <typeparam name="T">The type of objects stored within the array.</typeparam>
		///// <param name="computeCountingKey">Method specifically for computing object keys in the Counting Sort algorithm.</param>
		///// <param name="array">The array to be sorted</param>
		///// <runtime>Θ(Max(key)). Memory: Max(key). Stablity: yes</runtime>
		//public static void Counting(ComputeCountingKey computeCountingKey, T[] array, int start, int end)
		//{
		//	throw new System.NotImplementedException();
		//}

		///// <summary>Sorts an entire array in non-decreasing order using the heap sort algorithm.</summary>
		///// <typeparam name="T">The type of objects stored within the array.</typeparam>
		///// <param name="computeCountingKey">Method specifically for computing object keys in the Counting Sort algorithm.</param>
		///// <param name="array">The array to be sorted</param>
		///// <runtime>Θ(Max(key)). Memory: Max(key). Stablity: yes</runtime>
		//public static void Counting(ComputeCountingKey computeCountingKey, Get<T> get, Assign<T> set, int start, int end)
		//{
		//	throw new System.NotImplementedException();
		//}

#endif

		#endregion

		#region Shuffle

		/// <summary>Sorts an entire array in a randomized order.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="random">The random to shuffle with.</param>
		/// <param name="array">The aray to shuffle.</param>
		/// <runtime>O(n)</runtime>
		/// <memory>O(1)</memory>
		public static void Shuffle<T>(Random random, T[] array) =>
			Shuffle(random, array, 0, array.Length);

		/// <summary>Sorts an entire array in a randomized order.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="array">The array to shuffle.</param>
		/// <runtime>O(n)</runtime>
		/// <memory>O(1)</memory>
		public static void Shuffle<T>(T[] array) =>
			Shuffle(array, 0, array.Length);

		/// <summary>Sorts an entire array in a randomized order.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="random">The random to shuffle with.</param>
		/// <param name="array">The array to shuffle.</param>
		/// <param name="start">The starting index of the shuffle.</param>
		/// <param name="end">The ending index of the shuffle.</param>
		/// <runtime>O(n)</runtime>
		/// <memory>O(1)</memory>
		public static void Shuffle<T>(Random random, T[] array, int start, int end) =>
			Shuffle(random, array.WrapGetIndex(), array.WrapSetIndex(), start, end);

		/// <summary>Sorts an entire array in a randomized order.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="array">The array to shuffle.</param>
		/// <param name="start">The starting index of the shuffle.</param>
		/// <param name="end">The ending index of the shuffle.</param>
		/// <runtime>O(n)</runtime>
		/// <memory>O(1)</memory>
		public static void Shuffle<T>(T[] array, int start, int end) =>
			Shuffle(array.WrapGetIndex(), array.WrapSetIndex(), start, end);

		/// <summary>Sorts an entire array in a randomized order.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="get">The get accessor of the structure to shuffle.</param>
		/// <param name="set">The set accessor of the structure to shuffle.</param>
		/// <param name="start">The starting index of the shuffle.</param>
		/// <param name="end">The ending index of the shuffle.</param>
		/// <runtime>O(n)</runtime>
		/// <memory>O(1)</memory>
		public static void Shuffle<T>(GetIndex<T> get, SetIndex<T> set, int start, int end) =>
			Shuffle(new Random(), get, set, start, end);

		/// <summary>Sorts an entire array in a randomized order.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="random">The random to shuffle with.</param>
		/// <param name="get">The get accessor of the structure to shuffle.</param>
		/// <param name="set">The set accessor of the structure to shuffle.</param>
		/// <param name="start">The starting index of the shuffle.</param>
		/// <param name="end">The ending index of the shuffle.</param>
		/// <runtime>O(n)</runtime>
		/// <memory>O(1)</memory>
		public static void Shuffle<T>(Random random, GetIndex<T> get, SetIndex<T> set, int start, int end)
		{
			for (int i = start; i < end; i++)
			{
				int randomIndex = random.Next(start, end);
				T temp = get(i);
				set(i, get(randomIndex));
				set(randomIndex, temp);
			}
		}

		#endregion

		#region Bogo

		/// <summary>Sorts an entire array in non-decreasing order using the slow sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="array">The array to be sorted.</param>
		/// <runtime>Ω(n), ε(n*n!), O(∞)</runtime>
		/// <stability>False</stability>
		/// <memory>O(1)</memory>
		public static void Bogo<T>(T[] array) =>
			Bogo(Compare.Default, array);

		/// <summary>Sorts an entire array in non-decreasing order using the slow sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="compare">The method of compare for the sort.</param>
		/// <param name="array">The array to be sorted.</param>
		/// <runtime>Ω(n), ε(n*n!), O(∞)</runtime>
		/// <stability>False</stability>
		/// <memory>O(1)</memory>
		public static void Bogo<T>(Compare<T> compare, T[] array) =>
			Bogo(compare, array, 0, array.Length - 1);

		/// <summary>Sorts an entire array in non-decreasing order using the slow sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="compare">The method of compare for the sort.</param>
		/// <param name="array">The array to be sorted.</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <runtime>Ω(n), ε(n*n!), O(∞)</runtime>
		/// <stability>False</stability>
		/// <memory>O(1)</memory>
		public static void Bogo<T>(Compare<T> compare, T[] array, int start, int end) =>
			Bogo(compare, array.WrapGetIndex(), array.WrapSetIndex(), start, end);

		/// <summary>Sorts an entire array in non-decreasing order using the slow sort algorithm.</summary>
		/// <typeparam name="T">The type of objects stored within the array.</typeparam>
		/// <param name="compare">The method of compare for the sort.</param>
		/// <param name="get">Delegate for getting a value at a specified index.</param>
		/// <param name="set">Delegate for setting a value at a specified index.</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <runtime>Ω(n), ε(n*n!), O(∞)</runtime>
		/// <stability>False</stability>
		/// <memory>O(1)</memory>
		public static void Bogo<T>(Compare<T> compare, GetIndex<T> get, SetIndex<T> set, int start, int end)
		{
			while (!BogoCheck(compare, get, start, end))
			{
				Shuffle(get, set, start, end);
			}
		}

		internal static bool BogoCheck<T>(Compare<T> compare, GetIndex<T> get, int start, int end)
		{
			for (int i = start; i <= end - 1; i++)
			{
				if (compare(get(i), get(i + 1)) == Greater)
				{
					return false;
				}
			}
			return true;
		}

		#endregion

		#region Slow

#if false

		///// <summary>Sorts an entire array of in non-decreasing order using the slow sort algorithm.</summary>
		///// <typeparam name="T">The type of objects stored within the array.</typeparam>
		///// <param name="array">The array to be sorted</param>
		///// <runtime>Ω(n), ε(n*n!), O(n*n!)</runtime>
		///// <stability></stability>
		///// <memory>O(1)</memory>
		//public static void Slow<T>(T[] array)
		//{
		//    Slow(Compare.Default, array, 0, array.Length);
		//}

		///// <summary>Sorts an entire array of in non-decreasing order using the slow sort algorithm.</summary>
		///// <typeparam name="T">The type of objects stored within the array.</typeparam>
		///// <param name="compare">The method of compare for the sort.</param>
		///// <param name="array">The array to be sorted</param>
		///// <runtime>Ω(n), ε(n*n!), O(n*n!)</runtime>
		///// <stability></stability>
		///// <memory>O(1)</memory>
		//public static void Slow<T>(Compare<T> compare, T[] array)
		//{
		//    Slow(compare, array, 0, array.Length);
		//}

		///// <summary>Sorts an entire array of in non-decreasing order using the slow sort algorithm.</summary>
		///// <typeparam name="T">The type of objects stored within the array.</typeparam>
		///// <param name="compare">The method of compare for the sort.</param>
		///// <param name="array">The array to be sorted</param>
		///// <runtime>Ω(n), ε(n*n!), O(n*n!)</runtime>
		///// <stability></stability>
		///// <memory>O(1)</memory>
		//public static void Slow<T>(Compare<T> compare, T[] array, int start, int end)
		//{
		//    T get(int index) { return array[index]; }
		//    void set(int index, T value) { array[index] = value; }
		//    Slow(compare, get, set, start, end);
		//}

		///// <summary>Sorts an entire array of in non-decreasing order using the slow sort algorithm.</summary>
		///// <typeparam name="T">The type of objects stored within the array.</typeparam>
		///// <param name="compare">The method of compare for the sort.</param>
		///// <param name="array">The array to be sorted</param>
		///// <runtime>Ω(n), ε(n*n!), O(n*n!)</runtime>
		///// <stability></stability>
		///// <memory>O(1)</memory>
		//public static void Slow<T>(Compare<T> compare, GetIndex<T> get, SetIndex<T> set, int start, int end)
		//{
		//    throw new System.NotImplementedException();
		//    Slow_Recursive(compare, get, set, start, end);
		//}

		//internal static void Slow_Recursive<T>(Compare<T> compare, GetIndex<T> get, SetIndex<T> set, int i, int j)
		//{
		//    if (i >= j)
		//    {
		//        return;
		//    }
		//    int m = (i + j) / 2;
		//    Slow_Recursive(compare, get, set, i, m);
		//    Slow_Recursive(compare, get, set, m + 1, j);
		//    if (compare(get(m), get(j)) == Comparison.Less)
		//    {
		//        T temp = get(j);
		//        set(j, get(m));
		//        set(m, temp);
		//    }
		//    Slow_Recursive(compare, get, set, i, j - 1);
		//}

#endif

		#endregion
	}
}
