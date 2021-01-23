using System;
using System.Collections.Generic;
using System.Linq;
using ConfigurationService.Domain.ValueObjects;

namespace ConfigurationService.Domain.Entities
{
    public class OptionGroup : Entity
    {
        public OptionGroupName Name { get; private set; }
        public Description Description { get; private set; }
        public OptionGroup Parent { get; private set; }
        public Environment Environment { get; private set; }
        public Guid EnvironmentId { get; private set; }

        private readonly HashSet<OptionGroup> _nestedGroups;
        public IEnumerable<OptionGroup> NestedGroups => _nestedGroups;

        private readonly HashSet<Option> _options;
        public IEnumerable<Option> Options => _options;

        protected OptionGroup() { }

        private OptionGroup(OptionGroupName name, Description description, IEnumerable<Option> options, OptionGroup parent, IEnumerable<OptionGroup> children, Environment environment)
        {
            Name = name;
            Description = description;
            _options = new HashSet<Option>(options);
            Parent = parent;
            _nestedGroups = new HashSet<OptionGroup>(children);
            Environment = environment;
            EnvironmentId = environment?.Id ?? Guid.Empty;
        }

        public static OptionGroup Create(OptionGroupName name, Description description, IEnumerable<Option> options, OptionGroup parent, 
            IEnumerable<OptionGroup> children, 
            Environment environment = null)
        {
            return new OptionGroup(name, description, options, parent, children, environment);
        }

        public void AddNestedGroup(OptionGroup group)
        {
            if (_nestedGroups.Any(x => x.Name == group.Name))
            {
                throw new ApplicationException("The group already contains nested group with the same name");
            }
            _nestedGroups.Add(group);
        }

        public void AddOption(Option option)
        {
            if (_options.Any(x => x.Name == option.Name))
            {
                throw new ApplicationException("The group already contains option with the same name");
            }
            _options.Add(option);
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

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
