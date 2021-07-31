# Map vs Dictionary (Add)

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1110 (20H2/October2020Update)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.6.21355.2
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  DefaultJob : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT


```
|       Method |     N |         Mean |       Error |       StdDev |
|------------- |------ |-------------:|------------:|-------------:|
| **MapDelegates** |    **10** |     **520.4 ns** |     **7.94 ns** |      **8.16 ns** |
|   MapStructs |    10 |     455.2 ns |     9.10 ns |     12.76 ns |
|   Dictionary |    10 |     197.4 ns |     3.29 ns |      2.92 ns |
| **MapDelegates** |   **100** |   **4,133.3 ns** |    **37.15 ns** |     **31.02 ns** |
|   MapStructs |   100 |   3,716.8 ns |    72.63 ns |    117.28 ns |
|   Dictionary |   100 |   1,700.2 ns |    33.61 ns |     57.07 ns |
| **MapDelegates** |  **1000** |  **32,807.2 ns** |   **372.95 ns** |    **330.61 ns** |
|   MapStructs |  1000 |  27,958.3 ns |   490.14 ns |    458.48 ns |
|   Dictionary |  1000 |  15,436.3 ns |   204.14 ns |    190.95 ns |
| **MapDelegates** | **10000** | **499,552.4 ns** | **4,540.48 ns** |  **3,791.51 ns** |
|   MapStructs | 10000 | 478,678.6 ns | 8,563.30 ns |  7,150.74 ns |
|   Dictionary | 10000 | 256,607.8 ns | 5,117.49 ns | 13,025.64 ns |

