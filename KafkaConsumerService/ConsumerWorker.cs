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
            await ConsumeMessages(stoppingToken);
        }

        private async Task ConsumeMessages(CancellationToken stoppingToken)
        {
            _kafkaClient.Subscribe("Deneme");
            while (!stoppingToken.IsCancellationRequested)
            {
                var data = _kafkaClient.Consume<Message>(stoppingToken);
                Console.Out.WriteLine($"{data.Id}  {data.Value}");
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
        }
    }
}
