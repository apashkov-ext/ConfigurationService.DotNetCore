using System.Linq;
using Microsoft.EntityFrameworkCore;
using Environment = ConfigurationManagementSystem.Domain.Entities.Environment;

namespace ConfigurationManagementSystem.Persistence.Extensions
{
    internal static class EnvironmentQueryableCollectionExtensions
    {
        public static IQueryable<Environment> EnvironmentsWithIncludedEntities(this IQueryable<Environment> source)
        {
            return source
                .Include(x => x.OptionGroups)
                //.ThenInclude(x => x.Parent)
                //.ThenInclude(x => x.NestedGroups)
                .ThenInclude(x => x.Options)
                .Include(x => x.Project)
                .AsSingleQuery();
        }
    }
}
