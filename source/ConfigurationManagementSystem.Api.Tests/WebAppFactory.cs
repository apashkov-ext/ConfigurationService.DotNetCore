using System;
using System.Linq;
using ConfigurationManagementSystem.Persistence;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationManagementSystem.Api.Tests
{
    public abstract class DbContextVariant : IDisposable
    {
        public void Dispose()
        {
            DisposeInstance();
        }

        public abstract void ConfigureContext(IServiceCollection services);
        protected abstract void DisposeInstance();
    }

    public class InMemoryContext : DbContextVariant
    {
        private readonly string _dbName = $"Database-{Guid.NewGuid()}.db";

        public override void ConfigureContext(IServiceCollection services)
        {
            services.AddDbContext<ConfigurationServiceContext>(options =>
            {
                options.UseInMemoryDatabase(_dbName);
            });
        }

        protected override void DisposeInstance()
        {
            return;
        }
    }

    public class SqliteContext : DbContextVariant
    {
        public override void ConfigureContext(IServiceCollection services)
        {
            throw new NotImplementedException();
        }

        protected override void DisposeInstance()
        {
            throw new NotImplementedException();
        }
    }

    public class WebAppFactory<TContextVariant> : WebApplicationFactory<Startup> where TContextVariant : DbContextVariant, new()
    {
        private readonly TContextVariant _contextVariant = new TContextVariant();

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder().UseEnvironment("Development").UseStartup<Startup>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ConfigurationServiceContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                _contextVariant.ConfigureContext(services);
            });
        }

        public new void Dispose()
        {
            base.Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            _contextVariant.Dispose();
            base.Dispose(disposing);
        }
    }
}
