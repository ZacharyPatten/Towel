# Lazy Caching

<a href="https://github.com/ZacharyPatten/Towel" alt="Github Repository"><img alt="github repo" src="https://img.shields.io/badge/github-repo-black?logo=github&amp;style=flat" title="Go To Github Repo" alt="Github Repository"></a>

The source code for all benchmarks are in [Tools/Towel.Benchmarking](https://github.com/ZacharyPatten/Towel/tree/main/Tools/Towel_Benchmarking).

``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19042.1110 (20H2/October2020Update)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.6.21355.2
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  DefaultJob : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT


```
|                          Method |    N |       Mean |      Error |     StdDev |     Median | Ratio | RatioSD |
|-------------------------------- |----- |-----------:|-----------:|-----------:|-----------:|------:|--------:|
|                            **Lazy** |    **1** |  **30.862 ns** |  **0.4427 ns** |  **0.3924 ns** |  **30.724 ns** |  **1.00** |    **0.00** |
|                        LazyNone |    1 |  18.947 ns |  0.3677 ns |  0.3439 ns |  18.815 ns |  0.61 |    0.01 |
|             LazyPublicationOnly |    1 |  21.604 ns |  0.4601 ns |  0.8753 ns |  21.303 ns |  0.72 |    0.03 |
|                           SLazy |    1 |  25.503 ns |  0.3819 ns |  0.3572 ns |  25.475 ns |  0.82 |    0.02 |
|                    SLazyNoCatch |    1 |  23.901 ns |  0.0781 ns |  0.0609 ns |  23.897 ns |  0.77 |    0.01 |
|            SLazyPublicationLock |    1 |  25.200 ns |  0.2728 ns |  0.2552 ns |  25.192 ns |  0.82 |    0.01 |
|     SLazyPublicationLockNoCatch |    1 |  24.802 ns |  0.1757 ns |  0.1467 ns |  24.798 ns |  0.80 |    0.01 |
|                     SLazyNoLock |    1 |  11.896 ns |  0.1831 ns |  0.1713 ns |  11.858 ns |  0.38 |    0.01 |
|              SLazyNoLockNoCatch |    1 |  10.111 ns |  0.0989 ns |  0.0826 ns |  10.097 ns |  0.33 |    0.01 |
|                       ValueLazy |    1 |  20.179 ns |  0.2200 ns |  0.1837 ns |  20.232 ns |  0.65 |    0.01 |
|                ValueLazyNoCatch |    1 |  19.035 ns |  0.3854 ns |  0.3605 ns |  18.930 ns |  0.62 |    0.01 |
|        ValueLazyPublicationLock |    1 |  21.487 ns |  0.4350 ns |  0.5954 ns |  21.489 ns |  0.70 |    0.02 |
| ValueLazyPublicationLockNoCatch |    1 |  20.290 ns |  0.1659 ns |  0.1471 ns |  20.234 ns |  0.66 |    0.01 |
|                 ValueLazyNoLock |    1 |   7.458 ns |  0.1596 ns |  0.1333 ns |   7.401 ns |  0.24 |    0.00 |
|          ValueLazyNoLockNoCatch |    1 |   5.672 ns |  0.0416 ns |  0.0348 ns |   5.679 ns |  0.18 |    0.00 |
|                                 |      |            |            |            |            |       |         |
|                            **Lazy** |   **10** |  **40.699 ns** |  **0.5908 ns** |  **0.5527 ns** |  **40.757 ns** |  **1.00** |    **0.00** |
|                        LazyNone |   10 |  25.093 ns |  0.2615 ns |  0.2446 ns |  25.074 ns |  0.62 |    0.01 |
|             LazyPublicationOnly |   10 |  27.518 ns |  0.5714 ns |  1.1927 ns |  26.964 ns |  0.70 |    0.03 |
|                           SLazy |   10 |  28.954 ns |  0.2155 ns |  0.1682 ns |  28.921 ns |  0.71 |    0.01 |
|                    SLazyNoCatch |   10 |  28.813 ns |  0.2420 ns |  0.2021 ns |  28.812 ns |  0.71 |    0.01 |
|            SLazyPublicationLock |   10 |  29.856 ns |  0.4875 ns |  0.5987 ns |  29.641 ns |  0.74 |    0.02 |
|     SLazyPublicationLockNoCatch |   10 |  28.951 ns |  0.0839 ns |  0.0655 ns |  28.963 ns |  0.71 |    0.01 |
|                     SLazyNoLock |   10 |  18.458 ns |  0.1508 ns |  0.1337 ns |  18.422 ns |  0.45 |    0.01 |
|              SLazyNoLockNoCatch |   10 |  16.861 ns |  0.0979 ns |  0.0764 ns |  16.863 ns |  0.41 |    0.01 |
|                       ValueLazy |   10 |  24.287 ns |  0.1536 ns |  0.1437 ns |  24.288 ns |  0.60 |    0.01 |
|                ValueLazyNoCatch |   10 |  23.283 ns |  0.2339 ns |  0.1953 ns |  23.264 ns |  0.57 |    0.01 |
|        ValueLazyPublicationLock |   10 |  25.317 ns |  0.2061 ns |  0.1928 ns |  25.358 ns |  0.62 |    0.01 |
| ValueLazyPublicationLockNoCatch |   10 |  24.809 ns |  0.1341 ns |  0.1120 ns |  24.780 ns |  0.61 |    0.01 |
|                 ValueLazyNoLock |   10 |  14.965 ns |  0.0992 ns |  0.0928 ns |  14.958 ns |  0.37 |    0.01 |
|          ValueLazyNoLockNoCatch |   10 |  12.874 ns |  0.0460 ns |  0.0384 ns |  12.862 ns |  0.32 |    0.00 |
|                                 |      |            |            |            |            |       |         |
|                            **Lazy** |  **100** |  **91.838 ns** |  **0.5093 ns** |  **0.4514 ns** |  **91.656 ns** |  **1.00** |    **0.00** |
|                        LazyNone |  100 | 105.193 ns |  1.4903 ns |  1.3940 ns | 104.791 ns |  1.15 |    0.02 |
|             LazyPublicationOnly |  100 |  81.861 ns |  0.4681 ns |  0.3655 ns |  82.008 ns |  0.89 |    0.00 |
|                           SLazy |  100 |  79.852 ns |  0.5897 ns |  0.4924 ns |  79.751 ns |  0.87 |    0.01 |
|                    SLazyNoCatch |  100 |  81.056 ns |  1.6281 ns |  2.2286 ns |  81.134 ns |  0.88 |    0.02 |
|            SLazyPublicationLock |  100 |  81.578 ns |  0.4488 ns |  0.4198 ns |  81.479 ns |  0.89 |    0.01 |
|     SLazyPublicationLockNoCatch |  100 |  79.908 ns |  0.3864 ns |  0.3615 ns |  79.869 ns |  0.87 |    0.01 |
|                     SLazyNoLock |  100 |  71.874 ns |  0.5779 ns |  0.5123 ns |  71.746 ns |  0.78 |    0.01 |
|              SLazyNoLockNoCatch |  100 |  69.266 ns |  0.4168 ns |  0.3695 ns |  69.131 ns |  0.75 |    0.01 |
|                       ValueLazy |  100 |  72.879 ns |  0.2705 ns |  0.2112 ns |  72.913 ns |  0.79 |    0.00 |
|                ValueLazyNoCatch |  100 |  74.209 ns |  0.2873 ns |  0.2687 ns |  74.206 ns |  0.81 |    0.01 |
|        ValueLazyPublicationLock |  100 |  74.352 ns |  0.6568 ns |  0.6143 ns |  74.037 ns |  0.81 |    0.01 |
| ValueLazyPublicationLockNoCatch |  100 |  75.525 ns |  0.6337 ns |  0.5928 ns |  75.316 ns |  0.82 |    0.01 |
|                 ValueLazyNoLock |  100 |  67.334 ns |  0.2796 ns |  0.2183 ns |  67.339 ns |  0.73 |    0.00 |
|          ValueLazyNoLockNoCatch |  100 |  64.932 ns |  0.2492 ns |  0.2331 ns |  64.937 ns |  0.71 |    0.01 |
|                                 |      |            |            |            |            |       |         |
|                            **Lazy** | **1000** | **535.144 ns** |  **3.5164 ns** |  **3.2892 ns** | **534.105 ns** |  **1.00** |    **0.00** |
|                        LazyNone | 1000 | 550.618 ns |  3.6293 ns |  3.3949 ns | 550.567 ns |  1.03 |    0.01 |
|             LazyPublicationOnly | 1000 | 524.728 ns |  2.3492 ns |  2.0825 ns | 525.565 ns |  0.98 |    0.01 |
|                           SLazy | 1000 | 523.096 ns |  3.4357 ns |  2.8689 ns | 523.300 ns |  0.98 |    0.01 |
|                    SLazyNoCatch | 1000 | 518.426 ns |  1.8299 ns |  1.4287 ns | 518.825 ns |  0.97 |    0.01 |
|            SLazyPublicationLock | 1000 | 524.129 ns |  5.8493 ns |  5.1853 ns | 522.364 ns |  0.98 |    0.01 |
|     SLazyPublicationLockNoCatch | 1000 | 520.111 ns |  3.6929 ns |  3.0837 ns | 520.165 ns |  0.97 |    0.01 |
|                     SLazyNoLock | 1000 | 517.457 ns |  5.5981 ns |  5.2365 ns | 515.705 ns |  0.97 |    0.01 |
|              SLazyNoLockNoCatch | 1000 | 519.805 ns |  6.2953 ns |  5.8886 ns | 519.773 ns |  0.97 |    0.01 |
|                       ValueLazy | 1000 | 520.671 ns | 10.3221 ns | 11.8870 ns | 515.596 ns |  0.98 |    0.02 |
|                ValueLazyNoCatch | 1000 | 518.974 ns |  7.2035 ns |  5.6240 ns | 517.924 ns |  0.97 |    0.01 |
|        ValueLazyPublicationLock | 1000 | 525.665 ns | 10.4357 ns | 16.8517 ns | 524.213 ns |  0.98 |    0.03 |
| ValueLazyPublicationLockNoCatch | 1000 | 514.050 ns |  2.4043 ns |  2.1314 ns | 514.358 ns |  0.96 |    0.01 |
|                 ValueLazyNoLock | 1000 | 507.868 ns |  3.8694 ns |  3.2312 ns | 506.572 ns |  0.95 |    0.01 |
|          ValueLazyNoLockNoCatch | 1000 | 506.869 ns |  2.5243 ns |  2.2377 ns | 506.804 ns |  0.95 |    0.01 |

