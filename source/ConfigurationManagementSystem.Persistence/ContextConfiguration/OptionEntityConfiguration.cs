using ConfigurationManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ConfigurationManagementSystem.Persistence.ContextConfiguration
{
    internal class OptionEntityConfiguration : IEntityTypeConfiguration<OptionEntity>
    {
        public void Configure(EntityTypeBuilder<OptionEntity> builder)
        {
            builder.ToTable("Options").HasKey(x => x.Id);
            builder.Property(x => x.Created).DateTimeConversion().IsRequired();
            builder.Property(x => x.Modified).DateTimeConversion().IsRequired();

            builder.HasOne(x => x.OptionGroup).WithMany(x => x.Options);

            builder.OwnsOne(x => x.Name, y =>
            {
                y.Property(p => p.Value).HasColumnName("Name");
                y.HasIndex(i => i.Value);
            });
            builder.OwnsOne(x => x.Value, a =>
            {
                a.Property(x => x.Value).HasColumnName("Value");
                a.Property(x => x.Type).HasColumnName("Type").HasConversion(new EnumToNumberConverter<OptionValueType, int>()).HasDefaultValue(OptionValueType.String);
            });
        }
    }
}
