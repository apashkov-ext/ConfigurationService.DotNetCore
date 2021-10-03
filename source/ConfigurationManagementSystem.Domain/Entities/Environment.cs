using System;
using System.Collections.Generic;
using System.Linq;
using ConfigurationManagementSystem.Domain.Exceptions;
using ConfigurationManagementSystem.Domain.ValueObjects;

namespace ConfigurationManagementSystem.Domain.Entities
{
    public class Environment : DomainEntity
    {
        public EnvironmentName Name { get; private set; }
        public bool IsDefault { get; }
        public Project Project { get; }

        private readonly List<OptionGroup> _optionGroups = new List<OptionGroup>();
        public IEnumerable<OptionGroup> OptionGroups => _optionGroups;

        protected Environment() {}

        protected Environment(EnvironmentName name, Project project, bool isDefault)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Project = project ?? throw new ArgumentNullException(nameof(project));
            IsDefault = isDefault;
            SetMainOptionGroup();
        }

        public static Environment Create(EnvironmentName name, Project project)
        {
            return new Environment(name, project, false);
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
            var s = $"{nameof(Environment)} {{ Id={Id}, Name={Name.Value} }}";
            return s;
        }
    }
}
