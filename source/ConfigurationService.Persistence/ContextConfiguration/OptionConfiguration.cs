using ConfigurationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ConfigurationService.Persistence.ContextConfiguration
{
    internal class OptionConfiguration : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.ToTable("Options").HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.Modified).IsRequired();

            builder.HasOne(x => x.OptionGroup).WithMany(x => x.Options);

            builder.OwnsOne(x => x.Name).Property(x => x.Value).HasColumnName("Name");
            builder.OwnsOne(x => x.Description).Property(x => x.Value).HasColumnName("Description");
            builder.OwnsOne(x => x.Value, a =>
            {
                a.Property(x => x.Value).HasColumnName("Value");
                a.Property(x => x.Type).HasColumnName("Type").HasConversion(new EnumToNumberConverter<OptionValueType, int>()).HasDefaultValue(OptionValueType.String);
            });
        }
    }
}
