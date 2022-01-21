using System.Threading;

namespace Towel_Benchmarking;

[Tag(Program.Name, "Lazy Initialization")]
[Tag(Program.OutputFile, nameof(LazyInitializationBenchmarks))]
public class LazyInitializationBenchmarks
{
	private Lazy<int>[]? lazys;
	private Lazy<int>[]? lazyNones;
	private Lazy<int>[]? lazyPublicationLocks;
	private SLazy<int>[]? slazys;
	private SLazyNoCatch<int>[]? slazyNoCatchs;
	private SLazyNoLock<int>[]? slazyNoLocks;
	private SLazyNoLockNoCatch<int>[]? slazyNoLockNoCatchs;
	private SLazyPublicationLock<int>[]? slazyPublicationLocks;
	private SLazyPublicationLockNoCatch<int>[]? slazyPublicationLockNoCatchs;
	private ValueLazy<int>[]? valueLazys;
	private ValueLazyNoCatch<int>[]? valueLazyNoCatchs;
	private ValueLazyNoLock<int>[]? valueLazyNoLocks;
	private ValueLazyNoLockNoCatch<int>[]? valueLazyNoLockNoCatchs;
	private ValueLazyPublicationLock<int>[]? valueLazyPublicationLocks;
	private ValueLazyPublicationLockNoCatch<int>[]? valueLazyPublicationLockNoCatchs;

	[Params(1, 10, 100, 1000, 10000)]
	public int N;

	internal int temp = -1;

