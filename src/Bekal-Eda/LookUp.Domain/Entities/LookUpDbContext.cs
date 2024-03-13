using Framework.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookUp.Domain.Entities
{
    public class LookUpDbContext: DbContext
    {
        public LookUpDbContext(DbContextOptions<LookUpDbContext> options): base(options)
        {
        }

        public DbSet<AttributesEntity> Attributes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Seed();
            modelBuilder.ApplyConfiguration(new AttributesConfiguration());
        }
    }
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AttributesEntity>()
                .HasData(
                new AttributesEntity
                {
                    Id = Guid.NewGuid(),
                    Type = AttributeTypeEnum.Text,
                    Status = RecordStatusEnum.Active,
                    Unit = "Tes1"
                }
                );
        }
    }
}
