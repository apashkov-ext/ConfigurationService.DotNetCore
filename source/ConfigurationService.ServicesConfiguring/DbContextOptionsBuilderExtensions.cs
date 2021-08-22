using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace ConfigurationService.ServiceCollectionConfiguring
{
    internal static class DbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder ConfigureSqlServerLocal(this DbContextOptionsBuilder optionsBuilder)
        {
            return optionsBuilder.UseSqlServer($@"Data Source=(localdb)\MSSQLLocalDb;Initial Catalog=ConfigurationStorage;");
        }

        public static DbContextOptionsBuilder ConfigureSqlite(this DbContextOptionsBuilder optionsBuilder, IHostEnvironment environment)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder, Environment.SpecialFolderOption.Create);
            var dbPath = $"{path}{Path.DirectorySeparatorChar}configurations.db";

            return optionsBuilder.UseSqlite($"data source={dbPath};foreign keys=true;");
        }
    }
}
