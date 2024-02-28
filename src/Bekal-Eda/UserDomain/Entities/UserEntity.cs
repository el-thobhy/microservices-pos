using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace User.Domain.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;

        public UserTypeEnum Type { get; set; } = UserTypeEnum.Customer;
        public RecordStatusEnum Status { get; set; } = RecordStatusEnum.Inactive;
        public DateTime Modified { get; set; } = DateTime.Now;
    }

    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        //fluent
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.UserName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(100).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50).IsRequired(false);

            builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Status).IsRequired();
        }
    }
}
