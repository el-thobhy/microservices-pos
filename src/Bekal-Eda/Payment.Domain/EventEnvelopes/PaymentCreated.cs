using Framework.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain.EventEnvelopes
{
    public record PaymentCreated(
        Guid CartId,
        List<CartProductItem> CartProducts,
        CartStatusEnum Status
    )
    {
        public static PaymentCreated Create(
            Guid cartId,
            List<CartProductItem> cartProducts,
            CartStatusEnum status
        ) => new(cartId, cartProducts, status);
    }

    public class CartProductItem
    {
        public Guid ProductId { get; set; }
        public int Stock { get; set; }
        public int Quantity { get; set; }
    }
}
