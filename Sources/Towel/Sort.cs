using System;
using static Towel.Syntax;

namespace Towel
{
	/// <summary>Contains static sorting algorithms.</summary>
	public static class Sort
	{
		#region XML
#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
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
		/// <param name="span">The span to be sorted.</param>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Sort_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
		#endregion

		#region Bubble

		/// <summary>Sorts values using the bubble sort algorithm.</summary>
		/// <runtime>Ω(n), ε(n^2), O(n^2)</runtime>
		/// <stability>True</stability>
		/// <memory>O(1)</memory>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Bubble_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="Bubble_XML"/>
		public static void Bubble<T>(T[] array, Func<T, T, CompareResult> compare = null) =>
			Bubble(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Bubble_XML"/>
		public static void Bubble<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			Bubble(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Bubble_XML"/>
		public static void Bubble<T>(T[] array, int start, int end, Func<T, T, CompareResult> compare = null) =>
			Bubble<T, FuncRuntime<T, T, CompareResult>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Comparison, array, array);

		/// <inheritdoc cref="Bubble_XML"/>
		public static void Bubble<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			Bubble<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <inheritdoc cref="Bubble_XML"/>
		public static void Bubble<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult> compare = null) =>
			Bubble<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Comparison, get, set);

		/// <inheritdoc cref="Bubble_XML"/>
		public static void Bubble<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			for (int i = start; i <= end; i++)
			{
				for (int j = start; j <= end - 1; j++)
				{
					if (compare.Do(get.Do(j), get.Do(j + 1)) is Greater)
					{
						// Swap
						T temp = get.Do(j + 1);
						set.Do(j + 1, get.Do(j));
						set.Do(j, temp);
					}
				}
			}
		}

		/// <inheritdoc cref="Bubble_XML"/>
		public static void Bubble<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			for (int i = 0; i <= span.Length - 1; i++)
			{
				for (int j = 0; j <= span.Length - 2; j++)
				{
					if (compare.Do(span[j], span[j + 1]) is Greater)
					{
						Swap(ref span[j], ref span[j + 1]);
					}
				}
			}
		}

		#endregion

		#region Selection

		/// <summary>Sorts values using the selection sort algoritm.</summary>
		/// <runtime>Ω(n^2), ε(n^2), O(n^2)</runtime>
		/// <stability>False</stability>
		/// <memory>O(1)</memory>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Selection_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="Selection_XML"/>
		public static void Selection<T>(T[] array, Func<T, T, CompareResult> compare = null) =>
			Selection(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Selection_XML"/>
		public static void Selection<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			Selection(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Selection_XML"/>
		public static void Selection<T>(T[] array, int start, int end, Func<T, T, CompareResult> compare = null) =>
			Selection<T, FuncRuntime<T, T, CompareResult>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Comparison, array, array);

		/// <inheritdoc cref="Selection_XML"/>
		public static void Selection<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			Selection<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <inheritdoc cref="Selection_XML"/>
		public static void Selection<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult> compare = null) =>
			Selection<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Comparison, get, set);

		/// <inheritdoc cref="Selection_XML"/>
		public static void Selection<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			for (int i = start; i <= end; i++)
			{
				int min = i;
				for (int j = i + 1; j <= end; j++)
				{
					if (compare.Do(get.Do(j), get.Do(min)) is Less)
					{
						min = j;
					}
				}
				// Swap
				T temp = get.Do(min);
				set.Do(min, get.Do(i));
				set.Do(i, temp);
			}
		}

		/// <inheritdoc cref="Selection_XML"/>
		public static void Selection<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			for (int i = 0; i <= span.Length - 1; i++)
			{
				int min = i;
				for (int j = i + 1; j <= span.Length - 2; j++)
				{
					if (compare.Do(span[j], span[min]) is Less)
					{
						min = j;
					}
				}
				Swap(ref span[min], ref span[i]);
			}
		}

		#endregion

		#region Insertion

		/// <summary>Sorts values using the insertion sort algorithm.</summary>
		/// <runtime>Ω(n), ε(n^2), O(n^2)</runtime>
		/// <stability>True</stability>
		/// <memory>O(1)</memory>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Insertion_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="Insertion_XML"/>
		public static void Insertion<T>(T[] array, Func<T, T, CompareResult> compare = null) =>
			Insertion(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Insertion_XML"/>
		public static void Insertion<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			Insertion(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Insertion_XML"/>
		public static void Insertion<T>(T[] array, int start, int end, Func<T, T, CompareResult> compare = null) =>
			Insertion<T, FuncRuntime<T, T, CompareResult>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Comparison, array, array);

		/// <inheritdoc cref="Insertion_XML"/>
		public static void Insertion<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			Insertion<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <inheritdoc cref="Insertion_XML"/>
		public static void Insertion<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult> compare = null) =>
			Insertion<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Comparison, get, set);

		/// <inheritdoc cref="Insertion_XML"/>
		public static void Insertion<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
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

		/// <inheritdoc cref="Insertion_XML"/>
		public static void Insertion<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			for (int i = 1; i <= span.Length - 1; i++)
			{
				T temp = span[i];
				int j;
				for (j = i; j > 0 && compare.Do(span[j - 1], temp) is Greater; j--)
				{
					span[j] = span[j - 1];
				}
				span[j] = temp;
			}
		}

		#endregion

		#region Quick

		/// <summary>Sorts values using the quick sort algorithm.</summary>
		/// <runtime>Ω(n*ln(n)), ε(n*ln(n)), O(n^2)</runtime>
		/// <stability>False</stability>
		/// <memory>ln(n)</memory>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Quick_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="Quick_XML"/>
		public static void Quick<T>(T[] array, Func<T, T, CompareResult> compare = null) =>
			Quick(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Quick_XML"/>
		public static void Quick<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			Quick(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Quick_XML"/>
		public static void Quick<T>(T[] array, int start, int end, Func<T, T, CompareResult> compare = null) =>
			Quick<T, FuncRuntime<T, T, CompareResult>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Comparison, array, array);

		/// <inheritdoc cref="Quick_XML"/>
		public static void Quick<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			Quick<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <inheritdoc cref="Quick_XML"/>
		public static void Quick<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult> compare = null) =>
			Quick<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Comparison, get, set);

		/// <inheritdoc cref="Quick_XML"/>
		public static void Quick<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			Quick_Recursive(start, end - start + 1);

			void Quick_Recursive(int start, int length)
			{
				if (length > 1)
				{
					T pivot = get.Do(start);
					int i = start;
					int j = start + length - 1;
					int k = j;
					while (i <= j)
					{
						if (compare.Do(get.Do(j), pivot) is Less)
						{
							Swap(i++, j);
						}
						else if (compare.Do(get.Do(j), pivot) is Equal)
						{
							j--;
						}
						else
						{
							Swap(k--, j--);
						}
					}
					Quick_Recursive(start, i - start);
					Quick_Recursive(k + 1, start + length - (k + 1));
				}
			}
			void Swap(int a, int b)
			{
				T temp = get.Do(a);
				set.Do(a, get.Do(b));
				set.Do(b, temp);
			}
		}

		/// <inheritdoc cref="Quick_XML"/>
		public static void Quick<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			Quick_Recursive(span, 0, span.Length);

			void Quick_Recursive(Span<T> span, int startIndex, int len)
			{
				if (len > 1)
				{
					T pivot = span[startIndex];
					int i = startIndex;
					int j = startIndex + len - 1;
					int k = j;
					while (i <= j)
					{
						if (compare.Do(span[j], pivot) is Less)
						{
							Swap(ref span[i++], ref span[j]);
						}
						else if (compare.Do(span[j], pivot) is Equal)
						{
							j--;
						}
						else
						{
							Swap(ref span[k--], ref span[j--]);
						}
					}
					Quick_Recursive(span, startIndex, i - startIndex);
					Quick_Recursive(span, k + 1, startIndex + len - (k + 1));
				}
			}
		}

		#endregion

		#region Merge

		/// <summary>Sorts values using the merge sort algorithm.</summary>
		/// <runtime>Ω(n*ln(n)), ε(n*ln(n)), O(n*ln(n))</runtime>
		/// <stability>True</stability>
		/// <memory>Θ(n)</memory>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Merge_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="Merge_XML"/>
		public static void Merge<T>(T[] array, Func<T, T, CompareResult> compare = null) =>
			Merge(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Merge_XML"/>
		public static void Merge<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			Merge(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Merge_XML"/>
		public static void Merge<T>(T[] array, int start, int end, Func<T, T, CompareResult> compare = null) =>
			Merge<T, FuncRuntime<T, T, CompareResult>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Comparison, array, array);

		/// <inheritdoc cref="Merge_XML"/>
		public static void Merge<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			Merge<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <inheritdoc cref="Merge_XML"/>
		public static void Merge<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult> compare = null) =>
			Merge<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Comparison, get, set);

		/// <inheritdoc cref="Merge_XML"/>
		public static void Merge<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			Merge_Recursive(start, end - start + 1);

			void Merge_Recursive(int start, int length)
			{
				if (length > 1)
				{
					int half = length / 2;
					Merge_Recursive(start, half);
					Merge_Recursive(start + half, length - half);
					T[] sorted = new T[length];
					int i = start;
					int j = start + half;
					int k = 0;
					while (i < start + half && j < start + length)
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
					for (int h = 0; h < start + length - j; h++)
					{
						sorted[k + h] = get.Do(j + h);
					}
					for (int h = 0; h < length; h++)
					{
						set.Do(start + h, sorted[0 + h]);
					}
				}
			}
		}

		/// <inheritdoc cref="Merge_XML"/>
		public static void Merge<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			Merge_Recursive(span);

			void Merge_Recursive(Span<T> span)
			{
				if (span.Length > 1)
				{
					int half = span.Length / 2;
					Merge_Recursive(span[..half]);
					Merge_Recursive(span[half..]);
					T[] sorted = new T[span.Length];
					int i = 0;
					int j = half;
					int k = 0;
					while (i < half && j < span.Length)
					{
						if (compare.Do(span[i], span[j]) is Greater)
						{
							sorted[k++] = span[j++];
						}
						else
						{
							sorted[k++] = span[i++];
						}
					}
					for (int h = 0; h < half - i; h++)
					{
						sorted[k + h] = span[i + h];
					}
					for (int h = 0; h < span.Length - j; h++)
					{
						sorted[k + h] = span[j + h];
					}
					for (int h = 0; h < span.Length; h++)
					{
						span[h] = sorted[0 + h];
					}
				}
			}
		}

		#endregion

		#region Heap

		/// <summary>Sorts values using the heap sort algorithm.</summary>
		/// <runtime>Ω(n*ln(n)), ε(n*ln(n)), O(n^2)</runtime>
		/// <stability>False</stability>
		/// <memory>O(1)</memory>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Heap_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="Heap_XML"/>
		public static void Heap<T>(T[] array, Func<T, T, CompareResult> compare = null) =>
			Heap(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Heap_XML"/>
		public static void Heap<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			Heap(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Heap_XML"/>
		public static void Heap<T>(T[] array, int start, int end, Func<T, T, CompareResult> compare = null) =>
			Heap<T, FuncRuntime<T, T, CompareResult>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Comparison, array, array);

		/// <inheritdoc cref="Heap_XML"/>
		public static void Heap<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			Heap<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <inheritdoc cref="Heap_XML"/>
		public static void Heap<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult> compare = null) =>
			Heap<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Comparison, get, set);

		/// <inheritdoc cref="Heap_XML"/>
		public static void Heap<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			int heapSize = end - start + 1;
			for (int i = heapSize / 2; i >= 0; i--)
			{
				MaxHeapify(heapSize + start, i, start);
			}
			for (int i = end; i >= start; i--)
			{
				Swap(start, i);
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
					Swap(index, largest);
					MaxHeapify(heapSize, largest - offset, offset);
				}
			}
			void Swap(int a, int b)
			{
				T temp = get.Do(a);
				set.Do(a, get.Do(b));
				set.Do(b, temp);
			}
		}

		/// <inheritdoc cref="Heap_XML"/>
		public static void Heap<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			int start = 0;
			int end = span.Length - 1;
			int heapSize = end - start + 1;
			for (int i = heapSize / 2; i >= 0; i--)
			{
				MaxHeapify(span, heapSize + start, i, start);
			}
			for (int i = end; i >= start; i--)
			{
				Swap(ref span[start], ref span[i]);
				heapSize--;
				MaxHeapify(span, heapSize + start, 0, start);
			}
			void MaxHeapify(Span<T> span, int heapSize, int index, int offset)
			{
				int left = ((index + 1) * 2 - 1) + offset;
				int right = ((index + 1) * 2) + offset;
				index += offset;
				int largest = index;
				if (left < heapSize && compare.Do(span[left], span[largest]) is Greater)
				{
					largest = left;
				}
				if (right < heapSize && compare.Do(span[right], span[largest]) is Greater)
				{
					largest = right;
				}
				if (largest != index)
				{
					Swap(ref span[index], ref span[largest]);
					MaxHeapify(span, heapSize, largest - offset, offset);
				}
			}
		}

		#endregion

		#region OddEven

		/// <summary>Sorts values using the odd even sort algorithm.</summary>
		/// <runtime>Ω(n), ε(n^2), O(n^2)</runtime>
		/// <stability>True</stability>
		/// <memory>O(1)</memory>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void OddEven_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="OddEven_XML"/>
		public static void OddEven<T>(T[] array, Func<T, T, CompareResult> compare = null) =>
			OddEven(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="OddEven_XML"/>
		public static void OddEven<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			OddEven(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="OddEven_XML"/>
		public static void OddEven<T>(T[] array, int start, int end, Func<T, T, CompareResult> compare = null) =>
			OddEven<T, FuncRuntime<T, T, CompareResult>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Comparison, array, array);

		/// <inheritdoc cref="OddEven_XML"/>
		public static void OddEven<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			OddEven<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <inheritdoc cref="OddEven_XML"/>
		public static void OddEven<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult> compare = null) =>
			OddEven<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Comparison, get, set);

		/// <inheritdoc cref="OddEven_XML"/>
		public static void OddEven<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			bool sorted = false;
			while (!sorted)
			{
				sorted = true;
				for (int i = start; i < end; i += 2)
				{
					if (compare.Do(get.Do(i), get.Do(i + 1)) is Greater)
					{
						Swap(i, i + 1);
						sorted = false;
					}
				}
				for (int i = start + 1; i < end; i += 2)
				{
					if (compare.Do(get.Do(i), get.Do(i + 1)) is Greater)
					{
						Swap(i, i + 1);
						sorted = false;
					}
				}
				void Swap(int a, int b)
				{
					T temp = get.Do(a);
					set.Do(a, get.Do(b));
					set.Do(b, temp);
				}
			}
		}

		/// <inheritdoc cref="OddEven_XML"/>
		public static void OddEven<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			bool sorted = false;
			while (!sorted)
			{
				sorted = true;
				for (int i = 0; i < span.Length - 1; i += 2)
				{
					if (compare.Do(span[i], span[i + 1]) is Greater)
					{
						Swap(ref span[i], ref span[i + 1]);
						sorted = false;
					}
				}
				for (int i = 1; i < span.Length - 1; i += 2)
				{
					if (compare.Do(span[i], span[i + 1]) is Greater)
					{
						Swap(ref span[i], ref span[i + 1]);
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
		[Obsolete(TowelConstants.NotIntended, true)]
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
		public static void Shuffle<T>(int start, int end, Func<int, T> get, Action<int, T> set, Random random = null) =>
			Shuffle<T, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, get, set, random);

		/// <inheritdoc cref="Shuffle_XML"/>
		public static void Shuffle<T, Get, Set>(int start, int end, Get get = default, Set set = default, Random random = null)
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			random ??= new Random();
			for (int i = start; i <= end; i++)
			{
				int randomIndex = random.Next(start, end);
				// Swap
				T temp = get.Do(i);
				set.Do(i, get.Do(randomIndex));
				set.Do(randomIndex, temp);
			}
		}

		/// <inheritdoc cref="Shuffle_XML"/>
		public static void Shuffle<T>(Span<T> span, Random random = null)
		{
			random ??= new Random();
			for (int i = 0; i < span.Length; i++)
			{
				int index = random.Next(span.Length);
				Swap(ref span[i], ref span[index]);
			}
		}

		#endregion

		#region Bogo

		/// <summary>Sorts values using the bogo sort algorithm.</summary>
		/// <runtime>Ω(n), ε(n*n!), O(∞)</runtime>
		/// <stability>False</stability>
		/// <memory>O(1)</memory>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Bogo_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="Bogo_XML"/>
		public static void Bogo<T>(T[] array, Func<T, T, CompareResult> compare = null, Random random = null) =>
			Bogo(array, 0, array.Length - 1, compare, random);

		/// <inheritdoc cref="Bogo_XML"/>
		public static void Bogo<T, Compare>(T[] array, Compare compare = default, Random random = null)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			Bogo(array, 0, array.Length - 1, compare, random);

		/// <inheritdoc cref="Bogo_XML"/>
		public static void Bogo<T>(T[] array, int start, int end, Func<T, T, CompareResult> compare = null, Random random = null) =>
			Bogo<T, FuncRuntime<T, T, CompareResult>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Comparison, array, array, random);

		/// <inheritdoc cref="Bogo_XML"/>
		public static void Bogo<T, Compare>(T[] array, int start, int end, Compare compare = default, Random random = null)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			Bogo<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array, random);

		/// <inheritdoc cref="Bogo_XML"/>
		public static void Bogo<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult> compare = null, Random random = null) =>
			Bogo<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Comparison, get, set, random);

		/// <inheritdoc cref="Bogo_XML"/>
		public static void Bogo<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default, Random random = null)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
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

		/// <inheritdoc cref="Bogo_XML"/>
		public static void Bogo<T, Compare>(Span<T> span, Compare compare = default, Random random = null)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			random ??= new Random();
			while (!BogoCheck(span))
			{
				Shuffle<T>(span, random);
			}
			bool BogoCheck(Span<T> span)
			{
				for (int i = 1; i < span.Length; i++)
				{
					if (compare.Do(span[i - 1], span[i]) is Greater)
					{
						return false;
					}
				}
				return true;
			}
		}

		#endregion

		#region Slow

		/// <summary>Sorts values using the slow sort algorithm.</summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Slow_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="Slow_XML"/>
		public static void Slow<T>(T[] array, Func<T, T, CompareResult> compare = null) =>
			Slow(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Slow_XML"/>
		public static void Slow<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			Slow(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Slow_XML"/>
		public static void Slow<T>(T[] array, int start, int end, Func<T, T, CompareResult> compare = null) =>
			Slow<T, FuncRuntime<T, T, CompareResult>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Comparison, array, array);

		/// <inheritdoc cref="Slow_XML"/>
		public static void Slow<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			Slow<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <inheritdoc cref="Slow_XML"/>
		public static void Slow<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult> compare = null) =>
			Slow<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Comparison, get, set);

		/// <inheritdoc cref="Slow_XML"/>
		public static void Slow<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			SlowRecursive(start, end);
			void SlowRecursive(int i, int j)
			{
				if (i >= j)
				{
					return;
				}
				int mid = (i + j) / 2;
				SlowRecursive(i, mid);
				SlowRecursive(mid + 1, j);
				if (compare.Do(get.Do(j), get.Do(mid)) is Less)
				{
					// Swap
					T temp = get.Do(j);
					set.Do(j, get.Do(mid));
					set.Do(mid, temp);
				}
				SlowRecursive(i, j - 1);
			}
		}

		/// <inheritdoc cref="Slow_XML"/>
		public static void Slow<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			SlowRecursive(span, 0, span.Length - 1);
			void SlowRecursive(Span<T> span, int i, int j)
			{
				if (i >= j)
				{
					return;
				}
				int mid = (i + j) / 2;
				SlowRecursive(span, i, mid);
				SlowRecursive(span, mid + 1, j);
				if (compare.Do(span[j], span[mid]) is Less)
				{
					Swap(ref span[j], ref span[mid]);
				}
				SlowRecursive(span, i, j - 1);
			}
		}

		#endregion

		#region Gnome

		/// <summary>Sorts values using the gnome sort algorithm.</summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Gnome_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="Gnome_XML"/>
		public static void Gnome<T>(T[] array, Func<T, T, CompareResult> compare = null) =>
			Gnome(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Gnome_XML"/>
		public static void Gnome<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			Gnome(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Gnome_XML"/>
		public static void Gnome<T>(T[] array, int start, int end, Func<T, T, CompareResult> compare = null) =>
			Gnome<T, FuncRuntime<T, T, CompareResult>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Comparison, array, array);

		/// <inheritdoc cref="Gnome_XML"/>
		public static void Gnome<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			Gnome<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <inheritdoc cref="Gnome_XML"/>
		public static void Gnome<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult> compare = null) =>
			Gnome<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Comparison, get, set);

		/// <inheritdoc cref="Gnome_XML"/>
		public static void Gnome<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
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
					// Swap
					T temp = get.Do(i);
					set.Do(i, get.Do(i - 1));
					set.Do(i - 1, temp);
					i--;
				}
			}
		}

		/// <inheritdoc cref="Gnome_XML"/>
		public static void Gnome<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			int i = 0;
			while (i < span.Length)
			{
				if (i == 0 || compare.Do(span[i], span[i - 1]) != Less)
				{
					i++;
				}
				else
				{
					Swap(ref span[i], ref span[i - 1]);
					i--;
				}
			}
		}

		#endregion

		#region Comb

		/// <summary>Sorts values using the comb sort algorithm.</summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Comb_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="Comb_XML"/>
		public static void Comb<T>(T[] array, Func<T, T, CompareResult> compare = null) =>
			Comb(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Comb_XML"/>
		public static void Comb<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			Comb(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Comb_XML"/>
		public static void Comb<T>(T[] array, int start, int end, Func<T, T, CompareResult> compare = null) =>
			Comb<T, FuncRuntime<T, T, CompareResult>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Comparison, array, array);

		/// <inheritdoc cref="Comb_XML"/>
		public static void Comb<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			Comb<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <inheritdoc cref="Comb_XML"/>
		public static void Comb<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult> compare = null) =>
			Comb<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Comparison, get, set);

		/// <inheritdoc cref="Comb_XML"/>
		public static void Comb<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
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

		/// <inheritdoc cref="Comb_XML"/>
		public static void Comb<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			const double shrink = 1.3;
			int gap = span.Length;
			bool sorted = false;
			while (!sorted)
			{
				gap = (int)(gap / shrink);
				if (gap <= 1)
				{
					gap = 1;
					sorted = true;
				}
				for (int i = 0; i + gap < span.Length; i++)
				{
					if (compare.Do(span[i], span[i + gap]) is Greater)
					{
						Swap(ref span[i], ref span[i + gap]);
						sorted = false;
					}
				}
			}
		}

		#endregion

		#region Shell

		/// <summary>Sorts values using the shell sort algorithm.</summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Shell_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="Shell_XML"/>
		public static void Shell<T>(T[] array, Func<T, T, CompareResult> compare = null) =>
			Shell(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Shell_XML"/>
		public static void Shell<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			Shell(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Shell_XML"/>
		public static void Shell<T>(T[] array, int start, int end, Func<T, T, CompareResult> compare = null) =>
			Shell<T, FuncRuntime<T, T, CompareResult>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Comparison, array, array);

		/// <inheritdoc cref="Shell_XML"/>
		public static void Shell<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			Shell<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <inheritdoc cref="Shell_XML"/>
		public static void Shell<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult> compare = null) =>
			Shell<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Comparison, get, set);

		/// <inheritdoc cref="Shell_XML"/>
		public static void Shell<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			int[] gaps = { 701, 301, 132, 57, 23, 10, 4, 1 };
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

		/// <inheritdoc cref="Shell_XML"/>
		public static void Shell<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			int[] gaps = { 701, 301, 132, 57, 23, 10, 4, 1 };
			foreach (int gap in gaps)
			{
				for (int i = gap; i < span.Length; i++)
				{
					T temp = span[i];
					int j = i;
					for (; j >= gap && compare.Do(span[j - gap], temp) is Greater; j -= gap)
					{
						span[j] = span[j - gap];
					}
					span[j] = temp;
				}
			}
		}

		#endregion

		#region Cocktail

		/// <summary>Sorts values using the shell sort algorithm.</summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Cocktail_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="Cocktail_XML"/>
		public static void Cocktail<T>(T[] array, Func<T, T, CompareResult> compare = null) =>
			Cocktail(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Cocktail_XML"/>
		public static void Cocktail<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			Cocktail(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="Cocktail_XML"/>
		public static void Cocktail<T>(T[] array, int start, int end, Func<T, T, CompareResult> compare = null) =>
			Cocktail<T, FuncRuntime<T, T, CompareResult>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Comparison, array, array);

		/// <inheritdoc cref="Cocktail_XML"/>
		public static void Cocktail<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			Cocktail<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);

		/// <inheritdoc cref="Cocktail_XML"/>
		public static void Cocktail<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult> compare = null) =>
			Cocktail<T, FuncRuntime<T, T, CompareResult>, FuncRuntime<int, T>, ActionRuntime<int, T>>(start, end, compare ?? Comparison, get, set);

		/// <inheritdoc cref="Cocktail_XML"/>
		public static void Cocktail<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
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

		/// <inheritdoc cref="Cocktail_XML"/>
		public static void Cocktail<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			while (true)
			{
				bool swapped = false;
				for (int i = 0; i < span.Length - 1; i++)
				{
					if (compare.Do(span[i], span[i + 1]) is Greater)
					{
						Swap(ref span[i], ref span[i + 1]);
						swapped = true;
					}
				}
				if (!swapped)
				{
					break;
				}
				swapped = false;
				for (int i = span.Length - 2; i >= 0; i--)
				{
					if (compare.Do(span[i], span[i + 1]) is Greater)
					{
						Swap(ref span[i], ref span[i + 1]);
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
