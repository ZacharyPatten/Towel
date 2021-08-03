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
|                      Method |     N |          Mean |        Error |       StdDev | Ratio | RatioSD |    Gen 0 | Gen 1 | Gen 2 |   Allocated |
|---------------------------- |------ |--------------:|-------------:|-------------:|------:|--------:|---------:|------:|------:|------------:|
|                        **Lazy** |     **1** |      **21.62 ns** |     **0.443 ns** |     **0.415 ns** |  **1.00** |    **0.00** |   **0.0382** |     **-** |     **-** |       **160 B** |
| LazyExecutionAndPublication |     1 |      21.11 ns |     0.314 ns |     0.262 ns |  0.98 |    0.02 |   0.0382 |     - |     - |       160 B |
|                       SLazy |     1 |      15.93 ns |     0.157 ns |     0.139 ns |  0.74 |    0.02 |   0.0287 |     - |     - |       120 B |
|                   ValueLazy |     1 |      11.91 ns |     0.266 ns |     0.544 ns |  0.54 |    0.02 |   0.0210 |     - |     - |        88 B |
|                             |       |               |              |              |       |         |          |       |       |             |
|                        **Lazy** |    **10** |     **192.09 ns** |     **2.707 ns** |     **2.532 ns** |  **1.00** |    **0.00** |   **0.3309** |     **-** |     **-** |     **1,384 B** |
| LazyExecutionAndPublication |    10 |     199.57 ns |     3.994 ns |     5.855 ns |  1.04 |    0.04 |   0.3309 |     - |     - |     1,384 B |
|                       SLazy |    10 |     140.37 ns |     2.527 ns |     2.364 ns |  0.73 |    0.01 |   0.2351 |     - |     - |       984 B |
|                   ValueLazy |    10 |      94.04 ns |     1.169 ns |     1.094 ns |  0.49 |    0.01 |   0.1587 |     - |     - |       664 B |
|                             |       |               |              |              |       |         |          |       |       |             |
|                        **Lazy** |   **100** |   **1,910.85 ns** |    **12.321 ns** |    **11.525 ns** |  **1.00** |    **0.00** |   **3.2539** |     **-** |     **-** |    **13,624 B** |
| LazyExecutionAndPublication |   100 |   1,897.59 ns |    37.460 ns |    52.513 ns |  0.99 |    0.04 |   3.2558 |     - |     - |    13,624 B |
|                       SLazy |   100 |   1,314.51 ns |    25.721 ns |    24.060 ns |  0.69 |    0.01 |   2.3003 |     - |     - |     9,624 B |
|                   ValueLazy |   100 |     858.02 ns |    13.774 ns |    11.502 ns |  0.45 |    0.01 |   1.5354 |     - |     - |     6,424 B |
|                             |       |               |              |              |       |         |          |       |       |             |
|                        **Lazy** |  **1000** |  **19,371.04 ns** |   **381.131 ns** |   **423.627 ns** |  **1.00** |    **0.00** |  **32.5012** |     **-** |     **-** |   **136,024 B** |
| LazyExecutionAndPublication |  1000 |  19,359.57 ns |   327.790 ns |   414.549 ns |  1.00 |    0.02 |  32.5012 |     - |     - |   136,024 B |
|                       SLazy |  1000 |  13,613.61 ns |   134.713 ns |   112.492 ns |  0.70 |    0.02 |  22.9492 |     - |     - |    96,024 B |
|                   ValueLazy |  1000 |   8,841.21 ns |   175.317 ns |   392.122 ns |  0.45 |    0.02 |  15.3046 |     - |     - |    64,024 B |
|                             |       |               |              |              |       |         |          |       |       |             |
|                        **Lazy** | **10000** | **182,700.92 ns** | **2,167.632 ns** | **1,921.550 ns** |  **1.00** |    **0.00** | **325.1953** |     **-** |     **-** | **1,360,024 B** |
| LazyExecutionAndPublication | 10000 | 187,679.63 ns | 3,718.996 ns | 5,090.604 ns |  1.04 |    0.03 | 325.1953 |     - |     - | 1,360,024 B |
|                       SLazy | 10000 | 139,473.31 ns | 2,775.461 ns | 5,280.603 ns |  0.76 |    0.02 | 229.4922 |     - |     - |   960,024 B |
|                   ValueLazy | 10000 |  85,404.33 ns |   615.786 ns |   514.209 ns |  0.47 |    0.01 | 152.9541 |     - |     - |   640,024 B |

