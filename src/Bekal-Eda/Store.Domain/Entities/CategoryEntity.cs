using Framework.Core.Entity;
using Framework.Core.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    //public class CategoryEntity: BaseEntity
    //{
    //    public Guid Id { get; set; } = default!;
    //    public string Name { get; set; } = default!;
    //    public string Descriprion { get; set; } = default!;
    //    public RecordStatusEnum Status { get; set; } = RecordStatusEnum.Inactive;
    //}
    //public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
    //{
    //    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    //    {
    //        builder.ToTable("Category");
    //        builder.HasKey(x => x.Id);
    //        builder.Property(x => x.Id).IsRequired();

    //    }
    //}
}
