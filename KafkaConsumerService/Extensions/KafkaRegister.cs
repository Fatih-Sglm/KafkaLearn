using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumerService.Extensions
{
    public static class KafkaRegister
    {
        public static IServiceCollection AddKafka(this IServiceCollection services, IConfiguration configuration)
        {
            var consumerConfig = configuration.GetSection("KafkaCongif").Get<ConsumerConfig>();
            services.AddSingleton(opt => consumerConfig);
            return services;
        }
    }
}
