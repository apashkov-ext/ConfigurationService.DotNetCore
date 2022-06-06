using System;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Persistence;
using ConfigurationManagementSystem.Tests.Fixtures.ContextInitialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationManagementSystem.Tests
{
    public abstract class IntegrationTests<TContext> : IDisposable where TContext : DbContext, ICleanableDbContext, new()
    {
        protected WebAppFactory<TContext> WebAppFactory { get; }

        public IntegrationTests()
        {
            WebAppFactory = new WebAppFactory<TContext>();
        }

        public virtual void Dispose()
        {
            WebAppFactory.Dispose();
            GC.SuppressFinalize(this);
        }

        protected void ActWithDbContext(Action<TContext> action)
        {
            using var scope = WebAppFactory.Services.CreateScope();
            using var context = scope.ServiceProvider.GetService<TContext>();
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

        protected void ActWithDbContext(Action<ContextInitializer<TContext>> initialize, Action<TContext> action)
        {
            Initialize(initialize);

            RunInScope(ctx =>
            {
                using var tr = ctx.Database.BeginTransaction();
                try
                {
                    action(ctx);
                }
                finally
                {
                    tr.Rollback();
                }
            });
        }

        protected async Task ActWithDbContextAsync(Action<ContextInitializer<TContext>> initialize, Func<TContext, Task> action)
        {
            Initialize(initialize);

            using var scope = WebAppFactory.Services.CreateScope();
            using var ctx = scope.ServiceProvider.GetRequiredService<TContext>();
            using var tr = await ctx.Database.BeginTransactionAsync();
            try
            {
                await action(ctx);
            }
            finally
            {
                await tr.RollbackAsync();
            }
        }

        private void Initialize(Action<ContextInitializer<TContext>> initialize)
        {
            RunInScope(ctx =>
            {
                try
                {
                    initialize(new ContextSetup<TContext>(ctx).Setup());
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while initializing database before testing.", ex);
                }
            });
        }

        private void RunInScope(Action<TContext> action)
        {
            using var scope = WebAppFactory.Services.CreateScope();
            using var ctx = scope.ServiceProvider.GetRequiredService<TContext>();
            action(ctx);
        }
    }
}
