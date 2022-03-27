using System;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using Configuration = ConfigurationManagementSystem.Domain.Entities.Configuration;

namespace ConfigurationManagementSystem.Tests.Stubs
{
    internal class TestableOptionGroup : OptionGroup
    {
        public TestableOptionGroup(Guid id, string name, string desc, Configuration env, OptionGroup parent) 
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
