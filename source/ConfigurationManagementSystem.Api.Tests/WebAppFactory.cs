using System;
using System.IO;
using System.Linq;
using ConfigurationManagementSystem.Persistence;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationManagementSystem.Api.Tests
{
    public class WebAppFactory : WebApplicationFactory<Startup>
    {
        private readonly string _dbName = $"test-db-{Guid.NewGuid()}.db";

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder().UseEnvironment("Development").UseStartup<Startup>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ConfigurationManagementSystemContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ConfigurationManagementSystemContext>(options =>
                {
                    var path = GetDataSourcePath(_dbName);
                    options.UseSqlite($"data source={path};foreign keys=true;");
                });
            });
        }

        private static string GetDataSourcePath(string dbName)
        {
            var tempDir = Path.GetTempPath();
            var dbPath = $"{tempDir}{dbName}";
            return dbPath;
        }
    }
}
