# Benchmarks

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

## Sorting Algorithms

- The `XXXRunTime` benchmarks use runtime delegates for their compare functions.
- The `XXXCompileTime` benchmarks use structs for their compare functions (so there is no delegate at runtime).
- The `SystemArraySort` bencmark is the `System.Array.Sort` method. Note: the `System.Array.Sort` method does not allow custom sorting while all other benchmarked methods do.
- The `SystemArraySortDelegate` benchmark is the `System.Array.Sort<T>(T[], System.Comparison<T>)` method, where `System.Comparison<T>` is a runtime delegate.
- The `SystemArraySortIComparer` bencmark is the `System.Array.Sort<T>(T[], System.Collections.Generic.IComparer<T>)` method.

Source Code: https://github.com/ZacharyPatten/Towel/blob/master/Tools/Towel_Benchmarking/Sort.cs

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-preview.8.20417.9
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  Job-PRHXCF : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                   Method |     N |             Mean |             Error |            StdDev |           Median |
|------------------------- |------ |-----------------:|------------------:|------------------:|-----------------:|
|          **SystemArraySort** |    **10** |         **557.6 ns** |          **24.78 ns** |          **69.90 ns** |         **550.0 ns** |
|  SystemArraySortDelegate |    10 |         662.4 ns |          22.00 ns |          62.40 ns |         700.0 ns |
| SystemArraySortIComparer |    10 |       4,209.3 ns |         473.95 ns |       1,375.01 ns |       3,700.0 ns |
|            BubbleRunTime |    10 |       2,187.2 ns |          47.55 ns |          83.29 ns |       2,200.0 ns |
|        BubbleCompileTime |    10 |         375.0 ns |          18.68 ns |          52.68 ns |         400.0 ns |
|         SelectionRunTime |    10 |       2,640.7 ns |         345.10 ns |         967.70 ns |       2,400.0 ns |
|     SelectionCompileTime |    10 |         344.3 ns |          20.49 ns |          59.46 ns |         300.0 ns |
|         InsertionRunTime |    10 |       2,052.7 ns |         315.62 ns |         895.35 ns |       1,600.0 ns |
|     InsertionCompileTime |    10 |         280.6 ns |          21.12 ns |          61.61 ns |         250.0 ns |
|             QuickRunTime |    10 |       2,461.5 ns |         295.93 ns |         829.83 ns |       2,000.0 ns |
|         QuickCompileTime |    10 |         500.0 ns |           0.00 ns |           0.00 ns |         500.0 ns |
|             MergeRunTime |    10 |       2,247.3 ns |         309.31 ns |         867.35 ns |       1,900.0 ns |
|         MergeCompileTime |    10 |       1,871.6 ns |         313.50 ns |         899.49 ns |       1,400.0 ns |
|              HeapRunTime |    10 |       2,672.4 ns |          61.41 ns |         134.81 ns |       2,650.0 ns |
|          HeapCompileTime |    10 |       1,628.9 ns |          36.52 ns |          69.49 ns |       1,600.0 ns |
|           OddEvenRunTime |    10 |       2,152.2 ns |         293.38 ns |         827.48 ns |       1,700.0 ns |
|       OddEvenCompileTime |    10 |         373.8 ns |          36.66 ns |          43.64 ns |         350.0 ns |
|             GnomeRunTime |    10 |       2,317.0 ns |         359.97 ns |       1,027.02 ns |       1,800.0 ns |
|         GnomeCompileTime |    10 |         286.4 ns |          12.53 ns |          34.51 ns |         300.0 ns |
|              CombRunTime |    10 |       2,196.7 ns |         333.98 ns |         931.01 ns |       1,750.0 ns |
|          CombCompileTime |    10 |         366.5 ns |          22.07 ns |          64.03 ns |         350.0 ns |
|             ShellRunTime |    10 |       2,071.7 ns |         328.51 ns |         926.57 ns |       1,600.0 ns |
|         ShellCompileTime |    10 |       1,685.9 ns |         328.78 ns |         964.26 ns |       1,100.0 ns |
|          CocktailRunTime |    10 |       2,258.4 ns |         290.94 ns |         806.20 ns |       1,900.0 ns |
|      CocktailCompileTime |    10 |         400.0 ns |           0.00 ns |           0.00 ns |         400.0 ns |
|              SlowRunTime |    10 |       4,063.0 ns |          84.28 ns |         118.15 ns |       4,100.0 ns |
|          SlowCompileTime |    10 |       2,823.3 ns |          57.48 ns |         106.54 ns |       2,800.0 ns |
|              BogoRunTime |    10 | 500,108,407.4 ns | 154,305,408.76 ns | 442,731,143.25 ns | 374,585,200.0 ns |
|          BogoCompileTime |    10 | 343,163,024.5 ns |  87,229,642.35 ns | 248,871,077.99 ns | 293,393,500.0 ns |
|          **SystemArraySort** |  **1000** |      **28,988.5 ns** |         **274.56 ns** |         **229.27 ns** |      **29,050.0 ns** |
|  SystemArraySortDelegate |  1000 |      66,950.0 ns |         289.00 ns |         225.63 ns |      67,000.0 ns |
| SystemArraySortIComparer |  1000 |      77,266.7 ns |       1,534.45 ns |       2,919.45 ns |      76,600.0 ns |
|            BubbleRunTime |  1000 |   6,970,054.4 ns |   1,015,705.82 ns |   2,208,060.86 ns |   5,775,900.0 ns |
|        BubbleCompileTime |  1000 |   1,855,403.6 ns |      36,371.63 ns |      78,293.76 ns |   1,855,350.0 ns |
|         SelectionRunTime |  1000 |   3,473,812.1 ns |     347,722.94 ns |   1,019,811.20 ns |   2,856,900.0 ns |
|     SelectionCompileTime |  1000 |     862,653.3 ns |      17,165.69 ns |      32,659.51 ns |     858,500.0 ns |
|         InsertionRunTime |  1000 |   2,266,998.3 ns |      45,222.57 ns |      99,264.64 ns |   2,250,350.0 ns |
|     InsertionCompileTime |  1000 |     444,649.0 ns |      21,186.07 ns |      61,464.62 ns |     462,650.0 ns |
|             QuickRunTime |  1000 |     217,163.2 ns |       4,331.21 ns |      10,377.31 ns |     213,800.0 ns |
|         QuickCompileTime |  1000 |      63,166.7 ns |         812.53 ns |         634.37 ns |      63,350.0 ns |
|             MergeRunTime |  1000 |     140,150.0 ns |       2,794.53 ns |       6,134.07 ns |     139,400.0 ns |
|         MergeCompileTime |  1000 |      64,879.8 ns |       1,289.66 ns |       3,464.58 ns |      64,250.0 ns |
|              HeapRunTime |  1000 |     469,488.8 ns |       9,324.86 ns |      24,401.54 ns |     464,100.0 ns |
|          HeapCompileTime |  1000 |     389,193.7 ns |       8,211.75 ns |      18,867.87 ns |     385,500.0 ns |
|           OddEvenRunTime |  1000 |   5,136,794.7 ns |      99,872.19 ns |     111,007.68 ns |   5,142,200.0 ns |
|       OddEvenCompileTime |  1000 |   1,007,526.7 ns |      20,108.95 ns |      56,055.71 ns |   1,008,800.0 ns |
|             GnomeRunTime |  1000 |   4,329,040.5 ns |      86,438.02 ns |     158,056.80 ns |   4,316,000.0 ns |
|         GnomeCompileTime |  1000 |     884,110.1 ns |      17,685.26 ns |      49,005.76 ns |     877,700.0 ns |
|              CombRunTime |  1000 |     245,118.0 ns |       5,230.72 ns |      11,806.62 ns |     240,100.0 ns |
|          CombCompileTime |  1000 |      53,770.0 ns |         841.97 ns |         787.58 ns |      53,650.0 ns |
|             ShellRunTime |  1000 |     177,924.2 ns |       3,948.03 ns |      11,070.69 ns |     173,800.0 ns |
|         ShellCompileTime |  1000 |      54,037.9 ns |       1,478.52 ns |       2,167.19 ns |      53,600.0 ns |
|          CocktailRunTime |  1000 |   2,704,473.3 ns |      53,760.68 ns |     135,860.09 ns |   2,659,500.0 ns |
|      CocktailCompileTime |  1000 |   1,032,641.0 ns |      22,327.85 ns |      65,834.16 ns |   1,018,450.0 ns |
|              SlowRunTime |  1000 |               NA |                NA |                NA |               NA |
|          SlowCompileTime |  1000 |               NA |                NA |                NA |               NA |
|              BogoRunTime |  1000 |               NA |                NA |                NA |               NA |
|          BogoCompileTime |  1000 |               NA |                NA |                NA |               NA |
|          **SystemArraySort** | **10000** |     **389,468.8 ns** |       **9,913.69 ns** |      **28,603.27 ns** |     **384,050.0 ns** |
|  SystemArraySortDelegate | 10000 |     959,571.6 ns |      19,139.95 ns |      45,488.12 ns |     952,600.0 ns |
| SystemArraySortIComparer | 10000 |   1,066,255.6 ns |      21,223.66 ns |      35,459.96 ns |   1,059,150.0 ns |
|            BubbleRunTime | 10000 | 590,371,335.7 ns |   5,946,110.71 ns |   5,271,072.26 ns | 589,782,550.0 ns |
|        BubbleCompileTime | 10000 | 198,464,685.7 ns |   2,097,657.92 ns |   1,859,519.11 ns | 198,080,350.0 ns |
|         SelectionRunTime | 10000 | 249,398,900.0 ns |   1,872,183.40 ns |   1,659,641.82 ns | 248,815,850.0 ns |
|     SelectionCompileTime | 10000 |  72,469,253.3 ns |     547,039.19 ns |     511,700.80 ns |  72,489,900.0 ns |
|         InsertionRunTime | 10000 | 119,132,107.7 ns |     669,566.20 ns |     559,118.11 ns | 119,176,000.0 ns |
|     InsertionCompileTime | 10000 |  39,867,140.0 ns |     616,129.95 ns |     576,328.34 ns |  39,928,900.0 ns |
|             QuickRunTime | 10000 |   2,856,362.9 ns |      56,682.99 ns |      86,560.91 ns |   2,849,650.0 ns |
|         QuickCompileTime | 10000 |     851,603.2 ns |      16,839.36 ns |      38,351.75 ns |     841,100.0 ns |
|             MergeRunTime | 10000 |   1,866,013.2 ns |      39,334.55 ns |      82,105.85 ns |   1,852,600.0 ns |
|         MergeCompileTime | 10000 |     816,088.8 ns |      16,207.91 ns |      42,413.28 ns |     810,300.0 ns |
|              HeapRunTime | 10000 |   1,618,751.8 ns |      35,195.13 ns |      93,942.92 ns |   1,591,500.0 ns |
|          HeapCompileTime | 10000 |   1,148,800.0 ns |      28,939.41 ns |      75,217.37 ns |   1,132,900.0 ns |
|           OddEvenRunTime | 10000 | 284,252,200.0 ns |   3,109,635.65 ns |   2,908,755.12 ns | 282,684,500.0 ns |
|       OddEvenCompileTime | 10000 | 104,177,857.1 ns |   1,320,051.36 ns |   1,170,191.15 ns | 103,801,600.0 ns |
|             GnomeRunTime | 10000 | 244,479,442.9 ns |   1,266,260.81 ns |   1,122,507.23 ns | 244,298,900.0 ns |
|         GnomeCompileTime | 10000 |  78,912,081.2 ns |   1,219,021.91 ns |   1,197,242.51 ns |  79,163,650.0 ns |
|              CombRunTime | 10000 |   3,669,326.7 ns |      71,234.31 ns |     106,620.15 ns |   3,629,400.0 ns |
|          CombCompileTime | 10000 |     798,921.4 ns |      17,340.86 ns |      50,584.10 ns |     781,750.0 ns |
|             ShellRunTime | 10000 |   2,641,397.6 ns |      55,380.87 ns |     101,267.05 ns |   2,627,400.0 ns |
|         ShellCompileTime | 10000 |     745,555.0 ns |      21,618.46 ns |      24,895.86 ns |     734,550.0 ns |
|          CocktailRunTime | 10000 | 286,082,546.7 ns |   1,037,972.93 ns |     970,920.53 ns | 286,135,500.0 ns |
|      CocktailCompileTime | 10000 | 111,864,750.0 ns |     741,444.26 ns |     657,271.02 ns | 111,810,400.0 ns |
|              SlowRunTime | 10000 |               NA |                NA |                NA |               NA |
|          SlowCompileTime | 10000 |               NA |                NA |                NA |               NA |
|              BogoRunTime | 10000 |               NA |                NA |                NA |               NA |
|          BogoCompileTime | 10000 |               NA |                NA |                NA |               NA |

