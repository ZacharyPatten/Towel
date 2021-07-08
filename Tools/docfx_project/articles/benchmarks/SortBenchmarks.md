# Sorting Algorithms

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=6.0.100-preview.4.21255.9
  [Host]     : .NET Core 5.0.7 (CoreCLR 5.0.721.25508, CoreFX 5.0.721.25508), X64 RyuJIT
  Job-TGYVEK : .NET Core 5.0.7 (CoreCLR 5.0.721.25508, CoreFX 5.0.721.25508), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                   Method |     N |             Mean |             Error |            StdDev |           Median |
|------------------------- |------ |-----------------:|------------------:|------------------:|-----------------:|
|          **SystemArraySort** |    **10** |         **353.5 ns** |          **17.09 ns** |          **50.13 ns** |         **400.0 ns** |
|  SystemArraySortDelegate |    10 |         661.6 ns |          19.91 ns |          58.39 ns |         700.0 ns |
| SystemArraySortIComparer |    10 |       3,304.3 ns |          65.20 ns |          82.45 ns |       3,300.0 ns |
|            BubbleRunTime |    10 |       1,868.9 ns |          40.07 ns |          90.45 ns |       1,900.0 ns |
|        BubbleCompileTime |    10 |         396.8 ns |          11.76 ns |          17.96 ns |         400.0 ns |
|         SelectionRunTime |    10 |       1,632.3 ns |          36.24 ns |          82.53 ns |       1,600.0 ns |
|     SelectionCompileTime |    10 |         391.4 ns |          17.57 ns |          51.53 ns |         350.0 ns |
|         InsertionRunTime |    10 |       1,448.1 ns |          32.93 ns |          86.76 ns |       1,500.0 ns |
|     InsertionCompileTime |    10 |         361.0 ns |          17.31 ns |          51.04 ns |         400.0 ns |
|             QuickRunTime |    10 |       1,827.9 ns |          37.83 ns |          70.12 ns |       1,800.0 ns |
|         QuickCompileTime |    10 |         535.0 ns |          13.73 ns |          35.93 ns |         550.0 ns |
|             MergeRunTime |    10 |       1,718.5 ns |          38.31 ns |         106.17 ns |       1,750.0 ns |
|         MergeCompileTime |    10 |       1,296.4 ns |          27.04 ns |          57.62 ns |       1,300.0 ns |
|              HeapRunTime |    10 |       2,438.9 ns |          50.13 ns |          83.76 ns |       2,400.0 ns |
|          HeapCompileTime |    10 |       1,371.1 ns |          31.31 ns |          79.69 ns |       1,400.0 ns |
|           OddEvenRunTime |    10 |       1,634.1 ns |          42.15 ns |         113.97 ns |       1,600.0 ns |
|       OddEvenCompileTime |    10 |         352.0 ns |          17.03 ns |          50.21 ns |         400.0 ns |
|             GnomeRunTime |    10 |       1,658.5 ns |          37.15 ns |          66.99 ns |       1,600.0 ns |
|         GnomeCompileTime |    10 |         422.2 ns |          17.26 ns |          50.62 ns |         400.0 ns |
|              CombRunTime |    10 |       1,512.5 ns |          33.95 ns |          78.68 ns |       1,500.0 ns |
|          CombCompileTime |    10 |         345.5 ns |          17.06 ns |          50.05 ns |         300.0 ns |
|             ShellRunTime |    10 |       1,518.1 ns |          42.37 ns |         120.90 ns |       1,500.0 ns |
|         ShellCompileTime |    10 |       1,072.1 ns |          24.49 ns |          45.39 ns |       1,100.0 ns |
|          CocktailRunTime |    10 |       2,133.0 ns |         152.51 ns |         435.12 ns |       2,000.0 ns |
|      CocktailCompileTime |    10 |         474.7 ns |          16.41 ns |          48.11 ns |         500.0 ns |
|             CycleRunTime |    10 |       2,314.3 ns |          47.66 ns |          87.15 ns |       2,300.0 ns |
|         CycleCompileTime |    10 |         700.0 ns |           0.00 ns |           0.00 ns |         700.0 ns |
|           PancakeRunTime |    10 |       1,921.3 ns |          39.91 ns |          77.84 ns |       1,900.0 ns |
|       PancakeCompileTime |    10 |         650.0 ns |           0.00 ns |           0.00 ns |         650.0 ns |
|            StoogeRunTime |    10 |       9,959.1 ns |         192.46 ns |         236.36 ns |       9,950.0 ns |
|        StoogeCompileTime |    10 |       8,121.4 ns |         126.54 ns |         112.17 ns |       8,100.0 ns |
|              SlowRunTime |    10 |       4,076.5 ns |          84.56 ns |         172.73 ns |       4,100.0 ns |
|          SlowCompileTime |    10 |       2,969.5 ns |          59.75 ns |         107.75 ns |       2,950.0 ns |
|              BogoRunTime |    10 | 360,780,767.9 ns | 106,273,128.80 ns | 304,917,528.09 ns | 272,635,950.0 ns |
|          BogoCompileTime |    10 | 361,179,578.6 ns | 100,319,805.25 ns | 292,637,580.67 ns | 265,504,650.0 ns |
|          **SystemArraySort** |  **1000** |      **27,471.4 ns** |         **467.06 ns** |         **414.04 ns** |      **27,600.0 ns** |
|  SystemArraySortDelegate |  1000 |      66,991.7 ns |         884.80 ns |         690.79 ns |      67,150.0 ns |
| SystemArraySortIComparer |  1000 |      78,207.7 ns |       1,148.08 ns |         958.70 ns |      77,900.0 ns |
|            BubbleRunTime |  1000 |   6,086,470.0 ns |     120,252.15 ns |     112,483.94 ns |   6,122,250.0 ns |
|        BubbleCompileTime |  1000 |   1,458,394.4 ns |      28,806.36 ns |      48,128.96 ns |   1,434,550.0 ns |
|         SelectionRunTime |  1000 |   2,781,681.2 ns |      54,152.74 ns |     125,507.21 ns |   2,729,100.0 ns |
|     SelectionCompileTime |  1000 |     740,369.0 ns |      39,595.88 ns |     116,749.34 ns |     761,000.0 ns |
|         InsertionRunTime |  1000 |   1,353,115.8 ns |      26,350.31 ns |      29,288.30 ns |   1,345,800.0 ns |
|     InsertionCompileTime |  1000 |     249,877.5 ns |       4,965.25 ns |       8,825.72 ns |     248,100.0 ns |
|             QuickRunTime |  1000 |     138,215.4 ns |       2,735.12 ns |       2,283.95 ns |     137,000.0 ns |
|         QuickCompileTime |  1000 |      58,136.8 ns |       1,154.60 ns |       1,991.63 ns |      58,050.0 ns |
|             MergeRunTime |  1000 |     103,596.2 ns |       1,703.16 ns |       1,422.21 ns |     103,150.0 ns |
|         MergeCompileTime |  1000 |      59,152.4 ns |       1,175.30 ns |       2,149.11 ns |      59,600.0 ns |
|              HeapRunTime |  1000 |     305,110.6 ns |       5,544.79 ns |      10,814.69 ns |     301,800.0 ns |
|          HeapCompileTime |  1000 |     268,941.2 ns |       5,364.34 ns |      10,957.92 ns |     269,200.0 ns |
|           OddEvenRunTime |  1000 |   3,061,550.0 ns |      59,527.17 ns |      73,104.70 ns |   3,063,050.0 ns |
|       OddEvenCompileTime |  1000 |     873,458.8 ns |      17,457.09 ns |      28,189.94 ns |     871,750.0 ns |
|             GnomeRunTime |  1000 |   3,062,191.4 ns |      59,534.86 ns |      97,817.49 ns |   3,028,500.0 ns |
|         GnomeCompileTime |  1000 |     828,609.4 ns |      18,821.08 ns |      54,303.12 ns |     818,850.0 ns |
|              CombRunTime |  1000 |     172,252.9 ns |       3,392.39 ns |       3,483.73 ns |     170,100.0 ns |
|          CombCompileTime |  1000 |      52,764.3 ns |         873.35 ns |         774.21 ns |      52,450.0 ns |
|             ShellRunTime |  1000 |     113,742.9 ns |         289.18 ns |         256.35 ns |     113,700.0 ns |
|         ShellCompileTime |  1000 |      50,586.7 ns |         808.00 ns |         755.80 ns |      50,500.0 ns |
|          CocktailRunTime |  1000 |   3,267,919.0 ns |      60,542.89 ns |     139,107.36 ns |   3,222,100.0 ns |
|      CocktailCompileTime |  1000 |     951,018.9 ns |      18,039.04 ns |      45,256.41 ns |     935,850.0 ns |
|             CycleRunTime |  1000 |   5,832,101.6 ns |     116,325.08 ns |     262,565.08 ns |   5,752,700.0 ns |
|         CycleCompileTime |  1000 |   1,936,038.0 ns |      35,385.72 ns |      47,238.93 ns |   1,926,450.0 ns |
|           PancakeRunTime |  1000 |   2,984,704.4 ns |      59,139.93 ns |     112,519.87 ns |   2,955,100.0 ns |
|       PancakeCompileTime |  1000 |     500,758.3 ns |       9,766.54 ns |      12,699.26 ns |     496,500.0 ns |
|            StoogeRunTime |  1000 | 386,567,900.0 ns |   1,998,698.64 ns |   1,669,003.88 ns | 386,473,700.0 ns |
|        StoogeCompileTime |  1000 | 254,315,946.2 ns |     880,095.72 ns |     734,919.78 ns | 254,456,000.0 ns |
|              SlowRunTime |  1000 |               NA |                NA |                NA |               NA |
|          SlowCompileTime |  1000 |               NA |                NA |                NA |               NA |
|              BogoRunTime |  1000 |               NA |                NA |                NA |               NA |
|          BogoCompileTime |  1000 |               NA |                NA |                NA |               NA |
|          **SystemArraySort** | **10000** |     **366,420.8 ns** |       **5,743.58 ns** |       **7,468.28 ns** |     **366,000.0 ns** |
|  SystemArraySortDelegate | 10000 |     871,654.2 ns |      13,348.50 ns |      17,356.82 ns |     871,400.0 ns |
| SystemArraySortIComparer | 10000 |   1,002,290.9 ns |      16,521.44 ns |      11,946.08 ns |   1,000,700.0 ns |
|            BubbleRunTime | 10000 | 400,515,638.5 ns |   1,021,218.89 ns |     852,764.02 ns | 400,672,200.0 ns |
|        BubbleCompileTime | 10000 | 162,093,700.0 ns |   1,112,625.89 ns |   1,040,750.95 ns | 161,927,800.0 ns |
|         SelectionRunTime | 10000 | 177,166,450.0 ns |     994,061.96 ns |     881,210.04 ns | 177,106,850.0 ns |
|     SelectionCompileTime | 10000 |  61,754,505.3 ns |   1,208,418.72 ns |   2,084,462.92 ns |  62,000,000.0 ns |
|         InsertionRunTime | 10000 |  83,978,464.3 ns |     449,477.49 ns |     398,450.08 ns |  83,970,050.0 ns |
|     InsertionCompileTime | 10000 |  22,476,033.3 ns |     272,495.64 ns |     212,746.68 ns |  22,481,350.0 ns |
|             QuickRunTime | 10000 |   1,838,646.3 ns |      36,187.46 ns |      65,253.46 ns |   1,814,600.0 ns |
|         QuickCompileTime | 10000 |     770,387.5 ns |      10,940.82 ns |      10,745.35 ns |     770,300.0 ns |
|             MergeRunTime | 10000 |   1,339,641.9 ns |      24,927.02 ns |      38,066.19 ns |   1,328,700.0 ns |
|         MergeCompileTime | 10000 |     740,178.9 ns |      14,323.58 ns |      15,920.62 ns |     739,400.0 ns |
|              HeapRunTime | 10000 |   4,076,530.8 ns |      77,215.28 ns |      64,478.26 ns |   4,049,200.0 ns |
|          HeapCompileTime | 10000 |   3,575,539.2 ns |      70,328.45 ns |     119,423.05 ns |   3,573,050.0 ns |
|           OddEvenRunTime | 10000 | 204,355,878.6 ns |   1,289,414.50 ns |   1,143,032.37 ns | 204,146,850.0 ns |
|       OddEvenCompileTime | 10000 |  91,219,833.3 ns |     982,113.19 ns |     918,669.29 ns |  91,312,100.0 ns |
|             GnomeRunTime | 10000 | 179,862,721.4 ns |     954,578.33 ns |     846,208.83 ns | 179,709,700.0 ns |
|         GnomeCompileTime | 10000 |  64,431,616.7 ns |   1,241,906.84 ns |   1,614,829.69 ns |  64,258,900.0 ns |
|              CombRunTime | 10000 |   2,214,813.0 ns |      44,230.28 ns |      55,937.14 ns |   2,204,000.0 ns |
|          CombCompileTime | 10000 |     714,366.7 ns |      14,045.89 ns |      19,690.37 ns |     712,300.0 ns |
|             ShellRunTime | 10000 |   1,678,685.2 ns |      32,847.36 ns |      46,047.41 ns |   1,667,900.0 ns |
|         ShellCompileTime | 10000 |     702,341.7 ns |      12,544.85 ns |       9,794.20 ns |     705,450.0 ns |
|          CocktailRunTime | 10000 | 202,038,626.7 ns |   1,525,551.19 ns |   1,427,001.53 ns | 202,149,200.0 ns |
|      CocktailCompileTime | 10000 | 100,844,646.7 ns |     944,921.70 ns |     883,880.35 ns | 100,806,500.0 ns |
|             CycleRunTime | 10000 | 568,247,553.8 ns |   1,998,474.14 ns |   1,668,816.42 ns | 568,229,800.0 ns |
|         CycleCompileTime | 10000 | 206,582,214.3 ns |     530,765.08 ns |     470,509.42 ns | 206,634,450.0 ns |
|           PancakeRunTime | 10000 | 195,465,521.4 ns |     618,556.67 ns |     548,334.38 ns | 195,338,450.0 ns |
|       PancakeCompileTime | 10000 |  43,603,858.3 ns |     498,515.33 ns |     389,207.98 ns |  43,614,450.0 ns |
|            StoogeRunTime | 10000 |               NA |                NA |                NA |               NA |
|        StoogeCompileTime | 10000 |               NA |                NA |                NA |               NA |
|              SlowRunTime | 10000 |               NA |                NA |                NA |               NA |
|          SlowCompileTime | 10000 |               NA |                NA |                NA |               NA |
|              BogoRunTime | 10000 |               NA |                NA |                NA |               NA |
|          BogoCompileTime | 10000 |               NA |                NA |                NA |               NA |

Benchmarks with issues:
  SortBenchmarks.SlowRunTime: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [N=1000]
  SortBenchmarks.SlowCompileTime: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [N=1000]
  SortBenchmarks.BogoRunTime: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [N=1000]
  SortBenchmarks.BogoCompileTime: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [N=1000]
  SortBenchmarks.StoogeRunTime: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [N=10000]
  SortBenchmarks.StoogeCompileTime: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [N=10000]
  SortBenchmarks.SlowRunTime: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [N=10000]
  SortBenchmarks.SlowCompileTime: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [N=10000]
  SortBenchmarks.BogoRunTime: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [N=10000]
  SortBenchmarks.BogoCompileTime: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [N=10000]

