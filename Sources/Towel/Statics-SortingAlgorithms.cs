using System;

namespace Towel
{
	/// <summary>Root type of the static functional methods in Towel.</summary>
	public static partial class Statics
	{
		#region Shuffle

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
		/// <summary>
		/// Sorts values into a randomized order.
		/// <para>Runtime: O(n)</para>
		/// <para>Memory: O(1)</para>
		/// </summary>
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="Get">The get function.</typeparam>
		/// <typeparam name="Set">The set function.</typeparam>
		/// <param name="start">The starting index of the shuffle.</param>
		/// <param name="end">The ending index of the shuffle.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		/// <param name="random">The random to shuffle with.</param>
		/// <param name="array">The array to shuffle.</param>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void Shuffle_XML() => throw new DocumentationMethodException();
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="Shuffle_XML"/>
		public static void Shuffle<T>(int start, int end, Func<int, T> get, Action<int, T> set, Random? random = null) =>
			Shuffle<T, SFunc<int, T>, SAction<int, T>>(start, end, get, set, random);

		/// <inheritdoc cref="Shuffle_XML"/>
		public static void Shuffle<T, Get, Set>(int start, int end, Get get = default, Set set = default, Random? random = null)
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T> =>
			Shuffle<T, Get, Set, RandomNextIntMinValueIntMaxValue>(start, end, get, set, random ?? new Random());

		/// <inheritdoc cref="Shuffle_XML"/>
		public static void Shuffle<T, Get, Set, Random>(int start, int end, Get get = default, Set set = default, Random random = default)
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
			where Random : struct, IFunc<int, int, int>
		{
			for (int i = start; i <= end; i++)
			{
				int randomIndex = random.Invoke(start, end);
				// Swap
				T temp = get.Invoke(i);
				set.Invoke(i, get.Invoke(randomIndex));
				set.Invoke(randomIndex, temp);
			}
		}

		/// <inheritdoc cref="Shuffle_XML"/>
		public static void Shuffle<T>(Span<T> span, Random? random = null) =>
			Shuffle<T, RandomNextIntMinValueIntMaxValue>(span, random ?? new Random());

		/// <inheritdoc cref="Shuffle_XML"/>
		public static void Shuffle<T, Random>(Span<T> span, Random random = default)
			where Random : struct, IFunc<int, int, int>
		{
			for (int i = 0; i < span.Length; i++)
			{
				int index = random.Invoke(0, span.Length);
				Swap(ref span[i], ref span[index]);
			}
		}

		#endregion

		#region Sort_XML
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

		#region SortBubble

		/// <summary>
		/// Sorts values using the bubble sort algorithm.
		/// <para>Runtime: Ω(n), ε(n^2), O(n^2)</para>
		/// <para>Memory: O(1)</para>
		/// <para>Stable: True</para>
		/// </summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortBubble_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortBubble_XML"/>
		public static void SortBubble<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortBubble<T, SFunc<T, T, CompareResult>, SFunc<int, T>, SAction<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortBubble_XML"/>
		public static void SortBubble<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			for (int i = start; i <= end; i++)
			{
				for (int j = start; j <= end - 1; j++)
				{
					if (compare.Invoke(get.Invoke(j), get.Invoke(j + 1)) is Greater)
					{
						// Swap
						T temp = get.Invoke(j + 1);
						set.Invoke(j + 1, get.Invoke(j));
						set.Invoke(j, temp);
					}
				}
			}
		}

