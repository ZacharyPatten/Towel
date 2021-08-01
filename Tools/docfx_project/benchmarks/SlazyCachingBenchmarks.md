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
| Method |    N |      Mean |     Error |   StdDev | Ratio | RatioSD |
|------- |----- |----------:|----------:|---------:|------:|--------:|
|   **Lazy** |    **1** |  **28.52 ns** |  **0.307 ns** | **0.302 ns** |  **1.00** |    **0.00** |
|  SLazy |    1 |  22.58 ns |  0.152 ns | 0.142 ns |  0.79 |    0.01 |
|        |      |           |           |          |       |         |
|   **Lazy** |   **10** |  **34.82 ns** |  **0.712 ns** | **1.044 ns** |  **1.00** |    **0.00** |
|  SLazy |   10 |  27.05 ns |  0.420 ns | 0.654 ns |  0.78 |    0.03 |
|        |      |           |           |          |       |         |
|   **Lazy** |  **100** |  **87.83 ns** |  **1.404 ns** | **1.968 ns** |  **1.00** |    **0.00** |
|  SLazy |  100 |  77.15 ns |  0.483 ns | 0.428 ns |  0.87 |    0.02 |
|        |      |           |           |          |       |         |
|   **Lazy** | **1000** | **518.99 ns** | **10.327 ns** | **9.660 ns** |  **1.00** |    **0.00** |
|  SLazy | 1000 | 500.72 ns |  4.551 ns | 4.257 ns |  0.97 |    0.02 |

