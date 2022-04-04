using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigurationEntity = ConfigurationManagementSystem.Domain.Entities.ConfigurationEntity;

namespace ConfigurationManagementSystem.Application
{
    public interface IEnvironments
    {
        Task<IEnumerable<ConfigurationEntity>> GetAsync(string name);
        Task<ConfigurationEntity> GetAsync(Guid id);
        Task<ConfigurationEntity> AddAsync(Guid projectId, string name);
        Task UpdateAsync(Guid id, string name);
        Task RemoveAsync(Guid envId);
    }
}
