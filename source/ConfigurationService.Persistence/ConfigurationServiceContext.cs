using System;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Persistence.ContextConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Environment = ConfigurationService.Domain.Entities.Environment;

namespace ConfigurationService.Persistence
{
    public class ConfigurationServiceContext : DbContext
    {
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Environment> Environments { get; set; }
        public virtual DbSet<OptionGroup> OptionGroups { get; set; }
        public virtual DbSet<Option> Options { get; set; }

        public ConfigurationServiceContext() { }

        public ConfigurationServiceContext(DbContextOptions<ConfigurationServiceContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
            DataSeeding.Seed(this);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .ApplyConfiguration(new ProjectConfiguration())
                .ApplyConfiguration(new EnvironmentConfiguration())
                .ApplyConfiguration(new OptionGroupConfiguration())
                .ApplyConfiguration(new OptionConfiguration());
        }

        public override int SaveChanges()
        {
            var now = DateTime.Now;
            UpdateTimestampForCreated(now);
            UpdateTimestampForModified(now);

            return base.SaveChanges();
        }

        private void UpdateTimestampForCreated(DateTime stamp)
        {
            var created = ChangeTracker.Entries().SelectEntityInstances(EntityState.Added);
            foreach (var entity in created)
            {
                entity.UpdateCreated(stamp);
                entity.UpdateModified(stamp);
            }
        }

        private void UpdateTimestampForModified(DateTime stamp)
        {
            var modified = ChangeTracker.Entries().SelectEntityInstances(EntityState.Modified);
            foreach (var entity in modified)
            {
                entity.UpdateModified(stamp);
            }
        }
    }
}