	[IterationSetup]
	public void IterationSetup()
	{
		int N1 = N + 1;
		lazys = (1..N1).ToArray(i => new Lazy<int>(() => i));
		lazyNones = (1..N1).ToArray(i => new Lazy<int>(() => i, LazyThreadSafetyMode.None));
		lazyPublicationLocks = (1..N1).ToArray(i => new Lazy<int>(() => i, LazyThreadSafetyMode.PublicationOnly));
		slazys = (1..N1).ToArray(i => new SLazy<int>(() => i));
		slazyNoCatchs = (1..N1).ToArray(i => new SLazyNoCatch<int>(() => i));
		slazyPublicationLocks = (1..N1).ToArray(i => new SLazyPublicationLock<int>(() => i));
		slazyPublicationLockNoCatchs = (1..N1).ToArray(i => new SLazyPublicationLockNoCatch<int>(() => i));
		slazyNoLocks = (1..N1).ToArray(i => new SLazyNoLock<int>(() => i));
		slazyNoLockNoCatchs = (1..N1).ToArray(i => new SLazyNoLockNoCatch<int>(() => i));
		valueLazys = (1..N1).ToArray(i => new ValueLazy<int>(() => i));
		valueLazyNoCatchs = (1..N1).ToArray(i => new ValueLazyNoCatch<int>(() => i));
		valueLazyPublicationLocks = (1..N1).ToArray(i => new ValueLazyPublicationLock<int>(() => i));
		valueLazyPublicationLockNoCatchs = (1..N1).ToArray(i => new ValueLazyPublicationLockNoCatch<int>(() => i));
		valueLazyNoLocks = (1..N1).ToArray(i => new ValueLazyNoLock<int>(() => i));
		valueLazyNoLockNoCatchs = (1..N1).ToArray(i => new ValueLazyNoLockNoCatch<int>(() => i));
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
	public void LazyPublicationOnly()
	{
		for (int i = 0; i < N; i++)
		{
			temp = lazyPublicationLocks![i].Value;
		}
	}

	[Benchmark]
	public void LazyNone()
	{
		for (int i = 0; i < N; i++)
		{
			temp = lazyNones![i].Value;
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
	public void SLazyNoCatch()
	{
		for (int i = 0; i < N; i++)
		{
			temp = slazyNoCatchs![i].Value;
		}
	}

	[Benchmark]
	public void SLazyPublicationLock()
	{
		for (int i = 0; i < N; i++)
		{
			temp = slazyPublicationLocks![i].Value;
		}
	}

	[Benchmark]
	public void SLazyPublicationLockNoCatch()
	{
		for (int i = 0; i < N; i++)
		{
			temp = slazyPublicationLockNoCatchs![i].Value;
		}
	}

	[Benchmark]
	public void SLazyNoLock()
	{
		for (int i = 0; i < N; i++)
		{
			temp = slazyNoLocks![i].Value;
		}
	}

	[Benchmark]
	public void SLazyNoLockNoCatch()
	{
		for (int i = 0; i < N; i++)
		{
			temp = slazyNoLockNoCatchs![i].Value;
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

	[Benchmark]
	public void ValueLazyNoCatch()
	{
		for (int i = 0; i < N; i++)
		{
			temp = valueLazyNoCatchs![i].Value;
		}
	}

	[Benchmark]
	public void ValueLazyPublicationLock()
	{
		for (int i = 0; i < N; i++)
		{
			temp = valueLazyPublicationLocks![i].Value;
		}
	}

	[Benchmark]
	public void ValueLazyPublicationLockNoCatch()
	{
		for (int i = 0; i < N; i++)
		{
			temp = valueLazyPublicationLockNoCatchs![i].Value;
		}
	}

	[Benchmark]
	public void ValueLazyNoLock()
	{
		for (int i = 0; i < N; i++)
		{
			temp = valueLazyNoLocks![i].Value;
		}
	}

	[Benchmark]
	public void ValueLazyNoLockNoCatch()
	{
		for (int i = 0; i < N; i++)
		{
			temp = valueLazyNoLockNoCatchs![i].Value;
		}
	}
}

[Tag(Program.Name, "Lazy Caching")]
[Tag(Program.OutputFile, nameof(LazyCachingBenchmarks))]
public class LazyCachingBenchmarks
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
	public void LazyPublicationOnly()
	{
		Lazy<int> value = new(() => -1, LazyThreadSafetyMode.PublicationOnly);
		for (int i = 0; i < N; i++)
		{
			temp = value.Value;
		}
	}

	[Benchmark]
	public void LazyNone()
	{
		Lazy<int> value = new(() => -1, LazyThreadSafetyMode.None);
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
	public void SLazyNoCatch()
	{
		SLazyNoCatch<int> value = new(() => -1);
		for (int i = 0; i < N; i++)
		{
			temp = value.Value;
		}
	}

	[Benchmark]
	public void SLazyPublicationLock()
	{
		SLazyPublicationLock<int> value = new(() => -1);
		for (int i = 0; i < N; i++)
		{
			temp = value.Value;
		}
	}

	[Benchmark]
	public void SLazyPublicationLockNoCatch()
	{
		SLazyPublicationLockNoCatch<int> value = new(() => -1);
		for (int i = 0; i < N; i++)
		{
			temp = value.Value;
		}
	}

	[Benchmark]
	public void SLazyNoLock()
	{
		SLazyNoLock<int> value = new(() => -1);
		for (int i = 0; i < N; i++)
		{
			temp = value.Value;
		}
	}

	[Benchmark]
	public void SLazyNoLockNoCatch()
	{
		SLazyNoLockNoCatch<int> value = new(() => -1);
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

	[Benchmark]
	public void ValueLazyNoCatch()
	{
		ValueLazyNoCatch<int> value = new(() => -1);
		for (int i = 0; i < N; i++)
		{
			temp = value.Value;
		}
	}

	[Benchmark]
	public void ValueLazyPublicationLock()
	{
		ValueLazyPublicationLock<int> value = new(() => -1);
		for (int i = 0; i < N; i++)
		{
			temp = value.Value;
		}
	}

	[Benchmark]
	public void ValueLazyPublicationLockNoCatch()
	{
		ValueLazyPublicationLockNoCatch<int> value = new(() => -1);
		for (int i = 0; i < N; i++)
		{
			temp = value.Value;
		}
	}

	[Benchmark]
	public void ValueLazyNoLock()
	{
		ValueLazyNoLock<int> value = new(() => -1);
		for (int i = 0; i < N; i++)
		{
			temp = value.Value;
		}
	}

	[Benchmark]
	public void ValueLazyNoLockNoCatch()
	{
		ValueLazyNoLockNoCatch<int> value = new(() => -1);
		for (int i = 0; i < N; i++)
		{
			temp = value.Value;
		}
	}
}

[Tag(Program.Name, "Lazy Construction")]
[Tag(Program.OutputFile, nameof(LazyConstructionBenchmarks))]
[MemoryDiagnoser]
public class LazyConstructionBenchmarks
{
	[Params(1, 10, 100, 1000, 10000)]
	public int N;

	[Benchmark(Baseline = true)]
	public void Lazy()
	{
		for (int i = 0; i < N; i++)
		{
			_ = new Lazy<int>(() => i);
		}
	}

	[Benchmark]
	public void LazyPublicationOnly()
	{
		for (int i = 0; i < N; i++)
		{
			_ = new Lazy<int>(() => i, LazyThreadSafetyMode.PublicationOnly);
		}
	}

	[Benchmark]
	public void LazyNone()
	{
		for (int i = 0; i < N; i++)
		{
			_ = new Lazy<int>(() => i, LazyThreadSafetyMode.None);
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
	public void SLazyNoCatch()
	{
		for (int i = 0; i < N; i++)
		{
			_ = new SLazyNoCatch<int>(() => i);
		}
	}

	[Benchmark]
	public void SLazyPublicationLock()
	{
		for (int i = 0; i < N; i++)
		{
			_ = new SLazyPublicationLock<int>(() => i);
		}
	}

	[Benchmark]
	public void SLazyPublicationLockNoCatch()
	{
		for (int i = 0; i < N; i++)
		{
			_ = new SLazyPublicationLockNoCatch<int>(() => i);
		}
	}

	[Benchmark]
	public void SLazyNoLock()
	{
		for (int i = 0; i < N; i++)
		{
			_ = new SLazyNoLock<int>(() => i);
		}
	}

	[Benchmark]
	public void SLazyNoLockNoCatch()
	{
		for (int i = 0; i < N; i++)
		{
			_ = new SLazyNoLockNoCatch<int>(() => i);
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

	[Benchmark]
	public void ValueLazyNoCatch()
	{
		for (int i = 0; i < N; i++)
		{
			_ = new ValueLazyNoCatch<int>(() => i);
		}
	}

	[Benchmark]
	public void ValueLazyPublicationLock()
	{
		for (int i = 0; i < N; i++)
		{
			_ = new ValueLazyPublicationLock<int>(() => i);
		}
	}

	[Benchmark]
	public void ValueLazyPublicationLockNoCatch()
	{
		for (int i = 0; i < N; i++)
		{
			_ = new ValueLazyPublicationLockNoCatch<int>(() => i);
		}
	}

	[Benchmark]
	public void ValueLazyNoLock()
	{
		for (int i = 0; i < N; i++)
		{
			_ = new ValueLazyNoLock<int>(() => i);
		}
	}

	[Benchmark]
	public void ValueLazyNoLockNoCatch()
	{
		for (int i = 0; i < N; i++)
		{
			_ = new ValueLazyNoLockNoCatch<int>(() => i);
		}
	}
}
