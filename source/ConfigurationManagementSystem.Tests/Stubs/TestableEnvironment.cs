using System;
using ConfigurationManagementSystem.Domain;
using ConfigurationManagementSystem.Domain.Entities;
using Environment = ConfigurationManagementSystem.Domain.Entities.Environment;

namespace ConfigurationManagementSystem.Tests.Stubs
{
    internal class TestableEnvironment : Environment
    {
        public TestableEnvironment(Guid id, string name, Project project) : base(new EnvironmentName(name), project, false)
        {
            Id = id;
        }
    }
}
