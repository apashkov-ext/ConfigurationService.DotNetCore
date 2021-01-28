using System;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using Environment = ConfigurationService.Domain.Entities.Environment;

namespace ConfigurationService.Tests.TestSetup.Stubs
{
    internal class TestableOptionGroup : OptionGroup
    {
        public TestableOptionGroup(Guid id, string name, string desc, Environment env) : base(new OptionGroupName(name), new Description(desc), env, null)
        {
            Id = id;
        }
    }
}
