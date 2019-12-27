using BenchmarkDotNet.Running;

namespace Towel_Benchmarking
{
	public class Program
	{
		public static void Main()
		{
			BenchmarkRunner.Run(typeof(DecimalToEnglishWords_Benchmarks));
			BenchmarkRunner.Run(typeof(Sort_Benchmarks));
			BenchmarkRunner.Run(typeof(DataStructures_Benchmarks));
		}
	}
}
