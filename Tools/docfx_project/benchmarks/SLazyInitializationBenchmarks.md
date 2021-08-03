# SLazy Initialization

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1110 (20H2/October2020Update)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.6.21355.2
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  Job-VQFEFS : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                      Method |     N |         Mean |       Error |       StdDev |       Median | Ratio | RatioSD |
|---------------------------- |------ |-------------:|------------:|-------------:|-------------:|------:|--------:|
|                        **Lazy** |     **1** |     **400.0 ns** |     **0.00 ns** |      **0.00 ns** |     **400.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |     1 |     400.0 ns |     0.00 ns |      0.00 ns |     400.0 ns |  1.00 |    0.00 |
|                       SLazy |     1 |     290.2 ns |    10.59 ns |     29.87 ns |     300.0 ns |  0.68 |    0.12 |
|                   ValueLazy |     1 |     300.0 ns |     0.00 ns |      0.00 ns |     300.0 ns |  0.75 |    0.00 |
|                             |       |              |             |              |              |       |         |
|                        **Lazy** |    **10** |     **669.7 ns** |    **19.15 ns** |     **56.16 ns** |     **700.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |    10 |     660.6 ns |    19.38 ns |     55.30 ns |     700.0 ns |  0.99 |    0.11 |
|                       SLazy |    10 |     478.3 ns |    14.70 ns |     41.47 ns |     500.0 ns |  0.72 |    0.09 |
|                   ValueLazy |    10 |     474.2 ns |    15.15 ns |     43.97 ns |     500.0 ns |  0.71 |    0.08 |
|                             |       |              |             |              |              |       |         |
|                        **Lazy** |   **100** |   **2,931.8 ns** |    **58.32 ns** |     **71.62 ns** |   **2,900.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |   100 |   2,873.3 ns |    48.93 ns |     45.77 ns |   2,900.0 ns |  0.98 |    0.03 |
|                       SLazy |   100 |   2,272.0 ns |    45.97 ns |     61.37 ns |   2,300.0 ns |  0.77 |    0.03 |
|                   ValueLazy |   100 |   2,314.3 ns |    40.16 ns |     47.81 ns |   2,300.0 ns |  0.79 |    0.03 |
|                             |       |              |             |              |              |       |         |
|                        **Lazy** |  **1000** |  **24,971.4 ns** |   **214.17 ns** |    **189.85 ns** |  **24,900.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |  1000 |  24,908.3 ns |   247.03 ns |    192.87 ns |  24,900.0 ns |  1.00 |    0.01 |
|                       SLazy |  1000 |  19,850.0 ns |    66.89 ns |     52.22 ns |  19,850.0 ns |  0.79 |    0.01 |
|                   ValueLazy |  1000 |  20,313.3 ns |   387.29 ns |    362.27 ns |  20,100.0 ns |  0.81 |    0.02 |
|                             |       |              |             |              |              |       |         |
|                        **Lazy** | **10000** | **199,220.0 ns** | **5,568.43 ns** | **15,522.57 ns** | **192,000.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication | 10000 | 202,664.4 ns | 7,030.86 ns | 19,599.23 ns | 193,250.0 ns |  1.02 |    0.13 |
|                       SLazy | 10000 | 182,331.1 ns | 4,032.21 ns | 11,240.20 ns | 178,550.0 ns |  0.92 |    0.09 |
|                   ValueLazy | 10000 | 182,376.6 ns | 5,121.04 ns | 14,610.60 ns | 175,350.0 ns |  0.92 |    0.09 |

