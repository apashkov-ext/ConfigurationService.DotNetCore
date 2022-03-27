using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Configuration = ConfigurationManagementSystem.Domain.Entities.Configuration;

namespace ConfigurationManagementSystem.Application
{
    public interface IEnvironments
    {
        Task<IEnumerable<Configuration>> GetAsync(string name);
        Task<Configuration> GetAsync(Guid id);
        Task<Configuration> AddAsync(Guid projectId, string name);
        Task UpdateAsync(Guid id, string name);
        Task RemoveAsync(Guid envId);
    }
}
