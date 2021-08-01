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
| Method |    N |      Mean |     Error |    StdDev | Ratio | RatioSD |
|------- |----- |----------:|----------:|----------:|------:|--------:|
|   **Lazy** |    **1** |  **31.01 ns** |  **0.449 ns** |  **0.398 ns** |  **1.00** |    **0.00** |
|  SLazy |    1 |  26.70 ns |  0.252 ns |  0.236 ns |  0.86 |    0.01 |
|        |      |           |           |           |       |         |
|   **Lazy** |   **10** |  **36.83 ns** |  **0.474 ns** |  **0.420 ns** |  **1.00** |    **0.00** |
|  SLazy |   10 |  29.16 ns |  0.404 ns |  0.378 ns |  0.79 |    0.01 |
|        |      |           |           |           |       |         |
|   **Lazy** |  **100** |  **91.55 ns** |  **1.279 ns** |  **1.134 ns** |  **1.00** |    **0.00** |
|  SLazy |  100 |  81.45 ns |  1.136 ns |  1.007 ns |  0.89 |    0.02 |
|        |      |           |           |           |       |         |
|   **Lazy** | **1000** | **552.06 ns** | **11.075 ns** | **15.159 ns** |  **1.00** |    **0.00** |
|  SLazy | 1000 | 518.34 ns |  3.411 ns |  3.024 ns |  0.93 |    0.03 |

