using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Application
{
    public interface IProjects
    {
        Task<IEnumerable<Project>> Get(string name);
        Task<Project> Get(Guid id);
        Task<Project> Add(string name);
        Task Remove(Guid id);
    }
}