		/// <inheritdoc cref="SortBubble_XML"/>
		public static void SortBubble<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortBubble<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortBubble_XML"/>
		public static void SortBubble<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			for (int i = 0; i <= span.Length - 1; i++)
			{
				for (int j = 0; j <= span.Length - 2; j++)
				{
					if (compare.Invoke(span[j], span[j + 1]) is Greater)
					{
						Swap(ref span[j], ref span[j + 1]);
					}
				}
			}
		}

		#endregion

		#region SortSelection

		/// <summary>
		/// Sorts values using the selection sort algoritm.
		/// <para>Runtime: Ω(n^2), ε(n^2), O(n^2)</para>
		/// <para>Memory: O(1)</para>
		/// <para>Stable: False</para>
		/// </summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortSelection_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortSelection_XML"/>
		public static void SortSelection<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortSelection<T, SFunc<T, T, CompareResult>, SFunc<int, T>, SAction<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortSelection_XML"/>
		public static void SortSelection<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			for (int i = start; i <= end; i++)
			{
				int min = i;
				for (int j = i + 1; j <= end; j++)
				{
					if (compare.Invoke(get.Invoke(j), get.Invoke(min)) is Less)
					{
						min = j;
					}
				}
				// Swap
				T temp = get.Invoke(min);
				set.Invoke(min, get.Invoke(i));
				set.Invoke(i, temp);
			}
		}

		/// <inheritdoc cref="SortSelection_XML"/>
		public static void SortSelection<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortSelection<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortSelection_XML"/>
		public static void SortSelection<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			for (int i = 0; i < span.Length; i++)
			{
				int min = i;
				for (int j = i + 1; j < span.Length; j++)
				{
					if (compare.Invoke(span[j], span[min]) is Less)
					{
						min = j;
					}
				}
				Swap(ref span[min], ref span[i]);
			}
		}

		#endregion

		#region SortInsertion

		/// <summary>
		/// Sorts values using the insertion sort algorithm.
		/// <para>Runtime: Ω(n), ε(n^2), O(n^2)</para>
		/// <para>Memory: O(1)</para>
		/// <para>Stable: True</para>
		/// </summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortInsertion_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortInsertion_XML"/>
		public static void SortInsertion<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortInsertion<T, SFunc<T, T, CompareResult>, SFunc<int, T>, SAction<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortInsertion_XML"/>
		public static void SortInsertion<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			for (int i = start + 1; i <= end; i++)
			{
				T temp = get.Invoke(i);
				int j = i;
				for (; j > start && compare.Invoke(get.Invoke(j - 1), temp) is Greater; j--)
				{
					set.Invoke(j, get.Invoke(j - 1));
				}
				set.Invoke(j, temp);
			}
		}

		/// <inheritdoc cref="SortInsertion_XML"/>
		public static void SortInsertion<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortInsertion<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortInsertion_XML"/>
		public static void SortInsertion<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			for (int i = 1; i <= span.Length - 1; i++)
			{
				T temp = span[i];
				int j;
				for (j = i; j > 0 && compare.Invoke(span[j - 1], temp) is Greater; j--)
				{
					span[j] = span[j - 1];
				}
				span[j] = temp;
			}
		}

		#endregion

		#region SortQuick

		/// <summary>
		/// Sorts values using the quick sort algorithm.
		/// <para>Runtime: Ω(n*ln(n)), ε(n*ln(n)), O(n^2)</para>
		/// <para>Memory: ln(n)</para>
		/// <para>Stable: False</para>
		/// </summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortQuick_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortQuick_XML"/>
		public static void SortQuick<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortQuick<T, SFunc<T, T, CompareResult>, SFunc<int, T>, SAction<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortQuick_XML"/>
		public static void SortQuick<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			SortQuick_Recursive(start, end - start + 1);

			void SortQuick_Recursive(int start, int length)
			{
				if (length > 1)
				{
					T pivot = get.Invoke(start);
					int i = start;
					int j = start + length - 1;
					int k = j;
					while (i <= j)
					{
						if (compare.Invoke(get.Invoke(j), pivot) is Less)
						{
							Swap(i++, j);
						}
						else if (compare.Invoke(get.Invoke(j), pivot) is Equal)
						{
							j--;
						}
						else
						{
							Swap(k--, j--);
						}
					}
					SortQuick_Recursive(start, i - start);
					SortQuick_Recursive(k + 1, start + length - (k + 1));
				}
			}
			void Swap(int a, int b)
			{
				T temp = get.Invoke(a);
				set.Invoke(a, get.Invoke(b));
				set.Invoke(b, temp);
			}
		}

		/// <inheritdoc cref="SortQuick_XML"/>
		public static void SortQuick<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortQuick<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortQuick_XML"/>
		public static void SortQuick<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			SortQuick_Recursive(span, 0, span.Length);

			void SortQuick_Recursive(Span<T> span, int startIndex, int len)
			{
				if (len > 1)
				{
					T pivot = span[startIndex];
					int i = startIndex;
					int j = startIndex + len - 1;
					int k = j;
					while (i <= j)
					{
						if (compare.Invoke(span[j], pivot) is Less)
						{
							Swap(ref span[i++], ref span[j]);
						}
						else if (compare.Invoke(span[j], pivot) is Equal)
						{
							j--;
						}
						else
						{
							Swap(ref span[k--], ref span[j--]);
						}
					}
					SortQuick_Recursive(span, startIndex, i - startIndex);
					SortQuick_Recursive(span, k + 1, startIndex + len - (k + 1));
				}
			}
		}

		#endregion

		#region SortMerge

		/// <summary>
		/// Sorts values using the merge sort algorithm.
		/// <para>Runtime: Ω(n*ln(n)), ε(n*ln(n)), O(n*ln(n))</para>
		/// <para>Memory: Θ(n)</para>
		/// <para>Stable: True</para>
		/// </summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortMerge_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortMerge_XML"/>
		public static void SortMerge<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortMerge<T, SFunc<T, T, CompareResult>, SFunc<int, T>, SAction<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortMerge_XML"/>
		public static void SortMerge<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			SortMerge_Recursive(start, end - start + 1);

			void SortMerge_Recursive(int start, int length)
			{
				if (length > 1)
				{
					int half = length / 2;
					SortMerge_Recursive(start, half);
					SortMerge_Recursive(start + half, length - half);
					T[] sorted = new T[length];
					int i = start;
					int j = start + half;
					int k = 0;
					while (i < start + half && j < start + length)
					{
						if (compare.Invoke(get.Invoke(i), get.Invoke(j)) is Greater)
						{
							sorted[k++] = get.Invoke(j++);
						}
						else
						{
							sorted[k++] = get.Invoke(i++);
						}
					}
					for (int h = 0; h < start + half - i; h++)
					{
						sorted[k + h] = get.Invoke(i + h);
					}
					for (int h = 0; h < start + length - j; h++)
					{
						sorted[k + h] = get.Invoke(j + h);
					}
					for (int h = 0; h < length; h++)
					{
						set.Invoke(start + h, sorted[0 + h]);
					}
				}
			}
		}

		/// <inheritdoc cref="SortMerge_XML"/>
		public static void SortMerge<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortMerge<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortMerge_XML"/>
		public static void SortMerge<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			SortMerge_Recursive(span);

			void SortMerge_Recursive(Span<T> span)
			{
				if (span.Length > 1)
				{
					int half = span.Length / 2;
					SortMerge_Recursive(span[..half]);
					SortMerge_Recursive(span[half..]);
					T[] sorted = new T[span.Length];
					int i = 0;
					int j = half;
					int k = 0;
					while (i < half && j < span.Length)
					{
						if (compare.Invoke(span[i], span[j]) is Greater)
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

		#region SortHeap

		/// <summary>
		/// Sorts values using the heap sort algorithm.
		/// <para>Runtime: Ω(n*ln(n)), ε(n*ln(n)), O(n^2)</para>
		/// <para>Memory: O(1)</para>
		/// <para>Stable: False</para>
		/// </summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortHeap_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortHeap_XML"/>
		public static void SortHeap<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortHeap<T, SFunc<T, T, CompareResult>, SFunc<int, T>, SAction<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortHeap_XML"/>
		public static void SortHeap<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			int heapSize = end - start + 1;
			for (int i = heapSize / 2; i >= 0; i--)
			{
				MaxSortHeapify(heapSize + start, i, start);
			}
			for (int i = end; i >= start; i--)
			{
				Swap(start, i);
				heapSize--;
				MaxSortHeapify(heapSize + start, 0, start);
			}
			void MaxSortHeapify(int heapSize, int index, int offset)
			{
				int left = ((index + 1) * 2 - 1) + offset;
				int right = ((index + 1) * 2) + offset;
				index += offset;
				int largest = index;
				if (left < heapSize && compare.Invoke(get.Invoke(left), get.Invoke(largest)) is Greater)
				{
					largest = left;
				}
				if (right < heapSize && compare.Invoke(get.Invoke(right), get.Invoke(largest)) is Greater)
				{
					largest = right;
				}
				if (largest != index)
				{
					Swap(index, largest);
					MaxSortHeapify(heapSize, largest - offset, offset);
				}
			}
			void Swap(int a, int b)
			{
				T temp = get.Invoke(a);
				set.Invoke(a, get.Invoke(b));
				set.Invoke(b, temp);
			}
		}

		/// <inheritdoc cref="SortHeap_XML"/>
		public static void SortHeap<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortHeap<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortHeap_XML"/>
		public static void SortHeap<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			int start = 0;
			int end = span.Length - 1;
			int heapSize = end - start + 1;
			for (int i = heapSize / 2; i >= 0; i--)
			{
				MaxSortHeapify(span, heapSize + start, i, start);
			}
			for (int i = end; i >= start; i--)
			{
				Swap(ref span[start], ref span[i]);
				heapSize--;
				MaxSortHeapify(span, heapSize + start, 0, start);
			}
			void MaxSortHeapify(Span<T> span, int heapSize, int index, int offset)
			{
				int left = ((index + 1) * 2 - 1) + offset;
				int right = ((index + 1) * 2) + offset;
				index += offset;
				int largest = index;
				if (left < heapSize && compare.Invoke(span[left], span[largest]) is Greater)
				{
					largest = left;
				}
				if (right < heapSize && compare.Invoke(span[right], span[largest]) is Greater)
				{
					largest = right;
				}
				if (largest != index)
				{
					Swap(ref span[index], ref span[largest]);
					MaxSortHeapify(span, heapSize, largest - offset, offset);
				}
			}
		}

		#endregion

		#region SortOddEven

		/// <summary>
		/// Sorts values using the odd even sort algorithm.
		/// <para>Runtime: Ω(n), ε(n^2), O(n^2)</para>
		/// <para>Memory: O(1)</para>
		/// <para>Stable: True</para>
		/// </summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortOddEven_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortOddEven_XML"/>
		public static void SortOddEven<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortOddEven<T, SFunc<T, T, CompareResult>, SFunc<int, T>, SAction<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortOddEven_XML"/>
		public static void SortOddEven<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
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
					if (compare.Invoke(get.Invoke(i), get.Invoke(i + 1)) is Greater)
					{
						Swap(i, i + 1);
						sorted = false;
					}
				}
				for (int i = start + 1; i < end; i += 2)
				{
					if (compare.Invoke(get.Invoke(i), get.Invoke(i + 1)) is Greater)
					{
						Swap(i, i + 1);
						sorted = false;
					}
				}
				void Swap(int a, int b)
				{
					T temp = get.Invoke(a);
					set.Invoke(a, get.Invoke(b));
					set.Invoke(b, temp);
				}
			}
		}

		/// <inheritdoc cref="SortOddEven_XML"/>
		public static void SortOddEven<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortOddEven<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortOddEven_XML"/>
		public static void SortOddEven<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			bool sorted = false;
			while (!sorted)
			{
				sorted = true;
				for (int i = 0; i < span.Length - 1; i += 2)
				{
					if (compare.Invoke(span[i], span[i + 1]) is Greater)
					{
						Swap(ref span[i], ref span[i + 1]);
						sorted = false;
					}
				}
				for (int i = 1; i < span.Length - 1; i += 2)
				{
					if (compare.Invoke(span[i], span[i + 1]) is Greater)
					{
						Swap(ref span[i], ref span[i + 1]);
						sorted = false;
					}
				}
			}
		}

		#endregion

		#region SortCounting

