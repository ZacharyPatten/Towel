# decimal To English Words

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1110 (20H2/October2020Update)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.6.21355.2
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  DefaultJob : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT


```
|                 Method |  Range |     Mean |    Error |   StdDev |
|----------------------- |------- |---------:|---------:|---------:|
| **Decimal_ToEnglishWords** |     **10** | **68.08 ms** | **0.632 ms** | **0.561 ms** |
| **Decimal_ToEnglishWords** |    **100** | **65.27 ms** | **0.269 ms** | **0.239 ms** |
| **Decimal_ToEnglishWords** |   **1000** | **61.95 ms** | **1.065 ms** | **0.889 ms** |
| **Decimal_ToEnglishWords** |  **10000** | **64.06 ms** | **0.822 ms** | **0.769 ms** |
| **Decimal_ToEnglishWords** | **100000** | **62.67 ms** | **1.125 ms** | **1.105 ms** |

