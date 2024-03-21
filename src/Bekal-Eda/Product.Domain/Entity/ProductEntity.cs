using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Product.Domain.Entity
{
    public class ProductEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public decimal Price { get; set; } = default!;
    }
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {

        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Description).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Price).HasPrecision(18, 2);

        }
    }
}
