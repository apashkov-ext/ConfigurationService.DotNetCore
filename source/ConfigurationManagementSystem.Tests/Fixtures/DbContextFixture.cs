using System;
using ConfigurationManagementSystem.Persistence;

namespace ConfigurationManagementSystem.Tests.Fixtures
{
    public class DbContextFixture
    {
        public ConfigurationServiceContext Context { get; }

        public DbContextFixture(Action<TestContextConfigurator<ConfigurationServiceContext>> builder = null)
        {
            var m = new MockedContext<ConfigurationServiceContext>(builder);
            Context = m.Object;
        }
    }
}