using ConfigurationManagementSystem.Application.Pagination;
using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.GetConfigurationsStory
{
    [Component]
    public class GetConfigurationsStory
    {
        private readonly GetConfigurationsWithHierarchyQuery _getConfigurationsWithHierarchyQuery;
        private readonly GetConfigurationsWithoutHierarchyQuery _getConfigurationsWithoutHierarchyQuery;

        public GetConfigurationsStory(GetConfigurationsWithHierarchyQuery getConfigurationsWithHierarchyQuery,
            GetConfigurationsWithoutHierarchyQuery getConfigurationsWithoutHierarchyQuery)
        {
            _getConfigurationsWithHierarchyQuery = getConfigurationsWithHierarchyQuery;
            _getConfigurationsWithoutHierarchyQuery = getConfigurationsWithoutHierarchyQuery;
        }

        public async Task<PagedList<ConfigurationEntity>> ExecuteAsync(string name, PaginationOptions paginationOptions, bool withHierarchy)
        {
            if (paginationOptions == null) throw new ArgumentNullException(nameof(paginationOptions));

            var result = withHierarchy
                ? await _getConfigurationsWithHierarchyQuery.ExecuteAsync()
                : await _getConfigurationsWithoutHierarchyQuery.ExecuteAsync();

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
