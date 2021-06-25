# Map vs Dictionary (Add)

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=6.0.100-preview.4.21255.9
  [Host]     : .NET Core 5.0.7 (CoreCLR 5.0.721.25508, CoreFX 5.0.721.25508), X64 RyuJIT
  DefaultJob : .NET Core 5.0.7 (CoreCLR 5.0.721.25508, CoreFX 5.0.721.25508), X64 RyuJIT


```
|       Method |     N |         Mean |       Error |      StdDev |       Median |
|------------- |------ |-------------:|------------:|------------:|-------------:|
| **MapDelegates** |    **10** |     **513.5 ns** |    **10.23 ns** |    **23.70 ns** |     **504.5 ns** |
|   MapStructs |    10 |     428.3 ns |     8.42 ns |     9.70 ns |     426.7 ns |
|   Dictionary |    10 |     193.0 ns |     3.86 ns |     6.96 ns |     189.7 ns |
| **MapDelegates** |   **100** |   **4,132.6 ns** |    **82.08 ns** |    **87.82 ns** |   **4,108.1 ns** |
|   MapStructs |   100 |   3,494.3 ns |    24.75 ns |    23.15 ns |   3,491.9 ns |
|   Dictionary |   100 |   1,532.7 ns |    22.04 ns |    18.41 ns |   1,526.3 ns |
| **MapDelegates** |  **1000** |  **32,341.7 ns** |   **247.40 ns** |   **219.32 ns** |  **32,399.8 ns** |
|   MapStructs |  1000 |  27,707.2 ns |   136.99 ns |   128.14 ns |  27,683.7 ns |
|   Dictionary |  1000 |  16,244.3 ns |   323.92 ns |   724.49 ns |  16,007.0 ns |
| **MapDelegates** | **10000** | **504,423.2 ns** | **5,092.85 ns** | **4,763.85 ns** | **502,377.0 ns** |
|   MapStructs | 10000 | 461,974.0 ns | 2,316.02 ns | 2,053.09 ns | 462,227.0 ns |
|   Dictionary | 10000 | 253,072.9 ns | 1,924.67 ns | 1,706.17 ns | 252,354.0 ns |

