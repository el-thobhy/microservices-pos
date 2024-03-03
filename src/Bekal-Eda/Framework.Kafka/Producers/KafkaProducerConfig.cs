using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Kafka.Producers
{
    public class KafkaProducerConfig
    {
        public ProducerConfig? PriducerConfig { get; set; }
        public string? Topic { get; set; }
    }
    public static class KafkaProducerConfigExtensions
    {
        public const string DefaultConfigKey = "KafkaProducer";
        public static KafkaProducerConfig GetKafkaProducerConfig(this IConfiguration configuration)
        {
            return configuration.GetSection(DefaultConfigKey).Get<KafkaProducerConfig>();
        }
    }
}
