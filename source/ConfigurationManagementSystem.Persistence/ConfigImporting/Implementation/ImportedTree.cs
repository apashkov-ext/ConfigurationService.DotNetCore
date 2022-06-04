using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Persistence.ConfigImporting.Abstractions;

namespace ConfigurationManagementSystem.Persistence.ConfigImporting.Implementation
{
    public class ImportedTree : Tree<OptionGroupEntity>
    {
        protected override Node<OptionGroupEntity> Root { get; }

        public ImportedTree(OptionGroupEntity root)
        {
            Root = new ImportedNode(root);
        }
    }
}
