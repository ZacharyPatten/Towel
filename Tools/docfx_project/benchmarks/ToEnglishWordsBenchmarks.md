# decimal To English Words

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1110 (20H2/October2020Update)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.6.21355.2
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  Job-FANCYY : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                 Method |             Range |      Mean |    Error |    StdDev |       Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------------- |------------------ |----------:|---------:|----------:|------------:|------:|------:|----------:|
|          **StringBuilder** | **0...1 (+=.000005)** |  **66.20 ms** | **1.313 ms** |  **2.798 ms** |  **28000.0000** |     **-** |     **-** |    **114 MB** |
| Decimal_ToEnglishWords | 0...1 (+=.000005) |  43.15 ms | 0.606 ms |  0.506 ms |   7000.0000 |     - |     - |     28 MB |
|          **StringBuilder** | **0...1000000 (+=1)** | **293.35 ms** | **5.753 ms** | **11.357 ms** | **121000.0000** |     **-** |     **-** |    **486 MB** |
| Decimal_ToEnglishWords | 0...1000000 (+=1) | 215.54 ms | 3.926 ms |  7.658 ms |  30000.0000 |     - |     - |    123 MB |

