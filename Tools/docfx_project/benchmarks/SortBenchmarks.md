# Sorting Algorithms

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1110 (20H2/October2020Update)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.6.21355.2
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  Job-HBXSTX : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                   Method |     N |             Mean |             Error |            StdDev |           Median |
|------------------------- |------ |-----------------:|------------------:|------------------:|-----------------:|
|          **SystemArraySort** |    **10** |         **377.3 ns** |          **20.82 ns** |          **60.39 ns** |         **400.0 ns** |
|  SystemArraySortDelegate |    10 |         695.8 ns |          15.70 ns |          20.41 ns |         700.0 ns |
| SystemArraySortIComparer |    10 |       3,258.2 ns |          66.78 ns |         142.32 ns |       3,200.0 ns |
|            BubbleRunTime |    10 |       1,860.3 ns |          38.87 ns |          96.81 ns |       1,900.0 ns |
|        BubbleCompileTime |    10 |         849.5 ns |          27.31 ns |          79.22 ns |         900.0 ns |
|         SelectionRunTime |    10 |       1,605.2 ns |          34.02 ns |          87.20 ns |       1,600.0 ns |
|     SelectionCompileTime |    10 |         634.3 ns |          21.87 ns |          64.15 ns |         600.0 ns |
|         InsertionRunTime |    10 |       1,428.2 ns |          30.53 ns |          82.54 ns |       1,400.0 ns |
|     InsertionCompileTime |    10 |         378.3 ns |          18.89 ns |          55.40 ns |         350.0 ns |
|             QuickRunTime |    10 |       1,819.5 ns |          37.99 ns |          97.39 ns |       1,800.0 ns |
|         QuickCompileTime |    10 |         773.2 ns |          31.50 ns |          92.39 ns |         750.0 ns |
|             MergeRunTime |    10 |       1,717.9 ns |          36.27 ns |          93.62 ns |       1,700.0 ns |
|         MergeCompileTime |    10 |       1,554.3 ns |          33.41 ns |          94.24 ns |       1,500.0 ns |
|              HeapRunTime |    10 |       2,539.0 ns |          52.43 ns |          94.55 ns |       2,500.0 ns |
|          HeapCompileTime |    10 |       1,445.2 ns |          30.80 ns |          82.73 ns |       1,400.0 ns |
|           OddEvenRunTime |    10 |       1,561.9 ns |          33.89 ns |          91.05 ns |       1,550.0 ns |
|       OddEvenCompileTime |    10 |         658.2 ns |          18.37 ns |          53.58 ns |         700.0 ns |
|             GnomeRunTime |    10 |       1,705.6 ns |          35.86 ns |          88.63 ns |       1,700.0 ns |
|         GnomeCompileTime |    10 |         614.3 ns |          25.10 ns |          73.22 ns |         600.0 ns |
|              CombRunTime |    10 |       1,545.0 ns |          30.94 ns |          80.98 ns |       1,500.0 ns |
|          CombCompileTime |    10 |         553.1 ns |          20.42 ns |          59.56 ns |         550.0 ns |
|             ShellRunTime |    10 |       1,471.7 ns |          32.96 ns |          92.97 ns |       1,450.0 ns |
|         ShellCompileTime |    10 |       1,279.4 ns |          25.42 ns |          41.04 ns |       1,300.0 ns |
|          CocktailRunTime |    10 |       1,819.5 ns |          37.99 ns |          97.39 ns |       1,800.0 ns |
|      CocktailCompileTime |    10 |         871.4 ns |          27.85 ns |          81.23 ns |         900.0 ns |
|             CycleRunTime |    10 |       2,226.9 ns |          46.24 ns |          80.99 ns |       2,250.0 ns |
|         CycleCompileTime |    10 |       1,175.3 ns |          26.81 ns |          77.77 ns |       1,200.0 ns |
|           PancakeRunTime |    10 |       1,847.1 ns |          38.07 ns |          61.47 ns |       1,800.0 ns |
|       PancakeCompileTime |    10 |         844.4 ns |          24.46 ns |          71.75 ns |         800.0 ns |
|            StoogeRunTime |    10 |      10,192.3 ns |         172.58 ns |         144.12 ns |      10,100.0 ns |
|        StoogeCompileTime |    10 |       8,400.0 ns |          96.43 ns |          85.49 ns |       8,350.0 ns |
|              SlowRunTime |    10 |       3,916.7 ns |          73.95 ns |          57.74 ns |       3,900.0 ns |
|          SlowCompileTime |    10 |       2,969.1 ns |          61.26 ns |         174.77 ns |       3,000.0 ns |
|              BogoRunTime |    10 | 353,963,652.7 ns | 105,955,865.94 ns | 300,579,155.92 ns | 263,567,200.0 ns |
|          BogoCompileTime |    10 | 438,762,787.8 ns | 121,381,674.63 ns | 354,076,042.25 ns | 357,948,400.0 ns |
|          **SystemArraySort** |  **1000** |      **27,815.4 ns** |         **296.76 ns** |         **247.81 ns** |      **27,900.0 ns** |
|  SystemArraySortDelegate |  1000 |      64,842.9 ns |       1,021.23 ns |         905.30 ns |      65,200.0 ns |
| SystemArraySortIComparer |  1000 |      76,053.8 ns |       1,437.56 ns |       1,200.43 ns |      75,700.0 ns |
|            BubbleRunTime |  1000 |   4,032,814.6 ns |     126,780.13 ns |     336,202.91 ns |   3,930,250.0 ns |
|        BubbleCompileTime |  1000 |   4,151,049.1 ns |      81,877.64 ns |     177,995.24 ns |   4,109,100.0 ns |
|         SelectionRunTime |  1000 |   2,680,517.9 ns |     140,723.23 ns |     403,761.32 ns |   2,781,900.0 ns |
|     SelectionCompileTime |  1000 |   1,801,469.2 ns |      29,576.70 ns |      24,697.89 ns |   1,796,800.0 ns |
|         InsertionRunTime |  1000 |   1,299,696.6 ns |      77,972.38 ns |     216,061.10 ns |   1,354,000.0 ns |
|     InsertionCompileTime |  1000 |     660,386.8 ns |      13,177.34 ns |      22,730.27 ns |     654,650.0 ns |
|             QuickRunTime |  1000 |     138,857.1 ns |       2,122.31 ns |       1,881.37 ns |     137,600.0 ns |
|         QuickCompileTime |  1000 |     118,668.6 ns |       2,366.25 ns |       3,887.82 ns |     116,700.0 ns |
|             MergeRunTime |  1000 |     106,738.0 ns |       1,980.80 ns |       4,858.93 ns |     105,100.0 ns |
|         MergeCompileTime |  1000 |      87,722.7 ns |       1,752.02 ns |       4,825.57 ns |      86,300.0 ns |
|              HeapRunTime |  1000 |     309,850.0 ns |       6,196.23 ns |       6,629.90 ns |     309,300.0 ns |
|          HeapCompileTime |  1000 |     277,786.7 ns |       5,346.96 ns |       5,001.55 ns |     279,200.0 ns |
|           OddEvenRunTime |  1000 |   2,859,605.0 ns |     190,032.52 ns |     560,315.18 ns |   3,071,400.0 ns |
|       OddEvenCompileTime |  1000 |   2,301,443.5 ns |      45,796.79 ns |      88,234.86 ns |   2,291,550.0 ns |
|             GnomeRunTime |  1000 |   2,801,488.9 ns |     154,781.19 ns |     453,946.45 ns |   2,908,400.0 ns |
|         GnomeCompileTime |  1000 |   2,010,964.9 ns |      43,873.79 ns |     125,174.40 ns |   1,985,250.0 ns |
|              CombRunTime |  1000 |     170,757.1 ns |       2,916.47 ns |       2,585.38 ns |     169,500.0 ns |
|          CombCompileTime |  1000 |     117,500.0 ns |       2,117.15 ns |       2,520.32 ns |     116,200.0 ns |
|             ShellRunTime |  1000 |     115,508.7 ns |       1,787.75 ns |       3,444.39 ns |     113,450.0 ns |
|         ShellCompileTime |  1000 |      89,090.0 ns |       1,660.67 ns |       3,354.63 ns |      88,000.0 ns |
|          CocktailRunTime |  1000 |   2,944,593.0 ns |     199,366.59 ns |     587,836.90 ns |   3,204,950.0 ns |
|      CocktailCompileTime |  1000 |   2,522,879.2 ns |      50,403.31 ns |     129,202.96 ns |   2,508,400.0 ns |
|             CycleRunTime |  1000 |   5,995,136.8 ns |     119,739.49 ns |     286,888.16 ns |   5,963,850.0 ns |
|         CycleCompileTime |  1000 |   6,036,355.8 ns |     120,359.09 ns |     345,332.80 ns |   5,962,900.0 ns |
|           PancakeRunTime |  1000 |   2,953,752.4 ns |      58,248.56 ns |     133,835.76 ns |   2,932,700.0 ns |
|       PancakeCompileTime |  1000 |   1,657,752.0 ns |      46,233.24 ns |     136,319.74 ns |   1,591,850.0 ns |
|            StoogeRunTime |  1000 | 424,535,483.3 ns |   5,727,552.01 ns |   5,357,555.70 ns | 423,015,250.0 ns |
|        StoogeCompileTime |  1000 | 356,142,028.6 ns |   3,250,979.94 ns |   2,881,909.04 ns | 355,955,650.0 ns |
|              SlowRunTime |  1000 |               NA |                NA |                NA |               NA |
|          SlowCompileTime |  1000 |               NA |                NA |                NA |               NA |
|              BogoRunTime |  1000 |               NA |                NA |                NA |               NA |
|          BogoCompileTime |  1000 |               NA |                NA |                NA |               NA |
|          **SystemArraySort** | **10000** |     **374,584.1 ns** |       **7,409.41 ns** |      **13,916.66 ns** |     **366,800.0 ns** |
|  SystemArraySortDelegate | 10000 |     884,176.7 ns |      17,079.72 ns |      46,466.64 ns |     863,950.0 ns |
| SystemArraySortIComparer | 10000 |   1,003,600.5 ns |      20,072.12 ns |      57,590.68 ns |     982,750.0 ns |
|            BubbleRunTime | 10000 | 417,247,246.7 ns |   4,429,846.64 ns |   4,143,681.30 ns | 415,671,700.0 ns |
|        BubbleCompileTime | 10000 | 405,981,846.7 ns |   3,566,120.72 ns |   3,335,751.54 ns | 405,977,400.0 ns |
|         SelectionRunTime | 10000 | 186,935,566.7 ns |   3,481,723.15 ns |   3,256,805.99 ns | 186,769,400.0 ns |
|     SelectionCompileTime | 10000 | 160,624,435.7 ns |   1,547,803.11 ns |   1,372,087.14 ns | 160,170,200.0 ns |
|         InsertionRunTime | 10000 |  87,202,385.7 ns |     768,038.51 ns |     680,846.13 ns |  87,502,250.0 ns |
|     InsertionCompileTime | 10000 |  64,857,392.9 ns |   1,220,403.53 ns |   1,081,855.94 ns |  64,624,750.0 ns |
|             QuickRunTime | 10000 |   1,742,392.0 ns |      93,468.80 ns |     275,594.85 ns |   1,821,700.0 ns |
|         QuickCompileTime | 10000 |   1,535,855.9 ns |      32,432.42 ns |      92,005.38 ns |   1,493,400.0 ns |
|             MergeRunTime | 10000 |   1,339,569.6 ns |      54,696.97 ns |     154,273.78 ns |   1,355,250.0 ns |
|         MergeCompileTime | 10000 |   1,097,729.7 ns |      21,582.51 ns |      50,020.74 ns |   1,073,750.0 ns |
|              HeapRunTime | 10000 |   3,117,995.0 ns |     479,665.58 ns |   1,414,304.77 ns |   3,106,300.0 ns |
|          HeapCompileTime | 10000 |   2,989,999.0 ns |     396,696.56 ns |   1,169,668.74 ns |   3,744,800.0 ns |
|           OddEvenRunTime | 10000 | 211,354,258.3 ns |   1,327,634.58 ns |   1,036,529.75 ns | 211,136,900.0 ns |
|       OddEvenCompileTime | 10000 | 232,384,164.3 ns |   1,769,876.51 ns |   1,568,949.43 ns | 231,946,050.0 ns |
|             GnomeRunTime | 10000 | 190,084,257.1 ns |   2,903,179.74 ns |   2,573,593.22 ns | 190,074,900.0 ns |
|         GnomeCompileTime | 10000 | 182,490,696.7 ns |   2,217,510.55 ns |   2,074,260.75 ns | 181,492,250.0 ns |
|              CombRunTime | 10000 |   2,243,427.6 ns |      93,950.83 ns |     274,058.97 ns |   2,238,900.0 ns |
|          CombCompileTime | 10000 |   1,611,841.6 ns |      30,578.74 ns |      78,385.01 ns |   1,578,000.0 ns |
|             ShellRunTime | 10000 |   1,683,550.0 ns |      31,082.90 ns |      42,546.63 ns |   1,674,800.0 ns |
|         ShellCompileTime | 10000 |   1,322,414.1 ns |      32,340.64 ns |      94,849.49 ns |   1,293,700.0 ns |
|          CocktailRunTime | 10000 | 210,443,203.3 ns |   1,537,785.09 ns |   1,438,445.13 ns | 210,363,950.0 ns |
|      CocktailCompileTime | 10000 | 230,137,692.9 ns |   2,508,873.02 ns |   2,224,050.59 ns | 229,239,250.0 ns |
|             CycleRunTime | 10000 | 596,679,286.7 ns |   8,607,300.56 ns |   8,051,274.28 ns | 593,812,600.0 ns |
|         CycleCompileTime | 10000 | 624,581,557.1 ns |  10,175,210.79 ns |   9,020,059.33 ns | 622,123,800.0 ns |
|           PancakeRunTime | 10000 | 193,368,488.2 ns |   2,815,219.76 ns |   2,891,023.42 ns | 193,262,400.0 ns |
|       PancakeCompileTime | 10000 | 169,229,623.1 ns |   1,433,306.79 ns |   1,196,876.08 ns | 169,665,000.0 ns |
|            StoogeRunTime | 10000 |               NA |                NA |                NA |               NA |
|        StoogeCompileTime | 10000 |               NA |                NA |                NA |               NA |
|              SlowRunTime | 10000 |               NA |                NA |                NA |               NA |
|          SlowCompileTime | 10000 |               NA |                NA |                NA |               NA |
|              BogoRunTime | 10000 |               NA |                NA |                NA |               NA |
|          BogoCompileTime | 10000 |               NA |                NA |                NA |               NA |

Benchmarks with issues:
  SortBenchmarks.SlowRunTime: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [N=1000]
  SortBenchmarks.SlowCompileTime: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [N=1000]
  SortBenchmarks.BogoRunTime: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [N=1000]
  SortBenchmarks.BogoCompileTime: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [N=1000]
  SortBenchmarks.StoogeRunTime: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [N=10000]
  SortBenchmarks.StoogeCompileTime: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [N=10000]
  SortBenchmarks.SlowRunTime: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [N=10000]
  SortBenchmarks.SlowCompileTime: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [N=10000]
  SortBenchmarks.BogoRunTime: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [N=10000]
  SortBenchmarks.BogoCompileTime: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [N=10000]

