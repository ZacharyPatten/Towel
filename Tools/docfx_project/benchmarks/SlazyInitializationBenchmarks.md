# Slazy Initialization

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1110 (20H2/October2020Update)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.6.21355.2
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  Job-RLZZYG : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
| Method |     N |         Mean |       Error |       StdDev |       Median | Ratio | RatioSD |
|------- |------ |-------------:|------------:|-------------:|-------------:|------:|--------:|
|   **Lazy** |     **1** |     **429.2 ns** |    **15.93 ns** |     **40.84 ns** |     **450.0 ns** |  **1.00** |    **0.00** |
|  SLazy |     1 |     331.3 ns |    16.62 ns |     48.75 ns |     300.0 ns |  0.78 |    0.15 |
|        |       |              |             |              |              |       |         |
|   **Lazy** |    **10** |     **700.0 ns** |     **0.00 ns** |      **0.00 ns** |     **700.0 ns** |  **1.00** |    **0.00** |
|  SLazy |    10 |     483.5 ns |    13.31 ns |     37.31 ns |     500.0 ns |  0.69 |    0.05 |
|        |       |              |             |              |              |       |         |
|   **Lazy** |   **100** |   **2,976.9 ns** |    **52.52 ns** |     **43.85 ns** |   **3,000.0 ns** |  **1.00** |    **0.00** |
|  SLazy |   100 |   2,392.3 ns |    33.21 ns |     27.74 ns |   2,400.0 ns |  0.80 |    0.02 |
|        |       |              |             |              |              |       |         |
|   **Lazy** |  **1000** |  **25,323.1 ns** |   **170.44 ns** |    **142.33 ns** |  **25,300.0 ns** |  **1.00** |    **0.00** |
|  SLazy |  1000 |  20,660.0 ns |   251.69 ns |    235.43 ns |  20,500.0 ns |  0.81 |    0.01 |
|        |       |              |             |              |              |       |         |
|   **Lazy** | **10000** | **226,109.6 ns** | **9,479.85 ns** | **27,046.53 ns** | **237,300.0 ns** |  **1.00** |    **0.00** |
|  SLazy | 10000 | 203,530.8 ns | 2,464.13 ns |  2,057.66 ns | 203,400.0 ns |  0.82 |    0.05 |

