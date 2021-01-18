using ConfigurationService.Domain.ValueObjects;

namespace ConfigurationService.Domain.Entities
{
    public class Option : Entity
    {
        public OptionName Name { get; private set; }
        public Description Description { get; private set; }
        public OptionValue Value { get; private set; }
        public OptionGroup OptionGroup { get; private set; }

        protected Option() { }

        private Option(OptionName name, Description description, OptionValue value, OptionGroup optionGroup)
        {
            Name = name;
            Description = description;
            Value = value;
            OptionGroup = optionGroup;
        }

        public static Option Create(OptionName name, Description description, OptionValue value, OptionGroup optionGroup)
        {
            return new Option(name, description, value, optionGroup);
        }
    }
}
