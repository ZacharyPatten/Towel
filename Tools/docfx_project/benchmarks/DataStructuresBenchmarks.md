# Data Structures

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1110 (20H2/October2020Update)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.6.21355.2
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  DefaultJob : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT


```
|                         Method | RandomTestData |          Mean |         Error |        StdDev |        Median |
|------------------------------- |--------------- |--------------:|--------------:|--------------:|--------------:|
|                  **ListArray_Add** |   **Person[1000]** |  **11,015.90 ns** |     **81.497 ns** |     **72.245 ns** |  **10,998.10 ns** |
|      ListArray_AddWithCapacity |   Person[1000] |  10,335.02 ns |    121.002 ns |    113.185 ns |  10,298.58 ns |
|                            Add |   Person[1000] |  13,570.85 ns |    140.211 ns |    109.468 ns |  13,570.42 ns |
|             QueueArray_Enqueue |   Person[1000] |   9,458.07 ns |     55.164 ns |     48.902 ns |   9,461.79 ns |
| QueueArray_EnqueueWithCapacity |   Person[1000] |   9,624.94 ns |     65.120 ns |     57.727 ns |   9,618.21 ns |
|            QueueLinked_Enqueue |   Person[1000] |  10,308.74 ns |    201.484 ns |    188.468 ns |  10,296.75 ns |
|                StackArray_Push |   Person[1000] |   6,319.35 ns |    100.663 ns |     89.235 ns |   6,287.48 ns |
|    StackArray_PushWithCapacity |   Person[1000] |   5,155.39 ns |     45.183 ns |     40.054 ns |   5,139.53 ns |
|               StackLinked_Push |   Person[1000] |   9,692.62 ns |     55.127 ns |     51.566 ns |   9,711.06 ns |
|       AvlTreeLinked_AddRunTime |   Person[1000] | 915,860.04 ns | 16,403.690 ns | 14,541.444 ns | 914,604.35 ns |
|   AvlTreeLinked_AddCompileTime |   Person[1000] | 873,177.48 ns | 17,206.094 ns | 25,220.469 ns | 875,975.59 ns |
|        RedBlackTree_AddRunTime |   Person[1000] | 854,808.08 ns | 17,055.066 ns | 38,843.016 ns | 856,955.57 ns |
|    RedBlackTree_AddCompileTime |   Person[1000] | 835,680.94 ns | 16,427.580 ns | 31,650.369 ns | 829,658.20 ns |
|       SetHashLinked_AddRunTime |   Person[1000] |  59,599.87 ns |  1,189.346 ns |  2,174.787 ns |  59,220.92 ns |
|   SetHashLinked_AddCompileTime |   Person[1000] |  52,755.17 ns |  1,042.306 ns |  1,741.459 ns |  52,718.70 ns |
|                  **ListArray_Add** |    **Person[100]** |   **1,150.45 ns** |     **22.362 ns** |     **27.462 ns** |   **1,139.59 ns** |
|      ListArray_AddWithCapacity |    Person[100] |     964.81 ns |     11.200 ns |      9.928 ns |     960.69 ns |
|                            Add |    Person[100] |   1,344.19 ns |     26.186 ns |     45.862 ns |   1,347.19 ns |
|             QueueArray_Enqueue |    Person[100] |   1,091.15 ns |     20.993 ns |     26.549 ns |   1,083.57 ns |
| QueueArray_EnqueueWithCapacity |    Person[100] |     997.42 ns |     18.816 ns |     34.876 ns |   1,002.33 ns |
|            QueueLinked_Enqueue |    Person[100] |     999.09 ns |      7.021 ns |      6.568 ns |     999.79 ns |
|                StackArray_Push |    Person[100] |     770.46 ns |      5.229 ns |      4.083 ns |     770.46 ns |
|    StackArray_PushWithCapacity |    Person[100] |     530.65 ns |      6.248 ns |      5.539 ns |     528.92 ns |
|               StackLinked_Push |    Person[100] |   1,002.70 ns |     19.979 ns |     44.272 ns |     985.68 ns |
|       AvlTreeLinked_AddRunTime |    Person[100] |  52,447.65 ns |    356.975 ns |    298.090 ns |  52,446.02 ns |
|   AvlTreeLinked_AddCompileTime |    Person[100] |  51,185.53 ns |    745.975 ns |    697.786 ns |  51,282.73 ns |
|        RedBlackTree_AddRunTime |    Person[100] |  53,718.77 ns |    702.302 ns |    586.454 ns |  53,601.14 ns |
|    RedBlackTree_AddCompileTime |    Person[100] |  54,187.34 ns |    588.843 ns |    459.730 ns |  54,144.57 ns |
|       SetHashLinked_AddRunTime |    Person[100] |   6,087.62 ns |    100.355 ns |    111.544 ns |   6,081.36 ns |
|   SetHashLinked_AddCompileTime |    Person[100] |   4,940.82 ns |     97.212 ns |    129.775 ns |   4,893.09 ns |
|                  **ListArray_Add** |     **Person[10]** |     **164.82 ns** |      **1.601 ns** |      **1.497 ns** |     **164.53 ns** |
|      ListArray_AddWithCapacity |     Person[10] |     109.52 ns |      0.738 ns |      0.654 ns |     109.40 ns |
|                            Add |     Person[10] |     137.26 ns |      2.738 ns |      3.839 ns |     135.87 ns |
|             QueueArray_Enqueue |     Person[10] |     126.00 ns |      0.580 ns |      0.484 ns |     125.93 ns |
| QueueArray_EnqueueWithCapacity |     Person[10] |     107.46 ns |      0.699 ns |      0.584 ns |     107.23 ns |
|            QueueLinked_Enqueue |     Person[10] |     107.07 ns |      1.075 ns |      0.898 ns |     106.74 ns |
|                StackArray_Push |     Person[10] |     133.98 ns |      1.253 ns |      1.111 ns |     133.74 ns |
|    StackArray_PushWithCapacity |     Person[10] |      59.79 ns |      0.592 ns |      0.525 ns |      59.77 ns |
|               StackLinked_Push |     Person[10] |     104.34 ns |      1.328 ns |      1.631 ns |     104.14 ns |
|       AvlTreeLinked_AddRunTime |     Person[10] |   2,323.73 ns |     44.924 ns |     48.068 ns |   2,313.07 ns |
|   AvlTreeLinked_AddCompileTime |     Person[10] |   2,311.27 ns |     35.614 ns |     33.314 ns |   2,296.56 ns |
|        RedBlackTree_AddRunTime |     Person[10] |   2,725.51 ns |     38.273 ns |     35.801 ns |   2,708.83 ns |
|    RedBlackTree_AddCompileTime |     Person[10] |   2,686.89 ns |     26.898 ns |     22.461 ns |   2,685.79 ns |
|       SetHashLinked_AddRunTime |     Person[10] |     635.50 ns |     11.374 ns |     10.640 ns |     630.80 ns |
|   SetHashLinked_AddCompileTime |     Person[10] |     499.24 ns |      7.215 ns |      6.396 ns |     498.29 ns |

