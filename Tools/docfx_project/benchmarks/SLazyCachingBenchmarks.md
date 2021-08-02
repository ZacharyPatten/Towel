# SLazy Caching

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1110 (20H2/October2020Update)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.6.21355.2
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  DefaultJob : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT


```
|                      Method |    N |      Mean |     Error |    StdDev | Ratio | RatioSD |
|---------------------------- |----- |----------:|----------:|----------:|------:|--------:|
|                        **Lazy** |    **1** |  **33.33 ns** |  **0.691 ns** |  **1.076 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |    1 |  32.84 ns |  0.662 ns |  0.680 ns |  0.98 |    0.03 |
|                       SLazy |    1 |  25.77 ns |  0.264 ns |  0.247 ns |  0.77 |    0.02 |
|                             |      |           |           |           |       |         |
|                        **Lazy** |   **10** |  **38.30 ns** |  **0.327 ns** |  **0.306 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |   10 |  38.25 ns |  0.550 ns |  0.487 ns |  1.00 |    0.02 |
|                       SLazy |   10 |  30.19 ns |  0.213 ns |  0.188 ns |  0.79 |    0.01 |
|                             |      |           |           |           |       |         |
|                        **Lazy** |  **100** |  **95.82 ns** |  **1.104 ns** |  **1.032 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication |  100 |  96.57 ns |  0.873 ns |  0.774 ns |  1.01 |    0.02 |
|                       SLazy |  100 |  85.34 ns |  0.895 ns |  0.793 ns |  0.89 |    0.01 |
|                             |      |           |           |           |       |         |
|                        **Lazy** | **1000** | **563.30 ns** |  **4.441 ns** |  **4.154 ns** |  **1.00** |    **0.00** |
| LazyExecutionAndPublication | 1000 | 572.02 ns | 11.437 ns | 15.269 ns |  1.02 |    0.03 |
|                       SLazy | 1000 | 551.57 ns |  3.831 ns |  3.583 ns |  0.98 |    0.01 |

