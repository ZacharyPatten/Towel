using System;
using System.Threading;
using BenchmarkDotNet.Attributes;
using Towel;

namespace Towel_Benchmarking
{
	[Tag(Program.Name, "SLazy Initialization")]
	[Tag(Program.OutputFile, nameof(SLazyInitializationBenchmarks))]
	public class SLazyInitializationBenchmarks
	{
		private Lazy<int>[]? lazys;
		private Lazy<int>[]? lazysExecutionAndPublication;
		private SLazy<int>[]? slazys;
		private ValueLazy<int>[]? valueLazys;

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

			lazysExecutionAndPublication = new Lazy<int>[N];
			for (int i = 0; i < N; i++)
			{
				lazysExecutionAndPublication[i] = new(() => i, LazyThreadSafetyMode.ExecutionAndPublication);
			}

			slazys = new SLazy<int>[N];
			for (int i = 0; i < N; i++)
			{
				slazys[i] = new(() => i);
			}

			valueLazys = new ValueLazy<int>[N];
			for (int i = 0; i < N; i++)
			{
				valueLazys[i] = new(() => i);
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
		public void LazyExecutionAndPublication()
		{
			for (int i = 0; i < N; i++)
			{
				temp = lazysExecutionAndPublication![i].Value;
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

		[Benchmark]
		public void ValueLazy()
		{
			for (int i = 0; i < N; i++)
			{
				temp = valueLazys![i].Value;
			}
		}
	}

	[Tag(Program.Name, "SLazy Caching")]
	[Tag(Program.OutputFile, nameof(SLazyCachingBenchmarks))]
	public class SLazyCachingBenchmarks
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
		public void LazyExecutionAndPublication()
		{
			Lazy<int> value = new(() => -1, LazyThreadSafetyMode.ExecutionAndPublication);
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

		[Benchmark]
		public void ValueLazy()
		{
			ValueLazy<int> value = new(() => -1);
			for (int i = 0; i < N; i++)
			{
				temp = value.Value;
			}
		}
	}

	[Tag(Program.Name, "SLazy Construction")]
	[Tag(Program.OutputFile, nameof(SLazyConstructionBenchmarks))]
	[MemoryDiagnoser]
	public class SLazyConstructionBenchmarks
	{
		[Params(1, 10, 100, 1000, 10000)]
		public int N;

		[Benchmark(Baseline = true)]
		public void Lazy()
		{
			for (int i = 0; i < N; i++)
			{
				_ =  new Lazy<int>(() => i);
			}
		}

		[Benchmark]
		public void LazyExecutionAndPublication()
		{
			for (int i = 0; i < N; i++)
			{
				_ = new Lazy<int>(() => i, LazyThreadSafetyMode.ExecutionAndPublication);
			}
		}

		[Benchmark]
		public void SLazy()
		{
			for (int i = 0; i < N; i++)
			{
				_ = new SLazy<int>(() => i);
			}
		}

		[Benchmark]
		public void ValueLazy()
		{
			for (int i = 0; i < N; i++)
			{
				_ = new ValueLazy<int>(() => i);
			}
		}
	}
}
