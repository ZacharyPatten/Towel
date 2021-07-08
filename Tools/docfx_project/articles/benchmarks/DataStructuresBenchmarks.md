# Data Structures

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=6.0.100-preview.4.21255.9
  [Host]     : .NET Core 5.0.7 (CoreCLR 5.0.721.25508, CoreFX 5.0.721.25508), X64 RyuJIT
  DefaultJob : .NET Core 5.0.7 (CoreCLR 5.0.721.25508, CoreFX 5.0.721.25508), X64 RyuJIT


```
|                         Method | RandomTestData |          Mean |        Error |       StdDev |
|------------------------------- |--------------- |--------------:|-------------:|-------------:|
|                  **ListArray_Add** |   **Person[1000]** |  **10,395.55 ns** |    **40.364 ns** |    **37.757 ns** |
|      ListArray_AddWithCapacity |   Person[1000] |   9,713.90 ns |    38.997 ns |    36.478 ns |
|                            Add |   Person[1000] |  12,723.88 ns |    88.506 ns |    82.789 ns |
|             QueueArray_Enqueue |   Person[1000] |   8,346.96 ns |    43.790 ns |    40.961 ns |
| QueueArray_EnqueueWithCapacity |   Person[1000] |   8,723.28 ns |    63.384 ns |    56.188 ns |
|            QueueLinked_Enqueue |   Person[1000] |   9,368.39 ns |    38.625 ns |    36.130 ns |
|                StackArray_Push |   Person[1000] |   5,707.74 ns |    27.670 ns |    24.529 ns |
|    StackArray_PushWithCapacity |   Person[1000] |   5,022.83 ns |    69.428 ns |    64.943 ns |
|               StackLinked_Push |   Person[1000] |   8,928.06 ns |    68.731 ns |    64.291 ns |
|       AvlTreeLinked_AddRunTime |   Person[1000] | 857,591.34 ns | 3,945.878 ns | 3,497.918 ns |
|   AvlTreeLinked_AddCompileTime |   Person[1000] | 825,090.86 ns | 2,947.740 ns | 2,613.094 ns |
|        RedBlackTree_AddRunTime |   Person[1000] | 830,337.59 ns | 4,862.751 ns | 4,548.620 ns |
|    RedBlackTree_AddCompileTime |   Person[1000] | 790,710.41 ns | 4,552.720 ns | 4,035.868 ns |
|       SetHashLinked_AddRunTime |   Person[1000] |  60,008.45 ns |   197.993 ns |   185.203 ns |
|   SetHashLinked_AddCompileTime |   Person[1000] |  50,559.18 ns |   232.999 ns |   206.548 ns |
|                  **ListArray_Add** |    **Person[100]** |   **1,144.82 ns** |     **4.351 ns** |     **3.857 ns** |
|      ListArray_AddWithCapacity |    Person[100] |     968.13 ns |     3.011 ns |     2.817 ns |
|                            Add |    Person[100] |   1,258.12 ns |     6.515 ns |     6.094 ns |
|             QueueArray_Enqueue |    Person[100] |     974.70 ns |     3.872 ns |     3.622 ns |
| QueueArray_EnqueueWithCapacity |    Person[100] |     903.74 ns |     4.004 ns |     3.746 ns |
|            QueueLinked_Enqueue |    Person[100] |     926.65 ns |     2.728 ns |     2.552 ns |
|                StackArray_Push |    Person[100] |     769.47 ns |     3.979 ns |     3.722 ns |
|    StackArray_PushWithCapacity |    Person[100] |     523.01 ns |     1.624 ns |     1.519 ns |
|               StackLinked_Push |    Person[100] |     879.73 ns |     2.504 ns |     2.220 ns |
|       AvlTreeLinked_AddRunTime |    Person[100] |  49,414.86 ns |   245.010 ns |   204.595 ns |
|   AvlTreeLinked_AddCompileTime |    Person[100] |  46,861.50 ns |   156.177 ns |   130.415 ns |
|        RedBlackTree_AddRunTime |    Person[100] |  53,245.98 ns |   264.847 ns |   247.738 ns |
|    RedBlackTree_AddCompileTime |    Person[100] |  48,926.35 ns |   198.586 ns |   155.043 ns |
|       SetHashLinked_AddRunTime |    Person[100] |   5,726.09 ns |    30.758 ns |    25.684 ns |
|   SetHashLinked_AddCompileTime |    Person[100] |   4,603.19 ns |    29.869 ns |    26.478 ns |
|                  **ListArray_Add** |     **Person[10]** |     **159.38 ns** |     **2.217 ns** |     **1.965 ns** |
|      ListArray_AddWithCapacity |     Person[10] |     104.96 ns |     0.495 ns |     0.463 ns |
|                            Add |     Person[10] |     128.67 ns |     0.469 ns |     0.439 ns |
|             QueueArray_Enqueue |     Person[10] |     118.15 ns |     0.516 ns |     0.483 ns |
| QueueArray_EnqueueWithCapacity |     Person[10] |      99.58 ns |     0.589 ns |     0.551 ns |
|            QueueLinked_Enqueue |     Person[10] |     100.54 ns |     0.305 ns |     0.255 ns |
|                StackArray_Push |     Person[10] |     122.37 ns |     0.453 ns |     0.401 ns |
|    StackArray_PushWithCapacity |     Person[10] |      60.02 ns |     0.345 ns |     0.323 ns |
|               StackLinked_Push |     Person[10] |      96.70 ns |     1.354 ns |     1.200 ns |
|       AvlTreeLinked_AddRunTime |     Person[10] |   2,317.13 ns |    13.178 ns |    11.682 ns |
|   AvlTreeLinked_AddCompileTime |     Person[10] |   2,086.63 ns |    11.263 ns |    10.535 ns |
|        RedBlackTree_AddRunTime |     Person[10] |   2,631.92 ns |    11.882 ns |    11.114 ns |
|    RedBlackTree_AddCompileTime |     Person[10] |   2,534.39 ns |     7.265 ns |     6.440 ns |
|       SetHashLinked_AddRunTime |     Person[10] |     621.49 ns |     3.033 ns |     2.837 ns |
|   SetHashLinked_AddCompileTime |     Person[10] |     497.69 ns |     2.289 ns |     2.141 ns |

