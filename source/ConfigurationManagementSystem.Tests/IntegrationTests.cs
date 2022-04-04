using System;
using ConfigurationManagementSystem.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationManagementSystem.Api.Tests.Tests
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
        }

        protected void ActWithDbContext(Action<ConfigurationManagementSystemContext> action)
        {
            var scopeFactory = WebAppFactory.Services;
            using var scope = scopeFactory.CreateScope();
            using var context = scope.ServiceProvider.GetService<ConfigurationManagementSystemContext>();
            action(context);
        }
    }
}
