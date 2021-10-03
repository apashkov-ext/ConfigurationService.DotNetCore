using System.Collections.Generic;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Persistence.ConfigImporting.Abstractions;

namespace ConfigurationManagementSystem.Persistence.ConfigImporting.Implementation
{
    public class ImportedNode : Node<OptionGroup>
    {
        public override OptionGroup Value { get; }
        private readonly List<Node<OptionGroup>> _children = new List<Node<OptionGroup>>();
        public override IEnumerable<Node<OptionGroup>> Children => _children;

        public ImportedNode(OptionGroup value)
        {
            Value = value;

            foreach (var nested in value.NestedGroups)
            {
                _children.Add(new ImportedNode(nested));
            }
        }

        protected override void ReplaceAction(OptionGroup value) { }

        public override Node<OptionGroup> AddChild(OptionGroup value)
        {
            return null;
        }

        public override void RemoveChild(Node<OptionGroup> child) { }

        public override Node<OptionGroup> FindChild(OptionGroup value)
        {
            return null;
        }
    }
}