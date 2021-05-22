using BenchmarkDotNet.Attributes;
using System;
using Towel;
using static Towel.Statics;

namespace Towel_Benchmarking
{
	[Tag(Program.Name, "Sorting Algorithms")]
	public class Sort_Benchmarks
	{
		[Params(10, 1000, 10000)] public int N;

		public int[] Values;

		[IterationSetup] public void IterationSetup()
		{
			Values = new int[N];
			Extensions.Iterate(N, i => Values[i] = i);
			Random random = new(7);
			Shuffle<int>(Values, random);
		}

		[Benchmark] public void SystemArraySort() =>
			Array.Sort(Values);

		[Benchmark] public void SystemArraySortDelegate() =>
			Array.Sort(Values, (int a, int b) => a.CompareTo(b));

		[Benchmark] public void SystemArraySortIComparer() =>
			Array.Sort(Values, default(ComparerInt));

		[Benchmark] public void BubbleRunTime() =>
			SortBubble<int>(Values);

		[Benchmark] public void BubbleCompileTime() =>
			SortBubble<int, IntCompare>(Values);

		[Benchmark] public void SelectionRunTime() =>
			SortSelection<int>(Values);

		[Benchmark] public void SelectionCompileTime() =>
			SortSelection<int, IntCompare>(Values);

		[Benchmark] public void InsertionRunTime() =>
			SortInsertion<int>(Values);

		[Benchmark] public void InsertionCompileTime() =>
			SortInsertion<int, IntCompare>(Values);

		[Benchmark] public void QuickRunTime() =>
			SortQuick<int>(Values);

		[Benchmark] public void QuickCompileTime() =>
			SortQuick<int, IntCompare>(Values);

		[Benchmark] public void MergeRunTime() =>
			SortMerge<int>(Values);

		[Benchmark] public void MergeCompileTime() =>
			SortMerge<int, IntCompare>(Values);

		[Benchmark] public void HeapRunTime() =>
			SortHeap<int>(Values);

		[Benchmark] public void HeapCompileTime() =>
			SortHeap<int, IntCompare>(Values);

		[Benchmark] public void OddEvenRunTime() =>
			SortOddEven<int>(Values);

		[Benchmark] public void OddEvenCompileTime() =>
			SortOddEven<int, IntCompare>(Values);

		[Benchmark] public void GnomeRunTime() =>
			SortGnome<int>(Values);

		[Benchmark] public void GnomeCompileTime() =>
			SortGnome<int, IntCompare>(Values);

		[Benchmark] public void CombRunTime() =>
			SortComb<int>(Values);

		[Benchmark] public void CombCompileTime() =>
			SortComb<int, IntCompare>(Values);

		[Benchmark] public void ShellRunTime() =>
			SortShell<int>(Values);

		[Benchmark] public void ShellCompileTime() =>
			SortShell<int, IntCompare>(Values);

		[Benchmark] public void CocktailRunTime() =>
			SortCocktail<int>(Values);

		[Benchmark] public void CocktailCompileTime() =>
			SortCocktail<int, IntCompare>(Values);

		[Benchmark] public void CycleRunTime() =>
			SortCycle<int>(Values);

		[Benchmark] public void CycleCompileTime() =>
			SortCycle<int, IntCompare>(Values);

		[Benchmark] public void PancakeRunTime() =>
			SortPancake<int>(Values);

		[Benchmark] public void PancakeCompileTime() =>
			SortPancake<int, IntCompare>(Values);

		[Benchmark] public void StoogeRunTime()
		{
			if (Values.Length > 1000)
			{
				throw new Exception("Too Slow.");
			}
			SortStooge<int>(Values);
		}

		[Benchmark] public void StoogeCompileTime()
		{
			if (Values.Length > 1000)
			{
				throw new Exception("Too Slow.");
			}
			SortStooge<int, IntCompare>(Values);
		}

		[Benchmark] public void SlowRunTime()
		{
			if (Values.Length > 10)
			{
				throw new Exception("Too Slow.");
			}
			SortSlow<int>(Values);
		}

		[Benchmark] public void SlowCompileTime()
		{
			if (Values.Length > 10)
			{
				throw new Exception("Too Slow.");
			}
			SortSlow<int, IntCompare>(Values);
		}

		[Benchmark] public void BogoRunTime()
		{
			if (Values.Length > 10)
			{
				throw new Exception("Too Slow.");
			}
			SortBogo<int>(Values);
		}

		[Benchmark] public void BogoCompileTime()
		{
			if (Values.Length > 10)
			{
				throw new Exception("Too Slow.");
			}
			SortBogo<int, IntCompare>(Values);
		}

		public struct ComparerInt : System.Collections.Generic.IComparer<int>
		{
			public int Compare(int a, int b) => a.CompareTo(b);
		}
	}
}
