using Framework.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Event
{
    public record ProductCreated(
         Guid Id,
         string Name,
         string Description,
         decimal Price
        )
    {
        public static ProductCreated Create(
         Guid id,
         string name,
         string description,
         decimal price) => new(id, name, description, price);
    }
}
