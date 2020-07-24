using BenchmarkDotNet.Running;

namespace Towel_Benchmarking
{
	public class Program
	{
		public static void Main()
		{
			BenchmarkRunner.Run(typeof(Random_Benchmarks));
			BenchmarkRunner.Run(typeof(ToEnglishWords_Benchmarks));
			BenchmarkRunner.Run(typeof(Permute_Benchmarks));
			BenchmarkRunner.Run(typeof(Sort_Benchmarks));
			BenchmarkRunner.Run(typeof(DataStructures_Benchmarks));
            BenchmarkRunner.Run(typeof(MatrixBenchmark));
		}
	}
}
