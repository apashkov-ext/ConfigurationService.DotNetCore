using ConfigurationManagementSystem.Application.Pagination;
using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.GetApplicationsStory
{
    [UserStory]
    public class GetApplicationsStory
    {
        private readonly GetApplicationsWithHierarchyQuery _getApplicationsWithHierarchyQuery;
        private readonly GetApplicationsWithoutHierarchyQuery _getApplicationsWithoutHierarchyQuery;

        public GetApplicationsStory(
            GetApplicationsWithHierarchyQuery getApplicationsWithHierarchyQuery,
            GetApplicationsWithoutHierarchyQuery getApplicationsWithoutHierarchyQuery)
        {
            _getApplicationsWithHierarchyQuery = getApplicationsWithHierarchyQuery;
            _getApplicationsWithoutHierarchyQuery = getApplicationsWithoutHierarchyQuery;
        }

        public async Task<PagedList<ApplicationEntity>> ExecuteAsync(string name, PaginationOptions paginationOptions, bool withHierarchy)
        {
            if (paginationOptions == null) throw new ArgumentNullException(nameof(paginationOptions));

            var result = withHierarchy
                ? await _getApplicationsWithHierarchyQuery.ExecuteAsync()
                : await _getApplicationsWithoutHierarchyQuery.ExecuteAsync();

            if (string.IsNullOrEmpty(name))
            {
                return result.ToPagedList(paginationOptions);
            }

            var filtered = result
                .Where(x => x.Name.Value.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
                .ToPagedList(paginationOptions);

            return filtered;
        }
    }
}
