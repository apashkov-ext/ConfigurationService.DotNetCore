using System;
using System.Collections.Generic;
using System.Linq;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Persistence.ConfigImporting.Abstractions;
using ConfigurationManagementSystem.Persistence.Extensions;

namespace ConfigurationManagementSystem.Persistence.ConfigImporting.Implementation
{
    public class ConfigNode : Node<OptionGroupEntity>
    {
        private readonly ConfigurationManagementSystemContext _context;
        public override OptionGroupEntity Value { get; }

        private readonly List<Node<OptionGroupEntity>> _children = new List<Node<OptionGroupEntity>>();
        public override IEnumerable<Node<OptionGroupEntity>> Children => _children;

        public ConfigNode(OptionGroupEntity value, ConfigurationManagementSystemContext context)
        {
            _context = context;
            Value = value;

            foreach (var nested in value.NestedGroups)
            {
                _children.Add(new ConfigNode(nested, context));
            }
        }

        private ConfigNode(OptionGroupEntity value, bool visited, ConfigurationManagementSystemContext context)
        {
            _context = context;
            Value = value;
            Visited = visited;
        }

        protected override void ReplaceAction(OptionGroupEntity value)
        {
            if (!value.Options.Any())
            {
                foreach (var option in Value.Options.ToArray())
                {
                    Value.RemoveOption(option);
                }

                return;
            }

            var notVisitedOptions = new List<OptionEntity>(Value.Options);

            foreach (var valueOption in value.Options)
            {
                var existed = Value.Options.FirstOrDefault(x => x.Name.Value.Equals(valueOption.Name.Value, StringComparison.InvariantCultureIgnoreCase));
                if (existed != null)
                {
                    UpdateOption(existed, valueOption);
                    notVisitedOptions.Remove(existed);
                }
                else
                {
                    Value.AddOption(valueOption.Name, valueOption.Value);
                }
            }

            if (!notVisitedOptions.Any())
            {
                return;
            }
            
            foreach (var option in notVisitedOptions.ToArray())
            {
                Value.RemoveOption(option);
            }
        }

        public override Node<OptionGroupEntity> AddChild(OptionGroupEntity value)
        {
            var group = Value.AddNestedGroup(value.Name);
            AddOptions(group, value.Options);

            var child = new ConfigNode(group, true, _context);
            _children.Add(child);

            foreach (var nested in value.NestedGroups)
            {
                child.AddChild(nested);
            }

            return child;
        }

        public override void RemoveChild(Node<OptionGroupEntity> child)
        {
            var allElements = child.Value.ExpandHierarchy();
            _context.OptionGroups.RemoveRange(allElements);
            _children.Remove(child);
        }

        public override Node<OptionGroupEntity> FindChild(OptionGroupEntity value)
        {
            return _children.FirstOrDefault(x => x.Value.Name.Value.Equals(value.Name.Value, StringComparison.InvariantCultureIgnoreCase));
        }

        private static void UpdateOption(OptionEntity dest, OptionEntity source)
        {
            dest.UpdateValue(source.Value);
        }

        private static void AddOptions(OptionGroupEntity group, IEnumerable<OptionEntity> options)
        {
            foreach (var sourceOption in options)
            {
                group.AddOption(sourceOption.Name, sourceOption.Value);
            }
        }
    }
}