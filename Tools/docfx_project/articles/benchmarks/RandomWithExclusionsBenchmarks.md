# Random With Exclusions

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
|                Method | MinValue | MaxValue |                Count |              Exclued |          Mean |       Error |      StdDev |        Median |
|---------------------- |--------- |--------- |--------------------- |--------------------- |--------------:|------------:|------------:|--------------:|
|                 **Towel** |        **0** |       **10** | **1: sqrt(sqrt(range))** | **1: sqrt(sqrt(range))** |      **1.551 μs** |   **0.0329 μs** |   **0.0728 μs** |      **1.500 μs** |
|       HashSetAndArray |        0 |       10 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |      1.463 μs |   0.0312 μs |   0.0692 μs |      1.500 μs |
| SetHashLinkedAndArray |        0 |       10 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |      1.577 μs |   0.0278 μs |   0.0425 μs |      1.600 μs |
|  RelativelySimpleCode |        0 |       10 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |      3.835 μs |   0.0765 μs |   0.0786 μs |      3.800 μs |
|                 **Towel** |        **0** |       **10** | **1: sqrt(sqrt(range))** |    **2: .5*sqrt(range)** |      **1.368 μs** |   **0.0311 μs** |   **0.0475 μs** |      **1.400 μs** |
|       HashSetAndArray |        0 |       10 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |      1.445 μs |   0.0306 μs |   0.0738 μs |      1.400 μs |
| SetHashLinkedAndArray |        0 |       10 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |      1.617 μs |   0.0342 μs |   0.0706 μs |      1.600 μs |
|  RelativelySimpleCode |        0 |       10 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |      4.088 μs |   0.1040 μs |   0.2898 μs |      4.000 μs |
|                 **Towel** |        **0** |       **10** | **1: sqrt(sqrt(range))** |       **3: sqrt(range)** |      **1.600 μs** |   **0.0000 μs** |   **0.0000 μs** |      **1.600 μs** |
|       HashSetAndArray |        0 |       10 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |      1.589 μs |   0.0309 μs |   0.0753 μs |      1.600 μs |
| SetHashLinkedAndArray |        0 |       10 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |      1.933 μs |   0.0400 μs |   0.0701 μs |      1.900 μs |
|  RelativelySimpleCode |        0 |       10 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |      3.772 μs |   0.0772 μs |   0.0826 μs |      3.750 μs |
|                 **Towel** |        **0** |       **10** | **1: sqrt(sqrt(range))** |     **4: 2*sqrt(range)** |      **2.710 μs** |   **0.0544 μs** |   **0.0831 μs** |      **2.700 μs** |
|       HashSetAndArray |        0 |       10 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |      1.582 μs |   0.0321 μs |   0.0395 μs |      1.600 μs |
| SetHashLinkedAndArray |        0 |       10 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |      2.062 μs |   0.0428 μs |   0.0913 μs |      2.100 μs |
|  RelativelySimpleCode |        0 |       10 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |      3.904 μs |   0.0772 μs |   0.0976 μs |      3.900 μs |
|                 **Towel** |        **0** |       **10** |    **2: .5*sqrt(range)** | **1: sqrt(sqrt(range))** |      **1.582 μs** |   **0.0334 μs** |   **0.0748 μs** |      **1.600 μs** |
|       HashSetAndArray |        0 |       10 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |      1.464 μs |   0.0310 μs |   0.0605 μs |      1.500 μs |
| SetHashLinkedAndArray |        0 |       10 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |      1.569 μs |   0.0302 μs |   0.0471 μs |      1.600 μs |
|  RelativelySimpleCode |        0 |       10 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |      3.849 μs |   0.0785 μs |   0.1374 μs |      3.800 μs |
|                 **Towel** |        **0** |       **10** |    **2: .5*sqrt(range)** |    **2: .5*sqrt(range)** |      **1.493 μs** |   **0.0301 μs** |   **0.0267 μs** |      **1.500 μs** |
|       HashSetAndArray |        0 |       10 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |      1.444 μs |   0.0307 μs |   0.0786 μs |      1.400 μs |
| SetHashLinkedAndArray |        0 |       10 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |      1.573 μs |   0.0292 μs |   0.0716 μs |      1.600 μs |
|  RelativelySimpleCode |        0 |       10 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |      3.904 μs |   0.0700 μs |   0.0935 μs |      3.900 μs |
|                 **Towel** |        **0** |       **10** |    **2: .5*sqrt(range)** |       **3: sqrt(range)** |      **1.664 μs** |   **0.0346 μs** |   **0.0549 μs** |      **1.700 μs** |
|       HashSetAndArray |        0 |       10 |    2: .5*sqrt(range) |       3: sqrt(range) |      1.588 μs |   0.0308 μs |   0.0812 μs |      1.600 μs |
| SetHashLinkedAndArray |        0 |       10 |    2: .5*sqrt(range) |       3: sqrt(range) |      1.953 μs |   0.0410 μs |   0.0819 μs |      1.900 μs |
|  RelativelySimpleCode |        0 |       10 |    2: .5*sqrt(range) |       3: sqrt(range) |      3.769 μs |   0.0773 μs |   0.1203 μs |      3.700 μs |
|                 **Towel** |        **0** |       **10** |    **2: .5*sqrt(range)** |     **4: 2*sqrt(range)** |      **2.671 μs** |   **0.0541 μs** |   **0.0644 μs** |      **2.700 μs** |
|       HashSetAndArray |        0 |       10 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |      1.615 μs |   0.0378 μs |   0.1077 μs |      1.600 μs |
| SetHashLinkedAndArray |        0 |       10 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |      2.081 μs |   0.0425 μs |   0.0710 μs |      2.100 μs |
|  RelativelySimpleCode |        0 |       10 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |      3.847 μs |   0.0699 μs |   0.0717 μs |      3.800 μs |
|                 **Towel** |        **0** |       **10** |       **3: sqrt(range)** | **1: sqrt(sqrt(range))** |      **1.582 μs** |   **0.0321 μs** |   **0.0395 μs** |      **1.600 μs** |
|       HashSetAndArray |        0 |       10 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |      1.473 μs |   0.0312 μs |   0.0760 μs |      1.500 μs |
| SetHashLinkedAndArray |        0 |       10 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |      1.624 μs |   0.0342 μs |   0.0805 μs |      1.600 μs |
|  RelativelySimpleCode |        0 |       10 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |      4.713 μs |   0.0703 μs |   0.1212 μs |      4.700 μs |
|                 **Towel** |        **0** |       **10** |       **3: sqrt(range)** |    **2: .5*sqrt(range)** |      **1.549 μs** |   **0.0327 μs** |   **0.0710 μs** |      **1.500 μs** |
|       HashSetAndArray |        0 |       10 |       3: sqrt(range) |    2: .5*sqrt(range) |      1.437 μs |   0.0305 μs |   0.0731 μs |      1.400 μs |
| SetHashLinkedAndArray |        0 |       10 |       3: sqrt(range) |    2: .5*sqrt(range) |      1.641 μs |   0.0347 μs |   0.0955 μs |      1.600 μs |
|  RelativelySimpleCode |        0 |       10 |       3: sqrt(range) |    2: .5*sqrt(range) |      4.685 μs |   0.0825 μs |   0.0689 μs |      4.700 μs |
|                 **Towel** |        **0** |       **10** |       **3: sqrt(range)** |       **3: sqrt(range)** |      **1.670 μs** |   **0.0311 μs** |   **0.0466 μs** |      **1.700 μs** |
|       HashSetAndArray |        0 |       10 |       3: sqrt(range) |       3: sqrt(range) |      1.592 μs |   0.0332 μs |   0.0277 μs |      1.600 μs |
| SetHashLinkedAndArray |        0 |       10 |       3: sqrt(range) |       3: sqrt(range) |      2.009 μs |   0.0422 μs |   0.1161 μs |      2.000 μs |
|  RelativelySimpleCode |        0 |       10 |       3: sqrt(range) |       3: sqrt(range) |      4.685 μs |   0.0952 μs |   0.2491 μs |      4.650 μs |
|                 **Towel** |        **0** |       **10** |       **3: sqrt(range)** |     **4: 2*sqrt(range)** |      **2.692 μs** |   **0.0552 μs** |   **0.1129 μs** |      **2.700 μs** |
|       HashSetAndArray |        0 |       10 |       3: sqrt(range) |     4: 2*sqrt(range) |      1.636 μs |   0.0342 μs |   0.0729 μs |      1.600 μs |
| SetHashLinkedAndArray |        0 |       10 |       3: sqrt(range) |     4: 2*sqrt(range) |      2.068 μs |   0.0430 μs |   0.0478 μs |      2.100 μs |
|  RelativelySimpleCode |        0 |       10 |       3: sqrt(range) |     4: 2*sqrt(range) |      4.914 μs |   0.0953 μs |   0.1619 μs |      4.900 μs |
|                 **Towel** |        **0** |       **10** |     **4: 2*sqrt(range)** | **1: sqrt(sqrt(range))** |      **1.569 μs** |   **0.0321 μs** |   **0.0471 μs** |      **1.600 μs** |
|       HashSetAndArray |        0 |       10 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |      1.537 μs |   0.0326 μs |   0.0762 μs |      1.500 μs |
| SetHashLinkedAndArray |        0 |       10 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |      1.646 μs |   0.0349 μs |   0.0787 μs |      1.600 μs |
|  RelativelySimpleCode |        0 |       10 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |      5.515 μs |   0.1087 μs |   0.1488 μs |      5.500 μs |
|                 **Towel** |        **0** |       **10** |     **4: 2*sqrt(range)** |    **2: .5*sqrt(range)** |      **1.640 μs** |   **0.0346 μs** |   **0.0753 μs** |      **1.600 μs** |
|       HashSetAndArray |        0 |       10 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |      1.475 μs |   0.0307 μs |   0.0441 μs |      1.500 μs |
| SetHashLinkedAndArray |        0 |       10 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |      1.649 μs |   0.0349 μs |   0.0774 μs |      1.600 μs |
|  RelativelySimpleCode |        0 |       10 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |      5.739 μs |   0.1112 μs |   0.1406 μs |      5.800 μs |
|                 **Towel** |        **0** |       **10** |     **4: 2*sqrt(range)** |       **3: sqrt(range)** |      **2.451 μs** |   **0.0475 μs** |   **0.0781 μs** |      **2.400 μs** |
|       HashSetAndArray |        0 |       10 |     4: 2*sqrt(range) |       3: sqrt(range) |      1.653 μs |   0.0313 μs |   0.0814 μs |      1.600 μs |
| SetHashLinkedAndArray |        0 |       10 |     4: 2*sqrt(range) |       3: sqrt(range) |      1.948 μs |   0.0405 μs |   0.0740 μs |      1.900 μs |
|  RelativelySimpleCode |        0 |       10 |     4: 2*sqrt(range) |       3: sqrt(range) |      5.866 μs |   0.1182 μs |   0.2738 μs |      5.800 μs |
|                 **Towel** |        **0** |       **10** |     **4: 2*sqrt(range)** |     **4: 2*sqrt(range)** |      **2.667 μs** |   **0.0553 μs** |   **0.0658 μs** |      **2.700 μs** |
|       HashSetAndArray |        0 |       10 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |      1.684 μs |   0.0356 μs |   0.0846 μs |      1.700 μs |
| SetHashLinkedAndArray |        0 |       10 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |      2.166 μs |   0.0445 μs |   0.0959 μs |      2.200 μs |
|  RelativelySimpleCode |        0 |       10 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |      6.018 μs |   0.1131 μs |   0.2577 μs |      6.000 μs |
|                 **Towel** |        **0** |      **100** | **1: sqrt(sqrt(range))** | **1: sqrt(sqrt(range))** |      **1.669 μs** |   **0.0344 μs** |   **0.0471 μs** |      **1.700 μs** |
|       HashSetAndArray |        0 |      100 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |      2.094 μs |   0.0255 μs |   0.0250 μs |      2.100 μs |
| SetHashLinkedAndArray |        0 |      100 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |      2.577 μs |   0.0525 μs |   0.0439 μs |      2.600 μs |
|  RelativelySimpleCode |        0 |      100 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |      9.874 μs |   0.1971 μs |   0.3239 μs |      9.800 μs |
|                 **Towel** |        **0** |      **100** | **1: sqrt(sqrt(range))** |    **2: .5*sqrt(range)** |      **1.795 μs** |   **0.0378 μs** |   **0.1028 μs** |      **1.800 μs** |
|       HashSetAndArray |        0 |      100 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |      2.316 μs |   0.0433 μs |   0.0951 μs |      2.300 μs |
| SetHashLinkedAndArray |        0 |      100 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |      3.278 μs |   0.0674 μs |   0.1550 μs |      3.200 μs |
|  RelativelySimpleCode |        0 |      100 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |     11.226 μs |   0.2240 μs |   0.3681 μs |     11.300 μs |
|                 **Towel** |        **0** |      **100** | **1: sqrt(sqrt(range))** |       **3: sqrt(range)** |      **1.873 μs** |   **0.0393 μs** |   **0.0708 μs** |      **1.900 μs** |
|       HashSetAndArray |        0 |      100 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |      2.526 μs |   0.0516 μs |   0.0891 μs |      2.500 μs |
| SetHashLinkedAndArray |        0 |      100 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |      3.610 μs |   0.0741 μs |   0.1317 μs |      3.600 μs |
|  RelativelySimpleCode |        0 |      100 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |     12.276 μs |   0.2360 μs |   0.2809 μs |     12.200 μs |
|                 **Towel** |        **0** |      **100** | **1: sqrt(sqrt(range))** |     **4: 2*sqrt(range)** |      **4.644 μs** |   **0.0908 μs** |   **0.0892 μs** |      **4.700 μs** |
|       HashSetAndArray |        0 |      100 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |      2.840 μs |   0.0579 μs |   0.1144 μs |      2.800 μs |
| SetHashLinkedAndArray |        0 |      100 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |      3.903 μs |   0.0648 μs |   0.1082 μs |      3.900 μs |
|  RelativelySimpleCode |        0 |      100 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |     13.067 μs |   0.2345 μs |   0.2193 μs |     13.100 μs |
|                 **Towel** |        **0** |      **100** |    **2: .5*sqrt(range)** | **1: sqrt(sqrt(range))** |      **1.732 μs** |   **0.0363 μs** |   **0.0813 μs** |      **1.700 μs** |
|       HashSetAndArray |        0 |      100 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |      2.111 μs |   0.0441 μs |   0.0849 μs |      2.100 μs |
| SetHashLinkedAndArray |        0 |      100 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |      2.702 μs |   0.0547 μs |   0.0987 μs |      2.700 μs |
|  RelativelySimpleCode |        0 |      100 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |     14.186 μs |   0.2840 μs |   0.3381 μs |     14.200 μs |
|                 **Towel** |        **0** |      **100** |    **2: .5*sqrt(range)** |    **2: .5*sqrt(range)** |      **1.773 μs** |   **0.0371 μs** |   **0.0456 μs** |      **1.800 μs** |
|       HashSetAndArray |        0 |      100 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |      2.424 μs |   0.0504 μs |   0.0739 μs |      2.400 μs |
| SetHashLinkedAndArray |        0 |      100 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |      3.322 μs |   0.0666 μs |   0.0934 μs |      3.300 μs |
|  RelativelySimpleCode |        0 |      100 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |     16.720 μs |   0.3135 μs |   0.2933 μs |     16.800 μs |
|                 **Towel** |        **0** |      **100** |    **2: .5*sqrt(range)** |       **3: sqrt(range)** |      **1.938 μs** |   **0.0400 μs** |   **0.0789 μs** |      **1.900 μs** |
|       HashSetAndArray |        0 |      100 |    2: .5*sqrt(range) |       3: sqrt(range) |      2.611 μs |   0.0537 μs |   0.1266 μs |      2.600 μs |
| SetHashLinkedAndArray |        0 |      100 |    2: .5*sqrt(range) |       3: sqrt(range) |      3.617 μs |   0.0715 μs |   0.1175 μs |      3.600 μs |
|  RelativelySimpleCode |        0 |      100 |    2: .5*sqrt(range) |       3: sqrt(range) |     16.618 μs |   0.3289 μs |   0.5217 μs |     16.700 μs |
|                 **Towel** |        **0** |      **100** |    **2: .5*sqrt(range)** |     **4: 2*sqrt(range)** |      **4.606 μs** |   **0.0869 μs** |   **0.0854 μs** |      **4.600 μs** |
|       HashSetAndArray |        0 |      100 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |      2.768 μs |   0.0561 μs |   0.0748 μs |      2.800 μs |
| SetHashLinkedAndArray |        0 |      100 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |      4.022 μs |   0.0811 μs |   0.1263 μs |      4.000 μs |
|  RelativelySimpleCode |        0 |      100 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |     17.615 μs |   0.3444 μs |   0.6718 μs |     17.700 μs |
|                 **Towel** |        **0** |      **100** |       **3: sqrt(range)** | **1: sqrt(sqrt(range))** |      **1.764 μs** |   **0.0373 μs** |   **0.0826 μs** |      **1.700 μs** |
|       HashSetAndArray |        0 |      100 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |      2.202 μs |   0.0457 μs |   0.1259 μs |      2.200 μs |
| SetHashLinkedAndArray |        0 |      100 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |      2.766 μs |   0.0549 μs |   0.0990 μs |      2.800 μs |
|  RelativelySimpleCode |        0 |      100 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |     26.556 μs |   0.5258 μs |   0.5164 μs |     26.450 μs |
|                 **Towel** |        **0** |      **100** |       **3: sqrt(range)** |    **2: .5*sqrt(range)** |      **1.854 μs** |   **0.0390 μs** |   **0.0788 μs** |      **1.800 μs** |
|       HashSetAndArray |        0 |      100 |       3: sqrt(range) |    2: .5*sqrt(range) |      2.413 μs |   0.0496 μs |   0.0944 μs |      2.400 μs |
| SetHashLinkedAndArray |        0 |      100 |       3: sqrt(range) |    2: .5*sqrt(range) |      3.317 μs |   0.0679 μs |   0.1324 μs |      3.300 μs |
|  RelativelySimpleCode |        0 |      100 |       3: sqrt(range) |    2: .5*sqrt(range) |     27.279 μs |   0.5257 μs |   0.4660 μs |     27.150 μs |
|                 **Towel** |        **0** |      **100** |       **3: sqrt(range)** |       **3: sqrt(range)** |      **4.333 μs** |   **0.0848 μs** |   **0.0907 μs** |      **4.350 μs** |
|       HashSetAndArray |        0 |      100 |       3: sqrt(range) |       3: sqrt(range) |      2.653 μs |   0.0547 μs |   0.0819 μs |      2.600 μs |
| SetHashLinkedAndArray |        0 |      100 |       3: sqrt(range) |       3: sqrt(range) |      3.613 μs |   0.0731 μs |   0.1392 μs |      3.600 μs |
|  RelativelySimpleCode |        0 |      100 |       3: sqrt(range) |       3: sqrt(range) |     27.364 μs |   0.5067 μs |   1.0907 μs |     27.000 μs |
|                 **Towel** |        **0** |      **100** |       **3: sqrt(range)** |     **4: 2*sqrt(range)** |      **4.646 μs** |   **0.0949 μs** |   **0.1559 μs** |      **4.600 μs** |
|       HashSetAndArray |        0 |      100 |       3: sqrt(range) |     4: 2*sqrt(range) |      2.844 μs |   0.0580 μs |   0.0969 μs |      2.800 μs |
| SetHashLinkedAndArray |        0 |      100 |       3: sqrt(range) |     4: 2*sqrt(range) |      4.117 μs |   0.0817 μs |   0.1197 μs |      4.100 μs |
|  RelativelySimpleCode |        0 |      100 |       3: sqrt(range) |     4: 2*sqrt(range) |     27.862 μs |   0.5368 μs |   0.4482 μs |     27.800 μs |
|                 **Towel** |        **0** |      **100** |     **4: 2*sqrt(range)** | **1: sqrt(sqrt(range))** |      **1.978 μs** |   **0.0408 μs** |   **0.0786 μs** |      **2.000 μs** |
|       HashSetAndArray |        0 |      100 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |      2.342 μs |   0.0483 μs |   0.0806 μs |      2.300 μs |
| SetHashLinkedAndArray |        0 |      100 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |      2.908 μs |   0.0600 μs |   0.1253 μs |      2.900 μs |
|  RelativelySimpleCode |        0 |      100 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |     47.979 μs |   0.9564 μs |   1.4019 μs |     47.700 μs |
|                 **Towel** |        **0** |      **100** |     **4: 2*sqrt(range)** |    **2: .5*sqrt(range)** |      **2.084 μs** |   **0.0435 μs** |   **0.0677 μs** |      **2.100 μs** |
|       HashSetAndArray |        0 |      100 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |      2.513 μs |   0.0515 μs |   0.1075 μs |      2.500 μs |
| SetHashLinkedAndArray |        0 |      100 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |      3.404 μs |   0.0700 μs |   0.0958 μs |      3.400 μs |
|  RelativelySimpleCode |        0 |      100 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |     48.428 μs |   0.9432 μs |   1.0093 μs |     48.450 μs |
|                 **Towel** |        **0** |      **100** |     **4: 2*sqrt(range)** |       **3: sqrt(range)** |      **4.485 μs** |   **0.0903 μs** |   **0.1582 μs** |      **4.500 μs** |
|       HashSetAndArray |        0 |      100 |     4: 2*sqrt(range) |       3: sqrt(range) |      2.734 μs |   0.0562 μs |   0.1210 μs |      2.700 μs |
| SetHashLinkedAndArray |        0 |      100 |     4: 2*sqrt(range) |       3: sqrt(range) |      3.670 μs |   0.0692 μs |   0.0876 μs |      3.700 μs |
|  RelativelySimpleCode |        0 |      100 |     4: 2*sqrt(range) |       3: sqrt(range) |     48.380 μs |   0.7811 μs |   0.8995 μs |     48.250 μs |
|                 **Towel** |        **0** |      **100** |     **4: 2*sqrt(range)** |     **4: 2*sqrt(range)** |      **4.871 μs** |   **0.0690 μs** |   **0.0611 μs** |      **4.900 μs** |
|       HashSetAndArray |        0 |      100 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |      2.968 μs |   0.0602 μs |   0.0863 μs |      3.000 μs |
| SetHashLinkedAndArray |        0 |      100 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |      4.200 μs |   0.0860 μs |   0.1087 μs |      4.200 μs |
|  RelativelySimpleCode |        0 |      100 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |     51.296 μs |   1.0118 μs |   1.3849 μs |     51.300 μs |
|                 **Towel** |        **0** |     **1000** | **1: sqrt(sqrt(range))** | **1: sqrt(sqrt(range))** |      **1.778 μs** |   **0.0370 μs** |   **0.0704 μs** |      **1.800 μs** |
|       HashSetAndArray |        0 |     1000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |      6.695 μs |   0.1323 μs |   0.1471 μs |      6.700 μs |
| SetHashLinkedAndArray |        0 |     1000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |      9.315 μs |   0.1534 μs |   0.1281 μs |      9.300 μs |
|  RelativelySimpleCode |        0 |     1000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |    112.508 μs |   0.9661 μs |   0.8067 μs |    112.100 μs |
|                 **Towel** |        **0** |     **1000** | **1: sqrt(sqrt(range))** |    **2: .5*sqrt(range)** |      **2.018 μs** |   **0.0416 μs** |   **0.0940 μs** |      **2.000 μs** |
|       HashSetAndArray |        0 |     1000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |      7.854 μs |   0.1538 μs |   0.2000 μs |      7.800 μs |
| SetHashLinkedAndArray |        0 |     1000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |     10.627 μs |   0.2143 μs |   0.2933 μs |     10.700 μs |
|  RelativelySimpleCode |        0 |     1000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |    104.769 μs |   1.0178 μs |   0.8499 μs |    104.800 μs |
|                 **Towel** |        **0** |     **1000** | **1: sqrt(sqrt(range))** |       **3: sqrt(range)** |      **2.416 μs** |   **0.0499 μs** |   **0.0924 μs** |      **2.400 μs** |
|       HashSetAndArray |        0 |     1000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |      8.881 μs |   0.1776 μs |   0.3248 μs |      8.900 μs |
| SetHashLinkedAndArray |        0 |     1000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |     12.314 μs |   0.2465 μs |   0.3613 μs |     12.300 μs |
|  RelativelySimpleCode |        0 |     1000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |     89.390 μs |   1.6109 μs |   3.8595 μs |     88.650 μs |
|                 **Towel** |        **0** |     **1000** | **1: sqrt(sqrt(range))** |     **4: 2*sqrt(range)** |     **16.438 μs** |   **0.3298 μs** |   **0.5326 μs** |     **16.600 μs** |
|       HashSetAndArray |        0 |     1000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |     11.375 μs |   0.3327 μs |   0.9219 μs |     11.600 μs |
| SetHashLinkedAndArray |        0 |     1000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |     15.478 μs |   0.3093 μs |   0.4335 μs |     15.600 μs |
|  RelativelySimpleCode |        0 |     1000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |    114.750 μs |   2.1144 μs |   2.8942 μs |    114.450 μs |
|                 **Towel** |        **0** |     **1000** |    **2: .5*sqrt(range)** | **1: sqrt(sqrt(range))** |      **1.972 μs** |   **0.0343 μs** |   **0.0458 μs** |      **2.000 μs** |
|       HashSetAndArray |        0 |     1000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |      6.835 μs |   0.1375 μs |   0.1412 μs |      6.800 μs |
| SetHashLinkedAndArray |        0 |     1000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |      9.515 μs |   0.1884 μs |   0.1573 μs |      9.500 μs |
|  RelativelySimpleCode |        0 |     1000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |    304.667 μs |   5.3716 μs |   4.1938 μs |    303.600 μs |
|                 **Towel** |        **0** |     **1000** |    **2: .5*sqrt(range)** |    **2: .5*sqrt(range)** |      **2.277 μs** |   **0.0469 μs** |   **0.0770 μs** |      **2.300 μs** |
|       HashSetAndArray |        0 |     1000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |      7.911 μs |   0.1581 μs |   0.3726 μs |      7.950 μs |
| SetHashLinkedAndArray |        0 |     1000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |     10.583 μs |   0.2091 μs |   0.3130 μs |     10.600 μs |
|  RelativelySimpleCode |        0 |     1000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |    305.764 μs |   5.2373 μs |   4.6427 μs |    305.550 μs |
|                 **Towel** |        **0** |     **1000** |    **2: .5*sqrt(range)** |       **3: sqrt(range)** |      **2.692 μs** |   **0.0370 μs** |   **0.0289 μs** |      **2.700 μs** |
|       HashSetAndArray |        0 |     1000 |    2: .5*sqrt(range) |       3: sqrt(range) |      8.881 μs |   0.1772 μs |   0.4071 μs |      8.900 μs |
| SetHashLinkedAndArray |        0 |     1000 |    2: .5*sqrt(range) |       3: sqrt(range) |     12.484 μs |   0.2512 μs |   0.3911 μs |     12.500 μs |
|  RelativelySimpleCode |        0 |     1000 |    2: .5*sqrt(range) |       3: sqrt(range) |    333.569 μs |   6.6644 μs |   9.1223 μs |    332.150 μs |
|                 **Towel** |        **0** |     **1000** |    **2: .5*sqrt(range)** |     **4: 2*sqrt(range)** |     **16.717 μs** |   **0.3352 μs** |   **0.6458 μs** |     **16.800 μs** |
|       HashSetAndArray |        0 |     1000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |     11.775 μs |   0.2352 μs |   0.3373 μs |     11.700 μs |
| SetHashLinkedAndArray |        0 |     1000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |     15.280 μs |   0.3051 μs |   0.6887 μs |     15.500 μs |
|  RelativelySimpleCode |        0 |     1000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |    365.732 μs |   6.2931 μs |  16.5786 μs |    359.500 μs |
|                 **Towel** |        **0** |     **1000** |       **3: sqrt(range)** | **1: sqrt(sqrt(range))** |      **2.310 μs** |   **0.0473 μs** |   **0.0841 μs** |      **2.300 μs** |
|       HashSetAndArray |        0 |     1000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |      6.936 μs |   0.1298 μs |   0.1151 μs |      6.950 μs |
| SetHashLinkedAndArray |        0 |     1000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |      9.637 μs |   0.1891 μs |   0.1857 μs |      9.700 μs |
|  RelativelySimpleCode |        0 |     1000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |    602.429 μs |  11.9118 μs |  18.1905 μs |    595.200 μs |
|                 **Towel** |        **0** |     **1000** |       **3: sqrt(range)** |    **2: .5*sqrt(range)** |      **2.592 μs** |   **0.0370 μs** |   **0.0289 μs** |      **2.600 μs** |
|       HashSetAndArray |        0 |     1000 |       3: sqrt(range) |    2: .5*sqrt(range) |      8.030 μs |   0.1619 μs |   0.2423 μs |      8.000 μs |
| SetHashLinkedAndArray |        0 |     1000 |       3: sqrt(range) |    2: .5*sqrt(range) |     10.987 μs |   0.2215 μs |   0.2801 μs |     11.000 μs |
|  RelativelySimpleCode |        0 |     1000 |       3: sqrt(range) |    2: .5*sqrt(range) |    626.646 μs |   5.7337 μs |   4.7879 μs |    625.900 μs |
|                 **Towel** |        **0** |     **1000** |       **3: sqrt(range)** |       **3: sqrt(range)** |     **13.668 μs** |   **0.2723 μs** |   **0.4697 μs** |     **13.800 μs** |
|       HashSetAndArray |        0 |     1000 |       3: sqrt(range) |       3: sqrt(range) |      9.170 μs |   0.1834 μs |   0.4102 μs |      9.150 μs |
| SetHashLinkedAndArray |        0 |     1000 |       3: sqrt(range) |       3: sqrt(range) |     12.731 μs |   0.2560 μs |   0.3752 μs |     12.800 μs |
|  RelativelySimpleCode |        0 |     1000 |       3: sqrt(range) |       3: sqrt(range) |    719.688 μs |  14.2474 μs |  18.5257 μs |    711.450 μs |
|                 **Towel** |        **0** |     **1000** |       **3: sqrt(range)** |     **4: 2*sqrt(range)** |     **17.091 μs** |   **0.3341 μs** |   **0.6973 μs** |     **17.100 μs** |
|       HashSetAndArray |        0 |     1000 |       3: sqrt(range) |     4: 2*sqrt(range) |     11.662 μs |   0.2604 μs |   0.7215 μs |     11.800 μs |
| SetHashLinkedAndArray |        0 |     1000 |       3: sqrt(range) |     4: 2*sqrt(range) |     15.591 μs |   0.3128 μs |   0.6100 μs |     15.800 μs |
|  RelativelySimpleCode |        0 |     1000 |       3: sqrt(range) |     4: 2*sqrt(range) |    727.754 μs |  14.4715 μs |  25.3457 μs |    720.400 μs |
|                 **Towel** |        **0** |     **1000** |     **4: 2*sqrt(range)** | **1: sqrt(sqrt(range))** |      **2.947 μs** |   **0.0601 μs** |   **0.0900 μs** |      **2.900 μs** |
|       HashSetAndArray |        0 |     1000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |      7.331 μs |   0.0899 μs |   0.0751 μs |      7.300 μs |
| SetHashLinkedAndArray |        0 |     1000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |      9.931 μs |   0.1498 μs |   0.1251 μs |      9.900 μs |
|  RelativelySimpleCode |        0 |     1000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |  1,212.062 μs |  23.9207 μs |  32.7429 μs |  1,204.050 μs |
|                 **Towel** |        **0** |     **1000** |     **4: 2*sqrt(range)** |    **2: .5*sqrt(range)** |      **3.338 μs** |   **0.0679 μs** |   **0.1402 μs** |      **3.300 μs** |
|       HashSetAndArray |        0 |     1000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |      8.444 μs |   0.1690 μs |   0.2256 μs |      8.400 μs |
| SetHashLinkedAndArray |        0 |     1000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |     11.272 μs |   0.2122 μs |   0.2270 μs |     11.300 μs |
|  RelativelySimpleCode |        0 |     1000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |  1,293.224 μs |  23.7190 μs |  66.1190 μs |  1,272.550 μs |
|                 **Towel** |        **0** |     **1000** |     **4: 2*sqrt(range)** |       **3: sqrt(range)** |     **14.533 μs** |   **0.2857 μs** |   **0.4532 μs** |     **14.500 μs** |
|       HashSetAndArray |        0 |     1000 |     4: 2*sqrt(range) |       3: sqrt(range) |      9.576 μs |   0.1885 μs |   0.3201 μs |      9.600 μs |
| SetHashLinkedAndArray |        0 |     1000 |     4: 2*sqrt(range) |       3: sqrt(range) |     12.951 μs |   0.2554 μs |   0.4196 μs |     13.100 μs |
|  RelativelySimpleCode |        0 |     1000 |     4: 2*sqrt(range) |       3: sqrt(range) |  1,397.737 μs |  27.3040 μs |  35.5030 μs |  1,388.450 μs |
|                 **Towel** |        **0** |     **1000** |     **4: 2*sqrt(range)** |     **4: 2*sqrt(range)** |     **17.212 μs** |   **0.3441 μs** |   **0.8112 μs** |     **17.400 μs** |
|       HashSetAndArray |        0 |     1000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |     11.943 μs |   0.2453 μs |   0.6673 μs |     12.100 μs |
| SetHashLinkedAndArray |        0 |     1000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |     15.935 μs |   0.3154 μs |   0.5355 μs |     16.100 μs |
|  RelativelySimpleCode |        0 |     1000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |  1,515.784 μs |  29.9331 μs |  78.8557 μs |  1,484.700 μs |
|                 **Towel** |        **0** |    **10000** | **1: sqrt(sqrt(range))** | **1: sqrt(sqrt(range))** |      **2.004 μs** |   **0.0369 μs** |   **0.0518 μs** |      **2.000 μs** |
|       HashSetAndArray |        0 |    10000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |     53.715 μs |   0.4230 μs |   0.3532 μs |     53.800 μs |
| SetHashLinkedAndArray |        0 |    10000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |     70.969 μs |   1.3699 μs |   1.3455 μs |     70.400 μs |
|  RelativelySimpleCode |        0 |    10000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** | **1: sqrt(sqrt(range))** |    **2: .5*sqrt(range)** |      **3.514 μs** |   **0.0666 μs** |   **0.0793 μs** |      **3.500 μs** |
|       HashSetAndArray |        0 |    10000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |     56.362 μs |   0.7723 μs |   0.6449 μs |     56.400 μs |
| SetHashLinkedAndArray |        0 |    10000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |     86.726 μs |   1.3466 μs |   1.7030 μs |     86.600 μs |
|  RelativelySimpleCode |        0 |    10000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** | **1: sqrt(sqrt(range))** |       **3: sqrt(range)** |      **6.529 μs** |   **0.1031 μs** |   **0.0914 μs** |      **6.500 μs** |
|       HashSetAndArray |        0 |    10000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |     61.636 μs |   1.1828 μs |   1.0485 μs |     61.750 μs |
| SetHashLinkedAndArray |        0 |    10000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |     86.614 μs |   1.5108 μs |   1.3393 μs |     86.100 μs |
|  RelativelySimpleCode |        0 |    10000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** | **1: sqrt(sqrt(range))** |     **4: 2*sqrt(range)** |    **105.662 μs** |   **2.0411 μs** |   **2.0046 μs** |    **106.050 μs** |
|       HashSetAndArray |        0 |    10000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |     69.785 μs |   1.3669 μs |   2.4297 μs |     70.400 μs |
| SetHashLinkedAndArray |        0 |    10000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |     99.709 μs |   1.9692 μs |   3.0659 μs |    100.050 μs |
|  RelativelySimpleCode |        0 |    10000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** |    **2: .5*sqrt(range)** | **1: sqrt(sqrt(range))** |      **2.864 μs** |   **0.0576 μs** |   **0.0826 μs** |      **2.900 μs** |
|       HashSetAndArray |        0 |    10000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |     53.100 μs |   0.2185 μs |   0.1706 μs |     53.000 μs |
| SetHashLinkedAndArray |        0 |    10000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |     72.514 μs |   0.2293 μs |   0.2033 μs |     72.550 μs |
|  RelativelySimpleCode |        0 |    10000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** |    **2: .5*sqrt(range)** |    **2: .5*sqrt(range)** |      **5.081 μs** |   **0.0999 μs** |   **0.0981 μs** |      **5.100 μs** |
|       HashSetAndArray |        0 |    10000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |     56.320 μs |   0.8154 μs |   0.7627 μs |     56.300 μs |
| SetHashLinkedAndArray |        0 |    10000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |    101.348 μs |   1.9612 μs |   2.4803 μs |    100.500 μs |
|  RelativelySimpleCode |        0 |    10000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** |    **2: .5*sqrt(range)** |       **3: sqrt(range)** |      **9.177 μs** |   **0.1828 μs** |   **0.2245 μs** |      **9.200 μs** |
|       HashSetAndArray |        0 |    10000 |    2: .5*sqrt(range) |       3: sqrt(range) |     62.755 μs |   1.1864 μs |   1.3663 μs |     62.850 μs |
| SetHashLinkedAndArray |        0 |    10000 |    2: .5*sqrt(range) |       3: sqrt(range) |     88.705 μs |   1.7552 μs |   1.9509 μs |     89.200 μs |
|  RelativelySimpleCode |        0 |    10000 |    2: .5*sqrt(range) |       3: sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** |    **2: .5*sqrt(range)** |     **4: 2*sqrt(range)** |    **106.223 μs** |   **1.9980 μs** |   **1.6684 μs** |    **106.800 μs** |
|       HashSetAndArray |        0 |    10000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |     71.912 μs |   0.7453 μs |   0.7320 μs |     72.050 μs |
| SetHashLinkedAndArray |        0 |    10000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |    101.760 μs |   1.9666 μs |   1.8396 μs |    102.000 μs |
|  RelativelySimpleCode |        0 |    10000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** |       **3: sqrt(range)** | **1: sqrt(sqrt(range))** |      **3.891 μs** |   **0.0791 μs** |   **0.0971 μs** |      **3.900 μs** |
|       HashSetAndArray |        0 |    10000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |     55.085 μs |   0.3382 μs |   0.2824 μs |     55.100 μs |
| SetHashLinkedAndArray |        0 |    10000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |     71.943 μs |   1.2155 μs |   1.0775 μs |     71.350 μs |
|  RelativelySimpleCode |        0 |    10000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** |       **3: sqrt(range)** |    **2: .5*sqrt(range)** |      **6.961 μs** |   **0.1327 μs** |   **0.1420 μs** |      **6.900 μs** |
|       HashSetAndArray |        0 |    10000 |       3: sqrt(range) |    2: .5*sqrt(range) |     60.326 μs |   1.8014 μs |   4.9918 μs |     57.800 μs |
| SetHashLinkedAndArray |        0 |    10000 |       3: sqrt(range) |    2: .5*sqrt(range) |     89.342 μs |   1.7600 μs |   1.3741 μs |     89.100 μs |
|  RelativelySimpleCode |        0 |    10000 |       3: sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** |       **3: sqrt(range)** |       **3: sqrt(range)** |     **96.508 μs** |   **1.5246 μs** |   **1.2731 μs** |     **96.200 μs** |
|       HashSetAndArray |        0 |    10000 |       3: sqrt(range) |       3: sqrt(range) |     63.000 μs |   0.7980 μs |   0.6230 μs |     62.900 μs |
| SetHashLinkedAndArray |        0 |    10000 |       3: sqrt(range) |       3: sqrt(range) |     86.746 μs |   0.8477 μs |   0.7078 μs |     87.000 μs |
|  RelativelySimpleCode |        0 |    10000 |       3: sqrt(range) |       3: sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** |       **3: sqrt(range)** |     **4: 2*sqrt(range)** |    **107.024 μs** |   **2.1003 μs** |   **3.3916 μs** |    **106.700 μs** |
|       HashSetAndArray |        0 |    10000 |       3: sqrt(range) |     4: 2*sqrt(range) |     75.882 μs |   2.7361 μs |   7.8504 μs |     72.500 μs |
| SetHashLinkedAndArray |        0 |    10000 |       3: sqrt(range) |     4: 2*sqrt(range) |    100.015 μs |   1.3485 μs |   1.1261 μs |    100.000 μs |
|  RelativelySimpleCode |        0 |    10000 |       3: sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** |     **4: 2*sqrt(range)** | **1: sqrt(sqrt(range))** |      **5.833 μs** |   **0.1043 μs** |   **0.0976 μs** |      **5.800 μs** |
|       HashSetAndArray |        0 |    10000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |     55.967 μs |   0.4448 μs |   0.3473 μs |     56.050 μs |
| SetHashLinkedAndArray |        0 |    10000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |     73.581 μs |   1.4294 μs |   1.4039 μs |     73.800 μs |
|  RelativelySimpleCode |        0 |    10000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** |     **4: 2*sqrt(range)** |    **2: .5*sqrt(range)** |     **97.243 μs** |   **1.3870 μs** |   **1.2296 μs** |     **97.500 μs** |
|       HashSetAndArray |        0 |    10000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |     58.592 μs |   0.4744 μs |   0.3704 μs |     58.550 μs |
| SetHashLinkedAndArray |        0 |    10000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |     90.142 μs |   0.9356 μs |   0.7305 μs |     90.200 μs |
|  RelativelySimpleCode |        0 |    10000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** |     **4: 2*sqrt(range)** |       **3: sqrt(range)** |     **96.775 μs** |   **1.8872 μs** |   **2.1733 μs** |     **96.550 μs** |
|       HashSetAndArray |        0 |    10000 |     4: 2*sqrt(range) |       3: sqrt(range) |     62.644 μs |   1.1978 μs |   1.1764 μs |     62.750 μs |
| SetHashLinkedAndArray |        0 |    10000 |     4: 2*sqrt(range) |       3: sqrt(range) |     89.880 μs |   1.4864 μs |   1.3904 μs |     90.000 μs |
|  RelativelySimpleCode |        0 |    10000 |     4: 2*sqrt(range) |       3: sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |    **10000** |     **4: 2*sqrt(range)** |     **4: 2*sqrt(range)** |    **106.025 μs** |   **2.1000 μs** |   **2.0625 μs** |    **104.900 μs** |
|       HashSetAndArray |        0 |    10000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |     73.335 μs |   1.4513 μs |   1.4904 μs |     73.300 μs |
| SetHashLinkedAndArray |        0 |    10000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |    105.311 μs |   2.3478 μs |   6.7363 μs |    103.200 μs |
|  RelativelySimpleCode |        0 |    10000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** | **1: sqrt(sqrt(range))** | **1: sqrt(sqrt(range))** |      **2.427 μs** |   **0.0489 μs** |   **0.0458 μs** |      **2.400 μs** |
|       HashSetAndArray |        0 |   100000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |    524.860 μs |  10.0774 μs |  11.6051 μs |    520.850 μs |
| SetHashLinkedAndArray |        0 |   100000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |    680.913 μs |  12.6962 μs |  11.8760 μs |    677.400 μs |
|  RelativelySimpleCode |        0 |   100000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** | **1: sqrt(sqrt(range))** |    **2: .5*sqrt(range)** |     **13.647 μs** |   **0.0891 μs** |   **0.0834 μs** |     **13.600 μs** |
|       HashSetAndArray |        0 |   100000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |    539.178 μs |  10.2282 μs |  10.9441 μs |    535.100 μs |
| SetHashLinkedAndArray |        0 |   100000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |    770.206 μs |  15.2810 μs |  31.2151 μs |    760.200 μs |
|  RelativelySimpleCode |        0 |   100000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** | **1: sqrt(sqrt(range))** |       **3: sqrt(range)** |     **39.471 μs** |   **0.6464 μs** |   **0.5730 μs** |     **39.200 μs** |
|       HashSetAndArray |        0 |   100000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |    555.146 μs |   9.4716 μs |   7.9092 μs |    551.100 μs |
| SetHashLinkedAndArray |        0 |   100000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |    875.579 μs |  16.7006 μs |  18.5627 μs |    866.000 μs |
|  RelativelySimpleCode |        0 |   100000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** | **1: sqrt(sqrt(range))** |     **4: 2*sqrt(range)** |  **1,096.450 μs** |  **21.5534 μs** |  **19.1065 μs** |  **1,103.000 μs** |
|       HashSetAndArray |        0 |   100000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |    663.008 μs |  13.1224 μs |  26.5079 μs |    655.900 μs |
| SetHashLinkedAndArray |        0 |   100000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |  1,102.638 μs |  21.7993 μs |  21.4098 μs |  1,099.150 μs |
|  RelativelySimpleCode |        0 |   100000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** |    **2: .5*sqrt(range)** | **1: sqrt(sqrt(range))** |      **5.841 μs** |   **0.1036 μs** |   **0.1064 μs** |      **5.800 μs** |
|       HashSetAndArray |        0 |   100000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |    524.432 μs |   8.3138 μs |  10.2101 μs |    523.100 μs |
| SetHashLinkedAndArray |        0 |   100000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |    675.000 μs |  13.3210 μs |  13.6796 μs |    679.600 μs |
|  RelativelySimpleCode |        0 |   100000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** |    **2: .5*sqrt(range)** |    **2: .5*sqrt(range)** |     **25.231 μs** |   **0.3767 μs** |   **0.3146 μs** |     **25.300 μs** |
|       HashSetAndArray |        0 |   100000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |    545.120 μs |   9.9308 μs |  19.1332 μs |    537.650 μs |
| SetHashLinkedAndArray |        0 |   100000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |    802.692 μs |   9.7244 μs |   7.5922 μs |    803.650 μs |
|  RelativelySimpleCode |        0 |   100000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** |    **2: .5*sqrt(range)** |       **3: sqrt(range)** |     **62.136 μs** |   **1.0018 μs** |   **0.8880 μs** |     **62.050 μs** |
|       HashSetAndArray |        0 |   100000 |    2: .5*sqrt(range) |       3: sqrt(range) |    552.778 μs |   9.6573 μs |  13.5382 μs |    548.700 μs |
| SetHashLinkedAndArray |        0 |   100000 |    2: .5*sqrt(range) |       3: sqrt(range) |    892.917 μs |  12.1160 μs |   9.4594 μs |    890.950 μs |
|  RelativelySimpleCode |        0 |   100000 |    2: .5*sqrt(range) |       3: sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** |    **2: .5*sqrt(range)** |     **4: 2*sqrt(range)** |  **1,121.787 μs** |  **21.6563 μs** |  **21.2694 μs** |  **1,123.800 μs** |
|       HashSetAndArray |        0 |   100000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |    664.746 μs |  13.1093 μs |  23.6387 μs |    663.400 μs |
| SetHashLinkedAndArray |        0 |   100000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |  1,051.315 μs |  13.2168 μs |  11.0366 μs |  1,054.800 μs |
|  RelativelySimpleCode |        0 |   100000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** |       **3: sqrt(range)** | **1: sqrt(sqrt(range))** |      **9.300 μs** |   **0.1327 μs** |   **0.1177 μs** |      **9.300 μs** |
|       HashSetAndArray |        0 |   100000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |    535.879 μs |  10.6039 μs |  13.7881 μs |    532.350 μs |
| SetHashLinkedAndArray |        0 |   100000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |    679.583 μs |   9.7589 μs |   7.6191 μs |    678.900 μs |
|  RelativelySimpleCode |        0 |   100000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** |       **3: sqrt(range)** |    **2: .5*sqrt(range)** |     **38.846 μs** |   **0.5048 μs** |   **0.4215 μs** |     **38.900 μs** |
|       HashSetAndArray |        0 |   100000 |       3: sqrt(range) |    2: .5*sqrt(range) |    545.363 μs |  10.7937 μs |  11.9972 μs |    550.800 μs |
| SetHashLinkedAndArray |        0 |   100000 |       3: sqrt(range) |    2: .5*sqrt(range) |    817.379 μs |  14.6204 μs |  12.9606 μs |    813.700 μs |
|  RelativelySimpleCode |        0 |   100000 |       3: sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** |       **3: sqrt(range)** |       **3: sqrt(range)** |    **910.889 μs** |  **17.8170 μs** |  **37.5820 μs** |    **899.000 μs** |
|       HashSetAndArray |        0 |   100000 |       3: sqrt(range) |       3: sqrt(range) |    553.225 μs |  10.9406 μs |  12.5992 μs |    546.250 μs |
| SetHashLinkedAndArray |        0 |   100000 |       3: sqrt(range) |       3: sqrt(range) |    875.715 μs |  15.5906 μs |  13.0188 μs |    872.600 μs |
|  RelativelySimpleCode |        0 |   100000 |       3: sqrt(range) |       3: sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** |       **3: sqrt(range)** |     **4: 2*sqrt(range)** |  **1,099.431 μs** |   **7.3959 μs** |   **6.1759 μs** |  **1,098.100 μs** |
|       HashSetAndArray |        0 |   100000 |       3: sqrt(range) |     4: 2*sqrt(range) |    669.352 μs |  11.8931 μs |  17.4328 μs |    669.700 μs |
| SetHashLinkedAndArray |        0 |   100000 |       3: sqrt(range) |     4: 2*sqrt(range) |  1,117.325 μs |  21.6687 μs |  16.9175 μs |  1,123.750 μs |
|  RelativelySimpleCode |        0 |   100000 |       3: sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** |     **4: 2*sqrt(range)** | **1: sqrt(sqrt(range))** |     **16.247 μs** |   **0.2796 μs** |   **0.2615 μs** |     **16.300 μs** |
|       HashSetAndArray |        0 |   100000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |    536.687 μs |  10.6602 μs |   9.9716 μs |    538.400 μs |
| SetHashLinkedAndArray |        0 |   100000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |    691.975 μs |  11.1183 μs |   8.6804 μs |    691.250 μs |
|  RelativelySimpleCode |        0 |   100000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** |     **4: 2*sqrt(range)** |    **2: .5*sqrt(range)** |    **874.665 μs** |  **15.3002 μs** |  **20.9431 μs** |    **866.200 μs** |
|       HashSetAndArray |        0 |   100000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |    547.586 μs |  10.4517 μs |   9.2651 μs |    550.150 μs |
| SetHashLinkedAndArray |        0 |   100000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |    815.115 μs |  16.0057 μs |  22.4378 μs |    812.300 μs |
|  RelativelySimpleCode |        0 |   100000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** |     **4: 2*sqrt(range)** |       **3: sqrt(range)** |    **912.467 μs** |  **14.9146 μs** |  **11.6444 μs** |    **915.200 μs** |
|       HashSetAndArray |        0 |   100000 |     4: 2*sqrt(range) |       3: sqrt(range) |    557.123 μs |   9.3225 μs |   7.7847 μs |    553.700 μs |
| SetHashLinkedAndArray |        0 |   100000 |     4: 2*sqrt(range) |       3: sqrt(range) |    887.358 μs |  11.1361 μs |   8.6944 μs |    887.350 μs |
|  RelativelySimpleCode |        0 |   100000 |     4: 2*sqrt(range) |       3: sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |   **100000** |     **4: 2*sqrt(range)** |     **4: 2*sqrt(range)** |  **1,141.622 μs** |  **22.6852 μs** |  **58.1507 μs** |  **1,122.700 μs** |
|       HashSetAndArray |        0 |   100000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |    675.462 μs |  13.2631 μs |  20.6490 μs |    673.750 μs |
| SetHashLinkedAndArray |        0 |   100000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |  1,121.169 μs |  22.0665 μs |  30.2049 μs |  1,113.100 μs |
|  RelativelySimpleCode |        0 |   100000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** | **1: sqrt(sqrt(range))** | **1: sqrt(sqrt(range))** |      **3.267 μs** |   **0.0656 μs** |   **0.0920 μs** |      **3.300 μs** |
|       HashSetAndArray |        0 |  1000000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |  6,053.645 μs | 119.1924 μs | 271.4614 μs |  5,997.050 μs |
| SetHashLinkedAndArray |        0 |  1000000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |  6,083.168 μs | 121.3303 μs | 308.8243 μs |  6,044.200 μs |
|  RelativelySimpleCode |        0 |  1000000 | 1: sqrt(sqrt(range)) | 1: sqrt(sqrt(range)) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** | **1: sqrt(sqrt(range))** |    **2: .5*sqrt(range)** |     **95.025 μs** |   **0.2683 μs** |   **0.2094 μs** |     **95.000 μs** |
|       HashSetAndArray |        0 |  1000000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |  6,491.652 μs | 129.2164 μs | 248.9562 μs |  6,501.550 μs |
| SetHashLinkedAndArray |        0 |  1000000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |  6,667.963 μs | 132.5852 μs | 325.2337 μs |  6,610.300 μs |
|  RelativelySimpleCode |        0 |  1000000 | 1: sqrt(sqrt(range)) |    2: .5*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** | **1: sqrt(sqrt(range))** |       **3: sqrt(range)** |    **356.648 μs** |   **6.9418 μs** |   **8.7792 μs** |    **356.600 μs** |
|       HashSetAndArray |        0 |  1000000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |  8,722.720 μs | 172.4893 μs | 258.1738 μs |  8,658.600 μs |
| SetHashLinkedAndArray |        0 |  1000000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |  8,656.161 μs | 170.6520 μs | 294.3663 μs |  8,626.500 μs |
|  RelativelySimpleCode |        0 |  1000000 | 1: sqrt(sqrt(range)) |       3: sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** | **1: sqrt(sqrt(range))** |     **4: 2*sqrt(range)** |  **9,361.467 μs** | **182.8541 μs** | **334.3591 μs** |  **9,296.100 μs** |
|       HashSetAndArray |        0 |  1000000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) | 11,716.018 μs | 234.2586 μs | 371.5590 μs | 11,674.600 μs |
| SetHashLinkedAndArray |        0 |  1000000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |  9,491.198 μs | 186.1313 μs | 367.4050 μs |  9,421.450 μs |
|  RelativelySimpleCode |        0 |  1000000 | 1: sqrt(sqrt(range)) |     4: 2*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** |    **2: .5*sqrt(range)** | **1: sqrt(sqrt(range))** |     **16.987 μs** |   **0.1846 μs** |   **0.1727 μs** |     **17.000 μs** |
|       HashSetAndArray |        0 |  1000000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |  6,019.413 μs | 114.7013 μs | 236.8780 μs |  6,039.600 μs |
| SetHashLinkedAndArray |        0 |  1000000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |  6,053.164 μs | 114.0854 μs | 252.8055 μs |  6,026.100 μs |
|  RelativelySimpleCode |        0 |  1000000 |    2: .5*sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** |    **2: .5*sqrt(range)** |    **2: .5*sqrt(range)** |    **222.214 μs** |   **4.3564 μs** |   **3.8618 μs** |    **220.900 μs** |
|       HashSetAndArray |        0 |  1000000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |  6,403.532 μs | 127.7928 μs | 220.4363 μs |  6,338.800 μs |
| SetHashLinkedAndArray |        0 |  1000000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |  6,828.072 μs | 136.3601 μs | 238.8237 μs |  6,796.700 μs |
|  RelativelySimpleCode |        0 |  1000000 |    2: .5*sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** |    **2: .5*sqrt(range)** |       **3: sqrt(range)** |    **616.496 μs** |  **12.1839 μs** |  **16.6774 μs** |    **610.650 μs** |
|       HashSetAndArray |        0 |  1000000 |    2: .5*sqrt(range) |       3: sqrt(range) |  8,733.976 μs | 170.0199 μs | 249.2129 μs |  8,698.900 μs |
| SetHashLinkedAndArray |        0 |  1000000 |    2: .5*sqrt(range) |       3: sqrt(range) |  8,820.931 μs | 156.9041 μs | 262.1514 μs |  8,845.350 μs |
|  RelativelySimpleCode |        0 |  1000000 |    2: .5*sqrt(range) |       3: sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** |    **2: .5*sqrt(range)** |     **4: 2*sqrt(range)** |  **9,248.879 μs** | **161.2262 μs** | **282.3746 μs** |  **9,278.400 μs** |
|       HashSetAndArray |        0 |  1000000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) | 11,650.367 μs | 224.4841 μs | 209.9826 μs | 11,714.900 μs |
| SetHashLinkedAndArray |        0 |  1000000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |  9,321.876 μs | 186.2952 μs | 321.3500 μs |  9,309.100 μs |
|  RelativelySimpleCode |        0 |  1000000 |    2: .5*sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** |       **3: sqrt(range)** | **1: sqrt(sqrt(range))** |     **31.875 μs** |   **0.4164 μs** |   **0.3251 μs** |     **32.000 μs** |
|       HashSetAndArray |        0 |  1000000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |  6,034.660 μs | 118.7386 μs | 245.2158 μs |  6,023.300 μs |
| SetHashLinkedAndArray |        0 |  1000000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |  5,963.119 μs | 118.2907 μs | 285.6853 μs |  5,924.700 μs |
|  RelativelySimpleCode |        0 |  1000000 |       3: sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** |       **3: sqrt(range)** |    **2: .5*sqrt(range)** |    **351.460 μs** |   **6.6617 μs** |   **6.2313 μs** |    **350.500 μs** |
|       HashSetAndArray |        0 |  1000000 |       3: sqrt(range) |    2: .5*sqrt(range) |  6,441.673 μs | 127.1867 μs | 190.3671 μs |  6,439.950 μs |
| SetHashLinkedAndArray |        0 |  1000000 |       3: sqrt(range) |    2: .5*sqrt(range) |  6,829.823 μs | 136.1020 μs | 284.0955 μs |  6,769.900 μs |
|  RelativelySimpleCode |        0 |  1000000 |       3: sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** |       **3: sqrt(range)** |       **3: sqrt(range)** |  **8,917.100 μs** | **176.4264 μs** | **339.9138 μs** |  **8,857.700 μs** |
|       HashSetAndArray |        0 |  1000000 |       3: sqrt(range) |       3: sqrt(range) |  8,728.950 μs | 173.6349 μs | 225.7745 μs |  8,716.250 μs |
| SetHashLinkedAndArray |        0 |  1000000 |       3: sqrt(range) |       3: sqrt(range) |  8,763.140 μs | 175.0016 μs | 341.3272 μs |  8,731.400 μs |
|  RelativelySimpleCode |        0 |  1000000 |       3: sqrt(range) |       3: sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** |       **3: sqrt(range)** |     **4: 2*sqrt(range)** |  **9,488.315 μs** | **187.8222 μs** | **416.2011 μs** |  **9,446.400 μs** |
|       HashSetAndArray |        0 |  1000000 |       3: sqrt(range) |     4: 2*sqrt(range) | 11,499.000 μs | 204.2725 μs | 170.5768 μs | 11,511.700 μs |
| SetHashLinkedAndArray |        0 |  1000000 |       3: sqrt(range) |     4: 2*sqrt(range) |  9,424.010 μs | 186.2230 μs | 420.3364 μs |  9,328.800 μs |
|  RelativelySimpleCode |        0 |  1000000 |       3: sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** |     **4: 2*sqrt(range)** | **1: sqrt(sqrt(range))** |     **61.900 μs** |   **1.1897 μs** |   **1.5045 μs** |     **61.200 μs** |
|       HashSetAndArray |        0 |  1000000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |  6,127.917 μs | 122.1617 μs | 295.0343 μs |  6,133.500 μs |
| SetHashLinkedAndArray |        0 |  1000000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |  6,693.298 μs | 305.4066 μs | 900.4984 μs |  6,174.100 μs |
|  RelativelySimpleCode |        0 |  1000000 |     4: 2*sqrt(range) | 1: sqrt(sqrt(range)) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** |     **4: 2*sqrt(range)** |    **2: .5*sqrt(range)** |  **6,860.860 μs** | **136.6719 μs** | **332.6782 μs** |  **6,787.750 μs** |
|       HashSetAndArray |        0 |  1000000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |  6,546.634 μs | 123.3613 μs | 270.7811 μs |  6,523.800 μs |
| SetHashLinkedAndArray |        0 |  1000000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |  7,143.450 μs | 142.2543 μs | 293.7797 μs |  7,094.900 μs |
|  RelativelySimpleCode |        0 |  1000000 |     4: 2*sqrt(range) |    2: .5*sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** |     **4: 2*sqrt(range)** |       **3: sqrt(range)** |  **9,037.810 μs** | **178.5877 μs** | **364.8073 μs** |  **8,969.600 μs** |
|       HashSetAndArray |        0 |  1000000 |     4: 2*sqrt(range) |       3: sqrt(range) |  8,797.000 μs | 175.2316 μs | 329.1275 μs |  8,747.050 μs |
| SetHashLinkedAndArray |        0 |  1000000 |     4: 2*sqrt(range) |       3: sqrt(range) |  9,003.493 μs | 178.7574 μs | 344.4048 μs |  8,944.350 μs |
|  RelativelySimpleCode |        0 |  1000000 |     4: 2*sqrt(range) |       3: sqrt(range) |            NA |          NA |          NA |            NA |
|                 **Towel** |        **0** |  **1000000** |     **4: 2*sqrt(range)** |     **4: 2*sqrt(range)** |  **9,476.029 μs** | **183.9040 μs** | **375.6671 μs** |  **9,420.300 μs** |
|       HashSetAndArray |        0 |  1000000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) | 11,723.264 μs | 230.7824 μs | 385.5855 μs | 11,637.100 μs |
| SetHashLinkedAndArray |        0 |  1000000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |  9,434.100 μs | 188.2735 μs | 432.5896 μs |  9,291.300 μs |
|  RelativelySimpleCode |        0 |  1000000 |     4: 2*sqrt(range) |     4: 2*sqrt(range) |            NA |          NA |          NA |            NA |

