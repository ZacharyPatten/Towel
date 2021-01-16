# Benchmarks

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

# Sorting Algorithms

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1316 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  Job-APNGAA : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                   Method |     N |             Mean |             Error |            StdDev |           Median |
|------------------------- |------ |-----------------:|------------------:|------------------:|-----------------:|
|          **SystemArraySort** |    **10** |         **361.5 ns** |          **18.39 ns** |          **53.05 ns** |         **400.0 ns** |
|  SystemArraySortDelegate |    10 |         650.0 ns |           0.00 ns |           0.00 ns |         650.0 ns |
| SystemArraySortIComparer |    10 |       3,025.0 ns |          63.19 ns |          98.37 ns |       3,000.0 ns |
|            BubbleRunTime |    10 |       1,833.3 ns |          44.45 ns |         121.68 ns |       1,800.0 ns |
|        BubbleCompileTime |    10 |         379.2 ns |          14.15 ns |          40.82 ns |         400.0 ns |
|         SelectionRunTime |    10 |       1,472.5 ns |          41.88 ns |         117.44 ns |       1,400.0 ns |
|     SelectionCompileTime |    10 |         355.0 ns |          18.28 ns |          53.89 ns |         400.0 ns |
|         InsertionRunTime |    10 |       1,410.4 ns |          32.01 ns |          82.05 ns |       1,400.0 ns |
|     InsertionCompileTime |    10 |         234.7 ns |          20.36 ns |          59.38 ns |         200.0 ns |
|             QuickRunTime |    10 |       1,748.9 ns |          36.84 ns |          71.85 ns |       1,700.0 ns |
|         QuickCompileTime |    10 |         549.5 ns |          21.99 ns |          64.48 ns |         500.0 ns |
|             MergeRunTime |    10 |       1,681.6 ns |          37.59 ns |         102.90 ns |       1,700.0 ns |
|         MergeCompileTime |    10 |       1,296.6 ns |          32.39 ns |          89.76 ns |       1,300.0 ns |
|              HeapRunTime |    10 |       2,502.2 ns |          51.95 ns |          98.83 ns |       2,500.0 ns |
|          HeapCompileTime |    10 |       1,429.7 ns |          32.32 ns |          74.92 ns |       1,400.0 ns |
|           OddEvenRunTime |    10 |       1,510.0 ns |          33.55 ns |          67.76 ns |       1,500.0 ns |
|       OddEvenCompileTime |    10 |         300.0 ns |           0.00 ns |           0.00 ns |         300.0 ns |
|             GnomeRunTime |    10 |       1,626.0 ns |          35.80 ns |          72.31 ns |       1,600.0 ns |
|         GnomeCompileTime |    10 |         410.0 ns |          18.67 ns |          55.05 ns |         450.0 ns |
|              CombRunTime |    10 |       1,462.2 ns |          35.09 ns |          97.82 ns |       1,400.0 ns |
|          CombCompileTime |    10 |         290.6 ns |          10.86 ns |          29.37 ns |         300.0 ns |
|             ShellRunTime |    10 |       1,374.1 ns |          31.42 ns |          68.98 ns |       1,400.0 ns |
|         ShellCompileTime |    10 |       1,134.5 ns |          25.42 ns |          69.60 ns |       1,100.0 ns |
|          CocktailRunTime |    10 |       1,677.9 ns |          37.14 ns |         101.05 ns |       1,700.0 ns |
|      CocktailCompileTime |    10 |         400.0 ns |           0.00 ns |           0.00 ns |         400.0 ns |
|             CycleRunTime |    10 |       2,204.9 ns |          47.95 ns |          86.46 ns |       2,200.0 ns |
|         CycleCompileTime |    10 |         576.4 ns |          16.34 ns |          45.28 ns |         600.0 ns |
|           PancakeRunTime |    10 |       1,765.7 ns |          38.90 ns |          63.91 ns |       1,800.0 ns |
|       PancakeCompileTime |    10 |         700.0 ns |           0.00 ns |           0.00 ns |         700.0 ns |
|            StoogeRunTime |    10 |      12,325.5 ns |       1,062.16 ns |       3,098.36 ns |      10,200.0 ns |
|        StoogeCompileTime |    10 |       8,413.3 ns |         105.88 ns |          99.04 ns |       8,400.0 ns |
|              SlowRunTime |    10 |       3,956.8 ns |          80.39 ns |         136.51 ns |       4,000.0 ns |
|          SlowCompileTime |    10 |       2,770.6 ns |          58.11 ns |          93.84 ns |       2,800.0 ns |
|              BogoRunTime |    10 | 471,579,735.4 ns | 156,142,568.21 ns | 457,939,123.26 ns | 268,604,300.0 ns |
|          BogoCompileTime |    10 | 389,693,315.8 ns | 111,798,124.96 ns | 320,769,777.77 ns | 310,239,500.0 ns |
|          **SystemArraySort** |  **1000** |      **27,676.9 ns** |         **434.96 ns** |         **363.21 ns** |      **27,700.0 ns** |
|  SystemArraySortDelegate |  1000 |      64,407.7 ns |         287.00 ns |         239.66 ns |      64,400.0 ns |
| SystemArraySortIComparer |  1000 |      90,613.0 ns |       4,654.38 ns |      13,723.55 ns |      93,750.0 ns |
|            BubbleRunTime |  1000 |   5,568,454.5 ns |     432,065.66 ns |   1,267,173.78 ns |   5,756,900.0 ns |
|        BubbleCompileTime |  1000 |   1,438,161.5 ns |      26,210.65 ns |      21,887.08 ns |   1,429,000.0 ns |
|         SelectionRunTime |  1000 |   2,880,250.0 ns |      56,813.37 ns |      69,771.92 ns |   2,861,850.0 ns |
|     SelectionCompileTime |  1000 |     720,196.9 ns |      21,590.08 ns |      62,292.33 ns |     738,000.0 ns |
|         InsertionRunTime |  1000 |   1,394,435.0 ns |      27,274.11 ns |      31,408.90 ns |   1,387,800.0 ns |
|     InsertionCompileTime |  1000 |     260,537.2 ns |       8,233.71 ns |      23,491.23 ns |     252,500.0 ns |
|             QuickRunTime |  1000 |     142,171.4 ns |       2,756.26 ns |       3,952.95 ns |     140,900.0 ns |
|         QuickCompileTime |  1000 |      62,207.7 ns |       1,220.73 ns |       1,019.36 ns |      62,100.0 ns |
|             MergeRunTime |  1000 |     108,796.6 ns |       3,646.37 ns |       9,981.88 ns |     106,900.0 ns |
|         MergeCompileTime |  1000 |      62,541.3 ns |       1,706.73 ns |       4,813.86 ns |      61,200.0 ns |
|              HeapRunTime |  1000 |     315,691.4 ns |       7,214.62 ns |      20,466.69 ns |     307,200.0 ns |
|          HeapCompileTime |  1000 |     285,116.7 ns |       5,690.86 ns |      12,728.42 ns |     282,850.0 ns |
|           OddEvenRunTime |  1000 |   2,813,612.0 ns |     230,424.48 ns |     679,411.79 ns |   3,103,400.0 ns |
|       OddEvenCompileTime |  1000 |     811,844.2 ns |      18,519.08 ns |      53,134.71 ns |     796,600.0 ns |
|             GnomeRunTime |  1000 |   3,015,538.5 ns |      54,837.75 ns |     113,249.47 ns |   2,980,250.0 ns |
|         GnomeCompileTime |  1000 |     627,844.4 ns |      12,464.12 ns |      34,744.99 ns |     618,800.0 ns |
|              CombRunTime |  1000 |     166,465.0 ns |       3,288.01 ns |       7,354.11 ns |     163,450.0 ns |
|          CombCompileTime |  1000 |      52,878.6 ns |         262.43 ns |         232.64 ns |      52,850.0 ns |
|             ShellRunTime |  1000 |     115,482.1 ns |       2,234.65 ns |       3,204.86 ns |     114,150.0 ns |
|         ShellCompileTime |  1000 |      51,588.5 ns |         627.12 ns |         523.67 ns |      51,850.0 ns |
|          CocktailRunTime |  1000 |   2,987,832.0 ns |     241,433.75 ns |     711,872.86 ns |   3,359,300.0 ns |
|      CocktailCompileTime |  1000 |   1,051,884.9 ns |      20,811.97 ns |      43,442.34 ns |   1,049,300.0 ns |
|             CycleRunTime |  1000 |   6,778,753.8 ns |     174,333.88 ns |     456,201.38 ns |   6,663,000.0 ns |
|         CycleCompileTime |  1000 |   2,299,450.0 ns |      45,579.02 ns |      89,968.55 ns |   2,305,450.0 ns |
|           PancakeRunTime |  1000 |   3,092,948.5 ns |      60,638.32 ns |      96,178.80 ns |   3,062,400.0 ns |
|       PancakeCompileTime |  1000 |     534,353.2 ns |      14,298.37 ns |      40,794.06 ns |     523,100.0 ns |
|            StoogeRunTime |  1000 | 459,306,675.3 ns |  16,162,919.46 ns |  46,891,560.08 ns | 439,382,300.0 ns |
|        StoogeCompileTime |  1000 | 276,878,284.2 ns |   5,523,138.57 ns |   6,138,953.89 ns | 274,912,200.0 ns |
|              SlowRunTime |  1000 |               NA |                NA |                NA |               NA |
|          SlowCompileTime |  1000 |               NA |                NA |                NA |               NA |
|              BogoRunTime |  1000 |               NA |                NA |                NA |               NA |
|          BogoCompileTime |  1000 |               NA |                NA |                NA |               NA |
|          **SystemArraySort** | **10000** |     **373,387.9 ns** |       **7,434.74 ns** |      **11,792.28 ns** |     **365,200.0 ns** |
|  SystemArraySortDelegate | 10000 |     875,882.2 ns |      17,416.91 ns |      33,137.48 ns |     869,500.0 ns |
| SystemArraySortIComparer | 10000 |     997,991.8 ns |      19,859.10 ns |      39,660.77 ns |     996,400.0 ns |
|            BubbleRunTime | 10000 | 446,263,627.8 ns |   8,428,210.82 ns |   9,018,091.15 ns | 445,007,900.0 ns |
|        BubbleCompileTime | 10000 | 195,086,021.0 ns |   6,107,351.57 ns |  18,007,663.89 ns | 188,378,750.0 ns |
|         SelectionRunTime | 10000 | 211,935,802.2 ns |   5,553,537.49 ns |  15,754,461.50 ns | 208,347,000.0 ns |
|     SelectionCompileTime | 10000 |  73,739,130.0 ns |   1,471,849.53 ns |   2,973,208.63 ns |  73,969,500.0 ns |
|         InsertionRunTime | 10000 |  97,344,313.5 ns |   1,931,626.74 ns |   3,989,144.18 ns |  96,730,100.0 ns |
|     InsertionCompileTime | 10000 |  26,845,495.8 ns |   1,231,789.45 ns |   3,534,234.82 ns |  25,305,300.0 ns |
|             QuickRunTime | 10000 |   1,895,021.4 ns |      35,045.68 ns |      31,067.08 ns |   1,882,750.0 ns |
|         QuickCompileTime | 10000 |   1,049,131.1 ns |      39,233.40 ns |     109,367.03 ns |   1,060,750.0 ns |
|             MergeRunTime | 10000 |   1,676,502.0 ns |     136,119.72 ns |     401,352.06 ns |   1,657,500.0 ns |
|         MergeCompileTime | 10000 |   1,128,268.1 ns |      24,371.72 ns |      68,340.92 ns |   1,105,900.0 ns |
|              HeapRunTime | 10000 |   3,836,647.0 ns |     497,381.64 ns |   1,466,540.99 ns |   4,258,950.0 ns |
|          HeapCompileTime | 10000 |   2,772,555.0 ns |     481,026.84 ns |   1,418,318.50 ns |   3,730,000.0 ns |
|           OddEvenRunTime | 10000 | 228,736,897.6 ns |   4,496,898.15 ns |   8,108,834.97 ns | 226,603,200.0 ns |
|       OddEvenCompileTime | 10000 | 107,086,767.0 ns |   4,132,450.94 ns |  12,184,624.84 ns | 104,965,450.0 ns |
|             GnomeRunTime | 10000 | 214,599,905.3 ns |   5,444,946.67 ns |  14,720,729.93 ns | 211,160,850.0 ns |
|         GnomeCompileTime | 10000 |  68,435,492.0 ns |   2,968,660.29 ns |   8,753,161.86 ns |  64,460,350.0 ns |
|              CombRunTime | 10000 |   2,505,591.0 ns |     238,904.20 ns |     704,414.42 ns |   2,197,600.0 ns |
|          CombCompileTime | 10000 |     814,242.3 ns |      39,446.13 ns |     114,440.38 ns |     756,400.0 ns |
|             ShellRunTime | 10000 |   1,904,678.1 ns |     116,314.65 ns |     335,594.36 ns |   1,860,500.0 ns |
|         ShellCompileTime | 10000 |     918,010.3 ns |      41,114.40 ns |     119,280.34 ns |     919,100.0 ns |
|          CocktailRunTime | 10000 | 235,205,400.0 ns |   7,741,605.68 ns |  21,835,336.18 ns | 225,081,500.0 ns |
|      CocktailCompileTime | 10000 | 109,793,525.6 ns |   1,968,058.18 ns |   3,548,814.85 ns | 108,680,050.0 ns |
|             CycleRunTime | 10000 | 758,220,283.0 ns |  36,542,340.18 ns | 107,745,914.50 ns | 713,811,150.0 ns |
|         CycleCompileTime | 10000 | 240,248,746.7 ns |   4,460,244.43 ns |   4,172,115.41 ns | 238,933,300.0 ns |
|           PancakeRunTime | 10000 | 212,381,853.3 ns |   4,019,236.09 ns |   3,759,595.93 ns | 211,954,100.0 ns |
|       PancakeCompileTime | 10000 |  46,376,547.6 ns |     885,219.00 ns |   1,053,790.25 ns |  46,107,800.0 ns |
|            StoogeRunTime | 10000 |               NA |                NA |                NA |               NA |
|        StoogeCompileTime | 10000 |               NA |                NA |                NA |               NA |
|              SlowRunTime | 10000 |               NA |                NA |                NA |               NA |
|          SlowCompileTime | 10000 |               NA |                NA |                NA |               NA |
|              BogoRunTime | 10000 |               NA |                NA |                NA |               NA |
|          BogoCompileTime | 10000 |               NA |                NA |                NA |               NA |

