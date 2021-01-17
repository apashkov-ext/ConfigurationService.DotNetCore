using System.Threading.Tasks;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Application
{
    public interface IConfigurations
    {
        Task<OptionGroup> GetConfiguration(string project, string environmnet, string apiKey);
    }
}
