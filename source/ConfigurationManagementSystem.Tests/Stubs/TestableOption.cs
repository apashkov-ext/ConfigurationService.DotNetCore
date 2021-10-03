using System;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;

namespace ConfigurationManagementSystem.Tests.Stubs
{
    internal class TestableOption : Option
    {
        public TestableOption(Guid id, string name, string desc, string value, OptionGroup group) 
            : base(new OptionName(name), new Description(desc), new OptionValue(value), group)
        {
            Id = id;
        }
    }
}
