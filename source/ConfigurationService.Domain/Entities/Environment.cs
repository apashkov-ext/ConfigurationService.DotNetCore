using System;
using System.Collections.Generic;

namespace ConfigurationService.Domain.Entities
{
    public class Environment : Entity
    {
        public EnvironmentName Name { get; private set; }
        public bool IsDefault { get; }
        public Project Project { get; }

        //private readonly List<OptionGroup> _optionGroups;
        //public IEnumerable<OptionGroup> OptionGroups => _optionGroups;
        public OptionGroup OptionGroup { get; private set; }

        protected Environment() {}

        private Environment(EnvironmentName name, Project project, bool isDefault, OptionGroup optionGroup)
        {
            Name = name;
            Project = project;
            IsDefault = isDefault;
            OptionGroup = optionGroup ?? throw new ArgumentNullException(nameof(optionGroup));
        }

        public static Environment Create(EnvironmentName name, Project project, bool isDefault, OptionGroup optionGroup)
        {
            return new Environment(name, project, isDefault, optionGroup);
        }

        public void UpdateName(EnvironmentName name)
        {
            Name = name;
        }
    }
}
