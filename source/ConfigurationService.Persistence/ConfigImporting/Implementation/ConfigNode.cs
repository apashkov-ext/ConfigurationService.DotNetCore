using System;
using System.Collections.Generic;
using System.Linq;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Persistence.ConfigImporting.Abstractions;
using ConfigurationService.Persistence.Extensions;

namespace ConfigurationService.Persistence.ConfigImporting.Implementation
{
    public class ConfigNode : Node<OptionGroup>
    {
        private readonly ConfigurationServiceContext _context;
        public override OptionGroup Value { get; }

        private readonly List<Node<OptionGroup>> _children = new List<Node<OptionGroup>>();
        public override IEnumerable<Node<OptionGroup>> Children => _children;

        public ConfigNode(OptionGroup value, ConfigurationServiceContext context)
        {
            _context = context;
            Value = value;

            foreach (var nested in value.NestedGroups)
            {
                _children.Add(new ConfigNode(nested, context));
            }
        }

        private ConfigNode(OptionGroup value, bool visited, ConfigurationServiceContext context)
        {
            _context = context;
            Value = value;
            Visited = visited;
        }

        protected override void ReplaceAction(OptionGroup value)
        {
            if (!value.Options.Any())
            {
                foreach (var option in Value.Options.ToArray())
                {
                    Value.RemoveOption(option);
                }

                return;
            }

            var notVisitedOptions = new List<Option>(Value.Options);

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
                    Value.AddOption(valueOption.Name, valueOption.Description, valueOption.Value);
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

        public override Node<OptionGroup> AddChild(OptionGroup value)
        {
            var group = Value.AddNestedGroup(value.Name, value.Description);
            AddOptions(group, value.Options);

            var child = new ConfigNode(group, true, _context);
            _children.Add(child);

            foreach (var nested in value.NestedGroups)
            {
                child.AddChild(nested);
            }

            return child;
        }

        public override void RemoveChild(Node<OptionGroup> child)
        {
            var allElements = child.Value.ExpandHierarchy();
            _context.OptionGroups.RemoveRange(allElements);
            _children.Remove(child);
        }

        public override Node<OptionGroup> FindChild(OptionGroup value)
        {
            return _children.FirstOrDefault(x => x.Value.Name.Value.Equals(value.Name.Value, StringComparison.InvariantCultureIgnoreCase));
        }

        private static void UpdateOption(Option dest, Option source)
        {
            dest.UpdateValue(source.Value);
        }

        private static void AddOptions(OptionGroup group, IEnumerable<Option> options)
        {
            foreach (var sourceOption in options)
            {
                group.AddOption(sourceOption.Name, sourceOption.Description, sourceOption.Value);
            }
        }
    }
}