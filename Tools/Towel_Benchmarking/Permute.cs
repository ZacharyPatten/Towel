using BenchmarkDotNet.Attributes;
using Towel;
using static Towel.Syntax;

namespace Towel_Benchmarking
{
	[Value(Program.Name, "Permute")]
	public class Permute_Benchmarks
	{
		[Params(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)]
		public int N;

		int[] array;

		[IterationSetup] public void IterationSetup()
		{
			array = new int[N];
			for (int i = 0; i < N; i++)
				array[i] = i;
		}

		[Benchmark] public void Recursive() => PermuteRecursive(array, () => { });
		[Benchmark] public void Iterative() => PermuteIterative(array, () => { });
	}
}
