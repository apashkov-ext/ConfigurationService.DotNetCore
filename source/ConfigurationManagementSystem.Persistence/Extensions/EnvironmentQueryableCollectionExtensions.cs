using System.Linq;
using Microsoft.EntityFrameworkCore;
using ConfigurationEntity = ConfigurationManagementSystem.Domain.Entities.ConfigurationEntity;

namespace ConfigurationManagementSystem.Persistence.Extensions
{
    internal static class EnvironmentQueryableCollectionExtensions
    {
        public static IQueryable<ConfigurationEntity> EnvironmentsWithIncludedEntities(this IQueryable<ConfigurationEntity> source)
        {
            return source
                .Include(x => x.OptionGroups)
                //.ThenInclude(x => x.Parent)
                //.ThenInclude(x => x.NestedGroups)
                .ThenInclude(x => x.Options)
                .Include(x => x.Application)
                .AsSingleQuery();
        }
    }
}
