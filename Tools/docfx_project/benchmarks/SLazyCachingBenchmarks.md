# Slazy Caching

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1110 (20H2/October2020Update)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.6.21355.2
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  DefaultJob : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT


```
|                      Method |    N |      Mean |    Error |   StdDev | Ratio | RatioSD |
|---------------------------- |----- |----------:|---------:|---------:|------:|--------:|
|                        **Lazy** |    **1** |  **31.19 ns** | **0.365 ns** | **0.305 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |    1 |  31.28 ns | 0.237 ns | 0.185 ns |  1.00 |    0.01 |
|                       SLazy |    1 |  25.68 ns | 0.537 ns | 0.820 ns |  0.81 |    0.03 |
|                             |      |           |          |          |       |         |
|                        **Lazy** |   **10** |  **39.98 ns** | **0.829 ns** | **1.134 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |   10 |  38.78 ns | 0.611 ns | 0.572 ns |  0.96 |    0.02 |
|                       SLazy |   10 |  30.19 ns | 0.171 ns | 0.160 ns |  0.75 |    0.02 |
|                             |      |           |          |          |       |         |
|                        **Lazy** |  **100** |  **96.05 ns** | **0.474 ns** | **0.443 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |  100 |  97.13 ns | 1.488 ns | 1.461 ns |  1.01 |    0.01 |
|                       SLazy |  100 |  85.09 ns | 1.363 ns | 1.275 ns |  0.89 |    0.01 |
|                             |      |           |          |          |       |         |
|                        **Lazy** | **1000** | **565.75 ns** | **7.929 ns** | **7.416 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication | 1000 | 562.23 ns | 5.964 ns | 5.578 ns |  0.99 |    0.02 |
|                       SLazy | 1000 | 547.25 ns | 4.835 ns | 4.523 ns |  0.97 |    0.01 |

