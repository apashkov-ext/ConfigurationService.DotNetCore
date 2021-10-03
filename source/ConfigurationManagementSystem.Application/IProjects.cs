using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Application
{
    public interface IProjects
    {
        Task<IEnumerable<Project>> GetAsync(string name);
        Task<Project> GetAsync(Guid id);
        Task<Project> AddAsync(string name);
        Task RemoveAsync(Guid id);
    }
}
