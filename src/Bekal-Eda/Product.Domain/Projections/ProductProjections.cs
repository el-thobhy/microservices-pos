using Framework.Core.Event;
using Product.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Projections
{
    public record ProductCreated
    (
        Guid Id,
         string Name,
         string Description,
         decimal Price
    );
    public class ProductProjections
    {
        public static bool Handle(EventEnvelope<ProductCreated> eventEnvelope)
        {
            var (id, name, description, price) = eventEnvelope.Data;
            using (var context = new ProductDbContext(ProductDbContext.OnConfigure()))
            {
                ProductEntity entity = new ProductEntity()
                {
                    Id = (Guid)id,
                    Name = name,
                    Description = description,
                    Price = price
                };
                context.Add(entity);
                context.SaveChanges();
            }
            return true;
        }
    }
}
