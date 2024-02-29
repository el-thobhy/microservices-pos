using Framework.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Entities
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasData(
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Email = "auriwanyasper007@gmail.com",
                    UserName = "admin",
                    Password = Encryption.HashSha256("Admin1234!"),
                    FirstName = "Super",
                    LastName = "User",
                    Modified = DateTime.Now,
                    Status = RecordStatusEnum.Active,
                    Type = UserTypeEnum.Administrator
                }
                );
        }
    }
}