using ConfigurationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfigurationService.Persistence.ContextConfiguration
{
    internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects").HasKey(x => x.Id);
            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.Modified).IsRequired();

            builder.OwnsOne(x => x.Name).Property(x => x.Value).HasColumnName("Name");
            builder.OwnsOne(x => x.ApiKey).Property(x => x.Value).HasColumnName("ApiKey");

            builder.HasMany(x => x.Environments).WithOne(x => x.Project).OnDelete(DeleteBehavior.Cascade); 
            builder.Navigation(x => x.Environments).HasField("_environments").UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
        }
    }
}
