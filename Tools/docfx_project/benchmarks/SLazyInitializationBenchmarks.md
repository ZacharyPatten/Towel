# SLazy Initialization

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1110 (20H2/October2020Update)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.6.21355.2
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  Job-BVARYK : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                      Method |     N |         Mean |        Error |       StdDev |       Median | Ratio | RatioSD |
|---------------------------- |------ |-------------:|-------------:|-------------:|-------------:|------:|--------:|
|                        **Lazy** |     **1** |     **400.0 ns** |      **0.00 ns** |      **0.00 ns** |     **400.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |     1 |     400.0 ns |      0.00 ns |      0.00 ns |     400.0 ns |  1.00 |    0.00 |
|                       SLazy |     1 |     200.0 ns |      0.00 ns |      0.00 ns |     200.0 ns |  0.50 |    0.00 |
|                             |       |              |              |              |              |       |         |
|                        **Lazy** |    **10** |     **670.9 ns** |     **21.74 ns** |     **59.14 ns** |     **700.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |    10 |     653.6 ns |     18.66 ns |     54.13 ns |     700.0 ns |  0.99 |    0.11 |
|                       SLazy |    10 |     456.6 ns |     20.18 ns |     59.18 ns |     500.0 ns |  0.69 |    0.10 |
|                             |       |              |              |              |              |       |         |
|                        **Lazy** |   **100** |   **2,916.7 ns** |     **57.79 ns** |     **61.83 ns** |   **2,900.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |   100 |   2,915.0 ns |     58.25 ns |     67.08 ns |   2,900.0 ns |  1.00 |    0.03 |
|                       SLazy |   100 |   2,252.9 ns |     46.33 ns |     74.81 ns |   2,200.0 ns |  0.78 |    0.03 |
|                             |       |              |              |              |              |       |         |
|                        **Lazy** |  **1000** |  **25,335.7 ns** |    **475.74 ns** |    **421.73 ns** |  **25,150.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |  1000 |  25,042.9 ns |    268.10 ns |    237.66 ns |  24,900.0 ns |  0.99 |    0.02 |
|                       SLazy |  1000 |  19,716.7 ns |    106.93 ns |     83.48 ns |  19,700.0 ns |  0.78 |    0.01 |
|                             |       |              |              |              |              |       |         |
|                        **Lazy** | **10000** | **223,144.9 ns** | **11,838.31 ns** | **34,532.90 ns** | **212,750.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication | 10000 | 221,203.1 ns | 12,431.72 ns | 36,066.68 ns | 203,500.0 ns |  1.00 |    0.17 |
|                       SLazy | 10000 | 196,614.6 ns |  7,590.36 ns | 22,261.20 ns | 193,650.0 ns |  0.90 |    0.13 |

