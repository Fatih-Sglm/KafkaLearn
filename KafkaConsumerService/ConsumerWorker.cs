using KafkaConsumerService.Abstraction;
using KafkaConsumerService.Concrete;
using KafkaConsumerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumerService
{
    public class ConsumerWorker : BackgroundService
    {
        private readonly ILogger<ConsumerWorker> _logger;
        private readonly IKafkaClient _kafkaClient;
        public ConsumerWorker(ILogger<ConsumerWorker> logger, IKafkaClient kafkaClient)
        {
            _logger = logger;
            _kafkaClient = kafkaClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Console.Out.WriteLineAsync("App run");
            await Console.Out.WriteLineAsync("App run 1");
            await _kafkaClient.Subscribe("Deneme", stoppingToken);
            while (!stoppingToken.IsCancellationRequested)
            {
                _ = Task.Run(async () =>
                {
                    var data = _kafkaClient.Consume<Message>();
                    await Console.Out.WriteLineAsync("App run 2");
                    await Console.Out.WriteLineAsync(data.Value);
                    //Console.Out.WriteLineAsync($"{data.Id}  {data.Value}");
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }, stoppingToken);
            }
        }
    }
}
