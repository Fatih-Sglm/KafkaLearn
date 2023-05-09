using Confluent.Kafka;

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
