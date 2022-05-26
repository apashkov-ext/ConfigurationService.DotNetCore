using System;
using ConfigurationManagementSystem.Api.Tests;
using ConfigurationManagementSystem.Persistence;
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
            var scopeFactory = WebAppFactory.Services;
            using var scope = scopeFactory.CreateScope();
            using var context = scope.ServiceProvider.GetService<ConfigurationManagementSystemContext>();
            //using var tr = context.Database.BeginTransaction();
            //try
            //{
                action(context);
            //}
            //finally
            //{
            //    tr.Rollback();
            //}
        }
    }
}
