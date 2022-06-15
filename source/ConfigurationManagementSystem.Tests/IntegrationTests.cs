using System;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Persistence;
using ConfigurationManagementSystem.Tests.Fixtures.ContextInitialization;
using Microsoft.EntityFrameworkCore;
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

        protected TContext GetContext()
        {
            using var scope = WebAppFactory.Services.CreateScope();
            return scope.ServiceProvider.GetRequiredService<TContext>();
        }

        protected void SetupTest()
        {
            using var scope = WebAppFactory.Services.CreateScope();
            using var ctx = scope.ServiceProvider.GetRequiredService<TContext>();
            new ContextSetup<TContext>(ctx).Initialize();
        }

        protected void SetupTest(Action<IContextSetup<TContext>> contextSetup)
        {
            if (contextSetup is null)
            {
                throw new ArgumentNullException(nameof(contextSetup));
            }

            using var scope = WebAppFactory.Services.CreateScope();
            using var ctx = scope.ServiceProvider.GetRequiredService<TContext>();
            var setup = new ContextSetup<TContext>(ctx);
            contextSetup(setup);
            setup.Initialize();
        }

        protected void Verify(Action<TContext> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            using var scope = WebAppFactory.Services.CreateScope();
            using var ctx = scope.ServiceProvider.GetRequiredService<TContext>();
            try
            {
                action(ctx);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
        }

        protected async Task VerifyAsync(Func<TContext, Task> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            using var scope = WebAppFactory.Services.CreateScope();
            using var ctx = scope.ServiceProvider.GetRequiredService<TContext>();
            try
            {
                await action(ctx);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
        }
    }
}
