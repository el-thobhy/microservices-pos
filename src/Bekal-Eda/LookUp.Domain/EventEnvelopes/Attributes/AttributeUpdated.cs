using Framework.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookUp.Domain.EventEnvelopes.Attributes
{
    public record AttributeUpdated(
        Guid Id,
        AttributeTypeEnum Type,
        string Unit)
    {
        public static AttributeUpdated Create(
            Guid id,
            AttributeTypeEnum type,
            string unit
        ) => new(id, type, unit);
    }
}
