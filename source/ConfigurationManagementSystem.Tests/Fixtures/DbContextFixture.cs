using System;
using ConfigurationManagementSystem.Persistence;

namespace ConfigurationManagementSystem.Tests.Fixtures
{
    public class DbContextFixture
    {
        public ConfigurationManagementSystemContext Context { get; }

        public DbContextFixture(Action<TestContextConfigurator<ConfigurationManagementSystemContext>> builder = null)
        {
            var m = new MockedContext<ConfigurationManagementSystemContext>(builder);
            Context = m.Object;
        }
    }
}