using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookUp.Domain.EventEnvelopes.Attributes
{
    public record AttributeCreated(Guid Id, AttributeTypeEnum Type, string Unit, LookUpStatusEnum Status)
    {
        public static AttributeCreated Created(
            Guid id,
            AttributeTypeEnum type,
            string unit,
            LookUpStatusEnum status
        ) => new(id, type, unit, status);
    }
}
