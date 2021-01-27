using System.Linq;
using ConfigurationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationService.Persistence.Extensions
{
    internal static class OptionGroupCollectionExtensions
    {
        public static IQueryable<OptionGroup> OptionGroupsWithIncludedEntities(this IQueryable<OptionGroup> source)
        {
            return source
                .Include(x => x.Parent)
                .ThenInclude(x => x.NestedGroups)
                .ThenInclude(x => x.Options)
                .Include(x => x.Environment);
        }
    }
}
