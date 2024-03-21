using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Entity
{
    public class ProductDbContext: DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        public DbSet<ProductEntity> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }

        public static DbContextOptions<ProductDbContext> OnConfigure()
        {
            var optionBuilder = new DbContextOptionsBuilder<ProductDbContext>();
            optionBuilder.UseSqlServer(ServiceExtension.Configuration.GetConnectionString(ServiceExtension.DefaultGetConnection))
                .LogTo(Console.WriteLine);
            return optionBuilder.Options;
        }
    }
}
