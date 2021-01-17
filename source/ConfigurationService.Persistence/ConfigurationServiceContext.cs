using System;
using System.Collections.Generic;
using System.Linq;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Persistence.ContextConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Environment = ConfigurationService.Domain.Entities.Environment;

namespace ConfigurationService.Persistence
{
    public class ConfigurationServiceContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Environment> Environments { get; set; }
        public DbSet<OptionGroup> OptionGroups { get; set; }
        public DbSet<Option> Options { get; set; }

        public ConfigurationServiceContext(DbContextOptions<ConfigurationServiceContext> options) : base(options)
        {
            Database.EnsureDeleted();
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
                entity.Created = stamp;
                entity.Modified = stamp;
            }
        }

        private void UpdateTimestampForModified(DateTime stamp)
        {
            var modified = ChangeTracker.Entries().SelectEntityInstances(EntityState.Modified);
            foreach (var entity in modified)
            {
                entity.Modified = stamp;
            }
        }
    }

    internal static class EntityEntryExtensions
    {
        public static IEnumerable<Entity> SelectEntityInstances(this IEnumerable<EntityEntry> source, EntityState state)
        {
            return source.Where(x => x.State == state && x.Entity is Entity).Select(x => x.Entity as Entity);
        }
    }
}
