# decimal To English Words

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1165 (20H2/October2020Update)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.7.21379.14
  [Host]     : .NET 5.0.9 (5.0.921.35908), X64 RyuJIT
  Job-HLNVHB : .NET 5.0.9 (5.0.921.35908), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|        Method |             Range |      Mean |    Error |   StdDev |       Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |------------------ |----------:|---------:|---------:|------------:|------:|------:|----------:|
| **StringBuilder** | **0...1 (+=.000005)** |  **63.53 ms** | **1.181 ms** | **2.641 ms** |  **29000.0000** |     **-** |     **-** |    **116 MB** |
|          Span | 0...1 (+=.000005) |  45.84 ms | 0.870 ms | 1.002 ms |   7000.0000 |     - |     - |     28 MB |
| **StringBuilder** | **0...1000000 (+=1)** | **282.17 ms** | **3.501 ms** | **3.274 ms** | **121000.0000** |     **-** |     **-** |    **486 MB** |
|          Span | 0...1000000 (+=1) | 207.02 ms | 1.308 ms | 1.223 ms |  30000.0000 |     - |     - |    123 MB |

