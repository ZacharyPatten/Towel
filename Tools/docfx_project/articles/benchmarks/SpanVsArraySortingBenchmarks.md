# Span vs Array Sorting

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
|                 Method |     N |             Mean |           Error |          StdDev |           Median |
|----------------------- |------ |-----------------:|----------------:|----------------:|-----------------:|
|     **ArrayBubbleRunTime** |    **10** |       **1,956.9 ns** |        **40.67 ns** |        **83.08 ns** |       **2,000.0 ns** |
| ArrayBubbleCompileTime |    10 |         451.0 ns |        18.98 ns |        55.95 ns |         450.0 ns |
|      SpanBubbleRunTime |    10 |       1,918.3 ns |        39.90 ns |        89.24 ns |       1,900.0 ns |
|  SpanBubbleCompileTime |    10 |         400.0 ns |         0.00 ns |         0.00 ns |         400.0 ns |
|     **ArrayBubbleRunTime** |  **1000** |   **4,642,027.0 ns** |    **79,102.77 ns** |   **181,751.77 ns** |   **4,605,200.0 ns** |
| ArrayBubbleCompileTime |  1000 |   1,715,742.1 ns |    32,448.20 ns |    36,066.09 ns |   1,715,000.0 ns |
|      SpanBubbleRunTime |  1000 |   4,229,012.5 ns |   103,672.82 ns |   271,293.70 ns |   4,126,800.0 ns |
|  SpanBubbleCompileTime |  1000 |   1,377,350.0 ns |    27,003.38 ns |    23,937.80 ns |   1,385,700.0 ns |
|     **ArrayBubbleRunTime** | **10000** | **482,982,092.3 ns** | **1,263,001.85 ns** | **1,054,663.75 ns** | **483,339,000.0 ns** |
| ArrayBubbleCompileTime | 10000 | 180,410,985.7 ns | 1,325,364.52 ns | 1,174,901.13 ns | 180,101,750.0 ns |
|      SpanBubbleRunTime | 10000 | 418,354,530.8 ns | 1,024,717.35 ns |   855,685.40 ns | 418,427,200.0 ns |
|  SpanBubbleCompileTime | 10000 | 169,622,880.0 ns |   680,676.38 ns |   636,705.11 ns | 169,660,000.0 ns |

