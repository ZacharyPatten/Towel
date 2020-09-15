# Benchmarks

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

# Sorting Algorithms

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-preview.8.20417.9
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  Job-MADVMA : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                   Method |     N |             Mean |             Error |            StdDev |           Median |
|------------------------- |------ |-----------------:|------------------:|------------------:|-----------------:|
|          **SystemArraySort** |    **10** |         **477.8 ns** |          **15.00 ns** |          **41.81 ns** |         **500.0 ns** |
|  SystemArraySortDelegate |    10 |         691.7 ns |          46.00 ns |         132.72 ns |         600.0 ns |
| SystemArraySortIComparer |    10 |       3,033.3 ns |          61.35 ns |          73.03 ns |       3,000.0 ns |
|            BubbleRunTime |    10 |       2,296.9 ns |          49.80 ns |         115.43 ns |       2,300.0 ns |
|        BubbleCompileTime |    10 |         384.6 ns |          12.94 ns |          36.28 ns |         400.0 ns |
|         SelectionRunTime |    10 |       2,166.3 ns |         327.53 ns |         939.74 ns |       1,700.0 ns |
|     SelectionCompileTime |    10 |         342.0 ns |          18.15 ns |          53.52 ns |         300.0 ns |
|         InsertionRunTime |    10 |       1,505.9 ns |          33.73 ns |          80.82 ns |       1,500.0 ns |
|     InsertionCompileTime |    10 |         200.0 ns |           0.00 ns |           0.00 ns |         200.0 ns |
|             QuickRunTime |    10 |       2,332.5 ns |          49.98 ns |          88.83 ns |       2,300.0 ns |
|         QuickCompileTime |    10 |         825.5 ns |          22.72 ns |          66.29 ns |         800.0 ns |
|             MergeRunTime |    10 |       1,814.3 ns |          40.04 ns |          86.19 ns |       1,800.0 ns |
|         MergeCompileTime |    10 |       1,667.7 ns |         270.72 ns |         793.96 ns |       1,200.0 ns |
|              HeapRunTime |    10 |       2,867.6 ns |          60.59 ns |         102.89 ns |       2,900.0 ns |
|          HeapCompileTime |    10 |       1,786.7 ns |          37.62 ns |          35.19 ns |       1,800.0 ns |
|           OddEvenRunTime |    10 |       2,694.7 ns |         339.18 ns |         967.69 ns |       2,200.0 ns |
|       OddEvenCompileTime |    10 |         641.4 ns |          17.57 ns |          51.53 ns |         600.0 ns |
|             GnomeRunTime |    10 |       2,310.4 ns |         342.54 ns |         988.30 ns |       1,900.0 ns |
|         GnomeCompileTime |    10 |         354.0 ns |          18.31 ns |          53.97 ns |         400.0 ns |
|              CombRunTime |    10 |       1,656.9 ns |          36.97 ns |          75.51 ns |       1,700.0 ns |
|          CombCompileTime |    10 |         300.0 ns |           0.00 ns |           0.00 ns |         300.0 ns |
|             ShellRunTime |    10 |       2,103.2 ns |         328.73 ns |         932.56 ns |       1,600.0 ns |
|         ShellCompileTime |    10 |       1,475.8 ns |         287.84 ns |         844.20 ns |       1,000.0 ns |
|          CocktailRunTime |    10 |       1,872.3 ns |          40.99 ns |          79.95 ns |       1,900.0 ns |
|      CocktailCompileTime |    10 |         353.1 ns |          19.19 ns |          55.99 ns |         350.0 ns |
|              SlowRunTime |    10 |       4,147.4 ns |          86.75 ns |          96.43 ns |       4,200.0 ns |
|          SlowCompileTime |    10 |       2,863.6 ns |          59.18 ns |          72.67 ns |       2,900.0 ns |
|              BogoRunTime |    10 | 402,130,793.7 ns | 117,936,087.39 ns | 338,380,742.60 ns | 341,082,000.0 ns |
|          BogoCompileTime |    10 | 423,697,769.1 ns | 122,523,836.93 ns | 355,463,867.48 ns | 281,170,000.0 ns |
|          **SystemArraySort** |  **1000** |      **28,957.1 ns** |         **519.24 ns** |         **460.29 ns** |      **29,050.0 ns** |
|  SystemArraySortDelegate |  1000 |      65,757.1 ns |         830.98 ns |         736.64 ns |      65,400.0 ns |
| SystemArraySortIComparer |  1000 |      77,485.7 ns |       1,116.60 ns |         989.84 ns |      77,000.0 ns |
|            BubbleRunTime |  1000 |  10,723,192.9 ns |     261,492.82 ns |     231,806.57 ns |  10,711,250.0 ns |
|        BubbleCompileTime |  1000 |   1,848,755.4 ns |      37,078.49 ns |      93,022.68 ns |   1,834,150.0 ns |
|         SelectionRunTime |  1000 |   3,506,127.0 ns |     362,534.27 ns |   1,068,940.48 ns |   2,799,250.0 ns |
|     SelectionCompileTime |  1000 |     873,157.7 ns |      19,382.32 ns |      26,530.74 ns |     874,250.0 ns |
|         InsertionRunTime |  1000 |   2,189,488.1 ns |      45,729.40 ns |     101,333.19 ns |   2,158,000.0 ns |
|     InsertionCompileTime |  1000 |     450,805.9 ns |       8,587.46 ns |       8,818.69 ns |     449,600.0 ns |
|             QuickRunTime |  1000 |     326,215.2 ns |       6,915.26 ns |      19,504.62 ns |     318,750.0 ns |
|         QuickCompileTime |  1000 |     180,653.6 ns |       3,962.40 ns |       5,682.75 ns |     180,200.0 ns |
|             MergeRunTime |  1000 |     144,652.9 ns |       2,845.10 ns |       2,921.71 ns |     144,700.0 ns |
|         MergeCompileTime |  1000 |      73,154.5 ns |       1,962.07 ns |       5,404.10 ns |      71,650.0 ns |
|              HeapRunTime |  1000 |     493,156.9 ns |       9,309.81 ns |      21,761.39 ns |     492,100.0 ns |
|          HeapCompileTime |  1000 |     416,325.9 ns |       8,742.50 ns |      19,190.00 ns |     409,350.0 ns |
|           OddEvenRunTime |  1000 |   3,323,241.7 ns |      65,999.00 ns |     147,616.21 ns |   3,298,450.0 ns |
|       OddEvenCompileTime |  1000 |   3,475,116.7 ns |      68,170.53 ns |     143,794.77 ns |   3,467,750.0 ns |
|             GnomeRunTime |  1000 |   3,300,550.0 ns |     341,821.48 ns |   1,007,868.36 ns |   2,658,400.0 ns |
|         GnomeCompileTime |  1000 |     881,766.0 ns |      19,979.68 ns |      57,964.68 ns |     872,800.0 ns |
|              CombRunTime |  1000 |     241,295.0 ns |      10,600.93 ns |      12,208.04 ns |     236,500.0 ns |
|          CombCompileTime |  1000 |      55,518.9 ns |       1,619.56 ns |       2,750.14 ns |      54,700.0 ns |
|             ShellRunTime |  1000 |     173,100.0 ns |       3,414.48 ns |       5,980.19 ns |     171,500.0 ns |
|         ShellCompileTime |  1000 |      53,253.3 ns |       1,006.01 ns |         941.02 ns |      53,200.0 ns |
|          CocktailRunTime |  1000 |   4,996,885.4 ns |      98,848.70 ns |     195,117.69 ns |   4,946,250.0 ns |
|      CocktailCompileTime |  1000 |   1,010,495.9 ns |      23,359.93 ns |      68,142.02 ns |   1,001,300.0 ns |
|              SlowRunTime |  1000 |               NA |                NA |                NA |               NA |
|          SlowCompileTime |  1000 |               NA |                NA |                NA |               NA |
|              BogoRunTime |  1000 |               NA |                NA |                NA |               NA |
|          BogoCompileTime |  1000 |               NA |                NA |                NA |               NA |
|          **SystemArraySort** | **10000** |     **374,023.6 ns** |       **7,374.91 ns** |      **15,716.54 ns** |     **366,400.0 ns** |
|  SystemArraySortDelegate | 10000 |     922,430.0 ns |      18,373.52 ns |      21,158.97 ns |     914,400.0 ns |
| SystemArraySortIComparer | 10000 |   1,085,884.5 ns |      43,541.44 ns |     126,321.61 ns |   1,077,800.0 ns |
|            BubbleRunTime | 10000 | 584,489,700.0 ns |   5,006,869.28 ns |   4,683,428.63 ns | 583,277,400.0 ns |
|        BubbleCompileTime | 10000 | 197,777,873.3 ns |   3,691,588.43 ns |   3,453,114.11 ns | 197,532,000.0 ns |
|         SelectionRunTime | 10000 | 243,380,031.2 ns |   4,558,932.56 ns |   4,477,481.36 ns | 242,778,400.0 ns |
|     SelectionCompileTime | 10000 |  72,415,406.7 ns |     662,560.42 ns |     619,759.43 ns |  72,477,000.0 ns |
|         InsertionRunTime | 10000 | 114,582,171.4 ns |     716,411.74 ns |     635,080.35 ns | 114,683,200.0 ns |
|     InsertionCompileTime | 10000 |  40,525,453.3 ns |     307,248.66 ns |     287,400.59 ns |  40,472,400.0 ns |
|             QuickRunTime | 10000 |   4,331,593.9 ns |      88,360.80 ns |     176,466.06 ns |   4,299,900.0 ns |
|         QuickCompileTime | 10000 |   2,428,853.4 ns |      47,912.24 ns |     105,168.54 ns |   2,411,350.0 ns |
|             MergeRunTime | 10000 |   1,801,689.0 ns |      81,243.78 ns |     227,816.27 ns |   1,824,200.0 ns |
|         MergeCompileTime | 10000 |     892,195.5 ns |      17,929.94 ns |      42,262.99 ns |     881,250.0 ns |
|              HeapRunTime | 10000 |   6,601,850.0 ns |     130,221.18 ns |     149,962.90 ns |   6,604,050.0 ns |
|          HeapCompileTime | 10000 |   1,411,759.3 ns |      28,272.09 ns |      74,480.01 ns |   1,401,200.0 ns |
|           OddEvenRunTime | 10000 | 330,227,841.2 ns |   6,568,207.68 ns |   6,745,065.70 ns | 326,998,800.0 ns |
|       OddEvenCompileTime | 10000 | 146,578,326.7 ns |   2,111,155.76 ns |   1,974,776.40 ns | 146,855,200.0 ns |
|             GnomeRunTime | 10000 | 242,447,970.0 ns |   1,383,276.20 ns |   1,293,917.41 ns | 242,346,550.0 ns |
|         GnomeCompileTime | 10000 |  76,675,423.5 ns |   1,456,002.01 ns |   1,495,206.86 ns |  76,515,300.0 ns |
|              CombRunTime | 10000 |   3,475,097.5 ns |      68,817.74 ns |     122,323.47 ns |   3,448,750.0 ns |
|          CombCompileTime | 10000 |     753,560.0 ns |      15,056.38 ns |      17,338.96 ns |     751,400.0 ns |
|             ShellRunTime | 10000 |   2,584,190.2 ns |      51,671.49 ns |      93,174.35 ns |   2,546,100.0 ns |
|         ShellCompileTime | 10000 |     742,521.2 ns |      14,825.39 ns |      30,617.01 ns |     732,000.0 ns |
|          CocktailRunTime | 10000 | 280,279,953.3 ns |   4,941,271.59 ns |   4,622,068.51 ns | 281,199,100.0 ns |
|      CocktailCompileTime | 10000 | 108,856,442.9 ns |   1,216,923.85 ns |   1,078,771.30 ns | 108,448,050.0 ns |
|              SlowRunTime | 10000 |               NA |                NA |                NA |               NA |
|          SlowCompileTime | 10000 |               NA |                NA |                NA |               NA |
|              BogoRunTime | 10000 |               NA |                NA |                NA |               NA |
|          BogoCompileTime | 10000 |               NA |                NA |                NA |               NA |

