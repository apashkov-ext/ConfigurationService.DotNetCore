﻿using ConfigurationManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfigurationManagementSystem.Persistence.ContextConfiguration
{
    internal class ProjectConfiguration : IEntityTypeConfiguration<Domain.Entities.Application>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Application> builder)
        {
            builder.ToTable("Projects").HasKey(x => x.Id);
            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.Modified).IsRequired();

            builder.OwnsOne(x => x.Name).Property(x => x.Value).HasColumnName("Name");
            builder.OwnsOne(x => x.ApiKey).Property(x => x.Value).HasColumnName("ApiKey");

            builder.HasMany(x => x.Environments).WithOne(x => x.Project); 
            builder.Navigation(x => x.Environments).HasField("_environments").UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
        }
    }
}
