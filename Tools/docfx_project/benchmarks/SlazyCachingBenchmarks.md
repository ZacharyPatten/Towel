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
| Method |    N |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |
|------- |----- |----------:|----------:|----------:|----------:|------:|--------:|
|   **Lazy** |    **1** |  **28.52 ns** |  **0.462 ns** |  **0.409 ns** |  **28.44 ns** |  **1.00** |    **0.00** |
|  SLazy |    1 |  22.27 ns |  0.104 ns |  0.092 ns |  22.25 ns |  0.78 |    0.01 |
|        |      |           |           |           |           |       |         |
|   **Lazy** |   **10** |  **33.64 ns** |  **0.578 ns** |  **0.568 ns** |  **33.67 ns** |  **1.00** |    **0.00** |
|  SLazy |   10 |  27.18 ns |  0.533 ns |  1.000 ns |  26.67 ns |  0.83 |    0.03 |
|        |      |           |           |           |           |       |         |
|   **Lazy** |  **100** |  **86.80 ns** |  **0.590 ns** |  **0.492 ns** |  **86.75 ns** |  **1.00** |    **0.00** |
|  SLazy |  100 |  76.55 ns |  1.124 ns |  0.996 ns |  76.62 ns |  0.88 |    0.01 |
|        |      |           |           |           |           |       |         |
|   **Lazy** | **1000** | **531.80 ns** | **10.674 ns** | **22.748 ns** | **521.80 ns** |  **1.00** |    **0.00** |
|  SLazy | 1000 | 502.72 ns |  8.423 ns |  7.879 ns | 500.57 ns |  0.90 |    0.04 |

