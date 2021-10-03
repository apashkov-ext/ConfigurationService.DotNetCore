using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Persistence.ConfigImporting.Abstractions;

namespace ConfigurationManagementSystem.Persistence.ConfigImporting.Implementation
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
