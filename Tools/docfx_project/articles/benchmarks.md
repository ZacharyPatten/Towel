# Benchmarks

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

# Sorting Algorithms

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1316 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.200-preview.20614.14
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  Job-CZUCIY : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                   Method |     N |             Mean |             Error |            StdDev |           Median |
|------------------------- |------ |-----------------:|------------------:|------------------:|-----------------:|
|          **SystemArraySort** |    **10** |         **372.7 ns** |          **16.75 ns** |          **49.11 ns** |         **400.0 ns** |
|  SystemArraySortDelegate |    10 |         637.8 ns |          16.70 ns |          48.73 ns |         600.0 ns |
| SystemArraySortIComparer |    10 |       3,097.2 ns |          54.46 ns |          90.98 ns |       3,050.0 ns |
|            BubbleRunTime |    10 |       1,727.0 ns |          38.39 ns |          65.19 ns |       1,700.0 ns |
|        BubbleCompileTime |    10 |         426.1 ns |          15.56 ns |          42.87 ns |         450.0 ns |
|         SelectionRunTime |    10 |       1,422.6 ns |          32.22 ns |          73.37 ns |       1,400.0 ns |
|     SelectionCompileTime |    10 |         296.0 ns |           9.80 ns |          19.79 ns |         300.0 ns |
|         InsertionRunTime |    10 |       1,395.6 ns |          33.97 ns |          94.70 ns |       1,400.0 ns |
|     InsertionCompileTime |    10 |         223.0 ns |          17.94 ns |          52.91 ns |         200.0 ns |
|             QuickRunTime |    10 |       1,676.2 ns |          37.11 ns |          97.10 ns |       1,700.0 ns |
|         QuickCompileTime |    10 |         580.0 ns |          14.88 ns |          40.24 ns |         600.0 ns |
|             MergeRunTime |    10 |       1,738.2 ns |          36.67 ns |          78.15 ns |       1,700.0 ns |
|         MergeCompileTime |    10 |       1,268.2 ns |          34.18 ns |          94.13 ns |       1,300.0 ns |
|              HeapRunTime |    10 |       2,284.6 ns |          44.97 ns |          37.55 ns |       2,300.0 ns |
|          HeapCompileTime |    10 |       1,328.9 ns |          31.05 ns |          90.09 ns |       1,300.0 ns |
|           OddEvenRunTime |    10 |       1,556.2 ns |          32.89 ns |          64.93 ns |       1,500.0 ns |
|       OddEvenCompileTime |    10 |         282.5 ns |          13.17 ns |          38.22 ns |         300.0 ns |
|             GnomeRunTime |    10 |       1,500.0 ns |          33.79 ns |          72.01 ns |       1,500.0 ns |
|         GnomeCompileTime |    10 |         381.3 ns |          13.98 ns |          39.19 ns |         400.0 ns |
|              CombRunTime |    10 |       1,524.1 ns |          32.47 ns |          88.88 ns |       1,500.0 ns |
|          CombCompileTime |    10 |         371.7 ns |          16.18 ns |          47.47 ns |         400.0 ns |
|             ShellRunTime |    10 |       1,384.1 ns |          32.98 ns |          90.83 ns |       1,400.0 ns |
|         ShellCompileTime |    10 |         961.4 ns |          26.65 ns |          73.39 ns |       1,000.0 ns |
|          CocktailRunTime |    10 |       1,738.5 ns |          36.15 ns |          63.31 ns |       1,700.0 ns |
|      CocktailCompileTime |    10 |         474.7 ns |          14.89 ns |          43.67 ns |         500.0 ns |
|             CycleRunTime |    10 |       2,134.3 ns |          46.55 ns |          76.48 ns |       2,100.0 ns |
|         CycleCompileTime |    10 |         589.6 ns |          15.64 ns |          30.87 ns |         600.0 ns |
|           PancakeRunTime |    10 |       1,760.0 ns |          38.73 ns |          78.25 ns |       1,750.0 ns |
|       PancakeCompileTime |    10 |         679.0 ns |          15.55 ns |          40.98 ns |         700.0 ns |
|            StoogeRunTime |    10 |       9,983.3 ns |         202.60 ns |         303.24 ns |       9,950.0 ns |
|        StoogeCompileTime |    10 |       8,530.8 ns |         123.53 ns |         103.16 ns |       8,600.0 ns |
|              SlowRunTime |    10 |       3,866.7 ns |          78.52 ns |          84.02 ns |       3,900.0 ns |
|          SlowCompileTime |    10 |       2,889.0 ns |          77.11 ns |         227.37 ns |       2,900.0 ns |
|              BogoRunTime |    10 | 316,387,652.7 ns | 101,030,797.34 ns | 286,607,556.06 ns | 237,779,100.0 ns |
|          BogoCompileTime |    10 | 423,131,828.0 ns | 120,336,320.69 ns | 341,374,112.50 ns | 368,026,300.0 ns |
|          **SystemArraySort** |  **1000** |      **27,900.0 ns** |         **370.19 ns** |         **328.17 ns** |      **27,950.0 ns** |
|  SystemArraySortDelegate |  1000 |      63,023.1 ns |         313.63 ns |         261.90 ns |      63,000.0 ns |
| SystemArraySortIComparer |  1000 |      73,912.5 ns |         879.89 ns |       1,144.10 ns |      73,400.0 ns |
|            BubbleRunTime |  1000 |   3,943,503.7 ns |      85,277.27 ns |     226,143.23 ns |   3,900,500.0 ns |
|        BubbleCompileTime |  1000 |   1,524,554.2 ns |      30,960.70 ns |      89,328.69 ns |   1,486,700.0 ns |
|         SelectionRunTime |  1000 |   2,815,611.8 ns |      56,123.05 ns |     119,602.78 ns |   2,763,750.0 ns |
|     SelectionCompileTime |  1000 |     778,960.6 ns |      38,001.21 ns |     111,450.97 ns |     793,200.0 ns |
|         InsertionRunTime |  1000 |   1,378,462.2 ns |      27,529.65 ns |      52,378.02 ns |   1,361,900.0 ns |
|     InsertionCompileTime |  1000 |     238,833.3 ns |       3,767.81 ns |       2,941.65 ns |     239,200.0 ns |
|             QuickRunTime |  1000 |     143,319.2 ns |       2,852.88 ns |       3,905.05 ns |     143,450.0 ns |
|         QuickCompileTime |  1000 |      56,728.9 ns |       1,133.90 ns |       1,260.33 ns |      56,050.0 ns |
|             MergeRunTime |  1000 |     105,940.3 ns |       2,100.35 ns |       4,783.57 ns |     104,250.0 ns |
|         MergeCompileTime |  1000 |      60,606.7 ns |       1,181.25 ns |       1,768.05 ns |      60,400.0 ns |
|              HeapRunTime |  1000 |     313,895.5 ns |       5,712.55 ns |      10,729.55 ns |     312,400.0 ns |
|          HeapCompileTime |  1000 |     266,892.9 ns |       3,527.09 ns |       3,126.68 ns |     265,400.0 ns |
|           OddEvenRunTime |  1000 |   2,783,015.0 ns |     173,883.58 ns |     512,699.67 ns |   2,947,750.0 ns |
|       OddEvenCompileTime |  1000 |     898,589.5 ns |      24,478.35 ns |      70,232.98 ns |     864,700.0 ns |
|             GnomeRunTime |  1000 |   2,640,159.0 ns |     154,007.09 ns |     454,093.37 ns |   2,761,300.0 ns |
|         GnomeCompileTime |  1000 |     828,492.3 ns |      16,413.64 ns |      42,368.84 ns |     827,850.0 ns |
|              CombRunTime |  1000 |     174,061.1 ns |       3,403.56 ns |       3,641.77 ns |     173,450.0 ns |
|          CombCompileTime |  1000 |      52,108.3 ns |         611.74 ns |         477.60 ns |      52,050.0 ns |
|             ShellRunTime |  1000 |     116,173.1 ns |       1,158.12 ns |         967.09 ns |     116,050.0 ns |
|         ShellCompileTime |  1000 |      51,035.7 ns |         543.00 ns |         481.36 ns |      51,000.0 ns |
|          CocktailRunTime |  1000 |   3,336,822.4 ns |      65,528.73 ns |     130,867.96 ns |   3,322,000.0 ns |
|      CocktailCompileTime |  1000 |     984,533.0 ns |      24,293.72 ns |      69,311.34 ns |     955,300.0 ns |
|             CycleRunTime |  1000 |   5,977,548.8 ns |     127,376.27 ns |     333,321.51 ns |   5,900,600.0 ns |
|         CycleCompileTime |  1000 |   2,010,707.0 ns |      39,819.75 ns |      97,678.53 ns |   1,972,900.0 ns |
|           PancakeRunTime |  1000 |   3,138,614.0 ns |      62,301.24 ns |     115,479.34 ns |   3,104,400.0 ns |
|       PancakeCompileTime |  1000 |     537,182.3 ns |      11,991.76 ns |      34,598.98 ns |     529,000.0 ns |
|            StoogeRunTime |  1000 | 424,315,740.0 ns |   8,145,514.41 ns |   7,619,319.23 ns | 422,050,800.0 ns |
|        StoogeCompileTime |  1000 | 264,726,133.3 ns |   1,755,304.78 ns |   1,641,913.18 ns | 264,397,600.0 ns |
|              SlowRunTime |  1000 |               NA |                NA |                NA |               NA |
|          SlowCompileTime |  1000 |               NA |                NA |                NA |               NA |
|              BogoRunTime |  1000 |               NA |                NA |                NA |               NA |
|          BogoCompileTime |  1000 |               NA |                NA |                NA |               NA |
|          **SystemArraySort** | **10000** |     **379,043.8 ns** |       **7,560.60 ns** |      **20,950.39 ns** |     **370,400.0 ns** |
|  SystemArraySortDelegate | 10000 |     853,012.9 ns |      14,586.55 ns |      22,275.20 ns |     842,500.0 ns |
| SystemArraySortIComparer | 10000 |     963,951.7 ns |      17,789.12 ns |      26,075.07 ns |     953,900.0 ns |
|            BubbleRunTime | 10000 | 413,957,150.0 ns |   2,297,777.24 ns |   2,036,919.67 ns | 414,324,700.0 ns |
|        BubbleCompileTime | 10000 | 168,948,060.0 ns |   1,744,240.03 ns |   1,631,563.21 ns | 168,969,800.0 ns |
|         SelectionRunTime | 10000 | 190,097,364.7 ns |   3,500,535.93 ns |   5,652,713.03 ns | 188,933,150.0 ns |
|     SelectionCompileTime | 10000 |  63,073,153.3 ns |   1,083,459.84 ns |   1,013,469.01 ns |  63,069,900.0 ns |
|         InsertionRunTime | 10000 |  89,060,353.8 ns |     598,452.82 ns |     499,735.21 ns |  88,913,100.0 ns |
|     InsertionCompileTime | 10000 |  24,019,059.1 ns |     476,741.55 ns |     585,481.43 ns |  24,082,300.0 ns |
|             QuickRunTime | 10000 |   1,947,870.6 ns |      38,377.70 ns |      78,395.48 ns |   1,938,600.0 ns |
|         QuickCompileTime | 10000 |     761,071.2 ns |      15,213.50 ns |      39,811.09 ns |     748,350.0 ns |
|             MergeRunTime | 10000 |   1,413,580.2 ns |      32,547.80 ns |      93,907.83 ns |   1,394,050.0 ns |
|         MergeCompileTime | 10000 |     766,438.9 ns |      15,295.04 ns |      37,805.56 ns |     759,000.0 ns |
|              HeapRunTime | 10000 |   2,989,990.0 ns |     457,445.63 ns |   1,348,788.76 ns |   2,030,550.0 ns |
|          HeapCompileTime | 10000 |   2,668,805.0 ns |     459,803.85 ns |   1,355,742.01 ns |   3,571,750.0 ns |
|           OddEvenRunTime | 10000 | 214,413,664.3 ns |   1,920,320.16 ns |   1,702,313.80 ns | 214,029,000.0 ns |
|       OddEvenCompileTime | 10000 |  95,895,914.3 ns |   1,206,368.97 ns |   1,069,414.67 ns |  95,688,900.0 ns |
|             GnomeRunTime | 10000 | 193,239,600.0 ns |   3,604,627.23 ns |   3,371,770.55 ns | 191,566,400.0 ns |
|         GnomeCompileTime | 10000 |  66,973,468.0 ns |   1,110,014.54 ns |   1,481,837.52 ns |  67,150,600.0 ns |
|              CombRunTime | 10000 |   2,212,203.0 ns |     104,823.39 ns |     309,074.14 ns |   2,237,950.0 ns |
|          CombCompileTime | 10000 |     728,271.4 ns |      14,554.58 ns |      29,067.06 ns |     714,500.0 ns |
|             ShellRunTime | 10000 |   1,791,268.8 ns |      46,710.08 ns |     134,769.25 ns |   1,752,350.0 ns |
|         ShellCompileTime | 10000 |     743,280.4 ns |      18,110.48 ns |      52,541.79 ns |     721,900.0 ns |
|          CocktailRunTime | 10000 | 214,813,600.0 ns |   2,122,224.65 ns |   1,985,130.25 ns | 214,471,200.0 ns |
|      CocktailCompileTime | 10000 | 107,730,884.2 ns |   2,147,991.76 ns |   2,387,487.15 ns | 107,340,100.0 ns |
|             CycleRunTime | 10000 | 606,112,046.7 ns |   3,860,322.14 ns |   3,610,947.72 ns | 605,707,600.0 ns |
|         CycleCompileTime | 10000 | 220,104,953.8 ns |   1,926,905.13 ns |   1,609,053.05 ns | 219,799,800.0 ns |
|           PancakeRunTime | 10000 | 195,626,764.3 ns |   1,816,460.54 ns |   1,610,244.96 ns | 195,926,200.0 ns |
|       PancakeCompileTime | 10000 |  47,558,046.7 ns |     926,820.07 ns |     866,948.07 ns |  47,423,700.0 ns |
|            StoogeRunTime | 10000 |               NA |                NA |                NA |               NA |
|        StoogeCompileTime | 10000 |               NA |                NA |                NA |               NA |
|              SlowRunTime | 10000 |               NA |                NA |                NA |               NA |
|          SlowCompileTime | 10000 |               NA |                NA |                NA |               NA |
|              BogoRunTime | 10000 |               NA |                NA |                NA |               NA |
|          BogoCompileTime | 10000 |               NA |                NA |                NA |               NA |

