using System;
using System.Collections.Generic;
using System.Linq;
using ConfigurationManagementSystem.Domain.Exceptions;
using ConfigurationManagementSystem.Domain.ValueObjects;

namespace ConfigurationManagementSystem.Domain.Entities
{
    public class Configuration : DomainEntity
    {
        public EnvironmentName Name { get; private set; }
        public bool IsDefault { get; }
        public Application Project { get; }

        private readonly List<OptionGroup> _optionGroups = new List<OptionGroup>();
        public IEnumerable<OptionGroup> OptionGroups => _optionGroups;

        protected Configuration() {}

        protected Configuration(EnvironmentName name, Application project, bool isDefault)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Project = project ?? throw new ArgumentNullException(nameof(project));
            IsDefault = isDefault;
            SetMainOptionGroup();
        }

        public static Configuration Create(EnvironmentName name, Application project)
        {
            return new Configuration(name, project, false);
        }

        public void UpdateName(EnvironmentName name)
        {
            Name = name;
        }

        public OptionGroup GetRootOptionGroop()
        {
            var root = _optionGroups.SingleOrDefault(x => x.Parent == null);
            if (root == null)
            {
                throw new InconsistentDataStateException("The root option group cannot be null");
            }
            return root;
        }

        private void SetMainOptionGroup()
        {
            var g = OptionGroup.Create(new OptionGroupName(""), new Description(""), this);
            _optionGroups.Add(g);
        }

        public override string ToString()
        {
            var s = $"{nameof(Configuration)} {{ Id={Id}, Name={Name.Value} }}";
            return s;
        }
    }
}
