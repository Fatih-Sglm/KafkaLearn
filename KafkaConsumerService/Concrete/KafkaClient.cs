using Confluent.Kafka;
using System.Text.Json;

namespace KafkaConsumerService.Concrete
{
    public class KafkaClient
    {
        private readonly ConsumerConfig _cosumerConfig;
        private readonly IConsumer<Null, string> consumer;

        public KafkaClient(ConsumerConfig cosumerConfig , IConfiguration configuration)
        {
            _cosumerConfig = cosumerConfig;
            consumer = new ConsumerBuilder<Null, string>(_cosumerConfig).Build();
            var topics = configuration.GetSection("KafkaCongif:Topics").Get<List<string>>();
            if (topics != null)
                consumer.Subscribe(topics);
        }

        public T Consume<T>(CancellationToken cancellationToken = default)
        {
            return JsonSerializer.Deserialize<T>(consumer.Consume(cancellationToken).Message.Value);
        }

        public async Task Subscribe(string queueName, CancellationToken cancellationToken = default)
        {
            consumer.Subscribe(queueName);
        }
    }
}
