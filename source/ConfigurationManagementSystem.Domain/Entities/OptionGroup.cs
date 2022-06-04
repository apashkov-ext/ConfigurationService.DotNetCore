using System.Collections.Generic;
using System.Linq;
using ConfigurationManagementSystem.Domain.Exceptions;
using ConfigurationManagementSystem.Domain.ValueObjects;

namespace ConfigurationManagementSystem.Domain.Entities
{
    public class OptionGroupEntity : DomainEntity
    {
        public OptionGroupName Name { get; private set; }
        public OptionGroupEntity Parent { get; }
        public ConfigurationEntity Configuration { get; }

        protected readonly List<OptionGroupEntity> _nestedGroups = new List<OptionGroupEntity>();
        public IEnumerable<OptionGroupEntity> NestedGroups => _nestedGroups;

        protected readonly List<OptionEntity> _options = new List<OptionEntity>();
        public IEnumerable<OptionEntity> Options => _options;

        protected OptionGroupEntity() { }

        protected OptionGroupEntity(OptionGroupName name, ConfigurationEntity configuration, OptionGroupEntity parent)
        {
            Name = name;
            Configuration = configuration;
            Parent = parent;
        }

        public static OptionGroupEntity Create(OptionGroupName name, ConfigurationEntity configuration, OptionGroupEntity parent = null)
        {
            return new OptionGroupEntity(name, configuration, parent);
        }

        public OptionEntity AddOption(OptionName name, OptionValue value)
        {
            if (_options.Any(x => x.Name == name))
            {
                throw new InconsistentDataStateException("The group already contains option with the same name");
            }

            var o = OptionEntity.Create(name, value, this);
            _options.Add(o);
            return o;
        }

        public void RemoveOption(OptionEntity option)
        {
            _options.Remove(option);
        }

        public void RemoveOptions(IEnumerable<OptionEntity> options)
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

        public OptionGroupEntity AddNestedGroup(OptionGroupName name)
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

        public IEnumerable<OptionGroupEntity> GetOptionGroupsDeep()
        {
            return GetOptionGroupsDeep(this);
        }

        private IEnumerable<OptionGroupEntity> GetOptionGroupsDeep(OptionGroupEntity root)
        {
            yield return root;

            foreach (var group in root.NestedGroups)
            {
                var groups = GetOptionGroupsDeep(group);
                foreach (var g in groups)
                {
                    yield return g;
                }
            }
        }

        public override string ToString()
        {
            var s = $"{nameof(OptionGroupEntity)} {{ Id={Id}, Name={Name.Value} }}";
            return s;
        }
    }
}
