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
|                      Method |     N |          Mean |        Error |       StdDev |        Median | Ratio | RatioSD |    Gen 0 | Gen 1 | Gen 2 |   Allocated |
|---------------------------- |------ |--------------:|-------------:|-------------:|--------------:|------:|--------:|---------:|------:|------:|------------:|
|                        **Lazy** |     **1** |      **22.20 ns** |     **0.147 ns** |     **0.130 ns** |      **22.23 ns** |  **1.00** |    **0.00** |   **0.0382** |     **-** |     **-** |       **160 B** |
| LazyExecutionAndPublication |     1 |      22.13 ns |     0.078 ns |     0.070 ns |      22.13 ns |  1.00 |    0.01 |   0.0382 |     - |     - |       160 B |
|                       SLazy |     1 |      15.54 ns |     0.116 ns |     0.097 ns |      15.53 ns |  0.70 |    0.01 |   0.0287 |     - |     - |       120 B |
|                             |       |               |              |              |               |       |         |          |       |       |             |
|                        **Lazy** |    **10** |     **208.97 ns** |     **4.112 ns** |     **8.116 ns** |     **204.83 ns** |  **1.00** |    **0.00** |   **0.3309** |     **-** |     **-** |     **1,384 B** |
| LazyExecutionAndPublication |    10 |     201.07 ns |     1.125 ns |     0.997 ns |     200.83 ns |  0.96 |    0.04 |   0.3309 |     - |     - |     1,384 B |
|                       SLazy |    10 |     142.71 ns |     0.798 ns |     0.707 ns |     142.77 ns |  0.68 |    0.03 |   0.2351 |     - |     - |       984 B |
|                             |       |               |              |              |               |       |         |          |       |       |             |
|                        **Lazy** |   **100** |   **1,973.65 ns** |    **38.768 ns** |    **80.063 ns** |   **1,932.24 ns** |  **1.00** |    **0.00** |   **3.2539** |     **-** |     **-** |    **13,624 B** |
| LazyExecutionAndPublication |   100 |   1,960.29 ns |    14.341 ns |    11.976 ns |   1,953.84 ns |  0.95 |    0.03 |   3.2578 |     - |     - |    13,624 B |
|                       SLazy |   100 |   1,406.01 ns |    27.404 ns |    45.786 ns |   1,399.79 ns |  0.71 |    0.04 |   2.3003 |     - |     - |     9,624 B |
|                             |       |               |              |              |               |       |         |          |       |       |             |
|                        **Lazy** |  **1000** |  **19,776.75 ns** |   **391.643 ns** |   **621.187 ns** |  **19,699.88 ns** |  **1.00** |    **0.00** |  **32.5012** |     **-** |     **-** |   **136,024 B** |
| LazyExecutionAndPublication |  1000 |  19,512.20 ns |   387.086 ns |   489.540 ns |  19,246.26 ns |  0.99 |    0.04 |  32.5012 |     - |     - |   136,024 B |
|                       SLazy |  1000 |  12,911.30 ns |   249.429 ns |   503.859 ns |  12,677.52 ns |  0.66 |    0.04 |  22.9492 |     - |     - |    96,024 B |
|                             |       |               |              |              |               |       |         |          |       |       |             |
|                        **Lazy** | **10000** | **191,085.52 ns** | **1,097.719 ns** |   **973.099 ns** | **190,886.58 ns** |  **1.00** |    **0.00** | **325.1953** |     **-** |     **-** | **1,360,024 B** |
| LazyExecutionAndPublication | 10000 | 193,555.10 ns |   514.903 ns |   456.448 ns | 193,557.75 ns |  1.01 |    0.01 | 325.1953 |     - |     - | 1,360,024 B |
|                       SLazy | 10000 | 128,945.22 ns | 2,566.185 ns | 5,005.146 ns | 126,364.55 ns |  0.68 |    0.03 | 229.4922 |     - |     - |   960,024 B |

