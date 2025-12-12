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
    public int CreateCount { get; set; }

    ServiceProvider CreateServices()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddSingleton(sw);
        return services.BuildServiceProvider();
    }

    void Run(ServiceProvider sp, Func<ServiceProvider, int, string, ClassA> createInstance)
    {
        for (var i = 0; i < CreateCount; i++)
        {
            var _ = createInstance(sp, i+1, "Hi!");
        }
    }

    [Benchmark]
    public void UseNew()
    {
        var sp = CreateServices();
        var logger = sp.GetRequiredService<ILoggerFactory>().CreateLogger<ClassA>();
        var sw = sp.GetRequiredService<Stopwatch>();
        Run(sp, (sp, no, message) =>
        {
            return new ClassA(logger, message, sw, no);
        });
    }

    [Benchmark(Baseline = true)]
    public void UseCreateInstance()
    {
        var sp = CreateServices();
        Run(sp, (sp, no, message) => ActivatorUtilities.CreateInstance<ClassA>(sp, message, no));
    }

    [Benchmark]
    public void UseCreateFactory()
    {
        var createInstance = ActivatorUtilities.CreateFactory<ClassA>([typeof(int), typeof(string)]);
        var sp = CreateServices();
        Run(sp, (sp, no, message) => createInstance(sp, [no, message]));
    }
}

internal class Program
{
    static void Main()
    {
        BenchmarkRunner.Run<CreateFactoryBench>();
    }
}
