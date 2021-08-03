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
|                      Method |    N |      Mean |    Error |   StdDev | Ratio | RatioSD |
|---------------------------- |----- |----------:|---------:|---------:|------:|--------:|
|                        **Lazy** |    **1** |  **30.92 ns** | **0.632 ns** | **0.907 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |    1 |  30.79 ns | 0.640 ns | 0.500 ns |  0.99 |    0.04 |
|                       SLazy |    1 |  24.99 ns | 0.524 ns | 0.751 ns |  0.81 |    0.03 |
|                   ValueLazy |    1 |  19.93 ns | 0.314 ns | 0.336 ns |  0.64 |    0.02 |
|                             |      |           |          |          |       |         |
|                        **Lazy** |   **10** |  **36.36 ns** | **0.754 ns** | **1.082 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |   10 |  35.81 ns | 0.446 ns | 0.373 ns |  0.99 |    0.03 |
|                       SLazy |   10 |  29.72 ns | 0.415 ns | 0.368 ns |  0.82 |    0.02 |
|                   ValueLazy |   10 |  25.96 ns | 0.517 ns | 0.531 ns |  0.72 |    0.03 |
|                             |      |           |          |          |       |         |
|                        **Lazy** |  **100** |  **92.71 ns** | **1.697 ns** | **1.587 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |  100 |  91.83 ns | 1.016 ns | 0.849 ns |  0.99 |    0.02 |
|                       SLazy |  100 |  79.19 ns | 0.906 ns | 0.848 ns |  0.85 |    0.02 |
|                   ValueLazy |  100 |  73.69 ns | 0.896 ns | 0.795 ns |  0.80 |    0.02 |
|                             |      |           |          |          |       |         |
|                        **Lazy** | **1000** | **536.37 ns** | **5.678 ns** | **5.312 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication | 1000 | 535.64 ns | 2.491 ns | 2.331 ns |  1.00 |    0.01 |
|                       SLazy | 1000 | 529.02 ns | 5.518 ns | 4.892 ns |  0.99 |    0.01 |
|                   ValueLazy | 1000 | 520.57 ns | 6.519 ns | 5.090 ns |  0.97 |    0.01 |

