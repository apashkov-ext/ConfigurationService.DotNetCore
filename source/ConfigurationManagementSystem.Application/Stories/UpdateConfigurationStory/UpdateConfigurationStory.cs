using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Application.Stories.Framework;
using ConfigurationManagementSystem.Domain;
using ConfigurationManagementSystem.Domain.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.UpdateConfigurationStory
{
    [UserStory]
    public class UpdateConfigurationStory
    {
        private readonly GetConfigWithNeighborsAndApplicationQuery _getConfigWithNeighborsAndApplicationQuery;
        private readonly UpdateConfigurationNameCommand _updateConfigurationNameCommand;

        public UpdateConfigurationStory(GetConfigWithNeighborsAndApplicationQuery getConfigWithNeighborsAndApplicationQuery,
            UpdateConfigurationNameCommand updateConfigurationNameCommand)
        {
            _getConfigWithNeighborsAndApplicationQuery = getConfigWithNeighborsAndApplicationQuery;
            _updateConfigurationNameCommand = updateConfigurationNameCommand;
        }

        public async Task ExecuteAsync(Guid id, string newName)
        {
            var name = new ConfigurationName(newName);

            var config = await _getConfigWithNeighborsAndApplicationQuery.ExecuteAsync(id);
            if (config == null)
            {
                throw new EntityNotFoundException("Configuration does not exist");
            }

            if (config.Application.Configurations.Any(x => x.Name == name))
            {
                throw new InconsistentDataStateException("Configuration with the same name already exists in this application");
            }

            await _updateConfigurationNameCommand.ExecuteAsync(config, name);
        }
    }
}
