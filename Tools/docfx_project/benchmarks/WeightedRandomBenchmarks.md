# Random

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
|                       Method |      N |         Mean |      Error |      StdDev |       Median |
|----------------------------- |------- |-------------:|-----------:|------------:|-------------:|
| **Next_IEnumerable_TotalWeight** |     **10** |     **1.988 μs** |  **0.0438 μs** |   **0.1214 μs** |     **2.000 μs** |
|             Next_IEnumerable |     10 |     2.353 μs |  0.0488 μs |   0.0788 μs |     2.300 μs |
| **Next_IEnumerable_TotalWeight** |    **100** |     **3.306 μs** |  **0.0681 μs** |   **0.1343 μs** |     **3.300 μs** |
|             Next_IEnumerable |    100 |     4.863 μs |  0.0979 μs |   0.0915 μs |     4.850 μs |
| **Next_IEnumerable_TotalWeight** |   **1000** |    **14.569 μs** |  **0.1575 μs** |   **0.1316 μs** |    **14.500 μs** |
|             Next_IEnumerable |   1000 |    29.246 μs |  0.1516 μs |   0.1266 μs |    29.200 μs |
| **Next_IEnumerable_TotalWeight** |  **10000** |   **126.475 μs** |  **1.1942 μs** |   **0.9324 μs** |   **126.700 μs** |
|             Next_IEnumerable |  10000 |   273.327 μs |  5.4000 μs |   9.1695 μs |   269.700 μs |
| **Next_IEnumerable_TotalWeight** | **100000** | **1,242.976 μs** | **23.7169 μs** |  **24.3556 μs** | **1,227.600 μs** |
|             Next_IEnumerable | 100000 | 2,742.114 μs | 54.5632 μs | 126.4584 μs | 2,701.650 μs |

