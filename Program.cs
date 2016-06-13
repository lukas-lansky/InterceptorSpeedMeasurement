using System;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Diagnostics.Windows;

namespace InterceptorSpeedMeasurement
{
    class Program
    {
        static void Main(string[] args)
        {
            var benchmarkResult = BenchmarkRunner.Run<Measurement>();

            Console.ReadLine();
        }
    }

    public class MeasurementConfig : ManualConfig
    {
        public MeasurementConfig()
        {
            Add(Job.LegacyJitX64);
            Add(Job.RyuJitX64);

            Add(new MemoryDiagnoser());
            Add(new InliningDiagnoser());
        }
    }
}
