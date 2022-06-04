using System.Collections.Generic;
using System.Linq;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Persistence.Extensions
{
    internal static class OptionGroupExtensions
    {
        public static IEnumerable<OptionGroupEntity> ExpandHierarchy(this OptionGroupEntity group)
        {
            var allElements = new List<OptionGroupEntity>{ group };

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

        public static void RemoveWithHierarchy(this OptionGroupEntity group, ConfigurationManagementSystemContext context)
        {
            context.Options.RemoveRange(group.Options);

            foreach (var g in group.NestedGroups)
            {
                RemoveWithHierarchy(g, context);
            }

            context.OptionGroups.Remove(group);
        }
    }
}
