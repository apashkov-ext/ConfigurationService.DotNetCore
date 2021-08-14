using System;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using Environment = ConfigurationService.Domain.Entities.Environment;

namespace ConfigurationService.Tests.Stubs
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
