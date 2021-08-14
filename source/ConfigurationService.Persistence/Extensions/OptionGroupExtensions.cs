using System.Collections.Generic;
using System.Linq;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Persistence.Extensions
{
    internal static class OptionGroupExtensions
    {
        public static IEnumerable<OptionGroup> WithChildren(this OptionGroup group)
        {
            var allElements = new List<OptionGroup>{ group };

            foreach (var nested in group.NestedGroups)
            {
                if (nested.NestedGroups.Any())
                {
                    allElements.AddRange(WithChildren(nested));
                }
                else
                {
                    allElements.Add(nested);
                }
            }

            return allElements;
        }
    }
}
