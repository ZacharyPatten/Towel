# Slazy Initialization

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1110 (20H2/October2020Update)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.6.21355.2
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  Job-HBXSTX : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
| Method |     N |         Mean |       Error |       StdDev |       Median | Ratio | RatioSD |
|------- |------ |-------------:|------------:|-------------:|-------------:|------:|--------:|
|   **Lazy** |     **1** |     **453.5 ns** |    **19.06 ns** |     **55.90 ns** |     **500.0 ns** |  **1.00** |    **0.00** |
|  SLazy |     1 |     290.7 ns |    10.05 ns |     29.16 ns |     300.0 ns |  0.65 |    0.11 |
|        |       |              |             |              |              |       |         |
|   **Lazy** |    **10** |     **595.8 ns** |    **15.70 ns** |     **20.41 ns** |     **600.0 ns** |  **1.00** |    **0.00** |
|  SLazy |    10 |     457.7 ns |    17.82 ns |     51.71 ns |     500.0 ns |  0.78 |    0.11 |
|        |       |              |             |              |              |       |         |
|   **Lazy** |   **100** |   **2,895.5 ns** |    **58.81 ns** |     **72.22 ns** |   **2,900.0 ns** |  **1.00** |    **0.00** |
|  SLazy |   100 |   2,270.6 ns |    45.74 ns |     46.97 ns |   2,300.0 ns |  0.78 |    0.03 |
|        |       |              |             |              |              |       |         |
|   **Lazy** |  **1000** |  **24,678.6 ns** |   **283.93 ns** |    **251.70 ns** |  **24,800.0 ns** |  **1.00** |    **0.00** |
|  SLazy |  1000 |  19,550.0 ns |     0.00 ns |      0.00 ns |  19,550.0 ns |  0.79 |    0.01 |
|        |       |              |             |              |              |       |         |
|   **Lazy** | **10000** | **200,038.9 ns** | **7,143.13 ns** | **19,912.18 ns** | **191,300.0 ns** |  **1.00** |    **0.00** |
|  SLazy | 10000 | 193,445.5 ns | 2,806.10 ns |  3,446.15 ns | 192,350.0 ns |  0.87 |    0.09 |

