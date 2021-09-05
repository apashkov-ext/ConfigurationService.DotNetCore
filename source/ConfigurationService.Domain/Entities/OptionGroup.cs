using System;
using System.Collections.Generic;
using System.Linq;
using ConfigurationService.Domain.ValueObjects;

namespace ConfigurationService.Domain.Entities
{
    public class OptionGroup : DomainEntity
    {
        public OptionGroupName Name { get; private set; }
        public Description Description { get; private set; }
        public OptionGroup Parent { get; }
        public Environment Environment { get; }

        protected readonly List<OptionGroup> _nestedGroups = new List<OptionGroup>();
        public IEnumerable<OptionGroup> NestedGroups => _nestedGroups;

        protected readonly List<Option> _options = new List<Option>();
        public IEnumerable<Option> Options => _options;

        protected OptionGroup() { }

        protected OptionGroup(OptionGroupName name, Description description, Environment environment, OptionGroup parent)
        {
            Name = name;
            Description = description;
            Environment = environment;
            Parent = parent;
        }

        public static OptionGroup Create(OptionGroupName name, Description description, Environment environment, OptionGroup parent = null)
        {
            return new OptionGroup(name, description, environment, parent);
        }

        public Option AddOption(OptionName name, Description description, OptionValue value)
        {
            if (_options.Any(x => x.Name == name))
            {
                throw new ApplicationException("The group already contains option with the same name");
            }

            var o = Option.Create(name, description, value, this);
            _options.Add(o);
            return o;
        }

        public void RemoveOption(Option option)
        {
            _options.Remove(option);
        }

        public void UpdateName(OptionGroupName name)
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

        public OptionGroup AddNestedGroup(OptionGroupName name, Description description)
        {
            if (_nestedGroups.Any(x => x.Name == name))
            {
                throw new ApplicationException("This option group already contains nested group with the same name");
            }

            if (name.Value == string.Empty)
            {
                throw new ApplicationException("Invalid nested group name");
            }

            var g = Create(name, description, Environment, this);
            _nestedGroups.Add(g);
            return g;
        }

        public override string ToString()
        {
            var s = $"{nameof(OptionGroup)} {{ Id={Id}, Name={Name.Value} }}";
            return s;
        }
    }
}
