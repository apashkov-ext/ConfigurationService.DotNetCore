using System;
using ConfigurationManagementSystem.Domain.ValueObjects;

namespace ConfigurationManagementSystem.Domain.Entities
{
    /// <summary>
    /// Single option with type and value.
    /// </summary>
    public class Option : DomainEntity
    {
        public OptionName Name { get; private set; }
        public Description Description { get; private set; }
        public OptionValue Value { get; private set; }
        public OptionGroup OptionGroup { get; }

        protected Option() { }

        protected Option(OptionName name, Description description, OptionValue value, OptionGroup optionGroup)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            OptionGroup = optionGroup ?? throw new ArgumentNullException(nameof(optionGroup));
        }

        public static Option Create(OptionName name, Description description, OptionValue value, OptionGroup optionGroup)
        {
            return new Option(name, description, value, optionGroup);
        }

        public void UpdateName(OptionName name)
        {
            if (Name != name)
            {
                Name = name;
            }
        }

        public void UpdateDescription(Description description)
        {
            if (Description != description)
            {
                Description = description;
            }
        }

        public void UpdateValue(OptionValue optionValue)
        {
            if (Value != optionValue)
            {
                Value = optionValue;
            }
        }

        public override string ToString()
        {
            var s = $"{nameof(Option)} {{ Id={Id}, Name={Name.Value}, Type={Value.Type}, Value={Value.Value} }}";
            return s;
        }
    }
}
