using ConfigurationService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigurationService.Application.Sources
{
    public interface ISourceApi
    {
        Task<IEnumerable<Project>> GetProjects();
        Task<Project> GetProject(string name);
        Task<Project> CreateProjects(string name);
        Task DeleteProject(string name);
        Task<Configuration> GetConfig(string projectName, string environment);
        Task<Configuration> CreateConfig(string projectName, string environment);
        Task UpdateConfig(string projectName, string environment, string content);
        Task DeleteConfig(string projectName, string environment);
    }
}
