using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace KafkaConsumerService
{
    internal class ConsumerConfiguration
    {
        private static IConsumer<Null, string> consumer = null;
        private ConsumerConfiguration() { }

        public static IConsumer<Null, string> Consumer
        {
            get
            {
                consumer ??= CreateConsumerConfig();
                return consumer;
            }
        }

        private static IConsumer<Null, string> CreateConsumerConfig()
        {
            var config = new ConsumerConfig()
            {
                GroupId = "topic-group",
                BootstrapServers = "localhost:9092",
            };

            var consumer = new ConsumerBuilder<Null, string>(config).Build();
            consumer.Subscribe("topic2");

            return consumer;
        }
    }
}
