using Framework.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.EventEnvelopes
{
    public record CartCreated(
        Guid Id,
        Guid CustomerId,
        CartStatusEnum Status
        )
    {
        public static CartCreated Created(
            Guid id,
            Guid customerId,
            CartStatusEnum status
            ) => new(id, customerId, status);
    }
}
