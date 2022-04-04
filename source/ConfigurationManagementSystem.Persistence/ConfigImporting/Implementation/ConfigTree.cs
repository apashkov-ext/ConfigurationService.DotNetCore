using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Persistence.ConfigImporting.Abstractions;
using System.Collections.Generic;

namespace ConfigurationManagementSystem.Persistence.ConfigImporting.Implementation
{
    public class ConfigTree : Tree<OptionGroup>
    {
        protected override Node<OptionGroup> Root { get; }

        public ConfigTree(OptionGroup root, ConfigurationManagementSystemContext context)
        {
            Root = new ConfigNode(root, context);
        }
    }
}
