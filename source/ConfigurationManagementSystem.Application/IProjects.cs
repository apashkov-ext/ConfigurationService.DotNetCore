using System;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Application.Pagination;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Application
{
    public interface IProjects
    {
        Task<PagedList<Project>> GetAsync(string name, PaginationOptions paginationOptions);
        Task<Project> GetAsync(Guid id);
        Task<Project> AddAsync(string name);
        Task RemoveAsync(Guid id);
    }
}
