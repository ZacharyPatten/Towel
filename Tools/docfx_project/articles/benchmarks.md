# Benchmarks

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

# Sorting Algorithms

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-rc.1.20452.10
  [Host]     : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  Job-ZKCIBR : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                   Method |     N |             Mean |             Error |            StdDev |           Median |
|------------------------- |------ |-----------------:|------------------:|------------------:|-----------------:|
|          **SystemArraySort** |    **10** |         **476.5 ns** |          **15.78 ns** |          **42.67 ns** |         **500.0 ns** |
|  SystemArraySortDelegate |    10 |         645.4 ns |          20.53 ns |          59.55 ns |         600.0 ns |
| SystemArraySortIComparer |    10 |       3,120.6 ns |          66.31 ns |         158.88 ns |       3,100.0 ns |
|            BubbleRunTime |    10 |       1,976.2 ns |          43.23 ns |          79.05 ns |       2,000.0 ns |
|        BubbleCompileTime |    10 |         480.0 ns |          14.02 ns |          40.21 ns |         500.0 ns |
|         SelectionRunTime |    10 |       1,844.1 ns |         267.22 ns |         758.07 ns |       1,500.0 ns |
|     SelectionCompileTime |    10 |         380.4 ns |          14.14 ns |          39.89 ns |         400.0 ns |
|         InsertionRunTime |    10 |       1,442.4 ns |          32.67 ns |          72.41 ns |       1,400.0 ns |
|     InsertionCompileTime |    10 |         236.0 ns |          17.06 ns |          50.29 ns |         200.0 ns |
|             QuickRunTime |    10 |       1,798.3 ns |          38.63 ns |          84.79 ns |       1,800.0 ns |
|         QuickCompileTime |    10 |         544.9 ns |          19.14 ns |          55.84 ns |         500.0 ns |
|             MergeRunTime |    10 |       1,696.2 ns |          37.54 ns |          78.35 ns |       1,700.0 ns |
|         MergeCompileTime |    10 |       1,772.7 ns |         294.60 ns |         864.00 ns |       1,300.0 ns |
|              HeapRunTime |    10 |       2,530.0 ns |          53.07 ns |          79.44 ns |       2,500.0 ns |
|          HeapCompileTime |    10 |       1,637.3 ns |          36.58 ns |          74.73 ns |       1,600.0 ns |
|           OddEvenRunTime |    10 |       2,089.0 ns |         323.56 ns |         907.31 ns |       1,700.0 ns |
|       OddEvenCompileTime |    10 |         362.0 ns |          16.54 ns |          48.78 ns |         400.0 ns |
|             GnomeRunTime |    10 |       1,723.9 ns |          38.03 ns |          93.30 ns |       1,700.0 ns |
|         GnomeCompileTime |    10 |         400.0 ns |           0.00 ns |           0.00 ns |         400.0 ns |
|              CombRunTime |    10 |       1,545.5 ns |          34.76 ns |          74.08 ns |       1,500.0 ns |
|          CombCompileTime |    10 |         351.0 ns |          19.58 ns |          57.73 ns |         300.0 ns |
|             ShellRunTime |    10 |       1,438.8 ns |          32.72 ns |          77.76 ns |       1,400.0 ns |
|         ShellCompileTime |    10 |       1,566.7 ns |         301.69 ns |         884.79 ns |       1,100.0 ns |
|          CocktailRunTime |    10 |       2,173.3 ns |         228.90 ns |         638.08 ns |       1,900.0 ns |
|      CocktailCompileTime |    10 |         468.7 ns |          20.46 ns |          60.01 ns |         500.0 ns |
|              SlowRunTime |    10 |       5,664.3 ns |          71.44 ns |          63.33 ns |       5,700.0 ns |
|          SlowCompileTime |    10 |       4,722.2 ns |          93.76 ns |         100.33 ns |       4,700.0 ns |
|              BogoRunTime |    10 | 348,409,920.6 ns | 100,596,876.19 ns | 291,849,778.49 ns | 269,969,000.0 ns |
|          BogoCompileTime |    10 | 347,184,452.6 ns |  96,283,325.77 ns | 279,335,386.55 ns | 295,738,800.0 ns |
|          **SystemArraySort** |  **1000** |      **28,684.6 ns** |         **493.39 ns** |         **412.00 ns** |      **28,600.0 ns** |
|  SystemArraySortDelegate |  1000 |      64,800.0 ns |         976.99 ns |         762.77 ns |      64,550.0 ns |
| SystemArraySortIComparer |  1000 |      77,868.4 ns |       2,053.15 ns |       2,282.07 ns |      77,300.0 ns |
|            BubbleRunTime |  1000 |   5,091,730.9 ns |     101,322.99 ns |     242,763.42 ns |   5,034,550.0 ns |
|        BubbleCompileTime |  1000 |   1,582,448.0 ns |      49,845.54 ns |     146,970.71 ns |   1,526,950.0 ns |
|         SelectionRunTime |  1000 |   2,990,598.0 ns |     234,394.18 ns |     691,116.53 ns |   3,339,850.0 ns |
|     SelectionCompileTime |  1000 |     897,590.2 ns |      17,855.33 ns |      47,349.79 ns |     886,600.0 ns |
|         InsertionRunTime |  1000 |   1,752,852.2 ns |      39,726.95 ns |      76,540.34 ns |   1,730,050.0 ns |
|     InsertionCompileTime |  1000 |     283,010.3 ns |       8,318.96 ns |      24,134.81 ns |     294,200.0 ns |
|             QuickRunTime |  1000 |     175,508.2 ns |       3,464.97 ns |       6,919.92 ns |     174,700.0 ns |
|         QuickCompileTime |  1000 |      57,376.0 ns |       1,142.97 ns |       2,308.86 ns |      57,050.0 ns |
|             MergeRunTime |  1000 |     122,831.0 ns |       2,440.34 ns |       3,577.02 ns |     123,000.0 ns |
|         MergeCompileTime |  1000 |      62,775.0 ns |       1,231.23 ns |       1,417.88 ns |      62,700.0 ns |
|              HeapRunTime |  1000 |     386,460.9 ns |       7,709.37 ns |      18,619.01 ns |     386,600.0 ns |
|          HeapCompileTime |  1000 |     344,105.5 ns |       6,836.67 ns |      14,569.51 ns |     340,500.0 ns |
|           OddEvenRunTime |  1000 |   3,219,749.0 ns |     296,402.27 ns |     873,948.78 ns |   3,759,250.0 ns |
|       OddEvenCompileTime |  1000 |     920,867.0 ns |      25,097.76 ns |      72,813.15 ns |     906,600.0 ns |
|             GnomeRunTime |  1000 |   3,684,235.1 ns |      91,476.53 ns |     155,334.09 ns |   3,635,000.0 ns |
|         GnomeCompileTime |  1000 |     775,531.7 ns |      15,367.77 ns |      40,753.14 ns |     771,100.0 ns |
|              CombRunTime |  1000 |     215,395.0 ns |       8,990.71 ns |      10,353.72 ns |     210,400.0 ns |
|          CombCompileTime |  1000 |      51,766.7 ns |         635.36 ns |         496.04 ns |      51,550.0 ns |
|             ShellRunTime |  1000 |     137,058.3 ns |       1,665.96 ns |       1,300.67 ns |     137,100.0 ns |
|         ShellCompileTime |  1000 |      53,066.7 ns |         884.59 ns |         690.63 ns |      52,850.0 ns |
|          CocktailRunTime |  1000 |   4,109,638.3 ns |      87,453.26 ns |     170,570.88 ns |   4,052,200.0 ns |
|      CocktailCompileTime |  1000 |   1,063,801.0 ns |      24,536.05 ns |      70,792.11 ns |   1,048,500.0 ns |
|              SlowRunTime |  1000 |               NA |                NA |                NA |               NA |
|          SlowCompileTime |  1000 |               NA |                NA |                NA |               NA |
|              BogoRunTime |  1000 |               NA |                NA |                NA |               NA |
|          BogoCompileTime |  1000 |               NA |                NA |                NA |               NA |
|          **SystemArraySort** | **10000** |     **375,588.2 ns** |       **7,159.17 ns** |       **7,351.95 ns** |     **374,600.0 ns** |
|  SystemArraySortDelegate | 10000 |     882,028.6 ns |      13,078.25 ns |      11,593.53 ns |     879,050.0 ns |
| SystemArraySortIComparer | 10000 |   1,023,307.7 ns |      19,933.85 ns |      27,285.67 ns |   1,019,150.0 ns |
|            BubbleRunTime | 10000 | 528,211,900.0 ns |     881,965.43 ns |     781,839.38 ns | 528,035,200.0 ns |
|        BubbleCompileTime | 10000 | 181,560,513.3 ns |   2,627,085.18 ns |   2,457,377.11 ns | 180,623,300.0 ns |
|         SelectionRunTime | 10000 | 212,526,813.3 ns |   2,282,016.46 ns |   2,134,599.61 ns | 211,880,200.0 ns |
|     SelectionCompileTime | 10000 |  87,702,120.0 ns |     730,295.57 ns |     683,118.93 ns |  87,626,300.0 ns |
|         InsertionRunTime | 10000 | 100,515,369.2 ns |     399,236.14 ns |     333,380.26 ns | 100,502,100.0 ns |
|     InsertionCompileTime | 10000 |  23,704,820.0 ns |     460,820.33 ns |     431,051.62 ns |  23,787,900.0 ns |
|             QuickRunTime | 10000 |   2,300,021.9 ns |      73,932.91 ns |     115,104.63 ns |   2,272,750.0 ns |
|         QuickCompileTime | 10000 |     736,716.7 ns |      10,410.63 ns |       8,127.94 ns |     734,950.0 ns |
|             MergeRunTime | 10000 |   1,605,363.8 ns |      31,941.39 ns |      62,299.23 ns |   1,591,700.0 ns |
|         MergeCompileTime | 10000 |     777,888.5 ns |      15,554.01 ns |      21,290.51 ns |     771,400.0 ns |
|              HeapRunTime | 10000 |   1,813,392.6 ns |      75,455.52 ns |     198,780.02 ns |   1,726,600.0 ns |
|          HeapCompileTime | 10000 |   2,778,552.0 ns |     626,971.35 ns |   1,848,639.16 ns |   1,254,400.0 ns |
|           OddEvenRunTime | 10000 | 249,007,246.2 ns |     783,602.86 ns |     654,343.88 ns | 248,769,600.0 ns |
|       OddEvenCompileTime | 10000 |  98,183,846.7 ns |     771,000.03 ns |     721,193.91 ns |  97,977,500.0 ns |
|             GnomeRunTime | 10000 | 192,898,900.0 ns |   1,776,236.67 ns |   1,661,492.89 ns | 192,680,100.0 ns |
|         GnomeCompileTime | 10000 |  73,236,420.0 ns |     916,576.37 ns |     857,366.10 ns |  73,181,800.0 ns |
|              CombRunTime | 10000 |   2,451,925.0 ns |     197,472.37 ns |     582,251.73 ns |   2,730,650.0 ns |
|          CombCompileTime | 10000 |     722,571.4 ns |      11,346.35 ns |      10,058.25 ns |     720,500.0 ns |
|             ShellRunTime | 10000 |   1,973,082.1 ns |     116,071.31 ns |     333,030.35 ns |   2,049,300.0 ns |
|         ShellCompileTime | 10000 |     785,991.3 ns |      15,949.16 ns |      38,519.02 ns |     770,400.0 ns |
|          CocktailRunTime | 10000 | 235,964,593.3 ns |   1,158,553.71 ns |   1,083,711.86 ns | 235,888,000.0 ns |
|      CocktailCompileTime | 10000 | 109,647,266.7 ns |     756,960.54 ns |     708,061.36 ns | 109,853,500.0 ns |
|              SlowRunTime | 10000 |               NA |                NA |                NA |               NA |
|          SlowCompileTime | 10000 |               NA |                NA |                NA |               NA |
|              BogoRunTime | 10000 |               NA |                NA |                NA |               NA |
|          BogoCompileTime | 10000 |               NA |                NA |                NA |               NA |

