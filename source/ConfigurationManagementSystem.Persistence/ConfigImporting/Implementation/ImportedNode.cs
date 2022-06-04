using System.Collections.Generic;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Persistence.ConfigImporting.Abstractions;

namespace ConfigurationManagementSystem.Persistence.ConfigImporting.Implementation
{
    public class ImportedNode : Node<OptionGroupEntity>
    {
        public override OptionGroupEntity Value { get; }
        private readonly List<Node<OptionGroupEntity>> _children = new List<Node<OptionGroupEntity>>();
        public override IEnumerable<Node<OptionGroupEntity>> Children => _children;

        public ImportedNode(OptionGroupEntity value)
        {
            Value = value;

            foreach (var nested in value.NestedGroups)
            {
                _children.Add(new ImportedNode(nested));
            }
        }

        protected override void ReplaceAction(OptionGroupEntity value) { }

        public override Node<OptionGroupEntity> AddChild(OptionGroupEntity value)
        {
            return null;
        }

        public override void RemoveChild(Node<OptionGroupEntity> child) { }

        public override Node<OptionGroupEntity> FindChild(OptionGroupEntity value)
        {
            return null;
        }
    }
}