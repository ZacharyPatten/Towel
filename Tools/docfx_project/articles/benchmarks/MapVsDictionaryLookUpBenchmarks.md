# Map vs Dictionary (Look Up)

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=6.0.100-preview.4.21255.9
  [Host]     : .NET Core 5.0.7 (CoreCLR 5.0.721.25508, CoreFX 5.0.721.25508), X64 RyuJIT
  Job-HLOVVA : .NET Core 5.0.7 (CoreCLR 5.0.721.25508, CoreFX 5.0.721.25508), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|       Method |     N |         Mean |       Error |      StdDev |       Median |
|------------- |------ |-------------:|------------:|------------:|-------------:|
| **MapDelegates** |    **10** |     **677.1 ns** |    **17.38 ns** |    **42.29 ns** |     **700.0 ns** |
|   MapStructs |    10 |     462.0 ns |    16.54 ns |    48.78 ns |     500.0 ns |
|   Dictionary |    10 |     269.0 ns |    15.76 ns |    46.48 ns |     300.0 ns |
| **MapDelegates** |   **100** |   **2,533.3 ns** |    **52.16 ns** |    **48.80 ns** |   **2,500.0 ns** |
|   MapStructs |   100 |   1,481.8 ns |    32.15 ns |    39.48 ns |   1,500.0 ns |
|   Dictionary |   100 |   1,081.2 ns |    25.47 ns |    39.66 ns |   1,100.0 ns |
| **MapDelegates** |  **1000** |  **21,400.0 ns** |   **334.05 ns** |   **296.13 ns** |  **21,450.0 ns** |
|   MapStructs |  1000 |  14,171.4 ns |   173.80 ns |   154.07 ns |  14,200.0 ns |
|   Dictionary |  1000 |   8,407.7 ns |    59.10 ns |    49.35 ns |   8,400.0 ns |
| **MapDelegates** | **10000** | **209,200.0 ns** | **3,929.61 ns** | **8,458.91 ns** | **206,150.0 ns** |
|   MapStructs | 10000 | 121,888.1 ns | 2,361.60 ns | 2,811.31 ns | 121,050.0 ns |
|   Dictionary | 10000 |  81,471.4 ns | 1,212.89 ns | 1,075.19 ns |  81,050.0 ns |

