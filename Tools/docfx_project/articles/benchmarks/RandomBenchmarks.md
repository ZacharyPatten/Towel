# Random

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=6.0.100-preview.4.21255.9
  [Host]     : .NET Core 5.0.7 (CoreCLR 5.0.721.25508, CoreFX 5.0.721.25508), X64 RyuJIT
  Job-TGYVEK : .NET Core 5.0.7 (CoreCLR 5.0.721.25508, CoreFX 5.0.721.25508), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                       Method |      N |         Mean |      Error |     StdDev |       Median |
|----------------------------- |------- |-------------:|-----------:|-----------:|-------------:|
| **Next_IEnumerable_TotalWeight** |     **10** |     **2.036 μs** |  **0.0444 μs** |  **0.1207 μs** |     **2.000 μs** |
|             Next_IEnumerable |     10 |     2.268 μs |  0.0482 μs |  0.0818 μs |     2.200 μs |
| **Next_IEnumerable_TotalWeight** |    **100** |     **3.206 μs** |  **0.0681 μs** |  **0.1099 μs** |     **3.200 μs** |
|             Next_IEnumerable |    100 |     4.832 μs |  0.0911 μs |  0.1307 μs |     4.800 μs |
| **Next_IEnumerable_TotalWeight** |   **1000** |    **14.308 μs** |  **0.2211 μs** |  **0.1847 μs** |    **14.400 μs** |
|             Next_IEnumerable |   1000 |    29.731 μs |  0.5110 μs |  0.8538 μs |    29.400 μs |
| **Next_IEnumerable_TotalWeight** |  **10000** |   **123.638 μs** |  **0.1342 μs** |  **0.1121 μs** |   **123.700 μs** |
|             Next_IEnumerable |  10000 |   268.193 μs |  2.7887 μs |  2.4721 μs |   267.650 μs |
| **Next_IEnumerable_TotalWeight** | **100000** | **1,230.176 μs** | **23.7936 μs** | **24.4342 μs** | **1,223.600 μs** |
|             Next_IEnumerable | 100000 | 2,676.489 μs | 51.8394 μs | 55.4675 μs | 2,664.600 μs |

