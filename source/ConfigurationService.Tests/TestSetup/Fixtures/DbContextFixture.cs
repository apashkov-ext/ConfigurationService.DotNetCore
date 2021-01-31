using System;
using ConfigurationService.Persistence;

namespace ConfigurationService.Tests.TestSetup.Fixtures
{
    internal class DbContextFixture
    {
        public ConfigurationServiceContext Context { get; }

        public DbContextFixture(Action<TestContextConfigurer<ConfigurationServiceContext>> builder = null)
        {
            var m = new MockedContext<ConfigurationServiceContext>(builder);
            Context = m.Object;
        }
    }
}