Benchmarks with issues:
  Sort_Benchmarks.SlowRunTime: Job-APNGAA(InvocationCount=1, UnrollFactor=1) [N=1000]
  Sort_Benchmarks.SlowCompileTime: Job-APNGAA(InvocationCount=1, UnrollFactor=1) [N=1000]
  Sort_Benchmarks.BogoRunTime: Job-APNGAA(InvocationCount=1, UnrollFactor=1) [N=1000]
  Sort_Benchmarks.BogoCompileTime: Job-APNGAA(InvocationCount=1, UnrollFactor=1) [N=1000]
  Sort_Benchmarks.StoogeRunTime: Job-APNGAA(InvocationCount=1, UnrollFactor=1) [N=10000]
  Sort_Benchmarks.StoogeCompileTime: Job-APNGAA(InvocationCount=1, UnrollFactor=1) [N=10000]
  Sort_Benchmarks.SlowRunTime: Job-APNGAA(InvocationCount=1, UnrollFactor=1) [N=10000]
  Sort_Benchmarks.SlowCompileTime: Job-APNGAA(InvocationCount=1, UnrollFactor=1) [N=10000]
  Sort_Benchmarks.BogoRunTime: Job-APNGAA(InvocationCount=1, UnrollFactor=1) [N=10000]
  Sort_Benchmarks.BogoCompileTime: Job-APNGAA(InvocationCount=1, UnrollFactor=1) [N=10000]

