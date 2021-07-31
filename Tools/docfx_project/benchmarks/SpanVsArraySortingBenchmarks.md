# Span vs Array Sorting

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
|                 Method |     N |             Mean |           Error |          StdDev |           Median |
|----------------------- |------ |-----------------:|----------------:|----------------:|-----------------:|
|     **ArrayBubbleRunTime** |    **10** |       **2,268.4 ns** |        **42.97 ns** |        **47.76 ns** |       **2,300.0 ns** |
| ArrayBubbleCompileTime |    10 |       1,101.1 ns |        23.74 ns |        66.19 ns |       1,100.0 ns |
|      SpanBubbleRunTime |    10 |       1,870.0 ns |        39.16 ns |        95.33 ns |       1,900.0 ns |
|  SpanBubbleCompileTime |    10 |         806.1 ns |        32.97 ns |        96.70 ns |         800.0 ns |
|     **ArrayBubbleRunTime** |  **1000** |   **4,418,744.8 ns** |    **74,527.92 ns** |   **177,123.48 ns** |   **4,361,000.0 ns** |
| ArrayBubbleCompileTime |  1000 |   4,829,523.7 ns |    77,343.79 ns |   196,864.63 ns |   4,759,050.0 ns |
|      SpanBubbleRunTime |  1000 |   6,032,968.8 ns |   116,032.05 ns |   113,958.99 ns |   6,031,150.0 ns |
|  SpanBubbleCompileTime |  1000 |   4,243,637.0 ns |    83,305.62 ns |   116,782.84 ns |   4,215,100.0 ns |
|     **ArrayBubbleRunTime** | **10000** | **455,368,007.1 ns** | **2,358,146.70 ns** | **2,090,435.63 ns** | **454,983,900.0 ns** |
| ArrayBubbleCompileTime | 10000 | 510,762,966.7 ns | 1,215,746.52 ns |   949,174.91 ns | 511,029,800.0 ns |
|      SpanBubbleRunTime | 10000 | 397,857,792.9 ns | 1,226,017.16 ns | 1,086,832.28 ns | 398,157,850.0 ns |
|  SpanBubbleCompileTime | 10000 | 484,632,850.0 ns | 1,375,325.98 ns | 1,219,190.66 ns | 484,314,750.0 ns |

