using Framework.Core.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Core.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Domain.Entities
{
    public class CartEntity: BaseEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public CartStatusEnum Status { get; set; } = CartStatusEnum.Pending;

        [ForeignKey("CustomerId")]
        public virtual UserEntity Customer { get; set; }
        public virtual ICollection<CartProductEntity> CartProducts { get; set; }
    }

    public class CartConfiguration : IEntityTypeConfiguration<CartEntity>
    {
        public void Configure(EntityTypeBuilder<CartEntity> builder)
        {
            builder.ToTable("Carts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.CustomerId).IsRequired();
            builder.Property(x => x.Status).IsRequired();
        }
    }
}