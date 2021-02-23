using ConfigurationService.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationService.Tests.Fixtures
{
    public class InMemoryContextOptions
    {
        public static DbContextOptions<ConfigurationServiceContext> GetContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ConfigurationServiceContext>()
                .UseInMemoryDatabase("ConfigContext")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}
