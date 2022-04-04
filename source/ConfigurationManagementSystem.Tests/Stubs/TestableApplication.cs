using System;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;

namespace ConfigurationManagementSystem.Tests.Stubs
{
    internal class TestableApplication : ApplicationEntity
    {
        public TestableApplication(Guid id, string name, Guid apiKey) : base(new ApplicationName(name), new ApiKey(apiKey))
        {
            Id = id;
        }

        public void AddConfig(ConfigurationEntity env)
        {
            _configurations.Add(env);
        }
    }
}
