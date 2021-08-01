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
|   **Lazy** |    **1** |  **30.25 ns** |  **0.545 ns** |  **0.509 ns** |  **30.17 ns** |  **1.00** |    **0.00** |
|  SLazy |    1 |  23.57 ns |  0.297 ns |  0.277 ns |  23.47 ns |  0.78 |    0.02 |
|        |      |           |           |           |           |       |         |
|   **Lazy** |   **10** |  **36.88 ns** |  **0.761 ns** |  **1.910 ns** |  **36.69 ns** |  **1.00** |    **0.00** |
|  SLazy |   10 |  27.13 ns |  0.158 ns |  0.140 ns |  27.09 ns |  0.72 |    0.04 |
|        |      |           |           |           |           |       |         |
|   **Lazy** |  **100** |  **90.31 ns** |  **1.656 ns** |  **2.375 ns** |  **89.69 ns** |  **1.00** |    **0.00** |
|  SLazy |  100 |  81.43 ns |  1.630 ns |  2.586 ns |  80.60 ns |  0.91 |    0.02 |
|        |      |           |           |           |           |       |         |
|   **Lazy** | **1000** | **531.13 ns** |  **3.800 ns** |  **3.173 ns** | **529.85 ns** |  **1.00** |    **0.00** |
|  SLazy | 1000 | 530.96 ns | 10.452 ns | 17.749 ns | 521.25 ns |  1.03 |    0.03 |

