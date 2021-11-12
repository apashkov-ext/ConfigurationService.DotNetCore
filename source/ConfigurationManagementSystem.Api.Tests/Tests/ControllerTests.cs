using System;
using System.Net.Http;
using ConfigurationManagementSystem.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationManagementSystem.Api.Tests.Tests
{
    public abstract class ControllerTests : IDisposable
    {
        protected readonly WebAppFactory WebAppFactory;
        protected readonly HttpClient HttpClient;

        public ControllerTests()
        {
            WebAppFactory = new WebAppFactory();
            HttpClient = WebAppFactory.CreateClient();
        }

        public void Dispose()
        {
            //WebAppFactory.Dispose();
        }

        protected void ActWithDbContext(Action<ConfigurationManagementSystemContext> action)
        {
            var scopeFactory = WebAppFactory.Services;
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ConfigurationManagementSystemContext>();
            action(context);
        }
    }
}
