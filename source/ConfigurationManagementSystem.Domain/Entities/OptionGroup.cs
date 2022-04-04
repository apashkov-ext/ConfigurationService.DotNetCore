using System.Collections.Generic;
using System.Linq;
using ConfigurationManagementSystem.Domain.Exceptions;
using ConfigurationManagementSystem.Domain.ValueObjects;

namespace ConfigurationManagementSystem.Domain.Entities
{
    public class OptionGroup : DomainEntity
    {
        public OptionGroupName Name { get; private set; }
        public OptionGroup Parent { get; }
        public ConfigurationEntity Configuration { get; }

        protected readonly List<OptionGroup> _nestedGroups = new List<OptionGroup>();
        public IEnumerable<OptionGroup> NestedGroups => _nestedGroups;

        protected readonly List<Option> _options = new List<Option>();
        public IEnumerable<Option> Options => _options;

        protected OptionGroup() { }

        protected OptionGroup(OptionGroupName name, ConfigurationEntity configuration, OptionGroup parent)
        {
            Name = name;
            Configuration = configuration;
            Parent = parent;
        }

        public static OptionGroup Create(OptionGroupName name, ConfigurationEntity configuration, OptionGroup parent = null)
        {
            return new OptionGroup(name, configuration, parent);
        }

        public Option AddOption(OptionName name, OptionValue value)
        {
            if (_options.Any(x => x.Name == name))
            {
                throw new InconsistentDataStateException("The group already contains option with the same name");
            }

            var o = Option.Create(name, value, this);
            _options.Add(o);
            return o;
        }

        public void RemoveOption(Option option)
        {
            _options.Remove(option);
        }

        public void RemoveOptions(IEnumerable<Option> options)
        {
            _options.RemoveAll(x => options.Contains(x));
        }

        public void UpdateName(OptionGroupName name)
        {
            if (Name != name)
            {
                Name = name;
            }
        }

        public OptionGroup AddNestedGroup(OptionGroupName name)
        {
            if (_nestedGroups.Any(x => x.Name == name))
            {
                throw new InconsistentDataStateException("This option group already contains nested group with the same name");
            }

            if (name.Value == string.Empty)
            {
                throw new InconsistentDataStateException("Invalid nested group name");
            }

            var g = Create(name, Configuration, this);
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
