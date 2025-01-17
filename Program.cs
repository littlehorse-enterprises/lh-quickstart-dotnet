using QuickStart.Dotnet;
using LittleHorse.Sdk;
using LittleHorse.Sdk.Common.Proto;
using LittleHorse.Sdk.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    private static ServiceProvider? _serviceProvider;

    private static void SetupApplication()
    {
        _serviceProvider = new ServiceCollection()
            .AddLogging(config =>
            {
                config.AddConsole();
                config.SetMinimumLevel(LogLevel.Debug);
            })
            .BuildServiceProvider();
    }


    private static LHConfig GetLHConfig(ILoggerFactory loggerFactory)
    {
        var config = new LHConfig(loggerFactory);
        
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), ".config/littlehorse.config");
        if (File.Exists(filePath))
            config = new LHConfig(filePath, loggerFactory);
        
        return config;
    }

    static void Main(string[] args)
    {
        SetupApplication();
        if (_serviceProvider != null)
        {
            var loggerFactory = _serviceProvider.GetRequiredService<ILoggerFactory>();
            var config = GetLHConfig(loggerFactory);

            var request = new PutExternalEventDefRequest
            {
                Name = "identity-verified"
            };
            config.GetGrpcClientInstance().PutExternalEventDef(request);
            
            var tasks = new KnowYourCustomerTasks();

            var taskWorker = new LHTaskWorker<KnowYourCustomerTasks>(tasks, "verify-identity", config);
            var notifyCustomerVerifiedWorker = new LHTaskWorker<KnowYourCustomerTasks>(tasks, "notify-customer-verified", config);
            var notifyCustomerNotVerifiedWorker = new LHTaskWorker<KnowYourCustomerTasks>(tasks, "notify-customer-not-verified", config);

            taskWorker.RegisterTaskDef();
            notifyCustomerVerifiedWorker.RegisterTaskDef();
            notifyCustomerNotVerifiedWorker.RegisterTaskDef();

            Thread.Sleep(1000);

            taskWorker.Start();
            notifyCustomerVerifiedWorker.Start();
            notifyCustomerNotVerifiedWorker.Start();
        }
    }
}
