using ConfigurationManagementSystem.Application.Pagination;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using ConfigurationManagementSystem.Framework.Attributes;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.FindOptionGroupsByNameStory;

[Component]
public interface IGetOptionGroupsByNameQuery
{
    Task<PagedList<OptionGroupEntity>> ExecuteAsync(OptionGroupName oGroupName, PaginationOptions paginationOptions);
}

