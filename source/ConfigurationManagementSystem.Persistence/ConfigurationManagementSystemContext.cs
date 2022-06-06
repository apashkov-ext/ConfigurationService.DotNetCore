using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationManagementSystem.Persistence;

public interface ICleanableDbContext
{
    public void CleanData();
}

public class ConfigurationManagementSystemContext : DbContext, ICleanableDbContext
{
    public virtual DbSet<ApplicationEntity> Applications { get; set; }
    public virtual DbSet<ConfigurationEntity> Configurations { get; set; }
    public virtual DbSet<OptionGroupEntity> OptionGroups { get; set; }
    public virtual DbSet<OptionEntity> Options { get; set; }
    public virtual DbSet<UserEntity> Users { get; set; }

    public ConfigurationManagementSystemContext() 
    { 
    }

    public ConfigurationManagementSystemContext(DbContextOptions<ConfigurationManagementSystemContext> options) : base(options) 
    { 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamp();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        UpdateTimestamp();
        return base.SaveChanges();
    }

    private void UpdateTimestamp()
    {
        var now = DateTime.UtcNow;
        UpdateTimestampForCreated(now);
        UpdateTimestampForModified(now);
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

    public void CleanData()
    {
        Database.ExecuteSqlRaw("truncate table \"Applications\" cascade");
    }
}