Benchmarks with issues:
  Sort_Benchmarks.SlowRunTime: Job-PRHXCF(InvocationCount=1, UnrollFactor=1) [N=1000]
  Sort_Benchmarks.SlowCompileTime: Job-PRHXCF(InvocationCount=1, UnrollFactor=1) [N=1000]
  Sort_Benchmarks.BogoRunTime: Job-PRHXCF(InvocationCount=1, UnrollFactor=1) [N=1000]
  Sort_Benchmarks.BogoCompileTime: Job-PRHXCF(InvocationCount=1, UnrollFactor=1) [N=1000]
  Sort_Benchmarks.SlowRunTime: Job-PRHXCF(InvocationCount=1, UnrollFactor=1) [N=10000]
  Sort_Benchmarks.SlowCompileTime: Job-PRHXCF(InvocationCount=1, UnrollFactor=1) [N=10000]
  Sort_Benchmarks.BogoRunTime: Job-PRHXCF(InvocationCount=1, UnrollFactor=1) [N=10000]
  Sort_Benchmarks.BogoCompileTime: Job-PRHXCF(InvocationCount=1, UnrollFactor=1) [N=10000]

> **Note** The becnhmarks with issues are disabled because they take too long.

## Data Structures

Source Code: https://github.com/ZacharyPatten/Towel/blob/master/Tools/Towel_Benchmarking/DataStructures.cs

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-preview.8.20417.9
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT


