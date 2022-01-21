# Sorting Algorithms

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1469 (21H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.200-preview.21617.4
  [Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
  Job-BVYZER : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                   Method |     N |             Mean |             Error |            StdDev |           Median |
|------------------------- |------ |-----------------:|------------------:|------------------:|-----------------:|
|          **SystemArraySort** |    **10** |         **439.6 ns** |          **25.43 ns** |          **71.30 ns** |         **400.0 ns** |
|  SystemArraySortDelegate |    10 |         612.8 ns |          28.26 ns |          80.63 ns |         600.0 ns |
| SystemArraySortIComparer |    10 |       3,210.6 ns |         120.67 ns |         326.24 ns |       3,100.0 ns |
|            BubbleRunTime |    10 |       1,916.7 ns |          41.70 ns |          76.24 ns |       1,900.0 ns |
|        BubbleCompileTime |    10 |         771.9 ns |          26.09 ns |          72.28 ns |         800.0 ns |
|         SelectionRunTime |    10 |       1,745.9 ns |         105.37 ns |         307.36 ns |       1,600.0 ns |
|     SelectionCompileTime |    10 |         607.0 ns |          37.73 ns |         111.24 ns |         600.0 ns |
|         InsertionRunTime |    10 |       1,469.7 ns |          33.26 ns |          78.39 ns |       1,500.0 ns |
|     InsertionCompileTime |    10 |         452.1 ns |          24.11 ns |          69.55 ns |         400.0 ns |
|             QuickRunTime |    10 |       1,967.0 ns |         115.32 ns |         329.02 ns |       1,800.0 ns |
|         QuickCompileTime |    10 |         804.1 ns |          48.21 ns |         139.88 ns |         800.0 ns |
|             MergeRunTime |    10 |       1,746.8 ns |          38.88 ns |         101.07 ns |       1,700.0 ns |
|         MergeCompileTime |    10 |       1,498.9 ns |          43.35 ns |         121.56 ns |       1,500.0 ns |
|              HeapRunTime |    10 |       2,538.2 ns |         123.64 ns |         342.62 ns |       2,400.0 ns |
|          HeapCompileTime |    10 |       1,334.7 ns |          52.78 ns |         151.42 ns |       1,300.0 ns |
|             IntroRunTime |    10 |       1,551.2 ns |          47.12 ns |         126.58 ns |       1,500.0 ns |
|         IntroCompileTime |    10 |         543.8 ns |          20.32 ns |          56.32 ns |         500.0 ns |
|           OddEvenRunTime |    10 |       1,572.8 ns |          36.83 ns |         103.88 ns |       1,600.0 ns |
|       OddEvenCompileTime |    10 |         613.8 ns |          23.89 ns |          68.16 ns |         600.0 ns |
|             GnomeRunTime |    10 |       1,738.1 ns |          38.64 ns |          88.77 ns |       1,700.0 ns |
|         GnomeCompileTime |    10 |         684.8 ns |          39.08 ns |         114.61 ns |         600.0 ns |
|              CombRunTime |    10 |       1,595.7 ns |          42.67 ns |         120.36 ns |       1,600.0 ns |
|          CombCompileTime |    10 |         522.1 ns |          30.58 ns |          87.74 ns |         500.0 ns |
|             ShellRunTime |    10 |       1,541.4 ns |          34.62 ns |          84.26 ns |       1,500.0 ns |
|         ShellCompileTime |    10 |       1,386.2 ns |          51.30 ns |         146.35 ns |       1,400.0 ns |
|          CocktailRunTime |    10 |       1,777.4 ns |          39.46 ns |          82.37 ns |       1,800.0 ns |
|      CocktailCompileTime |    10 |         828.7 ns |          30.68 ns |          87.53 ns |         800.0 ns |
|             CycleRunTime |    10 |       2,270.0 ns |          39.06 ns |          78.90 ns |       2,300.0 ns |
|         CycleCompileTime |    10 |       1,123.6 ns |          41.08 ns |         113.84 ns |       1,100.0 ns |
|           PancakeRunTime |    10 |       1,892.6 ns |          40.81 ns |          92.12 ns |       1,950.0 ns |
|       PancakeCompileTime |    10 |         859.1 ns |          23.38 ns |          66.33 ns |         900.0 ns |
|            StoogeRunTime |    10 |       9,905.3 ns |         135.70 ns |         150.83 ns |       9,900.0 ns |
|        StoogeCompileTime |    10 |       8,523.1 ns |          86.82 ns |          72.50 ns |       8,500.0 ns |
|              SlowRunTime |    10 |       4,116.0 ns |          85.63 ns |         114.31 ns |       4,100.0 ns |
|          SlowCompileTime |    10 |       2,873.6 ns |          61.39 ns |         168.07 ns |       2,900.0 ns |
|              BogoRunTime |    10 | 424,096,257.9 ns | 111,382,711.78 ns | 319,577,879.50 ns | 326,603,100.0 ns |
|          BogoCompileTime |    10 | 359,858,541.1 ns | 110,332,762.88 ns | 316,565,379.28 ns | 266,327,800.0 ns |
|          **SystemArraySort** |  **1000** |      **30,560.4 ns** |       **1,702.48 ns** |       **4,912.04 ns** |      **27,600.0 ns** |
|  SystemArraySortDelegate |  1000 |      67,884.6 ns |         563.49 ns |         470.54 ns |      67,900.0 ns |
| SystemArraySortIComparer |  1000 |      78,663.0 ns |       1,553.82 ns |       2,178.24 ns |      78,400.0 ns |
|            BubbleRunTime |  1000 |   4,151,658.5 ns |     136,818.85 ns |     362,824.19 ns |   4,044,550.0 ns |
|        BubbleCompileTime |  1000 |   4,963,148.0 ns |     167,748.29 ns |     494,609.63 ns |   5,006,950.0 ns |
|         SelectionRunTime |  1000 |   2,657,178.7 ns |     196,031.56 ns |     559,289.08 ns |   2,744,100.0 ns |
|     SelectionCompileTime |  1000 |   1,665,601.1 ns |      52,024.37 ns |     147,584.47 ns |   1,656,100.0 ns |
|         InsertionRunTime |  1000 |   1,336,716.1 ns |      96,956.15 ns |     265,415.89 ns |   1,385,300.0 ns |
|     InsertionCompileTime |  1000 |     799,776.0 ns |      25,939.38 ns |      74,841.05 ns |     786,350.0 ns |
|             QuickRunTime |  1000 |     146,104.7 ns |       4,405.11 ns |      11,984.44 ns |     140,250.0 ns |
|         QuickCompileTime |  1000 |     117,486.3 ns |       3,458.27 ns |       9,922.42 ns |     112,700.0 ns |
|             MergeRunTime |  1000 |     104,468.5 ns |       1,904.51 ns |       4,017.27 ns |     102,750.0 ns |
|         MergeCompileTime |  1000 |      90,309.6 ns |       2,523.46 ns |       7,199.58 ns |      86,850.0 ns |
|              HeapRunTime |  1000 |     305,411.2 ns |       9,603.13 ns |      26,610.23 ns |     293,800.0 ns |
|          HeapCompileTime |  1000 |     269,632.1 ns |       5,033.64 ns |      13,522.55 ns |     268,700.0 ns |
|             IntroRunTime |  1000 |      81,008.3 ns |       1,221.80 ns |         953.90 ns |      81,400.0 ns |
|         IntroCompileTime |  1000 |      61,917.9 ns |       1,205.62 ns |       1,729.07 ns |      61,650.0 ns |
|           OddEvenRunTime |  1000 |   2,882,238.0 ns |     230,199.81 ns |     678,749.33 ns |   3,128,450.0 ns |
|       OddEvenCompileTime |  1000 |   2,082,878.5 ns |      62,813.11 ns |     178,190.34 ns |   2,050,000.0 ns |
|             GnomeRunTime |  1000 |   2,707,822.0 ns |     194,540.31 ns |     573,606.51 ns |   2,804,200.0 ns |
|         GnomeCompileTime |  1000 |   1,690,678.7 ns |      39,214.55 ns |     108,663.33 ns |   1,681,600.0 ns |
|              CombRunTime |  1000 |     168,153.9 ns |       6,446.68 ns |      17,863.73 ns |     161,900.0 ns |
|          CombCompileTime |  1000 |     114,025.0 ns |       1,172.80 ns |         915.65 ns |     113,750.0 ns |
|             ShellRunTime |  1000 |     120,358.5 ns |       3,785.80 ns |      10,801.09 ns |     114,450.0 ns |
|         ShellCompileTime |  1000 |      95,123.7 ns |       4,039.66 ns |      11,719.78 ns |      91,100.0 ns |
|          CocktailRunTime |  1000 |   3,003,676.0 ns |     229,877.50 ns |     677,798.99 ns |   3,243,950.0 ns |
|      CocktailCompileTime |  1000 |   2,514,984.8 ns |      70,078.43 ns |     205,527.92 ns |   2,456,200.0 ns |
|             CycleRunTime |  1000 |   6,463,850.0 ns |     152,723.82 ns |     405,001.90 ns |   6,384,800.0 ns |
|         CycleCompileTime |  1000 |   6,625,554.0 ns |     124,366.66 ns |     340,451.71 ns |   6,584,700.0 ns |
|           PancakeRunTime |  1000 |   2,749,750.0 ns |     198,013.62 ns |     583,847.63 ns |   2,936,450.0 ns |
|       PancakeCompileTime |  1000 |   1,581,428.4 ns |      45,926.31 ns |     131,771.19 ns |   1,537,800.0 ns |
|            StoogeRunTime |  1000 | 408,470,813.3 ns |   7,854,312.63 ns |   7,346,928.91 ns | 406,921,800.0 ns |
|        StoogeCompileTime |  1000 | 414,823,045.0 ns |   8,069,358.72 ns |   9,292,685.50 ns | 416,861,300.0 ns |
|              SlowRunTime |  1000 |               NA |                NA |                NA |               NA |
|          SlowCompileTime |  1000 |               NA |                NA |                NA |               NA |
|              BogoRunTime |  1000 |               NA |                NA |                NA |               NA |
|          BogoCompileTime |  1000 |               NA |                NA |                NA |               NA |
|          **SystemArraySort** | **10000** |     **383,528.3 ns** |      **10,513.30 ns** |      **29,652.96 ns** |     **378,900.0 ns** |
|  SystemArraySortDelegate | 10000 |     974,164.6 ns |      30,814.96 ns |      88,908.19 ns |     939,850.0 ns |
| SystemArraySortIComparer | 10000 |   1,088,168.1 ns |      25,574.40 ns |      72,965.19 ns |   1,078,950.0 ns |
|            BubbleRunTime | 10000 | 428,516,114.3 ns |   5,440,235.59 ns |   4,822,627.15 ns | 428,480,750.0 ns |
|        BubbleCompileTime | 10000 | 484,112,821.4 ns |   3,718,833.17 ns |   3,296,648.74 ns | 483,764,150.0 ns |
|         SelectionRunTime | 10000 | 191,183,027.6 ns |   3,722,456.51 ns |   5,456,328.49 ns | 189,413,100.0 ns |
|     SelectionCompileTime | 10000 | 178,497,797.4 ns |   3,502,500.99 ns |   6,041,642.13 ns | 177,918,800.0 ns |
|         InsertionRunTime | 10000 |  89,412,651.9 ns |   1,554,852.02 ns |   2,179,685.38 ns |  89,099,300.0 ns |
|     InsertionCompileTime | 10000 |  70,144,792.3 ns |   1,263,513.59 ns |   1,055,091.07 ns |  70,097,900.0 ns |
|             QuickRunTime | 10000 |   1,956,968.8 ns |      60,664.34 ns |     172,094.64 ns |   1,895,900.0 ns |
|         QuickCompileTime | 10000 |   1,616,468.4 ns |      39,142.61 ns |     112,307.50 ns |   1,590,200.0 ns |
|             MergeRunTime | 10000 |   1,462,862.9 ns |      54,669.96 ns |     158,607.47 ns |   1,416,400.0 ns |
|         MergeCompileTime | 10000 |   1,190,146.4 ns |      43,669.18 ns |     126,692.20 ns |   1,147,800.0 ns |
|              HeapRunTime | 10000 |   3,093,205.0 ns |     483,060.93 ns |   1,424,316.04 ns |   2,264,550.0 ns |
|          HeapCompileTime | 10000 |   3,054,873.0 ns |     399,702.35 ns |   1,178,531.41 ns |   3,616,200.0 ns |
|             IntroRunTime | 10000 |   1,113,648.4 ns |      52,831.97 ns |     151,584.82 ns |   1,102,200.0 ns |
|         IntroCompileTime | 10000 |     857,353.2 ns |      19,893.56 ns |      56,757.44 ns |     840,650.0 ns |
|           OddEvenRunTime | 10000 | 220,546,913.3 ns |   3,099,090.36 ns |   2,898,891.04 ns | 219,659,300.0 ns |
|       OddEvenCompileTime | 10000 | 218,622,257.7 ns |   2,351,128.63 ns |   1,963,298.89 ns | 218,875,350.0 ns |
|             GnomeRunTime | 10000 | 202,969,580.8 ns |   2,394,378.93 ns |   1,999,414.84 ns | 202,584,250.0 ns |
|         GnomeCompileTime | 10000 | 164,490,740.0 ns |   2,912,645.62 ns |   2,724,490.52 ns | 164,022,200.0 ns |
|              CombRunTime | 10000 |   2,280,071.7 ns |     212,626.33 ns |     623,596.20 ns |   2,185,900.0 ns |
|          CombCompileTime | 10000 |   1,608,994.8 ns |      43,918.92 ns |     126,716.12 ns |   1,580,300.0 ns |
|             ShellRunTime | 10000 |   1,809,631.2 ns |      56,037.01 ns |     161,679.60 ns |   1,768,900.0 ns |
|         ShellCompileTime | 10000 |   1,334,984.4 ns |      38,578.28 ns |     111,307.17 ns |   1,316,850.0 ns |
|          CocktailRunTime | 10000 | 221,824,703.3 ns |   4,298,401.50 ns |   4,020,727.43 ns | 220,848,650.0 ns |
|      CocktailCompileTime | 10000 | 239,352,485.7 ns |   4,197,100.50 ns |   3,720,620.27 ns | 238,813,700.0 ns |
|             CycleRunTime | 10000 | 673,035,540.0 ns |  11,177,615.96 ns |  10,455,548.91 ns | 670,368,500.0 ns |
|         CycleCompileTime | 10000 | 684,887,392.3 ns |   3,124,591.55 ns |   2,609,175.45 ns | 685,157,600.0 ns |
|           PancakeRunTime | 10000 | 194,495,007.1 ns |   3,410,170.78 ns |   3,023,027.57 ns | 194,083,650.0 ns |
|       PancakeCompileTime | 10000 | 174,066,488.9 ns |   3,422,255.40 ns |   3,661,774.94 ns | 172,792,100.0 ns |
|            StoogeRunTime | 10000 |               NA |                NA |                NA |               NA |
|        StoogeCompileTime | 10000 |               NA |                NA |                NA |               NA |
|              SlowRunTime | 10000 |               NA |                NA |                NA |               NA |
|          SlowCompileTime | 10000 |               NA |                NA |                NA |               NA |
|              BogoRunTime | 10000 |               NA |                NA |                NA |               NA |
|          BogoCompileTime | 10000 |               NA |                NA |                NA |               NA |

Benchmarks with issues:
  SortBenchmarks.SlowRunTime: Job-BVYZER(InvocationCount=1, UnrollFactor=1) [N=1000]
  SortBenchmarks.SlowCompileTime: Job-BVYZER(InvocationCount=1, UnrollFactor=1) [N=1000]
  SortBenchmarks.BogoRunTime: Job-BVYZER(InvocationCount=1, UnrollFactor=1) [N=1000]
  SortBenchmarks.BogoCompileTime: Job-BVYZER(InvocationCount=1, UnrollFactor=1) [N=1000]
  SortBenchmarks.StoogeRunTime: Job-BVYZER(InvocationCount=1, UnrollFactor=1) [N=10000]
  SortBenchmarks.StoogeCompileTime: Job-BVYZER(InvocationCount=1, UnrollFactor=1) [N=10000]
  SortBenchmarks.SlowRunTime: Job-BVYZER(InvocationCount=1, UnrollFactor=1) [N=10000]
  SortBenchmarks.SlowCompileTime: Job-BVYZER(InvocationCount=1, UnrollFactor=1) [N=10000]
  SortBenchmarks.BogoRunTime: Job-BVYZER(InvocationCount=1, UnrollFactor=1) [N=10000]
  SortBenchmarks.BogoCompileTime: Job-BVYZER(InvocationCount=1, UnrollFactor=1) [N=10000]