# Data Structures

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1316 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.200-preview.20614.14
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|                         Method | RandomTestData |             Mean |          Error |         StdDev |           Median |
|------------------------------- |--------------- |-----------------:|---------------:|---------------:|-----------------:|
|                  **ListArray_Add** |  **Person[10000]** |    **160,135.20 ns** |   **2,945.671 ns** |   **2,459.769 ns** |    **159,371.78 ns** |
|      ListArray_AddWithCapacity |  Person[10000] |     77,882.78 ns |   1,330.127 ns |   2,364.299 ns |     77,368.15 ns |
|                            Add |  Person[10000] |    156,369.63 ns |     974.597 ns |     813.832 ns |    156,228.59 ns |
|             QueueArray_Enqueue |  Person[10000] |    185,399.31 ns |   3,539.639 ns |   3,934.299 ns |    184,201.90 ns |
| QueueArray_EnqueueWithCapacity |  Person[10000] |    174,676.06 ns |   1,076.067 ns |     953.905 ns |    174,614.62 ns |
|            QueueLinked_Enqueue |  Person[10000] |    127,832.26 ns |   1,952.000 ns |   1,825.902 ns |    127,330.44 ns |
|                StackArray_Push |  Person[10000] |    139,552.66 ns |   2,021.946 ns |   1,792.402 ns |    138,930.66 ns |
|    StackArray_PushWithCapacity |  Person[10000] |     55,207.79 ns |     245.971 ns |     192.038 ns |     55,180.13 ns |
|               StackLinked_Push |  Person[10000] |    123,644.40 ns |   1,710.373 ns |   1,516.201 ns |    123,298.96 ns |
|       AvlTreeLinked_AddRunTime |  Person[10000] | 13,895,854.74 ns | 274,233.723 ns | 401,968.236 ns | 13,863,178.12 ns |
|   AvlTreeLinked_AddCompileTime |  Person[10000] | 13,233,691.52 ns | 209,774.371 ns | 185,959.516 ns | 13,191,041.41 ns |
|        RedBlackTree_AddRunTime |  Person[10000] | 13,667,144.97 ns | 267,797.868 ns | 447,430.078 ns | 13,630,856.25 ns |
|    RedBlackTree_AddCompileTime |  Person[10000] | 13,545,627.28 ns | 107,478.776 ns |  89,749.646 ns | 13,551,917.19 ns |
|       SetHashLinked_AddRunTime |  Person[10000] |  1,230,068.70 ns |  15,375.474 ns |  12,839.217 ns |  1,228,112.89 ns |
|   SetHashLinked_AddCompileTime |  Person[10000] |  1,170,743.64 ns |  22,927.940 ns |  43,622.789 ns |  1,147,034.77 ns |
|                  **ListArray_Add** |   **Person[1000]** |      **9,327.72 ns** |     **154.449 ns** |     **158.608 ns** |      **9,263.06 ns** |
|      ListArray_AddWithCapacity |   Person[1000] |      8,593.45 ns |     170.800 ns |     341.106 ns |      8,533.87 ns |
|                            Add |   Person[1000] |     13,636.52 ns |      92.753 ns |      82.223 ns |     13,643.25 ns |
|             QueueArray_Enqueue |   Person[1000] |     10,352.20 ns |      85.253 ns |      75.574 ns |     10,316.56 ns |
| QueueArray_EnqueueWithCapacity |   Person[1000] |     10,692.27 ns |     188.289 ns |     184.925 ns |     10,604.10 ns |
|            QueueLinked_Enqueue |   Person[1000] |     10,793.02 ns |      41.460 ns |      36.753 ns |     10,789.71 ns |
|                StackArray_Push |   Person[1000] |      6,900.52 ns |      36.497 ns |      30.476 ns |      6,904.78 ns |
|    StackArray_PushWithCapacity |   Person[1000] |      5,782.84 ns |     113.422 ns |     139.292 ns |      5,721.75 ns |
|               StackLinked_Push |   Person[1000] |     10,620.09 ns |     189.816 ns |     210.980 ns |     10,562.71 ns |
|       AvlTreeLinked_AddRunTime |   Person[1000] |    932,789.77 ns |  11,475.819 ns |   9,582.829 ns |    930,057.71 ns |
|   AvlTreeLinked_AddCompileTime |   Person[1000] |    912,223.78 ns |  11,845.657 ns |  11,080.435 ns |    907,134.18 ns |
|        RedBlackTree_AddRunTime |   Person[1000] |    895,322.57 ns |   5,199.184 ns |   4,059.181 ns |    894,594.78 ns |
|    RedBlackTree_AddCompileTime |   Person[1000] |    938,100.40 ns |  15,290.224 ns |  15,017.044 ns |    932,195.36 ns |
|       SetHashLinked_AddRunTime |   Person[1000] |     68,142.25 ns |   1,338.465 ns |   2,793.875 ns |     67,187.44 ns |
|   SetHashLinked_AddCompileTime |   Person[1000] |     54,290.18 ns |     360.535 ns |     319.605 ns |     54,224.42 ns |
|                  **ListArray_Add** |    **Person[100]** |      **1,118.58 ns** |      **22.204 ns** |      **45.357 ns** |      **1,105.84 ns** |
|      ListArray_AddWithCapacity |    Person[100] |        854.38 ns |      17.092 ns |      33.737 ns |        839.74 ns |
|                            Add |    Person[100] |      1,374.35 ns |      27.066 ns |      54.675 ns |      1,366.38 ns |
|             QueueArray_Enqueue |    Person[100] |      1,228.81 ns |      24.253 ns |      28.872 ns |      1,226.79 ns |
| QueueArray_EnqueueWithCapacity |    Person[100] |      1,020.68 ns |      14.973 ns |      12.503 ns |      1,016.73 ns |
|            QueueLinked_Enqueue |    Person[100] |      1,094.78 ns |       7.516 ns |       6.276 ns |      1,094.63 ns |
|                StackArray_Push |    Person[100] |        912.04 ns |      12.326 ns |      11.530 ns |        908.24 ns |
|    StackArray_PushWithCapacity |    Person[100] |        593.35 ns |       4.093 ns |       3.418 ns |        593.62 ns |
|               StackLinked_Push |    Person[100] |      1,094.11 ns |      15.556 ns |      14.552 ns |      1,089.87 ns |
|       AvlTreeLinked_AddRunTime |    Person[100] |     55,268.24 ns |     965.045 ns |     902.704 ns |     54,790.81 ns |
|   AvlTreeLinked_AddCompileTime |    Person[100] |     55,817.93 ns |     419.494 ns |     327.513 ns |     55,744.05 ns |
|        RedBlackTree_AddRunTime |    Person[100] |     57,468.42 ns |     294.027 ns |     245.526 ns |     57,388.60 ns |
|    RedBlackTree_AddCompileTime |    Person[100] |     60,850.01 ns |   1,207.225 ns |   2,410.959 ns |     59,690.39 ns |
|       SetHashLinked_AddRunTime |    Person[100] |      6,459.98 ns |      40.867 ns |      34.125 ns |      6,461.56 ns |
|   SetHashLinked_AddCompileTime |    Person[100] |      5,103.46 ns |      58.685 ns |      54.894 ns |      5,105.71 ns |
|                  **ListArray_Add** |     **Person[10]** |        **174.14 ns** |       **1.186 ns** |       **0.926 ns** |        **174.34 ns** |
|      ListArray_AddWithCapacity |     Person[10] |         99.06 ns |       0.970 ns |       0.860 ns |         98.84 ns |
|                            Add |     Person[10] |        152.74 ns |       3.010 ns |       2.815 ns |        151.50 ns |
|             QueueArray_Enqueue |     Person[10] |        142.03 ns |       1.032 ns |       0.862 ns |        142.09 ns |
| QueueArray_EnqueueWithCapacity |     Person[10] |        126.50 ns |       2.560 ns |       5.566 ns |        125.07 ns |
|            QueueLinked_Enqueue |     Person[10] |        120.25 ns |       1.642 ns |       1.371 ns |        119.89 ns |
|                StackArray_Push |     Person[10] |        158.77 ns |       2.531 ns |       2.599 ns |        157.60 ns |
|    StackArray_PushWithCapacity |     Person[10] |         68.74 ns |       1.339 ns |       1.488 ns |         67.93 ns |
|               StackLinked_Push |     Person[10] |        118.45 ns |       2.274 ns |       2.127 ns |        117.89 ns |
|       AvlTreeLinked_AddRunTime |     Person[10] |      2,447.64 ns |      19.027 ns |      15.889 ns |      2,443.75 ns |
|   AvlTreeLinked_AddCompileTime |     Person[10] |      2,431.35 ns |      32.381 ns |      33.252 ns |      2,418.13 ns |
|        RedBlackTree_AddRunTime |     Person[10] |      2,759.53 ns |      18.998 ns |      17.771 ns |      2,757.82 ns |
|    RedBlackTree_AddCompileTime |     Person[10] |      2,857.02 ns |      21.061 ns |      18.670 ns |      2,853.51 ns |
|       SetHashLinked_AddRunTime |     Person[10] |        665.11 ns |       4.325 ns |       3.611 ns |        666.42 ns |
|   SetHashLinked_AddCompileTime |     Person[10] |        540.89 ns |       3.034 ns |       2.690 ns |        540.61 ns |

# Random

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1316 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.200-preview.20614.14
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  Job-CZUCIY : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                       Method |      N |         Mean |      Error |     StdDev |
|----------------------------- |------- |-------------:|-----------:|-----------:|
| **Next_IEnumerable_TotalWeight** |     **10** |     **2.165 μs** |  **0.0622 μs** |  **0.1702 μs** |
|             Next_IEnumerable |     10 |     2.263 μs |  0.0489 μs |  0.1171 μs |
| **Next_IEnumerable_TotalWeight** |    **100** |     **3.206 μs** |  **0.0671 μs** |  **0.1045 μs** |
|             Next_IEnumerable |    100 |     4.840 μs |  0.1007 μs |  0.1963 μs |
| **Next_IEnumerable_TotalWeight** |   **1000** |    **14.475 μs** |  **0.1458 μs** |  **0.1138 μs** |
|             Next_IEnumerable |   1000 |    29.346 μs |  0.2376 μs |  0.1984 μs |
| **Next_IEnumerable_TotalWeight** |  **10000** |   **126.162 μs** |  **2.0739 μs** |  **1.7318 μs** |
|             Next_IEnumerable |  10000 |   270.725 μs |  3.2513 μs |  3.1932 μs |
| **Next_IEnumerable_TotalWeight** | **100000** | **1,237.129 μs** | **23.1271 μs** | **20.5016 μs** |
|             Next_IEnumerable | 100000 | 2,740.381 μs | 54.4264 μs | 92.4202 μs |