Benchmarks with issues:
  Sort_Benchmarks.SlowRunTime: Job-ZKCIBR(InvocationCount=1, UnrollFactor=1) [N=1000]
  Sort_Benchmarks.SlowCompileTime: Job-ZKCIBR(InvocationCount=1, UnrollFactor=1) [N=1000]
  Sort_Benchmarks.BogoRunTime: Job-ZKCIBR(InvocationCount=1, UnrollFactor=1) [N=1000]
  Sort_Benchmarks.BogoCompileTime: Job-ZKCIBR(InvocationCount=1, UnrollFactor=1) [N=1000]
  Sort_Benchmarks.SlowRunTime: Job-ZKCIBR(InvocationCount=1, UnrollFactor=1) [N=10000]
  Sort_Benchmarks.SlowCompileTime: Job-ZKCIBR(InvocationCount=1, UnrollFactor=1) [N=10000]
  Sort_Benchmarks.BogoRunTime: Job-ZKCIBR(InvocationCount=1, UnrollFactor=1) [N=10000]
  Sort_Benchmarks.BogoCompileTime: Job-ZKCIBR(InvocationCount=1, UnrollFactor=1) [N=10000]

# Data Structures

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-rc.1.20452.10
  [Host]     : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  DefaultJob : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT


```
|                         Method |       RandomTestData |             Mean |           Error |          StdDev |
|------------------------------- |--------------------- |-----------------:|----------------:|----------------:|
|                  ListArray_Add | Towel(...)son[] [27] |         963.2 ns |         6.52 ns |         6.10 ns |
|                  ListArray_Add | Towel(...)son[] [27] |   2,035,842.5 ns |    18,823.29 ns |    17,607.32 ns |
|      ListArray_AddWithCapacity | Towel(...)son[] [27] |         623.3 ns |         2.14 ns |         1.78 ns |
|      ListArray_AddWithCapacity | Towel(...)son[] [27] |   1,594,337.3 ns |     9,843.56 ns |     8,726.06 ns |
|                            Add | Towel(...)son[] [27] |       1,255.1 ns |        24.99 ns |        26.74 ns |
|                            Add | Towel(...)son[] [27] |   2,588,119.8 ns |    30,657.49 ns |    27,177.07 ns |
|             QueueArray_Enqueue | Towel(...)son[] [27] |       1,531.0 ns |        20.50 ns |        19.17 ns |
|             QueueArray_Enqueue | Towel(...)son[] [27] |   3,094,074.6 ns |    25,860.20 ns |    24,189.65 ns |
| QueueArray_EnqueueWithCapacity | Towel(...)son[] [27] |         782.1 ns |        14.97 ns |        14.70 ns |
| QueueArray_EnqueueWithCapacity | Towel(...)son[] [27] |   1,621,417.3 ns |    20,138.13 ns |    18,837.22 ns |
|            QueueLinked_Enqueue | Towel(...)son[] [27] |       1,040.8 ns |        13.55 ns |        12.67 ns |
|            QueueLinked_Enqueue | Towel(...)son[] [27] |   2,375,892.9 ns |    30,222.14 ns |    26,791.14 ns |
|                StackArray_Push | Towel(...)son[] [27] |       1,005.4 ns |         5.49 ns |         4.87 ns |
|                StackArray_Push | Towel(...)son[] [27] |   2,765,587.7 ns |     9,648.74 ns |     8,553.35 ns |
|    StackArray_PushWithCapacity | Towel(...)son[] [27] |         522.3 ns |         2.11 ns |         1.76 ns |
|    StackArray_PushWithCapacity | Towel(...)son[] [27] |   1,450,710.2 ns |    14,544.67 ns |    12,893.47 ns |
|               StackLinked_Push | Towel(...)son[] [27] |       1,011.8 ns |        10.15 ns |         9.49 ns |
|               StackLinked_Push | Towel(...)son[] [27] |   2,414,910.8 ns |    45,902.85 ns |    42,937.55 ns |
|       AvlTreeLinked_AddRunTime | Towel(...)son[] [27] |      63,596.4 ns |       767.54 ns |       717.96 ns |
|       AvlTreeLinked_AddRunTime | Towel(...)son[] [27] | 264,524,833.3 ns | 5,203,594.52 ns | 4,867,445.54 ns |
|   AvlTreeLinked_AddCompileTime | Towel(...)son[] [27] |      63,154.6 ns |       217.25 ns |       181.41 ns |
|   AvlTreeLinked_AddCompileTime | Towel(...)son[] [27] | 261,191,700.0 ns | 3,299,321.67 ns | 2,924,762.72 ns |
|        RedBlackTree_AddRunTime | Towel(...)son[] [27] |      71,869.3 ns |       244.19 ns |       203.91 ns |
|        RedBlackTree_AddRunTime | Towel(...)son[] [27] | 247,596,395.6 ns | 2,756,212.87 ns | 2,578,163.23 ns |
|    RedBlackTree_AddCompileTime | Towel(...)son[] [27] |      69,429.1 ns |       214.10 ns |       189.80 ns |
|    RedBlackTree_AddCompileTime | Towel(...)son[] [27] | 253,333,080.0 ns | 3,015,928.60 ns | 2,821,101.48 ns |
|       SetHashLinked_AddRunTime | Towel(...)son[] [27] |       5,664.4 ns |        34.30 ns |        30.40 ns |
|       SetHashLinked_AddRunTime | Towel(...)son[] [27] |  36,965,666.7 ns |   673,118.14 ns |   629,635.12 ns |
|   SetHashLinked_AddCompileTime | Towel(...)son[] [27] |       4,584.7 ns |        20.36 ns |        15.90 ns |
|   SetHashLinked_AddCompileTime | Towel(...)son[] [27] |  35,193,594.8 ns |   459,667.39 ns |   407,483.17 ns |

# Random

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-rc.1.20452.10
  [Host]     : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  Job-ZKCIBR : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                       Method |      N |         Mean |       Error |      StdDev |       Median |
|----------------------------- |------- |-------------:|------------:|------------:|-------------:|
| **Next_IEnumerable_TotalWeight** |     **10** |     **2.556 us** |   **0.2915 us** |   **0.8175 us** |     **2.100 us** |
|             Next_IEnumerable |     10 |     2.410 us |   0.0550 us |   0.1478 us |     2.400 us |
| **Next_IEnumerable_TotalWeight** |    **100** |     **4.098 us** |   **0.3785 us** |   **1.0551 us** |     **3.500 us** |
|             Next_IEnumerable |    100 |     4.983 us |   0.1282 us |   0.2312 us |     5.000 us |
| **Next_IEnumerable_TotalWeight** |   **1000** |    **17.706 us** |   **0.8182 us** |   **2.3475 us** |    **17.200 us** |
|             Next_IEnumerable |   1000 |    33.939 us |   1.5423 us |   4.4004 us |    32.600 us |
| **Next_IEnumerable_TotalWeight** |  **10000** |   **136.041 us** |   **8.8173 us** |   **9.0548 us** |   **131.500 us** |
|             Next_IEnumerable |  10000 |   330.240 us |  12.9513 us |  37.9838 us |   323.900 us |
| **Next_IEnumerable_TotalWeight** | **100000** | **1,327.625 us** |  **26.3989 us** |  **61.7065 us** | **1,303.400 us** |
|             Next_IEnumerable | 100000 | 2,876.262 us | 179.3327 us | 528.7664 us | 3,045.800 us |

# [Numeric] To English Words

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-rc.1.20452.10
  [Host]     : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  DefaultJob : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT


```
|                 Method |  Range |     Mean |    Error |   StdDev |
|----------------------- |------- |---------:|---------:|---------:|
| **Decimal_ToEnglishWords** |     **10** | **72.75 ms** | **0.382 ms** | **0.338 ms** |
| **Decimal_ToEnglishWords** |    **100** | **71.27 ms** | **0.247 ms** | **0.219 ms** |
| **Decimal_ToEnglishWords** |   **1000** | **65.85 ms** | **0.226 ms** | **0.189 ms** |
| **Decimal_ToEnglishWords** |  **10000** | **68.44 ms** | **0.337 ms** | **0.298 ms** |
| **Decimal_ToEnglishWords** | **100000** | **68.87 ms** | **0.870 ms** | **0.813 ms** |

