using BenchmarkDotNet.Attributes;
using System;
using Towel;
using static Towel.Statics;

namespace Towel_Benchmarking
{
	[Tag(Program.Name, "Sorting Algorithms")]
	[Tag(Program.OutputFile, nameof(SortBenchmarks))]
	public class SortBenchmarks
	{
		[Params(10, 1000, 10000)] public int N;

		public int[]? Values;

		[IterationSetup] public void IterationSetup()
		{
			Values = new int[N];
			Extensions.Iterate(N, i => Values[i] = i);
			Random random = new(7);
			Shuffle<int>(Values, random);
		}

		[Benchmark] public void SystemArraySort() => Array.Sort(Values!);

		[Benchmark] public void SystemArraySortDelegate() => Array.Sort(Values!, (int a, int b) => a.CompareTo(b));

		[Benchmark] public void SystemArraySortIComparer() => Array.Sort(Values!, default(ComparerInt));

		[Benchmark] public void BubbleRunTime() => SortBubble<int>(Values);

		[Benchmark] public void BubbleCompileTime() => SortBubble<int, Int32Compare>(Values);

		[Benchmark] public void SelectionRunTime() => SortSelection<int>(Values);

		[Benchmark] public void SelectionCompileTime() => SortSelection<int, Int32Compare>(Values);

		[Benchmark] public void InsertionRunTime() => SortInsertion<int>(Values);

		[Benchmark] public void InsertionCompileTime() => SortInsertion<int, Int32Compare>(Values);

		[Benchmark] public void QuickRunTime() => SortQuick<int>(Values);

		[Benchmark] public void QuickCompileTime() => SortQuick<int, Int32Compare>(Values);

		[Benchmark] public void MergeRunTime() => SortMerge<int>(Values);

		[Benchmark] public void MergeCompileTime() => SortMerge<int, Int32Compare>(Values);

		[Benchmark] public void HeapRunTime() => SortHeap<int>(Values);

		[Benchmark] public void HeapCompileTime() => SortHeap<int, Int32Compare>(Values);

		[Benchmark] public void OddEvenRunTime() => SortOddEven<int>(Values);

		[Benchmark] public void OddEvenCompileTime() => SortOddEven<int, Int32Compare>(Values);

		[Benchmark] public void GnomeRunTime() => SortGnome<int>(Values);

		[Benchmark] public void GnomeCompileTime() => SortGnome<int, Int32Compare>(Values);

		[Benchmark] public void CombRunTime() => SortComb<int>(Values);

		[Benchmark] public void CombCompileTime() => SortComb<int, Int32Compare>(Values);

		[Benchmark] public void ShellRunTime() => SortShell<int>(Values);

		[Benchmark] public void ShellCompileTime() => SortShell<int, Int32Compare>(Values);

		[Benchmark] public void CocktailRunTime() => SortCocktail<int>(Values);

		[Benchmark] public void CocktailCompileTime() => SortCocktail<int, Int32Compare>(Values);

		[Benchmark] public void CycleRunTime() => SortCycle<int>(Values);

		[Benchmark] public void CycleCompileTime() => SortCycle<int, Int32Compare>(Values);

		[Benchmark] public void PancakeRunTime() => SortPancake<int>(Values);

		[Benchmark] public void PancakeCompileTime() => SortPancake<int, Int32Compare>(Values);

		[Benchmark] public void StoogeRunTime()
		{
			if (N > 1000)
			{
				throw new Exception("Too Slow.");
			}
			SortStooge<int>(Values);
		}

		[Benchmark] public void StoogeCompileTime()
		{
			if (N > 1000)
			{
				throw new Exception("Too Slow.");
			}
			SortStooge<int, Int32Compare>(Values);
		}

		[Benchmark] public void SlowRunTime()
		{
			if (N > 10)
			{
				throw new Exception("Too Slow.");
			}
			SortSlow<int>(Values);
		}

		[Benchmark] public void SlowCompileTime()
		{
			if (N > 10)
			{
				throw new Exception("Too Slow.");
			}
			SortSlow<int, Int32Compare>(Values);
		}

		[Benchmark] public void BogoRunTime()
		{
			if (N > 10)
			{
				throw new Exception("Too Slow.");
			}
			SortBogo<int>(Values);
		}

		[Benchmark] public void BogoCompileTime()
		{
			if (N > 10)
			{
				throw new Exception("Too Slow.");
			}
			SortBogo<int, Int32Compare>(Values);
		}

		public struct ComparerInt : System.Collections.Generic.IComparer<int>
		{
			public int Compare(int a, int b) => a.CompareTo(b);
		}
	}
}
