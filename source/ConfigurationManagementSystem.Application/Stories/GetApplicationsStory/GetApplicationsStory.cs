using ConfigurationManagementSystem.Application.Pagination;
using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Domain.Entities;
using System;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Domain.ValueObjects;

namespace ConfigurationManagementSystem.Application.Stories.GetApplicationsStory
{
    [Component]
    public class GetApplicationsStory
    {
        private readonly IGetApplicationsWithoutHierarchyQuery _getApplicationsWithoutHierarchyQuery;

        public GetApplicationsStory(IGetApplicationsWithoutHierarchyQuery getApplicationsWithoutHierarchyQuery)
        {
            _getApplicationsWithoutHierarchyQuery = getApplicationsWithoutHierarchyQuery;
        }

        public Task<PagedList<ApplicationEntity>> ExecuteAsync(string name, PaginationOptions paginationOptions)
        {
            if (paginationOptions == null) throw new ArgumentNullException(nameof(paginationOptions));

            var nameFilter = string.IsNullOrEmpty(name) ? null : new ApplicationName(name);
            return _getApplicationsWithoutHierarchyQuery.ExecuteAsync(nameFilter, paginationOptions);
        }
    }
}
