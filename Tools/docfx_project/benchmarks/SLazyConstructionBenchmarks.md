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
|                        **Lazy** |     **1** |      **23.49 ns** |     **0.075 ns** |      **0.066 ns** |      **23.48 ns** |  **1.00** |    **0.00** |   **0.0382** |     **-** |     **-** |       **160 B** |
| LazyExecutionAndPublication |     1 |      21.54 ns |     0.143 ns |      0.127 ns |      21.55 ns |  0.92 |    0.01 |   0.0382 |     - |     - |       160 B |
|                       SLazy |     1 |      16.10 ns |     0.263 ns |      0.246 ns |      15.99 ns |  0.69 |    0.01 |   0.0287 |     - |     - |       120 B |
|                             |       |               |              |               |               |       |         |          |       |       |             |
|                        **Lazy** |    **10** |     **195.37 ns** |     **1.385 ns** |      **1.295 ns** |     **195.02 ns** |  **1.00** |    **0.00** |   **0.3309** |     **-** |     **-** |     **1,384 B** |
| LazyExecutionAndPublication |    10 |     207.25 ns |     4.024 ns |      6.264 ns |     206.40 ns |  1.07 |    0.03 |   0.3309 |     - |     - |     1,384 B |
|                       SLazy |    10 |     147.05 ns |     1.701 ns |      1.420 ns |     147.27 ns |  0.75 |    0.01 |   0.2351 |     - |     - |       984 B |
|                             |       |               |              |               |               |       |         |          |       |       |             |
|                        **Lazy** |   **100** |   **1,890.13 ns** |    **28.512 ns** |     **23.809 ns** |   **1,881.19 ns** |  **1.00** |    **0.00** |   **3.2558** |     **-** |     **-** |    **13,624 B** |
| LazyExecutionAndPublication |   100 |   1,996.97 ns |    38.941 ns |     41.666 ns |   1,978.26 ns |  1.06 |    0.03 |   3.2539 |     - |     - |    13,624 B |
|                       SLazy |   100 |   1,339.82 ns |    25.846 ns |     26.542 ns |   1,336.61 ns |  0.71 |    0.02 |   2.3003 |     - |     - |     9,624 B |
|                             |       |               |              |               |               |       |         |          |       |       |             |
|                        **Lazy** |  **1000** |  **19,320.80 ns** |   **381.224 ns** |    **423.730 ns** |  **19,347.76 ns** |  **1.00** |    **0.00** |  **32.5012** |     **-** |     **-** |   **136,024 B** |
| LazyExecutionAndPublication |  1000 |  19,640.55 ns |   347.447 ns |    325.002 ns |  19,690.26 ns |  1.01 |    0.03 |  32.5012 |     - |     - |   136,024 B |
|                       SLazy |  1000 |  13,186.66 ns |   209.667 ns |    185.865 ns |  13,151.21 ns |  0.68 |    0.02 |  22.9492 |     - |     - |    96,024 B |
|                             |       |               |              |               |               |       |         |          |       |       |             |
|                        **Lazy** | **10000** | **213,004.25 ns** | **9,668.635 ns** | **27,741.126 ns** | **198,535.77 ns** |  **1.00** |    **0.00** | **325.1953** |     **-** |     **-** | **1,360,024 B** |
| LazyExecutionAndPublication | 10000 | 192,453.36 ns | 3,600.379 ns |  3,367.797 ns | 190,729.71 ns |  0.92 |    0.10 | 325.1953 |     - |     - | 1,360,024 B |
|                       SLazy | 10000 | 129,510.58 ns | 2,045.435 ns |  2,799.815 ns | 128,340.17 ns |  0.60 |    0.07 | 229.4922 |     - |     - |   960,024 B |

