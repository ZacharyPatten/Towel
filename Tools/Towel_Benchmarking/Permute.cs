using BenchmarkDotNet.Attributes;
using Towel;

namespace Towel_Benchmarking
{
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

        [Benchmark] public void Recursive() => array.PermuteRecursive(() => { });
        [Benchmark] public void Iterative() => array.PermuteIterative(() => { });
    }
}
