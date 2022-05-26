using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Domain;
using ConfigurationManagementSystem.Domain.Entities;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.UpdateConfigurationStory
{
    [Command]
    public abstract class UpdateConfigurationNameCommand
    {
        public abstract Task ExecuteAsync(ConfigurationEntity config, ConfigurationName name);
    }
}