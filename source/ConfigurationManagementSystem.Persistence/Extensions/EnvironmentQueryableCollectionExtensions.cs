using System.Linq;
using Microsoft.EntityFrameworkCore;
using Configuration = ConfigurationManagementSystem.Domain.Entities.Configuration;

namespace ConfigurationManagementSystem.Persistence.Extensions
{
    internal static class EnvironmentQueryableCollectionExtensions
    {
        public static IQueryable<Configuration> EnvironmentsWithIncludedEntities(this IQueryable<Configuration> source)
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
