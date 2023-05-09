using KafkaConsumerService;
using KafkaConsumerService.Concrete;
using KafkaConsumerService.Extensions;

internal class Program
{
    // private static readonly string  env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    public static IConfiguration Configuration = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: false)
                                 .AddEnvironmentVariables()
                                 .Build();

    private static async Task Main(string[] args)
    {
        IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args);
        hostBuilder.ConfigureServices(services =>
        {
            ConfigureAppServices(services);
        });

        IHost host = hostBuilder.Build();
        await host.RunAsync();
    }

    private static IServiceCollection ConfigureAppServices(IServiceCollection services)
    {
        services.AddKafka(Configuration);
        services.AddHostedService<ConsumerWorker>();
        services.AddSingleton<KafkaClient>();
        return services;
    }
}

