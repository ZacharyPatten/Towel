# Benchmarks

_All the source code for the benchmarks are included in this GitHub repository._

## Sorting Algorithms

<details>
	<summary><strong>Benchmark Results [click to expand]</strong></summary>
<p>

- The `XXXRunTime` benchmarks use runtime delegates for their compare functions.
- The `XXXCompileTime` benchmarks use structs for their compare functions (so there is no delegate at runtime).
- The `SystemArraySort` bencmark is the `System.Array.Sort` method. Note: the `System.Array.Sort` method does not allow custom sorting while all other benchmarked methods do.
- The `SystemArraySortDelegate` benchmark is the `System.Array.Sort<T>(T[], System.Comparison<T>)` method, where `System.Comparison<T>` is a runtime delegate.
- The `SystemArraySortIComparer` bencmark is the `System.Array.Sort<T>(T[], System.Collections.Generic.IComparer<T>)` method.

Source Code: https://github.com/ZacharyPatten/Towel/blob/master/Tools/Towel_Benchmarking/Sort.cs

``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), 64bit RyuJIT
  Job-BDOEBU : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), 64bit RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                   Method |     N |             Mean |             Error |            StdDev |           Median |
|------------------------- |------ |-----------------:|------------------:|------------------:|-----------------:|
|          **SystemArraySort** |    **10** |         **442.0 ns** |          **18.15 ns** |          **53.52 ns** |         **400.0 ns** |
|  SystemArraySortDelegate |    10 |         582.2 ns |          15.47 ns |          38.52 ns |         600.0 ns |
| SystemArraySortIComparer |    10 |       2,822.5 ns |          60.41 ns |         107.39 ns |       2,800.0 ns |
|            BubbleRunTime |    10 |       2,261.9 ns |          55.29 ns |         101.10 ns |       2,250.0 ns |
|        BubbleCompileTime |    10 |         382.0 ns |          18.90 ns |          55.74 ns |         400.0 ns |
|         SelectionRunTime |    10 |       1,552.8 ns |          34.65 ns |          72.33 ns |       1,600.0 ns |
|     SelectionCompileTime |    10 |         360.0 ns |          17.38 ns |          51.25 ns |         400.0 ns |
|         InsertionRunTime |    10 |       1,320.8 ns |          30.24 ns |          63.12 ns |       1,300.0 ns |
|     InsertionCompileTime |    10 |         252.5 ns |          18.45 ns |          54.10 ns |         300.0 ns |
|             QuickRunTime |    10 |       1,822.6 ns |          40.08 ns |          91.29 ns |       1,800.0 ns |
|         QuickCompileTime |    10 |         464.0 ns |          20.18 ns |          59.49 ns |         500.0 ns |
|             MergeRunTime |    10 |       1,622.7 ns |          36.06 ns |          67.73 ns |       1,600.0 ns |
|         MergeCompileTime |    10 |       1,198.7 ns |          27.21 ns |          69.76 ns |       1,200.0 ns |
|              HeapRunTime |    10 |       2,315.7 ns |          50.28 ns |         102.71 ns |       2,300.0 ns |
|          HeapCompileTime |    10 |       1,401.6 ns |          31.74 ns |          72.94 ns |       1,400.0 ns |
|           OddEvenRunTime |    10 |       1,568.3 ns |          34.99 ns |          92.80 ns |       1,600.0 ns |
|       OddEvenCompileTime |    10 |         396.5 ns |          17.77 ns |          52.12 ns |         350.0 ns |
|             GnomeRunTime |    10 |       1,626.8 ns |          36.43 ns |          89.38 ns |       1,600.0 ns |
|         GnomeCompileTime |    10 |         339.4 ns |          20.56 ns |          60.30 ns |         300.0 ns |
|              CombRunTime |    10 |       1,534.2 ns |          34.54 ns |          75.09 ns |       1,550.0 ns |
|          CombCompileTime |    10 |         323.8 ns |          36.66 ns |          43.64 ns |         300.0 ns |
|             ShellRunTime |    10 |       1,498.6 ns |          31.74 ns |          79.04 ns |       1,500.0 ns |
|         ShellCompileTime |    10 |         872.2 ns |          21.43 ns |          45.21 ns |         900.0 ns |
|          CocktailRunTime |    10 |       1,684.5 ns |          37.67 ns |         101.19 ns |       1,700.0 ns |
|      CocktailCompileTime |    10 |         337.0 ns |          16.46 ns |          48.52 ns |         300.0 ns |
|              SlowRunTime |    10 |       3,860.7 ns |          79.02 ns |         113.33 ns |       3,900.0 ns |
|          SlowCompileTime |    10 |       2,714.3 ns |          56.42 ns |         103.17 ns |       2,750.0 ns |
|              BogoRunTime |    10 | 391,003,513.5 ns | 121,304,281.36 ns | 349,990,587.44 ns | 248,319,950.0 ns |
|          BogoCompileTime |    10 | 468,765,539.4 ns | 144,668,002.39 ns | 424,286,208.04 ns | 331,454,600.0 ns |
|          **SystemArraySort** |  **1000** |      **29,500.0 ns** |         **328.14 ns** |         **290.89 ns** |      **29,600.0 ns** |
|  SystemArraySortDelegate |  1000 |      66,483.3 ns |       1,217.36 ns |         950.44 ns |      66,700.0 ns |
| SystemArraySortIComparer |  1000 |      81,417.3 ns |       1,979.98 ns |       5,775.69 ns |      83,350.0 ns |
|            BubbleRunTime |  1000 |   5,873,307.8 ns |     116,589.84 ns |     270,214.68 ns |   5,815,000.0 ns |
|        BubbleCompileTime |  1000 |   1,733,672.4 ns |      42,492.04 ns |     123,951.28 ns |   1,699,600.0 ns |
|         SelectionRunTime |  1000 |   3,754,555.0 ns |     423,852.01 ns |   1,249,737.23 ns |   2,915,000.0 ns |
|     SelectionCompileTime |  1000 |     889,730.2 ns |      17,768.71 ns |      40,826.56 ns |     881,100.0 ns |
|         InsertionRunTime |  1000 |   2,162,679.2 ns |      43,095.95 ns |      56,036.91 ns |   2,143,050.0 ns |
|     InsertionCompileTime |  1000 |     257,657.0 ns |       8,369.26 ns |      24,676.95 ns |     244,150.0 ns |
|             QuickRunTime |  1000 |     225,142.2 ns |       5,032.60 ns |       9,575.05 ns |     223,500.0 ns |
|         QuickCompileTime |  1000 |      63,286.3 ns |       1,927.54 ns |       5,530.47 ns |      60,500.0 ns |
|             MergeRunTime |  1000 |     140,117.1 ns |       2,749.50 ns |       4,517.51 ns |     140,800.0 ns |
|         MergeCompileTime |  1000 |      68,186.3 ns |       1,798.55 ns |       5,160.38 ns |      65,800.0 ns |
|              HeapRunTime |  1000 |     393,914.5 ns |       7,822.91 ns |      16,671.25 ns |     389,400.0 ns |
|          HeapCompileTime |  1000 |     323,797.9 ns |       6,693.17 ns |      13,211.66 ns |     319,800.0 ns |
|           OddEvenRunTime |  1000 |   5,011,756.2 ns |      99,918.31 ns |     155,560.77 ns |   4,986,150.0 ns |
|       OddEvenCompileTime |  1000 |     973,746.0 ns |      33,628.10 ns |      99,153.20 ns |     933,100.0 ns |
|             GnomeRunTime |  1000 |   4,275,907.3 ns |      97,461.02 ns |     175,742.32 ns |   4,241,100.0 ns |
|         GnomeCompileTime |  1000 |     849,127.6 ns |      33,810.37 ns |      98,626.44 ns |     854,450.0 ns |
|              CombRunTime |  1000 |     241,954.2 ns |       4,787.35 ns |       6,224.91 ns |     239,800.0 ns |
|          CombCompileTime |  1000 |      55,334.8 ns |       2,596.98 ns |       3,284.34 ns |      54,100.0 ns |
|             ShellRunTime |  1000 |     176,859.1 ns |       5,896.09 ns |       7,240.93 ns |     174,500.0 ns |
|         ShellCompileTime |  1000 |      57,704.4 ns |       1,831.03 ns |       5,134.41 ns |      55,100.0 ns |
|          CocktailRunTime |  1000 |   3,791,028.0 ns |     376,516.23 ns |   1,110,166.58 ns |   3,015,250.0 ns |
|      CocktailCompileTime |  1000 |   1,053,579.4 ns |      24,157.94 ns |      70,086.57 ns |   1,027,600.0 ns |
|              SlowRunTime |  1000 |               NA |                NA |                NA |               NA |
|          SlowCompileTime |  1000 |               NA |                NA |                NA |               NA |
|              BogoRunTime |  1000 |               NA |                NA |                NA |               NA |
|          BogoCompileTime |  1000 |               NA |                NA |                NA |               NA |
|          **SystemArraySort** | **10000** |     **375,656.0 ns** |       **7,459.47 ns** |      **15,068.50 ns** |     **369,400.0 ns** |
|  SystemArraySortDelegate | 10000 |     923,688.9 ns |      18,135.84 ns |      19,405.15 ns |     921,550.0 ns |
| SystemArraySortIComparer | 10000 |   1,023,473.7 ns |      20,249.16 ns |      22,506.88 ns |   1,012,700.0 ns |
|            BubbleRunTime | 10000 | 600,867,614.3 ns |   4,690,845.44 ns |   4,158,312.30 ns | 599,017,050.0 ns |
|        BubbleCompileTime | 10000 | 192,833,142.9 ns |   1,284,212.04 ns |   1,138,420.52 ns | 193,233,400.0 ns |
|         SelectionRunTime | 10000 | 258,595,310.0 ns |   1,044,289.74 ns |     976,829.27 ns | 258,277,750.0 ns |
|     SelectionCompileTime | 10000 |  87,297,760.0 ns |     805,172.87 ns |     753,159.21 ns |  87,415,600.0 ns |
|         InsertionRunTime | 10000 | 118,025,760.0 ns |     667,270.63 ns |     624,165.36 ns | 118,021,000.0 ns |
|     InsertionCompileTime | 10000 |  23,708,953.8 ns |     366,291.32 ns |     305,869.84 ns |  23,604,100.0 ns |
|             QuickRunTime | 10000 |   2,771,133.9 ns |      58,543.19 ns |     126,020.39 ns |   2,722,000.0 ns |
|         QuickCompileTime | 10000 |     760,446.7 ns |      14,437.29 ns |      13,504.65 ns |     762,000.0 ns |
|             MergeRunTime | 10000 |   1,820,241.7 ns |      33,955.66 ns |      44,151.95 ns |   1,807,600.0 ns |
|         MergeCompileTime | 10000 |     785,671.7 ns |      16,656.79 ns |      21,065.50 ns |     781,250.0 ns |
|              HeapRunTime | 10000 |   5,502,445.1 ns |     109,420.81 ns |     223,517.71 ns |   5,475,900.0 ns |
|          HeapCompileTime | 10000 |   2,795,049.0 ns |     575,587.02 ns |   1,697,131.32 ns |   4,231,450.0 ns |
|           OddEvenRunTime | 10000 | 302,667,773.3 ns |   1,691,557.61 ns |   1,582,284.04 ns | 301,906,600.0 ns |
|       OddEvenCompileTime | 10000 | 101,055,620.0 ns |     961,584.93 ns |     899,467.14 ns | 100,707,900.0 ns |
|             GnomeRunTime | 10000 | 266,921,170.0 ns |   2,249,810.56 ns |   2,104,474.19 ns | 266,339,850.0 ns |
|         GnomeCompileTime | 10000 |  67,529,863.6 ns |   1,341,591.95 ns |   1,647,595.40 ns |  67,245,750.0 ns |
|              CombRunTime | 10000 |   3,588,997.7 ns |      70,925.62 ns |     131,465.18 ns |   3,519,500.0 ns |
|          CombCompileTime | 10000 |     744,873.7 ns |      14,401.55 ns |      16,007.28 ns |     747,200.0 ns |
|             ShellRunTime | 10000 |   2,640,967.4 ns |      51,866.18 ns |      96,137.29 ns |   2,611,300.0 ns |
|         ShellCompileTime | 10000 |     752,140.0 ns |      14,695.76 ns |      19,618.42 ns |     751,300.0 ns |
|          CocktailRunTime | 10000 | 294,716,906.7 ns |   1,392,103.18 ns |   1,302,174.18 ns | 295,169,200.0 ns |
|      CocktailCompileTime | 10000 | 113,411,773.3 ns |     967,880.93 ns |     905,356.43 ns | 113,199,600.0 ns |
|              SlowRunTime | 10000 |               NA |                NA |                NA |               NA |
|          SlowCompileTime | 10000 |               NA |                NA |                NA |               NA |
|              BogoRunTime | 10000 |               NA |                NA |                NA |               NA |
|          BogoCompileTime | 10000 |               NA |                NA |                NA |               NA |

