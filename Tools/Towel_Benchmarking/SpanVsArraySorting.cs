using BenchmarkDotNet.Attributes;
using System;
using Towel;
using static Towel.Statics;

namespace Towel_Benchmarking
{
	[Tag(Program.Name, "Span vs Array Sorting")]
	public class SpanVsArraySorting
	{
		[Params(10, 1000, 10000)] public int N;

		public int[] Values;

		[IterationSetup]
		public void IterationSetup()
		{
			Values = new int[N];
			Extensions.Iterate(N, i => Values[i] = i);
			Random random = new Random(7);
			Shuffle<int>(Values, random);
		}

		[Benchmark]
		public void ArrayBubbleRunTime() =>
			SortBubbleArray(Values);

		[Benchmark]
		public void ArrayBubbleCompileTime() =>
			SortBubbleArray<int, IntCompare>(Values);

		[Benchmark]
		public void SpanBubbleRunTime() =>
			SortBubble(Values.AsSpan());

		[Benchmark]
		public void SpanBubbleCompileTime() =>
			SortBubble<int, IntCompare>(Values.AsSpan());

		public struct ComparerInt : System.Collections.Generic.IComparer<int>
		{
			public int Compare(int a, int b) => a.CompareTo(b);
		}

		/// <inheritdoc cref="SortBubble_XML"/>
		public static void SortBubbleArray<T>(T[] array, Func<T, T, CompareResult> compare = null) =>
			SortBubbleArray(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="SortBubble_XML"/>
		public static void SortBubbleArray<T, Compare>(T[] array, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			SortBubbleArray(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="SortBubble_XML"/>
		public static void SortBubbleArray<T>(T[] array, int start, int end, Func<T, T, CompareResult> compare = null) =>
			SortBubble<T, FuncRuntime<T, T, CompareResult>, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare ?? Compare, array, array);

		/// <inheritdoc cref="SortBubble_XML"/>
		public static void SortBubbleArray<T, Compare>(T[] array, int start, int end, Compare compare = default)
			where Compare : struct, IFunc<T, T, CompareResult> =>
			SortBubble<T, Compare, GetIndexArray<T>, SetIndexArray<T>>(start, end, compare, array, array);
	}

	public struct GetIndexArray<T> : IFunc<int, T>
	{
		internal T[] Array;
		public T Do(int index) => Array[index];
		public static implicit operator GetIndexArray<T>(T[] array) => new GetIndexArray<T>() { Array = array, };
	}

	public struct SetIndexArray<T> : IAction<int, T>
	{
		internal T[] Array;
		public void Do(int index, T value) => Array[index] = value;
		public static implicit operator SetIndexArray<T>(T[] array) => new SetIndexArray<T>() { Array = array, };
	}
}
