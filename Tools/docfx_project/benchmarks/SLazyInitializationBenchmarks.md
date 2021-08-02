# SLazy Initialization

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1110 (20H2/October2020Update)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.6.21355.2
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  Job-PABSZC : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                      Method |     N |         Mean |        Error |       StdDev |       Median | Ratio | RatioSD |
|---------------------------- |------ |-------------:|-------------:|-------------:|-------------:|------:|--------:|
|                        **Lazy** |     **1** |     **442.7 ns** |     **20.58 ns** |     **59.37 ns** |     **400.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |     1 |     400.0 ns |      0.00 ns |      0.00 ns |     400.0 ns |  0.87 |    0.12 |
|                       SLazy |     1 |     300.0 ns |      0.00 ns |      0.00 ns |     300.0 ns |  0.66 |    0.09 |
|                             |       |              |              |              |              |       |         |
|                        **Lazy** |    **10** |     **689.8 ns** |     **15.31 ns** |     **30.58 ns** |     **700.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |    10 |     661.7 ns |     20.63 ns |     58.85 ns |     700.0 ns |  0.96 |    0.10 |
|                       SLazy |    10 |     474.4 ns |     17.03 ns |     43.95 ns |     500.0 ns |  0.70 |    0.07 |
|                             |       |              |              |              |              |       |         |
|                        **Lazy** |   **100** |   **2,929.4 ns** |     **57.25 ns** |     **58.79 ns** |   **2,900.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |   100 |   2,941.2 ns |     60.21 ns |     61.83 ns |   2,900.0 ns |  1.00 |    0.03 |
|                       SLazy |   100 |   2,338.9 ns |     46.88 ns |     50.16 ns |   2,300.0 ns |  0.80 |    0.02 |
|                             |       |              |              |              |              |       |         |
|                        **Lazy** |  **1000** |  **25,378.6 ns** |    **391.22 ns** |    **346.81 ns** |  **25,200.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |  1000 |  25,107.7 ns |    424.70 ns |    354.64 ns |  24,900.0 ns |  0.99 |    0.02 |
|                       SLazy |  1000 |  20,475.0 ns |     57.93 ns |     45.23 ns |  20,500.0 ns |  0.81 |    0.01 |
|                             |       |              |              |              |              |       |         |
|                        **Lazy** | **10000** | **224,441.9 ns** | **11,506.62 ns** | **32,642.35 ns** | **215,300.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication | 10000 | 234,046.4 ns | 13,531.38 ns | 39,257.00 ns | 222,500.0 ns |  1.07 |    0.20 |
|                       SLazy | 10000 | 207,803.3 ns |  8,491.96 ns | 23,951.73 ns | 204,700.0 ns |  0.94 |    0.14 |

