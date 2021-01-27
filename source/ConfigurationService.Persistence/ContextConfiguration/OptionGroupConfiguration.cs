using ConfigurationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfigurationService.Persistence.ContextConfiguration
{
    internal class OptionGroupConfiguration : IEntityTypeConfiguration<OptionGroup>
    {
        public void Configure(EntityTypeBuilder<OptionGroup> builder)
        {
            builder.ToTable("OptionGroups").HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.Modified).IsRequired();

            builder.HasMany(x => x.Options).WithOne(x => x.OptionGroup).OnDelete(DeleteBehavior.Cascade);
            builder.Navigation(x => x.Options).HasField("_options").UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);

            builder.HasOne(x => x.Parent).WithMany(x => x.NestedGroups).IsRequired(false);
            builder.Navigation(x => x.NestedGroups).HasField("_nestedGroups").UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);

            builder.OwnsOne(x => x.Name).Property(x => x.Value).HasColumnName("Name");
            builder.OwnsOne(x => x.Description).Property(x => x.Value).HasColumnName("Description");
        }
    }
}
