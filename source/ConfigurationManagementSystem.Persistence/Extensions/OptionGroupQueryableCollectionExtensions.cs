using System.Linq;
using ConfigurationManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationManagementSystem.Persistence.Extensions
{
    internal static class OptionGroupQueryableCollectionExtensions
    {
        public static IQueryable<OptionGroup> OptionGroupsWithIncludedEntities(this IQueryable<OptionGroup> source)
        {
            return source
                .Include(x => x.Parent)
                .Include(x => x.Environment)
                .ThenInclude(x => x.OptionGroups)
                .ThenInclude(x => x.Options)
                .AsSingleQuery();
        }
    }
}
