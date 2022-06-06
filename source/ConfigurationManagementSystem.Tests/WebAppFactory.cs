using System.Linq;
using ConfigurationManagementSystem.Api;
using ConfigurationManagementSystem.Core;
using ConfigurationManagementSystem.Persistence;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConfigurationManagementSystem.Tests;

public class WebAppFactory<TContext> : WebApplicationFactory<Startup> where TContext : DbContext
{
    protected override IWebHostBuilder CreateWebHostBuilder()
    {
        return WebHost.CreateDefaultBuilder().UseEnvironment(ApplicationConstants.EnvironmentName).UseStartup<Startup>();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<TContext>(options =>
            {
                options.ConfigureLocalhostDatabaseConnection();
            });
        });
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        var host = base.CreateHost(builder);

        using var scope = host.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<TContext>();
        context.Database.Migrate();

        return host;
    }
}
