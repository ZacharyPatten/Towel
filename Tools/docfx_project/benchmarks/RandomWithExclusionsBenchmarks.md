# Random With Exclusions

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
|                Method | MinValue | MaxValue |                Count |              Exclued |          Mean |       Error |      StdDev |        Median |
|---------------------- |--------- |--------- |--------------------- |--------------------- |--------------:|------------:|------------:|--------------:|
|                 **Towel** |        **0** |       **10** | **1: sqrt(sqrt(range))** | **1: sqrt(sqrt(range))** |      **1.512 μs** |   **0.0322 μs** |   **0.0777 μs** |      **1.500 μs** |
|       HashSetAndArray |        0 |       10 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |      1.531 μs |   0.0324 μs |   0.0737 μs |      1.500 μs |
| SetHashLinkedAndArray |        0 |       10 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |      1.566 μs |   0.0343 μs |   0.0861 μs |      1.550 μs |
|  RelativelySimpleCode |        0 |       10 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |      3.947 μs |   0.0779 μs |   0.0800 μs |      3.900 μs |
|                 **Towel** |        **0** |       **10** | **1: sqrt(sqrt(range))** |    **2: .5*sqrt(range)** |      **1.514 μs** |   **0.0322 μs** |   **0.0866 μs** |      **1.500 μs** |
|       HashSetAndArray |        0 |       10 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |      1.542 μs |   0.0327 μs |   0.0815 μs |      1.500 μs |
| SetHashLinkedAndArray |        0 |       10 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |      1.651 μs |   0.0346 μs |   0.0823 μs |      1.700 μs |
|  RelativelySimpleCode |        0 |       10 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |      4.007 μs |   0.0752 μs |   0.0704 μs |      4.000 μs |
|                 **Towel** |        **0** |       **10** | **1: sqrt(sqrt(range))** |       **3: sqrt(range)** |      **1.607 μs** |   **0.0341 μs** |   **0.0816 μs** |      **1.600 μs** |
|       HashSetAndArray |        0 |       10 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |      1.627 μs |   0.0344 μs |   0.0844 μs |      1.600 μs |
| SetHashLinkedAndArray |        0 |       10 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |      1.910 μs |   0.0391 μs |   0.0597 μs |      1.900 μs |
|  RelativelySimpleCode |        0 |       10 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |      3.929 μs |   0.0802 μs |   0.1150 μs |      3.900 μs |
|                 **Towel** |        **0** |       **10** | **1: sqrt(sqrt(range))** |     **4: 2*sqrt(range)** |      **2.615 μs** |   **0.0538 μs** |   **0.1226 μs** |      **2.600 μs** |
|       HashSetAndArray |        0 |       10 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |      1.646 μs |   0.0348 μs |   0.0846 μs |      1.600 μs |
| SetHashLinkedAndArray |        0 |       10 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |      2.090 μs |   0.0434 μs |   0.0857 μs |      2.100 μs |
|  RelativelySimpleCode |        0 |       10 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |      3.910 μs |   0.0747 μs |   0.0889 μs |      3.900 μs |
|                 **Towel** |        **0** |       **10** |    **2: .5*sqrt(range)** | **1: sqrt(sqrt(range))** |      **1.470 μs** |   **0.0273 μs** |   **0.0463 μs** |      **1.500 μs** |
|       HashSetAndArray |        0 |       10 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |      1.521 μs |   0.0323 μs |   0.0791 μs |      1.500 μs |
| SetHashLinkedAndArray |        0 |       10 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |      1.626 μs |   0.0343 μs |   0.0861 μs |      1.600 μs |
|  RelativelySimpleCode |        0 |       10 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |      3.962 μs |   0.0739 μs |   0.1083 μs |      3.900 μs |
|                 **Towel** |        **0** |       **10** |    **2: .5*sqrt(range)** |    **2: .5*sqrt(range)** |      **1.548 μs** |   **0.0326 μs** |   **0.0837 μs** |      **1.500 μs** |
|       HashSetAndArray |        0 |       10 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |      1.533 μs |   0.0326 μs |   0.0805 μs |      1.500 μs |
| SetHashLinkedAndArray |        0 |       10 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |      1.624 μs |   0.0343 μs |   0.0885 μs |      1.600 μs |
|  RelativelySimpleCode |        0 |       10 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |      4.043 μs |   0.0729 μs |   0.0646 μs |      4.000 μs |
|                 **Towel** |        **0** |       **10** |    **2: .5*sqrt(range)** |       **3: sqrt(range)** |      **1.647 μs** |   **0.0348 μs** |   **0.0847 μs** |      **1.600 μs** |
|       HashSetAndArray |        0 |       10 |    2: .5*sqrt(range) |       3: sqrt(range) |      1.659 μs |   0.0351 μs |   0.0740 μs |      1.700 μs |
| SetHashLinkedAndArray |        0 |       10 |    2: .5*sqrt(range) |       3: sqrt(range) |      1.854 μs |   0.0411 μs |   0.1097 μs |      1.900 μs |
|  RelativelySimpleCode |        0 |       10 |    2: .5*sqrt(range) |       3: sqrt(range) |      3.913 μs |   0.0795 μs |   0.0743 μs |      3.900 μs |
|                 **Towel** |        **0** |       **10** |    **2: .5*sqrt(range)** |     **4: 2*sqrt(range)** |      **2.542 μs** |   **0.0513 μs** |   **0.0703 μs** |      **2.500 μs** |
|       HashSetAndArray |        0 |       10 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |      1.667 μs |   0.0351 μs |   0.0764 μs |      1.700 μs |
| SetHashLinkedAndArray |        0 |       10 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |      1.962 μs |   0.0493 μs |   0.1300 μs |      1.900 μs |
|  RelativelySimpleCode |        0 |       10 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |      3.894 μs |   0.0589 μs |   0.0968 μs |      3.900 μs |
|                 **Towel** |        **0** |       **10** |       **3: sqrt(range)** | **1: sqrt(sqrt(range))** |      **1.578 μs** |   **0.0334 μs** |   **0.0712 μs** |      **1.600 μs** |
|       HashSetAndArray |        0 |       10 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |      1.576 μs |   0.0329 μs |   0.0756 μs |      1.600 μs |
| SetHashLinkedAndArray |        0 |       10 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |      1.675 μs |   0.0363 μs |   0.1023 μs |      1.700 μs |
|  RelativelySimpleCode |        0 |       10 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |      4.865 μs |   0.0875 μs |   0.1198 μs |      4.900 μs |
|                 **Towel** |        **0** |       **10** |       **3: sqrt(range)** |    **2: .5*sqrt(range)** |      **1.593 μs** |   **0.0338 μs** |   **0.0756 μs** |      **1.600 μs** |
|       HashSetAndArray |        0 |       10 |       3: sqrt(range) |    2: .5*sqrt(range) |      1.563 μs |   0.0330 μs |   0.0790 μs |      1.500 μs |
| SetHashLinkedAndArray |        0 |       10 |       3: sqrt(range) |    2: .5*sqrt(range) |      1.681 μs |   0.0383 μs |   0.1074 μs |      1.700 μs |
|  RelativelySimpleCode |        0 |       10 |       3: sqrt(range) |    2: .5*sqrt(range) |      4.966 μs |   0.1001 μs |   0.1644 μs |      4.900 μs |
|                 **Towel** |        **0** |       **10** |       **3: sqrt(range)** |       **3: sqrt(range)** |      **1.649 μs** |   **0.0349 μs** |   **0.0774 μs** |      **1.600 μs** |
|       HashSetAndArray |        0 |       10 |       3: sqrt(range) |       3: sqrt(range) |      1.633 μs |   0.0346 μs |   0.0774 μs |      1.600 μs |
| SetHashLinkedAndArray |        0 |       10 |       3: sqrt(range) |       3: sqrt(range) |      1.925 μs |   0.0398 μs |   0.0786 μs |      1.900 μs |
|  RelativelySimpleCode |        0 |       10 |       3: sqrt(range) |       3: sqrt(range) |      4.994 μs |   0.1011 μs |   0.1662 μs |      5.000 μs |
|                 **Towel** |        **0** |       **10** |       **3: sqrt(range)** |     **4: 2*sqrt(range)** |      **2.597 μs** |   **0.0539 μs** |   **0.0870 μs** |      **2.600 μs** |
|       HashSetAndArray |        0 |       10 |       3: sqrt(range) |     4: 2*sqrt(range) |      1.638 μs |   0.0344 μs |   0.0733 μs |      1.600 μs |
| SetHashLinkedAndArray |        0 |       10 |       3: sqrt(range) |     4: 2*sqrt(range) |      2.118 μs |   0.0439 μs |   0.0992 μs |      2.100 μs |
|  RelativelySimpleCode |        0 |       10 |       3: sqrt(range) |     4: 2*sqrt(range) |      5.159 μs |   0.1035 μs |   0.1672 μs |      5.150 μs |
|                 **Towel** |        **0** |       **10** |     **4: 2*sqrt(range)** | **1: sqrt(sqrt(range))** |      **1.633 μs** |   **0.0346 μs** |   **0.0774 μs** |      **1.600 μs** |
|       HashSetAndArray |        0 |       10 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |      1.602 μs |   0.0340 μs |   0.0754 μs |      1.600 μs |
| SetHashLinkedAndArray |        0 |       10 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |      1.724 μs |   0.0362 μs |   0.0824 μs |      1.700 μs |
|  RelativelySimpleCode |        0 |       10 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |      5.854 μs |   0.1178 μs |   0.2063 μs |      5.800 μs |
|                 **Towel** |        **0** |       **10** |     **4: 2*sqrt(range)** |    **2: .5*sqrt(range)** |      **1.608 μs** |   **0.0341 μs** |   **0.0879 μs** |      **1.600 μs** |
|       HashSetAndArray |        0 |       10 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |      1.616 μs |   0.0343 μs |   0.0787 μs |      1.600 μs |
| SetHashLinkedAndArray |        0 |       10 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |      1.712 μs |   0.0404 μs |   0.1153 μs |      1.700 μs |
|  RelativelySimpleCode |        0 |       10 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |      5.870 μs |   0.1193 μs |   0.2410 μs |      5.900 μs |
|                 **Towel** |        **0** |       **10** |     **4: 2*sqrt(range)** |       **3: sqrt(range)** |      **2.550 μs** |   **0.0527 μs** |   **0.0607 μs** |      **2.500 μs** |
|       HashSetAndArray |        0 |       10 |     4: 2*sqrt(range) |       3: sqrt(range) |      1.688 μs |   0.0356 μs |   0.0790 μs |      1.700 μs |
| SetHashLinkedAndArray |        0 |       10 |     4: 2*sqrt(range) |       3: sqrt(range) |      1.977 μs |   0.0415 μs |   0.0927 μs |      2.000 μs |
|  RelativelySimpleCode |        0 |       10 |     4: 2*sqrt(range) |       3: sqrt(range) |      6.125 μs |   0.1335 μs |   0.3787 μs |      6.100 μs |
|                 **Towel** |        **0** |       **10** |     **4: 2*sqrt(range)** |     **4: 2*sqrt(range)** |      **2.681 μs** |   **0.0553 μs** |   **0.1202 μs** |      **2.700 μs** |
|       HashSetAndArray |        0 |       10 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |      1.693 μs |   0.0354 μs |   0.0940 μs |      1.700 μs |
| SetHashLinkedAndArray |        0 |       10 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |      2.170 μs |   0.0442 μs |   0.0559 μs |      2.200 μs |
|  RelativelySimpleCode |        0 |       10 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |      6.159 μs |   0.1245 μs |   0.2114 μs |      6.200 μs |
|                 **Towel** |        **0** |      **100** | **1: sqrt(sqrt(range))** | **1: sqrt(sqrt(range))** |      **1.672 μs** |   **0.0343 μs** |   **0.0458 μs** |      **1.700 μs** |
|       HashSetAndArray |        0 |      100 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |      2.123 μs |   0.0444 μs |   0.0865 μs |      2.100 μs |
| SetHashLinkedAndArray |        0 |      100 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |      2.683 μs |   0.0499 μs |   0.0389 μs |      2.700 μs |
|  RelativelySimpleCode |        0 |      100 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |      9.940 μs |   0.1960 μs |   0.2257 μs |      9.900 μs |
|                 **Towel** |        **0** |      **100** | **1: sqrt(sqrt(range))** |    **2: .5*sqrt(range)** |      **1.755 μs** |   **0.0371 μs** |   **0.0774 μs** |      **1.800 μs** |
|       HashSetAndArray |        0 |      100 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |      2.296 μs |   0.0477 μs |   0.1273 μs |      2.300 μs |
| SetHashLinkedAndArray |        0 |      100 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |      3.337 μs |   0.0677 μs |   0.1254 μs |      3.300 μs |
|  RelativelySimpleCode |        0 |      100 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |     11.620 μs |   0.2282 μs |   0.2628 μs |     11.600 μs |
|                 **Towel** |        **0** |      **100** | **1: sqrt(sqrt(range))** |       **3: sqrt(range)** |      **1.839 μs** |   **0.0384 μs** |   **0.0875 μs** |      **1.800 μs** |
|       HashSetAndArray |        0 |      100 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |      2.590 μs |   0.0536 μs |   0.1383 μs |      2.600 μs |
| SetHashLinkedAndArray |        0 |      100 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |      3.537 μs |   0.0739 μs |   0.1215 μs |      3.500 μs |
|  RelativelySimpleCode |        0 |      100 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |     12.558 μs |   0.2515 μs |   0.2795 μs |     12.600 μs |
|                 **Towel** |        **0** |      **100** | **1: sqrt(sqrt(range))** |     **4: 2*sqrt(range)** |      **4.537 μs** |   **0.0910 μs** |   **0.1012 μs** |      **4.500 μs** |
|       HashSetAndArray |        0 |      100 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |      2.800 μs |   0.0579 μs |   0.1270 μs |      2.800 μs |
| SetHashLinkedAndArray |        0 |      100 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |      4.086 μs |   0.0821 μs |   0.1177 μs |      4.100 μs |
|  RelativelySimpleCode |        0 |      100 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |     12.958 μs |   0.2585 μs |   0.2874 μs |     13.000 μs |
|                 **Towel** |        **0** |      **100** |    **2: .5*sqrt(range)** | **1: sqrt(sqrt(range))** |      **1.790 μs** |   **0.0378 μs** |   **0.0763 μs** |      **1.800 μs** |
|       HashSetAndArray |        0 |      100 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |      2.163 μs |   0.0449 μs |   0.0629 μs |      2.200 μs |
| SetHashLinkedAndArray |        0 |      100 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |      2.700 μs |   0.0510 μs |   0.0906 μs |      2.700 μs |
|  RelativelySimpleCode |        0 |      100 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |     14.581 μs |   0.2832 μs |   0.3371 μs |     14.600 μs |
|                 **Towel** |        **0** |      **100** |    **2: .5*sqrt(range)** |    **2: .5*sqrt(range)** |      **1.781 μs** |   **0.0367 μs** |   **0.0671 μs** |      **1.800 μs** |
|       HashSetAndArray |        0 |      100 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |      2.370 μs |   0.0493 μs |   0.1255 μs |      2.400 μs |
| SetHashLinkedAndArray |        0 |      100 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |      3.287 μs |   0.0673 μs |   0.1008 μs |      3.300 μs |
|  RelativelySimpleCode |        0 |      100 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |     16.731 μs |   0.3277 μs |   0.4804 μs |     16.900 μs |
|                 **Towel** |        **0** |      **100** |    **2: .5*sqrt(range)** |       **3: sqrt(range)** |      **1.897 μs** |   **0.0393 μs** |   **0.0870 μs** |      **1.900 μs** |
|       HashSetAndArray |        0 |      100 |    2: .5*sqrt(range) |       3: sqrt(range) |      2.690 μs |   0.0555 μs |   0.1471 μs |      2.700 μs |
| SetHashLinkedAndArray |        0 |      100 |    2: .5*sqrt(range) |       3: sqrt(range) |      3.705 μs |   0.0744 μs |   0.1397 μs |      3.700 μs |
|  RelativelySimpleCode |        0 |      100 |    2: .5*sqrt(range) |       3: sqrt(range) |     16.888 μs |   0.3303 μs |   0.4409 μs |     17.000 μs |
|                 **Towel** |        **0** |      **100** |    **2: .5*sqrt(range)** |     **4: 2*sqrt(range)** |      **4.577 μs** |   **0.0831 μs** |   **0.1020 μs** |      **4.600 μs** |
|       HashSetAndArray |        0 |      100 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |      2.749 μs |   0.0567 μs |   0.1079 μs |      2.700 μs |
| SetHashLinkedAndArray |        0 |      100 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |      4.050 μs |   0.0828 μs |   0.1384 μs |      4.050 μs |
|  RelativelySimpleCode |        0 |      100 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |     17.509 μs |   0.3518 μs |   0.5476 μs |     17.550 μs |
|                 **Towel** |        **0** |      **100** |       **3: sqrt(range)** | **1: sqrt(sqrt(range))** |      **1.822 μs** |   **0.0381 μs** |   **0.0912 μs** |      **1.800 μs** |
|       HashSetAndArray |        0 |      100 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |      2.221 μs |   0.0462 μs |   0.1225 μs |      2.200 μs |
| SetHashLinkedAndArray |        0 |      100 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |      2.733 μs |   0.0553 μs |   0.0658 μs |      2.700 μs |
|  RelativelySimpleCode |        0 |      100 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |     26.680 μs |   0.5287 μs |   0.4945 μs |     26.600 μs |
|                 **Towel** |        **0** |      **100** |       **3: sqrt(range)** |    **2: .5*sqrt(range)** |      **1.838 μs** |   **0.0383 μs** |   **0.0561 μs** |      **1.800 μs** |
|       HashSetAndArray |        0 |      100 |       3: sqrt(range) |    2: .5*sqrt(range) |      2.431 μs |   0.0505 μs |   0.1302 μs |      2.400 μs |
| SetHashLinkedAndArray |        0 |      100 |       3: sqrt(range) |    2: .5*sqrt(range) |      3.421 μs |   0.0653 μs |   0.0579 μs |      3.400 μs |
|  RelativelySimpleCode |        0 |      100 |       3: sqrt(range) |    2: .5*sqrt(range) |     27.511 μs |   0.5406 μs |   0.6008 μs |     27.400 μs |
|                 **Towel** |        **0** |      **100** |       **3: sqrt(range)** |       **3: sqrt(range)** |      **4.223 μs** |   **0.0857 μs** |   **0.1671 μs** |      **4.200 μs** |
|       HashSetAndArray |        0 |      100 |       3: sqrt(range) |       3: sqrt(range) |      2.649 μs |   0.0546 μs |   0.1530 μs |      2.600 μs |
| SetHashLinkedAndArray |        0 |      100 |       3: sqrt(range) |       3: sqrt(range) |      3.676 μs |   0.0744 μs |   0.1283 μs |      3.700 μs |
|  RelativelySimpleCode |        0 |      100 |       3: sqrt(range) |       3: sqrt(range) |     27.773 μs |   0.5517 μs |   0.8751 μs |     27.900 μs |
|                 **Towel** |        **0** |      **100** |       **3: sqrt(range)** |     **4: 2*sqrt(range)** |      **4.694 μs** |   **0.0816 μs** |   **0.0873 μs** |      **4.700 μs** |
|       HashSetAndArray |        0 |      100 |       3: sqrt(range) |     4: 2*sqrt(range) |      2.960 μs |   0.0598 μs |   0.0894 μs |      3.000 μs |
| SetHashLinkedAndArray |        0 |      100 |       3: sqrt(range) |     4: 2*sqrt(range) |      4.162 μs |   0.0824 μs |   0.1208 μs |      4.200 μs |
|  RelativelySimpleCode |        0 |      100 |       3: sqrt(range) |     4: 2*sqrt(range) |     29.747 μs |   0.5967 μs |   1.2971 μs |     29.800 μs |
|                 **Towel** |        **0** |      **100** |     **4: 2*sqrt(range)** | **1: sqrt(sqrt(range))** |      **1.906 μs** |   **0.0420 μs** |   **0.0858 μs** |      **1.900 μs** |
|       HashSetAndArray |        0 |      100 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |      2.352 μs |   0.0441 μs |   0.1066 μs |      2.300 μs |
| SetHashLinkedAndArray |        0 |      100 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |      2.864 μs |   0.0592 μs |   0.0727 μs |      2.900 μs |
|  RelativelySimpleCode |        0 |      100 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |     47.938 μs |   0.5713 μs |   0.4770 μs |     47.800 μs |
|                 **Towel** |        **0** |      **100** |     **4: 2*sqrt(range)** |    **2: .5*sqrt(range)** |      **2.077 μs** |   **0.0428 μs** |   **0.0803 μs** |      **2.100 μs** |
|       HashSetAndArray |        0 |      100 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |      2.576 μs |   0.0601 μs |   0.1714 μs |      2.500 μs |
| SetHashLinkedAndArray |        0 |      100 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |      3.541 μs |   0.0715 μs |   0.1428 μs |      3.500 μs |
|  RelativelySimpleCode |        0 |      100 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |     48.746 μs |   0.7343 μs |   0.6132 μs |     48.700 μs |
|                 **Towel** |        **0** |      **100** |     **4: 2*sqrt(range)** |       **3: sqrt(range)** |      **4.329 μs** |   **0.0827 μs** |   **0.0849 μs** |      **4.300 μs** |
|       HashSetAndArray |        0 |      100 |     4: 2*sqrt(range) |       3: sqrt(range) |      2.840 μs |   0.0581 μs |   0.1033 μs |      2.800 μs |
| SetHashLinkedAndArray |        0 |      100 |     4: 2*sqrt(range) |       3: sqrt(range) |      3.913 μs |   0.0793 μs |   0.1566 μs |      3.900 μs |
|  RelativelySimpleCode |        0 |      100 |     4: 2*sqrt(range) |       3: sqrt(range) |     49.147 μs |   0.8104 μs |   0.8323 μs |     49.300 μs |
|                 **Towel** |        **0** |      **100** |     **4: 2*sqrt(range)** |     **4: 2*sqrt(range)** |      **4.846 μs** |   **0.0930 μs** |   **0.0776 μs** |      **4.800 μs** |
|       HashSetAndArray |        0 |      100 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |      2.933 μs |   0.0580 μs |   0.1117 μs |      2.900 μs |
| SetHashLinkedAndArray |        0 |      100 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |      4.279 μs |   0.0863 μs |   0.1264 μs |      4.300 μs |
|  RelativelySimpleCode |        0 |      100 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |     51.647 μs |   1.0190 μs |   1.9388 μs |     51.600 μs |
|                 **Towel** |        **0** |     **1000** | **1: sqrt(sqrt(range))** | **1: sqrt(sqrt(range))** |      **1.784 μs** |   **0.0337 μs** |   **0.0375 μs** |      **1.800 μs** |
|       HashSetAndArray |        0 |     1000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |      6.553 μs |   0.1294 μs |   0.1328 μs |      6.500 μs |
| SetHashLinkedAndArray |        0 |     1000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |      9.385 μs |   0.0959 μs |   0.0801 μs |      9.400 μs |
|  RelativelySimpleCode |        0 |     1000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |    114.259 μs |   2.1724 μs |   2.6679 μs |    113.350 μs |
|                 **Towel** |        **0** |     **1000** | **1: sqrt(sqrt(range))** |    **2: .5*sqrt(range)** |      **2.027 μs** |   **0.0425 μs** |   **0.0809 μs** |      **2.000 μs** |
|       HashSetAndArray |        0 |     1000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |      7.964 μs |   0.1602 μs |   0.2139 μs |      8.000 μs |
| SetHashLinkedAndArray |        0 |     1000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |     10.410 μs |   0.1950 μs |   0.2245 μs |     10.400 μs |
|  RelativelySimpleCode |        0 |     1000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |    109.154 μs |   1.9796 μs |   1.6531 μs |    108.600 μs |
|                 **Towel** |        **0** |     **1000** | **1: sqrt(sqrt(range))** |       **3: sqrt(range)** |      **2.453 μs** |   **0.0500 μs** |   **0.0862 μs** |      **2.500 μs** |
|       HashSetAndArray |        0 |     1000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |      8.692 μs |   0.1782 μs |   0.4939 μs |      8.800 μs |
| SetHashLinkedAndArray |        0 |     1000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |     12.588 μs |   0.2496 μs |   0.4565 μs |     12.700 μs |
|  RelativelySimpleCode |        0 |     1000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |     90.341 μs |   1.7740 μs |   3.5428 μs |     89.400 μs |
|                 **Towel** |        **0** |     **1000** | **1: sqrt(sqrt(range))** |     **4: 2*sqrt(range)** |     **16.124 μs** |   **0.3080 μs** |   **0.7077 μs** |     **16.300 μs** |
|       HashSetAndArray |        0 |     1000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |     11.702 μs |   0.3284 μs |   0.9210 μs |     11.800 μs |
| SetHashLinkedAndArray |        0 |     1000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |     15.493 μs |   0.3109 μs |   0.6558 μs |     15.700 μs |
|  RelativelySimpleCode |        0 |     1000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |    111.869 μs |   1.4531 μs |   1.2134 μs |    112.000 μs |
|                 **Towel** |        **0** |     **1000** |    **2: .5*sqrt(range)** | **1: sqrt(sqrt(range))** |      **1.984 μs** |   **0.0413 μs** |   **0.0776 μs** |      **2.000 μs** |
|       HashSetAndArray |        0 |     1000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |      6.884 μs |   0.1133 μs |   0.1259 μs |      6.900 μs |
| SetHashLinkedAndArray |        0 |     1000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |      9.247 μs |   0.1858 μs |   0.1908 μs |      9.200 μs |
|  RelativelySimpleCode |        0 |     1000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |    302.858 μs |   3.4720 μs |   2.8993 μs |    301.550 μs |
|                 **Towel** |        **0** |     **1000** |    **2: .5*sqrt(range)** |    **2: .5*sqrt(range)** |      **2.226 μs** |   **0.0464 μs** |   **0.1182 μs** |      **2.200 μs** |
|       HashSetAndArray |        0 |     1000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |      8.023 μs |   0.1592 μs |   0.2432 μs |      8.000 μs |
| SetHashLinkedAndArray |        0 |     1000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |     10.650 μs |   0.2130 μs |   0.2915 μs |     10.700 μs |
|  RelativelySimpleCode |        0 |     1000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |    312.349 μs |   6.1923 μs |  11.7815 μs |    309.200 μs |
|                 **Towel** |        **0** |     **1000** |    **2: .5*sqrt(range)** |       **3: sqrt(range)** |      **2.835 μs** |   **0.0582 μs** |   **0.0797 μs** |      **2.800 μs** |
|       HashSetAndArray |        0 |     1000 |    2: .5*sqrt(range) |       3: sqrt(range) |      8.900 μs |   0.1802 μs |   0.5082 μs |      8.950 μs |
| SetHashLinkedAndArray |        0 |     1000 |    2: .5*sqrt(range) |       3: sqrt(range) |     12.812 μs |   0.2579 μs |   0.5606 μs |     12.900 μs |
|  RelativelySimpleCode |        0 |     1000 |    2: .5*sqrt(range) |       3: sqrt(range) |    328.964 μs |   4.9172 μs |   4.3590 μs |    328.250 μs |
|                 **Towel** |        **0** |     **1000** |    **2: .5*sqrt(range)** |     **4: 2*sqrt(range)** |     **16.655 μs** |   **0.3179 μs** |   **0.7368 μs** |     **16.650 μs** |
|       HashSetAndArray |        0 |     1000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |     11.703 μs |   0.3102 μs |   0.9096 μs |     11.900 μs |
| SetHashLinkedAndArray |        0 |     1000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |     15.604 μs |   0.3086 μs |   0.6091 μs |     15.700 μs |
|  RelativelySimpleCode |        0 |     1000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |    351.521 μs |   6.7344 μs |   7.4853 μs |    351.100 μs |
|                 **Towel** |        **0** |     **1000** |       **3: sqrt(range)** | **1: sqrt(sqrt(range))** |      **2.274 μs** |   **0.0407 μs** |   **0.0452 μs** |      **2.300 μs** |
|       HashSetAndArray |        0 |     1000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |      6.983 μs |   0.1406 μs |   0.1505 μs |      6.900 μs |
| SetHashLinkedAndArray |        0 |     1000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |      9.662 μs |   0.1657 μs |   0.1628 μs |      9.700 μs |
|  RelativelySimpleCode |        0 |     1000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |    581.576 μs |   9.0128 μs |   9.2555 μs |    577.300 μs |
|                 **Towel** |        **0** |     **1000** |       **3: sqrt(range)** |    **2: .5*sqrt(range)** |      **2.518 μs** |   **0.0538 μs** |   **0.1074 μs** |      **2.500 μs** |
|       HashSetAndArray |        0 |     1000 |       3: sqrt(range) |    2: .5*sqrt(range) |      7.974 μs |   0.1604 μs |   0.2449 μs |      8.000 μs |
| SetHashLinkedAndArray |        0 |     1000 |       3: sqrt(range) |    2: .5*sqrt(range) |     10.941 μs |   0.2130 μs |   0.3123 μs |     11.000 μs |
|  RelativelySimpleCode |        0 |     1000 |       3: sqrt(range) |    2: .5*sqrt(range) |    659.794 μs |  12.5001 μs |  25.2508 μs |    655.500 μs |
|                 **Towel** |        **0** |     **1000** |       **3: sqrt(range)** |       **3: sqrt(range)** |     **13.857 μs** |   **0.2788 μs** |   **0.4735 μs** |     **14.000 μs** |
|       HashSetAndArray |        0 |     1000 |       3: sqrt(range) |       3: sqrt(range) |      8.944 μs |   0.1807 μs |   0.5007 μs |      9.000 μs |
| SetHashLinkedAndArray |        0 |     1000 |       3: sqrt(range) |       3: sqrt(range) |     12.694 μs |   0.2517 μs |   0.5085 μs |     12.800 μs |
|  RelativelySimpleCode |        0 |     1000 |       3: sqrt(range) |       3: sqrt(range) |    717.292 μs |  10.2793 μs |   8.5837 μs |    714.200 μs |
|                 **Towel** |        **0** |     **1000** |       **3: sqrt(range)** |     **4: 2*sqrt(range)** |     **17.241 μs** |   **0.3120 μs** |   **0.6372 μs** |     **17.400 μs** |
|       HashSetAndArray |        0 |     1000 |       3: sqrt(range) |     4: 2*sqrt(range) |     11.988 μs |   0.2396 μs |   0.6520 μs |     12.200 μs |
| SetHashLinkedAndArray |        0 |     1000 |       3: sqrt(range) |     4: 2*sqrt(range) |     15.623 μs |   0.2951 μs |   0.7183 μs |     15.800 μs |
|  RelativelySimpleCode |        0 |     1000 |       3: sqrt(range) |     4: 2*sqrt(range) |    738.064 μs |  13.2229 μs |  24.8358 μs |    725.400 μs |
|                 **Towel** |        **0** |     **1000** |     **4: 2*sqrt(range)** | **1: sqrt(sqrt(range))** |      **2.914 μs** |   **0.0591 μs** |   **0.0848 μs** |      **2.900 μs** |
|       HashSetAndArray |        0 |     1000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |      7.271 μs |   0.1359 μs |   0.1204 μs |      7.250 μs |
| SetHashLinkedAndArray |        0 |     1000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |     10.063 μs |   0.1971 μs |   0.2191 μs |     10.100 μs |
|  RelativelySimpleCode |        0 |     1000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |  1,210.574 μs |  23.7352 μs |  36.2462 μs |  1,195.600 μs |
|                 **Towel** |        **0** |     **1000** |     **4: 2*sqrt(range)** |    **2: .5*sqrt(range)** |      **3.335 μs** |   **0.0683 μs** |   **0.0702 μs** |      **3.300 μs** |
|       HashSetAndArray |        0 |     1000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |      8.437 μs |   0.1625 μs |   0.2804 μs |      8.400 μs |
| SetHashLinkedAndArray |        0 |     1000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |     11.017 μs |   0.2206 μs |   0.2790 μs |     11.100 μs |
|  RelativelySimpleCode |        0 |     1000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |  1,318.465 μs |  23.3225 μs |  19.4753 μs |  1,314.350 μs |
|                 **Towel** |        **0** |     **1000** |     **4: 2*sqrt(range)** |       **3: sqrt(range)** |     **14.496 μs** |   **0.2766 μs** |   **0.5650 μs** |     **14.500 μs** |
|       HashSetAndArray |        0 |     1000 |     4: 2*sqrt(range) |       3: sqrt(range) |      9.282 μs |   0.1866 μs |   0.3977 μs |      9.300 μs |
| SetHashLinkedAndArray |        0 |     1000 |     4: 2*sqrt(range) |       3: sqrt(range) |     13.189 μs |   0.2636 μs |   0.4330 μs |     13.200 μs |
|  RelativelySimpleCode |        0 |     1000 |     4: 2*sqrt(range) |       3: sqrt(range) |  1,379.938 μs |  26.1933 μs |  21.8726 μs |  1,376.600 μs |
|                 **Towel** |        **0** |     **1000** |     **4: 2*sqrt(range)** |     **4: 2*sqrt(range)** |     **16.756 μs** |   **0.3355 μs** |   **0.6219 μs** |     **16.800 μs** |
|       HashSetAndArray |        0 |     1000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |     11.804 μs |   0.3052 μs |   0.8609 μs |     11.900 μs |
| SetHashLinkedAndArray |        0 |     1000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |     16.216 μs |   0.3239 μs |   0.6084 μs |     16.200 μs |
|  RelativelySimpleCode |        0 |     1000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |  1,499.128 μs |  27.6820 μs |  36.9546 μs |  1,490.900 μs |
|                 **Towel** |        **0** |    **10000** | **1: sqrt(sqrt(range))** | **1: sqrt(sqrt(range))** |      **1.969 μs** |   **0.0412 μs** |   **0.0676 μs** |      **2.000 μs** |
|       HashSetAndArray |        0 |    10000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |     54.008 μs |   0.7008 μs |   0.5852 μs |     53.900 μs |
| SetHashLinkedAndArray |        0 |    10000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |     71.971 μs |   0.1793 μs |   0.1590 μs |     71.950 μs |
|  RelativelySimpleCode |        0 |    10000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** | **1: sqrt(sqrt(range))** |    **2: .5*sqrt(range)** |      **3.500 μs** |   **0.0716 μs** |   **0.0980 μs** |      **3.500 μs** |
|       HashSetAndArray |        0 |    10000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |     56.011 μs |   0.8702 μs |   0.9672 μs |     56.200 μs |
| SetHashLinkedAndArray |        0 |    10000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |     89.436 μs |   1.2574 μs |   1.1147 μs |     89.150 μs |
|  RelativelySimpleCode |        0 |    10000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** | **1: sqrt(sqrt(range))** |       **3: sqrt(range)** |      **6.514 μs** |   **0.1317 μs** |   **0.1167 μs** |      **6.550 μs** |
|       HashSetAndArray |        0 |    10000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |     62.987 μs |   0.9286 μs |   1.6507 μs |     63.050 μs |
| SetHashLinkedAndArray |        0 |    10000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |     87.106 μs |   1.7024 μs |   1.8216 μs |     86.750 μs |
|  RelativelySimpleCode |        0 |    10000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** | **1: sqrt(sqrt(range))** |     **4: 2*sqrt(range)** |    **110.796 μs** |   **2.1637 μs** |   **2.7364 μs** |    **110.500 μs** |
|       HashSetAndArray |        0 |    10000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |     72.930 μs |   1.1850 μs |   2.0123 μs |     72.600 μs |
| SetHashLinkedAndArray |        0 |    10000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |    101.566 μs |   1.3163 μs |   3.3741 μs |    101.200 μs |
|  RelativelySimpleCode |        0 |    10000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** |    **2: .5*sqrt(range)** | **1: sqrt(sqrt(range))** |      **2.859 μs** |   **0.0591 μs** |   **0.0867 μs** |      **2.800 μs** |
|       HashSetAndArray |        0 |    10000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |     55.077 μs |   1.0105 μs |   1.8730 μs |     54.500 μs |
| SetHashLinkedAndArray |        0 |    10000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |     74.727 μs |   1.6507 μs |   4.4629 μs |     72.600 μs |
|  RelativelySimpleCode |        0 |    10000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** |    **2: .5*sqrt(range)** |    **2: .5*sqrt(range)** |      **5.021 μs** |   **0.1007 μs** |   **0.0893 μs** |      **5.000 μs** |
|       HashSetAndArray |        0 |    10000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |     58.995 μs |   1.6685 μs |   4.7060 μs |     56.850 μs |
| SetHashLinkedAndArray |        0 |    10000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |     85.283 μs |   1.3398 μs |   1.0461 μs |     85.200 μs |
|  RelativelySimpleCode |        0 |    10000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** |    **2: .5*sqrt(range)** |       **3: sqrt(range)** |      **9.221 μs** |   **0.0653 μs** |   **0.0579 μs** |      **9.200 μs** |
|       HashSetAndArray |        0 |    10000 |    2: .5*sqrt(range) |       3: sqrt(range) |     74.186 μs |   6.1819 μs |  18.0328 μs |     64.450 μs |
| SetHashLinkedAndArray |        0 |    10000 |    2: .5*sqrt(range) |       3: sqrt(range) |     90.242 μs |   1.5021 μs |   1.1728 μs |     90.450 μs |
|  RelativelySimpleCode |        0 |    10000 |    2: .5*sqrt(range) |       3: sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** |    **2: .5*sqrt(range)** |     **4: 2*sqrt(range)** |    **102.711 μs** |   **1.9913 μs** |   **2.7915 μs** |    **101.700 μs** |
|       HashSetAndArray |        0 |    10000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |     70.695 μs |   1.3816 μs |   1.5357 μs |     70.500 μs |
| SetHashLinkedAndArray |        0 |    10000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |     98.805 μs |   1.9301 μs |   3.3293 μs |     98.450 μs |
|  RelativelySimpleCode |        0 |    10000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** |       **3: sqrt(range)** | **1: sqrt(sqrt(range))** |      **3.900 μs** |   **0.0791 μs** |   **0.1109 μs** |      **3.900 μs** |
|       HashSetAndArray |        0 |    10000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |     54.492 μs |   0.4752 μs |   0.3968 μs |     54.400 μs |
| SetHashLinkedAndArray |        0 |    10000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |     73.117 μs |   1.2865 μs |   1.0044 μs |     72.450 μs |
|  RelativelySimpleCode |        0 |    10000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** |       **3: sqrt(range)** |    **2: .5*sqrt(range)** |      **7.160 μs** |   **0.0788 μs** |   **0.0737 μs** |      **7.200 μs** |
|       HashSetAndArray |        0 |    10000 |       3: sqrt(range) |    2: .5*sqrt(range) |     57.542 μs |   0.6708 μs |   0.7456 μs |     57.500 μs |
| SetHashLinkedAndArray |        0 |    10000 |       3: sqrt(range) |    2: .5*sqrt(range) |     90.667 μs |   0.8624 μs |   0.6733 μs |     90.750 μs |
|  RelativelySimpleCode |        0 |    10000 |       3: sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** |       **3: sqrt(range)** |       **3: sqrt(range)** |     **95.725 μs** |   **1.6045 μs** |   **2.0862 μs** |     **95.400 μs** |
|       HashSetAndArray |        0 |    10000 |       3: sqrt(range) |       3: sqrt(range) |     61.793 μs |   1.2139 μs |   1.0761 μs |     61.700 μs |
| SetHashLinkedAndArray |        0 |    10000 |       3: sqrt(range) |       3: sqrt(range) |     86.957 μs |   1.3686 μs |   1.2132 μs |     86.700 μs |
|  RelativelySimpleCode |        0 |    10000 |       3: sqrt(range) |       3: sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** |       **3: sqrt(range)** |     **4: 2*sqrt(range)** |    **110.947 μs** |   **2.0774 μs** |   **3.8506 μs** |    **111.600 μs** |
|       HashSetAndArray |        0 |    10000 |       3: sqrt(range) |     4: 2*sqrt(range) |     71.600 μs |   1.3954 μs |   2.4070 μs |     71.500 μs |
| SetHashLinkedAndArray |        0 |    10000 |       3: sqrt(range) |     4: 2*sqrt(range) |    138.058 μs |   2.2533 μs |   4.2872 μs |    137.400 μs |
|  RelativelySimpleCode |        0 |    10000 |       3: sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** |     **4: 2*sqrt(range)** | **1: sqrt(sqrt(range))** |      **5.753 μs** |   **0.1095 μs** |   **0.1125 μs** |      **5.700 μs** |
|       HashSetAndArray |        0 |    10000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |     56.539 μs |   1.1253 μs |   2.3736 μs |     55.800 μs |
| SetHashLinkedAndArray |        0 |    10000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |     74.100 μs |   1.4494 μs |   1.2848 μs |     73.250 μs |
|  RelativelySimpleCode |        0 |    10000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** |     **4: 2*sqrt(range)** |    **2: .5*sqrt(range)** |    **100.554 μs** |   **1.9142 μs** |   **2.7453 μs** |     **99.650 μs** |
|       HashSetAndArray |        0 |    10000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |     62.348 μs |   2.9656 μs |   8.1182 μs |     58.300 μs |
| SetHashLinkedAndArray |        0 |    10000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |     89.950 μs |   1.7692 μs |   2.1727 μs |     89.450 μs |
|  RelativelySimpleCode |        0 |    10000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** |     **4: 2*sqrt(range)** |       **3: sqrt(range)** |     **96.489 μs** |   **1.9231 μs** |   **3.1597 μs** |     **96.000 μs** |
|       HashSetAndArray |        0 |    10000 |     4: 2*sqrt(range) |       3: sqrt(range) |     65.987 μs |   1.3176 μs |   1.2940 μs |     66.100 μs |
| SetHashLinkedAndArray |        0 |    10000 |     4: 2*sqrt(range) |       3: sqrt(range) |     87.847 μs |   1.5177 μs |   1.4197 μs |     88.100 μs |
|  RelativelySimpleCode |        0 |    10000 |     4: 2*sqrt(range) |       3: sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** |     **4: 2*sqrt(range)** |     **4: 2*sqrt(range)** |    **105.303 μs** |   **2.0980 μs** |   **3.6189 μs** |    **104.100 μs** |
|       HashSetAndArray |        0 |    10000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |     73.076 μs |   1.4593 μs |   1.4986 μs |     73.400 μs |
| SetHashLinkedAndArray |        0 |    10000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |    103.573 μs |   2.0080 μs |   1.8782 μs |    104.300 μs |
|  RelativelySimpleCode |        0 |    10000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** | **1: sqrt(sqrt(range))** | **1: sqrt(sqrt(range))** |      **2.362 μs** |   **0.0488 μs** |   **0.0828 μs** |      **2.400 μs** |
|       HashSetAndArray |        0 |   100000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |    524.359 μs |  10.1951 μs |  10.4697 μs |    520.000 μs |
| SetHashLinkedAndArray |        0 |   100000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |    681.058 μs |   9.7626 μs |   7.6220 μs |    679.800 μs |
|  RelativelySimpleCode |        0 |   100000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** | **1: sqrt(sqrt(range))** |    **2: .5*sqrt(range)** |     **13.627 μs** |   **0.1028 μs** |   **0.0961 μs** |     **13.600 μs** |
|       HashSetAndArray |        0 |   100000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |    534.438 μs |  10.3666 μs |  10.1813 μs |    531.500 μs |
| SetHashLinkedAndArray |        0 |   100000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |    767.654 μs |  10.1013 μs |   8.4350 μs |    768.800 μs |
|  RelativelySimpleCode |        0 |   100000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** | **1: sqrt(sqrt(range))** |       **3: sqrt(range)** |     **39.886 μs** |   **0.2686 μs** |   **0.2381 μs** |     **39.900 μs** |
|       HashSetAndArray |        0 |   100000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |    548.763 μs |   9.5315 μs |  17.1873 μs |    544.200 μs |
| SetHashLinkedAndArray |        0 |   100000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |    876.885 μs |  17.2499 μs |  24.1819 μs |    867.900 μs |
|  RelativelySimpleCode |        0 |   100000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** | **1: sqrt(sqrt(range))** |     **4: 2*sqrt(range)** |  **1,175.669 μs** |  **21.8461 μs** |  **21.4557 μs** |  **1,175.550 μs** |
|       HashSetAndArray |        0 |   100000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |    683.789 μs |  12.8770 μs |  22.2122 μs |    678.000 μs |
| SetHashLinkedAndArray |        0 |   100000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |  1,406.233 μs |  19.9076 μs |  15.5425 μs |  1,401.500 μs |
|  RelativelySimpleCode |        0 |   100000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** |    **2: .5*sqrt(range)** | **1: sqrt(sqrt(range))** |      **5.746 μs** |   **0.1159 μs** |   **0.0967 μs** |      **5.700 μs** |
|       HashSetAndArray |        0 |   100000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |    523.719 μs |  10.0685 μs |   9.8886 μs |    523.900 μs |
| SetHashLinkedAndArray |        0 |   100000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |    693.977 μs |   7.2968 μs |   6.0932 μs |    691.800 μs |
|  RelativelySimpleCode |        0 |   100000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** |    **2: .5*sqrt(range)** |    **2: .5*sqrt(range)** |     **25.157 μs** |   **0.3243 μs** |   **0.2875 μs** |     **25.300 μs** |
|       HashSetAndArray |        0 |   100000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |    539.986 μs |  10.2455 μs |   9.0823 μs |    537.100 μs |
| SetHashLinkedAndArray |        0 |   100000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |    790.953 μs |  15.4098 μs |  14.4144 μs |    789.700 μs |
|  RelativelySimpleCode |        0 |   100000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** |    **2: .5*sqrt(range)** |       **3: sqrt(range)** |     **62.500 μs** |   **0.8174 μs** |   **0.6382 μs** |     **62.700 μs** |
|       HashSetAndArray |        0 |   100000 |    2: .5*sqrt(range) |       3: sqrt(range) |    557.080 μs |  10.5508 μs |   9.8692 μs |    558.400 μs |
| SetHashLinkedAndArray |        0 |   100000 |    2: .5*sqrt(range) |       3: sqrt(range) |    853.123 μs |   9.9619 μs |   8.3187 μs |    849.100 μs |
|  RelativelySimpleCode |        0 |   100000 |    2: .5*sqrt(range) |       3: sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** |    **2: .5*sqrt(range)** |     **4: 2*sqrt(range)** |  **1,202.500 μs** |  **21.0413 μs** |  **17.5705 μs** |  **1,203.300 μs** |
|       HashSetAndArray |        0 |   100000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |    679.766 μs |  12.8489 μs |  27.6587 μs |    668.650 μs |
| SetHashLinkedAndArray |        0 |   100000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |  1,119.178 μs |  21.6644 μs |  23.1807 μs |  1,116.450 μs |
|  RelativelySimpleCode |        0 |   100000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** |       **3: sqrt(range)** | **1: sqrt(sqrt(range))** |      **9.181 μs** |   **0.1792 μs** |   **0.1759 μs** |      **9.150 μs** |
|       HashSetAndArray |        0 |   100000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |    520.625 μs |   9.7202 μs |   7.5889 μs |    519.550 μs |
| SetHashLinkedAndArray |        0 |   100000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |    681.883 μs |   4.8779 μs |   3.8083 μs |    681.950 μs |
|  RelativelySimpleCode |        0 |   100000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** |       **3: sqrt(range)** |    **2: .5*sqrt(range)** |     **39.013 μs** |   **0.2519 μs** |   **0.2356 μs** |     **39.100 μs** |
|       HashSetAndArray |        0 |   100000 |       3: sqrt(range) |    2: .5*sqrt(range) |    542.974 μs |   7.9034 μs |   8.7846 μs |    544.000 μs |
| SetHashLinkedAndArray |        0 |   100000 |       3: sqrt(range) |    2: .5*sqrt(range) |    808.495 μs |   3.9745 μs |   4.4177 μs |    808.200 μs |
|  RelativelySimpleCode |        0 |   100000 |       3: sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** |       **3: sqrt(range)** |       **3: sqrt(range)** |  **1,095.446 μs** |  **17.1478 μs** |  **14.3192 μs** |  **1,094.000 μs** |
|       HashSetAndArray |        0 |   100000 |       3: sqrt(range) |       3: sqrt(range) |    550.800 μs |  10.0925 μs |   8.4277 μs |    551.300 μs |
| SetHashLinkedAndArray |        0 |   100000 |       3: sqrt(range) |       3: sqrt(range) |    894.773 μs |  16.8285 μs |  15.7414 μs |    895.700 μs |
|  RelativelySimpleCode |        0 |   100000 |       3: sqrt(range) |       3: sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** |       **3: sqrt(range)** |     **4: 2*sqrt(range)** |  **1,181.946 μs** |  **23.0985 μs** |  **37.9515 μs** |  **1,170.100 μs** |
|       HashSetAndArray |        0 |   100000 |       3: sqrt(range) |     4: 2*sqrt(range) |    684.100 μs |  11.6130 μs |  10.2946 μs |    683.050 μs |
| SetHashLinkedAndArray |        0 |   100000 |       3: sqrt(range) |     4: 2*sqrt(range) |  1,080.970 μs |  48.4010 μs | 129.1919 μs |  1,078.900 μs |
|  RelativelySimpleCode |        0 |   100000 |       3: sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** |     **4: 2*sqrt(range)** | **1: sqrt(sqrt(range))** |     **16.233 μs** |   **0.3157 μs** |   **0.3378 μs** |     **16.250 μs** |
|       HashSetAndArray |        0 |   100000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |    530.200 μs |  10.3213 μs |  10.5992 μs |    525.900 μs |
| SetHashLinkedAndArray |        0 |   100000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |    693.650 μs |  12.0130 μs |   9.3789 μs |    694.050 μs |
|  RelativelySimpleCode |        0 |   100000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** |     **4: 2*sqrt(range)** |    **2: .5*sqrt(range)** |    **866.850 μs** |  **16.9351 μs** |  **13.2218 μs** |    **869.250 μs** |
|       HashSetAndArray |        0 |   100000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |    537.442 μs |   3.1979 μs |   2.4967 μs |    537.850 μs |
| SetHashLinkedAndArray |        0 |   100000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |    829.097 μs |  16.2801 μs |  28.5133 μs |    816.300 μs |
|  RelativelySimpleCode |        0 |   100000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** |     **4: 2*sqrt(range)** |       **3: sqrt(range)** |    **973.434 μs** |  **19.4479 μs** |  **35.0686 μs** |    **959.500 μs** |
|       HashSetAndArray |        0 |   100000 |     4: 2*sqrt(range) |       3: sqrt(range) |    551.175 μs |  10.6185 μs |  12.2283 μs |    547.450 μs |
| SetHashLinkedAndArray |        0 |   100000 |     4: 2*sqrt(range) |       3: sqrt(range) |    868.976 μs |  12.7562 μs |  24.2700 μs |    860.800 μs |
|  RelativelySimpleCode |        0 |   100000 |     4: 2*sqrt(range) |       3: sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** |     **4: 2*sqrt(range)** |     **4: 2*sqrt(range)** |  **1,147.648 μs** |  **17.9643 μs** |  **21.3852 μs** |  **1,150.100 μs** |
|       HashSetAndArray |        0 |   100000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |    683.693 μs |  13.2526 μs |  19.4255 μs |    681.100 μs |
| SetHashLinkedAndArray |        0 |   100000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |  1,100.950 μs |  20.3045 μs |  17.9994 μs |  1,101.850 μs |
|  RelativelySimpleCode |        0 |   100000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** | **1: sqrt(sqrt(range))** | **1: sqrt(sqrt(range))** |      **3.308 μs** |   **0.0677 μs** |   **0.0881 μs** |      **3.300 μs** |
|       HashSetAndArray |        0 |  1000000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |  5,931.787 μs | 118.4069 μs | 180.8198 μs |  5,926.500 μs |
| SetHashLinkedAndArray |        0 |  1000000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |  8,127.361 μs | 161.1742 μs | 231.1512 μs |  8,088.950 μs |
|  RelativelySimpleCode |        0 |  1000000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** | **1: sqrt(sqrt(range))** |    **2: .5*sqrt(range)** |     **96.715 μs** |   **0.9118 μs** |   **0.7614 μs** |     **97.000 μs** |
|       HashSetAndArray |        0 |  1000000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |  6,542.139 μs | 115.5361 μs | 176.4358 μs |  6,550.500 μs |
| SetHashLinkedAndArray |        0 |  1000000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) | 10,573.179 μs | 210.9036 μs | 186.9606 μs | 10,594.000 μs |
|  RelativelySimpleCode |        0 |  1000000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** | **1: sqrt(sqrt(range))** |       **3: sqrt(range)** |    **356.905 μs** |   **7.1217 μs** |   **8.7461 μs** |    **356.000 μs** |
|       HashSetAndArray |        0 |  1000000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |  8,872.458 μs | 170.0559 μs | 221.1206 μs |  8,838.500 μs |
| SetHashLinkedAndArray |        0 |  1000000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |  8,686.576 μs | 169.2679 μs | 291.9788 μs |  8,645.050 μs |
|  RelativelySimpleCode |        0 |  1000000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** | **1: sqrt(sqrt(range))** |     **4: 2*sqrt(range)** |  **9,681.273 μs** | **189.3883 μs** | **378.2289 μs** |  **9,589.000 μs** |
|       HashSetAndArray |        0 |  1000000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) | 11,726.050 μs | 221.9532 μs | 255.6017 μs | 11,731.150 μs |
| SetHashLinkedAndArray |        0 |  1000000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |  9,382.335 μs | 132.2128 μs | 201.9029 μs |  9,378.600 μs |
|  RelativelySimpleCode |        0 |  1000000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** |    **2: .5*sqrt(range)** | **1: sqrt(sqrt(range))** |     **17.223 μs** |   **0.1110 μs** |   **0.0927 μs** |     **17.200 μs** |
|       HashSetAndArray |        0 |  1000000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |  5,992.002 μs | 118.5224 μs | 231.1688 μs |  5,925.100 μs |
| SetHashLinkedAndArray |        0 |  1000000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |  7,823.658 μs | 153.2919 μs | 209.8277 μs |  7,805.850 μs |
|  RelativelySimpleCode |        0 |  1000000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** |    **2: .5*sqrt(range)** |    **2: .5*sqrt(range)** |    **218.715 μs** |   **3.7543 μs** |   **3.1350 μs** |    **219.100 μs** |
|       HashSetAndArray |        0 |  1000000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |  6,675.789 μs | 132.1309 μs | 281.5818 μs |  6,632.200 μs |
| SetHashLinkedAndArray |        0 |  1000000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) | 10,677.371 μs | 204.6702 μs | 181.4348 μs | 10,652.450 μs |
|  RelativelySimpleCode |        0 |  1000000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** |    **2: .5*sqrt(range)** |       **3: sqrt(range)** |    **615.505 μs** |  **11.9631 μs** |  **13.7767 μs** |    **609.250 μs** |
|       HashSetAndArray |        0 |  1000000 |    2: .5*sqrt(range) |       3: sqrt(range) |  8,914.900 μs | 173.1675 μs | 170.0736 μs |  8,930.200 μs |
| SetHashLinkedAndArray |        0 |  1000000 |    2: .5*sqrt(range) |       3: sqrt(range) |  8,835.350 μs | 147.9321 μs | 247.1614 μs |  8,832.550 μs |
|  RelativelySimpleCode |        0 |  1000000 |    2: .5*sqrt(range) |       3: sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** |    **2: .5*sqrt(range)** |     **4: 2*sqrt(range)** |  **9,442.768 μs** | **177.9920 μs** | **271.8126 μs** |  **9,420.600 μs** |
|       HashSetAndArray |        0 |  1000000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) | 11,934.731 μs | 231.5386 μs | 227.4018 μs | 11,870.000 μs |
| SetHashLinkedAndArray |        0 |  1000000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |  9,568.015 μs | 189.9499 μs | 444.0019 μs |  9,448.300 μs |
|  RelativelySimpleCode |        0 |  1000000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** |       **3: sqrt(range)** | **1: sqrt(sqrt(range))** |     **32.586 μs** |   **0.3725 μs** |   **0.3302 μs** |     **32.500 μs** |
|       HashSetAndArray |        0 |  1000000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |  5,957.269 μs | 117.9134 μs | 243.5117 μs |  5,906.800 μs |
| SetHashLinkedAndArray |        0 |  1000000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |  6,086.689 μs | 120.6725 μs | 284.4393 μs |  6,023.700 μs |
|  RelativelySimpleCode |        0 |  1000000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** |       **3: sqrt(range)** |    **2: .5*sqrt(range)** |    **349.731 μs** |   **3.3062 μs** |   **2.7609 μs** |    **349.900 μs** |
|       HashSetAndArray |        0 |  1000000 |       3: sqrt(range) |    2: .5*sqrt(range) |  6,364.303 μs | 115.6387 μs | 270.3017 μs |  6,305.200 μs |
| SetHashLinkedAndArray |        0 |  1000000 |       3: sqrt(range) |    2: .5*sqrt(range) |  7,062.667 μs | 118.1383 μs | 243.9762 μs |  7,034.900 μs |
|  RelativelySimpleCode |        0 |  1000000 |       3: sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** |       **3: sqrt(range)** |       **3: sqrt(range)** |  **9,433.620 μs** | **185.9115 μs** | **396.1925 μs** |  **9,333.800 μs** |
|       HashSetAndArray |        0 |  1000000 |       3: sqrt(range) |       3: sqrt(range) |  8,888.531 μs | 176.6772 μs | 241.8377 μs |  8,861.650 μs |
| SetHashLinkedAndArray |        0 |  1000000 |       3: sqrt(range) |       3: sqrt(range) |  8,690.118 μs | 172.0784 μs | 272.9346 μs |  8,648.900 μs |
|  RelativelySimpleCode |        0 |  1000000 |       3: sqrt(range) |       3: sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** |       **3: sqrt(range)** |     **4: 2*sqrt(range)** |  **9,432.558 μs** | **129.6752 μs** | **198.0278 μs** |  **9,467.900 μs** |
|       HashSetAndArray |        0 |  1000000 |       3: sqrt(range) |     4: 2*sqrt(range) | 11,760.619 μs | 231.2454 μs | 227.1139 μs | 11,752.450 μs |
| SetHashLinkedAndArray |        0 |  1000000 |       3: sqrt(range) |     4: 2*sqrt(range) |  9,383.345 μs | 180.1540 μs | 310.7567 μs |  9,325.150 μs |
|  RelativelySimpleCode |        0 |  1000000 |       3: sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** |     **4: 2*sqrt(range)** | **1: sqrt(sqrt(range))** |     **62.400 μs** |   **1.2387 μs** |   **1.0344 μs** |     **61.800 μs** |
|       HashSetAndArray |        0 |  1000000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |  6,000.110 μs | 119.0096 μs | 243.1052 μs |  5,969.800 μs |
| SetHashLinkedAndArray |        0 |  1000000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |  6,000.676 μs | 105.3549 μs | 222.2293 μs |  5,954.750 μs |
|  RelativelySimpleCode |        0 |  1000000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** |     **4: 2*sqrt(range)** |    **2: .5*sqrt(range)** |  **7,370.392 μs** | **128.5002 μs** | **256.6288 μs** |  **7,354.300 μs** |
|       HashSetAndArray |        0 |  1000000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |  6,650.873 μs | 130.7713 μs | 235.8077 μs |  6,611.700 μs |
| SetHashLinkedAndArray |        0 |  1000000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |  6,913.412 μs | 134.3436 μs | 274.4284 μs |  6,881.000 μs |
|  RelativelySimpleCode |        0 |  1000000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** |     **4: 2*sqrt(range)** |       **3: sqrt(range)** |  **8,801.070 μs** | **172.0775 μs** | **282.7283 μs** |  **8,755.050 μs** |
|       HashSetAndArray |        0 |  1000000 |     4: 2*sqrt(range) |       3: sqrt(range) |  8,872.288 μs | 168.9004 μs | 272.7426 μs |  8,810.600 μs |
| SetHashLinkedAndArray |        0 |  1000000 |     4: 2*sqrt(range) |       3: sqrt(range) |  9,076.731 μs | 173.6727 μs | 304.1737 μs |  9,039.000 μs |
|  RelativelySimpleCode |        0 |  1000000 |     4: 2*sqrt(range) |       3: sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** |     **4: 2*sqrt(range)** |     **4: 2*sqrt(range)** |  **9,390.041 μs** | **183.7147 μs** | **311.9615 μs** |  **9,383.400 μs** |
|       HashSetAndArray |        0 |  1000000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) | 11,808.315 μs | 179.7810 μs | 150.1253 μs | 11,826.500 μs |
| SetHashLinkedAndArray |        0 |  1000000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |  9,324.665 μs | 130.7383 μs | 222.0036 μs |  9,351.000 μs |
|  RelativelySimpleCode |        0 |  1000000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |          NA |            NA |

