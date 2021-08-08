# Lazy Construction

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1110 (20H2/October2020Update)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.6.21355.2
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  DefaultJob : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT


```
|                          Method |     N |          Mean |        Error |       StdDev |        Median | Ratio | RatioSD |    Gen 0 | Gen 1 | Gen 2 |   Allocated |
|-------------------------------- |------ |--------------:|-------------:|-------------:|--------------:|------:|--------:|---------:|------:|------:|------------:|
|                            **Lazy** |     **1** |      **22.10 ns** |     **0.165 ns** |     **0.154 ns** |      **22.11 ns** |  **1.00** |    **0.00** |   **0.0382** |     **-** |     **-** |       **160 B** |
|                        LazyNone |     1 |      18.91 ns |     0.110 ns |     0.098 ns |      18.91 ns |  0.86 |    0.01 |   0.0306 |     - |     - |       128 B |
|             LazyPublicationOnly |     1 |      20.77 ns |     0.238 ns |     0.223 ns |      20.73 ns |  0.94 |    0.01 |   0.0306 |     - |     - |       128 B |
|                           SLazy |     1 |      16.30 ns |     0.159 ns |     0.133 ns |      16.28 ns |  0.74 |    0.01 |   0.0287 |     - |     - |       120 B |
|                    SLazyNoCatch |     1 |      16.05 ns |     0.123 ns |     0.096 ns |      16.05 ns |  0.73 |    0.01 |   0.0287 |     - |     - |       120 B |
|            SLazyPublicationLock |     1 |      16.49 ns |     0.339 ns |     0.595 ns |      16.25 ns |  0.77 |    0.03 |   0.0287 |     - |     - |       120 B |
|     SLazyPublicationLockNoCatch |     1 |      16.26 ns |     0.194 ns |     0.181 ns |      16.21 ns |  0.74 |    0.01 |   0.0287 |     - |     - |       120 B |
|                     SLazyNoLock |     1 |      16.20 ns |     0.203 ns |     0.180 ns |      16.12 ns |  0.73 |    0.01 |   0.0287 |     - |     - |       120 B |
|              SLazyNoLockNoCatch |     1 |      16.11 ns |     0.130 ns |     0.121 ns |      16.10 ns |  0.73 |    0.01 |   0.0287 |     - |     - |       120 B |
|                       ValueLazy |     1 |      11.60 ns |     0.064 ns |     0.053 ns |      11.60 ns |  0.52 |    0.00 |   0.0210 |     - |     - |        88 B |
|                ValueLazyNoCatch |     1 |      11.67 ns |     0.096 ns |     0.085 ns |      11.67 ns |  0.53 |    0.01 |   0.0210 |     - |     - |        88 B |
|        ValueLazyPublicationLock |     1 |      11.96 ns |     0.121 ns |     0.113 ns |      11.90 ns |  0.54 |    0.01 |   0.0210 |     - |     - |        88 B |
| ValueLazyPublicationLockNoCatch |     1 |      11.67 ns |     0.111 ns |     0.103 ns |      11.66 ns |  0.53 |    0.01 |   0.0210 |     - |     - |        88 B |
|                 ValueLazyNoLock |     1 |      11.78 ns |     0.094 ns |     0.088 ns |      11.75 ns |  0.53 |    0.01 |   0.0210 |     - |     - |        88 B |
|          ValueLazyNoLockNoCatch |     1 |      11.45 ns |     0.122 ns |     0.108 ns |      11.43 ns |  0.52 |    0.01 |   0.0210 |     - |     - |        88 B |
|                                 |       |               |              |              |               |       |         |          |       |       |             |
|                            **Lazy** |    **10** |     **200.80 ns** |     **1.719 ns** |     **1.608 ns** |     **200.37 ns** |  **1.00** |    **0.00** |   **0.3309** |     **-** |     **-** |     **1,384 B** |
|                        LazyNone |    10 |     174.82 ns |     1.168 ns |     0.975 ns |     175.09 ns |  0.87 |    0.01 |   0.2544 |     - |     - |     1,064 B |
|             LazyPublicationOnly |    10 |     178.58 ns |     1.480 ns |     1.384 ns |     178.56 ns |  0.89 |    0.01 |   0.2544 |     - |     - |     1,064 B |
|                           SLazy |    10 |     140.66 ns |     2.178 ns |     2.037 ns |     140.02 ns |  0.70 |    0.01 |   0.2351 |     - |     - |       984 B |
|                    SLazyNoCatch |    10 |     141.17 ns |     1.986 ns |     1.658 ns |     140.42 ns |  0.70 |    0.01 |   0.2351 |     - |     - |       984 B |
|            SLazyPublicationLock |    10 |     142.33 ns |     0.973 ns |     0.910 ns |     142.50 ns |  0.71 |    0.01 |   0.2351 |     - |     - |       984 B |
|     SLazyPublicationLockNoCatch |    10 |     140.48 ns |     1.832 ns |     1.624 ns |     140.08 ns |  0.70 |    0.01 |   0.2351 |     - |     - |       984 B |
|                     SLazyNoLock |    10 |     158.42 ns |     0.818 ns |     0.683 ns |     158.38 ns |  0.79 |    0.01 |   0.2351 |     - |     - |       984 B |
|              SLazyNoLockNoCatch |    10 |     142.11 ns |     2.531 ns |     2.243 ns |     141.54 ns |  0.71 |    0.01 |   0.2351 |     - |     - |       984 B |
|                       ValueLazy |    10 |      99.29 ns |     2.011 ns |     2.316 ns |      98.56 ns |  0.50 |    0.01 |   0.1587 |     - |     - |       664 B |
|                ValueLazyNoCatch |    10 |      99.70 ns |     1.016 ns |     0.950 ns |      99.94 ns |  0.50 |    0.01 |   0.1587 |     - |     - |       664 B |
|        ValueLazyPublicationLock |    10 |      92.64 ns |     0.877 ns |     0.777 ns |      92.50 ns |  0.46 |    0.00 |   0.1587 |     - |     - |       664 B |
| ValueLazyPublicationLockNoCatch |    10 |      97.68 ns |     1.131 ns |     0.944 ns |      97.46 ns |  0.49 |    0.01 |   0.1587 |     - |     - |       664 B |
|                 ValueLazyNoLock |    10 |      99.30 ns |     1.983 ns |     2.204 ns |      98.47 ns |  0.50 |    0.01 |   0.1587 |     - |     - |       664 B |
|          ValueLazyNoLockNoCatch |    10 |      93.89 ns |     1.064 ns |     0.831 ns |      93.90 ns |  0.47 |    0.00 |   0.1587 |     - |     - |       664 B |
|                                 |       |               |              |              |               |       |         |          |       |       |             |
|                            **Lazy** |   **100** |   **1,953.87 ns** |    **30.726 ns** |    **35.384 ns** |   **1,945.85 ns** |  **1.00** |    **0.00** |   **3.2539** |     **-** |     **-** |    **13,624 B** |
|                        LazyNone |   100 |   1,635.63 ns |    18.970 ns |    16.817 ns |   1,632.88 ns |  0.84 |    0.02 |   2.4910 |     - |     - |    10,424 B |
|             LazyPublicationOnly |   100 |   1,684.44 ns |    21.139 ns |    18.739 ns |   1,680.53 ns |  0.86 |    0.02 |   2.4910 |     - |     - |    10,424 B |
|                           SLazy |   100 |   1,328.97 ns |    22.073 ns |    36.878 ns |   1,315.80 ns |  0.69 |    0.02 |   2.3003 |     - |     - |     9,624 B |
|                    SLazyNoCatch |   100 |   1,318.93 ns |    10.520 ns |     9.840 ns |   1,319.72 ns |  0.67 |    0.02 |   2.3003 |     - |     - |     9,624 B |
|            SLazyPublicationLock |   100 |   1,294.66 ns |    10.447 ns |     8.156 ns |   1,296.35 ns |  0.66 |    0.02 |   2.3003 |     - |     - |     9,624 B |
|     SLazyPublicationLockNoCatch |   100 |   1,319.31 ns |    16.120 ns |    15.079 ns |   1,323.51 ns |  0.67 |    0.01 |   2.3003 |     - |     - |     9,624 B |
|                     SLazyNoLock |   100 |   1,316.53 ns |    18.315 ns |    15.294 ns |   1,316.23 ns |  0.67 |    0.02 |   2.3003 |     - |     - |     9,624 B |
|              SLazyNoLockNoCatch |   100 |   1,322.93 ns |    16.727 ns |    13.059 ns |   1,318.43 ns |  0.68 |    0.02 |   2.3003 |     - |     - |     9,624 B |
|                       ValueLazy |   100 |     847.03 ns |    14.735 ns |    20.169 ns |     843.36 ns |  0.44 |    0.01 |   1.5354 |     - |     - |     6,424 B |
|                ValueLazyNoCatch |   100 |     843.30 ns |    11.659 ns |     9.102 ns |     841.84 ns |  0.43 |    0.01 |   1.5354 |     - |     - |     6,424 B |
|        ValueLazyPublicationLock |   100 |     865.78 ns |     7.362 ns |     6.886 ns |     865.23 ns |  0.44 |    0.01 |   1.5354 |     - |     - |     6,424 B |
| ValueLazyPublicationLockNoCatch |   100 |     889.05 ns |    17.599 ns |    31.282 ns |     875.91 ns |  0.47 |    0.02 |   1.5354 |     - |     - |     6,424 B |
|                 ValueLazyNoLock |   100 |     850.02 ns |    16.836 ns |    30.785 ns |     837.16 ns |  0.44 |    0.02 |   1.5354 |     - |     - |     6,424 B |
|          ValueLazyNoLockNoCatch |   100 |     865.57 ns |    17.225 ns |    40.936 ns |     874.01 ns |  0.43 |    0.02 |   1.5354 |     - |     - |     6,424 B |
|                                 |       |               |              |              |               |       |         |          |       |       |             |
|                            **Lazy** |  **1000** |  **18,721.44 ns** |   **162.584 ns** |   **152.081 ns** |  **18,648.43 ns** |  **1.00** |    **0.00** |  **32.5012** |     **-** |     **-** |   **136,024 B** |
|                        LazyNone |  1000 |  16,006.07 ns |   183.048 ns |   171.223 ns |  15,941.16 ns |  0.86 |    0.01 |  24.8718 |     - |     - |   104,024 B |
|             LazyPublicationOnly |  1000 |  15,884.03 ns |    88.118 ns |    73.582 ns |  15,874.19 ns |  0.85 |    0.01 |  24.8718 |     - |     - |   104,024 B |
|                           SLazy |  1000 |  12,717.44 ns |   158.874 ns |   148.610 ns |  12,679.13 ns |  0.68 |    0.01 |  22.9492 |     - |     - |    96,024 B |
|                    SLazyNoCatch |  1000 |  12,799.51 ns |   160.029 ns |   149.691 ns |  12,739.91 ns |  0.68 |    0.01 |  22.9492 |     - |     - |    96,024 B |
|            SLazyPublicationLock |  1000 |  12,706.73 ns |    55.077 ns |    45.992 ns |  12,709.53 ns |  0.68 |    0.01 |  22.9492 |     - |     - |    96,024 B |
|     SLazyPublicationLockNoCatch |  1000 |  12,650.54 ns |   139.108 ns |   123.316 ns |  12,644.26 ns |  0.68 |    0.01 |  22.9492 |     - |     - |    96,024 B |
|                     SLazyNoLock |  1000 |  13,361.71 ns |   255.774 ns |   632.210 ns |  13,127.82 ns |  0.73 |    0.03 |  22.9492 |     - |     - |    96,024 B |
|              SLazyNoLockNoCatch |  1000 |  13,082.74 ns |   253.224 ns |   270.947 ns |  12,968.63 ns |  0.70 |    0.01 |  22.9492 |     - |     - |    96,024 B |
|                       ValueLazy |  1000 |   9,312.85 ns |   184.760 ns |   364.698 ns |   9,247.48 ns |  0.51 |    0.02 |  15.3046 |     - |     - |    64,024 B |
|                ValueLazyNoCatch |  1000 |   8,554.24 ns |   169.824 ns |   269.360 ns |   8,475.98 ns |  0.47 |    0.01 |  15.3046 |     - |     - |    64,024 B |
|        ValueLazyPublicationLock |  1000 |   9,047.24 ns |   179.591 ns |   354.495 ns |   8,940.28 ns |  0.49 |    0.02 |  15.3046 |     - |     - |    64,024 B |
| ValueLazyPublicationLockNoCatch |  1000 |   8,224.12 ns |    56.538 ns |    50.120 ns |   8,219.14 ns |  0.44 |    0.00 |  15.3046 |     - |     - |    64,024 B |
|                 ValueLazyNoLock |  1000 |   8,306.73 ns |   108.731 ns |   101.707 ns |   8,282.03 ns |  0.44 |    0.01 |  15.3046 |     - |     - |    64,024 B |
|          ValueLazyNoLockNoCatch |  1000 |   9,156.83 ns |   182.971 ns |   507.011 ns |   9,094.83 ns |  0.49 |    0.02 |  15.3046 |     - |     - |    64,024 B |
|                                 |       |               |              |              |               |       |         |          |       |       |             |
|                            **Lazy** | **10000** | **191,531.93 ns** | **3,796.800 ns** | **7,841.051 ns** | **191,027.50 ns** |  **1.00** |    **0.00** | **325.1953** |     **-** |     **-** | **1,360,024 B** |
|                        LazyNone | 10000 | 156,821.77 ns | 2,929.792 ns | 4,010.332 ns | 155,088.22 ns |  0.84 |    0.04 | 248.5352 |     - |     - | 1,040,024 B |
|             LazyPublicationOnly | 10000 | 158,475.50 ns | 3,022.222 ns | 5,371.996 ns | 157,175.73 ns |  0.83 |    0.05 | 248.5352 |     - |     - | 1,040,024 B |
|                           SLazy | 10000 | 128,670.97 ns | 2,565.950 ns | 7,067.371 ns | 127,533.41 ns |  0.68 |    0.04 | 229.4922 |     - |     - |   960,024 B |
|                    SLazyNoCatch | 10000 | 138,004.68 ns | 2,750.295 ns | 7,935.229 ns | 138,240.94 ns |  0.70 |    0.05 | 229.4922 |     - |     - |   960,024 B |
|            SLazyPublicationLock | 10000 | 140,635.28 ns | 2,487.396 ns | 2,864.489 ns | 140,420.89 ns |  0.75 |    0.03 | 229.4922 |     - |     - |   960,024 B |
|     SLazyPublicationLockNoCatch | 10000 | 140,216.36 ns | 2,790.505 ns | 2,610.240 ns | 140,052.15 ns |  0.75 |    0.03 | 229.4922 |     - |     - |   960,024 B |
|                     SLazyNoLock | 10000 | 141,435.35 ns | 2,561.818 ns | 2,396.326 ns | 140,907.08 ns |  0.75 |    0.03 | 229.4922 |     - |     - |   960,024 B |
|              SLazyNoLockNoCatch | 10000 | 138,594.90 ns | 2,397.436 ns | 2,242.563 ns | 138,406.91 ns |  0.74 |    0.03 | 229.4922 |     - |     - |   960,024 B |
|                       ValueLazy | 10000 |  94,516.01 ns | 1,399.428 ns | 1,309.025 ns |  94,728.54 ns |  0.50 |    0.02 | 152.9541 |     - |     - |   640,024 B |
|                ValueLazyNoCatch | 10000 |  85,303.24 ns | 1,694.948 ns | 3,894.425 ns |  82,767.11 ns |  0.45 |    0.03 | 152.9541 |     - |     - |   640,024 B |
|        ValueLazyPublicationLock | 10000 |  83,087.98 ns |   597.770 ns |   559.154 ns |  83,069.21 ns |  0.44 |    0.02 | 152.9541 |     - |     - |   640,024 B |
| ValueLazyPublicationLockNoCatch | 10000 |  87,196.07 ns |   593.401 ns |   555.068 ns |  87,256.82 ns |  0.46 |    0.02 | 152.9541 |     - |     - |   640,024 B |
|                 ValueLazyNoLock | 10000 |  87,338.28 ns |   618.735 ns |   548.493 ns |  87,214.61 ns |  0.47 |    0.02 | 152.9541 |     - |     - |   640,024 B |
|          ValueLazyNoLockNoCatch | 10000 |  80,823.75 ns | 1,609.269 ns | 4,432.395 ns |  79,400.54 ns |  0.43 |    0.03 | 152.9541 |     - |     - |   640,024 B |

