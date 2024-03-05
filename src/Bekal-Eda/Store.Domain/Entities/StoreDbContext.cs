using Framework.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }

        public DbSet<AttributesEntity> Attributes { get; set; }
        //public DbSet<CategoryEntity> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AttributesConfiguration());
            //modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }

        //
        public static DbContextOptions<StoreDbContext> OnConfigure()
        {
            var optionBuilder = new DbContextOptionsBuilder<StoreDbContext>();
            optionBuilder.UseSqlServer(ServiceExtension.Configuration.GetConnectionString(ServiceExtension.DefaultConnection))
                .LogTo(Console.WriteLine);
            return optionBuilder.Options;
        }
    }
}