# Data Structures

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1316 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|                         Method | RandomTestData |             Mean |           Error |           StdDev |           Median |
|------------------------------- |--------------- |-----------------:|----------------:|-----------------:|-----------------:|
|                  **ListArray_Add** | **Person[100000]** |   **2,285,342.4 ns** |    **42,361.55 ns** |     **79,565.25 ns** |   **2,281,276.4 ns** |
|      ListArray_AddWithCapacity | Person[100000] |   1,795,587.4 ns |    34,903.12 ns |     59,268.15 ns |   1,801,331.8 ns |
|                            Add | Person[100000] |   2,594,485.3 ns |    51,616.89 ns |    107,743.67 ns |   2,593,817.6 ns |
|             QueueArray_Enqueue | Person[100000] |   3,101,683.3 ns |    61,525.58 ns |    166,337.97 ns |   3,019,151.6 ns |
| QueueArray_EnqueueWithCapacity | Person[100000] |   2,988,840.4 ns |    38,027.82 ns |     35,571.25 ns |   2,994,517.0 ns |
|            QueueLinked_Enqueue | Person[100000] |   2,153,740.8 ns |    30,382.07 ns |     28,419.41 ns |   2,146,433.6 ns |
|                StackArray_Push | Person[100000] |   1,919,690.6 ns |    15,790.45 ns |     12,328.15 ns |   1,920,148.0 ns |
|    StackArray_PushWithCapacity | Person[100000] |   1,581,830.9 ns |    30,551.52 ns |     84,658.11 ns |   1,563,808.6 ns |
|               StackLinked_Push | Person[100000] |   2,104,717.2 ns |    41,841.10 ns |     51,384.63 ns |   2,110,622.3 ns |
|       AvlTreeLinked_AddRunTime | Person[100000] | 239,457,861.9 ns | 4,788,234.86 ns | 11,001,765.87 ns | 238,182,033.3 ns |
|   AvlTreeLinked_AddCompileTime | Person[100000] | 241,620,916.3 ns | 4,833,643.96 ns | 14,252,108.27 ns | 236,429,883.3 ns |
|        RedBlackTree_AddRunTime | Person[100000] | 235,861,633.3 ns | 4,597,248.50 ns |  5,109,829.52 ns | 235,987,500.0 ns |
|    RedBlackTree_AddCompileTime | Person[100000] | 242,473,541.6 ns | 5,284,431.55 ns | 15,331,094.12 ns | 241,241,733.3 ns |
|       SetHashLinked_AddRunTime | Person[100000] |  40,493,740.2 ns |   906,044.37 ns |  2,657,271.31 ns |  39,780,666.7 ns |
|   SetHashLinked_AddCompileTime | Person[100000] |  37,621,726.5 ns |   745,015.58 ns |  1,504,968.22 ns |  37,160,846.7 ns |
|                  **ListArray_Add** |    **Person[100]** |       **1,034.9 ns** |         **8.56 ns** |          **7.59 ns** |       **1,037.1 ns** |
|      ListArray_AddWithCapacity |    Person[100] |         786.2 ns |        12.98 ns |         16.88 ns |         777.7 ns |
|                            Add |    Person[100] |       1,371.1 ns |        25.68 ns |         44.97 ns |       1,349.6 ns |
|             QueueArray_Enqueue |    Person[100] |       1,226.9 ns |        24.39 ns |         42.72 ns |       1,226.6 ns |
| QueueArray_EnqueueWithCapacity |    Person[100] |       1,066.4 ns |        16.61 ns |         13.87 ns |       1,073.9 ns |
|            QueueLinked_Enqueue |    Person[100] |       1,087.6 ns |        20.60 ns |         19.27 ns |       1,085.8 ns |
|                StackArray_Push |    Person[100] |         846.5 ns |        16.88 ns |         42.36 ns |         855.0 ns |
|    StackArray_PushWithCapacity |    Person[100] |         625.1 ns |        10.95 ns |         13.45 ns |         619.8 ns |
|               StackLinked_Push |    Person[100] |       1,084.5 ns |        19.10 ns |         17.86 ns |       1,076.0 ns |
|       AvlTreeLinked_AddRunTime |    Person[100] |      55,723.6 ns |       790.78 ns |        660.34 ns |      55,491.1 ns |
|   AvlTreeLinked_AddCompileTime |    Person[100] |      57,279.3 ns |     1,093.51 ns |      1,421.87 ns |      56,842.8 ns |
|        RedBlackTree_AddRunTime |    Person[100] |      64,029.5 ns |     1,017.33 ns |        849.52 ns |      64,057.7 ns |
|    RedBlackTree_AddCompileTime |    Person[100] |      65,265.7 ns |       983.14 ns |        871.53 ns |      65,564.8 ns |
|       SetHashLinked_AddRunTime |    Person[100] |       7,091.6 ns |       140.65 ns |        161.97 ns |       7,043.2 ns |
|   SetHashLinked_AddCompileTime |    Person[100] |       5,874.2 ns |       111.43 ns |        114.43 ns |       5,854.4 ns |

