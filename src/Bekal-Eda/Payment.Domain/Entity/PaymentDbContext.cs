using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain.Entity
{
    public class PaymentDbContext: DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options): base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CartProductEntity> CartProducts { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<PaymentEntity> Payments { get; set; }
        public DbSet<CartEntity> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration())
                .ApplyConfiguration(new PaymentConfiguration())
                .ApplyConfiguration(new CartProductConfiguration())
                .ApplyConfiguration(new UserConfiguration())
                .ApplyConfiguration(new CartConfiguration());
        }
        public static DbContextOptions<PaymentDbContext> OnConfigure()
        {
            var optionsBuilder = new DbContextOptionsBuilder<PaymentDbContext>();
            optionsBuilder.UseSqlServer(ServiceExtension.Configuration.GetConnectionString(ServiceExtension.DefaultConnection));
            return optionsBuilder.Options;
        }

    }

}