Benchmarks with issues:
  Sort_Benchmarks.SlowRunTime: Job-MADVMA(InvocationCount=1, UnrollFactor=1) [N=1000]
  Sort_Benchmarks.SlowCompileTime: Job-MADVMA(InvocationCount=1, UnrollFactor=1) [N=1000]
  Sort_Benchmarks.BogoRunTime: Job-MADVMA(InvocationCount=1, UnrollFactor=1) [N=1000]
  Sort_Benchmarks.BogoCompileTime: Job-MADVMA(InvocationCount=1, UnrollFactor=1) [N=1000]
  Sort_Benchmarks.SlowRunTime: Job-MADVMA(InvocationCount=1, UnrollFactor=1) [N=10000]
  Sort_Benchmarks.SlowCompileTime: Job-MADVMA(InvocationCount=1, UnrollFactor=1) [N=10000]
  Sort_Benchmarks.BogoRunTime: Job-MADVMA(InvocationCount=1, UnrollFactor=1) [N=10000]
  Sort_Benchmarks.BogoCompileTime: Job-MADVMA(InvocationCount=1, UnrollFactor=1) [N=10000]

# Data Structures

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-preview.8.20417.9
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT


```
|                         Method |       RandomTestData |             Mean |           Error |          StdDev |
|------------------------------- |--------------------- |-----------------:|----------------:|----------------:|
|                  ListArray_Add | Towel(...)son[] [27] |         892.1 ns |         6.48 ns |         6.06 ns |
|                  ListArray_Add | Towel(...)son[] [27] |   2,063,704.3 ns |    61,221.25 ns |    72,879.55 ns |
|      ListArray_AddWithCapacity | Towel(...)son[] [27] |         600.3 ns |         8.80 ns |         7.80 ns |
|      ListArray_AddWithCapacity | Towel(...)son[] [27] |   1,467,744.8 ns |    15,579.70 ns |    14,573.26 ns |
|                            Add | Towel(...)son[] [27] |       1,239.8 ns |         6.67 ns |         5.91 ns |
|                            Add | Towel(...)son[] [27] |   2,512,265.5 ns |    50,000.10 ns |    65,014.25 ns |
|             QueueArray_Enqueue | Towel(...)son[] [27] |       1,420.0 ns |         4.31 ns |         4.03 ns |
|             QueueArray_Enqueue | Towel(...)son[] [27] |   2,804,101.7 ns |    12,829.79 ns |    11,373.27 ns |
| QueueArray_EnqueueWithCapacity | Towel(...)son[] [27] |         725.0 ns |         4.14 ns |         3.67 ns |
| QueueArray_EnqueueWithCapacity | Towel(...)son[] [27] |   1,504,950.4 ns |    18,677.42 ns |    17,470.87 ns |
|            QueueLinked_Enqueue | Towel(...)son[] [27] |       1,061.5 ns |        14.21 ns |        11.87 ns |
|            QueueLinked_Enqueue | Towel(...)son[] [27] |   2,330,209.1 ns |    21,592.43 ns |    16,857.95 ns |
|                StackArray_Push | Towel(...)son[] [27] |       1,010.3 ns |        20.09 ns |        30.68 ns |
|                StackArray_Push | Towel(...)son[] [27] |   2,659,195.4 ns |    53,077.92 ns |   116,507.32 ns |
|    StackArray_PushWithCapacity | Towel(...)son[] [27] |         493.3 ns |         2.08 ns |         1.95 ns |
|    StackArray_PushWithCapacity | Towel(...)son[] [27] |   1,342,732.7 ns |    15,054.41 ns |    13,345.35 ns |
|               StackLinked_Push | Towel(...)son[] [27] |       1,064.2 ns |        20.95 ns |        44.18 ns |
|               StackLinked_Push | Towel(...)son[] [27] |   2,310,322.1 ns |    45,084.90 ns |    55,368.30 ns |
|       AvlTreeLinked_AddRunTime | Towel(...)son[] [27] |      44,550.1 ns |       101.36 ns |        84.64 ns |
|       AvlTreeLinked_AddRunTime | Towel(...)son[] [27] | 179,947,676.2 ns | 1,143,315.41 ns | 1,013,519.33 ns |
|   AvlTreeLinked_AddCompileTime | Towel(...)son[] [27] |      39,030.0 ns |       137.13 ns |       128.27 ns |
|   AvlTreeLinked_AddCompileTime | Towel(...)son[] [27] | 175,934,222.2 ns | 2,716,452.79 ns | 2,540,971.63 ns |
|        RedBlackTree_AddRunTime | Towel(...)son[] [27] |      46,873.7 ns |       222.96 ns |       197.65 ns |
|        RedBlackTree_AddRunTime | Towel(...)son[] [27] | 171,209,864.3 ns | 1,690,353.37 ns | 1,498,454.23 ns |
|    RedBlackTree_AddCompileTime | Towel(...)son[] [27] |      39,424.8 ns |       225.60 ns |       211.02 ns |
|    RedBlackTree_AddCompileTime | Towel(...)son[] [27] | 164,266,078.6 ns | 2,205,299.16 ns | 1,954,940.26 ns |
|       SetHashLinked_AddRunTime | Towel(...)son[] [27] |       5,507.0 ns |        44.48 ns |        39.43 ns |
|       SetHashLinked_AddRunTime | Towel(...)son[] [27] |  33,953,784.4 ns |   325,890.11 ns |   304,837.82 ns |
|   SetHashLinked_AddCompileTime | Towel(...)son[] [27] |       4,400.9 ns |        19.02 ns |        16.86 ns |
|   SetHashLinked_AddCompileTime | Towel(...)son[] [27] |  31,951,061.7 ns |   402,521.87 ns |   376,519.21 ns |

# Random

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-preview.8.20417.9
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  Job-MADVMA : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                       Method |      N |         Mean |       Error |      StdDev |       Median |
|----------------------------- |------- |-------------:|------------:|------------:|-------------:|
| **Next_IEnumerable_TotalWeight** |     **10** |     **2.176 us** |   **0.0452 us** |   **0.0902 us** |     **2.200 us** |
|             Next_IEnumerable |     10 |     2.900 us |   0.3263 us |   0.9256 us |     2.400 us |
| **Next_IEnumerable_TotalWeight** |    **100** |     **3.414 us** |   **0.0717 us** |   **0.0854 us** |     **3.400 us** |
|             Next_IEnumerable |    100 |     5.082 us |   0.1023 us |   0.1467 us |     5.100 us |
| **Next_IEnumerable_TotalWeight** |   **1000** |    **14.900 us** |   **0.1467 us** |   **0.1225 us** |    **14.900 us** |
|             Next_IEnumerable |   1000 |    31.565 us |   2.2504 us |   2.5915 us |    30.600 us |
| **Next_IEnumerable_TotalWeight** |  **10000** |   **130.036 us** |   **2.1626 us** |   **1.9171 us** |   **130.250 us** |
|             Next_IEnumerable |  10000 |   298.751 us |   8.1385 us |  23.3510 us |   291.500 us |
| **Next_IEnumerable_TotalWeight** | **100000** | **1,267.934 us** |  **25.0408 us** |  **41.1428 us** | **1,248.700 us** |
|             Next_IEnumerable | 100000 | 2,863.558 us | 160.4650 us | 473.1346 us | 3,064.100 us |

# [Numeric] To English Words

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-preview.8.20417.9
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT


```
|                 Method |  Range |     Mean |    Error |   StdDev |
|----------------------- |------- |---------:|---------:|---------:|
| **Decimal_ToEnglishWords** |     **10** | **68.17 ms** | **0.108 ms** | **0.085 ms** |
| **Decimal_ToEnglishWords** |    **100** | **67.70 ms** | **1.411 ms** | **1.932 ms** |
| **Decimal_ToEnglishWords** |   **1000** | **61.38 ms** | **0.389 ms** | **0.364 ms** |
| **Decimal_ToEnglishWords** |  **10000** | **63.52 ms** | **0.282 ms** | **0.250 ms** |
| **Decimal_ToEnglishWords** | **100000** | **63.63 ms** | **0.215 ms** | **0.168 ms** |

