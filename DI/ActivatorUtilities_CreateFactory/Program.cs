using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CSharp.Playground.DI.ActivatorUtilities_CreateFactory;

class ClassA(ILogger<ClassA> logger, string message, Stopwatch sw, int no)
{
    public void Print()
    {
        logger.LogInformation("[{no}][{elapsed}] {message}", no, sw.Elapsed, message);
    }
}

public class CreateFactoryBench
{
    private readonly Stopwatch sw = Stopwatch.StartNew();

    [Params(1, 1000, 5000)]
    public int CreateInstanceCount { get; set; }

    void Run(Func<ServiceProvider, int, string, ClassA> createInstance)
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddSingleton(sw);
        var serviceProvider = services.BuildServiceProvider();

        var instances = new List<ClassA>();
        for (var i = 0; i < CreateInstanceCount; i++)
        {
            var instance = createInstance(serviceProvider, i+1, "Hi!");
            instances.Add(instance);
        }

        foreach (var item in instances)
        {
            item.Print();
        }
    }

    [Benchmark]
    public void UseNew()
    {
        Run((sp, no, message) =>
        {
            var logger = sp.GetRequiredService<ILoggerFactory>().CreateLogger<ClassA>();
            var sw = sp.GetRequiredService<Stopwatch>();
            return new ClassA(logger, message, sw, no);
        });
    }

    [Benchmark(Baseline = true)]
    public void UseCreateInstance()
    {
        Run((sp, no, message) => ActivatorUtilities.CreateInstance<ClassA>(sp, message, no));
    }

    [Benchmark]
    public void UseCreateFactory()
    {
        var createInstance = ActivatorUtilities.CreateFactory<ClassA>([typeof(int), typeof(string)]);
        Run((sp, no, message) => createInstance(sp, [no, message]));
    }
}

internal class Program
{
    static void Main()
    {
        BenchmarkRunner.Run<CreateFactoryBench>();
    }
}
