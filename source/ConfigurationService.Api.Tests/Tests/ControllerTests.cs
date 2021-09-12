using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using ConfigurationService.Persistence;
using System;

namespace ConfigurationService.Api.Tests.Tests
{
    public abstract class ControllerTests
    {
        protected readonly WebAppFactory WebAppFactory;
        protected readonly HttpClient HttpClient;

        public ControllerTests()
        {
            WebAppFactory = new WebAppFactory();
            HttpClient = WebAppFactory.CreateClient();
        }

        protected void ActWithDbContext(Action<ConfigurationServiceContext> action)
        {
            var scopeFactory = WebAppFactory.Services;
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ConfigurationServiceContext>();
            action(context);
        }
    }
}
