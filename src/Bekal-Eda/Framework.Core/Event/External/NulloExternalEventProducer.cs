using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Event.External
{
    public class NulloExternalEventProducer : IExternalEventProducer
    {
        public Task Publish(IEventEnvelope @event, CancellationToken ct)
        {
            return Task.CompletedTask;
        }
    }
}