#if false

		///// <summary>Method specifically for computing object keys in the SortCounting Sort algorithm.</summary>
		///// <typeparam name="T">The type of instances in the array to be sorted.</typeparam>
		///// <param name="instance">The instance to compute a counting key for.</param>
		///// <returns>The counting key computed from the provided instance.</returns>
		//public delegate int ComputeSortCountingKey(T instance);

		///// <summary>
		///// Sorts an entire array in non-decreasing order using the heap sort algorithm.
		///// <para>Runtime: Θ(Max(key))</para>
		///// <para>Memory: Max(Key)</para>
		///// <para>Stable: True</para>
		///// </summary>
		///// <typeparam name="T">The type of objects stored within the array.</typeparam>
		///// <param name="computeSortCountingKey">Method specifically for computing object keys in the SortCounting Sort algorithm.</param>
		///// <param name="array">The array to be sorted</param>
		//public static void SortCounting(ComputeSortCountingKey computeSortCountingKey, T[] array)
		//{
		//	throw new System.NotImplementedException();

		//	// This code needs revision and conversion
		//	int[] count = new int[array.Length];
		//	int maxKey = 0;
		//	for (int i = 0; i < array.Length; i++)
		//	{
		//		int key = computeSortCountingKey(array[i]) / array.Length;
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
		//		int key = computeSortCountingKey(array[i]);
		//		output[count[key]] = array[i];
		//		count[computeSortCountingKey(array[i])] += 1;
		//	}
		//}

		//public static void SortCounting(ComputeSortCountingKey computeSortCountingKey, T[] array, int start, int end)
		//{
		//	throw new System.NotImplementedException();
		//}

		//public static void SortCounting(ComputeSortCountingKey computeSortCountingKey, Get<T> get, Assign<T> set, int start, int end)
		//{
		//	throw new System.NotImplementedException();
		//}

