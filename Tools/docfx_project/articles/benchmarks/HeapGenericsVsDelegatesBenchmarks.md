# Heap Generics Vs Delegates

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=6.0.100-preview.4.21255.9
  [Host]     : .NET Core 5.0.7 (CoreCLR 5.0.721.25508, CoreFX 5.0.721.25508), X64 RyuJIT
  DefaultJob : .NET Core 5.0.7 (CoreCLR 5.0.721.25508, CoreFX 5.0.721.25508), X64 RyuJIT


```
|        Method | RandomTestData |        Mean |     Error |    StdDev |
|-------------- |--------------- |------------:|----------:|----------:|
|   **HeapGAD_Add** |   **Person[1000]** | **22,771.6 ns** |  **99.96 ns** |  **93.50 ns** |
|    HeapGA_Add |   Person[1000] | 22,650.7 ns |  98.84 ns |  92.45 ns |
|     HeapG_Add |   Person[1000] | 25,845.0 ns |  84.31 ns |  78.86 ns |
| HeapD_Enqueue |   Person[1000] | 28,742.8 ns | 112.38 ns | 105.12 ns |
|   **HeapGAD_Add** |    **Person[100]** |  **1,864.2 ns** |   **9.48 ns** |   **8.40 ns** |
|    HeapGA_Add |    Person[100] |  1,988.8 ns |  15.85 ns |  14.05 ns |
|     HeapG_Add |    Person[100] |  2,275.9 ns |  11.77 ns |  11.01 ns |
| HeapD_Enqueue |    Person[100] |  2,749.1 ns |  13.29 ns |  12.43 ns |
|   **HeapGAD_Add** |     **Person[10]** |    **189.8 ns** |   **0.76 ns** |   **0.71 ns** |
|    HeapGA_Add |     Person[10] |    178.1 ns |   0.43 ns |   0.38 ns |
|     HeapG_Add |     Person[10] |    208.4 ns |   1.61 ns |   1.26 ns |
| HeapD_Enqueue |     Person[10] |    235.8 ns |   0.65 ns |   0.61 ns |

