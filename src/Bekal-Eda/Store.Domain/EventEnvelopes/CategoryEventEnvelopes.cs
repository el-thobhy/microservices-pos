using Framework.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.EventEnvelopes
{
    public record CategoryCreated(Guid Id, string Name, string Description, RecordStatusEnum Status)
    {
        public static CategoryCreated Create(Guid id, string name, string description, RecordStatusEnum status)
            => new(id, name, description, status);
    }
    public record CategoryStatusChange(Guid Id, RecordStatusEnum Status)
    {
        public static CategoryStatusChange Create(Guid id, RecordStatusEnum status)
            => new(id, status);
    }
    public record CategoryUpdated(Guid Id, string Name, string Description)
    {
        public static CategoryUpdated Create(Guid id, string name, string description)
            => new(id, name, description);
    }

}

