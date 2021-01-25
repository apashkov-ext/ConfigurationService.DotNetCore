using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Environment = ConfigurationService.Domain.Entities.Environment;

namespace ConfigurationService.Application
{
    public interface IEnvironments
    {
        Task<IEnumerable<Environment>> Get();
        Task<Environment> Get(Guid id);
        Task<Environment> Add(Guid projectId, string name);
        Task Update(Guid id, string name);
        Task Remove(Guid envId);
    }
}
