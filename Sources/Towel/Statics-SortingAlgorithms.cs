using System;

namespace Towel
{
	/// <summary>Root type of the static functional methods in Towel.</summary>
	public static partial class Statics
	{
		#region Reverse

		/// <inheritdoc cref="Reverse{T, TGet, TSet}(int, int, TGet, TSet)"/>
		public static void Reverse<T>(int start, int end, Func<int, T> get, Action<int, T> set) =>
			Reverse<T, SFunc<int, T>, SAction<int, T>>(start, end, get, set);

		/// <summary>Reverses the values in an <see cref="int"/> indexed sequence.</summary>
		/// <typeparam name="T">The type of values to reverse.</typeparam>
		/// <typeparam name="TGet">The type of the get function.</typeparam>
		/// <typeparam name="TSet">The type of the set function.</typeparam>
		/// <param name="start">The starting index of the reverse.</param>
		/// <param name="end">The ending index of the reverse.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		public static void Reverse<T, TGet, TSet>(int start, int end, TGet get = default, TSet set = default)
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
		{
			if (start <= end)
			{
				int mid = start + (end - start) / 2;
				for (int a = start, b = end; a <= mid; a++, b--)
				{
					Swap<T, TGet, TSet>(a, b, get, set);
				}
			}
			else
			{
				int mid = start - (start - end) / 2;
				for (int a = start, b = end; a >= mid; a--, b++)
				{
					Swap<T, TGet, TSet>(a, b, get, set);
				}
			}
		}

		#endregion

		#region Shuffle

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name

		/// <summary>
		/// Sorts values into a randomized order.<br/>
		/// Runtime: O(n)<br/>
		/// Memory: O(1)
		/// </summary>
		/// <typeparam name="T">The type of values to shuffle.</typeparam>
		/// <typeparam name="TGet">The type of the get function.</typeparam>
		/// <typeparam name="TSet">The type of the set function.</typeparam>
		/// <param name="start">The starting index of the shuffle.</param>
		/// <param name="end">The ending index of the shuffle.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		/// <param name="random">The random to shuffle with.</param>
		/// <param name="span">The span to shuffle.</param>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_Shuffle() => throw new DocumentationMethodException();

#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		/// <inheritdoc cref="XML_Shuffle"/>
		public static void Shuffle<T>(int start, int end, Func<int, T> get, Action<int, T> set, Random? random = null) =>
			Shuffle<T, SFunc<int, T>, SAction<int, T>>(start, end, get, set, random);

		/// <inheritdoc cref="XML_Shuffle"/>
		public static void Shuffle<T, TGet, TSet>(int start, int end, TGet get = default, TSet set = default, Random? random = null)
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T> =>
			Shuffle<T, TGet, TSet, RandomNextIntMinValueIntMaxValue>(start, end, get, set, random ?? new Random());

		/// <inheritdoc cref="XML_Shuffle"/>
		public static void Shuffle<T, TGet, TSet, TRandom>(int start, int end, TGet get = default, TSet set = default, TRandom random = default)
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
			where TRandom : struct, IFunc<int, int, int>
		{
			for (int i = start; i <= end; i++)
			{
				int randomIndex = random.Invoke(start, end);
				Swap<T, TGet, TSet>(i, randomIndex, get, set);
			}
		}

		/// <inheritdoc cref="XML_Shuffle"/>
		public static void Shuffle<T>(Span<T> span, Random? random = null) =>
			Shuffle<T, RandomNextIntMinValueIntMaxValue>(span, random ?? new Random());

		/// <inheritdoc cref="XML_Shuffle"/>
		public static void Shuffle<T, TRandom>(Span<T> span, TRandom random = default)
			where TRandom : struct, IFunc<int, int, int>
		{
			for (int i = 0; i < span.Length; i++)
			{
				int index = random.Invoke(0, span.Length);
				Swap(ref span[i], ref span[index]);
			}
		}

		#endregion

		#region XML

#pragma warning disable CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning disable SA1604 // Element documentation should have summary

		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="TCompare">The type of compare function.</typeparam>
		/// <typeparam name="TGet">The type of get function.</typeparam>
		/// <typeparam name="TSet">The type of set function.</typeparam>
		/// <param name="compare">The compare function.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <param name="array">The array to be sorted.</param>
		/// <param name="span">The span to be sorted.</param>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_Sort() => throw new DocumentationMethodException();

#pragma warning restore SA1604 // Element documentation should have summary
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning restore CS1711 // XML comment has a typeparam tag, but there is no type parameter by that name

		#endregion

		#region SortBubble

		/// <summary>
		/// Sorts values using the bubble sort algorithm.<br/>
		/// Runtime: Ω(n), ε(n^2), O(n^2)<br/>
		/// Memory: O(1)<br/>
		/// Stable: True
		/// </summary>
		/// <inheritdoc cref="XML_Sort"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_SortBubble() => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_SortBubble"/>
		public static void SortBubble<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortBubble<T, SFunc<int, T>, SAction<int, T>, SFunc<T, T, CompareResult>>(start, end, get, set, compare ?? Compare);

