using System;
using static Towel.Syntax;

namespace Towel
{
	/// <summary>Contains static sorting algorithms.</summary>
	public static class Sort
	{
		#region Bubble

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
		/// <summary>Sorts values using the bubble sort algorithm.</summary>
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="Compare">The compare function.</typeparam>
		/// <typeparam name="Get">The get function.</typeparam>
		/// <typeparam name="Set">The set function.</typeparam>
		/// <param name="compare">The compare function.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <param name="array">The array to be sorted.</param>
		/// <runtime>Ω(n), ε(n^2), O(n^2)</runtime>
		/// <stability>True</stability>
		/// <memory>O(1)</memory>
		[Obsolete("This method is for documentation only.", true)]
		internal static void Bubble_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="Bubble_XML"/>
		public static void Bubble<T>(T[] array, Compare<T> compare = null) =>
			Bubble(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Bubble_XML"/>
		public static void Bubble<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Bubble(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Bubble_XML"/>
		public static void Bubble<T>(T[] array, int start, int end, Compare<T> compare = null) =>
			Bubble<T, CompareRuntime<T>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Compare.Default, array, array);

		/// <inheritdoc cref="Bubble_XML"/>
		public static void Bubble<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Bubble<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <inheritdoc cref="Bubble_XML"/>
		public static void Bubble<T>(int start, int end, GetIndex<T> get, SetIndex<T> set, Compare<T> compare = null) =>
			Bubble<T, CompareRuntime<T>, GetIndexRuntime<T>, SetIndexRuntime<T>>(start, end, compare ?? Compare.Default, get, set);

		/// <inheritdoc cref="Bubble_XML"/>
		public static void Bubble<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, ICompare<T>
			where Get : struct, IGetIndex<T>
			where Set : struct, ISetIndex<T>
		{
			for (int i = start; i <= end; i++)
			{
				for (int j = start; j <= end - 1; j++)
				{
					if (compare.Do(get.Do(j), get.Do(j + 1)) is Greater)
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

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
		/// <summary>Sorts values using the selection sort algoritm.</summary>
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="Compare">The compare delegate.</typeparam>
		/// <typeparam name="Get">The get function.</typeparam>
		/// <typeparam name="Set">The set function.</typeparam>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <param name="compare">The compare delegate.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		/// <param name="array">The array to be sorted.</param>
		/// <runtime>Ω(n^2), ε(n^2), O(n^2)</runtime>
		/// <stability>False</stability>
		/// <memory>O(1)</memory>
		[Obsolete("This method is for documentation only.", true)]
		internal static void Selection_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="Selection_XML"/>
		public static void Selection<T>(T[] array, Compare<T> compare = null) =>
			Selection(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Selection_XML"/>
		public static void Selection<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Selection(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Selection_XML"/>
		public static void Selection<T>(T[] array, int start, int end, Compare<T> compare = null) =>
			Selection<T, CompareRuntime<T>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Compare.Default, array, array);

		/// <inheritdoc cref="Selection_XML"/>
		public static void Selection<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Selection<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <inheritdoc cref="Selection_XML"/>
		public static void Selection<T>(int start, int end, GetIndex<T> get, SetIndex<T> set, Compare<T> compare = null) =>
			Selection<T, CompareRuntime<T>, GetIndexRuntime<T>, SetIndexRuntime<T>>(start, end, compare ?? Compare.Default, get, set);

		/// <inheritdoc cref="Selection_XML"/>
		public static void Selection<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, ICompare<T>
			where Get : struct, IGetIndex<T>
			where Set : struct, ISetIndex<T>
		{
			for (int i = start; i <= end; i++)
			{
				int min_idx = i;
				for (int j = i + 1; j <= end; j++)
				{
					if (compare.Do(get.Do(j), get.Do(min_idx)) is Less)
					{
						min_idx = j;
					}
				}
				T temp = get.Do(min_idx);
				set.Do(min_idx, get.Do(i));
				set.Do(i, temp);
			}
		}

		#endregion

		#region Insertion

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
		/// <summary>Sorts values using the insertion sort algorithm.</summary>
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="Compare">The compare function.</typeparam>
		/// <typeparam name="Get">The get function.</typeparam>
		/// <typeparam name="Set">The set function.</typeparam>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <param name="compare">The compare function.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		/// <param name="array">The array to be sorted.</param>
		/// <runtime>Ω(n), ε(n^2), O(n^2)</runtime>
		/// <stability>True</stability>
		/// <memory>O(1)</memory>
		[Obsolete("This method is for documentation only.", true)]
		internal static void Insertion_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="Insertion_XML"/>
		public static void Insertion<T>(T[] array, Compare<T> compare = null) =>
			Insertion(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Insertion_XML"/>
		public static void Insertion<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Insertion(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Insertion_XML"/>
		public static void Insertion<T>(T[] array, int start, int end, Compare<T> compare = null) =>
			Insertion<T, CompareRuntime<T>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Compare.Default, array, array);

		/// <inheritdoc cref="Insertion_XML"/>
		public static void Insertion<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Insertion<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <inheritdoc cref="Insertion_XML"/>
		public static void Insertion<T>(int start, int end, GetIndex<T> get, SetIndex<T> set, Compare<T> compare = null) =>
			Insertion<T, CompareRuntime<T>, GetIndexRuntime<T>, SetIndexRuntime<T>>(start, end, compare ?? Compare.Default, get, set);

		/// <inheritdoc cref="Insertion_XML"/>
		public static void Insertion<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, ICompare<T>
			where Get : struct, IGetIndex<T>
			where Set : struct, ISetIndex<T>
		{
			for (int i = start + 1; i <= end; i++)
			{
				T temp = get.Do(i);
				int j = i;
				for (; j > start && compare.Do(get.Do(j - 1), temp) is Greater; j--)
				{
					set.Do(j, get.Do(j - 1));
				}
				set.Do(j, temp);
			}
		}

		#endregion

		#region Quick

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
		/// <summary>Sorts values using the quick sort algorithm.</summary>
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="Compare">The compare function.</typeparam>
		/// <typeparam name="Get">The get function.</typeparam>
		/// <typeparam name="Set">The set function.</typeparam>
		/// <param name="compare">The compare function.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <param name="array">The array to be sorted.</param>
		/// <runtime>Ω(n*ln(n)), ε(n*ln(n)), O(n^2)</runtime>
		/// <stability>False</stability>
		/// <memory>ln(n)</memory>
		[Obsolete("This method is for documentation only.", true)]
		internal static void Quick_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="Quick_XML"/>
		public static void Quick<T>(T[] array, Compare<T> compare = null) =>
			Quick(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Quick_XML"/>
		public static void Quick<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Quick(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Quick_XML"/>
		public static void Quick<T>(T[] array, int start, int end, Compare<T> compare = null) =>
			Quick<T, CompareRuntime<T>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Compare.Default, array, array);

		/// <inheritdoc cref="Quick_XML"/>
		public static void Quick<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Quick<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <inheritdoc cref="Quick_XML"/>
		public static void Quick<T>(int start, int end, GetIndex<T> get, SetIndex<T> set, Compare<T> compare = null) =>
			Quick<T, CompareRuntime<T>, GetIndexRuntime<T>, SetIndexRuntime<T>>(start, end, compare ?? Compare.Default, get, set);

		/// <inheritdoc cref="Quick_XML"/>
		public static void Quick<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, ICompare<T>
			where Get : struct, IGetIndex<T>
			where Set : struct, ISetIndex<T>
		{
			Quick_Recursive(start, end - start + 1);

			void Quick_Recursive(int startIndex, int len)
			{
				if (len > 1)
				{
					T pivot = get.Do(startIndex);
					int i = startIndex;
					int j = startIndex + len - 1;
					int k = j;
					while (i <= j)
					{
						if (compare.Do(get.Do(j), pivot) is Less)
						{
							T temp = get.Do(i);
							set.Do(i++, get.Do(j));
							set.Do(j, temp);
						}
						else if (compare.Do(get.Do(j), pivot) is Equal)
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
					Quick_Recursive(startIndex, i - startIndex);
					Quick_Recursive(k + 1, startIndex + len - (k + 1));
				}
			}
		}

		#endregion

		#region Merge

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
		/// <summary>Sorts values using the merge sort algorithm.</summary>
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="Compare">The compare function.</typeparam>
		/// <typeparam name="Get">The get function.</typeparam>
		/// <typeparam name="Set">The set function.</typeparam>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <param name="compare">The compare function.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		/// <param name="array">The array to be sorted.</param>
		/// <runtime>Ω(n*ln(n)), ε(n*ln(n)), O(n*ln(n))</runtime>
		/// <stability>True</stability>
		/// <memory>Θ(n)</memory>
		[Obsolete("This method is for documentation only.", true)]
		internal static void Merge_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="Merge_XML"/>
		public static void Merge<T>(T[] array, Compare<T> compare = null) =>
			Merge(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Merge_XML"/>
		public static void Merge<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Merge(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Merge_XML"/>
		public static void Merge<T>(T[] array, int start, int end, Compare<T> compare = null) =>
			Merge<T, CompareRuntime<T>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Compare.Default, array, array);

		/// <inheritdoc cref="Merge_XML"/>
		public static void Merge<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Merge<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <inheritdoc cref="Merge_XML"/>
		public static void Merge<T>(int start, int end, GetIndex<T> get, SetIndex<T> set, Compare<T> compare = null) =>
			Merge<T, CompareRuntime<T>, GetIndexRuntime<T>, SetIndexRuntime<T>>(start, end, compare ?? Compare.Default, get, set);

		/// <inheritdoc cref="Merge_XML"/>
		public static void Merge<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, ICompare<T>
			where Get : struct, IGetIndex<T>
			where Set : struct, ISetIndex<T> =>
			Merge_Recursive<T, Compare, Get, Set>(start, end - start + 1, compare, get, set);

		internal static void Merge_Recursive<T, Compare, Get, Set>(int start, int len, Compare compare, Get get, Set set)
			where Compare : struct, ICompare<T>
			where Get : struct, IGetIndex<T>
			where Set : struct, ISetIndex<T>
		{
			if (len > 1)
			{
				int half = len / 2;
				Merge_Recursive<T, Compare, Get, Set>(start, half, compare, get, set);
				Merge_Recursive<T, Compare, Get, Set>(start + half, len - half, compare, get, set);
				T[] sorted = new T[len];
				int i = start;
				int j = start + half;
				int k = 0;
				while (i < start + half && j < start + len)
				{
					if (compare.Do(get.Do(i), get.Do(j)) is Greater)
					{
						sorted[k++] = get.Do(j++);
					}
					else
					{
						sorted[k++] = get.Do(i++);
					}
				}
				for (int h = 0; h < start + half - i; h++)
				{
					sorted[k + h] = get.Do(i + h);
				}
				for (int h = 0; h < start + len - j; h++)
				{
					sorted[k + h] = get.Do(j + h);
				}
				for (int h = 0; h < len; h++)
				{
					set.Do(start + h, sorted[0 + h]);
				}
			}
		}

		#endregion

		#region Heap

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
		/// <summary>Sorts values using the heap sort algorithm.</summary>
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="Compare">The compare function.</typeparam>
		/// <typeparam name="Get">The get function.</typeparam>
		/// <typeparam name="Set">The set function.</typeparam>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <param name="compare">The compare function.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		/// <param name="array">The array to be sorted.</param>
		/// <runtime>Ω(n*ln(n)), ε(n*ln(n)), O(n^2)</runtime>
		/// <stability>False</stability>
		/// <memory>O(1)</memory>
		[Obsolete("This method is for documentation only.", true)]
		internal static void Heap_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="Heap_XML"/>
		public static void Heap<T>(T[] array, Compare<T> compare = null) =>
			Heap(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Heap_XML"/>
		public static void Heap<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Heap(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Heap_XML"/>
		public static void Heap<T>(T[] array, int start, int end, Compare<T> compare = null) =>
			Heap<T, CompareRuntime<T>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Compare.Default, array, array);

		/// <inheritdoc cref="Heap_XML"/>
		public static void Heap<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Heap<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <inheritdoc cref="Heap_XML"/>
		public static void Heap<T>(int start, int end, GetIndex<T> get, SetIndex<T> set, Compare<T> compare = null) =>
			Heap<T, CompareRuntime<T>, GetIndexRuntime<T>, SetIndexRuntime<T>>(start, end, compare ?? Compare.Default, get, set);

		/// <inheritdoc cref="Heap_XML"/>
		public static void Heap<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, ICompare<T>
			where Get : struct, IGetIndex<T>
			where Set : struct, ISetIndex<T>
		{
			int heapSize = end - start + 1;
			for (int i = heapSize / 2; i >= 0; i--)
			{
				MaxHeapify(heapSize + start, i, start);
			}
			for (int i = end; i >= start; i--)
			{
				T temp = get.Do(start);
				set.Do(start, get.Do(i));
				set.Do(i, temp);
				heapSize--;
				MaxHeapify(heapSize + start, 0, start);
			}

			void MaxHeapify(int heapSize, int index, int offset)
			{
				int left = ((index + 1) * 2 - 1) + offset;
				int right = ((index + 1) * 2) + offset;
				index += offset;
				int largest = index;
				if (left < heapSize && compare.Do(get.Do(left), get.Do(largest)) is Greater)
				{
					largest = left;
				}
				if (right < heapSize && compare.Do(get.Do(right), get.Do(largest)) is Greater)
				{
					largest = right;
				}
				if (largest != index)
				{
					T temp = get.Do(index);
					set.Do(index, get.Do(largest));
					set.Do(largest, temp);
					MaxHeapify(heapSize, largest - offset, offset);
				}
			}
		}

		#endregion

		#region OddEven

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
		/// <summary>Sorts values using the odd even sort algorithm.</summary>
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="Compare">The compare function.</typeparam>
		/// <typeparam name="Get">The get function.</typeparam>
		/// <typeparam name="Set">The set function.</typeparam>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <param name="compare">The compare function.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		/// <param name="array">The array to be sorted.</param>
		/// <runtime>Ω(n), ε(n^2), O(n^2)</runtime>
		/// <stability>True</stability>
		/// <memory>O(1)</memory>
		[Obsolete("This method is for documentation only.", true)]
		internal static void OddEven_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="OddEven_XML"/>
		public static void OddEven<T>(T[] array, Compare<T> compare = null) =>
			OddEven(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="OddEven_XML"/>
		public static void OddEven<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			OddEven(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="OddEven_XML"/>
		public static void OddEven<T>(T[] array, int start, int end, Compare<T> compare = null) =>
			OddEven<T, CompareRuntime<T>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Compare.Default, array, array);

		/// <inheritdoc cref="OddEven_XML"/>
		public static void OddEven<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			OddEven<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <inheritdoc cref="OddEven_XML"/>
		public static void OddEven<T>(int start, int end, GetIndex<T> get, SetIndex<T> set, Compare<T> compare = null) =>
			OddEven<T, CompareRuntime<T>, GetIndexRuntime<T>, SetIndexRuntime<T>>(start, end, compare ?? Compare.Default, get, set);

		/// <inheritdoc cref="OddEven_XML"/>
		public static void OddEven<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, ICompare<T>
			where Get : struct, IGetIndex<T>
			where Set : struct, ISetIndex<T>
		{
			bool sorted = false;
			while (!sorted)
			{
				sorted = true;
				for (int i = start; i < end; i += 2)
				{
					if (compare.Do(get.Do(i), get.Do(i + 1)) is Greater)
					{
						T temp = get.Do(i);
						set.Do(i, get.Do(i + 1));
						set.Do(i + 1, temp);
						sorted = false;
					}
				}
				for (int i = start + 1; i < end; i += 2)
				{
					if (compare.Do(get.Do(i), get.Do(i + 1)) is Greater)
					{
						T temp = get.Do(i);
						set.Do(i, get.Do(i + 1));
						set.Do(i + 1, temp);
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

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
		/// <summary>Sorts values into a randomized order.</summary>
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="Get">The get function.</typeparam>
		/// <typeparam name="Set">The set function.</typeparam>
		/// <param name="start">The starting index of the shuffle.</param>
		/// <param name="end">The ending index of the shuffle.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		/// <param name="random">The random to shuffle with.</param>
		/// <param name="array">The array to shuffle.</param>
		/// <runtime>O(n)</runtime>
		/// <memory>O(1)</memory>
		[Obsolete("This method is for documentation only.", true)]
		internal static void Shuffle_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="Shuffle_XML"/>
		public static void Shuffle<T>(T[] array, Random random = null) =>
			Shuffle(array, 0, array.Length - 1, random);

		/// <inheritdoc cref="Shuffle_XML"/>
		public static void Shuffle<T>(T[] array, int start, int end, Random random = null) =>
			Shuffle<T, GetIndexArray<T>, SetIndexArray<T>>(start, end, array, array, random);

		/// <inheritdoc cref="Shuffle_XML"/>
		public static void Shuffle<T>(int start, int end, GetIndex<T> get, SetIndex<T> set, Random random = null) =>
			Shuffle<T, GetIndexRuntime<T>, SetIndexRuntime<T>>(start, end, get, set, random);

		/// <inheritdoc cref="Shuffle_XML"/>
		public static void Shuffle<T, Get, Set>(int start, int end, Get get = default, Set set = default, Random random = null)
			where Get : struct, IGetIndex<T>
			where Set : struct, ISetIndex<T>
		{
			random ??= new Random();
			for (int i = start; i <= end; i++)
			{
				int randomIndex = random.Next(start, end);
				T temp = get.Do(i);
				set.Do(i, get.Do(randomIndex));
				set.Do(randomIndex, temp);
			}
		}

		#endregion

		#region Bogo

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
		/// <summary>Sorts values using the bogo sort algorithm.</summary>
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="Compare">The compare function.</typeparam>
		/// <typeparam name="Get">The get function.</typeparam>
		/// <typeparam name="Set">The set function.</typeparam>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <param name="compare">The compare function.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		/// <param name="random">The random to use while sorting.</param>
		/// <param name="array">The array to be sorted.</param>
		/// <runtime>Ω(n), ε(n*n!), O(∞)</runtime>
		/// <stability>False</stability>
		/// <memory>O(1)</memory>
		[Obsolete("This method is for documentation only.", true)]
		internal static void Bogo_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="Bogo_XML"/>
		public static void Bogo<T>(T[] array, Compare<T> compare = null, Random random = null) =>
			Bogo(array, 0, array.Length - 1, compare, random);

		/// <inheritdoc cref="Bogo_XML"/>
		public static void Bogo<T, Compare>(T[] array, Compare compare = default, Random random = null)
			where Compare : struct, ICompare<T> =>
			Bogo(array, 0, array.Length - 1, compare, random);

		/// <inheritdoc cref="Bogo_XML"/>
		public static void Bogo<T>(T[] array, int start, int end, Compare<T> compare = null, Random random = null) =>
			Bogo<T, CompareRuntime<T>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Compare.Default, array, array, random);

		/// <inheritdoc cref="Bogo_XML"/>
		public static void Bogo<T, Compare>(T[] array, int start, int end, Compare compare = default, Random random = null)
			where Compare : struct, ICompare<T> =>
			Bogo<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array, random);

		/// <inheritdoc cref="Bogo_XML"/>
		public static void Bogo<T>(int start, int end, GetIndex<T> get, SetIndex<T> set, Compare<T> compare = null, Random random = null) =>
			Bogo<T, CompareRuntime<T>, GetIndexRuntime<T>, SetIndexRuntime<T>>(start, end, compare ?? Compare.Default, get, set, random);

		/// <inheritdoc cref="Bogo_XML"/>
		public static void Bogo<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default, Random random = null)
			where Compare : struct, ICompare<T>
			where Get : struct, IGetIndex<T>
			where Set : struct, ISetIndex<T>
		{
			random ??= new Random();
			while (!BogoCheck(start, end))
			{
				Shuffle<T, Get, Set>(start, end, get, set, random);
			}

			bool BogoCheck(int start, int end)
			{
				for (int i = start; i <= end - 1; i++)
				{
					if (compare.Do(get.Do(i), get.Do(i + 1)) is Greater)
					{
						return false;
					}
				}
				return true;
			}
		}

		#endregion

		#region Slow

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
		/// <summary>Sorts values using the slow sort algorithm.</summary>
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="Compare">The compare function.</typeparam>
		/// <typeparam name="Get">The get function.</typeparam>
		/// <typeparam name="Set">The set function.</typeparam>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <param name="compare">The compare function.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		/// <param name="array">The array to be sorted.</param>
		[Obsolete("This method is for documentation only.", true)]
		internal static void Slow_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="Slow_XML"/>
		public static void Slow<T>(T[] array, Compare<T> compare = null) =>
			Slow(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Slow_XML"/>
		public static void Slow<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Slow(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Slow_XML"/>
		public static void Slow<T>(T[] array, int start, int end, Compare<T> compare = null) =>
			Slow<T, CompareRuntime<T>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Compare.Default, array, array);

		/// <inheritdoc cref="Slow_XML"/>
		public static void Slow<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Slow<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <inheritdoc cref="Slow_XML"/>
		public static void Slow<T>(int start, int end, GetIndex<T> get, SetIndex<T> set, Compare<T> compare = null) =>
			Slow<T, CompareRuntime<T>, GetIndexRuntime<T>, SetIndexRuntime<T>>(start, end, compare ?? Compare.Default, get, set);

		/// <inheritdoc cref="Slow_XML"/>
		public static void Slow<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, ICompare<T>
			where Get : struct, IGetIndex<T>
			where Set : struct, ISetIndex<T>
		{
			Slow_Recursive(start, end);

			void Slow_Recursive(int i, int j)
			{
				if (i >= j)
				{
					return;
				}
				int m = (i + j) / 2;
				Slow_Recursive(i, m);
				Slow_Recursive(m + 1, j);
				if (compare.Do(get.Do(j), get.Do(m)) is Less)
				{
					T temp = get.Do(j);
					set.Do(j, get.Do(m));
					set.Do(m, temp);
				}
				Slow_Recursive(i, j - 1);
			}
		}

		#endregion

		#region Gnome

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
		/// <summary>Sorts values using the gnome sort algorithm.</summary>
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="Compare">The compare function.</typeparam>
		/// <typeparam name="Get">The get function.</typeparam>
		/// <typeparam name="Set">The set function.</typeparam>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <param name="compare">The compare function.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		/// <param name="array">The array to be sorted.</param>
		[Obsolete("This method is for documentation only.", true)]
		internal static void Gnome_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="Gnome_XML"/>
		public static void Gnome<T>(T[] array, Compare<T> compare = null) =>
			Gnome(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Gnome_XML"/>
		public static void Gnome<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Gnome(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Gnome_XML"/>
		public static void Gnome<T>(T[] array, int start, int end, Compare<T> compare = null) =>
			Gnome<T, CompareRuntime<T>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Compare.Default, array, array);

		/// <inheritdoc cref="Gnome_XML"/>
		public static void Gnome<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Gnome<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <inheritdoc cref="Gnome_XML"/>
		public static void Gnome<T>(int start, int end, GetIndex<T> get, SetIndex<T> set, Compare<T> compare = null) =>
			Gnome<T, CompareRuntime<T>, GetIndexRuntime<T>, SetIndexRuntime<T>>(start, end, compare ?? Compare.Default, get, set);

		/// <inheritdoc cref="Gnome_XML"/>
		public static void Gnome<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, ICompare<T>
			where Get : struct, IGetIndex<T>
			where Set : struct, ISetIndex<T>
		{
			int i = start;
			while (i <= end)
			{
				if (i == start || compare.Do(get.Do(i), get.Do(i - 1)) != Less)
				{
					i++;
				}
				else
				{
					T temp = get.Do(i);
					set.Do(i, get.Do(i - 1));
					set.Do(i - 1, temp);
					i--;
				}
			}
		}

		#endregion

		#region Comb

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
		/// <summary>Sorts values using the comb sort algorithm.</summary>
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="Compare">The compare function.</typeparam>
		/// <typeparam name="Get">The get function.</typeparam>
		/// <typeparam name="Set">The set function.</typeparam>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <param name="compare">The compare function.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		/// <param name="array">The array to be sorted.</param>
		[Obsolete("This method is for documentation only.", true)]
		internal static void Comb_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="Comb_XML"/>
		public static void Comb<T>(T[] array, Compare<T> compare = null) =>
			Comb(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Comb_XML"/>
		public static void Comb<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Comb(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Comb_XML"/>
		public static void Comb<T>(T[] array, int start, int end, Compare<T> compare = null) =>
			Comb<T, CompareRuntime<T>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Compare.Default, array, array);

		/// <inheritdoc cref="Comb_XML"/>
		public static void Comb<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Comb<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <inheritdoc cref="Comb_XML"/>
		public static void Comb<T>(int start, int end, GetIndex<T> get, SetIndex<T> set, Compare<T> compare = null) =>
			Comb<T, CompareRuntime<T>, GetIndexRuntime<T>, SetIndexRuntime<T>>(start, end, compare ?? Compare.Default, get, set);

		/// <inheritdoc cref="Comb_XML"/>
		public static void Comb<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, ICompare<T>
			where Get : struct, IGetIndex<T>
			where Set : struct, ISetIndex<T>
		{
			const double shrink = 1.3;
			int gap = end - start + 1;
			bool sorted = false;
			while (!sorted)
			{
				gap = (int)(gap / shrink);
				if (gap <= 1)
				{
					gap = 1;
					sorted = true;
				}
				for (int i = start; i + gap <= end; i++)
				{
					if (compare.Do(get.Do(i), get.Do(i + gap)) is Greater)
					{
						T temp = get.Do(i);
						set.Do(i, get.Do(i + gap));
						set.Do(i + gap, temp);
						sorted = false;
					}
				}
			}
		}

		#endregion

		#region Shell

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
		/// <summary>Sorts values using the shell sort algorithm.</summary>
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="Compare">The compare function.</typeparam>
		/// <typeparam name="Get">The get function.</typeparam>
		/// <typeparam name="Set">The set function.</typeparam>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <param name="compare">The compare function.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		/// <param name="array">The array to be sorted.</param>
		[Obsolete("This method is for documentation only.", true)]
		internal static void Shell_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="Shell_XML"/>
		public static void Shell<T>(T[] array, Compare<T> compare = null) =>
			Shell(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Shell_XML"/>
		public static void Shell<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Shell(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Shell_XML"/>
		public static void Shell<T>(T[] array, int start, int end, Compare<T> compare = null) =>
			Shell<T, CompareRuntime<T>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Compare.Default, array, array);

		/// <inheritdoc cref="Shell_XML"/>
		public static void Shell<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Shell<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <inheritdoc cref="Shell_XML"/>
		public static void Shell<T>(int start, int end, GetIndex<T> get, SetIndex<T> set, Compare<T> compare = null) =>
			Shell<T, CompareRuntime<T>, GetIndexRuntime<T>, SetIndexRuntime<T>>(start, end, compare ?? Compare.Default, get, set);

		/// <inheritdoc cref="Shell_XML"/>
		public static void Shell<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, ICompare<T>
			where Get : struct, IGetIndex<T>
			where Set : struct, ISetIndex<T>
		{
			int[] gaps = Ɐ(701, 301, 132, 57, 23, 10, 4, 1);
			foreach (int gap in gaps)
			{
				for (int i = gap + start; i <= end; i++)
				{
					T temp = get.Do(i);
					int j = i;
					for (; j >= gap && compare.Do(get.Do(j - gap), temp) is Greater; j -= gap)
					{
						set.Do(j, get.Do(j - gap));
					}
					set.Do(j, temp);
				}
			}
		}

		#endregion

		#region Cocktail

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
		/// <summary>Sorts values using the shell sort algorithm.</summary>
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="Compare">The compare function.</typeparam>
		/// <typeparam name="Get">The get function.</typeparam>
		/// <typeparam name="Set">The set function.</typeparam>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <param name="compare">The compare function.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		/// <param name="array">The array to be sorted.</param>
		[Obsolete("This method is for documentation only.", true)]
		internal static void Cocktail_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="Cocktail_XML"/>
		public static void Cocktail<T>(T[] array, Compare<T> compare = null) =>
			Cocktail(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Cocktail_XML"/>
		public static void Cocktail<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Cocktail(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Cocktail_XML"/>
		public static void Cocktail<T>(T[] array, int start, int end, Compare<T> compare = null) =>
			Cocktail<T, CompareRuntime<T>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Compare.Default, array, array);

		/// <inheritdoc cref="Cocktail_XML"/>
		public static void Cocktail<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, ICompare<T> =>
			Cocktail<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <inheritdoc cref="Cocktail_XML"/>
		public static void Cocktail<T>(int start, int end, GetIndex<T> get, SetIndex<T> set, Compare<T> compare = null) =>
			Cocktail<T, CompareRuntime<T>, GetIndexRuntime<T>, SetIndexRuntime<T>>(start, end, compare ?? Compare.Default, get, set);

		/// <inheritdoc cref="Cocktail_XML"/>
		public static void Cocktail<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, ICompare<T>
			where Get : struct, IGetIndex<T>
			where Set : struct, ISetIndex<T>
		{
			while (true)
			{
				bool swapped = false;
				for (int i = start; i <= end - 1; i++)
				{
					if (compare.Do(get.Do(i), get.Do(i + 1)) is Greater)
					{
						T temp = get.Do(i);
						set.Do(i, get.Do(i + 1));
						set.Do(i + 1, temp);
						swapped = true;
					}
				}
				if (!swapped)
				{
					break;
				}
				swapped = false;
				for (int i = end - 1; i >= start; i--)
				{
					if (compare.Do(get.Do(i), get.Do(i + 1)) is Greater)
					{
						T temp = get.Do(i);
						set.Do(i, get.Do(i + 1));
						set.Do(i + 1, temp);
						swapped = true;
					}
				}
				if (!swapped)
				{
					break;
				}
			}
		}

		#endregion
	}
}
