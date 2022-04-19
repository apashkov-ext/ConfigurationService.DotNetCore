using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ConfigurationManagementSystem.ServicesConfiguring
{
    internal static class DbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder ConfigureSqlServerLocal(this DbContextOptionsBuilder optionsBuilder, IConfiguration configuration)
        {
            return optionsBuilder.UseSqlServer(configuration.GetConnectionString("mssqllocaldb"));
        }

        public static DbContextOptionsBuilder ConfigurePostgres(this DbContextOptionsBuilder optionsBuilder, IConfiguration configuration)
        {
            return optionsBuilder.UseNpgsql(configuration.GetConnectionString("postgres"));
        }

        public static DbContextOptionsBuilder ConfigureSqlite(this DbContextOptionsBuilder optionsBuilder)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder, Environment.SpecialFolderOption.Create);
            var dbPath = $"{path}{Path.DirectorySeparatorChar}configurations.db";

            return optionsBuilder.UseSqlite($"data source={dbPath};foreign keys=true;");
        }
    }
}
