using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Application
{
    public interface IProjects
    {
        Task<IEnumerable<Project>> GetAsync(string name);
        Task<Project> GetAsync(Guid id);
        Task<Project> AddAsync(string name);
        Task RemoveAsync(Guid id);
    }
}
