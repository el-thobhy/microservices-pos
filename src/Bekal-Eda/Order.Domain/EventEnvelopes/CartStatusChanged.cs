using Framework.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.EventEnvelopes
{
    public record CartStatusChanged(
        Guid Id,
        Guid CustomerId,
        CartStatusEnum Status,
        List<CartProductItem> CartProducts,
        Nullable<DateTime> Modified
        )
    {
        public static CartStatusChanged UpdateStatus(
            Guid id,
            Guid customerId,
            CartStatusEnum status,
            List<CartProductItem> cartProducts,
            Nullable<DateTime> modified
            ) => new(id, customerId, status, cartProducts, modified);
    }

    public class CartProductItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
