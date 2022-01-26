# Data Structures

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1469 (21H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.200-preview.21617.4
  [Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
  DefaultJob : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT


```
|                         Method | RandomTestData |            Mean |         Error |         StdDev |
|------------------------------- |--------------- |----------------:|--------------:|---------------:|
|                  **ListArray_Add** |   **Person[1000]** |    **11,953.79 ns** |    **238.245 ns** |     **512.847 ns** |
|      ListArray_AddWithCapacity |   Person[1000] |    11,264.89 ns |    126.023 ns |     111.716 ns |
|                 ListLinked_Add |   Person[1000] |    14,724.64 ns |    260.631 ns |     243.795 ns |
|             QueueArray_Enqueue |   Person[1000] |    10,033.62 ns |    156.998 ns |     146.856 ns |
| QueueArray_EnqueueWithCapacity |   Person[1000] |    10,556.92 ns |    121.369 ns |     107.590 ns |
|            QueueLinked_Enqueue |   Person[1000] |    11,149.43 ns |    222.536 ns |     601.639 ns |
|                StackArray_Push |   Person[1000] |     6,784.71 ns |    133.005 ns |     271.694 ns |
|    StackArray_PushWithCapacity |   Person[1000] |     5,827.44 ns |     78.805 ns |      73.715 ns |
|               StackLinked_Push |   Person[1000] |     9,975.14 ns |    179.241 ns |     167.662 ns |
|       AvlTreeLinked_AddRunTime |   Person[1000] |   978,537.46 ns | 37,366.842 ns | 108,407.985 ns |
|   AvlTreeLinked_AddCompileTime |   Person[1000] |   900,494.22 ns | 17,988.664 ns |  38,335.310 ns |
|        RedBlackTree_AddRunTime |   Person[1000] |   889,709.68 ns | 16,173.330 ns |  13,505.463 ns |
|    RedBlackTree_AddCompileTime |   Person[1000] | 1,034,283.42 ns | 20,464.748 ns |  58,717.198 ns |
|       SetHashLinked_AddRunTime |   Person[1000] |    65,663.44 ns |    928.168 ns |     868.209 ns |
|   SetHashLinked_AddCompileTime |   Person[1000] |    54,765.16 ns |    886.181 ns |     910.043 ns |
|                   SkipList_Add |   Person[1000] | 3,755,378.84 ns | 41,136.672 ns |  32,116.808 ns |
|                BTreeLinked_Add |   Person[1000] |   892,964.50 ns | 17,733.868 ns |  33,740.528 ns |
|                  **ListArray_Add** |    **Person[100]** |     **1,349.97 ns** |     **26.256 ns** |      **39.299 ns** |
|      ListArray_AddWithCapacity |    Person[100] |     1,012.64 ns |      6.842 ns |       6.400 ns |
|                 ListLinked_Add |    Person[100] |     1,510.59 ns |     29.630 ns |      48.683 ns |
|             QueueArray_Enqueue |    Person[100] |     1,187.26 ns |     23.653 ns |      39.519 ns |
| QueueArray_EnqueueWithCapacity |    Person[100] |     1,053.07 ns |     21.093 ns |      25.110 ns |
|            QueueLinked_Enqueue |    Person[100] |     1,110.71 ns |     21.718 ns |      23.238 ns |
|                StackArray_Push |    Person[100] |       837.50 ns |     15.113 ns |      17.404 ns |
|    StackArray_PushWithCapacity |    Person[100] |       596.88 ns |     11.342 ns |      11.648 ns |
|               StackLinked_Push |    Person[100] |     1,047.26 ns |     20.521 ns |      52.604 ns |
|       AvlTreeLinked_AddRunTime |    Person[100] |    59,298.06 ns |    961.996 ns |     852.784 ns |
|   AvlTreeLinked_AddCompileTime |    Person[100] |    54,780.93 ns |    867.949 ns |     677.637 ns |
|        RedBlackTree_AddRunTime |    Person[100] |    61,468.98 ns |  1,221.374 ns |   2,410.873 ns |
|    RedBlackTree_AddCompileTime |    Person[100] |    59,874.35 ns |  1,130.306 ns |   1,691.789 ns |
|       SetHashLinked_AddRunTime |    Person[100] |     6,387.72 ns |    107.977 ns |     101.002 ns |
|   SetHashLinked_AddCompileTime |    Person[100] |     5,188.74 ns |     68.079 ns |      60.350 ns |
|                   SkipList_Add |    Person[100] |   648,496.75 ns |  8,526.228 ns |   7,975.439 ns |
|                BTreeLinked_Add |    Person[100] |    51,567.06 ns |    921.117 ns |     861.614 ns |
|                  **ListArray_Add** |     **Person[10]** |       **183.83 ns** |      **3.361 ns** |       **3.143 ns** |
|      ListArray_AddWithCapacity |     Person[10] |       118.53 ns |      1.141 ns |       1.067 ns |
|                 ListLinked_Add |     Person[10] |       149.97 ns |      1.812 ns |       1.607 ns |
|             QueueArray_Enqueue |     Person[10] |       142.48 ns |      2.083 ns |       1.846 ns |
| QueueArray_EnqueueWithCapacity |     Person[10] |       119.23 ns |      1.473 ns |       1.306 ns |
|            QueueLinked_Enqueue |     Person[10] |       119.53 ns |      2.377 ns |       2.920 ns |
|                StackArray_Push |     Person[10] |       147.56 ns |      2.933 ns |       2.880 ns |
|    StackArray_PushWithCapacity |     Person[10] |        70.39 ns |      1.349 ns |       1.262 ns |
|               StackLinked_Push |     Person[10] |       114.01 ns |      1.840 ns |       1.722 ns |
|       AvlTreeLinked_AddRunTime |     Person[10] |     2,569.00 ns |     49.350 ns |      56.832 ns |
|   AvlTreeLinked_AddCompileTime |     Person[10] |     2,389.07 ns |     47.099 ns |      46.258 ns |
|        RedBlackTree_AddRunTime |     Person[10] |     2,924.05 ns |     57.001 ns |      90.409 ns |
|    RedBlackTree_AddCompileTime |     Person[10] |     2,760.31 ns |     48.891 ns |      71.664 ns |
|       SetHashLinked_AddRunTime |     Person[10] |       701.38 ns |      5.353 ns |       4.180 ns |
|   SetHashLinked_AddCompileTime |     Person[10] |       560.00 ns |      6.663 ns |       5.907 ns |
|                   SkipList_Add |     Person[10] |   577,439.00 ns | 11,119.887 ns |  12,359.725 ns |
|                BTreeLinked_Add |     Person[10] |     2,011.85 ns |     39.734 ns |      37.167 ns |

