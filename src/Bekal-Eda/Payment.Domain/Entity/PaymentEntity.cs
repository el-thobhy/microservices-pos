using Framework.Core.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain.Entity
{
    public class PaymentEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal? Total { get; set; }
        public decimal? Pay { get; set; }
        public CartStatusEnum Status { get; set; } = default!;
        public DateTime Modified { get; set; } = DateTime.Now;

        [ForeignKey("CustomerId")]
        public virtual UserEntity Customer { get; set; }
    }
    public class PaymentConfiguration : IEntityTypeConfiguration<PaymentEntity>
    {
        public void Configure(EntityTypeBuilder<PaymentEntity> builder)
        {
            builder.ToTable("Payments");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Total).HasPrecision(18, 2).IsRequired(false);
            builder.Property(e => e.Pay).HasPrecision(18, 2).IsRequired(false);
            builder.Property(e => e.Status).IsRequired();
        }
    }
}
