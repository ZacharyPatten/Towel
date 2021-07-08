# decimal To English Words

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=6.0.100-preview.4.21255.9
  [Host]     : .NET Core 5.0.7 (CoreCLR 5.0.721.25508, CoreFX 5.0.721.25508), X64 RyuJIT
  DefaultJob : .NET Core 5.0.7 (CoreCLR 5.0.721.25508, CoreFX 5.0.721.25508), X64 RyuJIT


```
|                 Method |  Range |     Mean |    Error |   StdDev |
|----------------------- |------- |---------:|---------:|---------:|
| **Decimal_ToEnglishWords** |     **10** | **63.61 ms** | **0.248 ms** | **0.207 ms** |
| **Decimal_ToEnglishWords** |    **100** | **63.09 ms** | **0.224 ms** | **0.199 ms** |
| **Decimal_ToEnglishWords** |   **1000** | **58.96 ms** | **0.226 ms** | **0.211 ms** |
| **Decimal_ToEnglishWords** |  **10000** | **60.75 ms** | **0.222 ms** | **0.185 ms** |
| **Decimal_ToEnglishWords** | **100000** | **59.66 ms** | **0.349 ms** | **0.309 ms** |

