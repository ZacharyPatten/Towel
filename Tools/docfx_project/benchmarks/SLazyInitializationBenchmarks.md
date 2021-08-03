# SLazy Initialization

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1110 (20H2/October2020Update)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.6.21355.2
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  Job-BJDLWC : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                      Method |     N |         Mean |        Error |       StdDev |       Median | Ratio | RatioSD |
|---------------------------- |------ |-------------:|-------------:|-------------:|-------------:|------:|--------:|
|                        **Lazy** |     **1** |     **350.5 ns** |     **23.02 ns** |     **65.31 ns** |     **300.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |     1 |     389.0 ns |     11.22 ns |     31.45 ns |     400.0 ns |  1.14 |    0.21 |
|                       SLazy |     1 |     300.0 ns |      0.00 ns |      0.00 ns |     300.0 ns |  0.78 |    0.14 |
|                             |       |              |              |              |              |       |         |
|                        **Lazy** |    **10** |     **665.7 ns** |     **18.97 ns** |     **55.63 ns** |     **700.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |    10 |     654.9 ns |     21.44 ns |     60.12 ns |     700.0 ns |  0.99 |    0.12 |
|                       SLazy |    10 |     476.9 ns |     21.32 ns |     59.77 ns |     500.0 ns |  0.72 |    0.11 |
|                             |       |              |              |              |              |       |         |
|                        **Lazy** |   **100** |   **2,926.3 ns** |     **58.78 ns** |     **65.34 ns** |   **2,900.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |   100 |   2,868.2 ns |     58.32 ns |     71.62 ns |   2,900.0 ns |  0.98 |    0.03 |
|                       SLazy |   100 |   2,268.2 ns |     38.82 ns |     47.67 ns |   2,300.0 ns |  0.78 |    0.02 |
|                             |       |              |              |              |              |       |         |
|                        **Lazy** |  **1000** |  **25,041.7 ns** |     **85.63 ns** |     **66.86 ns** |  **25,050.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |  1000 |  25,133.3 ns |    406.26 ns |    317.18 ns |  25,150.0 ns |  1.00 |    0.01 |
|                       SLazy |  1000 |  19,976.9 ns |    263.97 ns |    220.43 ns |  19,900.0 ns |  0.80 |    0.01 |
|                             |       |              |              |              |              |       |         |
|                        **Lazy** | **10000** | **218,455.6 ns** | **11,333.33 ns** | **33,238.69 ns** | **201,200.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication | 10000 | 242,215.4 ns |  3,729.99 ns |  3,114.71 ns | 243,200.0 ns |  0.96 |    0.09 |
|                       SLazy | 10000 | 187,578.5 ns |  4,858.01 ns | 13,781.37 ns | 185,700.0 ns |  0.88 |    0.13 |

