using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Event.External
{
    public interface IExternalEventProducer
    {
        Task Publish(IEventEnvelope @event, CancellationToken ct);
    }
}
