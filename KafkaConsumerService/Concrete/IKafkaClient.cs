using Confluent.Kafka;
using KafkaConsumerService.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Confluent.Kafka.ConfigPropertyNames;

namespace KafkaConsumerService.Concrete
{
    public class KafkaClient : IKafkaClient
    {
        private readonly ConsumerConfig _cosumerConfig;
        private readonly IConsumer<Null, string> consumer;

        public KafkaClient(ConsumerConfig cosumerConfig)
        {
            _cosumerConfig = cosumerConfig;
            consumer = new ConsumerBuilder<Null, string>(_cosumerConfig).Build();
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
