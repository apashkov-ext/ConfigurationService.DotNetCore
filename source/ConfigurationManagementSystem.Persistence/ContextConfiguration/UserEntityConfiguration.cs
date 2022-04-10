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

            builder.OwnsOne(x => x.Username, y =>
            {
                y.Property(p => p.Value).IsRequired().HasColumnName("Username");
                y.HasIndex(i => i.Value);
            });
            builder.OwnsOne(x => x.PasswordHash, y =>
            {
                y.Property(p => p.Value).IsRequired().HasColumnName("PasswordHash");
                y.HasIndex(i => i.Value);
            });
        }
    }
}