Benchmarks with issues:
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=1: sqrt(sqrt(range)), Exclued=1: sqrt(sqrt(range))]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=1: sqrt(sqrt(range)), Exclued=2: .5*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=1: sqrt(sqrt(range)), Exclued=3: sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=1: sqrt(sqrt(range)), Exclued=4: 2*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=2: .5*sqrt(range), Exclued=1: sqrt(sqrt(range))]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=2: .5*sqrt(range), Exclued=2: .5*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=2: .5*sqrt(range), Exclued=3: sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=2: .5*sqrt(range), Exclued=4: 2*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=3: sqrt(range), Exclued=1: sqrt(sqrt(range))]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=3: sqrt(range), Exclued=2: .5*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=3: sqrt(range), Exclued=3: sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=3: sqrt(range), Exclued=4: 2*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=4: 2*sqrt(range), Exclued=1: sqrt(sqrt(range))]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=4: 2*sqrt(range), Exclued=2: .5*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=4: 2*sqrt(range), Exclued=3: sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=10000, Count=4: 2*sqrt(range), Exclued=4: 2*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=1: sqrt(sqrt(range)), Exclued=1: sqrt(sqrt(range))]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=1: sqrt(sqrt(range)), Exclued=2: .5*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=1: sqrt(sqrt(range)), Exclued=3: sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=1: sqrt(sqrt(range)), Exclued=4: 2*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=2: .5*sqrt(range), Exclued=1: sqrt(sqrt(range))]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=2: .5*sqrt(range), Exclued=2: .5*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=2: .5*sqrt(range), Exclued=3: sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=2: .5*sqrt(range), Exclued=4: 2*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=3: sqrt(range), Exclued=1: sqrt(sqrt(range))]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=3: sqrt(range), Exclued=2: .5*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=3: sqrt(range), Exclued=3: sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=3: sqrt(range), Exclued=4: 2*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=4: 2*sqrt(range), Exclued=1: sqrt(sqrt(range))]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=4: 2*sqrt(range), Exclued=2: .5*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=4: 2*sqrt(range), Exclued=3: sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=100000, Count=4: 2*sqrt(range), Exclued=4: 2*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=1: sqrt(sqrt(range)), Exclued=1: sqrt(sqrt(range))]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=1: sqrt(sqrt(range)), Exclued=2: .5*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=1: sqrt(sqrt(range)), Exclued=3: sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=1: sqrt(sqrt(range)), Exclued=4: 2*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=2: .5*sqrt(range), Exclued=1: sqrt(sqrt(range))]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=2: .5*sqrt(range), Exclued=2: .5*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=2: .5*sqrt(range), Exclued=3: sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=2: .5*sqrt(range), Exclued=4: 2*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=3: sqrt(range), Exclued=1: sqrt(sqrt(range))]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=3: sqrt(range), Exclued=2: .5*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=3: sqrt(range), Exclued=3: sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=3: sqrt(range), Exclued=4: 2*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=4: 2*sqrt(range), Exclued=1: sqrt(sqrt(range))]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=4: 2*sqrt(range), Exclued=2: .5*sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=4: 2*sqrt(range), Exclued=3: sqrt(range)]
  RandomWithExclusionsBenchmarks.RelativelySimpleCode: Job-TGYVEK(InvocationCount=1, UnrollFactor=1) [MinValue=0, MaxValue=1000000, Count=4: 2*sqrt(range), Exclued=4: 2*sqrt(range)]

