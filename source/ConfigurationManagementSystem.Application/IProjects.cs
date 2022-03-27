using System;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Application.Pagination;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Application
{
    public interface IProjects
    {
        Task<PagedList<Domain.Entities.Application>> GetAsync(string name, PaginationOptions paginationOptions);
        Task<Domain.Entities.Application> GetAsync(Guid id);
        Task<Domain.Entities.Application> AddAsync(string name);
        Task RemoveAsync(Guid id);
    }
}
