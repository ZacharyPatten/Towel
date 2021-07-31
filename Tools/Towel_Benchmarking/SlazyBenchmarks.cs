using System;
using BenchmarkDotNet.Attributes;
using Towel;

namespace Towel_Benchmarking
{
	[Tag(Program.Name, "Slazy Initialization")]
	[Tag(Program.OutputFile, nameof(SlazyInitializationBenchmarks))]
	public class SlazyInitializationBenchmarks
	{
		private Lazy<int>[]? lazys;
		private SLazy<int>[]? slazys;

		[Params(1, 10, 100, 1000, 10000)]
		public int N;

		internal int temp = -1;

		[IterationSetup]
		public void IterationSetup()
		{
			lazys = new Lazy<int>[N];
			for (int i = 0; i < N; i++)
			{
				lazys[i] = new(() => i);
			}

			slazys = new SLazy<int>[N];
			for (int i = 0; i < N; i++)
			{
				slazys[i] = new(() => i);
			}
		}

		[GlobalCleanup]
		public void GlobalCleanUp()
		{
			Console.WriteLine(temp);
		}

		[Benchmark(Baseline = true)]
		public void Lazy()
		{
			for (int i = 0; i < N; i++)
			{
				temp = lazys![i].Value;
			}
		}

		[Benchmark]
		public void SLazy()
		{
			for (int i = 0; i < N; i++)
			{
				temp = slazys![i].Value;
			}
		}
	}

	[Tag(Program.Name, "Slazy Caching")]
	[Tag(Program.OutputFile, nameof(SlazyCachingBenchmarks))]
	public class SlazyCachingBenchmarks
	{
		[Params(1, 10, 100, 1000)]
		public int N;

		internal int temp = -1;

		[GlobalCleanup]
		public void GlobalCleanUp()
		{
			Console.WriteLine(temp);
		}

		[Benchmark(Baseline = true)]
		public void Lazy()
		{
			Lazy<int> value = new(() => -1);
			for (int i = 0; i < N; i++)
			{
				temp = value.Value;
			}
		}

		[Benchmark]
		public void SLazy()
		{
			SLazy<int> value = new(() => -1);
			for (int i = 0; i < N; i++)
			{
				temp = value.Value;
			}
		}
	}
}