		/// <inheritdoc cref="XML_SortBubble"/>
		public static void SortBubble<T, TGet, TSet, TCompare>(int start, int end, TGet get = default, TSet set = default, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
		{
			for (int i = start; i <= end; i++)
			{
				for (int j = start; j <= end - 1; j++)
				{
					if (compare.Invoke(get.Invoke(j), get.Invoke(j + 1)) is Greater)
					{
						Swap<T, TGet, TSet>(j + 1, j, get, set);
					}
				}
			}
		}

		/// <inheritdoc cref="XML_SortBubble"/>
		public static void SortBubble<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortBubble<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="XML_SortBubble"/>
		public static void SortBubble<T, TCompare>(Span<T> span, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
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
		/// Sorts values using the selection sort algoritm.<br/>
		/// Runtime: Ω(n^2), ε(n^2), O(n^2)<br/>
		/// Memory: O(1)<br/>
		/// Stable: False
		/// </summary>
		/// <inheritdoc cref="XML_Sort"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_SortSelection() => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_SortSelection"/>
		public static void SortSelection<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortSelection<T, SFunc<int, T>, SAction<int, T>, SFunc<T, T, CompareResult>>(start, end, get, set, compare ?? Compare);

		/// <inheritdoc cref="XML_SortSelection"/>
		public static void SortSelection<T, TGet, TSet, TCompare>(int start, int end, TGet get = default, TSet set = default, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
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
				Swap<T, TGet, TSet>(min, i, get, set);
			}
		}

		/// <inheritdoc cref="XML_SortSelection"/>
		public static void SortSelection<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortSelection<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="XML_SortSelection"/>
		public static void SortSelection<T, TCompare>(Span<T> span, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
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
		/// Sorts values using the insertion sort algorithm.<br/>
		/// Runtime: Ω(n), ε(n^2), O(n^2)<br/>
		/// Memory: O(1)<br/>
		/// Stable: True
		/// </summary>
		/// <inheritdoc cref="XML_Sort"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_SortInsertion() => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_SortInsertion"/>
		public static void SortInsertion<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortInsertion<T, SFunc<int, T>, SAction<int, T>, SFunc<T, T, CompareResult>>(start, end, get, set, compare ?? Compare);

		/// <inheritdoc cref="XML_SortInsertion"/>
		public static void SortInsertion<T, TGet, TSet, TCompare>(int start, int end, TGet get = default, TSet set = default, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
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

		/// <inheritdoc cref="XML_SortInsertion"/>
		public static void SortInsertion<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortInsertion<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="XML_SortInsertion"/>
		public static void SortInsertion<T, TCompare>(Span<T> span, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
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
		/// Sorts values using the quick sort algorithm.<br/>
		/// Runtime: Ω(n*ln(n)), ε(n*ln(n)), O(n^2)<br/>
		/// Memory: ln(n)<br/>
		/// Stable: False
		/// </summary>
		/// <inheritdoc cref="XML_Sort"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_SortQuick() => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_SortQuick"/>
		public static void SortQuick<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortQuick<T, SFunc<int, T>, SAction<int, T>, SFunc<T, T, CompareResult>>(start, end, get, set, compare ?? Compare);

		/// <inheritdoc cref="XML_SortQuick"/>
		public static void SortQuick<T, TGet, TSet, TCompare>(int start, int end, TGet get = default, TSet set = default, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
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
							Swap<T, TGet, TSet>(i++, j, get, set);
						}
						else if (compare.Invoke(get.Invoke(j), pivot) is Equal)
						{
							j--;
						}
						else
						{
							Swap<T, TGet, TSet>(k--, j--, get, set);
						}
					}
					SortQuick_Recursive(start, i - start);
					SortQuick_Recursive(k + 1, start + length - (k + 1));
				}
			}
		}

