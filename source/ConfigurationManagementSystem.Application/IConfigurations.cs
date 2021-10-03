using System;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Application
{
    public interface IConfigurations
    {
        Task<OptionGroup> GetItem(string project, string env, string apiKey);
        Task Import(Guid project, string environment, byte[] file);
    }
}
