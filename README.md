# InterceptorSpeedMeasurement

## Results 

```
BenchmarkDotNet=v0.9.7.0
OS=Microsoft Windows NT 6.1.7601 Service Pack 1
Processor=Intel(R) Core(TM) i7-3770 CPU 3.40GHz, ProcessorCount=8
Frequency=3312929 ticks, Resolution=301.8477 ns, Timer=TSC
HostCLR=MS.NET 4.0.30319.42000, Arch=32-bit RELEASE
JitModules=clrjit-v4.6.1055.0

Type=Measurement  Mode=Throughput  Platform=X64  

                                   Method |       Jit |             Median |         StdDev | Gen 0 | Gen 1 | Gen 2 | Bytes Allocated/Op |
----------------------------------------- |---------- |------------------- |--------------- |------ |------ |------ |------------------- |
       ThousandInterceptorsWithoutResolve | LegacyJit |     10,343.9765 ns |     50.2280 ns |  0.00 |     - |     - |              53.45 |
       HundreadInterceptorsWithoutResolve | LegacyJit |        986.4448 ns |      8.7995 ns |  0.00 |     - |     - |              48.91 |
            TenInterceptorsWithoutResolve | LegacyJit |        139.6157 ns |      0.9869 ns |  0.00 |     - |     - |              51.40 |
             OneInterceptorWithoutResolve | LegacyJit |         46.9036 ns |      0.4340 ns |  0.00 |     - |     - |              53.05 |
          ThousandInterceptorsWithResolve | LegacyJit |     10,987.0057 ns |     59.1582 ns |  0.01 |     - |     - |             218.97 |
          HundreadInterceptorsWithResolve | LegacyJit |      1,490.7432 ns |     22.3782 ns |  0.01 |     - |     - |             182.87 |
               TenInterceptorsWithResolve | LegacyJit |        618.9226 ns |      2.9148 ns |  0.01 |     - |     - |             195.71 |
                OneInterceptorWithResolve | LegacyJit |        429.9977 ns |      1.6285 ns |  0.01 |     - |     - |             201.44 |
 ThousandSingletonInterceptorsWithResolve | LegacyJit |     10,847.8314 ns |     67.1822 ns |  0.01 |     - |     - |             202.69 |
 HundreadSingletonInterceptorsWithResolve | LegacyJit |      1,455.6430 ns |     49.4083 ns |  0.01 |     - |     - |             206.33 |
      TenSingletonInterceptorsWithResolve | LegacyJit |        636.4181 ns |      5.2196 ns |  0.01 |     - |     - |             197.33 |
       OneSingletonInterceptorWithResolve | LegacyJit |        493.7499 ns |     52.1709 ns |  0.01 |     - |     - |             205.72 |
                      TenSlowInterceptors | LegacyJit | 99,827,807.2063 ns | 89,475.5396 ns |     - |     - |     - |          21,866.63 |
                            NoInterceptor | LegacyJit |          1.0731 ns |      0.0350 ns |     - |     - |     - |               0.00 |
       ThousandInterceptorsWithoutResolve |    RyuJit |     17,374.3437 ns |    311.9622 ns |     - |     - |     - |              60.51 |
       HundreadInterceptorsWithoutResolve |    RyuJit |      1,673.0449 ns |     10.2264 ns |  0.00 |     - |     - |              53.82 |
            TenInterceptorsWithoutResolve |    RyuJit |        176.7440 ns |      2.0136 ns |  0.00 |     - |     - |              56.53 |
             OneInterceptorWithoutResolve |    RyuJit |         44.9159 ns |      0.2126 ns |  0.00 |     - |     - |              52.54 |
          ThousandInterceptorsWithResolve |    RyuJit |     19,977.8216 ns |    243.4333 ns |  0.00 |     - |     - |             205.86 |
          HundreadInterceptorsWithResolve |    RyuJit |      2,134.0033 ns |     14.0142 ns |  0.01 |     - |     - |             210.81 |
               TenInterceptorsWithResolve |    RyuJit |        515.2422 ns |      6.0527 ns |  0.01 |     - |     - |             199.70 |
                OneInterceptorWithResolve |    RyuJit |        373.5398 ns |      5.8726 ns |  0.01 |     - |     - |             191.06 |
 ThousandSingletonInterceptorsWithResolve |    RyuJit |     19,339.5225 ns |    444.9160 ns |  0.00 |     - |     - |             197.71 |
 HundreadSingletonInterceptorsWithResolve |    RyuJit |      2,144.6468 ns |     13.7438 ns |  0.01 |     - |     - |             198.43 |
      TenSingletonInterceptorsWithResolve |    RyuJit |        525.4754 ns |      2.3437 ns |  0.01 |     - |     - |             191.16 |
       OneSingletonInterceptorWithResolve |    RyuJit |        382.5300 ns |      2.5971 ns |  0.01 |     - |     - |             197.33 |
                      TenSlowInterceptors |    RyuJit | 99,847,068.8625 ns | 90,560.9125 ns |     - |     - |     - |          26,155.59 |
                            NoInterceptor |    RyuJit |          1.0759 ns |      0.0184 ns |     - |     - |     - |               0.00 |
