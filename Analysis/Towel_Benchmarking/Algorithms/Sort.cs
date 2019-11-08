using BenchmarkDotNet.Attributes;
using System;
using Towel;

namespace Towel_Benchmarking.Algorithms
{
	[Benchmarks(Tag.Algorithms, Tag.Sort)]
	public class Sort
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

		[Benchmark] public void BubbleRunTime() =>
			Towel.Algorithms.Sort.Bubble(Values, Compare.Default);

		[Benchmark] public void BubbleCompileTime() =>
			Towel.Algorithms.Sort.Bubble<int, CompareInt>(Values);

		[Benchmark] public void SelectionRunTime() =>
			Towel.Algorithms.Sort.Selection(Values, Compare.Default);

		[Benchmark] public void SelectionCompileTime() =>
			Towel.Algorithms.Sort.Selection<int, CompareInt>(Values);

		[Benchmark] public void InsertionRunTime() =>
			Towel.Algorithms.Sort.Insertion(Values, Compare.Default);

		[Benchmark] public void InsertionCompileTime() =>
			Towel.Algorithms.Sort.Insertion<int, CompareInt>(Values);

		[Benchmark] public void QuickRunTime() =>
			Towel.Algorithms.Sort.Quick(Values, Compare.Default);

		[Benchmark] public void QuickCompileTime() =>
			Towel.Algorithms.Sort.Quick<int, CompareInt>(Values);

		[Benchmark] public void MergeRunTime() =>
			Towel.Algorithms.Sort.Merge(Values, Compare.Default);

		[Benchmark] public void MergeCompileTime() =>
			Towel.Algorithms.Sort.Merge<int, CompareInt>(Values);

		[Benchmark] public void HeapRunTime() =>
			Towel.Algorithms.Sort.Heap(Values, Compare.Default);

		[Benchmark] public void HeapCompileTime() =>
			Towel.Algorithms.Sort.Heap<int, CompareInt>(Values);

		[Benchmark] public void OddEvenRunTime() =>
			Towel.Algorithms.Sort.OddEven(Values, Compare.Default);

		[Benchmark] public void OddEvenCompileTime() =>
			Towel.Algorithms.Sort.OddEven<int, CompareInt>(Values);

		[Benchmark] public void SlowRunTime()
		{
			if (Values.Length > 10)
			{
				throw new Exception("Benchmark fail.");
			}
			Towel.Algorithms.Sort.Slow(Values, Compare.Default);
		}

		[Benchmark] public void SlowCompileTime()
		{
			if (Values.Length > 10)
			{
				throw new Exception("Benchmark fail.");
			}
			Towel.Algorithms.Sort.Slow<int, CompareInt>(Values);
		}

		[Benchmark] public void BogoRunTime()
		{
			if (Values.Length > 10)
			{
				throw new Exception("Benchmark fail.");
			}
			Towel.Algorithms.Sort.Bogo<int>(Values, Compare.Default);
		}

		[Benchmark] public void BogoCompileTime()
		{
			if (Values.Length > 10)
			{
				throw new Exception("Benchmark fail.");
			}
			Towel.Algorithms.Sort.Bogo<int, CompareInt>(Values);
		}

		public struct CompareInt : ICompare<int>
		{
			public CompareResult Do(int a, int b) => Compare.Wrap(a.CompareTo(b));
		}
	}
}
