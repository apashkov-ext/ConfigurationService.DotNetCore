using ConfigurationManagementSystem.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ConfigurationManagementSystem.Persistence
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<ConfigurationManagementSystemContext>
    {
        public ConfigurationManagementSystemContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ConfigurationManagementSystemContext>();
            optionsBuilder.ConfigureDatabaseConnection();
            return new ConfigurationManagementSystemContext(optionsBuilder.Options);
        }
    }
}
