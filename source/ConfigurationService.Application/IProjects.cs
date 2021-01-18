using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Application
{
    public interface IProjects
    {
        Task<IEnumerable<Project>> Items();
        Task<Project> GetItem(string name);
        Task<Project> Add(string name);
        Task Remove(string name);
    }
}