# Permute

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-rc.1.20452.10
  [Host]     : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  Job-ZKCIBR : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|          Method |  N |               Mean |            Error |           StdDev |             Median |
|---------------- |--- |-------------------:|-----------------:|-----------------:|-------------------:|
|       **Recursive** |  **1** |           **290.8 ns** |         **10.62 ns** |         **29.06 ns** |           **300.0 ns** |
|       Iterative |  1 |           993.4 ns |         43.67 ns |        111.16 ns |         1,000.0 ns |
| RecursiveStruct |  1 |           176.3 ns |         15.06 ns |         42.73 ns |           200.0 ns |
| IterativeStruct |  1 |         1,492.9 ns |        337.87 ns |        990.90 ns |         1,000.0 ns |
|       **Recursive** |  **2** |           **400.0 ns** |          **0.00 ns** |          **0.00 ns** |           **400.0 ns** |
|       Iterative |  2 |         1,029.5 ns |         26.49 ns |         72.97 ns |         1,000.0 ns |
| RecursiveStruct |  2 |           323.5 ns |         42.58 ns |         43.72 ns |           300.0 ns |
| IterativeStruct |  2 |         1,456.6 ns |        316.21 ns |        927.38 ns |         1,000.0 ns |
|       **Recursive** |  **3** |           **589.6 ns** |         **15.64 ns** |         **30.87 ns** |           **600.0 ns** |
|       Iterative |  3 |         1,628.3 ns |        319.19 ns |        936.13 ns |         1,100.0 ns |
| RecursiveStruct |  3 |           564.3 ns |         17.92 ns |         52.27 ns |           600.0 ns |
| IterativeStruct |  3 |         1,492.9 ns |        320.46 ns |        939.85 ns |         1,000.0 ns |
|       **Recursive** |  **4** |         **1,300.0 ns** |         **29.93 ns** |         **74.54 ns** |         **1,300.0 ns** |
|       Iterative |  4 |         1,971.4 ns |        288.96 ns |        842.92 ns |         1,500.0 ns |
| RecursiveStruct |  4 |         1,093.8 ns |         25.45 ns |         25.00 ns |         1,100.0 ns |
| IterativeStruct |  4 |         1,681.6 ns |        278.17 ns |        811.43 ns |         1,300.0 ns |
|       **Recursive** |  **5** |         **4,809.7 ns** |        **133.83 ns** |        **379.66 ns** |         **5,000.0 ns** |
|       Iterative |  5 |         2,861.4 ns |         61.04 ns |        162.92 ns |         2,900.0 ns |
| RecursiveStruct |  5 |         4,230.6 ns |        112.51 ns |        328.20 ns |         4,400.0 ns |
| IterativeStruct |  5 |         2,836.1 ns |        255.78 ns |        742.07 ns |         2,600.0 ns |
|       **Recursive** |  **6** |        **26,031.9 ns** |        **794.16 ns** |      **2,226.90 ns** |        **27,100.0 ns** |
|       Iterative |  6 |        11,505.4 ns |        368.80 ns |      1,040.22 ns |        11,400.0 ns |
| RecursiveStruct |  6 |        23,266.3 ns |        527.40 ns |      1,487.55 ns |        24,100.0 ns |
| IterativeStruct |  6 |         9,005.1 ns |        420.85 ns |      1,227.63 ns |         9,500.0 ns |
|       **Recursive** |  **7** |       **179,527.1 ns** |      **4,458.07 ns** |     **12,862.54 ns** |       **185,250.0 ns** |
|       Iterative |  7 |        70,106.9 ns |      1,396.67 ns |      3,452.22 ns |        70,650.0 ns |
| RecursiveStruct |  7 |       160,771.1 ns |      3,210.39 ns |      8,171.46 ns |       161,850.0 ns |
| IterativeStruct |  7 |        51,457.6 ns |      2,532.71 ns |      7,428.01 ns |        53,100.0 ns |
|       **Recursive** |  **8** |     **1,277,079.2 ns** |    **112,618.85 ns** |    **324,931.15 ns** |     **1,380,650.0 ns** |
|       Iterative |  8 |       549,762.2 ns |     10,745.88 ns |     20,445.16 ns |       551,200.0 ns |
| RecursiveStruct |  8 |     1,254,614.0 ns |     24,961.42 ns |     50,423.30 ns |     1,254,250.0 ns |
| IterativeStruct |  8 |       412,090.7 ns |     14,467.32 ns |     41,972.33 ns |       427,700.0 ns |
|       **Recursive** |  **9** |     **4,712,122.4 ns** |    **100,712.56 ns** |    **272,282.25 ns** |     **4,651,800.0 ns** |
|       Iterative |  9 |     4,784,983.9 ns |    106,671.09 ns |    162,898.02 ns |     4,770,700.0 ns |
| RecursiveStruct |  9 |     4,347,445.7 ns |     86,039.06 ns |    226,661.30 ns |     4,304,100.0 ns |
| IterativeStruct |  9 |     2,693,905.0 ns |    328,783.33 ns |    969,425.06 ns |     3,148,900.0 ns |
|       **Recursive** | **10** |    **44,466,333.3 ns** |    **695,222.78 ns** |    **650,311.82 ns** |    **44,210,200.0 ns** |
|       Iterative | 10 |    22,862,104.3 ns |    448,490.49 ns |    567,196.87 ns |    22,645,300.0 ns |
| RecursiveStruct | 10 |    42,560,269.2 ns |    687,196.44 ns |    573,840.15 ns |    42,688,700.0 ns |
| IterativeStruct | 10 |    13,990,683.3 ns |    273,603.30 ns |    409,516.54 ns |    13,932,700.0 ns |
|       **Recursive** | **11** |   **514,467,876.9 ns** |  **2,451,207.70 ns** |  **2,046,869.45 ns** |   **514,123,300.0 ns** |
|       Iterative | 11 |   249,272,342.9 ns |  1,355,880.84 ns |  1,201,953.05 ns |   249,310,450.0 ns |
| RecursiveStruct | 11 |   468,063,853.3 ns |  4,238,022.48 ns |  3,964,248.86 ns |   466,432,200.0 ns |
| IterativeStruct | 11 |   151,132,000.0 ns |  1,288,450.93 ns |  1,142,178.19 ns |   150,951,050.0 ns |
|       **Recursive** | **12** | **6,177,772,966.7 ns** | **73,324,436.57 ns** | **68,587,723.46 ns** | **6,129,063,100.0 ns** |
|       Iterative | 12 | 2,966,728,490.0 ns |  3,708,905.09 ns |  3,469,312.12 ns | 2,966,516,550.0 ns |
| RecursiveStruct | 12 | 5,582,501,335.7 ns | 67,418,310.47 ns | 59,764,576.19 ns | 5,574,910,200.0 ns |
| IterativeStruct | 12 | 1,821,838,092.9 ns |  5,106,766.30 ns |  4,527,015.31 ns | 1,822,382,050.0 ns |

