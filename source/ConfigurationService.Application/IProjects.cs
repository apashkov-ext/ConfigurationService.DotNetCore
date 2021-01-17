using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Application
{
    public interface IProjects
    {
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> GetProjectByName(string name);
    }
}
