# Map vs Dictionary (Look Up)

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
|       Method |     N |         Mean |       Error |      StdDev |       Median |
|------------- |------ |-------------:|------------:|------------:|-------------:|
| **MapDelegates** |    **10** |     **686.4 ns** |    **15.58 ns** |    **34.53 ns** |     **700.0 ns** |
|   MapStructs |    10 |     361.6 ns |    18.03 ns |    52.89 ns |     400.0 ns |
|   Dictionary |    10 |     251.5 ns |    17.81 ns |    52.22 ns |     300.0 ns |
| **MapDelegates** |   **100** |   **2,592.3 ns** |    **33.21 ns** |    **27.74 ns** |   **2,600.0 ns** |
|   MapStructs |   100 |   1,500.0 ns |     0.00 ns |     0.00 ns |   1,500.0 ns |
|   Dictionary |   100 |     985.7 ns |    21.61 ns |    35.50 ns |   1,000.0 ns |
| **MapDelegates** |  **1000** |  **21,391.7 ns** |   **101.57 ns** |    **79.30 ns** |  **21,400.0 ns** |
|   MapStructs |  1000 |  12,292.9 ns |   168.28 ns |   149.17 ns |  12,300.0 ns |
|   Dictionary |  1000 |   8,480.0 ns |   162.63 ns |   152.13 ns |   8,400.0 ns |
| **MapDelegates** | **10000** | **211,330.8 ns** | **3,541.49 ns** | **2,957.30 ns** | **209,400.0 ns** |
|   MapStructs | 10000 | 122,541.2 ns | 2,435.10 ns | 3,932.24 ns | 125,300.0 ns |
|   Dictionary | 10000 |  81,600.0 ns | 1,394.85 ns | 1,164.76 ns |  81,500.0 ns |

