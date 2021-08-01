# Slazy Initialization

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1110 (20H2/October2020Update)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.6.21355.2
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  Job-VGOPQH : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
| Method |     N |         Mean |        Error |       StdDev |       Median | Ratio | RatioSD |
|------- |------ |-------------:|-------------:|-------------:|-------------:|------:|--------:|
|   **Lazy** |     **1** |     **500.0 ns** |      **0.00 ns** |      **0.00 ns** |     **500.0 ns** |  **1.00** |    **0.00** |
|  SLazy |     1 |     341.8 ns |     23.05 ns |     67.23 ns |     300.0 ns |  0.63 |    0.12 |
|        |       |              |              |              |              |       |         |
|   **Lazy** |    **10** |     **683.8 ns** |     **15.48 ns** |     **37.10 ns** |     **700.0 ns** |  **1.00** |    **0.00** |
|  SLazy |    10 |     433.3 ns |     13.44 ns |     37.48 ns |     450.0 ns |  0.63 |    0.07 |
|        |       |              |              |              |              |       |         |
|   **Lazy** |   **100** |   **2,941.2 ns** |     **60.21 ns** |     **61.83 ns** |   **2,900.0 ns** |  **1.00** |    **0.00** |
|  SLazy |   100 |   2,426.1 ns |     48.96 ns |     61.92 ns |   2,400.0 ns |  0.82 |    0.03 |
|        |       |              |              |              |              |       |         |
|   **Lazy** |  **1000** |  **25,069.2 ns** |    **310.69 ns** |    **259.44 ns** |  **25,000.0 ns** |  **1.00** |    **0.00** |
|  SLazy |  1000 |  20,933.3 ns |    106.93 ns |     83.48 ns |  20,950.0 ns |  0.83 |    0.01 |
|        |       |              |              |              |              |       |         |
|   **Lazy** | **10000** | **234,102.1 ns** | **14,330.77 ns** | **41,117.66 ns** | **236,800.0 ns** |  **1.00** |    **0.00** |
|  SLazy | 10000 | 201,431.0 ns |  6,087.77 ns | 16,665.16 ns | 203,300.0 ns |  0.87 |    0.14 |

