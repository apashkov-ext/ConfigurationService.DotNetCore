using System.Linq;
using ConfigurationManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationManagementSystem.Persistence.Extensions
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
