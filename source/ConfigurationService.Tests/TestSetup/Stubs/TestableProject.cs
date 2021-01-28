using System;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;

namespace ConfigurationService.Tests.TestSetup.Stubs
{
    internal class TestableProject : Project
    {
        public TestableProject(Guid id, string name, Guid apiKey) : base(new ProjectName(name), new ApiKey(apiKey))
        {
            Id = id;
        }
    }
}
