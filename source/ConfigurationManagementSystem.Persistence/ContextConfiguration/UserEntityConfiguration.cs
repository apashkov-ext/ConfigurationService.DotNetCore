using ConfigurationManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfigurationManagementSystem.Persistence.ContextConfiguration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users").HasKey(x => x.Id);
            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.Modified).IsRequired();

            builder.OwnsOne(x => x.Username).Property(x => x.Value).HasColumnName("Username");
            builder.OwnsOne(x => x.PasswordHash).Property(x => x.Value).HasColumnName("PasswordHash");

            builder.HasIndex(x => x.Username);
            builder.HasIndex(x => x.PasswordHash);
        }
    }
}
