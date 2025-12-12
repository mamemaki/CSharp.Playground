## ActivatorUtilities_CreateFactory

Test `ActivatorUtilities.CreateFactory` performance.

```
| Method            | CreateInstanceCount | Mean         | Error      | StdDev     | Ratio | RatioSD |
|------------------ |-------------------- |-------------:|-----------:|-----------:|------:|--------:|
| UseNew            | 1                   |     9.460 us |  0.0988 us |  0.0925 us |  0.95 |    0.01 |
| UseCreateInstance | 1                   |     9.982 us |  0.0981 us |  0.0918 us |  1.00 |    0.01 |
| UseCreateFactory  | 1                   |   381.913 us |  1.4638 us |  1.2976 us | 38.26 |    0.36 |
|                   |                     |              |            |            |       |         |
| UseNew            | 1000                |   219.373 us |  0.9697 us |  0.9070 us |  0.64 |    0.00 |
| UseCreateInstance | 1000                |   342.574 us |  1.6888 us |  1.4970 us |  1.00 |    0.01 |
| UseCreateFactory  | 1000                |   541.009 us |  1.7474 us |  1.5490 us |  1.58 |    0.01 |
|                   |                     |              |            |            |       |         |
| UseNew            | 5000                | 1,023.105 us | 15.2688 us | 14.9960 us |  0.67 |    0.01 |
| UseCreateInstance | 5000                | 1,523.317 us |  7.9330 us |  7.4205 us |  1.00 |    0.01 |
| UseCreateFactory  | 5000                |   945.315 us |  6.0174 us |  5.0248 us |  0.62 |    0.00 |
```
