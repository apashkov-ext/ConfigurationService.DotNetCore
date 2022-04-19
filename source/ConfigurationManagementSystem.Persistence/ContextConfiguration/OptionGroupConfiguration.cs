﻿using ConfigurationManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ConfigurationManagementSystem.Persistence.ContextConfiguration
{
    internal class OptionGroupConfiguration : IEntityTypeConfiguration<OptionGroup>
    {
        public void Configure(EntityTypeBuilder<OptionGroup> builder)
        {
            builder.ToTable("OptionGroups").HasKey(x => x.Id);
            builder.Property(x => x.Created).DateTimeConversion().IsRequired();
            builder.Property(x => x.Modified).DateTimeConversion().IsRequired();

            builder.HasMany(x => x.Options).WithOne(x => x.OptionGroup);
            builder.Navigation(x => x.Options).HasField("_options").UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);

            builder.HasOne(x => x.Parent).WithMany(x => x.NestedGroups).IsRequired(false);
            builder.Navigation(x => x.NestedGroups).HasField("_nestedGroups").UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);

            builder.OwnsOne(x => x.Name, y =>
            {
                y.Property(p => p.Value).HasColumnName("Name");
                y.HasIndex(i => i.Value);
            });
        }
    }
}
