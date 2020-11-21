# Benchmarks

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

# Sorting Algorithms

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1198 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100
  [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT
  Job-BQFFMI : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                   Method |     N |             Mean |             Error |            StdDev |           Median |
|------------------------- |------ |-----------------:|------------------:|------------------:|-----------------:|
|          **SystemArraySort** |    **10** |         **373.6 ns** |          **23.93 ns** |          **65.52 ns** |         **400.0 ns** |
|  SystemArraySortDelegate |    10 |         731.8 ns |          23.06 ns |          63.51 ns |         750.0 ns |
| SystemArraySortIComparer |    10 |       3,086.4 ns |          64.29 ns |          78.95 ns |       3,050.0 ns |
|            BubbleRunTime |    10 |       1,787.0 ns |          38.30 ns |          80.79 ns |       1,750.0 ns |
|        BubbleCompileTime |    10 |         500.0 ns |           0.00 ns |           0.00 ns |         500.0 ns |
|         SelectionRunTime |    10 |       1,476.9 ns |          31.39 ns |          42.97 ns |       1,500.0 ns |
|     SelectionCompileTime |    10 |         386.8 ns |          13.37 ns |          34.03 ns |         400.0 ns |
|         InsertionRunTime |    10 |       1,258.5 ns |          28.86 ns |          60.24 ns |       1,300.0 ns |
|     InsertionCompileTime |    10 |         250.0 ns |           0.00 ns |           0.00 ns |         250.0 ns |
|             QuickRunTime |    10 |       1,721.1 ns |          36.41 ns |          89.31 ns |       1,700.0 ns |
|         QuickCompileTime |    10 |         529.2 ns |          15.93 ns |          40.84 ns |         550.0 ns |
|             MergeRunTime |    10 |       1,647.6 ns |          34.83 ns |          80.03 ns |       1,700.0 ns |
|         MergeCompileTime |    10 |       1,257.6 ns |          27.00 ns |          72.99 ns |       1,300.0 ns |
|              HeapRunTime |    10 |       2,296.7 ns |          48.79 ns |          73.03 ns |       2,250.0 ns |
|          HeapCompileTime |    10 |       1,506.5 ns |          31.89 ns |          89.94 ns |       1,500.0 ns |
|           OddEvenRunTime |    10 |       1,527.9 ns |          32.49 ns |          73.33 ns |       1,500.0 ns |
|       OddEvenCompileTime |    10 |         395.0 ns |           9.83 ns |          21.98 ns |         400.0 ns |
|             GnomeRunTime |    10 |       1,527.1 ns |          34.21 ns |          83.27 ns |       1,500.0 ns |
|         GnomeCompileTime |    10 |         473.0 ns |          21.48 ns |          63.33 ns |         500.0 ns |
|              CombRunTime |    10 |       1,521.5 ns |          32.41 ns |          84.23 ns |       1,500.0 ns |
|          CombCompileTime |    10 |         295.0 ns |           9.83 ns |          21.98 ns |         300.0 ns |
|             ShellRunTime |    10 |       1,407.8 ns |          31.03 ns |          82.82 ns |       1,450.0 ns |
|         ShellCompileTime |    10 |         967.2 ns |          19.91 ns |          47.32 ns |       1,000.0 ns |
|          CocktailRunTime |    10 |       1,766.7 ns |          37.17 ns |          70.71 ns |       1,800.0 ns |
|      CocktailCompileTime |    10 |         386.9 ns |          12.63 ns |          33.94 ns |         400.0 ns |
|             CycleRunTime |    10 |       2,065.2 ns |          45.29 ns |          57.28 ns |       2,100.0 ns |
|         CycleCompileTime |    10 |         664.9 ns |          17.26 ns |          50.09 ns |         700.0 ns |
|           PancakeRunTime |    10 |       1,883.9 ns |          86.64 ns |         237.17 ns |       1,800.0 ns |
|       PancakeCompileTime |    10 |         731.2 ns |          25.30 ns |          73.00 ns |         700.0 ns |
|            StoogeRunTime |    10 |       9,792.3 ns |         179.37 ns |         149.79 ns |       9,800.0 ns |
|        StoogeCompileTime |    10 |       8,236.7 ns |         165.95 ns |         155.23 ns |       8,250.0 ns |
|              SlowRunTime |    10 |       4,060.5 ns |         112.55 ns |         296.51 ns |       4,000.0 ns |
|          SlowCompileTime |    10 |       2,887.9 ns |          68.76 ns |         201.66 ns |       2,900.0 ns |
|              BogoRunTime |    10 | 408,640,771.9 ns | 120,405,364.98 ns | 347,397,008.15 ns | 306,325,400.0 ns |
|          BogoCompileTime |    10 | 366,271,451.6 ns |  97,933,882.34 ns | 280,990,666.77 ns | 280,804,700.0 ns |
|          **SystemArraySort** |  **1000** |      **27,376.9 ns** |         **400.64 ns** |         **334.55 ns** |      **27,200.0 ns** |
|  SystemArraySortDelegate |  1000 |      64,900.0 ns |       1,283.51 ns |       1,200.60 ns |      65,300.0 ns |
| SystemArraySortIComparer |  1000 |      74,223.1 ns |       1,345.26 ns |       1,123.35 ns |      74,500.0 ns |
|            BubbleRunTime |  1000 |   3,954,363.0 ns |      57,961.23 ns |     111,671.62 ns |   3,935,950.0 ns |
|        BubbleCompileTime |  1000 |   1,422,964.7 ns |      22,019.39 ns |      35,557.21 ns |   1,416,250.0 ns |
|         SelectionRunTime |  1000 |   2,784,468.8 ns |      51,721.92 ns |      50,797.84 ns |   2,781,000.0 ns |
|     SelectionCompileTime |  1000 |     739,492.2 ns |      14,496.94 ns |      33,598.86 ns |     745,400.0 ns |
|         InsertionRunTime |  1000 |   1,363,592.9 ns |      26,749.20 ns |      23,712.47 ns |   1,365,950.0 ns |
|     InsertionCompileTime |  1000 |     245,661.1 ns |       4,882.42 ns |       8,157.43 ns |     240,200.0 ns |
|             QuickRunTime |  1000 |     138,744.4 ns |       2,741.17 ns |       2,933.02 ns |     136,700.0 ns |
|         QuickCompileTime |  1000 |      60,723.1 ns |       1,008.06 ns |         841.78 ns |      60,800.0 ns |
|             MergeRunTime |  1000 |     102,642.1 ns |       1,274.23 ns |       1,416.30 ns |     102,700.0 ns |
|         MergeCompileTime |  1000 |      60,565.7 ns |       1,192.85 ns |       1,959.88 ns |      60,200.0 ns |
|              HeapRunTime |  1000 |     324,035.7 ns |       6,412.83 ns |      11,726.22 ns |     318,800.0 ns |
|          HeapCompileTime |  1000 |     279,686.7 ns |       5,487.56 ns |       5,133.07 ns |     280,500.0 ns |
|           OddEvenRunTime |  1000 |   3,077,887.0 ns |      58,226.69 ns |     112,183.07 ns |   3,040,950.0 ns |
|       OddEvenCompileTime |  1000 |     782,177.1 ns |      15,456.97 ns |      25,396.25 ns |     780,000.0 ns |
|             GnomeRunTime |  1000 |   2,999,268.4 ns |      59,654.41 ns |     102,900.92 ns |   2,980,350.0 ns |
|         GnomeCompileTime |  1000 |     616,600.0 ns |      12,266.74 ns |      27,937.58 ns |     611,400.0 ns |
|              CombRunTime |  1000 |     177,127.0 ns |       3,523.23 ns |       9,762.86 ns |     173,600.0 ns |
|          CombCompileTime |  1000 |      52,820.0 ns |         186.05 ns |         174.03 ns |      52,800.0 ns |
|             ShellRunTime |  1000 |     114,646.2 ns |       2,008.40 ns |       1,677.11 ns |     113,600.0 ns |
|         ShellCompileTime |  1000 |      51,540.0 ns |       1,011.03 ns |       1,349.69 ns |      51,000.0 ns |
|          CocktailRunTime |  1000 |   3,317,374.2 ns |      64,838.69 ns |      99,015.52 ns |   3,290,000.0 ns |
|      CocktailCompileTime |  1000 |   1,041,636.0 ns |      20,656.81 ns |      52,202.40 ns |   1,027,700.0 ns |
|             CycleRunTime |  1000 |   5,980,266.2 ns |     118,709.73 ns |     291,196.99 ns |   5,928,800.0 ns |
|         CycleCompileTime |  1000 |   2,306,741.4 ns |      46,009.61 ns |     125,950.55 ns |   2,283,500.0 ns |
|           PancakeRunTime |  1000 |   3,178,159.0 ns |      58,921.35 ns |     103,195.99 ns |   3,154,700.0 ns |
|       PancakeCompileTime |  1000 |     527,375.8 ns |      11,455.86 ns |      33,235.52 ns |     515,350.0 ns |
|            StoogeRunTime |  1000 | 401,364,786.7 ns |   7,218,995.96 ns |   6,752,653.30 ns | 400,306,300.0 ns |
|        StoogeCompileTime |  1000 | 266,373,092.9 ns |   2,447,164.00 ns |   2,169,347.15 ns | 266,326,000.0 ns |
|              SlowRunTime |  1000 |               NA |                NA |                NA |               NA |
|          SlowCompileTime |  1000 |               NA |                NA |                NA |               NA |
|              BogoRunTime |  1000 |               NA |                NA |                NA |               NA |
|          BogoCompileTime |  1000 |               NA |                NA |                NA |               NA |
|          **SystemArraySort** | **10000** |     **377,969.2 ns** |       **7,559.28 ns** |       **6,312.34 ns** |     **376,000.0 ns** |
|  SystemArraySortDelegate | 10000 |     872,571.4 ns |      17,393.41 ns |      28,577.88 ns |     869,400.0 ns |
| SystemArraySortIComparer | 10000 |   1,011,947.4 ns |      20,569.63 ns |      59,676.23 ns |     991,300.0 ns |
|            BubbleRunTime | 10000 | 424,016,446.2 ns |   2,577,204.80 ns |   2,152,082.73 ns | 424,601,400.0 ns |
|        BubbleCompileTime | 10000 | 175,176,506.7 ns |   1,969,299.84 ns |   1,842,084.29 ns | 175,173,100.0 ns |
|         SelectionRunTime | 10000 | 182,416,121.4 ns |     899,828.11 ns |     797,674.18 ns | 182,294,400.0 ns |
|     SelectionCompileTime | 10000 |  63,572,453.3 ns |   1,005,208.06 ns |     940,272.25 ns |  63,640,400.0 ns |
|         InsertionRunTime | 10000 |  87,353,211.1 ns |   1,641,611.43 ns |   1,756,505.84 ns |  86,837,050.0 ns |
|     InsertionCompileTime | 10000 |  23,283,000.0 ns |     323,235.82 ns |     302,354.99 ns |  23,255,300.0 ns |
|             QuickRunTime | 10000 |   1,852,821.4 ns |      36,896.49 ns |      32,707.78 ns |   1,844,550.0 ns |
|         QuickCompileTime | 10000 |     803,142.9 ns |      15,336.17 ns |      13,595.12 ns |     799,450.0 ns |
|             MergeRunTime | 10000 |   1,307,386.0 ns |      61,643.60 ns |     181,757.54 ns |   1,355,100.0 ns |
|         MergeCompileTime | 10000 |     745,082.9 ns |      14,834.55 ns |      24,373.59 ns |     749,900.0 ns |
|              HeapRunTime | 10000 |   4,353,192.0 ns |      83,442.36 ns |     111,393.16 ns |   4,359,200.0 ns |
|          HeapCompileTime | 10000 |   3,757,913.8 ns |      74,486.55 ns |     109,181.42 ns |   3,744,200.0 ns |
|           OddEvenRunTime | 10000 | 214,996,207.1 ns |   3,505,409.28 ns |   3,107,454.02 ns | 214,684,050.0 ns |
|       OddEvenCompileTime | 10000 |  88,363,146.7 ns |   1,240,145.97 ns |   1,160,033.31 ns |  88,172,300.0 ns |
|             GnomeRunTime | 10000 | 186,955,626.7 ns |   1,872,062.50 ns |   1,751,128.42 ns | 186,499,500.0 ns |
|         GnomeCompileTime | 10000 |  57,804,820.0 ns |     358,605.75 ns |     335,440.04 ns |  57,729,000.0 ns |
|              CombRunTime | 10000 |   2,269,483.3 ns |      43,621.32 ns |      56,720.04 ns |   2,263,150.0 ns |
|          CombCompileTime | 10000 |     715,031.8 ns |      14,017.13 ns |      17,214.29 ns |     719,000.0 ns |
|             ShellRunTime | 10000 |   1,675,561.5 ns |      23,374.25 ns |      19,518.56 ns |   1,678,000.0 ns |
|         ShellCompileTime | 10000 |     738,593.5 ns |      14,513.86 ns |      27,963.28 ns |     737,400.0 ns |
|          CocktailRunTime | 10000 | 210,972,130.8 ns |   3,805,184.72 ns |   5,208,579.80 ns | 209,136,000.0 ns |
|      CocktailCompileTime | 10000 | 104,287,557.1 ns |   1,857,076.57 ns |   1,646,250.01 ns | 103,847,800.0 ns |
|             CycleRunTime | 10000 | 601,703,246.7 ns |   6,858,433.28 ns |   6,415,382.74 ns | 602,074,700.0 ns |
|         CycleCompileTime | 10000 | 235,052,414.3 ns |   2,129,956.56 ns |   1,888,151.02 ns | 234,151,250.0 ns |
|           PancakeRunTime | 10000 | 193,687,771.4 ns |   1,840,584.01 ns |   1,631,629.79 ns | 193,974,550.0 ns |
|       PancakeCompileTime | 10000 |  45,405,792.9 ns |     602,539.69 ns |     534,135.74 ns |  45,538,750.0 ns |
|            StoogeRunTime | 10000 |               NA |                NA |                NA |               NA |
|        StoogeCompileTime | 10000 |               NA |                NA |                NA |               NA |
|              SlowRunTime | 10000 |               NA |                NA |                NA |               NA |
|          SlowCompileTime | 10000 |               NA |                NA |                NA |               NA |
|              BogoRunTime | 10000 |               NA |                NA |                NA |               NA |
|          BogoCompileTime | 10000 |               NA |                NA |                NA |               NA |

Benchmarks with issues:
  Sort_Benchmarks.SlowRunTime: Job-BQFFMI(InvocationCount=1, UnrollFactor=1) [N=1000]
  Sort_Benchmarks.SlowCompileTime: Job-BQFFMI(InvocationCount=1, UnrollFactor=1) [N=1000]
  Sort_Benchmarks.BogoRunTime: Job-BQFFMI(InvocationCount=1, UnrollFactor=1) [N=1000]
  Sort_Benchmarks.BogoCompileTime: Job-BQFFMI(InvocationCount=1, UnrollFactor=1) [N=1000]
  Sort_Benchmarks.StoogeRunTime: Job-BQFFMI(InvocationCount=1, UnrollFactor=1) [N=10000]
  Sort_Benchmarks.StoogeCompileTime: Job-BQFFMI(InvocationCount=1, UnrollFactor=1) [N=10000]
  Sort_Benchmarks.SlowRunTime: Job-BQFFMI(InvocationCount=1, UnrollFactor=1) [N=10000]
  Sort_Benchmarks.SlowCompileTime: Job-BQFFMI(InvocationCount=1, UnrollFactor=1) [N=10000]
  Sort_Benchmarks.BogoRunTime: Job-BQFFMI(InvocationCount=1, UnrollFactor=1) [N=10000]
  Sort_Benchmarks.BogoCompileTime: Job-BQFFMI(InvocationCount=1, UnrollFactor=1) [N=10000]

# Data Structures

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1198 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100
  [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT
  DefaultJob : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT


```
|                         Method | RandomTestData |             Mean |           Error |           StdDev |           Median |
|------------------------------- |--------------- |-----------------:|----------------:|-----------------:|-----------------:|
|                  **ListArray_Add** | **Person[100000]** |   **2,220,841.2 ns** |    **41,526.07 ns** |     **36,811.78 ns** |   **2,211,234.4 ns** |
|      ListArray_AddWithCapacity | Person[100000] |   1,638,193.4 ns |    12,819.78 ns |     11,364.40 ns |   1,639,400.1 ns |
|                            Add | Person[100000] |   2,463,200.7 ns |    49,257.41 ns |    103,900.59 ns |   2,429,375.2 ns |
|             QueueArray_Enqueue | Person[100000] |   2,969,548.7 ns |    59,356.80 ns |    147,819.02 ns |   2,965,686.7 ns |
| QueueArray_EnqueueWithCapacity | Person[100000] |   2,820,271.4 ns |    27,131.10 ns |     24,051.01 ns |   2,814,930.7 ns |
|            QueueLinked_Enqueue | Person[100000] |   2,064,972.5 ns |    16,182.77 ns |     15,137.37 ns |   2,065,725.0 ns |
|                StackArray_Push | Person[100000] |   2,603,447.9 ns |    31,568.63 ns |     29,529.31 ns |   2,592,617.2 ns |
|    StackArray_PushWithCapacity | Person[100000] |   1,381,933.3 ns |     9,664.79 ns |      9,040.45 ns |   1,382,645.7 ns |
|               StackLinked_Push | Person[100000] |   2,047,188.9 ns |    39,796.88 ns |     39,085.86 ns |   2,061,384.8 ns |
|       AvlTreeLinked_AddRunTime | Person[100000] | 215,499,238.5 ns | 4,302,211.21 ns |  6,698,024.54 ns | 211,333,366.7 ns |
|   AvlTreeLinked_AddCompileTime | Person[100000] | 236,006,146.4 ns | 7,349,520.87 ns | 21,322,292.70 ns | 232,342,400.0 ns |
|        RedBlackTree_AddRunTime | Person[100000] | 219,557,622.2 ns | 4,384,980.99 ns |  7,326,318.16 ns | 218,696,650.0 ns |
|    RedBlackTree_AddCompileTime | Person[100000] | 221,010,511.0 ns | 7,631,399.77 ns | 22,501,354.42 ns | 207,026,866.7 ns |
|       SetHashLinked_AddRunTime | Person[100000] |  34,458,951.8 ns |   302,179.84 ns |    252,333.85 ns |  34,463,266.7 ns |
|   SetHashLinked_AddCompileTime | Person[100000] |  35,764,827.2 ns |   847,782.05 ns |  2,499,704.50 ns |  35,313,506.2 ns |
|                  **ListArray_Add** |    **Person[100]** |       **1,038.6 ns** |         **7.54 ns** |          **6.30 ns** |       **1,038.4 ns** |
|      ListArray_AddWithCapacity |    Person[100] |         797.1 ns |        15.54 ns |         19.09 ns |         789.8 ns |
|                            Add |    Person[100] |       1,358.1 ns |        26.86 ns |         43.37 ns |       1,347.8 ns |
|             QueueArray_Enqueue |    Person[100] |       1,211.0 ns |        35.77 ns |        103.78 ns |       1,164.2 ns |
| QueueArray_EnqueueWithCapacity |    Person[100] |         980.4 ns |         7.85 ns |          7.34 ns |         979.7 ns |
|            QueueLinked_Enqueue |    Person[100] |       1,049.9 ns |         5.98 ns |          4.99 ns |       1,048.2 ns |
|                StackArray_Push |    Person[100] |       1,044.0 ns |         6.10 ns |          5.10 ns |       1,043.4 ns |
|    StackArray_PushWithCapacity |    Person[100] |         503.4 ns |         2.10 ns |          1.97 ns |         503.4 ns |
|               StackLinked_Push |    Person[100] |       1,002.8 ns |         7.47 ns |          6.62 ns |       1,002.3 ns |
|       AvlTreeLinked_AddRunTime |    Person[100] |      49,930.0 ns |       959.00 ns |      1,280.24 ns |      49,959.2 ns |
|   AvlTreeLinked_AddCompileTime |    Person[100] |      51,511.8 ns |     1,023.19 ns |      1,531.47 ns |      50,546.8 ns |
|        RedBlackTree_AddRunTime |    Person[100] |      57,721.9 ns |     1,148.32 ns |      1,410.24 ns |      58,476.9 ns |
|    RedBlackTree_AddCompileTime |    Person[100] |      56,844.1 ns |     1,126.60 ns |      1,882.30 ns |      57,075.9 ns |
|       SetHashLinked_AddRunTime |    Person[100] |       6,134.5 ns |        29.92 ns |         24.98 ns |       6,133.7 ns |
|   SetHashLinked_AddCompileTime |    Person[100] |       4,967.4 ns |        61.37 ns |         57.41 ns |       4,946.5 ns |

# Random

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1198 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100
  [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT
  Job-BQFFMI : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                       Method |      N |         Mean |      Error |      StdDev |       Median |
|----------------------------- |------- |-------------:|-----------:|------------:|-------------:|
| **Next_IEnumerable_TotalWeight** |     **10** |     **2.249 μs** |  **0.1355 μs** |   **0.3731 μs** |     **2.150 μs** |
|             Next_IEnumerable |     10 |     2.244 μs |  0.0483 μs |   0.1175 μs |     2.200 μs |
| **Next_IEnumerable_TotalWeight** |    **100** |     **3.228 μs** |  **0.0680 μs** |   **0.0996 μs** |     **3.200 μs** |
|             Next_IEnumerable |    100 |     4.865 μs |  0.0994 μs |   0.1518 μs |     4.900 μs |
| **Next_IEnumerable_TotalWeight** |   **1000** |    **14.377 μs** |  **0.2815 μs** |   **0.2351 μs** |    **14.400 μs** |
|             Next_IEnumerable |   1000 |    29.842 μs |  0.5778 μs |   0.8823 μs |    29.500 μs |
| **Next_IEnumerable_TotalWeight** |  **10000** |   **126.187 μs** |  **2.3495 μs** |   **2.1977 μs** |   **126.400 μs** |
|             Next_IEnumerable |  10000 |   275.433 μs |  3.9165 μs |   3.6635 μs |   273.000 μs |
| **Next_IEnumerable_TotalWeight** | **100000** | **1,280.184 μs** | **25.0843 μs** |  **39.0532 μs** | **1,260.550 μs** |
|             Next_IEnumerable | 100000 | 2,914.506 μs | 57.9758 μs | 137.7857 μs | 2,877.200 μs |

# [Numeric] To English Words

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1198 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100
  [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT
  DefaultJob : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT


```
|                 Method |  Range |     Mean |    Error |   StdDev |   Median |
|----------------------- |------- |---------:|---------:|---------:|---------:|
| **Decimal_ToEnglishWords** |     **10** | **70.89 ms** | **0.756 ms** | **0.670 ms** | **70.84 ms** |
| **Decimal_ToEnglishWords** |    **100** | **71.26 ms** | **1.402 ms** | **1.500 ms** | **70.69 ms** |
| **Decimal_ToEnglishWords** |   **1000** | **62.99 ms** | **1.246 ms** | **1.903 ms** | **61.89 ms** |
| **Decimal_ToEnglishWords** |  **10000** | **65.44 ms** | **0.225 ms** | **0.210 ms** | **65.36 ms** |
| **Decimal_ToEnglishWords** | **100000** | **65.56 ms** | **0.596 ms** | **0.528 ms** | **65.77 ms** |

# Permute

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1198 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100
  [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT
  Job-BQFFMI : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|          Method |  N |               Mean |             Error |            StdDev |             Median |
|---------------- |--- |-------------------:|------------------:|------------------:|-------------------:|
|       **Recursive** |  **1** |           **300.0 ns** |           **0.00 ns** |           **0.00 ns** |           **300.0 ns** |
|       Iterative |  1 |           971.1 ns |          26.48 ns |          73.81 ns |         1,000.0 ns |
| RecursiveStruct |  1 |           172.7 ns |          16.02 ns |          46.99 ns |           200.0 ns |
| IterativeStruct |  1 |           876.1 ns |          35.67 ns |          98.25 ns |           900.0 ns |
|       **Recursive** |  **2** |           **300.0 ns** |           **0.00 ns** |           **0.00 ns** |           **300.0 ns** |
|       Iterative |  2 |           994.3 ns |          34.54 ns |          95.12 ns |         1,000.0 ns |
| RecursiveStruct |  2 |           269.4 ns |          16.63 ns |          48.50 ns |           300.0 ns |
| IterativeStruct |  2 |           927.6 ns |          28.24 ns |          77.29 ns |           900.0 ns |
|       **Recursive** |  **3** |           **600.0 ns** |           **0.00 ns** |           **0.00 ns** |           **600.0 ns** |
|       Iterative |  3 |         1,111.2 ns |          29.15 ns |          78.82 ns |         1,150.0 ns |
| RecursiveStruct |  3 |           452.6 ns |          18.91 ns |          54.27 ns |           500.0 ns |
| IterativeStruct |  3 |           984.2 ns |          21.42 ns |          36.95 ns |         1,000.0 ns |
|       **Recursive** |  **4** |         **1,047.4 ns** |          **24.75 ns** |          **53.80 ns** |         **1,000.0 ns** |
|       Iterative |  4 |         1,323.6 ns |          38.38 ns |         105.06 ns |         1,350.0 ns |
| RecursiveStruct |  4 |         1,037.0 ns |          24.60 ns |          61.25 ns |         1,000.0 ns |
| IterativeStruct |  4 |         1,266.2 ns |          27.30 ns |          69.98 ns |         1,300.0 ns |
|       **Recursive** |  **5** |         **3,987.5 ns** |          **82.09 ns** |          **80.62 ns** |         **4,000.0 ns** |
|       Iterative |  5 |         2,478.4 ns |         103.64 ns |         285.45 ns |         2,400.0 ns |
| RecursiveStruct |  5 |         3,888.5 ns |          77.89 ns |          65.04 ns |         3,850.0 ns |
| IterativeStruct |  5 |         2,052.7 ns |          39.21 ns |          83.57 ns |         2,100.0 ns |
|       **Recursive** |  **6** |        **22,625.0 ns** |         **332.77 ns** |         **259.81 ns** |        **22,750.0 ns** |
|       Iterative |  6 |         8,557.9 ns |         173.10 ns |         192.40 ns |         8,500.0 ns |
| RecursiveStruct |  6 |        21,932.3 ns |         370.74 ns |         866.59 ns |        21,700.0 ns |
| IterativeStruct |  6 |         7,300.0 ns |         138.28 ns |         115.47 ns |         7,300.0 ns |
|       **Recursive** |  **7** |       **159,697.0 ns** |       **3,305.13 ns** |       **9,693.38 ns** |       **155,000.0 ns** |
|       Iterative |  7 |        53,107.7 ns |         732.46 ns |         611.64 ns |        52,900.0 ns |
| RecursiveStruct |  7 |       154,221.7 ns |       3,049.41 ns |       8,139.50 ns |       150,600.0 ns |
| IterativeStruct |  7 |        49,341.0 ns |       1,987.96 ns |       5,861.54 ns |        46,400.0 ns |
|       **Recursive** |  **8** |     **1,276,934.0 ns** |      **25,387.46 ns** |      **52,993.09 ns** |     **1,258,300.0 ns** |
|       Iterative |  8 |       461,081.5 ns |       9,216.40 ns |      19,440.51 ns |       459,850.0 ns |
| RecursiveStruct |  8 |     1,216,315.0 ns |      21,022.95 ns |      37,368.27 ns |     1,203,100.0 ns |
| IterativeStruct |  8 |       369,595.9 ns |       7,329.12 ns |      18,252.06 ns |       364,100.0 ns |
|       **Recursive** |  **9** |     **4,774,364.0 ns** |      **98,051.46 ns** |     **271,700.12 ns** |     **4,693,800.0 ns** |
|       Iterative |  9 |     3,911,729.6 ns |      76,388.25 ns |     107,085.66 ns |     3,885,500.0 ns |
| RecursiveStruct |  9 |    10,976,614.3 ns |     175,435.47 ns |     155,518.97 ns |    10,971,350.0 ns |
| IterativeStruct |  9 |     3,258,482.4 ns |      63,297.55 ns |      65,001.93 ns |     3,241,500.0 ns |
|       **Recursive** | **10** |    **46,189,421.4 ns** |     **311,005.39 ns** |     **275,698.17 ns** |    **46,242,800.0 ns** |
|       Iterative | 10 |    18,386,800.0 ns |     349,204.94 ns |     326,646.52 ns |    18,320,900.0 ns |
| RecursiveStruct | 10 |    42,757,413.3 ns |     498,034.42 ns |     465,861.70 ns |    42,864,800.0 ns |
| IterativeStruct | 10 |    14,422,250.0 ns |     270,887.26 ns |     289,846.33 ns |    14,353,100.0 ns |
|       **Recursive** | **11** |   **598,429,991.7 ns** |   **8,606,436.50 ns** |   **6,719,339.51 ns** |   **598,243,500.0 ns** |
|       Iterative | 11 |   202,086,675.0 ns |   1,114,766.42 ns |     870,336.30 ns |   202,033,050.0 ns |
| RecursiveStruct | 11 |   475,726,523.1 ns |   1,933,078.86 ns |   1,614,208.40 ns |   476,409,300.0 ns |
| IterativeStruct | 11 |   162,835,157.1 ns |   3,169,662.55 ns |   3,773,257.80 ns |   161,651,200.0 ns |
|       **Recursive** | **12** | **6,349,684,185.7 ns** | **118,931,964.55 ns** | **105,430,088.76 ns** | **6,330,535,550.0 ns** |
|       Iterative | 12 | 2,468,558,357.1 ns |  29,911,467.96 ns |  26,515,737.25 ns | 2,463,466,100.0 ns |
| RecursiveStruct | 12 | 5,829,397,746.2 ns |  44,463,411.79 ns |  37,128,962.60 ns | 5,815,656,900.0 ns |
| IterativeStruct | 12 | 1,951,315,707.1 ns |   4,256,182.02 ns |   3,772,994.50 ns | 1,952,321,350.0 ns |

# Map vs Dictionary (Add)

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1198 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100
  [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT
  DefaultJob : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT


```
|       Method |     N |         Mean |       Error |      StdDev |
|------------- |------ |-------------:|------------:|------------:|
| **MapDelegates** |    **10** |   **2,408.4 ns** |    **56.18 ns** |   **149.96 ns** |
|   MapStructs |    10 |     431.4 ns |     3.11 ns |     2.76 ns |
|   Dictionary |    10 |     212.9 ns |     3.75 ns |     4.88 ns |
| **MapDelegates** |   **100** |   **4,231.1 ns** |    **70.42 ns** |    **62.43 ns** |
|   MapStructs |   100 |   3,595.4 ns |    22.37 ns |    20.93 ns |
|   Dictionary |   100 |   1,804.4 ns |    34.91 ns |    55.36 ns |
| **MapDelegates** |  **1000** |  **33,570.6 ns** |   **333.35 ns** |   **311.82 ns** |
|   MapStructs |  1000 |  28,424.8 ns |    99.77 ns |    88.45 ns |
|   Dictionary |  1000 |  17,510.2 ns |   140.16 ns |   109.43 ns |
| **MapDelegates** | **10000** | **511,985.1 ns** | **2,254.90 ns** | **2,109.23 ns** |
|   MapStructs | 10000 | 483,271.4 ns | 9,418.26 ns | 9,671.86 ns |
|   Dictionary | 10000 | 246,583.2 ns | 1,839.56 ns | 1,536.11 ns |

# Map vs Dictionary (Look Up)

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1198 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100
  [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT
  Job-BQFFMI : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|       Method |     N |         Mean |        Error |       StdDev |       Median |
|------------- |------ |-------------:|-------------:|-------------:|-------------:|
| **MapDelegates** |    **10** |     **595.8 ns** |     **15.70 ns** |     **20.41 ns** |     **600.0 ns** |
|   MapStructs |    10 |     334.3 ns |     16.27 ns |     47.73 ns |     300.0 ns |
|   Dictionary |    10 |     239.4 ns |     19.37 ns |     56.82 ns |     200.0 ns |
| **MapDelegates** |   **100** |   **2,179.3 ns** |     **46.05 ns** |     **67.50 ns** |   **2,200.0 ns** |
|   MapStructs |   100 |   1,248.4 ns |     28.76 ns |     66.65 ns |   1,200.0 ns |
|   Dictionary |   100 |     969.4 ns |     21.90 ns |     49.88 ns |   1,000.0 ns |
| **MapDelegates** |  **1000** |  **17,333.3 ns** |    **137.45 ns** |    **107.31 ns** |  **17,300.0 ns** |
|   MapStructs |  1000 |   9,548.8 ns |    171.66 ns |    455.22 ns |   9,400.0 ns |
|   Dictionary |  1000 |   8,341.7 ns |    115.32 ns |     90.03 ns |   8,400.0 ns |
| **MapDelegates** | **10000** | **152,481.2 ns** | **11,036.78 ns** | **31,843.64 ns** | **162,600.0 ns** |
|   MapStructs | 10000 |  92,978.6 ns |  1,152.26 ns |  1,021.45 ns |  93,050.0 ns |
|   Dictionary | 10000 |  82,206.1 ns |  1,647.85 ns |  2,613.66 ns |  82,100.0 ns |

# Span vs Array Sorting

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1198 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100
  [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT
  Job-BQFFMI : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                 Method |     N |             Mean |           Error |          StdDev |           Median |
|----------------------- |------ |-----------------:|----------------:|----------------:|-----------------:|
|     **ArrayBubbleRunTime** |    **10** |       **2,021.1 ns** |        **42.35 ns** |        **92.07 ns** |       **2,000.0 ns** |
| ArrayBubbleCompileTime |    10 |         350.0 ns |         0.00 ns |         0.00 ns |         350.0 ns |
|      SpanBubbleRunTime |    10 |       1,857.1 ns |        38.47 ns |        70.34 ns |       1,900.0 ns |
|  SpanBubbleCompileTime |    10 |         454.0 ns |        18.31 ns |        53.97 ns |         500.0 ns |
|     **ArrayBubbleRunTime** |  **1000** |   **4,595,196.6 ns** |    **91,854.98 ns** |   **203,544.28 ns** |   **4,523,800.0 ns** |
| ArrayBubbleCompileTime |  1000 |   1,715,342.9 ns |    33,551.09 ns |    55,125.41 ns |   1,707,600.0 ns |
|      SpanBubbleRunTime |  1000 |   6,496,293.8 ns |   128,580.56 ns |   200,184.45 ns |   6,443,550.0 ns |
|  SpanBubbleCompileTime |  1000 |   1,510,602.7 ns |    29,881.82 ns |    75,515.18 ns |   1,483,400.0 ns |
|     **ArrayBubbleRunTime** | **10000** | **488,559,106.7 ns** | **7,100,330.50 ns** | **6,641,653.56 ns** | **485,937,400.0 ns** |
| ArrayBubbleCompileTime | 10000 | 194,363,291.3 ns | 3,715,210.03 ns | 4,698,551.13 ns | 193,171,800.0 ns |
|      SpanBubbleRunTime | 10000 | 424,195,800.0 ns | 4,705,578.81 ns | 3,929,371.42 ns | 424,222,100.0 ns |
|  SpanBubbleCompileTime | 10000 | 169,649,621.4 ns |   950,523.61 ns |   842,614.42 ns | 169,528,100.0 ns |

