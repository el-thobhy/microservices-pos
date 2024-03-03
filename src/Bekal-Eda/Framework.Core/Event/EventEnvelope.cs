using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Event
{

    public interface IEventEnvelope
    {
        object Data { get; }
    }
    public record EventEnvelope<T>(T Data) : IEventEnvelope where T : notnull
    {
        object IEventEnvelope.Data => Data;
    }
}
