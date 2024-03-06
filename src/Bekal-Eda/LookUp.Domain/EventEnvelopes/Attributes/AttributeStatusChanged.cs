using Framework.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookUp.Domain.EventEnvelopes.Attributes
{
    public record AttributeStatusChanged(
        Guid? Id,
        RecordStatusEnum Status
        )
    {
        public static AttributeStatusChanged Create(
           Guid id,
        RecordStatusEnum status
       ) => new(id, status);
    }
}
