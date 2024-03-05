using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Event
{
    public interface IEventHandler<in TEvent>
    {
        Task Handle(TEvent @event, CancellationToken cancellationToken);

    }
}
