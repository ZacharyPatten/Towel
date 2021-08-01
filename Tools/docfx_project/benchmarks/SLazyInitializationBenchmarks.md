# SLazy Initialization

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1110 (20H2/October2020Update)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.6.21355.2
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  Job-SDZCEC : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                      Method |     N |         Mean |        Error |       StdDev |       Median | Ratio | RatioSD |
|---------------------------- |------ |-------------:|-------------:|-------------:|-------------:|------:|--------:|
|                        **Lazy** |     **1** |     **449.5 ns** |     **21.19 ns** |     **60.12 ns** |     **400.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |     1 |     456.5 ns |     23.09 ns |     65.14 ns |     400.0 ns |  1.03 |    0.19 |
|                       SLazy |     1 |     300.0 ns |      0.00 ns |      0.00 ns |     300.0 ns |  0.64 |    0.09 |
|                             |       |              |              |              |              |       |         |
|                        **Lazy** |    **10** |     **682.9 ns** |     **15.60 ns** |     **37.96 ns** |     **700.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |    10 |     678.1 ns |     18.27 ns |     52.72 ns |     700.0 ns |  1.00 |    0.10 |
|                       SLazy |    10 |     490.5 ns |     11.74 ns |     29.47 ns |     500.0 ns |  0.72 |    0.06 |
|                             |       |              |              |              |              |       |         |
|                        **Lazy** |   **100** |   **2,986.5 ns** |     **60.76 ns** |    **103.18 ns** |   **3,000.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |   100 |   2,942.3 ns |     59.07 ns |     80.86 ns |   2,900.0 ns |  0.98 |    0.05 |
|                       SLazy |   100 |   2,300.0 ns |     48.08 ns |     51.45 ns |   2,300.0 ns |  0.76 |    0.03 |
|                             |       |              |              |              |              |       |         |
|                        **Lazy** |  **1000** |  **25,233.3 ns** |    **477.17 ns** |    **372.54 ns** |  **25,100.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |  1000 |  25,066.7 ns |    395.10 ns |    308.47 ns |  25,000.0 ns |  0.99 |    0.02 |
|                       SLazy |  1000 |  20,656.0 ns |    224.81 ns |    300.11 ns |  20,500.0 ns |  0.82 |    0.01 |
|                             |       |              |              |              |              |       |         |
|                        **Lazy** | **10000** | **228,634.7 ns** | **12,455.40 ns** | **36,332.98 ns** | **220,000.0 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication | 10000 | 226,258.2 ns | 12,450.39 ns | 36,318.38 ns | 214,250.0 ns |  1.00 |    0.18 |
|                       SLazy | 10000 | 189,620.6 ns |  7,809.96 ns | 22,658.10 ns | 178,900.0 ns |  0.84 |    0.15 |

