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

        // If you are going to run this code in a background service in an api,
        // you need to run it in a separate thread since the consume method blocks the request.

        //Eğer bu kodu bir api içindeki background erviste çalışacaksan consume metodu
        //istemi blokladığından ayrı bir thread de çalıştırmak gerekiyor
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
