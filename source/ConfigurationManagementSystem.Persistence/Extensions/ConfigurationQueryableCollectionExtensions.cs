using System.Linq;
using Microsoft.EntityFrameworkCore;
using ConfigurationEntity = ConfigurationManagementSystem.Domain.Entities.ConfigurationEntity;

namespace ConfigurationManagementSystem.Persistence.Extensions
{
    internal static class ConfigurationQueryableCollectionExtensions
    {
        public static IQueryable<ConfigurationEntity> ConfigurationsWithIncludedEntities(this IQueryable<ConfigurationEntity> source)
        {
            return source
                .Include(x => x.OptionGroups)
                .ThenInclude(x => x.Options)
                .Include(x => x.Application)
                .AsSingleQuery();
        }
    }
}
