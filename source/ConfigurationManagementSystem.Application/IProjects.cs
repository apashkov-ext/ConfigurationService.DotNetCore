using System;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Application.Pagination;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Application
{
    public interface IApplications
    {
        Task<PagedList<ApplicationEntity>> GetAsync(string name, PaginationOptions paginationOptions);
        Task<ApplicationEntity> GetAsync(Guid id);
        Task<ApplicationEntity> AddAsync(string name);
        Task RemoveAsync(Guid id);
    }
}
