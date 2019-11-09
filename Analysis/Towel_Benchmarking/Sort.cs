using BenchmarkDotNet.Attributes;
using System;
using System.Diagnostics.CodeAnalysis;
using Towel;

namespace Towel_Benchmarking
{
	public class Sort_Benchmarks
	{
		[Params(10, 1000, 10000)] public int N;

		public int[] Values;

		[IterationSetup] public void IterationSetup()
		{
			Values = new int[N];
			Stepper.Iterate(N, i => Values[i] = i);
			Random random = new Random(7);
			random.Shuffle(Values);
		}

		[Benchmark] public void SystemArraySort() =>
			Array.Sort(Values);

		[Benchmark] public void SystemArraySortDelegate() =>
			Array.Sort(Values, (int a, int b) => a.CompareTo(b));

		[Benchmark] public void SystemArraySortIComparer() =>
			Array.Sort(Values, default(ComparerInt));

		[Benchmark] public void BubbleRunTime() =>
			Towel.Sort.Bubble(Values, Compare.Default);

		[Benchmark] public void BubbleCompileTime() =>
			Towel.Sort.Bubble<int, CompareInt>(Values);

		[Benchmark] public void SelectionRunTime() =>
			Towel.Sort.Selection(Values, Compare.Default);

		[Benchmark] public void SelectionCompileTime() =>
			Towel.Sort.Selection<int, CompareInt>(Values);

		[Benchmark] public void InsertionRunTime() =>
			Towel.Sort.Insertion(Values, Compare.Default);

		[Benchmark] public void InsertionCompileTime() =>
			Towel.Sort.Insertion<int, CompareInt>(Values);

		[Benchmark] public void QuickRunTime() =>
			Towel.Sort.Quick(Values, Compare.Default);

		[Benchmark] public void QuickCompileTime() =>
			Towel.Sort.Quick<int, CompareInt>(Values);

		[Benchmark] public void MergeRunTime() =>
			Towel.Sort.Merge(Values, Compare.Default);

		[Benchmark] public void MergeCompileTime() =>
			Towel.Sort.Merge<int, CompareInt>(Values);

		[Benchmark] public void HeapRunTime() =>
			Towel.Sort.Heap(Values, Compare.Default);

		[Benchmark] public void HeapCompileTime() =>
			Towel.Sort.Heap<int, CompareInt>(Values);

		[Benchmark] public void OddEvenRunTime() =>
			Towel.Sort.OddEven(Values, Compare.Default);

		[Benchmark] public void OddEvenCompileTime() =>
			Towel.Sort.OddEven<int, CompareInt>(Values);

		[Benchmark] public void SlowRunTime()
		{
			if (Values.Length > 10)
			{
				throw new Exception("Benchmark fail.");
			}
			Towel.Sort.Slow(Values, Compare.Default);
		}

		[Benchmark] public void SlowCompileTime()
		{
			if (Values.Length > 10)
			{
				throw new Exception("Benchmark fail.");
			}
			Towel.Sort.Slow<int, CompareInt>(Values);
		}

		[Benchmark] public void BogoRunTime()
		{
			if (Values.Length > 10)
			{
				throw new Exception("Benchmark fail.");
			}
			Towel.Sort.Bogo<int>(Values, Compare.Default);
		}

		[Benchmark] public void BogoCompileTime()
		{
			if (Values.Length > 10)
			{
				throw new Exception("Benchmark fail.");
			}
			Towel.Sort.Bogo<int, CompareInt>(Values);
		}

		public struct CompareInt : ICompare<int>
		{
			public CompareResult Do(int a, int b) => Compare.Wrap(a.CompareTo(b));
		}

		public struct ComparerInt : System.Collections.Generic.IComparer<int>
		{
			public int Compare(int a, int b) => a.CompareTo(b);
		}
	}
}