# [Numeric] To English Words

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1316 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.200-preview.20614.14
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|                 Method |  Range |     Mean |    Error |   StdDev |
|----------------------- |------- |---------:|---------:|---------:|
| **Decimal_ToEnglishWords** |     **10** | **70.81 ms** | **0.186 ms** | **0.146 ms** |
| **Decimal_ToEnglishWords** |    **100** | **67.56 ms** | **0.331 ms** | **0.309 ms** |
| **Decimal_ToEnglishWords** |   **1000** | **64.40 ms** | **0.320 ms** | **0.299 ms** |
| **Decimal_ToEnglishWords** |  **10000** | **65.01 ms** | **0.491 ms** | **0.435 ms** |
| **Decimal_ToEnglishWords** | **100000** | **64.70 ms** | **0.461 ms** | **0.385 ms** |

# Permute

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1316 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.200-preview.20614.14
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  Job-CZUCIY : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|          Method |  N |               Mean |            Error |           StdDev |             Median |
|---------------- |--- |-------------------:|-----------------:|-----------------:|-------------------:|
|       **Recursive** |  **1** |           **200.0 ns** |          **0.00 ns** |          **0.00 ns** |           **200.0 ns** |
|       Iterative |  1 |           900.0 ns |         26.97 ns |         74.28 ns |           900.0 ns |
| RecursiveStruct |  1 |           166.7 ns |         16.16 ns |         47.38 ns |           200.0 ns |
| IterativeStruct |  1 |           926.4 ns |         26.98 ns |         73.86 ns |           900.0 ns |
|       **Recursive** |  **2** |           **360.6 ns** |         **18.75 ns** |         **54.99 ns** |           **400.0 ns** |
|       Iterative |  2 |         1,006.7 ns |         39.16 ns |        108.50 ns |         1,000.0 ns |
| RecursiveStruct |  2 |           265.0 ns |         18.28 ns |         53.89 ns |           300.0 ns |
| IterativeStruct |  2 |           921.4 ns |         35.18 ns |         94.51 ns |           900.0 ns |
|       **Recursive** |  **3** |           **494.6 ns** |         **13.50 ns** |         **22.92 ns** |           **500.0 ns** |
|       Iterative |  3 |         1,085.7 ns |         26.88 ns |         72.22 ns |         1,050.0 ns |
| RecursiveStruct |  3 |           473.7 ns |         16.24 ns |         46.61 ns |           500.0 ns |
| IterativeStruct |  3 |           944.4 ns |         24.71 ns |         68.88 ns |           900.0 ns |
|       **Recursive** |  **4** |         **1,211.4 ns** |         **27.15 ns** |         **59.02 ns** |         **1,250.0 ns** |
|       Iterative |  4 |         1,283.3 ns |         31.47 ns |         87.73 ns |         1,300.0 ns |
| RecursiveStruct |  4 |           988.9 ns |         22.85 ns |         32.03 ns |         1,000.0 ns |
| IterativeStruct |  4 |         1,195.5 ns |         29.60 ns |         81.52 ns |         1,200.0 ns |
|       **Recursive** |  **5** |         **4,193.3 ns** |         **85.40 ns** |         **79.88 ns** |         **4,200.0 ns** |
|       Iterative |  5 |         2,413.6 ns |         80.00 ns |        220.34 ns |         2,300.0 ns |
| RecursiveStruct |  5 |         4,062.1 ns |         84.37 ns |        123.68 ns |         4,000.0 ns |
| IterativeStruct |  5 |         2,100.0 ns |         44.66 ns |         65.47 ns |         2,100.0 ns |
|       **Recursive** |  **6** |        **23,038.5 ns** |        **344.10 ns** |        **287.34 ns** |        **22,900.0 ns** |
|       Iterative |  6 |         8,484.6 ns |        161.01 ns |        134.45 ns |         8,500.0 ns |
| RecursiveStruct |  6 |        22,235.7 ns |        332.90 ns |        295.11 ns |        22,400.0 ns |
| IterativeStruct |  6 |         8,127.6 ns |        161.95 ns |        237.39 ns |         8,100.0 ns |
|       **Recursive** |  **7** |       **166,438.8 ns** |      **3,321.80 ns** |      **9,689.85 ns** |       **161,200.0 ns** |
|       Iterative |  7 |        54,368.5 ns |      1,080.69 ns |      2,691.29 ns |        53,800.0 ns |
| RecursiveStruct |  7 |       163,676.5 ns |      3,425.53 ns |      9,992.44 ns |       160,650.0 ns |
| IterativeStruct |  7 |        54,633.7 ns |      2,050.25 ns |      5,980.69 ns |        51,500.0 ns |
|       **Recursive** |  **8** |     **1,315,012.1 ns** |     **68,058.82 ns** |    **199,604.75 ns** |     **1,318,800.0 ns** |
|       Iterative |  8 |       448,719.2 ns |     13,035.51 ns |     38,230.89 ns |       433,000.0 ns |
| RecursiveStruct |  8 |     1,269,447.5 ns |     24,110.65 ns |     42,856.66 ns |     1,263,850.0 ns |
| IterativeStruct |  8 |       404,932.7 ns |      8,057.26 ns |     17,170.68 ns |       403,100.0 ns |
|       **Recursive** |  **9** |     **4,657,353.2 ns** |     **89,964.84 ns** |    **233,830.57 ns** |     **4,628,600.0 ns** |
|       Iterative |  9 |     3,858,710.8 ns |     76,746.76 ns |    130,321.82 ns |     3,852,900.0 ns |
| RecursiveStruct |  9 |     4,625,247.6 ns |    243,503.09 ns |    654,155.23 ns |     4,369,600.0 ns |
| IterativeStruct |  9 |     2,816,584.0 ns |    341,327.52 ns |  1,006,411.89 ns |     3,344,600.0 ns |
|       **Recursive** | **10** |    **43,930,214.5 ns** |    **528,907.28 ns** |  **1,127,144.55 ns** |    **43,666,300.0 ns** |
|       Iterative | 10 |    18,629,136.4 ns |    369,712.39 ns |    454,040.01 ns |    18,696,750.0 ns |
| RecursiveStruct | 10 |    42,321,821.4 ns |    686,163.73 ns |    608,266.28 ns |    42,374,950.0 ns |
| IterativeStruct | 10 |    14,701,543.8 ns |    290,703.92 ns |    285,510.12 ns |    14,724,050.0 ns |
|       **Recursive** | **11** |   **519,684,653.8 ns** |  **7,644,918.49 ns** |  **6,383,853.18 ns** |   **516,783,500.0 ns** |
|       Iterative | 11 |   196,844,066.7 ns |  1,199,037.18 ns |  1,121,580.12 ns |   197,041,100.0 ns |
| RecursiveStruct | 11 |   494,052,621.4 ns |  7,784,238.20 ns |  6,900,524.41 ns |   495,862,200.0 ns |
| IterativeStruct | 11 |   157,538,764.3 ns |  1,529,311.22 ns |  1,355,694.57 ns |   157,523,150.0 ns |
|       **Recursive** | **12** | **6,237,950,673.3 ns** | **54,379,587.49 ns** | **50,866,699.88 ns** | **6,215,711,800.0 ns** |
|       Iterative | 12 | 2,435,382,112.5 ns | 46,584,835.85 ns | 60,573,445.18 ns | 2,441,110,200.0 ns |
| RecursiveStruct | 12 | 6,235,220,846.7 ns | 89,141,245.18 ns | 83,382,776.05 ns | 6,205,506,200.0 ns |
| IterativeStruct | 12 | 1,938,123,246.2 ns |  9,802,664.68 ns |  8,185,668.97 ns | 1,938,254,200.0 ns |

# Map vs Dictionary (Add)

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1316 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.200-preview.20614.14
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|       Method |     N |         Mean |       Error |      StdDev |
|------------- |------ |-------------:|------------:|------------:|
| **MapDelegates** |    **10** |     **548.3 ns** |     **6.29 ns** |     **5.57 ns** |
|   MapStructs |    10 |     437.1 ns |     3.55 ns |     3.14 ns |
|   Dictionary |    10 |     218.9 ns |     2.57 ns |     2.28 ns |
| **MapDelegates** |   **100** |   **4,348.7 ns** |    **21.35 ns** |    **17.83 ns** |
|   MapStructs |   100 |   3,677.4 ns |    15.61 ns |    13.84 ns |
|   Dictionary |   100 |   1,887.6 ns |    37.46 ns |    75.67 ns |
| **MapDelegates** |  **1000** |  **34,801.0 ns** |   **311.77 ns** |   **276.37 ns** |
|   MapStructs |  1000 |  29,809.5 ns |   571.66 ns |   680.52 ns |
|   Dictionary |  1000 |  18,732.5 ns |   302.52 ns |   282.98 ns |
| **MapDelegates** | **10000** | **523,970.4 ns** | **4,377.33 ns** | **3,655.26 ns** |
|   MapStructs | 10000 | 496,632.1 ns | 5,149.39 ns | 4,299.98 ns |
|   Dictionary | 10000 | 266,149.4 ns | 3,960.82 ns | 3,704.95 ns |

# Map vs Dictionary (Look Up)

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1316 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.200-preview.20614.14
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  Job-CZUCIY : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|       Method |     N |         Mean |       Error |       StdDev |       Median |
|------------- |------ |-------------:|------------:|-------------:|-------------:|
| **MapDelegates** |    **10** |     **571.3 ns** |    **22.22 ns** |     **60.82 ns** |     **600.0 ns** |
|   MapStructs |    10 |     277.8 ns |    15.88 ns |     41.83 ns |     300.0 ns |
|   Dictionary |    10 |     200.0 ns |     0.00 ns |      0.00 ns |     200.0 ns |
| **MapDelegates** |   **100** |   **2,157.9 ns** |    **45.64 ns** |     **50.73 ns** |   **2,200.0 ns** |
|   MapStructs |   100 |   1,170.3 ns |    27.29 ns |     46.34 ns |   1,200.0 ns |
|   Dictionary |   100 |   1,002.7 ns |    23.76 ns |     59.62 ns |   1,000.0 ns |
| **MapDelegates** |  **1000** |  **17,292.3 ns** |   **303.20 ns** |    **253.18 ns** |  **17,200.0 ns** |
|   MapStructs |  1000 |   9,594.4 ns |   188.21 ns |    201.38 ns |   9,550.0 ns |
|   Dictionary |  1000 |   8,781.6 ns |   177.21 ns |    305.67 ns |   8,800.0 ns |
| **MapDelegates** | **10000** | **154,094.8 ns** | **9,796.56 ns** | **28,421.60 ns** | **164,900.0 ns** |
|   MapStructs | 10000 |  92,330.9 ns | 5,878.63 ns | 16,772.06 ns |  95,100.0 ns |
|   Dictionary | 10000 |  89,604.2 ns | 6,460.35 ns | 18,535.96 ns |  83,700.0 ns |

# Span vs Array Sorting

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1316 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.200-preview.20614.14
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  Job-CZUCIY : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                 Method |     N |             Mean |            Error |           StdDev |           Median |
|----------------------- |------ |-----------------:|-----------------:|-----------------:|-----------------:|
|     **ArrayBubbleRunTime** |    **10** |       **2,012.4 ns** |         **44.80 ns** |        **124.15 ns** |       **2,000.0 ns** |
| ArrayBubbleCompileTime |    10 |         470.8 ns |         19.00 ns |         52.66 ns |         500.0 ns |
|      SpanBubbleRunTime |    10 |       1,918.2 ns |         37.33 ns |         79.56 ns |       1,900.0 ns |
|  SpanBubbleCompileTime |    10 |         376.3 ns |         17.78 ns |         51.59 ns |         400.0 ns |
|     **ArrayBubbleRunTime** |  **1000** |   **5,109,877.1 ns** |    **121,385.99 ns** |    **324,003.79 ns** |   **4,968,600.0 ns** |
| ArrayBubbleCompileTime |  1000 |   1,833,424.1 ns |     36,401.19 ns |     51,029.38 ns |   1,822,950.0 ns |
|      SpanBubbleRunTime |  1000 |   4,094,217.2 ns |     76,126.95 ns |    176,435.79 ns |   4,067,550.0 ns |
|  SpanBubbleCompileTime |  1000 |   1,470,395.1 ns |     31,647.60 ns |     88,743.25 ns |   1,450,050.0 ns |
|     **ArrayBubbleRunTime** | **10000** | **522,202,083.3 ns** | **10,320,654.71 ns** | **13,419,766.34 ns** | **520,538,450.0 ns** |
| ArrayBubbleCompileTime | 10000 | 197,253,926.9 ns |  2,335,129.82 ns |  1,949,939.16 ns | 197,464,750.0 ns |
|      SpanBubbleRunTime | 10000 | 440,615,044.0 ns |  8,580,511.53 ns | 11,454,736.44 ns | 437,660,900.0 ns |
|  SpanBubbleCompileTime | 10000 | 177,599,892.3 ns |  2,012,230.11 ns |  1,680,303.27 ns | 177,159,400.0 ns |

