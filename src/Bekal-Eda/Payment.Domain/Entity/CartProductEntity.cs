using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain.Entity
{
    public class CartProductEntity
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class CartProductConfiguration : IEntityTypeConfiguration<CartProductEntity>
    {
        public void Configure(EntityTypeBuilder<CartProductEntity> builder)
        {
            builder.ToTable("CartProducts");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.CartId).IsRequired();
            builder.Property(e => e.ProductId).IsRequired();
        }
    }
}
