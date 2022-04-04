using System;
using ConfigurationManagementSystem.Domain;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationEntity = ConfigurationManagementSystem.Domain.Entities.ConfigurationEntity;

namespace ConfigurationManagementSystem.Tests.Stubs
{
    internal class TestableEnvironment : ConfigurationEntity
    {
        public TestableEnvironment(Guid id, string name, Domain.Entities.ApplicationEntity project) : base(new ConfigurationName(name), project, false)
        {
            Id = id;
        }
    }
}
