using Confluent.Kafka;
using Framework.Core.Event;
using Framework.Core.Reflections;
using Framework.Core.Serialization.Newtonsoft;

namespace Framework.Kafka.Events
{
    public static class EventEnvelopExtensions
    {
        public static IEventEnvelope? ToEventEnvelope(this ConsumeResult<string, string> message)
        {
            var eventType = TypeProvider.GetTypeFromAnyReferencingAssembly(message.Message.Key);

            if (eventType == null)
                return null;

            var eventEnvelopeType = typeof(EventEnvelope<>).MakeGenericType(eventType);

            // deserialize event
            return message.Message.Value.FromJson(eventEnvelopeType) as IEventEnvelope;
        }
    }
}
