using Framework.Core.Event.External;
using Framework.Kafka.Producers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Framework.Kafka
{
    public static class Config
    {
        public static IServiceCollection AddKafkaProducer(this IServiceCollection services)
        {
            services.TryAddSingleton<IExternalEventProducer, KafkaProducer>();
            return services;
        }
        //public static IServiceCollection AddKafkaProducerAndConsumer(this IServiceCollection services)
        //{

        //}
    }
}