#endif

		#endregion

		#region SortBogo

		/// <summary>
		/// Sorts values using the bogo sort algorithm.
		/// <para>Runtime: Ω(n), ε(n*n!), O(∞)</para>
		/// <para>Memory: O(1)</para>
		/// <para>Stable: False</para>
		/// </summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortBogo_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortBogo_XML"/>
		public static void SortBogo<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null, Random? random = null) =>
			SortBogo<T, SFunc<T, T, CompareResult>, SFunc<int, T>, SAction<int, T>>(start, end, compare ?? Compare, get, set, random);

		/// <inheritdoc cref="SortBogo_XML"/>
		public static void SortBogo<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default, Random? random = null)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T> =>
			SortBogo<T, Compare, Get, Set, RandomNextIntMinValueIntMaxValue>(start, end, compare, get, set, random ?? new Random());

		/// <inheritdoc cref="SortBogo_XML"/>
		public static void SortBogo<T, Compare, Get, Set, Random>(int start, int end, Compare compare = default, Get get = default, Set set = default, Random random = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
			where Random : struct, IFunc<int, int, int>
		{
			while (!IsOrdered<T, Compare, Get>(start, end, compare, get))
			{
				Shuffle<T, Get, Set, Random>(start, end, get, set, random);
			}
		}

		/// <inheritdoc cref="SortBogo_XML"/>
		public static void SortBogo<T>(Span<T> span, Func<T, T, CompareResult>? compare = null, Random? random = null) =>
			SortBogo<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare, random ?? new Random());

		/// <inheritdoc cref="SortBogo_XML"/>
		public static void SortBogo<T, Compare>(Span<T> span, Compare compare = default, Random? random = null)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			SortBogo<T, Compare, RandomNextIntMinValueIntMaxValue>(span, compare, random ?? new Random());

		/// <inheritdoc cref="SortBogo_XML"/>
		public static void SortBogo<T, Compare, Random>(Span<T> span, Compare compare = default, Random random = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Random : struct, IFunc<int, int, int>
		{
			while (!IsOrdered<T, Compare>(span, compare))
			{
				Shuffle(span, random);
			}
		}

		#endregion

		#region SortSlow

		/// <summary>Sorts values using the slow sort algorithm.</summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortSlow_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortSlow_XML"/>
		public static void SortSlow<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortSlow<T, SFunc<T, T, CompareResult>, SFunc<int, T>, SAction<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortSlow_XML"/>
		public static void SortSlow<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			SortSlowRecursive(start, end);
			void SortSlowRecursive(int i, int j)
			{
				if (i >= j)
				{
					return;
				}
				int mid = (i + j) / 2;
				SortSlowRecursive(i, mid);
				SortSlowRecursive(mid + 1, j);
				if (compare.Invoke(get.Invoke(j), get.Invoke(mid)) is Less)
				{
					// Swap
					T temp = get.Invoke(j);
					set.Invoke(j, get.Invoke(mid));
					set.Invoke(mid, temp);
				}
				SortSlowRecursive(i, j - 1);
			}
		}

		/// <inheritdoc cref="SortSlow_XML"/>
		public static void SortSlow<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortSlow<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortSlow_XML"/>
		public static void SortSlow<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			SortSlowRecursive(span, 0, span.Length - 1);
			void SortSlowRecursive(Span<T> span, int i, int j)
			{
				if (i >= j)
				{
					return;
				}
				int mid = (i + j) / 2;
				SortSlowRecursive(span, i, mid);
				SortSlowRecursive(span, mid + 1, j);
				if (compare.Invoke(span[j], span[mid]) is Less)
				{
					Swap(ref span[j], ref span[mid]);
				}
				SortSlowRecursive(span, i, j - 1);
			}
		}

		#endregion

		#region SortGnome

		/// <summary>Sorts values using the gnome sort algorithm.</summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortGnome_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortGnome_XML"/>
		public static void SortGnome<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortGnome<T, SFunc<T, T, CompareResult>, SFunc<int, T>, SAction<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortGnome_XML"/>
		public static void SortGnome<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			int i = start;
			while (i <= end)
			{
				if (i == start || compare.Invoke(get.Invoke(i), get.Invoke(i - 1)) != Less)
				{
					i++;
				}
				else
				{
					// Swap
					T temp = get.Invoke(i);
					set.Invoke(i, get.Invoke(i - 1));
					set.Invoke(i - 1, temp);
					i--;
				}
			}
		}

		/// <inheritdoc cref="SortGnome_XML"/>
		public static void SortGnome<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortGnome<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortGnome_XML"/>
		public static void SortGnome<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			int i = 0;
			while (i < span.Length)
			{
				if (i == 0 || compare.Invoke(span[i], span[i - 1]) != Less)
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

		#region SortComb

		/// <summary>Sorts values using the comb sort algorithm.</summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortComb_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortComb_XML"/>
		public static void SortComb<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortComb<T, SFunc<T, T, CompareResult>, SFunc<int, T>, SAction<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortComb_XML"/>
		public static void SortComb<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
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
					if (compare.Invoke(get.Invoke(i), get.Invoke(i + gap)) is Greater)
					{
						T temp = get.Invoke(i);
						set.Invoke(i, get.Invoke(i + gap));
						set.Invoke(i + gap, temp);
						sorted = false;
					}
				}
			}
		}

		/// <inheritdoc cref="SortComb_XML"/>
		public static void SortComb<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortComb<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortComb_XML"/>
		public static void SortComb<T, Compare>(Span<T> span, Compare compare = default)
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
					if (compare.Invoke(span[i], span[i + gap]) is Greater)
					{
						Swap(ref span[i], ref span[i + gap]);
						sorted = false;
					}
				}
			}
		}

		#endregion

		#region SortShell

		/// <summary>Sorts values using the shell sort algorithm.</summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortShell_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortShell_XML"/>
		public static void SortShell<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortShell<T, SFunc<T, T, CompareResult>, SFunc<int, T>, SAction<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortShell_XML"/>
		public static void SortShell<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			int[] gaps = { 701, 301, 132, 57, 23, 10, 4, 1 };
			foreach (int gap in gaps)
			{
				for (int i = gap + start; i <= end; i++)
				{
					T temp = get.Invoke(i);
					int j = i;
					for (; j >= gap && compare.Invoke(get.Invoke(j - gap), temp) is Greater; j -= gap)
					{
						set.Invoke(j, get.Invoke(j - gap));
					}
					set.Invoke(j, temp);
				}
			}
		}

		/// <inheritdoc cref="SortShell_XML"/>
		public static void SortShell<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortShell<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortShell_XML"/>
		public static void SortShell<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			int[] gaps = { 701, 301, 132, 57, 23, 10, 4, 1 };
			foreach (int gap in gaps)
			{
				for (int i = gap; i < span.Length; i++)
				{
					T temp = span[i];
					int j = i;
					for (; j >= gap && compare.Invoke(span[j - gap], temp) is Greater; j -= gap)
					{
						span[j] = span[j - gap];
					}
					span[j] = temp;
				}
			}
		}

		#endregion

		#region SortCocktail

		/// <summary>Sorts values using the cocktail sort algorithm.</summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortCocktail_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortCocktail_XML"/>
		public static void SortCocktail<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortCocktail<T, SFunc<T, T, CompareResult>, SFunc<int, T>, SAction<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortCocktail_XML"/>
		public static void SortCocktail<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			while (true)
			{
				bool swapped = false;
				for (int i = start; i <= end - 1; i++)
				{
					if (compare.Invoke(get.Invoke(i), get.Invoke(i + 1)) is Greater)
					{
						T temp = get.Invoke(i);
						set.Invoke(i, get.Invoke(i + 1));
						set.Invoke(i + 1, temp);
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
					if (compare.Invoke(get.Invoke(i), get.Invoke(i + 1)) is Greater)
					{
						T temp = get.Invoke(i);
						set.Invoke(i, get.Invoke(i + 1));
						set.Invoke(i + 1, temp);
						swapped = true;
					}
				}
				if (!swapped)
				{
					break;
				}
			}
		}

		/// <inheritdoc cref="SortCocktail_XML"/>
		public static void SortCocktail<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortCocktail<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortCocktail_XML"/>
		public static void SortCocktail<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			while (true)
			{
				bool swapped = false;
				for (int i = 0; i < span.Length - 1; i++)
				{
					if (compare.Invoke(span[i], span[i + 1]) is Greater)
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
					if (compare.Invoke(span[i], span[i + 1]) is Greater)
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

		#region SortCycle

		/// <summary>Sorts values using the shell cycle algorithm.</summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortCycle_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortCycle_XML"/>
		public static void SortCycle<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortCycle<T, SFunc<T, T, CompareResult>, SFunc<int, T>, SAction<int, T>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="SortCycle_XML"/>
		public static void SortCycle<T, Compare, Get, Set>(int start, int end, Compare compare = default, Get get = default, Set set = default)
			where Compare : struct, IFunc<T, T, CompareResult>
			where Get : struct, IFunc<int, T>
			where Set : struct, IAction<int, T>
		{
			for (int i = start; i < end; i++)
			{
				T pivot = get.Invoke(i);
				int index = i;
				for (int j = i + 1; j <= end; j++)
				{
					if (compare.Invoke(get.Invoke(j), pivot) is Less)
					{
						index++;
					}
				}
				if (index == i)
				{
					continue;
				}
				while (compare.Invoke(pivot, get.Invoke(index)) is Equal)
				{
					index++;
				}
				if (index != i)
				{
					T temp = pivot;
					pivot = get.Invoke(index);
					set.Invoke(index, temp);
				}
				while (index != i)
				{
					index = i;
					for (int j = i + 1; j <= end; j++)
					{
						if (compare.Invoke(get.Invoke(j), pivot) is Less)
						{
							index++;
						}
					}
					while (compare.Invoke(pivot, get.Invoke(index)) is Equal)
					{
						index += 1;
					}
					if (compare.Invoke(pivot, get.Invoke(index)) is not Equal)
					{
						T temp = pivot;
						pivot = get.Invoke(index);
						set.Invoke(index, temp);
					}
				}
			}
		}

		/// <inheritdoc cref="SortCycle_XML"/>
		public static void SortCycle<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortCycle<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortCycle_XML"/>
		public static void SortCycle<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			for (int i = 0; i < span.Length - 1; i++)
			{
				T pivot = span[i];
				int index = i;
				for (int j = i + 1; j < span.Length; j++)
				{
					if (compare.Invoke(span[j], pivot) is Less)
					{
						index++;
					}
				}
				if (index == i)
				{
					continue;
				}
				while (compare.Invoke(pivot, span[index]) is Equal)
				{
					index++;
				}
				if (index != i)
				{
					Swap(ref span[index], ref pivot);
				}
				while (index != i)
				{
					index = i;
					for (int j = i + 1; j < span.Length; j++)
					{
						if (compare.Invoke(span[j], pivot) is Less)
						{
							index++;
						}
					}
					while (compare.Invoke(pivot, span[index]) is Equal)
					{
						index += 1;
					}
					if (compare.Invoke(pivot, span[index]) is not Equal)
					{
						Swap(ref span[index], ref pivot);
					}
				}
			}
		}

		#endregion

		#region SortPancake

		/// <summary>Sorts values using the shell pancake algorithm.</summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortPancake_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortPancake_XML"/>
		public static void SortPancake<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortPancake<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortPancake_XML"/>
		public static void SortPancake<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			for (int i = span.Length; i > 1; i--)
			{
				var (index, _) = Maximum<T, Compare>(span[0..i], compare);
				if (index != i - 1)
				{
					span[0..(index + 1)].Reverse();
					span[0..i].Reverse();
				}
			}
		}

		#endregion

		#region SortStooge

		/// <summary>Sorts values using the shell stooge algorithm.</summary>
		/// <inheritdoc cref="Sort_XML"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		internal static void SortStooge_XML() => throw new DocumentationMethodException();

		/// <inheritdoc cref="SortStooge_XML"/>
		public static void SortStooge<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortStooge<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="SortStooge_XML"/>
		public static void SortStooge<T, Compare>(Span<T> span, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult>
		{
			if (span.Length < 2)
			{
				return;
			}
			if (compare.Invoke(span[0], span[^1]) is Greater)
			{
				Swap(ref span[0], ref span[^1]);
			}
			if (span.Length > 2)
			{
				int third = span.Length / 3;
				SortStooge(span[third..], compare);
				SortStooge(span[..^third], compare);
				SortStooge(span[third..], compare);
			}
		}

		#endregion
	}
}
