using System.Linq;
using ConfigurationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationService.Persistence.Extensions
{
    internal static class OptionGroupDbSetExtensions
    {
        public static void RemoveRecursive(this DbSet<OptionGroup> db, OptionGroup group)
        {
            db.Remove(group);

            foreach (var nested in group.NestedGroups)
            {
                db.Remove(nested);
                if (nested.NestedGroups.Any())
                {
                    db.RemoveRecursive(nested);
                }
            }
        }
    }
}