		/// <inheritdoc cref="XML_SortQuick"/>
		public static void SortQuick<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortQuick<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="XML_SortQuick"/>
		public static void SortQuick<T, TCompare>(Span<T> span, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
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
		/// Sorts values using the merge sort algorithm.<br/>
		/// Runtime: Ω(n*ln(n)), ε(n*ln(n)), O(n*ln(n))<br/>
		/// Memory: Θ(n)<br/>
		/// Stable: True
		/// </summary>
		/// <inheritdoc cref="XML_Sort"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_SortMerge() => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_SortMerge"/>
		public static void SortMerge<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortMerge<T, SFunc<int, T>, SAction<int, T>, SFunc<T, T, CompareResult>>(start, end, get, set, compare ?? Compare);

		/// <inheritdoc cref="XML_SortMerge"/>
		public static void SortMerge<T, TGet, TSet, TCompare>(int start, int end, TGet get = default, TSet set = default, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
		{
			SortMerge_Recursive(start, end - start + 1);

			void SortMerge_Recursive(int start, int length)
			{
				if (length > 1)
				{
					int half = length / 2;
					SortMerge_Recursive(start, half);
					SortMerge_Recursive(start + half, length - half);
					Span<T> sorted = new T[length];
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

		/// <inheritdoc cref="XML_SortMerge"/>
		public static void SortMerge<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortMerge<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="XML_SortMerge"/>
		public static void SortMerge<T, TCompare>(Span<T> span, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
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
		/// Sorts values using the heap sort algorithm.<br/>
		/// Runtime: Ω(n*ln(n)), ε(n*ln(n)), O(n^2)<br/>
		/// Memory: O(1)<br/>
		/// Stable: False
		/// </summary>
		/// <inheritdoc cref="XML_Sort"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_SortHeap() => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_SortHeap"/>
		public static void SortHeap<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortHeap<T, SFunc<int, T>, SAction<int, T>, SFunc<T, T, CompareResult>>(start, end, get, set, compare ?? Compare);

		/// <inheritdoc cref="XML_SortHeap"/>
		public static void SortHeap<T, TGet, TSet, TCompare>(int start, int end, TGet get = default, TSet set = default, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
		{
			int heapSize = end - start + 1;
			for (int i = heapSize / 2; i >= 0; i--)
			{
				MaxSortHeapify(heapSize + start, i, start);
			}
			for (int i = end; i >= start; i--)
			{
				Swap<T, TGet, TSet>(start, i, get, set);
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
					Swap<T, TGet, TSet>(index, largest, get, set);
					MaxSortHeapify(heapSize, largest - offset, offset);
				}
			}
		}

		/// <inheritdoc cref="XML_SortHeap"/>
		public static void SortHeap<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortHeap<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="XML_SortHeap"/>
		public static void SortHeap<T, TCompare>(Span<T> span, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
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
		/// Sorts values using the odd even sort algorithm.<br/>
		/// Runtime: Ω(n), ε(n^2), O(n^2)<br/>
		/// Memory: O(1)<br/>
		/// Stable: True
		/// </summary>
		/// <inheritdoc cref="XML_Sort"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_SortOddEven() => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_SortOddEven"/>
		public static void SortOddEven<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortOddEven<T, SFunc<int, T>, SAction<int, T>, SFunc<T, T, CompareResult>>(start, end, get, set, compare ?? Compare);

		/// <inheritdoc cref="XML_SortOddEven"/>
		public static void SortOddEven<T, TGet, TSet, TCompare>(int start, int end, TGet get = default, TSet set = default, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
		{
			bool sorted = false;
			while (!sorted)
			{
				sorted = true;
				for (int i = start; i < end; i += 2)
				{
					if (compare.Invoke(get.Invoke(i), get.Invoke(i + 1)) is Greater)
					{
						Swap<T, TGet, TSet>(i, i + 1, get, set);
						sorted = false;
					}
				}
				for (int i = start + 1; i < end; i += 2)
				{
					if (compare.Invoke(get.Invoke(i), get.Invoke(i + 1)) is Greater)
					{
						Swap<T, TGet, TSet>(i, i + 1, get, set);
						sorted = false;
					}
				}
			}
		}

		/// <inheritdoc cref="XML_SortOddEven"/>
		public static void SortOddEven<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortOddEven<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="XML_SortOddEven"/>
		public static void SortOddEven<T, TCompare>(Span<T> span, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
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

		#region SortBogo

		/// <summary>
		/// Sorts values using the bogo sort algorithm.<br/>
		/// Runtime: Ω(n), ε(n*n!), O(∞)<br/>
		/// Memory: O(1)<br/>
		/// Stable: False
		/// </summary>
		/// <inheritdoc cref="XML_Sort"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_SortBogo() => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_SortBogo"/>
		public static void SortBogo<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null, Random? random = null) =>
			SortBogo<T, SFunc<int, T>, SAction<int, T>, SFunc<T, T, CompareResult>>(start, end, get, set, compare ?? Compare, random);

		/// <inheritdoc cref="XML_SortBogo"/>
		public static void SortBogo<T, TGet, TSet, TCompare>(int start, int end, TGet get = default, TSet set = default, TCompare compare = default, Random? random = null)
			where TCompare : struct, IFunc<T, T, CompareResult>
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T> =>
			SortBogo<T, TGet, TSet, TCompare, RandomNextIntMinValueIntMaxValue>(start, end, get, set, compare, random ?? new Random());

		/// <inheritdoc cref="XML_SortBogo"/>
		public static void SortBogo<T, TGet, TSet, TCompare, TRandom>(int start, int end, TGet get = default, TSet set = default, TCompare compare = default, TRandom random = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
			where TRandom : struct, IFunc<int, int, int>
		{
			while (!IsOrdered<T, TCompare, TGet>(start, end, compare, get))
			{
				Shuffle<T, TGet, TSet, TRandom>(start, end, get, set, random);
			}
		}

		/// <inheritdoc cref="XML_SortBogo"/>
		public static void SortBogo<T>(Span<T> span, Func<T, T, CompareResult>? compare = null, Random? random = null) =>
			SortBogo<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare, random ?? new Random());

		/// <inheritdoc cref="XML_SortBogo"/>
		public static void SortBogo<T, TCompare>(Span<T> span, TCompare compare = default, Random? random = null)
			where TCompare : struct, IFunc<T, T, CompareResult> =>
			SortBogo<T, TCompare, RandomNextIntMinValueIntMaxValue>(span, compare, random ?? new Random());

		/// <inheritdoc cref="XML_SortBogo"/>
		public static void SortBogo<T, TCompare, TRandom>(Span<T> span, TCompare compare = default, TRandom random = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
			where TRandom : struct, IFunc<int, int, int>
		{
			while (!IsOrdered<T, TCompare>(span, compare))
			{
				Shuffle(span, random);
			}
		}

		#endregion

		#region SortSlow

		/// <summary>Sorts values using the slow sort algorithm.</summary>
		/// <inheritdoc cref="XML_Sort"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_SortSlow() => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_SortSlow"/>
		public static void SortSlow<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortSlow<T, SFunc<int, T>, SAction<int, T>, SFunc<T, T, CompareResult>>(start, end, get, set, compare ?? Compare);

		/// <inheritdoc cref="XML_SortSlow"/>
		public static void SortSlow<T, TGet, TSet, TCompare>(int start, int end, TGet get = default, TSet set = default, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
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
					Swap<T, TGet, TSet>(j, mid, get, set);
				}
				SortSlowRecursive(i, j - 1);
			}
		}

		/// <inheritdoc cref="XML_SortSlow"/>
		public static void SortSlow<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortSlow<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="XML_SortSlow"/>
		public static void SortSlow<T, TCompare>(Span<T> span, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
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
		/// <inheritdoc cref="XML_Sort"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_SortGnome() => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_SortGnome"/>
		public static void SortGnome<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortGnome<T, SFunc<int, T>, SAction<int, T>, SFunc<T, T, CompareResult>>(start, end, get, set, compare ?? Compare);

		/// <inheritdoc cref="XML_SortGnome"/>
		public static void SortGnome<T, TGet, TSet, TCompare>(int start, int end, TGet get = default, TSet set = default, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
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
					Swap<T, TGet, TSet>(i, i - 1, get, set);
					i--;
				}
			}
		}

		/// <inheritdoc cref="XML_SortGnome"/>
		public static void SortGnome<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortGnome<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="XML_SortGnome"/>
		public static void SortGnome<T, TCompare>(Span<T> span, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
		{
			int i = 0;
			while (i < span.Length)
			{
				if (i is 0 || compare.Invoke(span[i], span[i - 1]) != Less)
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
		/// <inheritdoc cref="XML_Sort"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_SortComb() => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_SortComb"/>
		public static void SortComb<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortComb<T, SFunc<int, T>, SAction<int, T>, SFunc<T, T, CompareResult>>(start, end, get, set, compare ?? Compare);

		/// <inheritdoc cref="XML_SortComb"/>
		public static void SortComb<T, TGet, TSet, TCompare>(int start, int end, TGet get = default, TSet set = default, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
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
						Swap<T, TGet, TSet>(i, i + gap, get, set);
						sorted = false;
					}
				}
			}
		}

		/// <inheritdoc cref="XML_SortComb"/>
		public static void SortComb<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortComb<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="XML_SortComb"/>
		public static void SortComb<T, TCompare>(Span<T> span, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
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
		/// <inheritdoc cref="XML_Sort"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_SortShell() => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_SortShell"/>
		public static void SortShell<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortShell<T, SFunc<int, T>, SAction<int, T>, SFunc<T, T, CompareResult>>(start, end, get, set, compare ?? Compare);

		/// <inheritdoc cref="XML_SortShell"/>
		public static void SortShell<T, TGet, TSet, TCompare>(int start, int end, TGet get = default, TSet set = default, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
		{
			int[] gaps = { 701, 301, 132, 57, 23, 10, 4, 1 };
			foreach (int gap in gaps)
			{
				for (int i = gap + start; i <= end; i++)
				{
					T temp = get.Invoke(i);
					int j = i;
					for (; j >= gap + start && compare.Invoke(get.Invoke(j - gap), temp) is Greater; j -= gap)
					{
						set.Invoke(j, get.Invoke(j - gap));
					}
					set.Invoke(j, temp);
				}
			}
		}

		/// <inheritdoc cref="XML_SortShell"/>
		public static void SortShell<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortShell<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="XML_SortShell"/>
		public static void SortShell<T, TCompare>(Span<T> span, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
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
		/// <inheritdoc cref="XML_Sort"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_SortCocktail() => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_SortCocktail"/>
		public static void SortCocktail<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortCocktail<T, SFunc<int, T>, SAction<int, T>, SFunc<T, T, CompareResult>>(start, end, compare ?? Compare, get, set);

		/// <inheritdoc cref="XML_SortCocktail"/>
		public static void SortCocktail<T, TGet, TSet, TCompare>(int start, int end, TCompare compare = default, TGet get = default, TSet set = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
		{
			while (true)
			{
				bool swapped = false;
				for (int i = start; i <= end - 1; i++)
				{
					if (compare.Invoke(get.Invoke(i), get.Invoke(i + 1)) is Greater)
					{
						Swap<T, TGet, TSet>(i, i + 1, get, set);
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
						Swap<T, TGet, TSet>(i, i + 1, get, set);
						swapped = true;
					}
				}
				if (!swapped)
				{
					break;
				}
			}
		}

		/// <inheritdoc cref="XML_SortCocktail"/>
		public static void SortCocktail<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortCocktail<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="XML_SortCocktail"/>
		public static void SortCocktail<T, TCompare>(Span<T> span, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
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

		/// <summary>Sorts values using the cycle algorithm.</summary>
		/// <inheritdoc cref="XML_Sort"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_SortCycle() => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_SortCycle"/>
		public static void SortCycle<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortCycle<T, SFunc<int, T>, SAction<int, T>, SFunc<T, T, CompareResult>>(start, end, get, set, compare ?? Compare);

		/// <inheritdoc cref="XML_SortCycle"/>
		public static void SortCycle<T, TGet, TSet, TCompare>(int start, int end, TGet get = default, TSet set = default, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
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

		/// <inheritdoc cref="XML_SortCycle"/>
		public static void SortCycle<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortCycle<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="XML_SortCycle"/>
		public static void SortCycle<T, TCompare>(Span<T> span, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
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

		/// <summary>Sorts values using the pancake algorithm.</summary>
		/// <inheritdoc cref="XML_Sort"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_SortPancake() => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_SortPancake"/>
		public static void SortPancake<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortPancake<T, SFunc<int, T>, SAction<int, T>, SFunc<T, T, CompareResult>>(start, end, get, set, compare ?? Compare);

		/// <inheritdoc cref="XML_SortPancake"/>
		public static void SortPancake<T, TGet, TSet, TCompare>(int start, int end, TGet get = default, TSet set = default, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
		{
			for (int i = end; i > start; i--)
			{
				int index = MaximumIndex<T, TGet, TSet, TCompare>(start, i, get, set, compare);
				if (index != i)
				{
					Reverse<T, TGet, TSet>(start, index, get, set);
					Reverse<T, TGet, TSet>(start, i, get, set);
				}
			}
		}

		/// <inheritdoc cref="XML_SortPancake"/>
		public static void SortPancake<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortPancake<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="XML_SortPancake"/>
		public static void SortPancake<T, TCompare>(Span<T> span, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
		{
			for (int i = span.Length; i > 1; i--)
			{
				int index = MaximumIndex<T, TCompare>(span[0..i], compare);
				if (index != i - 1)
				{
					span[0..(index + 1)].Reverse();
					span[0..i].Reverse();
				}
			}
		}

		#endregion

		#region SortStooge

		/// <summary>Sorts values using the stooge algorithm.</summary>
		/// <inheritdoc cref="XML_Sort"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_SortStooge() => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_SortStooge"/>
		public static void SortStooge<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortStooge<T, SFunc<int, T>, SAction<int, T>, SFunc<T, T, CompareResult>>(start, end, get, set, compare ?? Compare);

		/// <inheritdoc cref="XML_SortPancake"/>
		public static void SortStooge<T, TGet, TSet, TCompare>(int start, int end, TGet get = default, TSet set = default, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
		{
			int n = end - start + 1;
			if (n < 2)
			{
				return;
			}
			if (compare.Invoke(get.Invoke(start), get.Invoke(end)) is Greater)
			{
				Swap<T, TGet, TSet>(start, end, get, set);
			}
			if (n > 2)
			{
				int third = n / 3;
				SortStooge<T, TGet, TSet, TCompare>(start, end - third, get, set, compare);
				SortStooge<T, TGet, TSet, TCompare>(start + third, end, get, set, compare);
				SortStooge<T, TGet, TSet, TCompare>(start, end - third, get, set, compare);
			}
		}

		/// <inheritdoc cref="XML_SortStooge"/>
		public static void SortStooge<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortStooge<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="XML_SortStooge"/>
		public static void SortStooge<T, TCompare>(Span<T> span, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
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

		#region SortTim

		/// <summary>
		/// Sorts values using the Tim sort algorithm.<br/>
		/// Runtime: Ω(n), ε(n*ln(n)), O(n*ln(n))<br/>
		/// Memory: O(n)<br/>
		/// Stable: True
		/// </summary>
		/// <inheritdoc cref="XML_Sort"/>
		[Obsolete(TowelConstants.NotIntended, true)]
		public static void XML_SortTim() => throw new DocumentationMethodException();

		/// <inheritdoc cref="XML_SortHeap"/>
		public static void SortTim<T>(int start, int end, Func<int, T> get, Action<int, T> set, Func<T, T, CompareResult>? compare = null) =>
			SortTim<T, SFunc<int, T>, SAction<int, T>, SFunc<T, T, CompareResult>>(start, end, get, set, compare ?? Compare);

		/// <inheritdoc cref="XML_SortTim"/>
		public static void SortTim<T, TGet, TSet, TCompare>(int start, int end, TGet get = default, TSet set = default, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
		{
			const int block = 32;
			int n = end - start + 1;
			for (int i = start; i < n; i += block)
			{
				SortInsertion<T, TGet, TSet, TCompare>(i, Math.Min(i + block, start + n - 1), get, set, compare);
			}
			for (int size = block; size < n; size *= 2)
			{
				for (int left = start; left < n; left += 2 * size)
				{
					int mid = left + size - 1;
					int right = Math.Min(left + 2 * size, n + start) - 1;
					if (mid < right)
					{
						// Merge
						Span<T> a = new T[mid - left + 1];
						Span<T> b = new T[right - mid];
						for (int i = 0; i < a.Length; i++)
						{
							a[i] = get.Invoke(left + i);
						}
						for (int i = 0; i < b.Length; i++)
						{
							b[i] = get.Invoke(mid + 1 + i);
						}
						int ai = 0;    // index of a
						int bi = 0;    // index of b
						int si = left; // index of span
						for (; ai < a.Length && bi < b.Length; si++)
						{
							if (compare.Invoke(a[ai], b[bi]) is not Greater)
							{
								set.Invoke(si, a[ai]);
								ai++;
							}
							else
							{
								set.Invoke(si, b[bi]);
								bi++;
							}
						}
						for (; ai < a.Length; si++, ai++)
						{
							set.Invoke(si, a[ai]);
						}
						for (; bi < b.Length; si++, bi++)
						{
							set.Invoke(si, b[bi]);
						}
					}
				}
			}
		}

		/// <inheritdoc cref="XML_SortTim"/>
		public static void SortTim<T>(Span<T> span, Func<T, T, CompareResult>? compare = null) =>
			SortTim<T, SFunc<T, T, CompareResult>>(span, compare ?? Compare);

		/// <inheritdoc cref="XML_SortTim"/>
		public static void SortTim<T, TCompare>(Span<T> span, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult>
		{
			const int block = 32;
			for (int i = 0; i < span.Length; i += block)
			{
				SortInsertion(span[i..Math.Min(i + block, span.Length)], compare);
			}
			for (int size = block; size < span.Length; size *= 2)
			{
				for (int left = 0; left < span.Length; left += 2 * size)
				{
					int mid = left + size - 1;
					int right = Math.Min(left + 2 * size, span.Length);
					if (mid < right)
					{
						// Merge
						Span<T> a = span[left..(mid + 1)].ToArray();
						Span<T> b = span[(mid + 1)..right].ToArray();
						int ai = 0;    // index of a
						int bi = 0;    // index of b
						int si = left; // index of span
						for (; ai < a.Length && bi < b.Length; si++)
						{
							if (compare.Invoke(a[ai], b[bi]) is not Greater)
							{
								span[si] = a[ai];
								ai++;
							}
							else
							{
								span[si] = b[bi];
								bi++;
							}
						}
						for (; ai < a.Length; si++, ai++)
						{
							span[si] = a[ai];
						}
						for (; bi < b.Length; si++, bi++)
						{
							span[si] = b[bi];
						}
					}
				}
			}
		}

		#endregion

		#region SortCounting

		/// <inheritdoc cref="SortCounting{T, TSelect, TGet, TSet}(int, int, TSelect, TGet, TSet)"/>
		public static void SortCounting(int start, int end, Func<int, uint> get, Action<int, uint> set)
		{
			if (get is null) throw new ArgumentNullException(nameof(get));
			if (set is null) throw new ArgumentNullException(nameof(set));
			SortCounting<uint, Identity<uint>, SFunc<int, uint>, SAction<int, uint>>(start, end, default, get, set);
		}

		/// <inheritdoc cref="SortCounting{T, TSelect, TGet, TSet}(int, int, TSelect, TGet, TSet)"/>
		public static void SortCounting<T>(int start, int end, Func<T, uint> select, Func<int, T> get, Action<int, T> set)
		{
			if (select is null) throw new ArgumentNullException(nameof(select));
			if (get is null) throw new ArgumentNullException(nameof(get));
			if (set is null) throw new ArgumentNullException(nameof(set));
			SortCounting<T, SFunc<T, uint>, SFunc<int, T>, SAction<int, T>>(start, end, select, get, set);
		}

		/// <inheritdoc cref="SortCounting{T, TSelect, TGet, TSet}(int, int, TSelect, TGet, TSet)"/>
		public static void SortCounting<TGet, TSet>(int start, int end, TGet get = default, TSet set = default)
			where TGet : struct, IFunc<int, uint>
			where TSet : struct, IAction<int, uint> =>
			SortCounting<uint, Identity<uint>, TGet, TSet>(start, end, default, get, set);

		/// <summary>
		/// Sorts values using the counting sort algorithm.<br/>
		/// Runtime: Θ(Max(<paramref name="select"/>(n)))<br/>
		/// Memory: Θ(Max(<paramref name="select"/>(n)))<br/>
		/// Stable: True
		/// </summary>
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="TSelect">The type of method for selecting an <see cref="int"/> values from <typeparamref name="T"/> values.</typeparam>
		/// <typeparam name="TGet">The type of get function.</typeparam>
		/// <typeparam name="TSet">The type of set function.</typeparam>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <param name="select">The method for selecting <see cref="int"/> values from <typeparamref name="T"/> values.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		public static void SortCounting<T, TSelect, TGet, TSet>(int start, int end, TSelect select = default, TGet get = default, TSet set = default)
			where TSelect : struct, IFunc<T, uint>
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
		{
			if (end - start + 1 < 2)
			{
				return;
			}
			Span<T> sortingSpan = new T[end - start + 1];
			uint max = 0;
			for (int i = start; i <= end; i++)
			{
				uint temp = select.Invoke(get.Invoke(i));
				max = Math.Max(max, temp);
			}
			SortCounting(start, end, select, get, set, sortingSpan, max);
		}

		internal static void SortCounting<T, TSelect, TGet, TSet>(int start, int end, TSelect select, TGet get, TSet set, Span<T> sortingSpan, uint max)
			where TSelect : struct, IFunc<T, uint>
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
		{
			if (end - start + 1 != sortingSpan.Length)
			{
				throw new TowelBugException(message: $"{nameof(end)} - {nameof(start)} + 1 != {nameof(sortingSpan)}.{nameof(sortingSpan.Length)}");
			}
			int[] count = new int[max + 1];
			for (int i = start; i <= end; i++)
			{
				count[select.Invoke(get.Invoke(i))]++;
			}
			for (int i = 1; i <= max; i++)
			{
				count[i] += count[i - 1];
			}
			for (int i = end; i >= start; i--)
			{
				sortingSpan[count[select.Invoke(get.Invoke(i))] - 1] = get.Invoke(i);
				count[select.Invoke(get.Invoke(i))]--;
			}
			for (int a = start, b = 0; a <= end; a++, b++)
			{
				set.Invoke(a, sortingSpan[b]);
			}
		}

		/// <inheritdoc cref="SortCounting{T, TGetKey}(Span{T}, TGetKey)"/>
		public static void SortCounting(Span<uint> span) =>
			SortCounting<uint, Identity<uint>>(span);

		/// <inheritdoc cref="SortCounting{T, TGetKey}(Span{T}, TGetKey)"/>
		public static void SortCounting<T>(Span<T> span, Func<T, uint> select) =>
			SortCounting<T, SFunc<T, uint>>(span, select);

		/// <summary>
		/// Sorts values using the counting sort algorithm.<br/>
		/// Runtime: Θ(Max(<paramref name="select"/>(<paramref name="span"/>)))<br/>
		/// Memory: Θ(Max(<paramref name="select"/>(<paramref name="span"/>)))<br/>
		/// Stable: True
		/// </summary>
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="TSelect">The type of method for selecting an <see cref="int"/> values from <typeparamref name="T"/> values.</typeparam>
		/// <param name="span">The span to be sorted.</param>
		/// <param name="select">The method for selecting <see cref="int"/> values from <typeparamref name="T"/> values.</param>
		public static void SortCounting<T, TSelect>(Span<T> span, TSelect select = default)
			where TSelect : struct, IFunc<T, uint>
		{
			if (span.Length < 2)
			{
				return;
			}
			Span<T> sortingSpan = new T[span.Length];
			uint max = uint.MinValue;
			foreach (T t in span)
			{
				uint temp = select.Invoke(t);
				max = Math.Max(max, temp);
			}
			SortCounting(span, select, sortingSpan, max);
		}

		internal static void SortCounting<T, TSelect>(Span<T> span, TSelect select, Span<T> sortingSpan, uint max)
			where TSelect : struct, IFunc<T, uint>
		{
			if (span.Length != sortingSpan.Length)
			{
				throw new TowelBugException(message: $"{nameof(span)}.{nameof(span.Length)} != {nameof(sortingSpan)}.{nameof(sortingSpan.Length)}");
			}
			int[] count = new int[max + 1];
			for (int i = 0; i < span.Length; i++)
			{
				count[select.Invoke(span[i])]++;
			}
			for (int i = 1; i <= max; i++)
			{
				count[i] += count[i - 1];
			}
			for (int i = span.Length - 1; i >= 0; i--)
			{
				sortingSpan[count[select.Invoke(span[i])] - 1] = span[i];
				count[select.Invoke(span[i])]--;
			}
			sortingSpan.CopyTo(span);
		}

		#endregion

		#region SortRadix

		/// <inheritdoc cref="SortRadix{T, TSelect, TGet, TSet}(int, int, TSelect, TGet, TSet)"/>
		public static void SortRadix(int start, int end, Func<int, uint> get, Action<int, uint> set)
		{
			if (get is null) throw new ArgumentNullException(nameof(get));
			if (set is null) throw new ArgumentNullException(nameof(set));
			SortRadix<uint, Identity<uint>, SFunc<int, uint>, SAction<int, uint>>(start, end, default, get, set);
		}

		/// <inheritdoc cref="SortRadix{T, TSelect, TGet, TSet}(int, int, TSelect, TGet, TSet)"/>
		public static void SortRadix<T>(int start, int end, Func<T, uint> select, Func<int, T> get, Action<int, T> set)
		{
			if (select is null) throw new ArgumentNullException(nameof(select));
			if (get is null) throw new ArgumentNullException(nameof(get));
			if (set is null) throw new ArgumentNullException(nameof(set));
			SortRadix<T, SFunc<T, uint>, SFunc<int, T>, SAction<int, T>>(start, end, select, get, set);
		}

		/// <inheritdoc cref="SortRadix{T, TSelect, TGet, TSet}(int, int, TSelect, TGet, TSet)"/>
		public static void SortRadix<TGet, TSet>(int start, int end, TGet get = default, TSet set = default)
			where TGet : struct, IFunc<int, uint>
			where TSet : struct, IAction<int, uint> =>
			SortRadix<uint, Identity<uint>, TGet, TSet>(start, end, default, get, set);

		/// <summary>
		/// Sorts values using the radix sort algorithm.
		/// </summary>
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="TSelect">The type of method for selecting an <see cref="int"/> values from <typeparamref name="T"/> values.</typeparam>
		/// <typeparam name="TGet">The type of get function.</typeparam>
		/// <typeparam name="TSet">The type of set function.</typeparam>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <param name="select">The method for selecting <see cref="int"/> values from <typeparamref name="T"/> values.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		public static void SortRadix<T, TSelect, TGet, TSet>(int start, int end, TSelect select = default, TGet get = default, TSet set = default)
			where TSelect : struct, IFunc<T, uint>
			where TGet : struct, IFunc<int, T>
			where TSet : struct, IAction<int, T>
		{
			if (end - start + 1 < 2)
			{
				return;
			}
			uint max = uint.MinValue;
			for (int i = start; i <= end; i++)
			{
				uint temp = select.Invoke(get.Invoke(i));
				max = Math.Max(max, temp);
			}
			Span<T> sortingSpan = new T[end - start + 1];
			for (uint divisor = 1; max / divisor > 0; divisor *= 10)
			{
				SortCounting<T, SortRadixSelect<T, TSelect>, TGet, TSet>(start, end, new() { Select = select, Divisor = divisor }, get, set, sortingSpan, max);
			}
		}

		/// <inheritdoc cref="SortRadix{T, TGetKey}(Span{T}, TGetKey)"/>
		public static void SortRadix(Span<uint> span) =>
			SortRadix<uint, Identity<uint>>(span);

		/// <summary>
		/// Sorts values using the radix sort algorithm.
		/// </summary>
		/// <typeparam name="T">The type of values to sort.</typeparam>
		/// <typeparam name="TSelect">The type of method for selecting an <see cref="int"/> values from <typeparamref name="T"/> values.</typeparam>
		/// <param name="span">The span to be sorted.</param>
		/// <param name="select">The method for selecting <see cref="int"/> values from <typeparamref name="T"/> values.</param>
		public static void SortRadix<T, TSelect>(Span<T> span, TSelect select = default)
			where TSelect : struct, IFunc<T, uint>
		{
			if (span.Length < 2)
			{
				return;
			}
			uint max = uint.MinValue;
			foreach (T t in span)
			{
				uint temp = select.Invoke(t);
				if (temp < 0)
				{
					throw new ArgumentException(message: $"a value exists in {nameof(span)} where {nameof(select)} is less than 0");
				}
				max = Math.Max(max, temp);
			}
			Span<T> sortingSpan = new T[span.Length];
			for (uint divisor = 1; max / divisor > 0; divisor *= 10)
			{
				SortCounting<T, SortRadixSelect<T, TSelect>>(span, new() { Select = select, Divisor = divisor }, sortingSpan, max);
			}
		}

		internal struct SortRadixSelect<T, TSelect> : IFunc<T, uint>
			where TSelect : struct, IFunc<T, uint>
		{
			internal TSelect Select;
			internal uint Divisor;

			public uint Invoke(T a) => Select.Invoke(a) / Divisor;
		}

		#endregion

		#region SortPidgeonHole

		/// <inheritdoc cref="SortPidgeonHole{TGet, TSet}(int, int, TGet, TSet)"/>
		public static void SortPidgeonHole(int start, int end, Func<int, int> get, Action<int, int> set)
		{
			if (get is null) throw new ArgumentNullException(nameof(get));
			if (set is null) throw new ArgumentNullException(nameof(set));
			SortPidgeonHole<SFunc<int, int>, SAction<int, int>>(start, end, get, set);
		}

		/// <summary>
		/// Sorts values using the pidgeon hole sort algorithm.
		/// </summary>
		/// <typeparam name="TGet">The type of get function.</typeparam>
		/// <typeparam name="TSet">The type of set function.</typeparam>
		/// <param name="start">The starting index of the sort.</param>
		/// <param name="end">The ending index of the sort.</param>
		/// <param name="get">The get function.</param>
		/// <param name="set">The set function.</param>
		public static void SortPidgeonHole<TGet, TSet>(int start, int end, TGet get = default, TSet set = default)
			where TGet : struct, IFunc<int, int>
			where TSet : struct, IAction<int, int>
		{
			if (end - start + 1 < 2)
			{
				return;
			}
			int min = int.MaxValue;
			int max = int.MinValue;
			for (int i = start; i <= end; i++)
			{
				int temp = get.Invoke(i);
				if (i < 0)
				{
					throw new ArgumentException(message: $"a value exists in the sequence that is less than 0");
				}
				max = Math.Max(max, temp);
				min = Math.Min(min, temp);
			}
			if (min < int.MinValue / 2 && max > -(int.MinValue / 2))
			{
				throw new OverflowException($"the range of values in the sequence exceends the range of the int data type");
			}
			Span<int> holes = new int[max - min + 1];
			for (int i = start; i <= end; i++)
			{
				holes[get.Invoke(i) - min]++;
			}
			for (int hole = 0, i = start; hole < holes.Length; hole++)
			{
				for (int j = 0; j < holes[hole]; j++)
				{
					set.Invoke(i++, hole + min);
				}
			}
		}

		/// <summary>
		/// Sorts values using the pidgeon hole sort algorithm.
		/// </summary>
		/// <param name="span">The span to be sorted.</param>
		public static void SortPidgeonHole(Span<int> span)
		{
			if (span.Length < 2)
			{
				return;
			}
			int min = int.MaxValue;
			int max = int.MinValue;
			foreach (int i in span)
			{
				max = Math.Max(max, i);
				min = Math.Min(min, i);
			}
			if (min < int.MinValue / 2 && max > -(int.MinValue / 2))
			{
				throw new OverflowException($"the range of values in {nameof(span)} exceends the range of the int data type");
			}
			Span<int> holes = new int[max - min + 1];
			foreach (int i in span)
			{
				holes[i - min]++;
			}
			for (int hole = 0, i = 0; hole < holes.Length; hole++)
			{
				for (int j = 0; j < holes[hole]; j++)
				{
					span[i++] = hole + min;
				}
			}
		}

		#endregion
	}
}