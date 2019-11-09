These are the benchmarking results for the Towel project.

## Sorting Algorithms

<details>
	<summary><strong>Benchmark Results [click to expand]</strong></summary>
<p>

- The `XXXRunTime` benchmarks use runtime delegates for their compare functions.
- The `XXXCompileTime` benchmarks use structs for their compare functions (so there is no delegate at runtime).
- The `SystemArraySort` bencmark is the `System.Array.Sort` method. Note: the `System.Array.Sort` method does not allow custom sorting while all other benchmarked methods do.
- The `SystemArraySortDelegate` benchmark is the `System.Array.Sort<T>(T[], System.Comparison<T>)` method, where `System.Comparison<T>` is a runtime delegate.
- The `SystemArraySortIComparer` bencmark is the `System.Array.Sort<T>(T[], System.Collections.Generic.IComparer<T>)`.

The source code for these bencharks are included in the Towel GitHub repository here:
TODO: add link to source

``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), 64bit RyuJIT
  Job-FBJKNR : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), 64bit RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                   Method |     N |             Mean |               Error |              StdDev |           Median |
|------------------------- |------ |-----------------:|--------------------:|--------------------:|-----------------:|
|          **SystemArraySort** |    **10** |         **496.8 ns** |          **11.7612 ns** |          **17.9605 ns** |         **500.0 ns** |
|  SystemArraySortDelegate |    10 |         576.7 ns |          15.6195 ns |          42.4941 ns |         600.0 ns |
| SystemArraySortIComparer |    10 |       2,852.4 ns |          66.4189 ns |         121.4508 ns |       2,800.0 ns |
|            BubbleRunTime |    10 |       2,316.2 ns |          47.0989 ns |          79.9775 ns |       2,300.0 ns |
|        BubbleCompileTime |    10 |         455.6 ns |          18.3696 ns |          53.8748 ns |         500.0 ns |
|         SelectionRunTime |    10 |       1,510.1 ns |          34.1626 ns |          82.5066 ns |       1,500.0 ns |
|     SelectionCompileTime |    10 |         455.6 ns |          17.0291 ns |          49.9433 ns |         500.0 ns |
|         InsertionRunTime |    10 |       1,426.8 ns |          30.2424 ns |          80.1985 ns |       1,400.0 ns |
|     InsertionCompileTime |    10 |         290.1 ns |          10.7051 ns |          30.0183 ns |         300.0 ns |
|             QuickRunTime |    10 |       1,870.0 ns |          38.6427 ns |          68.6873 ns |       1,900.0 ns |
|         QuickCompileTime |    10 |         600.0 ns |           0.0000 ns |           0.0000 ns |         600.0 ns |
|             MergeRunTime |    10 |       1,700.0 ns |          35.9302 ns |          88.8106 ns |       1,700.0 ns |
|         MergeCompileTime |    10 |       1,106.6 ns |          36.4823 ns |          92.8591 ns |       1,100.0 ns |
|              HeapRunTime |    10 |       2,309.1 ns |          48.0686 ns |          59.0326 ns |       2,350.0 ns |
|          HeapCompileTime |    10 |       1,184.1 ns |          27.3102 ns |          62.7498 ns |       1,200.0 ns |
|           OddEvenRunTime |    10 |       1,683.3 ns |          36.5239 ns |          54.6672 ns |       1,650.0 ns |
|       OddEvenCompileTime |    10 |         354.0 ns |          16.9884 ns |          50.0908 ns |         400.0 ns |
|              SlowRunTime |    10 |       3,857.5 ns |          77.3772 ns |         137.5379 ns |       3,850.0 ns |
|          SlowCompileTime |    10 |       2,665.5 ns |          55.5319 ns |          81.3979 ns |       2,700.0 ns |
|              BogoRunTime |    10 | 454,549,124.7 ns | 117,478,375.4330 ns | 340,826,068.8078 ns | 411,594,100.0 ns |
|          BogoCompileTime |    10 | 406,109,296.9 ns | 116,914,790.0490 ns | 339,191,005.4171 ns | 316,398,900.0 ns |
|          **SystemArraySort** |  **1000** |      **29,433.3 ns** |         **195.6524 ns** |         **152.7525 ns** |      **29,450.0 ns** |
|  SystemArraySortDelegate |  1000 |      66,369.2 ns |       1,063.7055 ns |         888.2423 ns |      65,700.0 ns |
| SystemArraySortIComparer |  1000 |      76,796.2 ns |         357.9802 ns |         298.9297 ns |      76,750.0 ns |
|            BubbleRunTime |  1000 |  10,405,147.1 ns |     202,357.3616 ns |     207,806.1119 ns |  10,406,000.0 ns |
|        BubbleCompileTime |  1000 |   1,632,356.7 ns |      31,708.7806 ns |      47,460.2097 ns |   1,630,250.0 ns |
|         SelectionRunTime |  1000 |   4,439,530.3 ns |      87,409.3925 ns |     138,640.5593 ns |   4,417,700.0 ns |
|     SelectionCompileTime |  1000 |     881,669.6 ns |      18,717.6255 ns |      40,291.6636 ns |     872,650.0 ns |
|         InsertionRunTime |  1000 |   2,154,475.0 ns |      92,723.9448 ns |      91,067.3121 ns |   2,126,700.0 ns |
|     InsertionCompileTime |  1000 |     258,826.3 ns |       9,141.9683 ns |      26,811.8105 ns |     246,000.0 ns |
|             QuickRunTime |  1000 |     214,740.7 ns |       4,281.0689 ns |       9,030.2274 ns |     213,950.0 ns |
|         QuickCompileTime |  1000 |      62,381.8 ns |       1,681.5602 ns |       4,931.7249 ns |      60,400.0 ns |
|             MergeRunTime |  1000 |     139,487.7 ns |       2,748.3562 ns |       6,203.4980 ns |     140,650.0 ns |
|         MergeCompileTime |  1000 |      63,593.3 ns |       1,240.6463 ns |       1,856.9410 ns |      63,550.0 ns |
|              HeapRunTime |  1000 |     373,636.4 ns |       7,409.9261 ns |       9,100.0547 ns |     372,950.0 ns |
|          HeapCompileTime |  1000 |     298,575.0 ns |       6,987.0480 ns |       6,862.2154 ns |     298,050.0 ns |
|           OddEvenRunTime |  1000 |   5,142,362.9 ns |     101,911.5831 ns |     167,443.6535 ns |   5,108,400.0 ns |
|       OddEvenCompileTime |  1000 |     909,385.4 ns |      25,623.9977 ns |      75,150.7496 ns |     879,750.0 ns |
|              SlowRunTime |  1000 |               NA |                  NA |                  NA |               NA |
|          SlowCompileTime |  1000 |               NA |                  NA |                  NA |               NA |
|              BogoRunTime |  1000 |               NA |                  NA |                  NA |               NA |
|          BogoCompileTime |  1000 |               NA |                  NA |                  NA |               NA |
|          **SystemArraySort** | **10000** |     **367,530.8 ns** |       **1,107.7347 ns** |         **925.0087 ns** |     **367,600.0 ns** |
|  SystemArraySortDelegate | 10000 |     934,860.7 ns |      23,544.0488 ns |      33,766.1713 ns |     925,800.0 ns |
| SystemArraySortIComparer | 10000 |   1,036,469.2 ns |      18,667.0549 ns |      15,587.8363 ns |   1,038,300.0 ns |
|            BubbleRunTime | 10000 | 577,112,500.0 ns |   2,706,925.0679 ns |   2,399,618.5655 ns | 577,886,450.0 ns |
|        BubbleCompileTime | 10000 | 186,719,750.0 ns |   1,325,377.5967 ns |   1,174,912.7174 ns | 186,986,700.0 ns |
|         SelectionRunTime | 10000 | 250,770,306.7 ns |   1,743,838.8797 ns |   1,631,187.9700 ns | 250,902,200.0 ns |
|     SelectionCompileTime | 10000 |  85,242,746.7 ns |   1,588,868.4152 ns |   1,486,228.5014 ns |  84,801,700.0 ns |
|         InsertionRunTime | 10000 | 114,083,280.0 ns |     714,929.5920 ns |     668,745.5839 ns | 114,074,400.0 ns |
|     InsertionCompileTime | 10000 |  23,572,335.7 ns |     383,199.9462 ns |     339,696.7711 ns |  23,572,400.0 ns |
|             QuickRunTime | 10000 |   2,788,000.0 ns |      53,786.3348 ns |      52,825.3727 ns |   2,778,150.0 ns |
|         QuickCompileTime | 10000 |     755,259.3 ns |      15,058.4123 ns |      21,109.7910 ns |     751,800.0 ns |
|             MergeRunTime | 10000 |   1,783,223.1 ns |      49,166.4782 ns |      67,299.6304 ns |   1,771,600.0 ns |
|         MergeCompileTime | 10000 |     778,580.0 ns |      15,216.6407 ns |      17,523.5060 ns |     775,150.0 ns |
|              HeapRunTime | 10000 |   5,200,527.1 ns |     101,792.8843 ns |     167,248.6280 ns |   5,134,950.0 ns |
|          HeapCompileTime | 10000 |   4,076,458.8 ns |      79,922.8053 ns |      82,074.8367 ns |   4,078,200.0 ns |
|           OddEvenRunTime | 10000 | 286,383,876.7 ns |   1,157,230.6908 ns |   1,082,474.3062 ns | 286,566,650.0 ns |
|       OddEvenCompileTime | 10000 |  97,981,677.8 ns |   1,958,626.8128 ns |   2,095,708.7460 ns |  97,417,450.0 ns |
|              SlowRunTime | 10000 |               NA |                  NA |                  NA |               NA |
|          SlowCompileTime | 10000 |               NA |                  NA |                  NA |               NA |
|              BogoRunTime | 10000 |               NA |                  NA |                  NA |               NA |
|          BogoCompileTime | 10000 |               NA |                  NA |                  NA |               NA |

</p>
</details>

## Data structures

These benchmarks are still in development. They are not yet ready.