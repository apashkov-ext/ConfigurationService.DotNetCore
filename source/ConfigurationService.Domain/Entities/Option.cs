using ConfigurationService.Domain.ValueObjects;

namespace ConfigurationService.Domain.Entities
{
    public class Option : Entity
    {
        public Name Name { get; private set; }
        public Description Description { get; private set; }
        public OptionValue Value { get; private set; }
        public OptionValueType Type { get; private set; }
        public OptionGroup OptionGroup { get; private set; }

        protected Option() { }

        private Option(Name name, Description description, OptionValue value, OptionValueType type, OptionGroup optionGroup)
        {
            Name = name;
            Description = description;
            Value = value;
            Type = type;
            OptionGroup = optionGroup;
        }

        public static Option Create(Name name, Description description, OptionValue value, OptionValueType type, OptionGroup optionGroup)
        {
            return new Option(name, description, value, type, optionGroup);
        }
    }
}
