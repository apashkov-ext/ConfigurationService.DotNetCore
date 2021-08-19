using Environment = ConfigurationService.Domain.Entities.Environment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfigurationService.Persistence.ContextConfiguration
{
    internal class EnvironmentConfiguration : IEntityTypeConfiguration<Environment>
    {
        public void Configure(EntityTypeBuilder<Environment> builder)
        {
            builder.ToTable("Environments").HasKey(x => x.Id);
            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.Modified).IsRequired();

            builder.OwnsOne(x => x.Name).Property(x => x.Value).HasColumnName("Name");
            builder.HasOne(x => x.Project).WithMany(x => x.Environments);
            builder.HasMany(x => x.OptionGroups).WithOne(x => x.Environment);
            builder.Navigation(x => x.OptionGroups).HasField("_optionGroups").UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
        }
    }
}
