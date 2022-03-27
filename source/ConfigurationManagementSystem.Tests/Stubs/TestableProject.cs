using System;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using Configuration = ConfigurationManagementSystem.Domain.Entities.Configuration;

namespace ConfigurationManagementSystem.Tests.Stubs
{
    internal class TestableProject : Domain.Entities.Application
    {
        public TestableProject(Guid id, string name, Guid apiKey) : base(new ProjectName(name), new ApiKey(apiKey))
        {
            Id = id;
        }

        public void AddEnv(Configuration env)
        {
            _environments.Add(env);
        }
    }
}