# Random With Exclusions

The source code for all becnhmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/master/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1316 (1909/November2018Update/19H2)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.200-preview.20614.14
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  Job-JNMXTF : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                Method | MinValue | MaxValue |                Count |              Exclued |          Mean |       Error |        StdDev |        Median |
|---------------------- |--------- |--------- |--------------------- |--------------------- |--------------:|------------:|--------------:|--------------:|
|                 **Towel** |        **0** |       **10** | **1: sqrt(sqrt(range))** | **1: sqrt(sqrt(range))** |      **1.551 μs** |   **0.0685 μs** |     **0.1864 μs** |      **1.500 μs** |
|       HashSetAndArray |        0 |       10 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |      1.435 μs |   0.0351 μs |     0.0955 μs |      1.400 μs |
| SetHashLinkedAndArray |        0 |       10 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |      1.644 μs |   0.0478 μs |     0.1309 μs |      1.600 μs |
|  RelativelySimpleCode |        0 |       10 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |      4.063 μs |   0.1095 μs |     0.2904 μs |      4.000 μs |
|                 **Towel** |        **0** |       **10** | **1: sqrt(sqrt(range))** |    **2: .5*sqrt(range)** |      **1.424 μs** |   **0.0420 μs** |     **0.1163 μs** |      **1.450 μs** |
|       HashSetAndArray |        0 |       10 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |      1.428 μs |   0.0476 μs |     0.1287 μs |      1.400 μs |
| SetHashLinkedAndArray |        0 |       10 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |      1.549 μs |   0.0464 μs |     0.1278 μs |      1.500 μs |
|  RelativelySimpleCode |        0 |       10 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |      4.090 μs |   0.0963 μs |     0.2569 μs |      4.000 μs |
|                 **Towel** |        **0** |       **10** | **1: sqrt(sqrt(range))** |       **3: sqrt(range)** |      **1.537 μs** |   **0.0351 μs** |     **0.0949 μs** |      **1.550 μs** |
|       HashSetAndArray |        0 |       10 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |      1.496 μs |   0.0501 μs |     0.1389 μs |      1.500 μs |
| SetHashLinkedAndArray |        0 |       10 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |      1.755 μs |   0.0478 μs |     0.1310 μs |      1.700 μs |
|  RelativelySimpleCode |        0 |       10 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |      4.368 μs |   0.2811 μs |     0.7976 μs |      4.000 μs |
|                 **Towel** |        **0** |       **10** | **1: sqrt(sqrt(range))** |     **4: 2*sqrt(range)** |      **2.494 μs** |   **0.0565 μs** |     **0.1537 μs** |      **2.500 μs** |
|       HashSetAndArray |        0 |       10 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |      1.502 μs |   0.0374 μs |     0.1023 μs |      1.500 μs |
| SetHashLinkedAndArray |        0 |       10 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |      1.928 μs |   0.0370 μs |     0.0812 μs |      1.900 μs |
|  RelativelySimpleCode |        0 |       10 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |      4.007 μs |   0.0846 μs |     0.2229 μs |      3.900 μs |
|                 **Towel** |        **0** |       **10** |    **2: .5*sqrt(range)** | **1: sqrt(sqrt(range))** |      **1.508 μs** |   **0.0484 μs** |     **0.1332 μs** |      **1.500 μs** |
|       HashSetAndArray |        0 |       10 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |      1.365 μs |   0.0313 μs |     0.0867 μs |      1.300 μs |
| SetHashLinkedAndArray |        0 |       10 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |      1.503 μs |   0.0416 μs |     0.1132 μs |      1.500 μs |
|  RelativelySimpleCode |        0 |       10 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |      4.075 μs |   0.1024 μs |     0.2768 μs |      4.000 μs |
|                 **Towel** |        **0** |       **10** |    **2: .5*sqrt(range)** |    **2: .5*sqrt(range)** |      **1.351 μs** |   **0.0306 μs** |     **0.0740 μs** |      **1.300 μs** |
|       HashSetAndArray |        0 |       10 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |      1.396 μs |   0.0317 μs |     0.0823 μs |      1.400 μs |
| SetHashLinkedAndArray |        0 |       10 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |      1.512 μs |   0.0421 μs |     0.1166 μs |      1.500 μs |
|  RelativelySimpleCode |        0 |       10 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |      4.648 μs |   0.3226 μs |     0.8993 μs |      4.250 μs |
|                 **Towel** |        **0** |       **10** |    **2: .5*sqrt(range)** |       **3: sqrt(range)** |      **1.521 μs** |   **0.0420 μs** |     **0.1163 μs** |      **1.500 μs** |
|       HashSetAndArray |        0 |       10 |    2: .5*sqrt(range) |       3: sqrt(range) |      1.496 μs |   0.0394 μs |     0.1051 μs |      1.450 μs |
| SetHashLinkedAndArray |        0 |       10 |    2: .5*sqrt(range) |       3: sqrt(range) |      1.799 μs |   0.0512 μs |     0.1418 μs |      1.800 μs |
|  RelativelySimpleCode |        0 |       10 |    2: .5*sqrt(range) |       3: sqrt(range) |      3.927 μs |   0.1107 μs |     0.3011 μs |      3.800 μs |
|                 **Towel** |        **0** |       **10** |    **2: .5*sqrt(range)** |     **4: 2*sqrt(range)** |      **2.604 μs** |   **0.0575 μs** |     **0.1555 μs** |      **2.550 μs** |
|       HashSetAndArray |        0 |       10 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |      1.519 μs |   0.0678 μs |     0.1844 μs |      1.500 μs |
| SetHashLinkedAndArray |        0 |       10 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |      1.931 μs |   0.0468 μs |     0.1304 μs |      1.900 μs |
|  RelativelySimpleCode |        0 |       10 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |      3.955 μs |   0.0923 μs |     0.2495 μs |      3.800 μs |
|                 **Towel** |        **0** |       **10** |       **3: sqrt(range)** | **1: sqrt(sqrt(range))** |      **1.421 μs** |   **0.0321 μs** |     **0.0810 μs** |      **1.400 μs** |
|       HashSetAndArray |        0 |       10 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |      1.434 μs |   0.0413 μs |     0.1123 μs |      1.400 μs |
| SetHashLinkedAndArray |        0 |       10 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |      1.591 μs |   0.0335 μs |     0.0708 μs |      1.600 μs |
|  RelativelySimpleCode |        0 |       10 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |      4.997 μs |   0.1014 μs |     0.2600 μs |      4.900 μs |
|                 **Towel** |        **0** |       **10** |       **3: sqrt(range)** |    **2: .5*sqrt(range)** |      **1.461 μs** |   **0.0366 μs** |     **0.0989 μs** |      **1.400 μs** |
|       HashSetAndArray |        0 |       10 |       3: sqrt(range) |    2: .5*sqrt(range) |      1.510 μs |   0.0338 μs |     0.0921 μs |      1.500 μs |
| SetHashLinkedAndArray |        0 |       10 |       3: sqrt(range) |    2: .5*sqrt(range) |      1.566 μs |   0.0450 μs |     0.1240 μs |      1.500 μs |
|  RelativelySimpleCode |        0 |       10 |       3: sqrt(range) |    2: .5*sqrt(range) |      5.007 μs |   0.0970 μs |     0.2451 μs |      5.000 μs |
|                 **Towel** |        **0** |       **10** |       **3: sqrt(range)** |       **3: sqrt(range)** |      **1.527 μs** |   **0.0388 μs** |     **0.1056 μs** |      **1.500 μs** |
|       HashSetAndArray |        0 |       10 |       3: sqrt(range) |       3: sqrt(range) |      1.586 μs |   0.0393 μs |     0.1095 μs |      1.550 μs |
| SetHashLinkedAndArray |        0 |       10 |       3: sqrt(range) |       3: sqrt(range) |      1.837 μs |   0.0552 μs |     0.1539 μs |      1.800 μs |
|  RelativelySimpleCode |        0 |       10 |       3: sqrt(range) |       3: sqrt(range) |      5.061 μs |   0.1342 μs |     0.3629 μs |      5.000 μs |
|                 **Towel** |        **0** |       **10** |       **3: sqrt(range)** |     **4: 2*sqrt(range)** |      **2.579 μs** |   **0.0703 μs** |     **0.1864 μs** |      **2.500 μs** |
|       HashSetAndArray |        0 |       10 |       3: sqrt(range) |     4: 2*sqrt(range) |      1.510 μs |   0.0373 μs |     0.0989 μs |      1.500 μs |
| SetHashLinkedAndArray |        0 |       10 |       3: sqrt(range) |     4: 2*sqrt(range) |      2.035 μs |   0.0516 μs |     0.1404 μs |      2.000 μs |
|  RelativelySimpleCode |        0 |       10 |       3: sqrt(range) |     4: 2*sqrt(range) |      5.152 μs |   0.1231 μs |     0.3307 μs |      5.100 μs |
|                 **Towel** |        **0** |       **10** |     **4: 2*sqrt(range)** | **1: sqrt(sqrt(range))** |      **1.539 μs** |   **0.0278 μs** |     **0.0737 μs** |      **1.550 μs** |
|       HashSetAndArray |        0 |       10 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |      1.520 μs |   0.0352 μs |     0.0963 μs |      1.500 μs |
| SetHashLinkedAndArray |        0 |       10 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |      1.555 μs |   0.0341 μs |     0.0848 μs |      1.550 μs |
|  RelativelySimpleCode |        0 |       10 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |      5.836 μs |   0.1198 μs |     0.3218 μs |      5.800 μs |
|                 **Towel** |        **0** |       **10** |     **4: 2*sqrt(range)** |    **2: .5*sqrt(range)** |      **1.483 μs** |   **0.0374 μs** |     **0.1004 μs** |      **1.500 μs** |
|       HashSetAndArray |        0 |       10 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |      1.497 μs |   0.0423 μs |     0.1164 μs |      1.450 μs |
| SetHashLinkedAndArray |        0 |       10 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |      1.573 μs |   0.0442 μs |     0.1180 μs |      1.600 μs |
|  RelativelySimpleCode |        0 |       10 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |      6.012 μs |   0.1220 μs |     0.3150 μs |      6.000 μs |
|                 **Towel** |        **0** |       **10** |     **4: 2*sqrt(range)** |       **3: sqrt(range)** |      **2.455 μs** |   **0.0526 μs** |     **0.1377 μs** |      **2.400 μs** |
|       HashSetAndArray |        0 |       10 |     4: 2*sqrt(range) |       3: sqrt(range) |      1.574 μs |   0.0415 μs |     0.1157 μs |      1.600 μs |
| SetHashLinkedAndArray |        0 |       10 |     4: 2*sqrt(range) |       3: sqrt(range) |      1.859 μs |   0.0505 μs |     0.1375 μs |      1.800 μs |
|  RelativelySimpleCode |        0 |       10 |     4: 2*sqrt(range) |       3: sqrt(range) |      6.606 μs |   0.3265 μs |     0.8994 μs |      6.350 μs |
|                 **Towel** |        **0** |       **10** |     **4: 2*sqrt(range)** |     **4: 2*sqrt(range)** |      **2.624 μs** |   **0.0545 μs** |     **0.1336 μs** |      **2.600 μs** |
|       HashSetAndArray |        0 |       10 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |      1.630 μs |   0.0409 μs |     0.1119 μs |      1.650 μs |
| SetHashLinkedAndArray |        0 |       10 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |      2.036 μs |   0.0575 μs |     0.1564 μs |      2.000 μs |
|  RelativelySimpleCode |        0 |       10 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |      6.408 μs |   0.2092 μs |     0.5655 μs |      6.300 μs |
|                 **Towel** |        **0** |      **100** | **1: sqrt(sqrt(range))** | **1: sqrt(sqrt(range))** |      **1.578 μs** |   **0.0354 μs** |     **0.0855 μs** |      **1.600 μs** |
|       HashSetAndArray |        0 |      100 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |      2.217 μs |   0.1379 μs |     0.3821 μs |      2.100 μs |
| SetHashLinkedAndArray |        0 |      100 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |      2.665 μs |   0.0754 μs |     0.2089 μs |      2.600 μs |
|  RelativelySimpleCode |        0 |      100 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |     10.651 μs |   0.4769 μs |     1.3295 μs |     10.000 μs |
|                 **Towel** |        **0** |      **100** | **1: sqrt(sqrt(range))** |    **2: .5*sqrt(range)** |      **1.592 μs** |   **0.0332 μs** |     **0.0277 μs** |      **1.600 μs** |
|       HashSetAndArray |        0 |      100 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |      2.297 μs |   0.0603 μs |     0.1641 μs |      2.200 μs |
| SetHashLinkedAndArray |        0 |      100 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |      3.166 μs |   0.0824 μs |     0.2228 μs |      3.100 μs |
|  RelativelySimpleCode |        0 |      100 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |     11.875 μs |   0.4431 μs |     1.2425 μs |     11.500 μs |
|                 **Towel** |        **0** |      **100** | **1: sqrt(sqrt(range))** |       **3: sqrt(range)** |      **1.761 μs** |   **0.0392 μs** |     **0.1059 μs** |      **1.700 μs** |
|       HashSetAndArray |        0 |      100 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |      2.460 μs |   0.0644 μs |     0.1740 μs |      2.400 μs |
| SetHashLinkedAndArray |        0 |      100 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |      3.439 μs |   0.0740 μs |     0.1974 μs |      3.500 μs |
|  RelativelySimpleCode |        0 |      100 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |     12.628 μs |   0.3863 μs |     1.1020 μs |     12.300 μs |
|                 **Towel** |        **0** |      **100** | **1: sqrt(sqrt(range))** |     **4: 2*sqrt(range)** |      **4.511 μs** |   **0.0918 μs** |     **0.2128 μs** |      **4.450 μs** |
|       HashSetAndArray |        0 |      100 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |      2.593 μs |   0.0591 μs |     0.1579 μs |      2.550 μs |
| SetHashLinkedAndArray |        0 |      100 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |      3.826 μs |   0.0794 μs |     0.1743 μs |      3.800 μs |
|  RelativelySimpleCode |        0 |      100 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |     13.244 μs |   0.4071 μs |     1.1146 μs |     13.400 μs |
|                 **Towel** |        **0** |      **100** |    **2: .5*sqrt(range)** | **1: sqrt(sqrt(range))** |      **1.602 μs** |   **0.0404 μs** |     **0.1086 μs** |      **1.600 μs** |
|       HashSetAndArray |        0 |      100 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |      2.059 μs |   0.0497 μs |     0.1360 μs |      2.000 μs |
| SetHashLinkedAndArray |        0 |      100 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |      2.638 μs |   0.0610 μs |     0.1711 μs |      2.600 μs |
|  RelativelySimpleCode |        0 |      100 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |     14.712 μs |   0.4265 μs |     1.1456 μs |     14.350 μs |
|                 **Towel** |        **0** |      **100** |    **2: .5*sqrt(range)** |    **2: .5*sqrt(range)** |      **1.620 μs** |   **0.0360 μs** |     **0.0813 μs** |      **1.600 μs** |
|       HashSetAndArray |        0 |      100 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |      2.303 μs |   0.0621 μs |     0.1719 μs |      2.250 μs |
| SetHashLinkedAndArray |        0 |      100 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |      3.151 μs |   0.0659 μs |     0.1254 μs |      3.100 μs |
|  RelativelySimpleCode |        0 |      100 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |     18.214 μs |   1.0003 μs |     2.8049 μs |     16.800 μs |
|                 **Towel** |        **0** |      **100** |    **2: .5*sqrt(range)** |       **3: sqrt(range)** |      **1.880 μs** |   **0.0396 μs** |     **0.1000 μs** |      **1.900 μs** |
|       HashSetAndArray |        0 |      100 |    2: .5*sqrt(range) |       3: sqrt(range) |      2.565 μs |   0.0548 μs |     0.1259 μs |      2.500 μs |
| SetHashLinkedAndArray |        0 |      100 |    2: .5*sqrt(range) |       3: sqrt(range) |      3.400 μs |   0.0694 μs |     0.1179 μs |      3.400 μs |
|  RelativelySimpleCode |        0 |      100 |    2: .5*sqrt(range) |       3: sqrt(range) |     17.705 μs |   0.6112 μs |     1.7338 μs |     17.150 μs |
|                 **Towel** |        **0** |      **100** |    **2: .5*sqrt(range)** |     **4: 2*sqrt(range)** |      **4.449 μs** |   **0.0921 μs** |     **0.1660 μs** |      **4.400 μs** |
|       HashSetAndArray |        0 |      100 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |      2.769 μs |   0.0574 μs |     0.1374 μs |      2.750 μs |
| SetHashLinkedAndArray |        0 |      100 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |      3.929 μs |   0.0816 μs |     0.1986 μs |      3.900 μs |
|  RelativelySimpleCode |        0 |      100 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |     17.752 μs |   0.5508 μs |     1.5446 μs |     17.500 μs |
|                 **Towel** |        **0** |      **100** |       **3: sqrt(range)** | **1: sqrt(sqrt(range))** |      **1.713 μs** |   **0.0398 μs** |     **0.1082 μs** |      **1.700 μs** |
|       HashSetAndArray |        0 |      100 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |      2.128 μs |   0.0551 μs |     0.1500 μs |      2.100 μs |
| SetHashLinkedAndArray |        0 |      100 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |      2.723 μs |   0.0663 μs |     0.1815 μs |      2.700 μs |
|  RelativelySimpleCode |        0 |      100 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |     28.895 μs |   1.3658 μs |     3.9188 μs |     26.700 μs |
|                 **Towel** |        **0** |      **100** |       **3: sqrt(range)** |    **2: .5*sqrt(range)** |      **1.818 μs** |   **0.0613 μs** |     **0.1656 μs** |      **1.800 μs** |
|       HashSetAndArray |        0 |      100 |       3: sqrt(range) |    2: .5*sqrt(range) |      2.388 μs |   0.0799 μs |     0.2132 μs |      2.300 μs |
| SetHashLinkedAndArray |        0 |      100 |       3: sqrt(range) |    2: .5*sqrt(range) |      3.278 μs |   0.0697 μs |     0.1931 μs |      3.250 μs |
|  RelativelySimpleCode |        0 |      100 |       3: sqrt(range) |    2: .5*sqrt(range) |     27.715 μs |   0.5506 μs |     0.4598 μs |     27.900 μs |
|                 **Towel** |        **0** |      **100** |       **3: sqrt(range)** |       **3: sqrt(range)** |      **4.141 μs** |   **0.0901 μs** |     **0.2451 μs** |      **4.100 μs** |
|       HashSetAndArray |        0 |      100 |       3: sqrt(range) |       3: sqrt(range) |      2.617 μs |   0.0543 μs |     0.1441 μs |      2.550 μs |
| SetHashLinkedAndArray |        0 |      100 |       3: sqrt(range) |       3: sqrt(range) |      3.555 μs |   0.0747 μs |     0.2021 μs |      3.500 μs |
|  RelativelySimpleCode |        0 |      100 |       3: sqrt(range) |       3: sqrt(range) |     30.846 μs |   1.7444 μs |     4.9487 μs |     28.200 μs |
|                 **Towel** |        **0** |      **100** |       **3: sqrt(range)** |     **4: 2*sqrt(range)** |      **4.505 μs** |   **0.0933 μs** |     **0.1610 μs** |      **4.500 μs** |
|       HashSetAndArray |        0 |      100 |       3: sqrt(range) |     4: 2*sqrt(range) |      2.740 μs |   0.0576 μs |     0.0946 μs |      2.700 μs |
| SetHashLinkedAndArray |        0 |      100 |       3: sqrt(range) |     4: 2*sqrt(range) |      3.999 μs |   0.0817 μs |     0.2093 μs |      3.900 μs |
|  RelativelySimpleCode |        0 |      100 |       3: sqrt(range) |     4: 2*sqrt(range) |     29.837 μs |   0.3924 μs |     0.4362 μs |     29.700 μs |
|                 **Towel** |        **0** |      **100** |     **4: 2*sqrt(range)** | **1: sqrt(sqrt(range))** |      **1.887 μs** |   **0.0422 μs** |     **0.1163 μs** |      **1.900 μs** |
|       HashSetAndArray |        0 |      100 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |      2.107 μs |   0.0395 μs |     0.0751 μs |      2.100 μs |
| SetHashLinkedAndArray |        0 |      100 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |      2.824 μs |   0.0711 μs |     0.1934 μs |      2.800 μs |
|  RelativelySimpleCode |        0 |      100 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |     52.396 μs |   2.5167 μs |     7.2612 μs |     48.000 μs |
|                 **Towel** |        **0** |      **100** |     **4: 2*sqrt(range)** |    **2: .5*sqrt(range)** |      **1.926 μs** |   **0.0472 μs** |     **0.1285 μs** |      **1.900 μs** |
|       HashSetAndArray |        0 |      100 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |      2.412 μs |   0.0598 μs |     0.1657 μs |      2.400 μs |
| SetHashLinkedAndArray |        0 |      100 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |      3.326 μs |   0.0727 μs |     0.2014 μs |      3.300 μs |
|  RelativelySimpleCode |        0 |      100 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |     47.457 μs |   0.8712 μs |     0.7723 μs |     47.500 μs |
|                 **Towel** |        **0** |      **100** |     **4: 2*sqrt(range)** |       **3: sqrt(range)** |      **4.232 μs** |   **0.0875 μs** |     **0.2179 μs** |      **4.200 μs** |
|       HashSetAndArray |        0 |      100 |     4: 2*sqrt(range) |       3: sqrt(range) |      2.645 μs |   0.0632 μs |     0.1708 μs |      2.600 μs |
| SetHashLinkedAndArray |        0 |      100 |     4: 2*sqrt(range) |       3: sqrt(range) |      3.597 μs |   0.0621 μs |     0.0967 μs |      3.600 μs |
|  RelativelySimpleCode |        0 |      100 |     4: 2*sqrt(range) |       3: sqrt(range) |     52.159 μs |   2.3572 μs |     6.7251 μs |     49.100 μs |
|                 **Towel** |        **0** |      **100** |     **4: 2*sqrt(range)** |     **4: 2*sqrt(range)** |      **4.857 μs** |   **0.0988 μs** |     **0.2385 μs** |      **4.800 μs** |
|       HashSetAndArray |        0 |      100 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |      2.984 μs |   0.0612 μs |     0.1405 μs |      3.000 μs |
| SetHashLinkedAndArray |        0 |      100 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |      4.659 μs |   0.2704 μs |     0.7539 μs |      4.250 μs |
|  RelativelySimpleCode |        0 |      100 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |     50.482 μs |   0.9721 μs |     0.9983 μs |     50.400 μs |
|                 **Towel** |        **0** |     **1000** | **1: sqrt(sqrt(range))** | **1: sqrt(sqrt(range))** |      **1.719 μs** |   **0.0383 μs** |     **0.0833 μs** |      **1.700 μs** |
|       HashSetAndArray |        0 |     1000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |      7.535 μs |   0.6482 μs |     1.8908 μs |      6.500 μs |
| SetHashLinkedAndArray |        0 |     1000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |     11.145 μs |   0.9894 μs |     2.8548 μs |      9.400 μs |
|  RelativelySimpleCode |        0 |     1000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |    122.996 μs |   6.5870 μs |    18.8993 μs |    112.800 μs |
|                 **Towel** |        **0** |     **1000** | **1: sqrt(sqrt(range))** |    **2: .5*sqrt(range)** |      **1.896 μs** |   **0.0417 μs** |     **0.0999 μs** |      **1.900 μs** |
|       HashSetAndArray |        0 |     1000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |      8.708 μs |   0.5981 μs |     1.7447 μs |      7.850 μs |
| SetHashLinkedAndArray |        0 |     1000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |     10.489 μs |   0.2090 μs |     0.2998 μs |     10.450 μs |
|  RelativelySimpleCode |        0 |     1000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |    109.950 μs |   1.9950 μs |     3.4413 μs |    108.600 μs |
|                 **Towel** |        **0** |     **1000** | **1: sqrt(sqrt(range))** |       **3: sqrt(range)** |      **2.228 μs** |   **0.0476 μs** |     **0.0882 μs** |      **2.200 μs** |
|       HashSetAndArray |        0 |     1000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |      9.427 μs |   0.6121 μs |     1.7660 μs |      8.750 μs |
| SetHashLinkedAndArray |        0 |     1000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |     12.122 μs |   0.2418 μs |     0.3765 μs |     12.050 μs |
|  RelativelySimpleCode |        0 |     1000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |     99.805 μs |   6.1832 μs |    17.9385 μs |     89.500 μs |
|                 **Towel** |        **0** |     **1000** | **1: sqrt(sqrt(range))** |     **4: 2*sqrt(range)** |     **16.608 μs** |   **0.3272 μs** |     **0.4368 μs** |     **16.600 μs** |
|       HashSetAndArray |        0 |     1000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |     11.809 μs |   0.5121 μs |     1.4611 μs |     11.500 μs |
| SetHashLinkedAndArray |        0 |     1000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |     15.332 μs |   0.2239 μs |     0.2750 μs |     15.300 μs |
|  RelativelySimpleCode |        0 |     1000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |    124.771 μs |   6.6232 μs |    18.5722 μs |    114.400 μs |
|                 **Towel** |        **0** |     **1000** |    **2: .5*sqrt(range)** | **1: sqrt(sqrt(range))** |      **1.837 μs** |   **0.0397 μs** |     **0.0975 μs** |      **1.800 μs** |
|       HashSetAndArray |        0 |     1000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |      7.595 μs |   0.5411 μs |     1.5612 μs |      6.800 μs |
| SetHashLinkedAndArray |        0 |     1000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |      9.285 μs |   0.1526 μs |     0.2713 μs |      9.200 μs |
|  RelativelySimpleCode |        0 |     1000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |    317.785 μs |   7.6733 μs |    20.6139 μs |    311.600 μs |
|                 **Towel** |        **0** |     **1000** |    **2: .5*sqrt(range)** |    **2: .5*sqrt(range)** |      **2.126 μs** |   **0.0424 μs** |     **0.1031 μs** |      **2.100 μs** |
|       HashSetAndArray |        0 |     1000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |      8.953 μs |   0.7378 μs |     2.1522 μs |      7.900 μs |
| SetHashLinkedAndArray |        0 |     1000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |     10.500 μs |   0.2007 μs |     0.1567 μs |     10.550 μs |
|  RelativelySimpleCode |        0 |     1000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |    350.769 μs |  16.4807 μs |    45.1157 μs |    338.400 μs |
|                 **Towel** |        **0** |     **1000** |    **2: .5*sqrt(range)** |       **3: sqrt(range)** |      **2.620 μs** |   **0.0560 μs** |     **0.1193 μs** |      **2.600 μs** |
|       HashSetAndArray |        0 |     1000 |    2: .5*sqrt(range) |       3: sqrt(range) |      9.783 μs |   0.5937 μs |     1.7223 μs |      9.050 μs |
| SetHashLinkedAndArray |        0 |     1000 |    2: .5*sqrt(range) |       3: sqrt(range) |     14.300 μs |   1.1327 μs |     3.3222 μs |     12.400 μs |
|  RelativelySimpleCode |        0 |     1000 |    2: .5*sqrt(range) |       3: sqrt(range) |    352.795 μs |  10.5168 μs |    29.1419 μs |    339.250 μs |
|                 **Towel** |        **0** |     **1000** |    **2: .5*sqrt(range)** |     **4: 2*sqrt(range)** |     **17.594 μs** |   **0.5696 μs** |     **1.6251 μs** |     **16.950 μs** |
|       HashSetAndArray |        0 |     1000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |     11.762 μs |   0.6633 μs |     1.9348 μs |     11.300 μs |
| SetHashLinkedAndArray |        0 |     1000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |     16.716 μs |   0.7010 μs |     1.9656 μs |     15.900 μs |
|  RelativelySimpleCode |        0 |     1000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |    378.082 μs |  15.9744 μs |    45.5757 μs |    361.750 μs |
|                 **Towel** |        **0** |     **1000** |       **3: sqrt(range)** | **1: sqrt(sqrt(range))** |      **2.144 μs** |   **0.0466 μs** |     **0.0920 μs** |      **2.100 μs** |
|       HashSetAndArray |        0 |     1000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |      6.765 μs |   0.1010 μs |     0.1631 μs |      6.800 μs |
| SetHashLinkedAndArray |        0 |     1000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |      9.496 μs |   0.1910 μs |     0.2615 μs |      9.450 μs |
|  RelativelySimpleCode |        0 |     1000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |    638.733 μs |  27.8338 μs |    78.9599 μs |    607.450 μs |
|                 **Towel** |        **0** |     **1000** |       **3: sqrt(range)** |    **2: .5*sqrt(range)** |      **2.596 μs** |   **0.0536 μs** |     **0.1164 μs** |      **2.600 μs** |
|       HashSetAndArray |        0 |     1000 |       3: sqrt(range) |    2: .5*sqrt(range) |      9.005 μs |   0.7799 μs |     2.2501 μs |      8.000 μs |
| SetHashLinkedAndArray |        0 |     1000 |       3: sqrt(range) |    2: .5*sqrt(range) |     12.197 μs |   1.0223 μs |     2.9331 μs |     10.700 μs |
|  RelativelySimpleCode |        0 |     1000 |       3: sqrt(range) |    2: .5*sqrt(range) |    690.754 μs |  21.1314 μs |    59.6016 μs |    666.050 μs |
|                 **Towel** |        **0** |     **1000** |       **3: sqrt(range)** |       **3: sqrt(range)** |     **15.354 μs** |   **0.9087 μs** |     **2.6362 μs** |     **14.000 μs** |
|       HashSetAndArray |        0 |     1000 |       3: sqrt(range) |       3: sqrt(range) |      8.937 μs |   0.2574 μs |     0.6645 μs |      9.000 μs |
| SetHashLinkedAndArray |        0 |     1000 |       3: sqrt(range) |       3: sqrt(range) |     14.627 μs |   1.2867 μs |     3.6919 μs |     12.600 μs |
|  RelativelySimpleCode |        0 |     1000 |       3: sqrt(range) |       3: sqrt(range) |    751.659 μs |  16.1410 μs |    44.1858 μs |    739.900 μs |
|                 **Towel** |        **0** |     **1000** |       **3: sqrt(range)** |     **4: 2*sqrt(range)** |     **17.843 μs** |   **0.6435 μs** |     **1.8462 μs** |     **17.100 μs** |
|       HashSetAndArray |        0 |     1000 |       3: sqrt(range) |     4: 2*sqrt(range) |     11.152 μs |   0.3998 μs |     1.0946 μs |     11.000 μs |
| SetHashLinkedAndArray |        0 |     1000 |       3: sqrt(range) |     4: 2*sqrt(range) |     16.545 μs |   0.5809 μs |     1.5804 μs |     15.900 μs |
|  RelativelySimpleCode |        0 |     1000 |       3: sqrt(range) |     4: 2*sqrt(range) |    787.246 μs |  33.8691 μs |    94.9726 μs |    745.200 μs |
|                 **Towel** |        **0** |     **1000** |     **4: 2*sqrt(range)** | **1: sqrt(sqrt(range))** |      **2.804 μs** |   **0.0592 μs** |     **0.1127 μs** |      **2.800 μs** |
|       HashSetAndArray |        0 |     1000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |      8.328 μs |   0.6973 μs |     2.0119 μs |      7.200 μs |
| SetHashLinkedAndArray |        0 |     1000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |     10.913 μs |   0.7752 μs |     2.1864 μs |      9.800 μs |
|  RelativelySimpleCode |        0 |     1000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |  1,276.846 μs |  59.5131 μs |   167.8578 μs |  1,234.950 μs |
|                 **Towel** |        **0** |     **1000** |     **4: 2*sqrt(range)** |    **2: .5*sqrt(range)** |      **3.255 μs** |   **0.0652 μs** |     **0.0800 μs** |      **3.300 μs** |
|       HashSetAndArray |        0 |     1000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |      8.317 μs |   0.1695 μs |     0.3575 μs |      8.400 μs |
| SetHashLinkedAndArray |        0 |     1000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |     12.219 μs |   0.8756 μs |     2.4840 μs |     11.100 μs |
|  RelativelySimpleCode |        0 |     1000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |  1,360.421 μs |  82.0956 μs |   235.5478 μs |  1,287.850 μs |
|                 **Towel** |        **0** |     **1000** |     **4: 2*sqrt(range)** |       **3: sqrt(range)** |     **14.123 μs** |   **0.2844 μs** |     **0.4672 μs** |     **14.000 μs** |
|       HashSetAndArray |        0 |     1000 |     4: 2*sqrt(range) |       3: sqrt(range) |     10.510 μs |   0.6198 μs |     1.8178 μs |      9.600 μs |
| SetHashLinkedAndArray |        0 |     1000 |     4: 2*sqrt(range) |       3: sqrt(range) |     14.670 μs |   1.0395 μs |     3.0157 μs |     13.100 μs |
|  RelativelySimpleCode |        0 |     1000 |     4: 2*sqrt(range) |       3: sqrt(range) |  1,416.335 μs |  89.0432 μs |   256.9099 μs |  1,401.000 μs |
|                 **Towel** |        **0** |     **1000** |     **4: 2*sqrt(range)** |     **4: 2*sqrt(range)** |     **17.141 μs** |   **0.3258 μs** |     **0.5073 μs** |     **17.150 μs** |
|       HashSetAndArray |        0 |     1000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |     12.358 μs |   0.6093 μs |     1.7284 μs |     12.000 μs |
| SetHashLinkedAndArray |        0 |     1000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |     17.055 μs |   0.5908 μs |     1.6370 μs |     16.400 μs |
|  RelativelySimpleCode |        0 |     1000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |  1,553.892 μs |  80.0496 μs |   223.1462 μs |  1,528.250 μs |
|                 **Towel** |        **0** |    **10000** | **1: sqrt(sqrt(range))** | **1: sqrt(sqrt(range))** |      **1.869 μs** |   **0.0414 μs** |     **0.0967 μs** |      **1.900 μs** |
|       HashSetAndArray |        0 |    10000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |     54.359 μs |   1.0774 μs |     2.2009 μs |     53.700 μs |
| SetHashLinkedAndArray |        0 |    10000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |     75.634 μs |   3.1168 μs |     8.3193 μs |     71.900 μs |
|  RelativelySimpleCode |        0 |    10000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |    **10000** | **1: sqrt(sqrt(range))** |    **2: .5*sqrt(range)** |      **3.370 μs** |   **0.0700 μs** |     **0.1244 μs** |      **3.400 μs** |
|       HashSetAndArray |        0 |    10000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |     59.179 μs |   2.1610 μs |     5.8792 μs |     56.850 μs |
| SetHashLinkedAndArray |        0 |    10000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |     88.100 μs |   1.6939 μs |     4.0256 μs |     86.800 μs |
|  RelativelySimpleCode |        0 |    10000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |    **10000** | **1: sqrt(sqrt(range))** |       **3: sqrt(range)** |      **6.457 μs** |   **0.1058 μs** |     **0.0938 μs** |      **6.500 μs** |
|       HashSetAndArray |        0 |    10000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |     75.938 μs |   6.1622 μs |    18.1693 μs |     63.400 μs |
| SetHashLinkedAndArray |        0 |    10000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |     96.020 μs |   4.6880 μs |    12.6743 μs |     90.200 μs |
|  RelativelySimpleCode |        0 |    10000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |    **10000** | **1: sqrt(sqrt(range))** |     **4: 2*sqrt(range)** |    **129.189 μs** |   **7.3747 μs** |    **21.5122 μs** |    **122.100 μs** |
|       HashSetAndArray |        0 |    10000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |     74.485 μs |   3.0290 μs |     8.1892 μs |     70.800 μs |
| SetHashLinkedAndArray |        0 |    10000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |    125.689 μs |   8.2227 μs |    24.2447 μs |    112.400 μs |
|  RelativelySimpleCode |        0 |    10000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |    **10000** |    **2: .5*sqrt(range)** | **1: sqrt(sqrt(range))** |      **2.798 μs** |   **0.0594 μs** |     **0.1102 μs** |      **2.800 μs** |
|       HashSetAndArray |        0 |    10000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |     61.651 μs |   4.0656 μs |    11.7950 μs |     55.500 μs |
| SetHashLinkedAndArray |        0 |    10000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |     77.674 μs |   2.8605 μs |     7.7823 μs |     73.550 μs |
|  RelativelySimpleCode |        0 |    10000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |    **10000** |    **2: .5*sqrt(range)** |    **2: .5*sqrt(range)** |      **4.971 μs** |   **0.1019 μs** |     **0.1758 μs** |      **4.950 μs** |
|       HashSetAndArray |        0 |    10000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |     64.766 μs |   4.7548 μs |    13.4110 μs |     58.350 μs |
| SetHashLinkedAndArray |        0 |    10000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |     90.850 μs |   1.7289 μs |     1.5326 μs |     90.900 μs |
|  RelativelySimpleCode |        0 |    10000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |    **10000** |    **2: .5*sqrt(range)** |       **3: sqrt(range)** |      **9.000 μs** |   **0.1251 μs** |     **0.1109 μs** |      **9.000 μs** |
|       HashSetAndArray |        0 |    10000 |    2: .5*sqrt(range) |       3: sqrt(range) |     63.820 μs |   2.1958 μs |     5.8610 μs |     61.800 μs |
| SetHashLinkedAndArray |        0 |    10000 |    2: .5*sqrt(range) |       3: sqrt(range) |     95.974 μs |   4.2888 μs |    11.5949 μs |     90.600 μs |
|  RelativelySimpleCode |        0 |    10000 |    2: .5*sqrt(range) |       3: sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |    **10000** |    **2: .5*sqrt(range)** |     **4: 2*sqrt(range)** |    **144.847 μs** |   **9.5973 μs** |    **27.9959 μs** |    **137.550 μs** |
|       HashSetAndArray |        0 |    10000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |     81.093 μs |   5.3666 μs |    15.3112 μs |     72.400 μs |
| SetHashLinkedAndArray |        0 |    10000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |    107.097 μs |   4.8042 μs |    13.1515 μs |    101.200 μs |
|  RelativelySimpleCode |        0 |    10000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |    **10000** |       **3: sqrt(range)** | **1: sqrt(sqrt(range))** |      **3.696 μs** |   **0.0775 μs** |     **0.1529 μs** |      **3.700 μs** |
|       HashSetAndArray |        0 |    10000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |     55.989 μs |   0.9195 μs |     1.5363 μs |     55.900 μs |
| SetHashLinkedAndArray |        0 |    10000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |     77.682 μs |   3.2348 μs |     8.7455 μs |     74.400 μs |
|  RelativelySimpleCode |        0 |    10000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |    **10000** |       **3: sqrt(range)** |    **2: .5*sqrt(range)** |      **6.813 μs** |   **0.1376 μs** |     **0.1740 μs** |      **6.800 μs** |
|       HashSetAndArray |        0 |    10000 |       3: sqrt(range) |    2: .5*sqrt(range) |     57.781 μs |   0.9843 μs |     2.0763 μs |     57.550 μs |
| SetHashLinkedAndArray |        0 |    10000 |       3: sqrt(range) |    2: .5*sqrt(range) |     91.874 μs |   1.8130 μs |     2.5416 μs |     91.500 μs |
|  RelativelySimpleCode |        0 |    10000 |       3: sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |    **10000** |       **3: sqrt(range)** |       **3: sqrt(range)** |     **95.383 μs** |   **1.2512 μs** |     **0.9769 μs** |     **95.450 μs** |
|       HashSetAndArray |        0 |    10000 |       3: sqrt(range) |       3: sqrt(range) |     64.848 μs |   2.1360 μs |     5.5517 μs |     62.900 μs |
| SetHashLinkedAndArray |        0 |    10000 |       3: sqrt(range) |       3: sqrt(range) |     91.066 μs |   1.8148 μs |     4.6194 μs |     89.750 μs |
|  RelativelySimpleCode |        0 |    10000 |       3: sqrt(range) |       3: sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |    **10000** |       **3: sqrt(range)** |     **4: 2*sqrt(range)** |    **123.606 μs** |   **7.3843 μs** |    **21.1869 μs** |    **112.850 μs** |
|       HashSetAndArray |        0 |    10000 |       3: sqrt(range) |     4: 2*sqrt(range) |     80.806 μs |   5.5690 μs |    16.1566 μs |     72.050 μs |
| SetHashLinkedAndArray |        0 |    10000 |       3: sqrt(range) |     4: 2*sqrt(range) |    122.301 μs |   8.7624 μs |    25.8362 μs |    109.650 μs |
|  RelativelySimpleCode |        0 |    10000 |       3: sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |    **10000** |     **4: 2*sqrt(range)** | **1: sqrt(sqrt(range))** |      **5.818 μs** |   **0.1118 μs** |     **0.1929 μs** |      **5.800 μs** |
|       HashSetAndArray |        0 |    10000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |     68.435 μs |   5.1989 μs |    15.2474 μs |     63.800 μs |
| SetHashLinkedAndArray |        0 |    10000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |     92.545 μs |   8.6540 μs |    24.9687 μs |     77.500 μs |
|  RelativelySimpleCode |        0 |    10000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |    **10000** |     **4: 2*sqrt(range)** |    **2: .5*sqrt(range)** |    **112.612 μs** |   **6.0668 μs** |    **17.7928 μs** |    **106.700 μs** |
|       HashSetAndArray |        0 |    10000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |     71.717 μs |   6.3592 μs |    18.7503 μs |     61.000 μs |
| SetHashLinkedAndArray |        0 |    10000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |    100.975 μs |   5.0216 μs |    14.5686 μs |     92.100 μs |
|  RelativelySimpleCode |        0 |    10000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |    **10000** |     **4: 2*sqrt(range)** |       **3: sqrt(range)** |    **103.249 μs** |   **2.5532 μs** |     **6.8150 μs** |    **100.300 μs** |
|       HashSetAndArray |        0 |    10000 |     4: 2*sqrt(range) |       3: sqrt(range) |     75.269 μs |   6.3932 μs |    18.8506 μs |     62.950 μs |
| SetHashLinkedAndArray |        0 |    10000 |     4: 2*sqrt(range) |       3: sqrt(range) |     95.277 μs |   3.1934 μs |     8.6337 μs |     91.750 μs |
|  RelativelySimpleCode |        0 |    10000 |     4: 2*sqrt(range) |       3: sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |    **10000** |     **4: 2*sqrt(range)** |     **4: 2*sqrt(range)** |    **134.248 μs** |   **9.0213 μs** |    **26.4580 μs** |    **120.800 μs** |
|       HashSetAndArray |        0 |    10000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |     75.285 μs |   1.8585 μs |     4.8633 μs |     73.550 μs |
| SetHashLinkedAndArray |        0 |    10000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |    123.721 μs |   7.4502 μs |    21.9671 μs |    111.250 μs |
|  RelativelySimpleCode |        0 |    10000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |   **100000** | **1: sqrt(sqrt(range))** | **1: sqrt(sqrt(range))** |      **2.491 μs** |   **0.1105 μs** |     **0.3026 μs** |      **2.400 μs** |
|       HashSetAndArray |        0 |   100000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |    611.667 μs |  35.0789 μs |   101.7702 μs |    572.100 μs |
| SetHashLinkedAndArray |        0 |   100000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |    989.045 μs | 101.1748 μs |   298.3162 μs |    866.000 μs |
|  RelativelySimpleCode |        0 |   100000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |   **100000** | **1: sqrt(sqrt(range))** |    **2: .5*sqrt(range)** |     **13.342 μs** |   **0.2317 μs** |     **0.1935 μs** |     **13.350 μs** |
|       HashSetAndArray |        0 |   100000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |    568.944 μs |  16.1126 μs |    43.8356 μs |    556.050 μs |
| SetHashLinkedAndArray |        0 |   100000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |    847.004 μs |  28.4384 μs |    79.7442 μs |    819.200 μs |
|  RelativelySimpleCode |        0 |   100000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |   **100000** | **1: sqrt(sqrt(range))** |       **3: sqrt(range)** |     **39.433 μs** |   **0.7632 μs** |     **0.9085 μs** |     **39.500 μs** |
|       HashSetAndArray |        0 |   100000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |    592.642 μs |  22.8443 μs |    64.8055 μs |    568.800 μs |
| SetHashLinkedAndArray |        0 |   100000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |    958.616 μs |  40.4951 μs |   112.8843 μs |    928.400 μs |
|  RelativelySimpleCode |        0 |   100000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |   **100000** | **1: sqrt(sqrt(range))** |     **4: 2*sqrt(range)** |  **1,329.857 μs** |  **40.5559 μs** |   **117.0129 μs** |  **1,304.050 μs** |
|       HashSetAndArray |        0 |   100000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |    714.291 μs |  25.9137 μs |    71.8068 μs |    691.600 μs |
| SetHashLinkedAndArray |        0 |   100000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |  1,055.651 μs |  68.2993 μs |   190.3912 μs |  1,073.700 μs |
|  RelativelySimpleCode |        0 |   100000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |   **100000** |    **2: .5*sqrt(range)** | **1: sqrt(sqrt(range))** |      **5.448 μs** |   **0.1083 μs** |     **0.1289 μs** |      **5.400 μs** |
|       HashSetAndArray |        0 |   100000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |    573.924 μs |  21.7875 μs |    62.5124 μs |    553.000 μs |
| SetHashLinkedAndArray |        0 |   100000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |    813.978 μs |  33.3653 μs |    92.4551 μs |    790.100 μs |
|  RelativelySimpleCode |        0 |   100000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |   **100000** |    **2: .5*sqrt(range)** |    **2: .5*sqrt(range)** |     **24.864 μs** |   **0.4674 μs** |     **0.4144 μs** |     **24.700 μs** |
|       HashSetAndArray |        0 |   100000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |    630.164 μs |  28.8566 μs |    83.2578 μs |    600.650 μs |
| SetHashLinkedAndArray |        0 |   100000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |    868.832 μs |  33.8583 μs |    94.3835 μs |    829.700 μs |
|  RelativelySimpleCode |        0 |   100000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |   **100000** |    **2: .5*sqrt(range)** |       **3: sqrt(range)** |     **66.567 μs** |   **2.2645 μs** |     **6.5336 μs** |     **63.050 μs** |
|       HashSetAndArray |        0 |   100000 |    2: .5*sqrt(range) |       3: sqrt(range) |    621.233 μs |  36.9491 μs |   104.8185 μs |    574.950 μs |
| SetHashLinkedAndArray |        0 |   100000 |    2: .5*sqrt(range) |       3: sqrt(range) |  1,052.471 μs |  48.8546 μs |   141.7360 μs |  1,004.700 μs |
|  RelativelySimpleCode |        0 |   100000 |    2: .5*sqrt(range) |       3: sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |   **100000** |    **2: .5*sqrt(range)** |     **4: 2*sqrt(range)** |  **1,204.647 μs** |  **35.7065 μs** |   **101.8726 μs** |  **1,164.100 μs** |
|       HashSetAndArray |        0 |   100000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |    772.796 μs |  35.7811 μs |   100.9212 μs |    740.200 μs |
| SetHashLinkedAndArray |        0 |   100000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |  1,180.344 μs |  52.4266 μs |   151.2627 μs |  1,106.450 μs |
|  RelativelySimpleCode |        0 |   100000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |   **100000** |       **3: sqrt(range)** | **1: sqrt(sqrt(range))** |      **8.880 μs** |   **0.1724 μs** |     **0.1612 μs** |      **8.900 μs** |
|       HashSetAndArray |        0 |   100000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |    563.978 μs |  16.6534 μs |    46.6978 μs |    551.400 μs |
| SetHashLinkedAndArray |        0 |   100000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |    723.120 μs |  18.0789 μs |    50.9919 μs |    706.900 μs |
|  RelativelySimpleCode |        0 |   100000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |   **100000** |       **3: sqrt(range)** |    **2: .5*sqrt(range)** |     **38.450 μs** |   **0.7473 μs** |     **0.7339 μs** |     **38.000 μs** |
|       HashSetAndArray |        0 |   100000 |       3: sqrt(range) |    2: .5*sqrt(range) |    565.405 μs |  13.4272 μs |    36.9824 μs |    555.550 μs |
| SetHashLinkedAndArray |        0 |   100000 |       3: sqrt(range) |    2: .5*sqrt(range) |    832.170 μs |  29.4737 μs |    82.1610 μs |    799.750 μs |
|  RelativelySimpleCode |        0 |   100000 |       3: sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |   **100000** |       **3: sqrt(range)** |       **3: sqrt(range)** |  **1,051.243 μs** |  **68.5979 μs** |   **195.7137 μs** |  **1,044.900 μs** |
|       HashSetAndArray |        0 |   100000 |       3: sqrt(range) |       3: sqrt(range) |    570.460 μs |  14.3691 μs |    39.5768 μs |    557.950 μs |
| SetHashLinkedAndArray |        0 |   100000 |       3: sqrt(range) |       3: sqrt(range) |  1,097.707 μs |  36.5423 μs |   101.2585 μs |  1,058.400 μs |
|  RelativelySimpleCode |        0 |   100000 |       3: sqrt(range) |       3: sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |   **100000** |       **3: sqrt(range)** |     **4: 2*sqrt(range)** |  **1,243.703 μs** |  **42.8555 μs** |   **122.9603 μs** |  **1,210.600 μs** |
|       HashSetAndArray |        0 |   100000 |       3: sqrt(range) |     4: 2*sqrt(range) |    697.620 μs |  22.5827 μs |    60.6671 μs |    677.550 μs |
| SetHashLinkedAndArray |        0 |   100000 |       3: sqrt(range) |     4: 2*sqrt(range) |  1,119.361 μs |  37.7032 μs |   103.8455 μs |  1,076.550 μs |
|  RelativelySimpleCode |        0 |   100000 |       3: sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |   **100000** |     **4: 2*sqrt(range)** | **1: sqrt(sqrt(range))** |     **15.729 μs** |   **0.2743 μs** |     **0.2431 μs** |     **15.700 μs** |
|       HashSetAndArray |        0 |   100000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |    563.433 μs |  18.7341 μs |    51.2844 μs |    545.000 μs |
| SetHashLinkedAndArray |        0 |   100000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |    735.434 μs |  17.9493 μs |    49.1360 μs |    720.000 μs |
|  RelativelySimpleCode |        0 |   100000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |   **100000** |     **4: 2*sqrt(range)** |    **2: .5*sqrt(range)** |    **916.341 μs** |  **34.5884 μs** |    **96.9897 μs** |    **897.200 μs** |
|       HashSetAndArray |        0 |   100000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |    584.816 μs |  17.9818 μs |    51.0115 μs |    566.200 μs |
| SetHashLinkedAndArray |        0 |   100000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |    837.826 μs |  25.8415 μs |    72.0357 μs |    817.200 μs |
|  RelativelySimpleCode |        0 |   100000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |   **100000** |     **4: 2*sqrt(range)** |       **3: sqrt(range)** |    **953.901 μs** |  **41.9288 μs** |   **118.9450 μs** |    **939.300 μs** |
|       HashSetAndArray |        0 |   100000 |     4: 2*sqrt(range) |       3: sqrt(range) |    582.329 μs |  18.2451 μs |    49.0144 μs |    559.100 μs |
| SetHashLinkedAndArray |        0 |   100000 |     4: 2*sqrt(range) |       3: sqrt(range) |    925.760 μs |  29.8357 μs |    81.6748 μs |    903.900 μs |
|  RelativelySimpleCode |        0 |   100000 |     4: 2*sqrt(range) |       3: sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |   **100000** |     **4: 2*sqrt(range)** |     **4: 2*sqrt(range)** |  **1,182.419 μs** |  **23.4893 μs** |    **54.4401 μs** |  **1,168.200 μs** |
|       HashSetAndArray |        0 |   100000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |    737.794 μs |  27.5527 μs |    76.3485 μs |    717.400 μs |
| SetHashLinkedAndArray |        0 |   100000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |  1,237.263 μs |  54.6905 μs |   158.6671 μs |  1,197.250 μs |
|  RelativelySimpleCode |        0 |   100000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |  **1000000** | **1: sqrt(sqrt(range))** | **1: sqrt(sqrt(range))** |      **3.575 μs** |   **0.2149 μs** |     **0.6025 μs** |      **3.300 μs** |
|       HashSetAndArray |        0 |  1000000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |  6,172.451 μs | 128.1054 μs |   369.6132 μs |  6,091.300 μs |
| SetHashLinkedAndArray |        0 |  1000000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |  6,975.152 μs | 344.0002 μs | 1,014.2922 μs |  6,633.050 μs |
|  RelativelySimpleCode |        0 |  1000000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |  **1000000** | **1: sqrt(sqrt(range))** |    **2: .5*sqrt(range)** |     **95.525 μs** |   **1.6255 μs** |     **1.8719 μs** |     **94.600 μs** |
|       HashSetAndArray |        0 |  1000000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |  6,501.083 μs | 127.5471 μs |   209.5635 μs |  6,533.300 μs |
| SetHashLinkedAndArray |        0 |  1000000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |  7,203.153 μs | 143.3859 μs |   305.5671 μs |  7,139.600 μs |
|  RelativelySimpleCode |        0 |  1000000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |  **1000000** | **1: sqrt(sqrt(range))** |       **3: sqrt(range)** |    **371.191 μs** |   **7.4235 μs** |    **19.5566 μs** |    **365.200 μs** |
|       HashSetAndArray |        0 |  1000000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |  9,251.968 μs | 184.2769 μs |   396.6755 μs |  9,214.850 μs |
| SetHashLinkedAndArray |        0 |  1000000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |  9,282.840 μs | 184.3712 μs |   400.8079 μs |  9,236.100 μs |
|  RelativelySimpleCode |        0 |  1000000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |  **1000000** | **1: sqrt(sqrt(range))** |     **4: 2*sqrt(range)** |  **9,662.765 μs** | **190.3304 μs** |   **413.7627 μs** |  **9,630.400 μs** |
|       HashSetAndArray |        0 |  1000000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) | 12,190.689 μs | 240.9239 μs |   345.5259 μs | 12,187.550 μs |
| SetHashLinkedAndArray |        0 |  1000000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |  9,681.355 μs | 176.6877 μs |   409.5005 μs |  9,625.750 μs |
|  RelativelySimpleCode |        0 |  1000000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |  **1000000** |    **2: .5*sqrt(range)** | **1: sqrt(sqrt(range))** |     **16.838 μs** |   **0.3335 μs** |     **0.2785 μs** |     **16.800 μs** |
|       HashSetAndArray |        0 |  1000000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |  6,224.877 μs | 145.5487 μs |   422.2631 μs |  6,120.300 μs |
| SetHashLinkedAndArray |        0 |  1000000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |  7,103.085 μs | 375.2392 μs | 1,106.4013 μs |  6,590.750 μs |
|  RelativelySimpleCode |        0 |  1000000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |  **1000000** |    **2: .5*sqrt(range)** |    **2: .5*sqrt(range)** |    **223.643 μs** |   **4.3739 μs** |     **8.9347 μs** |    **221.000 μs** |
|       HashSetAndArray |        0 |  1000000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |  6,520.874 μs | 129.7586 μs |   213.1970 μs |  6,528.800 μs |
| SetHashLinkedAndArray |        0 |  1000000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |  7,126.596 μs | 140.7365 μs |   342.5720 μs |  7,110.650 μs |
|  RelativelySimpleCode |        0 |  1000000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |  **1000000** |    **2: .5*sqrt(range)** |       **3: sqrt(range)** |    **734.658 μs** |  **44.1139 μs** |   **127.9823 μs** |    **681.600 μs** |
|       HashSetAndArray |        0 |  1000000 |    2: .5*sqrt(range) |       3: sqrt(range) |  9,173.915 μs | 181.2025 μs |   423.5550 μs |  9,110.300 μs |
| SetHashLinkedAndArray |        0 |  1000000 |    2: .5*sqrt(range) |       3: sqrt(range) |  9,280.582 μs | 185.2580 μs |   454.4410 μs |  9,148.250 μs |
|  RelativelySimpleCode |        0 |  1000000 |    2: .5*sqrt(range) |       3: sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |  **1000000** |    **2: .5*sqrt(range)** |     **4: 2*sqrt(range)** |  **9,957.600 μs** | **198.3510 μs** |   **455.7444 μs** |  **9,958.000 μs** |
|       HashSetAndArray |        0 |  1000000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) | 12,298.108 μs | 241.2666 μs |   422.5591 μs | 12,236.400 μs |
| SetHashLinkedAndArray |        0 |  1000000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |  9,753.620 μs | 194.7056 μs |   365.7043 μs |  9,711.500 μs |
|  RelativelySimpleCode |        0 |  1000000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |  **1000000** |       **3: sqrt(range)** | **1: sqrt(sqrt(range))** |     **31.200 μs** |   **0.4148 μs** |     **0.3464 μs** |     **31.100 μs** |
|       HashSetAndArray |        0 |  1000000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |  6,290.796 μs | 156.0404 μs |   447.7091 μs |  6,214.200 μs |
| SetHashLinkedAndArray |        0 |  1000000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |  7,073.344 μs | 373.9696 μs | 1,102.6577 μs |  6,625.650 μs |
|  RelativelySimpleCode |        0 |  1000000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |  **1000000** |       **3: sqrt(range)** |    **2: .5*sqrt(range)** |    **351.594 μs** |   **5.9542 μs** |     **6.3709 μs** |    **351.050 μs** |
|       HashSetAndArray |        0 |  1000000 |       3: sqrt(range) |    2: .5*sqrt(range) |  6,834.394 μs | 136.4090 μs |   366.4540 μs |  6,785.550 μs |
| SetHashLinkedAndArray |        0 |  1000000 |       3: sqrt(range) |    2: .5*sqrt(range) |  7,083.914 μs | 140.2902 μs |   330.6805 μs |  7,042.250 μs |
|  RelativelySimpleCode |        0 |  1000000 |       3: sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |  **1000000** |       **3: sqrt(range)** |       **3: sqrt(range)** |  **9,320.287 μs** | **183.4001 μs** |   **357.7077 μs** |  **9,275.100 μs** |
|       HashSetAndArray |        0 |  1000000 |       3: sqrt(range) |       3: sqrt(range) |  9,556.996 μs | 190.3172 μs |   549.1086 μs |  9,535.200 μs |
| SetHashLinkedAndArray |        0 |  1000000 |       3: sqrt(range) |       3: sqrt(range) |  9,326.591 μs | 186.0144 μs |   466.6738 μs |  9,253.050 μs |
|  RelativelySimpleCode |        0 |  1000000 |       3: sqrt(range) |       3: sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |  **1000000** |       **3: sqrt(range)** |     **4: 2*sqrt(range)** |  **9,856.723 μs** | **194.4498 μs** |   **340.5632 μs** |  **9,815.800 μs** |
|       HashSetAndArray |        0 |  1000000 |       3: sqrt(range) |     4: 2*sqrt(range) | 12,107.042 μs | 234.0084 μs |   304.2771 μs | 12,157.700 μs |
| SetHashLinkedAndArray |        0 |  1000000 |       3: sqrt(range) |     4: 2*sqrt(range) |  9,731.988 μs | 192.2726 μs |   388.4002 μs |  9,737.600 μs |
|  RelativelySimpleCode |        0 |  1000000 |       3: sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |  **1000000** |     **4: 2*sqrt(range)** | **1: sqrt(sqrt(range))** |     **61.722 μs** |   **1.0533 μs** |     **1.1270 μs** |     **61.200 μs** |
|       HashSetAndArray |        0 |  1000000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |  6,265.586 μs | 138.9565 μs |   398.6921 μs |  6,279.800 μs |
| SetHashLinkedAndArray |        0 |  1000000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |  7,045.887 μs | 366.5260 μs | 1,080.7101 μs |  6,653.100 μs |
|  RelativelySimpleCode |        0 |  1000000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |  **1000000** |     **4: 2*sqrt(range)** |    **2: .5*sqrt(range)** |  **7,269.188 μs** | **162.9913 μs** |   **448.9254 μs** |  **7,188.200 μs** |
|       HashSetAndArray |        0 |  1000000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |  6,676.519 μs | 134.9501 μs |   395.7853 μs |  6,608.600 μs |
| SetHashLinkedAndArray |        0 |  1000000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |  7,229.454 μs | 156.9550 μs |   424.3368 μs |  7,219.300 μs |
|  RelativelySimpleCode |        0 |  1000000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |  **1000000** |     **4: 2*sqrt(range)** |       **3: sqrt(range)** |  **9,173.020 μs** | **180.8630 μs** |   **344.1106 μs** |  **9,139.500 μs** |
|       HashSetAndArray |        0 |  1000000 |     4: 2*sqrt(range) |       3: sqrt(range) |  9,078.712 μs | 180.3220 μs |   399.5811 μs |  9,066.500 μs |
| SetHashLinkedAndArray |        0 |  1000000 |     4: 2*sqrt(range) |       3: sqrt(range) |  9,141.930 μs | 182.0765 μs |   350.7995 μs |  9,132.200 μs |
|  RelativelySimpleCode |        0 |  1000000 |     4: 2*sqrt(range) |       3: sqrt(range) |            NA |          NA |            NA |            NA |
|                 **Towel** |        **0** |  **1000000** |     **4: 2*sqrt(range)** |     **4: 2*sqrt(range)** |  **9,851.300 μs** | **196.2522 μs** |   **387.3827 μs** |  **9,842.100 μs** |
|       HashSetAndArray |        0 |  1000000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) | 12,231.558 μs | 242.1852 μs |   448.9058 μs | 12,150.300 μs |
| SetHashLinkedAndArray |        0 |  1000000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |  9,967.537 μs | 198.4607 μs |   342.3350 μs |  9,965.450 μs |
|  RelativelySimpleCode |        0 |  1000000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |            NA |            NA |


