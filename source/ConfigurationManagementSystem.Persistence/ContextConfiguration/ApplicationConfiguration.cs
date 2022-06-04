using ConfigurationManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfigurationManagementSystem.Persistence.ContextConfiguration
{
    internal class ApplicationEntityConfiguration : IEntityTypeConfiguration<ApplicationEntity>
    {
        public void Configure(EntityTypeBuilder<ApplicationEntity> builder)
        {
            builder.ToTable("Applications").HasKey(x => x.Id);
            builder.Property(x => x.Created).DateTimeConversion().IsRequired();
            builder.Property(x => x.Modified).DateTimeConversion().IsRequired();

            builder.OwnsOne(x => x.Name, y =>
            {
                y.Property(p => p.Value).HasColumnName("Name");
                y.HasIndex(i => i.Value);
            });

            builder.OwnsOne(x => x.ApiKey).Property(x => x.Value).HasColumnName("ApiKey");

            builder.HasMany(x => x.Configurations).WithOne(x => x.Application);
            builder.Navigation(x => x.Configurations).HasField("_configurations").UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
        }
    }
}
