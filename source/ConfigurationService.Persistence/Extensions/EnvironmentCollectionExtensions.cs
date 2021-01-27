using System.Linq;
using Microsoft.EntityFrameworkCore;
using Environment = ConfigurationService.Domain.Entities.Environment;

namespace ConfigurationService.Persistence.Extensions
{
    internal static class EnvironmentCollectionExtensions
    {
        public static IQueryable<Environment> EnvironmentsWithIncludedEntities(this IQueryable<Environment> source)
        {
            return source
                .Include(x => x.OptionGroups)
                .ThenInclude(x => x.Parent)
                .ThenInclude(x => x.NestedGroups)
                .ThenInclude(x => x.Options);
        }
    }
}
