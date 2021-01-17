using System.Linq;
using ConfigurationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationService.Persistence
{
    internal static class ProjectCollectionExtensions
    {
        public static IQueryable<Project> ProjectsWithIncludedEntities(this IQueryable<Project> source)
        {
            return source
                .Include(x => x.Environments)
                .ThenInclude(x => x.OptionGroup)
                .ThenInclude(x => x.NestedGroups)
                .ThenInclude(x => x.Options);
        }
    }
}
