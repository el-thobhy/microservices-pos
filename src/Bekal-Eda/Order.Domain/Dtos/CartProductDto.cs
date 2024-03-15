using Framework.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Dtos
{
    public class CartProductDto
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public string Sku { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductDto? Product { get; set; }
        public CartDto? Cart { get; set; }
    }
    public class CartProductInputDto
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
