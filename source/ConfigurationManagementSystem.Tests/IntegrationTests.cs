using System;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Api.Tests;
using ConfigurationManagementSystem.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationManagementSystem.Tests
{
    public abstract class IntegrationTests : IDisposable
    {
        protected WebAppFactory WebAppFactory { get; }

        public IntegrationTests()
        {
            WebAppFactory = new WebAppFactory();
        }

        public virtual void Dispose()
        {
            WebAppFactory.Dispose();
            GC.SuppressFinalize(this);
        }

        protected void ActWithDbContext(Action<ConfigurationManagementSystemContext> action)
        {
            using var scope = WebAppFactory.Services.CreateScope();
            using var context = scope.ServiceProvider.GetService<ConfigurationManagementSystemContext>();
            context.Database.Migrate();
            using var tr = context.Database.BeginTransaction();
            try
            {
                action(context);
            }
            finally
            {
                tr.Rollback();
            }
        }

        protected async Task ActWithDbContextAsync(Func<ConfigurationManagementSystemContext, Task> action)
        {
            using var scope = WebAppFactory.Services.CreateAsyncScope();
            using var context = scope.ServiceProvider.GetService<ConfigurationManagementSystemContext>();
            await context.Database.MigrateAsync();
            using var tr = await context.Database.BeginTransactionAsync();
            try
            {
                await action(context);
            }
            finally
            {
                await tr.RollbackAsync();
            }
        }
    }
}
