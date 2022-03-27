using System;
using ConfigurationManagementSystem.Domain;
using ConfigurationManagementSystem.Domain.Entities;
using Configuration = ConfigurationManagementSystem.Domain.Entities.Configuration;

namespace ConfigurationManagementSystem.Tests.Stubs
{
    internal class TestableEnvironment : Configuration
    {
        public TestableEnvironment(Guid id, string name, Domain.Entities.Application project) : base(new EnvironmentName(name), project, false)
        {
            Id = id;
        }
    }
}
