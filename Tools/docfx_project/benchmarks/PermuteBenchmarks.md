# Permute

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
|          Method |  N |               Mean |            Error |            StdDev |             Median |
|---------------- |--- |-------------------:|-----------------:|------------------:|-------------------:|
|       **Recursive** |  **1** |           **166.3 ns** |         **20.85 ns** |          **60.83 ns** |           **200.0 ns** |
|       Iterative |  1 |           962.2 ns |         24.39 ns |          68.00 ns |         1,000.0 ns |
| RecursiveStruct |  1 |           150.0 ns |          0.00 ns |           0.00 ns |           150.0 ns |
| IterativeStruct |  1 |           968.1 ns |         25.43 ns |          71.30 ns |         1,000.0 ns |
|       **Recursive** |  **2** |           **280.2 ns** |         **13.88 ns** |          **40.05 ns** |           **300.0 ns** |
|       Iterative |  2 |         1,006.5 ns |         34.00 ns |          96.47 ns |         1,000.0 ns |
| RecursiveStruct |  2 |           200.0 ns |          0.00 ns |           0.00 ns |           200.0 ns |
| IterativeStruct |  2 |           939.8 ns |         23.32 ns |          66.17 ns |           900.0 ns |
|       **Recursive** |  **3** |           **352.0 ns** |         **17.70 ns** |          **52.18 ns** |           **400.0 ns** |
|       Iterative |  3 |         1,050.0 ns |         24.20 ns |          67.46 ns |         1,000.0 ns |
| RecursiveStruct |  3 |           300.0 ns |          0.00 ns |           0.00 ns |           300.0 ns |
| IterativeStruct |  3 |         1,015.8 ns |         27.53 ns |          78.98 ns |         1,000.0 ns |
|       **Recursive** |  **4** |           **550.5 ns** |         **20.59 ns** |          **59.73 ns** |           **500.0 ns** |
|       Iterative |  4 |         1,175.0 ns |         25.38 ns |          69.89 ns |         1,200.0 ns |
| RecursiveStruct |  4 |           500.0 ns |          0.00 ns |           0.00 ns |           500.0 ns |
| IterativeStruct |  4 |         1,083.7 ns |         31.22 ns |          88.05 ns |         1,100.0 ns |
|       **Recursive** |  **5** |         **1,382.4 ns** |         **23.96 ns** |          **38.70 ns** |         **1,400.0 ns** |
|       Iterative |  5 |         1,596.6 ns |         39.10 ns |         107.69 ns |         1,600.0 ns |
| RecursiveStruct |  5 |         1,268.9 ns |         24.61 ns |          46.82 ns |         1,300.0 ns |
| IterativeStruct |  5 |         1,413.6 ns |         43.41 ns |         119.56 ns |         1,400.0 ns |
|       **Recursive** |  **6** |         **6,771.4 ns** |        **112.18 ns** |          **99.45 ns** |         **6,700.0 ns** |
|       Iterative |  6 |         4,129.0 ns |         83.16 ns |         127.00 ns |         4,100.0 ns |
| RecursiveStruct |  6 |         6,150.0 ns |        117.57 ns |         115.47 ns |         6,100.0 ns |
| IterativeStruct |  6 |         3,071.2 ns |         62.74 ns |         139.02 ns |         3,000.0 ns |
|       **Recursive** |  **7** |        **44,976.9 ns** |        **121.28 ns** |         **101.27 ns** |        **44,900.0 ns** |
|       Iterative |  7 |        21,766.7 ns |        376.17 ns |         351.87 ns |        21,800.0 ns |
| RecursiveStruct |  7 |        40,836.8 ns |        638.44 ns |         709.62 ns |        40,900.0 ns |
| IterativeStruct |  7 |        14,460.9 ns |        288.80 ns |         365.24 ns |        14,400.0 ns |
|       **Recursive** |  **8** |       **359,323.1 ns** |      **4,958.63 ns** |       **4,140.68 ns** |       **358,700.0 ns** |
|       Iterative |  8 |       165,626.7 ns |      3,247.52 ns |       3,037.73 ns |       165,000.0 ns |
| RecursiveStruct |  8 |       333,953.1 ns |      6,665.80 ns |      19,232.35 ns |       325,750.0 ns |
| IterativeStruct |  8 |       107,428.6 ns |      2,136.82 ns |       4,267.46 ns |       105,500.0 ns |
|       **Recursive** |  **9** |     **3,305,906.2 ns** |     **65,816.35 ns** |     **172,229.91 ns** |     **3,267,500.0 ns** |
|       Iterative |  9 |     1,519,943.1 ns |     29,304.00 ns |      64,323.00 ns |     1,501,950.0 ns |
| RecursiveStruct |  9 |     2,986,047.3 ns |     58,014.04 ns |     123,632.65 ns |     2,969,900.0 ns |
| IterativeStruct |  9 |       975,316.5 ns |     18,646.06 ns |      48,463.60 ns |       968,400.0 ns |
|       **Recursive** | **10** |    **31,801,242.9 ns** |    **530,535.11 ns** |     **470,305.56 ns** |    **31,582,700.0 ns** |
|       Iterative | 10 |    14,945,014.8 ns |    293,314.58 ns |     411,186.07 ns |    14,910,000.0 ns |
| RecursiveStruct | 10 |    30,084,138.5 ns |    570,505.14 ns |     476,397.63 ns |    30,097,800.0 ns |
| IterativeStruct | 10 |     9,805,436.8 ns |    163,571.04 ns |     181,808.77 ns |     9,830,600.0 ns |
|       **Recursive** | **11** |   **349,864,792.3 ns** |  **3,421,849.76 ns** |   **2,857,399.53 ns** |   **349,054,900.0 ns** |
|       Iterative | 11 |   162,403,513.3 ns |  2,478,922.39 ns |   2,318,785.54 ns |   161,515,500.0 ns |
| RecursiveStruct | 11 |   329,529,384.6 ns |  3,945,566.33 ns |   3,294,726.58 ns |   328,491,000.0 ns |
| IterativeStruct | 11 |   107,825,753.8 ns |  1,043,092.96 ns |     871,029.87 ns |   107,468,900.0 ns |
|       **Recursive** | **12** | **4,294,935,545.5 ns** | **84,370,785.47 ns** | **133,821,006.55 ns** | **4,275,146,400.0 ns** |
|       Iterative | 12 | 1,968,375,632.0 ns | 32,182,765.88 ns |  42,963,068.06 ns | 1,950,699,600.0 ns |
| RecursiveStruct | 12 | 4,029,030,530.8 ns | 49,457,784.88 ns |  41,299,490.33 ns | 4,009,864,600.0 ns |
| IterativeStruct | 12 | 1,313,628,035.7 ns |  5,360,420.71 ns |   4,751,873.34 ns | 1,312,349,500.0 ns |

