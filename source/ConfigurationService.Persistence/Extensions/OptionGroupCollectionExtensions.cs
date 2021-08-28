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
                .Include(x => x.Environment)
                .ThenInclude(x => x.OptionGroups)
                .ThenInclude(x => x.Options)
                .AsSingleQuery();
        }
    }
}
