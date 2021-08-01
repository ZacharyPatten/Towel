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
		private Lazy<int>[]? lazysThreadSafe;
		private Lazy<int>[]? lazysThreadSafePublicationOnly;
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

			lazysThreadSafe = new Lazy<int>[N];
			for (int i = 0; i < N; i++)
			{
				lazysThreadSafe[i] = new(() => i, true);
			}

			lazysThreadSafePublicationOnly = new Lazy<int>[N];
			for (int i = 0; i < N; i++)
			{
				lazysThreadSafePublicationOnly[i] = new(() => i, LazyThreadSafetyMode.PublicationOnly);
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
		public void LazyThreadSafe()
		{
			for (int i = 0; i < N; i++)
			{
				temp = lazysThreadSafe![i].Value;
			}
		}

		[Benchmark]
		public void LazyThreadSafePublicationOnly()
		{
			for (int i = 0; i < N; i++)
			{
				temp = lazysThreadSafePublicationOnly![i].Value;
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
		public void LazyThreadSafe()
		{
			Lazy<int> value = new(() => -1, true);
			for (int i = 0; i < N; i++)
			{
				temp = value.Value;
			}
		}

		[Benchmark]
		public void LazyThreadSafePublicationOnly()
		{
			Lazy<int> value = new(() => -1, LazyThreadSafetyMode.PublicationOnly);
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
