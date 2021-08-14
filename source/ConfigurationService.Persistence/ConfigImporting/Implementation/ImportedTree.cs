using ConfigurationService.Domain.Entities;
using ConfigurationService.Persistence.ConfigImporting.Abstractions;

namespace ConfigurationService.Persistence.ConfigImporting.Implementation
{
    public class ImportedTree : Tree<OptionGroup>
    {
        protected override Node<OptionGroup> Root { get; }

        public ImportedTree(OptionGroup root)
        {
            Root = new ImportedNode(root);
        }
    }
}
