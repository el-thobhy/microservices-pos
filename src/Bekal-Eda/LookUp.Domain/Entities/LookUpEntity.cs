using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookUp.Domain.Entities
{
    public class AttributesEntity
    {
        public Guid Id { get; set; }
        public AttributeTypeEnum Type { get; set; } = AttributeTypeEnum.Text;
        public string Unit { get; set; } = default!;
        public LookUpStatusEnum Status { get; set; } = LookUpStatusEnum.Inactive;
    }

    public class AttributesConfiguration : IEntityTypeConfiguration<AttributesEntity>
    {
        public void Configure(EntityTypeBuilder<AttributesEntity> builder)
        {
            builder.ToTable("Attributes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Unit).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Status).IsRequired();
        }
    }
}
