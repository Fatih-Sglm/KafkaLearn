using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumerService.Abstraction
{
    public interface IKafkaClient
    {
        T Consume<T>(CancellationToken cancellationToken = default);
        Task Subscribe(string queueName, CancellationToken cancellationToken = default);
    }
}
