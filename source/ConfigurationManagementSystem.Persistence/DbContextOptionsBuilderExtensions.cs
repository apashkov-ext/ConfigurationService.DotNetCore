using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace ConfigurationManagementSystem.Persistence;

public static class DbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder ConfigureLocalhostDatabaseConnection(this DbContextOptionsBuilder optionsBuilder)
    {
        return optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres");
    }

    public static DbContextOptionsBuilder ConfigureDatabaseConnection(this DbContextOptionsBuilder optionsBuilder, IConfiguration configuration)
    {
        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        return optionsBuilder.UseNpgsql(configuration.GetConnectionString("postgres"));
    }
}
