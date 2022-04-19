using System;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;

namespace ConfigurationManagementSystem.Tests.Stubs
{
    internal class TestableOption : OptionEntity
    {
        public TestableOption(Guid id, string name,  string value, OptionGroup group) 
            : base(new OptionName(name), new OptionValue(value), group)
        {
            Id = id;
        }
    }
}
