using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumerService
{
    public class ConsumerWorker : BackgroundService
    {
        private readonly ILogger<ICustomFormatter> _logger;

        public ConsumerWorker(ILogger<ICustomFormatter> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var response = ConsumerConfiguration.Consumer.Consume(stoppingToken);
                await Console.Out.WriteLineAsync(response.Value);
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
        }
    }
}
