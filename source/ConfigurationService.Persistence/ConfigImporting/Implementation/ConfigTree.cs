using ConfigurationService.Domain.Entities;
using ConfigurationService.Persistence.ConfigImporting.Abstractions;

namespace ConfigurationService.Persistence.ConfigImporting.Implementation
{
    public class ConfigTree : Tree<OptionGroup>
    {
        protected override Node<OptionGroup> Root { get; }

        public ConfigTree(OptionGroup root, ConfigurationServiceContext context)
        {
            Root = new ConfigNode(root, context);
        }
    }
}
