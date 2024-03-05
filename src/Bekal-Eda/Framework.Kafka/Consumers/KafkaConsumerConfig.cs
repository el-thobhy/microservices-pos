using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Kafka.Consumers
{
    public class KafkaConsumerConfig
    {
        public ConsumerConfig? ConsumerConfig { get; set; }
        public string[]? Topics { get; set; }
        public bool IgnoreDeserializationErrors { get; set; } = true;
    }
    public static class KafkaConsumerConfigExtensions
    {
        public static KafkaConsumerConfig GetKafkaConsumerConfig(this IConfiguration configuration)
        {
            return configuration.GetSection("KafkaConsumer").Get<KafkaConsumerConfig>();
        }
    }
}
