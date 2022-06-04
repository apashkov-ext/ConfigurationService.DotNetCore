using System;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using ConfigurationEntity = ConfigurationManagementSystem.Domain.Entities.ConfigurationEntity;

namespace ConfigurationManagementSystem.Tests.Stubs
{
    internal class TestableOptionGroup : OptionGroupEntity
    {
        public TestableOptionGroup(Guid id, string name, ConfigurationEntity env, OptionGroupEntity parent) 
            : base(new OptionGroupName(name), env, parent)
        {
            Id = id;
        }

        public void AddOption(OptionEntity option)
        {
            _options.Add(option);
        }

        public void AddNested(OptionGroupEntity group)
        {
            _nestedGroups.Add(group);
        }
    }
}
