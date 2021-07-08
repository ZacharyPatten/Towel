# Permute

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
|          Method |  N |               Mean |            Error |           StdDev |             Median |
|---------------- |--- |-------------------:|-----------------:|-----------------:|-------------------:|
|       **Recursive** |  **1** |           **300.0 ns** |          **0.00 ns** |          **0.00 ns** |           **300.0 ns** |
|       Iterative |  1 |           927.4 ns |         22.42 ns |         64.32 ns |           900.0 ns |
| RecursiveStruct |  1 |           200.0 ns |          0.00 ns |          0.00 ns |           200.0 ns |
| IterativeStruct |  1 |           945.7 ns |         22.86 ns |         55.65 ns |           900.0 ns |
|       **Recursive** |  **2** |           **335.4 ns** |         **16.38 ns** |         **48.05 ns** |           **300.0 ns** |
|       Iterative |  2 |         1,037.5 ns |         24.60 ns |         64.39 ns |         1,000.0 ns |
| RecursiveStruct |  2 |           286.5 ns |         11.92 ns |         34.40 ns |           300.0 ns |
| IterativeStruct |  2 |           954.8 ns |         26.25 ns |         74.48 ns |           900.0 ns |
|       **Recursive** |  **3** |           **536.4 ns** |         **16.49 ns** |         **48.35 ns** |           **500.0 ns** |
|       Iterative |  3 |         1,069.8 ns |         25.07 ns |         46.47 ns |         1,100.0 ns |
| RecursiveStruct |  3 |           494.6 ns |         13.50 ns |         22.92 ns |           500.0 ns |
| IterativeStruct |  3 |         1,027.1 ns |         24.47 ns |         66.17 ns |         1,000.0 ns |
|       **Recursive** |  **4** |         **1,093.8 ns** |         **25.45 ns** |         **25.00 ns** |         **1,100.0 ns** |
|       Iterative |  4 |         1,336.4 ns |         30.58 ns |         57.43 ns |         1,300.0 ns |
| RecursiveStruct |  4 |         1,013.6 ns |         24.11 ns |         66.40 ns |         1,000.0 ns |
| IterativeStruct |  4 |         1,241.8 ns |         28.51 ns |         67.75 ns |         1,200.0 ns |
|       **Recursive** |  **5** |         **4,021.4 ns** |         **48.03 ns** |         **42.58 ns** |         **4,000.0 ns** |
|       Iterative |  5 |         2,339.4 ns |         49.69 ns |         78.82 ns |         2,400.0 ns |
| RecursiveStruct |  5 |         3,878.6 ns |         48.03 ns |         42.58 ns |         3,900.0 ns |
| IterativeStruct |  5 |         2,042.2 ns |         44.10 ns |         83.91 ns |         2,100.0 ns |
|       **Recursive** |  **6** |        **21,308.3 ns** |        **221.57 ns** |        **172.99 ns** |        **21,250.0 ns** |
|       Iterative |  6 |         8,518.8 ns |        162.99 ns |        160.08 ns |         8,500.0 ns |
| RecursiveStruct |  6 |        20,815.4 ns |         95.88 ns |         80.06 ns |        20,800.0 ns |
| IterativeStruct |  6 |         7,343.8 ns |        141.42 ns |        138.89 ns |         7,350.0 ns |
|       **Recursive** |  **7** |       **148,616.7 ns** |      **2,807.17 ns** |      **2,191.65 ns** |       **148,850.0 ns** |
|       Iterative |  7 |        52,640.0 ns |        469.14 ns |        438.83 ns |        52,800.0 ns |
| RecursiveStruct |  7 |       143,308.3 ns |         65.95 ns |         51.49 ns |       143,300.0 ns |
| IterativeStruct |  7 |        45,082.6 ns |        897.97 ns |      1,135.64 ns |        44,900.0 ns |
|       **Recursive** |  **8** |     **1,234,143.8 ns** |     **24,388.49 ns** |     **23,952.76 ns** |     **1,231,700.0 ns** |
|       Iterative |  8 |       404,989.5 ns |      4,157.85 ns |      4,621.43 ns |       405,400.0 ns |
| RecursiveStruct |  8 |     1,174,680.6 ns |     23,003.65 ns |     38,433.93 ns |     1,163,500.0 ns |
| IterativeStruct |  8 |       351,466.7 ns |      6,846.22 ns |      7,325.38 ns |       350,350.0 ns |
|       **Recursive** |  **9** |     **5,231,354.4 ns** |    **291,317.03 ns** |    **812,075.43 ns** |     **5,548,050.0 ns** |
|       Iterative |  9 |     3,714,930.6 ns |     71,692.16 ns |    119,781.49 ns |     3,682,000.0 ns |
| RecursiveStruct |  9 |    11,040,000.0 ns |    212,311.93 ns |    208,518.70 ns |    11,026,050.0 ns |
| IterativeStruct |  9 |     3,180,016.2 ns |     52,608.12 ns |     89,332.58 ns |     3,153,700.0 ns |
|       **Recursive** | **10** |    **52,795,093.3 ns** |    **758,124.07 ns** |    **709,149.72 ns** |    **52,812,700.0 ns** |
|       Iterative | 10 |    18,701,213.3 ns |    321,308.77 ns |    300,552.42 ns |    18,603,500.0 ns |
| RecursiveStruct | 10 |    43,403,038.5 ns |    311,760.37 ns |    260,334.03 ns |    43,386,600.0 ns |
| IterativeStruct | 10 |    14,203,337.5 ns |    281,276.38 ns |    276,251.02 ns |    14,165,450.0 ns |
|       **Recursive** | **11** |   **463,013,520.7 ns** |  **4,149,460.30 ns** |  **9,108,165.94 ns** |   **460,350,250.0 ns** |
|       Iterative | 11 |   204,009,321.4 ns |  1,036,389.68 ns |    918,732.46 ns |   203,894,200.0 ns |
| RecursiveStruct | 11 |   479,163,326.7 ns |  1,853,621.78 ns |  1,733,878.97 ns |   478,468,600.0 ns |
| IterativeStruct | 11 |   153,796,864.3 ns |    805,313.29 ns |    713,889.25 ns |   153,857,250.0 ns |
|       **Recursive** | **12** | **5,689,652,386.7 ns** | **14,349,878.58 ns** | **13,422,885.33 ns** | **5,684,170,300.0 ns** |
|       Iterative | 12 | 2,449,465,946.7 ns |  6,993,208.45 ns |  6,541,451.53 ns | 2,449,544,900.0 ns |
| RecursiveStruct | 12 | 5,741,748,241.7 ns | 12,338,776.84 ns |  9,633,305.35 ns | 5,739,928,000.0 ns |
| IterativeStruct | 12 | 1,838,954,493.3 ns |  8,017,927.91 ns |  7,499,974.74 ns | 1,840,925,300.0 ns |

