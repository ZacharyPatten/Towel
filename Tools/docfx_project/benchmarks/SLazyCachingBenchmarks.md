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
|                        **Lazy** |    **1** |  **32.00 ns** | **0.655 ns** | **1.058 ns** |  **31.46 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |    1 |  30.76 ns | 0.362 ns | 0.339 ns |  30.63 ns |  0.97 |    0.03 |
|                       SLazy |    1 |  25.74 ns | 0.163 ns | 0.152 ns |  25.74 ns |  0.81 |    0.03 |
|                             |      |           |          |          |           |       |         |
|                        **Lazy** |   **10** |  **36.97 ns** | **0.755 ns** | **0.981 ns** |  **36.74 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |   10 |  38.74 ns | 0.802 ns | 1.842 ns |  38.45 ns |  1.07 |    0.05 |
|                       SLazy |   10 |  30.47 ns | 0.607 ns | 0.890 ns |  30.48 ns |  0.83 |    0.03 |
|                             |      |           |          |          |           |       |         |
|                        **Lazy** |  **100** |  **93.79 ns** | **1.500 ns** | **1.403 ns** |  **93.76 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |  100 |  91.69 ns | 1.021 ns | 0.905 ns |  91.61 ns |  0.98 |    0.02 |
|                       SLazy |  100 |  81.08 ns | 0.506 ns | 0.473 ns |  80.92 ns |  0.86 |    0.02 |
|                             |      |           |          |          |           |       |         |
|                        **Lazy** | **1000** | **556.80 ns** | **5.807 ns** | **5.147 ns** | **555.27 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication | 1000 | 543.50 ns | 4.022 ns | 3.762 ns | 542.86 ns |  0.98 |    0.01 |
|                       SLazy | 1000 | 526.17 ns | 1.857 ns | 1.737 ns | 526.79 ns |  0.94 |    0.01 |

