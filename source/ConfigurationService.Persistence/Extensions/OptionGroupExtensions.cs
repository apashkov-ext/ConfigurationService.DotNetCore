using System.Collections.Generic;
using System.Linq;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Persistence.Extensions
{
    internal static class OptionGroupExtensions
    {
        public static IEnumerable<OptionGroup> ExpandHierarchy(this OptionGroup group)
        {
            var allElements = new List<OptionGroup>{ group };

            foreach (var nested in group.NestedGroups)
            {
                if (nested.NestedGroups.Any())
                {
                    allElements.AddRange(ExpandHierarchy(nested));
                }
                else
                {
                    allElements.Add(nested);
                }
            }

            return allElements;
        }

        public static void RemoveOptionGroupWithHierarchy(this OptionGroup group, ConfigurationServiceContext context)
        {
            context.Options.RemoveRange(group.Options);

            foreach (var g in group.NestedGroups)
            {
                RemoveOptionGroupWithHierarchy(g, context);
            }

            context.OptionGroups.Remove(group);
        }
    }
}
