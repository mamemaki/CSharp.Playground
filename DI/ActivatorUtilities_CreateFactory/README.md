## ActivatorUtilities_CreateFactory

Test `ActivatorUtilities.CreateFactory` performance.

```
| Method            | CreateCount | Mean        | Error     | StdDev    | Median      | Ratio | RatioSD |
|------------------ |------------ |------------:|----------:|----------:|------------:|------:|--------:|
| UseNew            | 1           |    10.49 us |  0.204 us |  0.243 us |    10.55 us |  0.96 |    0.03 |
| UseCreateInstance | 1           |    10.90 us |  0.192 us |  0.235 us |    10.88 us |  1.00 |    0.03 |
| UseCreateFactory  | 1           |   407.03 us |  4.701 us |  4.397 us |   406.15 us | 37.37 |    0.88 |
|                   |             |             |           |           |             |       |         |
| UseNew            | 1000        |    14.51 us |  0.167 us |  0.148 us |    14.50 us |  0.05 |    0.00 |
| UseCreateInstance | 1000        |   300.63 us |  2.557 us |  2.843 us |   300.85 us |  1.00 |    0.01 |
| UseCreateFactory  | 1000        |   725.57 us | 23.186 us | 65.775 us |   747.93 us |  2.41 |    0.22 |
|                   |             |             |           |           |             |       |         |
| UseNew            | 5000        |    31.06 us |  0.368 us |  0.344 us |    31.07 us |  0.02 |    0.00 |
| UseCreateInstance | 5000        | 1,350.49 us | 12.421 us | 11.619 us | 1,353.84 us |  1.00 |    0.01 |
| UseCreateFactory  | 5000        |   693.87 us | 13.064 us | 10.909 us |   696.99 us |  0.51 |    0.01 |
```
