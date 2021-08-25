using System;
using BenchmarkDotNet.Attributes;
using Towel;
using static Towel.Statics;

namespace Towel_Benchmarking
{
	[Tag(Program.Name, "Span vs Array Sorting")]
	[Tag(Program.OutputFile, nameof(SpanVsArraySortingBenchmarks))]
	public class SpanVsArraySortingBenchmarks
	{
		[Params(10, 1000, 10000)]
		public int N;

		public int[]? Values;

		[IterationSetup]
		public void IterationSetup()
		{
			Values = (..N).ToArray();
			Random random = new(7);
			Shuffle<int>(Values, random);
		}

		[Benchmark]
		public void ArrayBubbleRunTime() => SortBubbleArray(Values!);

		[Benchmark]
		public void ArrayBubbleCompileTime() => SortBubbleArray<int, Int32Compare>(Values!);

		[Benchmark]
		public void SpanBubbleRunTime() => SortBubble(Values.AsSpan());

		[Benchmark]
		public void SpanBubbleCompileTime() => SortBubble<int, Int32Compare>(Values.AsSpan());

		public struct ComparerInt : System.Collections.Generic.IComparer<int>
		{
			public int Compare(int a, int b) => a.CompareTo(b);
		}

		/// <inheritdoc cref="XML_SortBubble"/>
		public static void SortBubbleArray<T>(T[] array, Func<T, T, CompareResult>? compare = null) =>
			SortBubbleArray(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="XML_SortBubble"/>
		public static void SortBubbleArray<T, TCompare>(T[] array, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult> =>
			SortBubbleArray(array, 0, array.Length - 1, compare);

		/// <inheritdoc cref="XML_SortBubble"/>
		public static void SortBubbleArray<T>(T[] array, int start, int end, Func<T, T, CompareResult>? compare = null) =>
			SortBubble<T, GetIndexArray<T>, SetIndexArray<T>, SFunc<T, T, CompareResult>>(start, end, array, array, compare ?? Compare);

		/// <inheritdoc cref="XML_SortBubble"/>
		public static void SortBubbleArray<T, TCompare>(T[] array, int start, int end, TCompare compare = default)
			where TCompare : struct, IFunc<T, T, CompareResult> =>
			SortBubble<T, GetIndexArray<T>, SetIndexArray<T>, TCompare>(start, end, array, array, compare);
	}

	public struct GetIndexArray<T> : IFunc<int, T>
	{
		internal T[] Array;
		public T Invoke(int index) => Array[index];
		public static implicit operator GetIndexArray<T>(T[] array) => new() { Array = array, };
	}

	public struct SetIndexArray<T> : IAction<int, T>
	{
		internal T[] Array;
		public void Invoke(int index, T value) => Array[index] = value;
		public static implicit operator SetIndexArray<T>(T[] array) => new() { Array = array, };
	}
}
