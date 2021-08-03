# SLazy Construction

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1110 (20H2/October2020Update)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.6.21355.2
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  DefaultJob : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT


```
|                      Method |     N |          Mean |        Error |        StdDev |        Median | Ratio | RatioSD |    Gen 0 | Gen 1 | Gen 2 |   Allocated |
|---------------------------- |------ |--------------:|-------------:|--------------:|--------------:|------:|--------:|---------:|------:|------:|------------:|
|                        **Lazy** |     **1** |      **22.50 ns** |     **0.449 ns** |      **0.763 ns** |      **22.16 ns** |  **1.00** |    **0.00** |   **0.0382** |     **-** |     **-** |       **160 B** |
| LazyExecutionAndPublication |     1 |      22.22 ns |     0.466 ns |      0.536 ns |      22.11 ns |  0.97 |    0.05 |   0.0382 |     - |     - |       160 B |
|                       SLazy |     1 |      16.22 ns |     0.137 ns |      0.114 ns |      16.23 ns |  0.70 |    0.02 |   0.0287 |     - |     - |       120 B |
|                             |       |               |              |               |               |       |         |          |       |       |             |
|                        **Lazy** |    **10** |     **202.49 ns** |     **3.694 ns** |      **3.628 ns** |     **200.95 ns** |  **1.00** |    **0.00** |   **0.3309** |     **-** |     **-** |     **1,384 B** |
| LazyExecutionAndPublication |    10 |     201.18 ns |     1.589 ns |      1.487 ns |     201.02 ns |  0.99 |    0.02 |   0.3309 |     - |     - |     1,384 B |
|                       SLazy |    10 |     143.28 ns |     2.875 ns |      2.824 ns |     142.50 ns |  0.71 |    0.02 |   0.2351 |     - |     - |       984 B |
|                             |       |               |              |               |               |       |         |          |       |       |             |
|                        **Lazy** |   **100** |   **2,012.92 ns** |    **30.173 ns** |     **34.747 ns** |   **2,003.43 ns** |  **1.00** |    **0.00** |   **3.2539** |     **-** |     **-** |    **13,624 B** |
| LazyExecutionAndPublication |   100 |   2,034.90 ns |    35.676 ns |     42.469 ns |   2,024.93 ns |  1.01 |    0.03 |   3.2539 |     - |     - |    13,624 B |
|                       SLazy |   100 |   1,360.76 ns |    25.861 ns |     21.595 ns |   1,359.66 ns |  0.67 |    0.02 |   2.3003 |     - |     - |     9,624 B |
|                             |       |               |              |               |               |       |         |          |       |       |             |
|                        **Lazy** |  **1000** |  **19,391.63 ns** |   **195.598 ns** |    **163.333 ns** |  **19,312.63 ns** |  **1.00** |    **0.00** |  **32.5012** |     **-** |     **-** |   **136,024 B** |
| LazyExecutionAndPublication |  1000 |  21,608.07 ns |   308.875 ns |    288.922 ns |  21,617.02 ns |  1.11 |    0.01 |  32.5012 |     - |     - |   136,024 B |
|                       SLazy |  1000 |  13,113.88 ns |   200.511 ns |    167.436 ns |  13,089.79 ns |  0.68 |    0.01 |  22.9492 |     - |     - |    96,024 B |
|                             |       |               |              |               |               |       |         |          |       |       |             |
|                        **Lazy** | **10000** | **201,931.58 ns** | **4,956.269 ns** | **14,140.514 ns** | **195,520.00 ns** |  **1.00** |    **0.00** | **325.1953** |     **-** |     **-** | **1,360,024 B** |
| LazyExecutionAndPublication | 10000 | 200,473.35 ns | 1,523.730 ns |  1,189.628 ns | 200,682.32 ns |  0.98 |    0.07 | 325.1953 |     - |     - | 1,360,024 B |
|                       SLazy | 10000 | 136,375.92 ns | 2,699.036 ns |  6,092.175 ns | 133,926.17 ns |  0.67 |    0.05 | 229.4922 |     - |     - |   960,024 B |