# Random

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1316 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  Job-APNGAA : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                       Method |      N |         Mean |       Error |      StdDev |       Median |
|----------------------------- |------- |-------------:|------------:|------------:|-------------:|
| **Next_IEnumerable_TotalWeight** |     **10** |     **2.955 μs** |   **0.4523 μs** |   **1.2380 μs** |     **2.500 μs** |
|             Next_IEnumerable |     10 |     2.613 μs |   0.1271 μs |   0.3392 μs |     2.500 μs |
| **Next_IEnumerable_TotalWeight** |    **100** |     **3.359 μs** |   **0.1351 μs** |   **0.3583 μs** |     **3.300 μs** |
|             Next_IEnumerable |    100 |     6.860 μs |   0.5139 μs |   1.3980 μs |     6.750 μs |
| **Next_IEnumerable_TotalWeight** |   **1000** |    **17.353 μs** |   **1.3569 μs** |   **3.8933 μs** |    **15.200 μs** |
|             Next_IEnumerable |   1000 |    32.282 μs |   1.2775 μs |   3.7266 μs |    30.850 μs |
| **Next_IEnumerable_TotalWeight** |  **10000** |   **137.651 μs** |   **5.6417 μs** |  **16.4572 μs** |   **129.500 μs** |
|             Next_IEnumerable |  10000 |   279.346 μs |   5.5487 μs |   9.1166 μs |   276.200 μs |
| **Next_IEnumerable_TotalWeight** | **100000** | **1,340.264 μs** |  **26.5897 μs** |  **73.6799 μs** | **1,327.000 μs** |
|             Next_IEnumerable | 100000 | 2,750.768 μs | 105.2400 μs | 305.3204 μs | 2,788.600 μs |