```
|                         Method |       RandomTestData |             Mean |           Error |           StdDev |           Median |
|------------------------------- |--------------------- |-----------------:|----------------:|-----------------:|-----------------:|
|                  ListArray_Add | Towel(...)son[] [27] |         936.4 ns |         7.09 ns |          5.92 ns |         936.3 ns |
|                  ListArray_Add | Towel(...)son[] [27] |   2,043,132.7 ns |    22,237.54 ns |     20,801.01 ns |   2,039,107.8 ns |
|      ListArray_AddWithCapacity | Towel(...)son[] [27] |         606.0 ns |         5.96 ns |          5.58 ns |         604.6 ns |
|      ListArray_AddWithCapacity | Towel(...)son[] [27] |   1,722,320.0 ns |    14,542.41 ns |     13,602.98 ns |   1,719,622.6 ns |
|                            Add | Towel(...)son[] [27] |       1,267.3 ns |        16.99 ns |         15.06 ns |       1,263.7 ns |
|                            Add | Towel(...)son[] [27] |   2,607,119.8 ns |    35,380.68 ns |     33,095.11 ns |   2,600,669.9 ns |
|             QueueArray_Enqueue | Towel(...)son[] [27] |       1,491.2 ns |        13.40 ns |         12.53 ns |       1,489.2 ns |
|             QueueArray_Enqueue | Towel(...)son[] [27] |   3,100,782.4 ns |    53,706.57 ns |     47,609.47 ns |   3,091,392.4 ns |
| QueueArray_EnqueueWithCapacity | Towel(...)son[] [27] |         771.2 ns |         4.05 ns |          3.39 ns |         770.7 ns |
| QueueArray_EnqueueWithCapacity | Towel(...)son[] [27] |   1,736,140.1 ns |    46,113.23 ns |    135,965.91 ns |   1,664,506.0 ns |
|            QueueLinked_Enqueue | Towel(...)son[] [27] |       1,160.9 ns |        24.54 ns |         35.98 ns |       1,145.6 ns |
|            QueueLinked_Enqueue | Towel(...)son[] [27] |   2,509,357.5 ns |    47,578.04 ns |     50,907.97 ns |   2,511,821.7 ns |
|                StackArray_Push | Towel(...)son[] [27] |       1,075.4 ns |        21.78 ns |         33.90 ns |       1,067.4 ns |
|                StackArray_Push | Towel(...)son[] [27] |   2,841,605.7 ns |    63,048.33 ns |     77,429.01 ns |   2,807,777.9 ns |
|    StackArray_PushWithCapacity | Towel(...)son[] [27] |         622.0 ns |        12.12 ns |         20.91 ns |         617.5 ns |
|    StackArray_PushWithCapacity | Towel(...)son[] [27] |   1,465,839.2 ns |     6,438.29 ns |      5,376.26 ns |   1,464,934.1 ns |
|               StackLinked_Push | Towel(...)son[] [27] |       1,145.8 ns |        22.80 ns |         36.16 ns |       1,138.8 ns |
|               StackLinked_Push | Towel(...)son[] [27] |   2,592,571.9 ns |    51,820.49 ns |     63,640.21 ns |   2,601,106.2 ns |
|       AvlTreeLinked_AddRunTime | Towel(...)son[] [27] |      44,230.8 ns |       646.96 ns |        573.52 ns |      44,268.0 ns |
|       AvlTreeLinked_AddRunTime | Towel(...)son[] [27] | 211,693,851.6 ns | 4,191,629.26 ns |  8,749,491.18 ns | 210,222,333.3 ns |
|   AvlTreeLinked_AddCompileTime | Towel(...)son[] [27] |      42,064.3 ns |       826.84 ns |      1,490.96 ns |      41,622.2 ns |
|   AvlTreeLinked_AddCompileTime | Towel(...)son[] [27] | 205,837,053.2 ns | 4,096,211.15 ns |  7,989,345.25 ns | 204,294,166.7 ns |
|        RedBlackTree_AddRunTime | Towel(...)son[] [27] |      48,758.6 ns |       790.27 ns |        739.22 ns |      48,606.8 ns |
|        RedBlackTree_AddRunTime | Towel(...)son[] [27] | 201,764,373.3 ns | 3,774,326.00 ns |  3,530,506.88 ns | 201,858,100.0 ns |
|    RedBlackTree_AddCompileTime | Towel(...)son[] [27] |      42,151.1 ns |       471.25 ns |        393.51 ns |      42,207.2 ns |
|    RedBlackTree_AddCompileTime | Towel(...)son[] [27] | 202,769,715.1 ns | 4,364,578.97 ns | 12,662,434.99 ns | 198,617,966.7 ns |
|       SetHashLinked_AddRunTime | Towel(...)son[] [27] |       5,877.4 ns |        88.54 ns |         82.82 ns |       5,880.4 ns |
|       SetHashLinked_AddRunTime | Towel(...)son[] [27] |  42,194,953.3 ns | 1,033,369.10 ns |  3,030,692.69 ns |  42,021,757.1 ns |
|   SetHashLinked_AddCompileTime | Towel(...)son[] [27] |       4,854.2 ns |        96.14 ns |        106.86 ns |       4,824.7 ns |
|   SetHashLinked_AddCompileTime | Towel(...)son[] [27] |  37,036,317.4 ns |   881,367.63 ns |  1,372,183.22 ns |  36,739,645.8 ns |

## Permute

Source Code: https://github.com/ZacharyPatten/Towel/blob/master/Tools/Towel_Benchmarking/Permute.cs

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-preview.8.20417.9
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  Job-PRHXCF : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|    Method |  N |               Mean |            Error |           StdDev |             Median |
|---------- |--- |-------------------:|-----------------:|-----------------:|-------------------:|
| **Recursive** |  **1** |           **235.3 ns** |         **12.42 ns** |         **35.64 ns** |           **250.0 ns** |
| Iterative |  1 |         1,450.5 ns |        290.32 ns |        851.45 ns |         1,000.0 ns |
| **Recursive** |  **2** |           **343.0 ns** |         **18.83 ns** |         **55.51 ns** |           **300.0 ns** |
| Iterative |  2 |         1,524.0 ns |        312.89 ns |        922.57 ns |         1,000.0 ns |
| **Recursive** |  **3** |           **579.0 ns** |         **15.55 ns** |         **40.98 ns** |           **600.0 ns** |
| Iterative |  3 |         1,669.4 ns |        317.47 ns |        926.07 ns |         1,200.0 ns |
| **Recursive** |  **4** |         **1,540.7 ns** |         **34.67 ns** |         **76.83 ns** |         **1,500.0 ns** |
| Iterative |  4 |         1,969.0 ns |        298.28 ns |        879.47 ns |         1,500.0 ns |
| **Recursive** |  **5** |         **6,668.3 ns** |        **136.70 ns** |        **314.09 ns** |         **6,800.0 ns** |
| Iterative |  5 |         3,163.2 ns |         67.22 ns |        161.06 ns |         3,150.0 ns |
| **Recursive** |  **6** |        **37,720.4 ns** |        **859.60 ns** |      **2,438.53 ns** |        **38,800.0 ns** |
| Iterative |  6 |        14,268.5 ns |        378.73 ns |      1,068.22 ns |        14,200.0 ns |
| **Recursive** |  **7** |       **257,284.7 ns** |      **5,139.47 ns** |     **12,703.49 ns** |       **259,400.0 ns** |
| Iterative |  7 |        90,575.8 ns |      2,131.26 ns |      5,976.27 ns |        89,900.0 ns |
| **Recursive** |  **8** |     **2,090,753.8 ns** |     **43,395.49 ns** |     **76,003.71 ns** |     **2,087,800.0 ns** |
| Iterative |  8 |       700,741.7 ns |     13,939.62 ns |     27,515.44 ns |       699,900.0 ns |
| **Recursive** |  **9** |     **4,807,112.7 ns** |     **98,265.29 ns** |    **225,780.85 ns** |     **4,724,600.0 ns** |
| Iterative |  9 |     2,160,441.2 ns |     36,199.57 ns |     94,727.96 ns |     2,131,000.0 ns |
| **Recursive** | **10** |    **50,116,392.9 ns** |    **656,147.92 ns** |    **581,658.04 ns** |    **49,830,000.0 ns** |
| Iterative | 10 |    21,760,244.4 ns |    451,949.55 ns |    483,580.96 ns |    21,686,600.0 ns |
| **Recursive** | **11** |   **546,920,473.3 ns** |  **5,249,425.20 ns** |  **4,910,315.59 ns** |   **546,790,200.0 ns** |
| Iterative | 11 |   228,724,876.9 ns |  1,822,982.93 ns |  1,522,273.31 ns |   228,578,100.0 ns |
| **Recursive** | **12** | **6,644,988,186.7 ns** | **46,447,192.95 ns** | **43,446,733.10 ns** | **6,648,290,500.0 ns** |
| Iterative | 12 | 2,903,205,660.0 ns | 48,057,455.08 ns | 44,952,973.30 ns | 2,919,752,200.0 ns |

## Decimal To English Words

Source Code: https://github.com/ZacharyPatten/Towel/blob/master/Tools/Towel_Benchmarking/ToEnglishWords.cs

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-preview.8.20417.9
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT


```
|                 Method |  Range |     Mean |    Error |   StdDev |
|----------------------- |------- |---------:|---------:|---------:|
| **Decimal_ToEnglishWords** |     **10** | **75.21 ms** | **0.809 ms** | **0.757 ms** |
| **Decimal_ToEnglishWords** |    **100** | **71.83 ms** | **0.885 ms** | **0.828 ms** |
| **Decimal_ToEnglishWords** |   **1000** | **67.53 ms** | **1.589 ms** | **1.486 ms** |
| **Decimal_ToEnglishWords** |  **10000** | **68.41 ms** | **1.125 ms** | **1.053 ms** |
| **Decimal_ToEnglishWords** | **100000** | **67.59 ms** | **0.191 ms** | **0.170 ms** |

