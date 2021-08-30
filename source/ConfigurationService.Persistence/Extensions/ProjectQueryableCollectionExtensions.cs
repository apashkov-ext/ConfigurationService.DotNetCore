using System.Linq;
using ConfigurationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationService.Persistence.Extensions
{
    internal static class ProjectQueryableCollectionExtensions
    {
        public static IQueryable<Project> ProjectsWithIncludedEntities(this IQueryable<Project> source)
        {
            return source
                .Include(x => x.Environments)
                .ThenInclude(x => x.OptionGroups)
                .ThenInclude(x => x.Options)
                .AsSingleQuery();
        }
    }
}
