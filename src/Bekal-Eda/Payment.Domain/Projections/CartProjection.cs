using Framework.Core.Enums;
using Framework.Core.Event;
using Payment.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain.Projections
{
    public record CartCreated(
         Guid Id,
         Guid CustomerId,
         CartStatusEnum Status
     );

    public record CartStatusChanged(
        Guid Id,
        Guid CustomerId,
        List<CartProducts> CartProducts,
        CartStatusEnum Status
    );

    public class CartProducts
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class CartProjection
    {
        public static bool Handle(EventEnvelope<CartCreated> eventEnvelope)
        {
            var (id, customerId, status) = eventEnvelope.Data;
            Debug.WriteLine("tes projection {0}",eventEnvelope.Data );
            using (var context = new PaymentDbContext(PaymentDbContext.OnConfigure()))
            {
                CartEntity entity = new CartEntity()
                {
                    Id = (Guid)id,
                    CustomerId = customerId,
                    Status = status
                };

                context.Carts.Add(entity);
                context.SaveChanges();
            }
            return true;
        }

        public static bool Handle(EventEnvelope<CartStatusChanged> eventEnvelope)
        {
            var (id, customerId, cartProducts, status) = eventEnvelope.Data;
            using (var context = new PaymentDbContext(PaymentDbContext.OnConfigure()))
            {
                CartEntity entity = context.Carts.Where(o => o.Id == id).FirstOrDefault();
                if (entity == null)
                {
                    CartEntity newEntity = new CartEntity()
                    {
                        Id = (Guid)id,
                        CustomerId = customerId,
                        Status = status
                    };
                    context.Carts.Add(newEntity);
                    context.SaveChanges();
                }
                else
                {
                    decimal total = 0;
                    foreach (var item in cartProducts)
                    {
                        ProductEntity prod = context.Products.Where(o => o.Id == item.ProductId).FirstOrDefault();
                        if (prod != null)
                            total += item.Quantity * prod.Price;

                        CartProductEntity cartProd = new CartProductEntity()
                        {
                            Id = (Guid)item.Id,
                            CartId = id,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity
                        };
                        context.CartProducts.Add(cartProd);
                    }
                    entity.Total = total;
                    context.SaveChanges();
                }
            }
            return true;
        }
    }
}
