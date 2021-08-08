# Lazy Initialization

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1110 (20H2/October2020Update)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.6.21355.2
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  Job-OIQAIQ : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                          Method |     N |          Mean |        Error |        StdDev |       Median | Ratio | RatioSD |
|-------------------------------- |------ |--------------:|-------------:|--------------:|-------------:|------:|--------:|
|                            **Lazy** |     **1** |     **500.00 ns** |     **0.000 ns** |      **0.000 ns** |     **500.0 ns** |  **1.00** |    **0.00** |
|                        LazyNone |     1 |     248.96 ns |    19.473 ns |     56.185 ns |     200.0 ns |  0.53 |    0.13 |
|             LazyPublicationOnly |     1 |     428.09 ns |    33.532 ns |     92.918 ns |     400.0 ns |  0.93 |    0.19 |
|                           SLazy |     1 |     338.95 ns |    21.142 ns |     60.661 ns |     300.0 ns |  0.71 |    0.13 |
|                    SLazyNoCatch |     1 |     271.13 ns |    23.314 ns |     67.637 ns |     300.0 ns |  0.63 |    0.13 |
|            SLazyPublicationLock |     1 |     370.67 ns |    18.137 ns |     45.836 ns |     400.0 ns |  0.74 |    0.09 |
|     SLazyPublicationLockNoCatch |     1 |     230.00 ns |    17.043 ns |     50.252 ns |     200.0 ns |  0.51 |    0.10 |
|                     SLazyNoLock |     1 |     135.00 ns |    16.258 ns |     47.937 ns |     100.0 ns |  0.30 |    0.10 |
|              SLazyNoLockNoCatch |     1 |      96.20 ns |     7.401 ns |     19.236 ns |     100.0 ns |  0.20 |    0.00 |
|                       ValueLazy |     1 |     401.02 ns |    41.622 ns |    121.413 ns |     400.0 ns |  0.76 |    0.22 |
|                ValueLazyNoCatch |     1 |     413.27 ns |    35.029 ns |    102.180 ns |     400.0 ns |  0.71 |    0.10 |
|        ValueLazyPublicationLock |     1 |     384.54 ns |    49.337 ns |    143.135 ns |     300.0 ns |  0.74 |    0.24 |
| ValueLazyPublicationLockNoCatch |     1 |     429.03 ns |    33.915 ns |     96.212 ns |     400.0 ns |  0.94 |    0.12 |
|                 ValueLazyNoLock |     1 |     161.00 ns |    17.969 ns |     52.982 ns |     200.0 ns |  0.31 |    0.10 |
|          ValueLazyNoLockNoCatch |     1 |     137.37 ns |    17.281 ns |     50.681 ns |     100.0 ns |  0.27 |    0.13 |
|                                 |       |               |              |               |              |       |         |
|                            **Lazy** |    **10** |     **797.70 ns** |    **26.703 ns** |     **73.099 ns** |     **800.0 ns** |  **1.00** |    **0.00** |
|                        LazyNone |    10 |     452.08 ns |    19.463 ns |     56.156 ns |     500.0 ns |  0.57 |    0.09 |
|             LazyPublicationOnly |    10 |     551.55 ns |    19.341 ns |     56.113 ns |     500.0 ns |  0.70 |    0.09 |
|                           SLazy |    10 |     519.79 ns |    17.142 ns |     49.460 ns |     500.0 ns |  0.66 |    0.09 |
|                    SLazyNoCatch |    10 |     486.36 ns |    12.531 ns |     34.514 ns |     500.0 ns |  0.61 |    0.07 |
|            SLazyPublicationLock |    10 |     554.55 ns |    20.530 ns |     56.546 ns |     600.0 ns |  0.70 |    0.09 |
|     SLazyPublicationLockNoCatch |    10 |     522.58 ns |    20.216 ns |     57.349 ns |     500.0 ns |  0.66 |    0.10 |
|                     SLazyNoLock |    10 |     234.34 ns |    17.671 ns |     51.827 ns |     200.0 ns |  0.30 |    0.07 |
|              SLazyNoLockNoCatch |    10 |     185.11 ns |    12.546 ns |     35.793 ns |     200.0 ns |  0.23 |    0.05 |
|                       ValueLazy |    10 |     562.24 ns |    18.097 ns |     52.789 ns |     600.0 ns |  0.71 |    0.09 |
|                ValueLazyNoCatch |    10 |     495.24 ns |    11.787 ns |     21.554 ns |     500.0 ns |  0.62 |    0.06 |
|        ValueLazyPublicationLock |    10 |     538.38 ns |    19.305 ns |     56.617 ns |     500.0 ns |  0.68 |    0.10 |
| ValueLazyPublicationLockNoCatch |    10 |     500.00 ns |     0.000 ns |      0.000 ns |     500.0 ns |  0.59 |    0.07 |
|                 ValueLazyNoLock |    10 |     177.00 ns |    18.579 ns |     54.781 ns |     150.0 ns |  0.22 |    0.07 |
|          ValueLazyNoLockNoCatch |    10 |     173.68 ns |    17.022 ns |     48.839 ns |     200.0 ns |  0.22 |    0.06 |
|                                 |       |               |              |               |              |       |         |
|                            **Lazy** |   **100** |   **3,017.24 ns** |    **60.662 ns** |     **88.918 ns** |   **3,000.0 ns** |  **1.00** |    **0.00** |
|                        LazyNone |   100 |   1,447.17 ns |    30.595 ns |     63.862 ns |   1,400.0 ns |  0.48 |    0.02 |
|             LazyPublicationOnly |   100 |   2,177.22 ns |    45.251 ns |    117.615 ns |   2,100.0 ns |  0.75 |    0.06 |
|                           SLazy |   100 |   2,333.33 ns |    47.513 ns |     71.116 ns |   2,300.0 ns |  0.77 |    0.04 |
|                    SLazyNoCatch |   100 |   2,275.00 ns |    45.535 ns |     44.721 ns |   2,300.0 ns |  0.75 |    0.03 |
|            SLazyPublicationLock |   100 |   2,515.38 ns |    44.972 ns |     37.553 ns |   2,500.0 ns |  0.82 |    0.02 |
|     SLazyPublicationLockNoCatch |   100 |   2,469.57 ns |    50.206 ns |     63.495 ns |   2,500.0 ns |  0.82 |    0.03 |
|                     SLazyNoLock |   100 |     821.11 ns |    18.232 ns |     50.823 ns |     800.0 ns |  0.27 |    0.02 |
|              SLazyNoLockNoCatch |   100 |     646.88 ns |    20.086 ns |     57.953 ns |     600.0 ns |  0.21 |    0.02 |
|                       ValueLazy |   100 |   2,483.33 ns |    50.585 ns |     84.515 ns |   2,500.0 ns |  0.83 |    0.04 |
|                ValueLazyNoCatch |   100 |   2,296.88 ns |    44.633 ns |     69.488 ns |   2,300.0 ns |  0.76 |    0.03 |
|        ValueLazyPublicationLock |   100 |   2,543.75 ns |    52.167 ns |     51.235 ns |   2,500.0 ns |  0.84 |    0.03 |
| ValueLazyPublicationLockNoCatch |   100 |   2,464.71 ns |    50.292 ns |     81.212 ns |   2,500.0 ns |  0.82 |    0.03 |
|                 ValueLazyNoLock |   100 |     770.67 ns |    19.269 ns |     48.695 ns |     800.0 ns |  0.25 |    0.02 |
|          ValueLazyNoLockNoCatch |   100 |     434.02 ns |    17.153 ns |     49.763 ns |     400.0 ns |  0.15 |    0.02 |
|                                 |       |               |              |               |              |       |         |
|                            **Lazy** |  **1000** |  **27,294.85 ns** | **2,234.495 ns** |  **6,482.675 ns** |  **25,600.0 ns** |  **1.00** |    **0.00** |
|                        LazyNone |  1000 |  11,654.55 ns |   229.937 ns |    282.383 ns |  11,600.0 ns |  0.41 |    0.08 |
|             LazyPublicationOnly |  1000 |  18,342.71 ns | 1,821.793 ns |  5,256.289 ns |  18,500.0 ns |  0.69 |    0.20 |
|                           SLazy |  1000 |  24,248.91 ns | 1,477.564 ns |  4,167.496 ns |  22,900.0 ns |  0.93 |    0.28 |
|                    SLazyNoCatch |  1000 |  22,102.11 ns | 1,683.197 ns |  4,829.409 ns |  20,700.0 ns |  0.83 |    0.21 |
|            SLazyPublicationLock |  1000 |  22,403.61 ns |   666.224 ns |  1,778.287 ns |  22,300.0 ns |  0.84 |    0.18 |
|     SLazyPublicationLockNoCatch |  1000 |  21,652.63 ns |   427.869 ns |    738.053 ns |  21,400.0 ns |  0.75 |    0.12 |
|                     SLazyNoLock |  1000 |   9,311.70 ns |   882.598 ns |  2,518.100 ns |   8,400.0 ns |  0.35 |    0.10 |
|              SLazyNoLockNoCatch |  1000 |   5,009.09 ns |   133.671 ns |    368.169 ns |   5,000.0 ns |  0.19 |    0.04 |
|                       ValueLazy |  1000 |  20,703.45 ns |   313.423 ns |    459.412 ns |  20,600.0 ns |  0.72 |    0.13 |
|                ValueLazyNoCatch |  1000 |  22,450.52 ns | 1,748.793 ns |  5,073.566 ns |  20,600.0 ns |  0.85 |    0.22 |
|        ValueLazyPublicationLock |  1000 |  22,500.00 ns |   444.912 ns |    456.892 ns |  22,300.0 ns |  0.82 |    0.14 |
| ValueLazyPublicationLockNoCatch |  1000 |  21,664.29 ns |   418.840 ns |    371.291 ns |  21,600.0 ns |  0.79 |    0.11 |
|                 ValueLazyNoLock |  1000 |   7,920.00 ns |   842.318 ns |  2,416.768 ns |   7,000.0 ns |  0.31 |    0.13 |
|          ValueLazyNoLockNoCatch |  1000 |   3,568.29 ns |   239.881 ns |    636.130 ns |   3,300.0 ns |  0.13 |    0.03 |
|                                 |       |               |              |               |              |       |         |
|                            **Lazy** | **10000** | **218,907.78 ns** | **9,542.685 ns** | **26,601.192 ns** | **208,050.0 ns** |  **1.00** |    **0.00** |
|                        LazyNone | 10000 | 110,638.89 ns | 7,558.797 ns | 21,070.906 ns | 104,300.0 ns |  0.51 |    0.12 |
|             LazyPublicationOnly | 10000 | 140,023.86 ns | 4,949.719 ns | 13,632.964 ns | 135,100.0 ns |  0.65 |    0.10 |
|                           SLazy | 10000 | 182,800.00 ns | 3,558.132 ns |  4,626.577 ns | 181,100.0 ns |  0.81 |    0.09 |
|                    SLazyNoCatch | 10000 | 174,667.07 ns | 2,857.663 ns |  5,152.956 ns | 173,050.0 ns |  0.80 |    0.09 |
|            SLazyPublicationLock | 10000 | 192,668.35 ns | 3,845.807 ns |  9,995.762 ns | 187,800.0 ns |  0.89 |    0.10 |
|     SLazyPublicationLockNoCatch | 10000 | 191,693.26 ns | 6,524.540 ns | 18,079.470 ns | 181,900.0 ns |  0.89 |    0.13 |
|                     SLazyNoLock | 10000 | 109,163.04 ns | 5,908.094 ns | 16,663.883 ns | 101,300.0 ns |  0.51 |    0.09 |
|              SLazyNoLockNoCatch | 10000 |  96,324.72 ns | 6,224.112 ns | 17,246.984 ns |  91,500.0 ns |  0.44 |    0.09 |
|                       ValueLazy | 10000 | 184,789.13 ns | 5,450.772 ns | 15,374.000 ns | 176,450.0 ns |  0.85 |    0.11 |
|                ValueLazyNoCatch | 10000 | 167,550.00 ns | 3,274.006 ns |  3,503.150 ns | 165,750.0 ns |  0.74 |    0.09 |
|        ValueLazyPublicationLock | 10000 | 190,793.10 ns | 5,096.158 ns | 13,950.651 ns | 184,500.0 ns |  0.88 |    0.11 |
| ValueLazyPublicationLockNoCatch | 10000 | 181,168.75 ns | 3,608.904 ns |  3,544.426 ns | 180,250.0 ns |  0.80 |    0.10 |
|                 ValueLazyNoLock | 10000 |  75,960.67 ns | 3,119.263 ns |  8,643.463 ns |  73,200.0 ns |  0.35 |    0.05 |
|          ValueLazyNoLockNoCatch | 10000 |  37,846.67 ns | 3,446.824 ns |  9,608.367 ns |  32,600.0 ns |  0.18 |    0.05 |

