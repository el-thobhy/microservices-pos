using Confluent.Kafka;
using Framework.Core.Event;
using Framework.Core.Event.External;
using Framework.Core.Serialization.Newtonsoft;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Kafka.Producers
{
    public class KafkaProducer : IExternalEventProducer
    {
        public readonly KafkaProducerConfig _config;
        public readonly ILogger<KafkaProducer> _logger;
        public KafkaProducer(IConfiguration config, ILogger<KafkaProducer> logger)
        {
            _config = config.GetKafkaProducerConfig();
            _logger = logger;
        }
        public async Task Publish(IEventEnvelope @event, CancellationToken ct)
        {
            try
            {
                using var p = new ProducerBuilder<string, string>(_config.PriducerConfig).Build();
                await p.ProduceAsync(_config.Topic, 
                new Message<string, string>
                {
                    Key = @event.Data.GetType().Name,
                    Value = @event.ToJson()
                }, ct).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error Producing Kafka Message: {e.Message}, {e.StackTrace}");
                throw;
            }
        }
    }
}
