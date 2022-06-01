using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Domain.Entities;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Application.Pagination;
using ConfigurationManagementSystem.Domain.ValueObjects;

namespace ConfigurationManagementSystem.Application.Stories.GetApplicationsStory
{
    [Component]
    public interface IGetApplicationsWithoutHierarchyQuery
    {
        Task<PagedList<ApplicationEntity>> ExecuteAsync(ApplicationName filterByName, PaginationOptions paginationOptions);
    }
}
