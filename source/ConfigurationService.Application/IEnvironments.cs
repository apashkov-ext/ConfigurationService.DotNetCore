using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Environment = ConfigurationService.Domain.Entities.Environment;

namespace ConfigurationService.Application
{
    public interface IEnvironments
    {
        Task<IEnumerable<Environment>> GetAsync(string name);
        Task<Environment> GetAsync(Guid id);
        Task<Environment> AddAsync(Guid projectId, string name);
        Task UpdateAsync(Guid id, string name);
        Task RemoveAsync(Guid envId);
    }
}
