using ConfigurationService.Domain.Entities;
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
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.Modified).IsRequired();
            builder.Property(x => x.IsDefault).HasDefaultValue(false);

            builder.OwnsOne(x => x.Name).Property(x => x.Value).HasColumnName("Name");
            builder.HasOne(x => x.Project).WithMany(x => x.Environments);
            builder.HasOne(x => x.OptionGroup).WithOne(x => x.Environment)
                .IsRequired(false)
                .HasForeignKey<OptionGroup>(x => x.EnvironmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