</p>
</details>

## Data Structures

These benchmarks are still in development. They are not yet ready.

## Other

<details>
	<summary><strong>Permute Benchmark Results [click to expand]</strong></summary>
<p>

Source Code: https://github.com/ZacharyPatten/Towel/blob/master/Tools/Towel_Benchmarking/Permute.cs

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.1.100
  [Host]     : .NET Core 3.1.0 (CoreCLR 4.700.19.56402, CoreFX 4.700.19.56404), X64 RyuJIT
  Job-KCPEJM : .NET Core 3.1.0 (CoreCLR 4.700.19.56402, CoreFX 4.700.19.56404), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|    Method |  N |               Mean |             Error |            StdDev |             Median |
|---------- |--- |-------------------:|------------------:|------------------:|-------------------:|
| **Recursive** |  **1** |           **238.0 ns** |          **16.54 ns** |          **48.78 ns** |           **200.0 ns** |
| Iterative |  1 |         1,308.1 ns |         264.19 ns |         774.82 ns |           900.0 ns |
| **Recursive** |  **2** |           **389.4 ns** |          **10.86 ns** |          **31.00 ns** |           **400.0 ns** |
| Iterative |  2 |           950.6 ns |          24.20 ns |          66.25 ns |           900.0 ns |
| **Recursive** |  **3** |           **678.3 ns** |          **15.53 ns** |          **41.46 ns** |           **700.0 ns** |
| Iterative |  3 |         1,580.4 ns |         263.52 ns |         764.53 ns |         1,200.0 ns |
| **Recursive** |  **4** |         **1,601.7 ns** |          **33.59 ns** |          **73.73 ns** |         **1,600.0 ns** |
| Iterative |  4 |         1,400.0 ns |           0.00 ns |           0.00 ns |         1,400.0 ns |
| **Recursive** |  **5** |         **6,634.7 ns** |         **133.61 ns** |         **330.24 ns** |         **6,800.0 ns** |
| Iterative |  5 |         3,672.4 ns |         270.95 ns |         790.38 ns |         3,300.0 ns |
| **Recursive** |  **6** |        **37,373.7 ns** |         **872.69 ns** |       **2,503.92 ns** |        **38,000.0 ns** |
| Iterative |  6 |        13,975.0 ns |         337.29 ns |         929.00 ns |        13,950.0 ns |
| **Recursive** |  **7** |       **257,323.4 ns** |       **5,127.95 ns** |      **13,328.21 ns** |       **259,450.0 ns** |
| Iterative |  7 |        90,068.2 ns |       1,883.18 ns |       5,186.82 ns |        90,000.0 ns |
| **Recursive** |  **8** |     **2,071,705.3 ns** |      **43,475.75 ns** |      **48,323.18 ns** |     **2,069,000.0 ns** |
| Iterative |  8 |       708,175.9 ns |      14,078.47 ns |      30,902.58 ns |       707,900.0 ns |
| **Recursive** |  **9** |     **5,120,522.2 ns** |     **101,374.93 ns** |     **213,834.14 ns** |     **5,082,150.0 ns** |
| Iterative |  9 |     6,219,026.3 ns |     117,917.14 ns |     131,064.59 ns |     6,180,000.0 ns |
| **Recursive** | **10** |    **47,170,913.3 ns** |     **471,880.60 ns** |     **441,397.40 ns** |    **47,008,400.0 ns** |
| Iterative | 10 |    21,223,106.7 ns |     308,924.28 ns |     288,967.96 ns |    21,233,400.0 ns |
| **Recursive** | **11** |   **539,089,553.3 ns** |   **4,859,598.37 ns** |   **4,545,671.33 ns** |   **537,287,000.0 ns** |
| Iterative | 11 |   222,299,092.9 ns |     705,266.78 ns |     625,200.63 ns |   222,250,150.0 ns |
| **Recursive** | **12** | **6,636,830,906.7 ns** | **123,635,606.88 ns** | **115,648,823.39 ns** | **6,648,385,900.0 ns** |
| Iterative | 12 | 2,751,250,807.1 ns |  12,233,483.09 ns |  10,844,664.12 ns | 2,753,578,500.0 ns |

</p>
</details>