# [Numeric] To English Words

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1316 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|                 Method |  Range |     Mean |    Error |   StdDev |
|----------------------- |------- |---------:|---------:|---------:|
| **Decimal_ToEnglishWords** |     **10** | **77.13 ms** | **1.489 ms** | **1.529 ms** |
| **Decimal_ToEnglishWords** |    **100** | **74.62 ms** | **0.770 ms** | **0.683 ms** |
| **Decimal_ToEnglishWords** |   **1000** | **70.74 ms** | **1.397 ms** | **2.334 ms** |
| **Decimal_ToEnglishWords** |  **10000** | **71.34 ms** | **1.390 ms** | **1.487 ms** |
| **Decimal_ToEnglishWords** | **100000** | **69.86 ms** | **0.920 ms** | **0.815 ms** |

# Permute

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1316 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  Job-APNGAA : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|          Method |  N |               Mean |            Error |           StdDev |             Median |
|---------------- |--- |-------------------:|-----------------:|-----------------:|-------------------:|
|       **Recursive** |  **1** |           **358.1 ns** |         **69.99 ns** |        **198.54 ns** |           **300.0 ns** |
|       Iterative |  1 |           993.8 ns |        102.62 ns |        270.35 ns |           900.0 ns |
| RecursiveStruct |  1 |           225.8 ns |         28.02 ns |         77.65 ns |           200.0 ns |
| IterativeStruct |  1 |         1,223.3 ns |        199.19 ns |        555.27 ns |         1,000.0 ns |
|       **Recursive** |  **2** |           **340.9 ns** |         **20.94 ns** |         **59.41 ns** |           **300.0 ns** |
|       Iterative |  2 |         1,002.3 ns |         51.98 ns |        141.40 ns |         1,000.0 ns |
| RecursiveStruct |  2 |           288.2 ns |         12.78 ns |         32.53 ns |           300.0 ns |
| IterativeStruct |  2 |         1,017.2 ns |         76.02 ns |        208.10 ns |         1,000.0 ns |
|       **Recursive** |  **3** |           **500.0 ns** |          **0.00 ns** |          **0.00 ns** |           **500.0 ns** |
|       Iterative |  3 |         1,095.1 ns |         57.72 ns |        153.08 ns |         1,100.0 ns |
| RecursiveStruct |  3 |           504.1 ns |         39.66 ns |        115.69 ns |           500.0 ns |
| IterativeStruct |  3 |           998.9 ns |         61.19 ns |        169.55 ns |           900.0 ns |
|       **Recursive** |  **4** |         **1,135.0 ns** |         **26.57 ns** |         **69.54 ns** |         **1,100.0 ns** |
|       Iterative |  4 |         1,331.4 ns |         64.10 ns |        174.40 ns |         1,300.0 ns |
| RecursiveStruct |  4 |         1,012.0 ns |         26.20 ns |         73.89 ns |         1,000.0 ns |
| IterativeStruct |  4 |         1,228.6 ns |         68.75 ns |        184.70 ns |         1,200.0 ns |
|       **Recursive** |  **5** |         **5,224.7 ns** |        **475.17 ns** |      **1,378.56 ns** |         **4,600.0 ns** |
|       Iterative |  5 |         2,521.6 ns |        100.82 ns |        277.70 ns |         2,400.0 ns |
| RecursiveStruct |  5 |         3,968.5 ns |         79.23 ns |        111.07 ns |         3,950.0 ns |
| IterativeStruct |  5 |         2,430.7 ns |        177.90 ns |        489.98 ns |         2,200.0 ns |
|       **Recursive** |  **6** |        **23,605.6 ns** |        **374.48 ns** |        **400.69 ns** |        **23,700.0 ns** |
|       Iterative |  6 |         9,040.0 ns |        175.20 ns |        163.88 ns |         9,000.0 ns |
| RecursiveStruct |  6 |        21,933.3 ns |        300.80 ns |        234.84 ns |        22,000.0 ns |
| IterativeStruct |  6 |         8,258.1 ns |        442.22 ns |      1,254.50 ns |         7,600.0 ns |
|       **Recursive** |  **7** |       **172,625.0 ns** |      **3,944.54 ns** |     **11,125.64 ns** |       **170,200.0 ns** |
|       Iterative |  7 |        61,489.9 ns |      2,735.61 ns |      8,023.07 ns |        57,000.0 ns |
| RecursiveStruct |  7 |       170,232.2 ns |      3,834.91 ns |     10,690.19 ns |       167,650.0 ns |
| IterativeStruct |  7 |        54,806.2 ns |      3,256.01 ns |      9,446.27 ns |        49,700.0 ns |
|       **Recursive** |  **8** |     **1,368,048.4 ns** |     **27,259.23 ns** |     **62,083.05 ns** |     **1,357,650.0 ns** |
|       Iterative |  8 |       482,213.5 ns |     13,317.16 ns |     38,423.06 ns |       470,550.0 ns |
| RecursiveStruct |  8 |     1,166,740.7 ns |    148,354.22 ns |    403,608.59 ns |     1,337,400.0 ns |
| IterativeStruct |  8 |       403,142.6 ns |     10,662.34 ns |     30,420.26 ns |       395,850.0 ns |
|       **Recursive** |  **9** |     **4,859,093.1 ns** |    **160,649.14 ns** |    **439,774.42 ns** |     **4,754,100.0 ns** |
|       Iterative |  9 |     3,200,890.0 ns |    403,954.28 ns |  1,191,068.31 ns |     3,939,250.0 ns |
| RecursiveStruct |  9 |     4,614,089.2 ns |    124,762.25 ns |    333,015.70 ns |     4,512,100.0 ns |
| IterativeStruct |  9 |     2,791,931.0 ns |    374,752.87 ns |  1,104,967.28 ns |     3,515,200.0 ns |
|       **Recursive** | **10** |    **45,860,687.0 ns** |    **901,276.12 ns** |  **1,139,825.71 ns** |    **45,813,300.0 ns** |
|       Iterative | 10 |    19,991,050.0 ns |    212,420.69 ns |    188,305.41 ns |    19,978,450.0 ns |
| RecursiveStruct | 10 |    45,028,071.4 ns |    892,680.58 ns |    791,338.08 ns |    44,858,450.0 ns |
| IterativeStruct | 10 |    14,261,908.0 ns |    268,746.43 ns |    358,768.77 ns |    14,139,200.0 ns |
|       **Recursive** | **11** |   **516,039,438.5 ns** |  **6,893,440.60 ns** |  **5,756,335.11 ns** |   **515,851,100.0 ns** |
|       Iterative | 11 |   209,356,640.0 ns |  4,137,867.27 ns |  3,870,563.61 ns |   209,218,000.0 ns |
| RecursiveStruct | 11 |   500,986,242.9 ns |  4,038,959.36 ns |  3,580,432.27 ns |   501,034,000.0 ns |
| IterativeStruct | 11 |   166,627,765.4 ns |  2,627,863.37 ns |  2,194,384.93 ns |   167,157,750.0 ns |
|       **Recursive** | **12** | **6,262,202,978.6 ns** | **65,461,996.05 ns** | **58,030,354.41 ns** | **6,243,439,850.0 ns** |
|       Iterative | 12 | 2,669,379,466.7 ns | 33,731,743.09 ns | 31,552,693.42 ns | 2,656,714,800.0 ns |
| RecursiveStruct | 12 | 6,014,308,915.4 ns | 70,148,580.89 ns | 58,577,242.09 ns | 5,983,791,600.0 ns |
| IterativeStruct | 12 | 2,100,294,175.0 ns | 40,906,356.70 ns | 40,175,511.95 ns | 2,111,504,950.0 ns |

