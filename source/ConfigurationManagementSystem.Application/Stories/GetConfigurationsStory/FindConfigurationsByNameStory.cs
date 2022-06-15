using ConfigurationManagementSystem.Application.Pagination;
using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Application.Dto;

namespace ConfigurationManagementSystem.Application.Stories.GetConfigurationsStory
{
    [Component]
    public class FindConfigurationsByNameStory
    {
        private readonly GetConfigurationsWithoutHierarchyQuery _getConfigurationsWithoutHierarchyQuery;

        public FindConfigurationsByNameStory(GetConfigurationsWithoutHierarchyQuery getConfigurationsWithoutHierarchyQuery)
        {
            _getConfigurationsWithoutHierarchyQuery = getConfigurationsWithoutHierarchyQuery;
        }

        public async Task<PagedList<ConfigurationEntity>> ExecuteAsync(PagedRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var result = await _getConfigurationsWithoutHierarchyQuery.ExecuteAsync();

            if (string.IsNullOrEmpty(request.Name))
            {
                return result.AsPagedList(PaginationOptions.Create(request.Offset, request.Limit));
            }

            var filtered = result
                .Where(x => x.Name.Value.StartsWith(request.Name, StringComparison.InvariantCultureIgnoreCase))
                .AsPagedList(PaginationOptions.Create(request.Offset, request.Limit));

            return filtered;
        }
    }
}
