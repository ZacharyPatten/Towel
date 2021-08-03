# SLazy Caching

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1110 (20H2/October2020Update)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.6.21355.2
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  DefaultJob : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT


```
|                      Method |    N |      Mean |    Error |   StdDev |    Median | Ratio | RatioSD |
|---------------------------- |----- |----------:|---------:|---------:|----------:|------:|--------:|
|                        **Lazy** |    **1** |  **32.33 ns** | **0.674 ns** | **1.422 ns** |  **32.05 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |    1 |  30.87 ns | 0.498 ns | 0.511 ns |  30.87 ns |  0.98 |    0.05 |
|                       SLazy |    1 |  25.03 ns | 0.524 ns | 0.514 ns |  24.86 ns |  0.79 |    0.03 |
|                             |      |           |          |          |           |       |         |
|                        **Lazy** |   **10** |  **35.87 ns** | **0.157 ns** | **0.139 ns** |  **35.83 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |   10 |  36.09 ns | 0.699 ns | 0.748 ns |  35.84 ns |  1.01 |    0.02 |
|                       SLazy |   10 |  29.76 ns | 0.624 ns | 0.694 ns |  29.85 ns |  0.83 |    0.02 |
|                             |      |           |          |          |           |       |         |
|                        **Lazy** |  **100** |  **95.12 ns** | **2.716 ns** | **7.661 ns** |  **91.62 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |  100 |  92.24 ns | 1.494 ns | 1.397 ns |  91.86 ns |  0.94 |    0.09 |
|                       SLazy |  100 |  81.92 ns | 1.615 ns | 1.795 ns |  82.14 ns |  0.82 |    0.08 |
|                             |      |           |          |          |           |       |         |
|                        **Lazy** | **1000** | **534.87 ns** | **7.279 ns** | **6.452 ns** | **532.73 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication | 1000 | 534.40 ns | 3.536 ns | 3.308 ns | 533.46 ns |  1.00 |    0.01 |
|                       SLazy | 1000 | 520.42 ns | 2.383 ns | 2.229 ns | 520.21 ns |  0.97 |    0.01 |