# Map vs Dictionary (Add)

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-rc.1.20452.10
  [Host]     : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  DefaultJob : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT


```
|       Method |     N |         Mean |       Error |      StdDev |
|------------- |------ |-------------:|------------:|------------:|
| **MapDelegates** |    **10** |     **512.3 ns** |     **6.07 ns** |     **5.38 ns** |
|   MapStructs |    10 |     435.5 ns |     3.62 ns |     3.21 ns |
|   Dictionary |    10 |     249.6 ns |     0.94 ns |     0.79 ns |
| **MapDelegates** |   **100** |   **4,164.9 ns** |    **15.83 ns** |    **14.03 ns** |
|   MapStructs |   100 |   3,711.3 ns |     8.72 ns |     8.16 ns |
|   Dictionary |   100 |   2,198.2 ns |    12.64 ns |    10.55 ns |
| **MapDelegates** |  **1000** |  **33,653.4 ns** |   **385.06 ns** |   **360.18 ns** |
|   MapStructs |  1000 |  29,771.6 ns |   120.02 ns |   100.22 ns |
|   Dictionary |  1000 |  21,078.6 ns |    82.18 ns |    76.87 ns |
| **MapDelegates** | **10000** | **617,945.4 ns** | **6,062.47 ns** | **5,374.22 ns** |
|   MapStructs | 10000 | 582,379.9 ns | 4,567.55 ns | 3,814.11 ns |
|   Dictionary | 10000 | 296,027.0 ns | 1,504.60 ns | 1,256.41 ns |

# Map vs Dictionary (Look Up)

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-rc.1.20452.10
  [Host]     : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  Job-ZKCIBR : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|       Method |     N |         Mean |        Error |       StdDev |       Median |
|------------- |------ |-------------:|-------------:|-------------:|-------------:|
| **MapDelegates** |    **10** |     **714.8 ns** |     **32.53 ns** |     **45.60 ns** |     **700.0 ns** |
|   MapStructs |    10 |     218.8 ns |     41.04 ns |     40.31 ns |     200.0 ns |
|   Dictionary |    10 |     361.3 ns |     20.15 ns |     57.17 ns |     400.0 ns |
| **MapDelegates** |   **100** |   **2,158.6 ns** |     **46.55 ns** |     **68.23 ns** |   **2,100.0 ns** |
|   MapStructs |   100 |     795.0 ns |     19.42 ns |     22.36 ns |     800.0 ns |
|   Dictionary |   100 |   1,051.4 ns |     24.90 ns |     62.47 ns |   1,000.0 ns |
| **MapDelegates** |  **1000** |  **17,393.8 ns** |    **339.67 ns** |    **333.60 ns** |  **17,300.0 ns** |
|   MapStructs |  1000 |   7,083.3 ns |     49.86 ns |     38.92 ns |   7,100.0 ns |
|   Dictionary |  1000 |   8,300.0 ns |    165.55 ns |    146.76 ns |   8,250.0 ns |
| **MapDelegates** | **10000** | **153,867.7 ns** | **11,274.79 ns** | **33,067.01 ns** | **166,500.0 ns** |
|   MapStructs | 10000 |  69,869.2 ns |  1,390.03 ns |  1,160.74 ns |  70,200.0 ns |
|   Dictionary | 10000 |  80,023.1 ns |  1,132.03 ns |    945.30 ns |  80,100.0 ns |

# Span vs Array Sorting

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-rc.1.20452.10
  [Host]     : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  Job-ZKCIBR : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                 Method |     N |             Mean |           Error |          StdDev |           Median |
|----------------------- |------ |-----------------:|----------------:|----------------:|-----------------:|
|     **ArrayBubbleRunTime** |    **10** |       **2,027.7 ns** |        **43.69 ns** |        **85.22 ns** |       **2,000.0 ns** |
| ArrayBubbleCompileTime |    10 |         536.7 ns |        20.53 ns |        59.88 ns |         500.0 ns |
|      SpanBubbleRunTime |    10 |       2,126.3 ns |        44.06 ns |        76.00 ns |       2,100.0 ns |
|  SpanBubbleCompileTime |    10 |         450.0 ns |        20.73 ns |        61.13 ns |         400.0 ns |
|     **ArrayBubbleRunTime** |  **1000** |   **6,184,537.0 ns** |   **670,352.44 ns** | **1,976,549.29 ns** |   **4,938,950.0 ns** |
| ArrayBubbleCompileTime |  1000 |   1,758,985.3 ns |    36,695.24 ns |   105,285.52 ns |   1,732,300.0 ns |
|      SpanBubbleRunTime |  1000 |   5,828,465.0 ns |   617,791.74 ns | 1,821,572.88 ns |   4,612,150.0 ns |
|  SpanBubbleCompileTime |  1000 |   1,442,844.0 ns |    74,397.07 ns |   219,361.45 ns |   1,340,600.0 ns |
|     **ArrayBubbleRunTime** | **10000** | **490,926,392.9 ns** | **1,007,005.29 ns** |   **892,683.96 ns** | **491,241,500.0 ns** |
| ArrayBubbleCompileTime | 10000 | 192,653,735.7 ns | 1,218,264.51 ns | 1,079,959.75 ns | 192,520,800.0 ns |
|      SpanBubbleRunTime | 10000 | 475,065,076.9 ns | 1,637,935.30 ns | 1,367,750.16 ns | 474,994,300.0 ns |
|  SpanBubbleCompileTime | 10000 | 181,612,793.3 ns |   684,740.86 ns |   640,507.03 ns | 181,776,400.0 ns |

