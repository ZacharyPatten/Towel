using BenchmarkDotNet.Attributes;
using System;
using Towel;
using System.Collections.Generic;
using System.Text;

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

		[Benchmark] public void QuickRunTime() =>
			Towel.Algorithms.Sort.Quick(Values, Compare.Default);

		[Benchmark] public void QuickCompileTime() =>
			Towel.Algorithms.Sort.Quick<int, CompareInt>(Values);

		public struct CompareInt : ICompare<int>
		{
			public CompareResult Do(int a, int b) => Compare.Wrap(a.CompareTo(b));
		}
	}
}
