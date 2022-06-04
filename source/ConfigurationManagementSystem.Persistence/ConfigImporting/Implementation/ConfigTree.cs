using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Persistence.ConfigImporting.Abstractions;
using System.Collections.Generic;

namespace ConfigurationManagementSystem.Persistence.ConfigImporting.Implementation
{
    public class ConfigTree : Tree<OptionGroupEntity>
    {
        protected override Node<OptionGroupEntity> Root { get; }

        public ConfigTree(OptionGroupEntity root, ConfigurationManagementSystemContext context)
        {
            Root = new ConfigNode(root, context);
        }
    }
}
