using KafkaConsumerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<ConsumerWorker>();
    })
    .Build();

await host.RunAsync();
