using System;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Persistence.ContextConfiguration;
using Microsoft.EntityFrameworkCore;
using ConfigurationEntity = ConfigurationManagementSystem.Domain.Entities.ConfigurationEntity;

namespace ConfigurationManagementSystem.Persistence
{
    public class ConfigurationManagementSystemContext : DbContext
    {
        public virtual DbSet<ApplicationEntity> Applications { get; set; }
        public virtual DbSet<ConfigurationEntity> Configurations { get; set; }
        public virtual DbSet<OptionGroup> OptionGroups { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }

        public ConfigurationManagementSystemContext() { }

        public ConfigurationManagementSystemContext(DbContextOptions<ConfigurationManagementSystemContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
            //DataSeeding.Seed(this);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .ApplyConfiguration(new ApplicationConfiguration())
                .ApplyConfiguration(new ConfigurationEntityConfiguration())
                .ApplyConfiguration(new OptionGroupConfiguration())
                .ApplyConfiguration(new OptionConfiguration())
                .ApplyConfiguration(new UserEntityConfiguration());
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
