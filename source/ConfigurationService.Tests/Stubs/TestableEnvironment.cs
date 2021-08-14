using System;
using ConfigurationService.Domain;
using ConfigurationService.Domain.Entities;
using Environment = ConfigurationService.Domain.Entities.Environment;

namespace ConfigurationService.Tests.Stubs
{
    internal class TestableEnvironment : Environment
    {
        public TestableEnvironment(Guid id, string name, Project project) : base(new EnvironmentName(name), project, false)
        {
            Id = id;
        }
    }
}
