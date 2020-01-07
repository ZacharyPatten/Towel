using BenchmarkDotNet.Attributes;
using Towel;

namespace Towel_Benchmarking
{
	public class ToEnglishWords_Benchmarks
	{
		[Params(10, 100, 1000, 10000, 100000)]
		public decimal Range;

		[Benchmark] public void Decimal_ToEnglishWords()
		{
			decimal increment = Range / 200000;
			for (decimal i = 0; i < Range; i += increment)
			{
				string englishWords = i.ToEnglishWords();
			}
		}
	}
}
