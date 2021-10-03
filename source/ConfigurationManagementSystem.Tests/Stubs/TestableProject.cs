using System;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using Environment = ConfigurationManagementSystem.Domain.Entities.Environment;

namespace ConfigurationManagementSystem.Tests.Stubs
{
    internal class TestableProject : Project
    {
        public TestableProject(Guid id, string name, Guid apiKey) : base(new ProjectName(name), new ApiKey(apiKey))
        {
            Id = id;
        }

        public void AddEnv(Environment env)
        {
            _environments.Add(env);
        }
    }
}
