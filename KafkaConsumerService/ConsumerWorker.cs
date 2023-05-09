using KafkaConsumerService.Concrete;
using KafkaConsumerService.Models;

namespace KafkaConsumerService
{
    public class ConsumerWorker : BackgroundService
    {
        private readonly ILogger<ConsumerWorker> _logger;
        private readonly KafkaClient _kafkaClient;
        public ConsumerWorker(ILogger<ConsumerWorker> logger, KafkaClient kafkaClient)
        {
            _logger = logger;
            _kafkaClient = kafkaClient;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(async () => { await ConsumeMessages(stoppingToken);} , stoppingToken);
        }

        private async Task ConsumeMessages(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                await _kafkaClient.Subscribe("Deneme", stoppingToken);
                var data = _kafkaClient.Consume<Message>(stoppingToken);
                Console.Out.WriteLine($"{data.Id}  {data.Value}");
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
        }
    }
}
