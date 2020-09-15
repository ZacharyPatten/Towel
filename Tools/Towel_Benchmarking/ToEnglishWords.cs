using BenchmarkDotNet.Attributes;
using Towel;

namespace Towel_Benchmarking
{
	[Value(Program.Name, "[Numeric] To English Words")]
	public class ToEnglishWords_Benchmarks
	{
		[Params(10, 100, 1000, 10000, 100000)]
		public decimal Range;

		[Benchmark] public void Decimal_ToEnglishWords()
		{
			decimal increment = Range / 200000;
			for (decimal i = 0; i < Range; i += increment)
			{
				_ = i.ToEnglishWords();
			}
		}
	}
}
