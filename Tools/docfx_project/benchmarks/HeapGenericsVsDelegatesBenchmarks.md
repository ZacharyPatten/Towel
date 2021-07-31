# Heap Generics Vs Delegates

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1110 (20H2/October2020Update)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.6.21355.2
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  DefaultJob : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT


```
|        Method | RandomTestData |        Mean |     Error |    StdDev |
|-------------- |--------------- |------------:|----------:|----------:|
|   **HeapGAD_Add** |   **Person[1000]** | **29,591.3 ns** | **203.10 ns** | **169.60 ns** |
|    HeapGA_Add |   Person[1000] | 26,643.2 ns |  97.54 ns |  86.47 ns |
|     HeapG_Add |   Person[1000] | 30,825.6 ns | 116.24 ns |  97.06 ns |
| HeapD_Enqueue |   Person[1000] | 32,277.3 ns | 145.15 ns | 135.77 ns |
|   **HeapGAD_Add** |    **Person[100]** |  **2,628.3 ns** |  **16.33 ns** |  **14.47 ns** |
|    HeapGA_Add |    Person[100] |  2,455.2 ns |  48.57 ns |  77.04 ns |
|     HeapG_Add |    Person[100] |  2,820.9 ns |  13.00 ns |  11.53 ns |
| HeapD_Enqueue |    Person[100] |  3,034.3 ns |  12.72 ns |  11.90 ns |
|   **HeapGAD_Add** |     **Person[10]** |    **217.2 ns** |   **0.99 ns** |   **0.87 ns** |
|    HeapGA_Add |     Person[10] |    197.9 ns |   1.36 ns |   1.27 ns |
|     HeapG_Add |     Person[10] |    239.0 ns |   4.77 ns |   9.52 ns |
| HeapD_Enqueue |     Person[10] |    251.6 ns |   5.00 ns |   6.14 ns |