## Random

Source Code: https://github.com/ZacharyPatten/Towel/blob/master/Tools/Towel_Benchmarking/Random.cs

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-preview.8.20417.9
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  Job-PRHXCF : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                       Method |      N |         Mean |      Error |      StdDev |       Median |
|----------------------------- |------- |-------------:|-----------:|------------:|-------------:|
| **Next_IEnumerable_TotalWeight** |     **10** |     **2.718 us** |  **0.3357 us** |   **0.9413 us** |     **2.200 us** |
|             Next_IEnumerable |     10 |     2.928 us |  0.3218 us |   0.8971 us |     2.500 us |
| **Next_IEnumerable_TotalWeight** |    **100** |     **3.345 us** |  **0.0709 us** |   **0.1415 us** |     **3.300 us** |
|             Next_IEnumerable |    100 |     5.063 us |  0.1040 us |   0.2004 us |     5.000 us |
| **Next_IEnumerable_TotalWeight** |   **1000** |    **14.786 us** |  **0.2828 us** |   **0.2507 us** |    **14.800 us** |
|             Next_IEnumerable |   1000 |    33.784 us |  1.1973 us |   3.4353 us |    32.900 us |
| **Next_IEnumerable_TotalWeight** |  **10000** |   **132.733 us** |  **3.4570 us** |   **3.6990 us** |   **131.500 us** |
|             Next_IEnumerable |  10000 |   312.578 us |  9.7314 us |  27.4477 us |   306.100 us |
| **Next_IEnumerable_TotalWeight** | **100000** | **1,311.456 us** | **25.7055 us** |  **34.3161 us** | **1,302.200 us** |
|             Next_IEnumerable | 100000 | 3,067.862 us | 60.2864 us | 125.8401 us | 3,052.500 us |