Benchmarks with issues:
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=1: sqrt(sqrt(range)), Exclued=1: sqrt(sqrt(range))]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=1: sqrt(sqrt(range)), Exclued=2: .5*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=1: sqrt(sqrt(range)), Exclued=3: sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=1: sqrt(sqrt(range)), Exclued=4: 2*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=2: .5*sqrt(range), Exclued=1: sqrt(sqrt(range))]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=2: .5*sqrt(range), Exclued=2: .5*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=2: .5*sqrt(range), Exclued=3: sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=2: .5*sqrt(range), Exclued=4: 2*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=3: sqrt(range), Exclued=1: sqrt(sqrt(range))]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=3: sqrt(range), Exclued=2: .5*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=3: sqrt(range), Exclued=3: sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=3: sqrt(range), Exclued=4: 2*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=4: 2*sqrt(range), Exclued=1: sqrt(sqrt(range))]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=4: 2*sqrt(range), Exclued=2: .5*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=4: 2*sqrt(range), Exclued=3: sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=4: 2*sqrt(range), Exclued=4: 2*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=1: sqrt(sqrt(range)), Exclued=1: sqrt(sqrt(range))]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=1: sqrt(sqrt(range)), Exclued=2: .5*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=1: sqrt(sqrt(range)), Exclued=3: sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=1: sqrt(sqrt(range)), Exclued=4: 2*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=2: .5*sqrt(range), Exclued=1: sqrt(sqrt(range))]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=2: .5*sqrt(range), Exclued=2: .5*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=2: .5*sqrt(range), Exclued=3: sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=2: .5*sqrt(range), Exclued=4: 2*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=3: sqrt(range), Exclued=1: sqrt(sqrt(range))]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=3: sqrt(range), Exclued=2: .5*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=3: sqrt(range), Exclued=3: sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=3: sqrt(range), Exclued=4: 2*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=4: 2*sqrt(range), Exclued=1: sqrt(sqrt(range))]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=4: 2*sqrt(range), Exclued=2: .5*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=4: 2*sqrt(range), Exclued=3: sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=4: 2*sqrt(range), Exclued=4: 2*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=1: sqrt(sqrt(range)), Exclued=1: sqrt(sqrt(range))]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=1: sqrt(sqrt(range)), Exclued=2: .5*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=1: sqrt(sqrt(range)), Exclued=3: sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=1: sqrt(sqrt(range)), Exclued=4: 2*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=2: .5*sqrt(range), Exclued=1: sqrt(sqrt(range))]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=2: .5*sqrt(range), Exclued=2: .5*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=2: .5*sqrt(range), Exclued=3: sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=2: .5*sqrt(range), Exclued=4: 2*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=3: sqrt(range), Exclued=1: sqrt(sqrt(range))]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=3: sqrt(range), Exclued=2: .5*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=3: sqrt(range), Exclued=3: sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=3: sqrt(range), Exclued=4: 2*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=4: 2*sqrt(range), Exclued=1: sqrt(sqrt(range))]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=4: 2*sqrt(range), Exclued=2: .5*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=4: 2*sqrt(range), Exclued=3: sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-HBXSTX(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=4: 2*sqrt(range), Exclued=4: 2*sqrt(range)]

