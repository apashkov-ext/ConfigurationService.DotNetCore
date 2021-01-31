using System;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using Environment = ConfigurationService.Domain.Entities.Environment;

namespace ConfigurationService.Tests.TestSetup.Stubs
{
    internal class TestableOptionGroup : OptionGroup
    {
        public TestableOptionGroup(Guid id, string name, string desc, Environment env, OptionGroup parent) 
            : base(new OptionGroupName(name), new Description(desc), env, parent)
        {
            Id = id;
        }

        public void AddOption(Option option)
        {
            _options.Add(option);
        }

        public void AddNested(OptionGroup group)
        {
            _nestedGroups.Add(group);
        }
    }
}