# Map vs Dictionary (Add)

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1316 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|       Method |     N |         Mean |       Error |       StdDev |       Median |
|------------- |------ |-------------:|------------:|-------------:|-------------:|
| **MapDelegates** |    **10** |   **3,092.2 ns** |   **330.46 ns** |    **921.19 ns** |   **2,600.0 ns** |
|   MapStructs |    10 |     504.4 ns |     9.97 ns |     21.45 ns |     499.1 ns |
|   Dictionary |    10 |     237.0 ns |     3.14 ns |      2.79 ns |     236.7 ns |
| **MapDelegates** |   **100** |   **4,604.9 ns** |    **65.06 ns** |     **60.86 ns** |   **4,576.3 ns** |
|   MapStructs |   100 |   3,957.8 ns |    46.12 ns |     38.52 ns |   3,953.2 ns |
|   Dictionary |   100 |   1,964.8 ns |    23.23 ns |     21.73 ns |   1,972.5 ns |
| **MapDelegates** |  **1000** |  **37,277.8 ns** |   **464.88 ns** |    **412.10 ns** |  **37,306.8 ns** |
|   MapStructs |  1000 |  31,133.2 ns |   331.05 ns |    293.47 ns |  31,141.7 ns |
|   Dictionary |  1000 |  19,486.6 ns |   350.93 ns |    328.26 ns |  19,531.6 ns |
| **MapDelegates** | **10000** | **562,365.3 ns** | **6,455.37 ns** |  **5,722.52 ns** | **560,431.8 ns** |
|   MapStructs | 10000 | 519,934.0 ns | 6,130.20 ns |  4,786.05 ns | 518,642.8 ns |
|   Dictionary | 10000 | 293,482.2 ns | 5,804.04 ns | 10,613.01 ns | 295,374.5 ns |

