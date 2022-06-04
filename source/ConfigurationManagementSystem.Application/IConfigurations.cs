using System;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Application
{
    public interface IConfigurations
    {
        Task<OptionGroupEntity> GetItem(string project, string env, string apiKey);
        Task Import(Guid app, string environment, byte[] file);
    }
}
