using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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
            var folder = $"{environment.ContentRootPath}{Path.DirectorySeparatorChar}appdata";
            Directory.CreateDirectory(folder);
            var dbPath = $"{folder}{Path.DirectorySeparatorChar}configurations.db";
            return optionsBuilder.UseSqlite($"data source={dbPath};foreign keys=true;");
        }
    }
}