# Map vs Dictionary (Look Up)

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1316 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  Job-APNGAA : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|       Method |     N |         Mean |        Error |       StdDev |       Median |
|------------- |------ |-------------:|-------------:|-------------:|-------------:|
| **MapDelegates** |    **10** |     **701.1 ns** |     **67.73 ns** |    **194.33 ns** |     **600.0 ns** |
|   MapStructs |    10 |     376.8 ns |     48.88 ns |    140.25 ns |     300.0 ns |
|   Dictionary |    10 |     311.2 ns |     46.41 ns |    135.37 ns |     300.0 ns |
| **MapDelegates** |   **100** |   **2,129.0 ns** |     **45.35 ns** |     **69.25 ns** |   **2,100.0 ns** |
|   MapStructs |   100 |   1,181.0 ns |     27.60 ns |     71.75 ns |   1,200.0 ns |
|   Dictionary |   100 |   1,008.5 ns |     24.13 ns |     53.46 ns |   1,000.0 ns |
| **MapDelegates** |  **1000** |  **17,856.1 ns** |    **356.19 ns** |    **642.28 ns** |  **17,900.0 ns** |
|   MapStructs |  1000 |  10,033.8 ns |    194.09 ns |    497.52 ns |   9,900.0 ns |
|   Dictionary |  1000 |   8,603.4 ns |    170.54 ns |    249.98 ns |   8,600.0 ns |
| **MapDelegates** | **10000** | **153,947.8 ns** | **11,714.71 ns** | **32,849.30 ns** | **165,350.0 ns** |
|   MapStructs | 10000 |  99,233.3 ns |  5,356.85 ns | 14,664.30 ns |  97,400.0 ns |
|   Dictionary | 10000 |  91,859.8 ns |  5,022.76 ns | 14,166.79 ns |  90,350.0 ns |

