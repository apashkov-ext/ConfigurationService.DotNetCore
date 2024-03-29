﻿using ConfigurationManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfigurationManagementSystem.Persistence.ContextConfiguration
{
    internal class ConfigurationEntityConfiguration : IEntityTypeConfiguration<ConfigurationEntity>
    {
        public void Configure(EntityTypeBuilder<ConfigurationEntity> builder)
        {
            builder.ToTable("Configurations").HasKey(x => x.Id);
            builder.Property(x => x.Created).DateTimeConversion().IsRequired();
            builder.Property(x => x.Modified).DateTimeConversion().IsRequired();

            builder.OwnsOne(x => x.Name, y =>
            {
                y.Property(p => p.Value).IsRequired().HasColumnName("Name");
                y.HasIndex(i => i.Value);
            });

            builder.HasOne(x => x.Application).WithMany(x => x.Configurations);
            builder.HasMany(x => x.OptionGroups).WithOne(x => x.Configuration);
            builder.Navigation(x => x.OptionGroups).HasField("_optionGroups").UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
        }
    }
}