# Permute

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-preview.8.20417.9
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  Job-MADVMA : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|    Method |  N |               Mean |           Error |          StdDev |             Median |
|---------- |--- |-------------------:|----------------:|----------------:|-------------------:|
| **Recursive** |  **1** |           **176.8 ns** |        **19.38 ns** |        **56.84 ns** |           **200.0 ns** |
| Iterative |  1 |         1,203.2 ns |       240.62 ns |       686.50 ns |           900.0 ns |
| **Recursive** |  **2** |           **374.7 ns** |        **15.67 ns** |        **45.94 ns** |           **400.0 ns** |
| Iterative |  2 |         1,485.9 ns |       278.63 ns |       817.16 ns |         1,000.0 ns |
| **Recursive** |  **3** |           **590.9 ns** |        **15.48 ns** |        **29.08 ns** |           **600.0 ns** |
| Iterative |  3 |         1,484.2 ns |       275.81 ns |       791.35 ns |         1,100.0 ns |
| **Recursive** |  **4** |         **1,571.4 ns** |        **35.41 ns** |        **70.71 ns** |         **1,600.0 ns** |
| Iterative |  4 |         1,983.8 ns |       278.66 ns |       817.25 ns |         1,600.0 ns |
| **Recursive** |  **5** |         **6,637.7 ns** |       **135.59 ns** |       **347.56 ns** |         **6,800.0 ns** |
| Iterative |  5 |         3,293.9 ns |        69.63 ns |       139.06 ns |         3,300.0 ns |
| **Recursive** |  **6** |        **37,993.8 ns** |       **794.10 ns** |     **2,303.84 ns** |        **38,900.0 ns** |
| Iterative |  6 |        13,976.0 ns |       278.52 ns |       562.63 ns |        14,100.0 ns |
| **Recursive** |  **7** |       **259,318.2 ns** |     **5,168.49 ns** |    **13,248.81 ns** |       **261,400.0 ns** |
| Iterative |  7 |        90,507.7 ns |     1,796.14 ns |     3,709.33 ns |        90,600.0 ns |
| **Recursive** |  **8** |     **2,011,738.5 ns** |    **39,769.08 ns** |    **69,652.33 ns** |     **1,993,700.0 ns** |
| Iterative |  8 |       702,852.2 ns |    15,225.96 ns |    29,335.26 ns |       692,050.0 ns |
| **Recursive** |  **9** |     **5,013,779.6 ns** |    **99,485.21 ns** |   **209,848.08 ns** |     **4,959,300.0 ns** |
| Iterative |  9 |     2,159,011.2 ns |    43,856.97 ns |   114,766.04 ns |     2,144,550.0 ns |
| **Recursive** | **10** |    **49,404,187.5 ns** |   **931,287.72 ns** |   **914,649.07 ns** |    **49,118,100.0 ns** |
| Iterative | 10 |    21,008,268.8 ns |   397,070.50 ns |   389,976.32 ns |    20,959,000.0 ns |
| **Recursive** | **11** |   **544,501,121.4 ns** | **9,151,655.05 ns** | **8,112,703.82 ns** |   **544,463,850.0 ns** |
| Iterative | 11 |   217,365,914.3 ns | 1,149,207.72 ns | 1,018,742.71 ns |   217,458,400.0 ns |
| **Recursive** | **12** | **6,370,422,315.4 ns** | **7,517,128.11 ns** | **6,277,142.42 ns** | **6,373,265,100.0 ns** |
| Iterative | 12 | 2,602,590,163.3 ns | 5,456,525.20 ns | 5,104,037.06 ns | 2,602,554,050.0 ns |