# Span vs Array Sorting

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1316 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  Job-APNGAA : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                 Method |     N |             Mean |            Error |          StdDev |           Median |
|----------------------- |------ |-----------------:|-----------------:|----------------:|-----------------:|
|     **ArrayBubbleRunTime** |    **10** |       **2,326.4 ns** |        **242.09 ns** |       **670.82 ns** |       **1,950.0 ns** |
| ArrayBubbleCompileTime |    10 |         486.5 ns |         52.41 ns |       151.22 ns |         400.0 ns |
|      SpanBubbleRunTime |    10 |       2,459.1 ns |        284.21 ns |       782.81 ns |       2,300.0 ns |
|  SpanBubbleCompileTime |    10 |         429.2 ns |         27.85 ns |        77.17 ns |         400.0 ns |
|     **ArrayBubbleRunTime** |  **1000** |   **5,761,469.0 ns** |    **408,140.88 ns** | **1,203,412.61 ns** |   **5,118,950.0 ns** |
| ArrayBubbleCompileTime |  1000 |   1,737,946.8 ns |     33,102.90 ns |    75,392.05 ns |   1,732,500.0 ns |
|      SpanBubbleRunTime |  1000 |   6,457,400.0 ns |    103,753.51 ns |    91,974.79 ns |   6,480,850.0 ns |
|  SpanBubbleCompileTime |  1000 |   1,555,967.7 ns |     31,003.93 ns |    72,470.69 ns |   1,539,400.0 ns |
|     **ArrayBubbleRunTime** | **10000** | **518,694,480.0 ns** | **10,266,381.15 ns** | **9,603,179.29 ns** | **515,454,100.0 ns** |
| ArrayBubbleCompileTime | 10000 | 197,976,292.3 ns |  2,832,979.52 ns | 2,365,666.20 ns | 197,512,500.0 ns |
|      SpanBubbleRunTime | 10000 | 444,006,056.7 ns |  8,585,645.59 ns | 8,031,018.21 ns | 442,002,450.0 ns |
|  SpanBubbleCompileTime | 10000 | 177,006,850.0 ns |  3,511,711.01 ns | 3,284,856.66 ns | 176,566,250.0 ns |

