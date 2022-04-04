using System.Linq;
using ConfigurationManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationManagementSystem.Persistence.Extensions
{
    internal static class ProjectQueryableCollectionExtensions
    {
        /// <summary>
        /// Returns Applications included full hierarchy.
        /// </summary>
        /// <param name="source"></param>
        public static IQueryable<ApplicationEntity> ApplicationsWithIncludedEntities(this IQueryable<ApplicationEntity> source)
        {
            return source
                .Include(x => x.Configurations)
                .ThenInclude(x => x.OptionGroups)
                .ThenInclude(x => x.Options)
                .AsSingleQuery();
        }
    }
}
