```ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.630 (2004/?/20H1)
Intel Core i7-1065G7 CPU 1.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100
  [Host]     : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  DefaultJob : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT


```

| Nonogram | Size  |     Mean |    Error |   StdDev |     Gen 0 | Gen 1 | Gen 2 | Allocated |
| -------- | ----- | -------: | -------: | -------: | --------: | ----: | ----: | --------: |
| Swing    | 45x45 | 13.81 ms | 0.272 ms | 0.373 ms | 1546.8750 |     - |     - |    6.2 MB |
| Sun      | 50x60 | 42.57 ms | 0.708 ms | 1.202 ms | 4416.6667 |     - |     - |  17.83 MB |
| Tiger    | 75x50 | 62.36 ms | 1.214 ms | 1.247 ms | 6444.4444 |     - |     - |  26.09 MB |
