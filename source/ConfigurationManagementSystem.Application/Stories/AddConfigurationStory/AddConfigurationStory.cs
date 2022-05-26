using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Application.Stories.GetConfigurationByIdStory;
using ConfigurationManagementSystem.Domain;
using ConfigurationManagementSystem.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.AddConfigurationStory
{
    [UserStory]
    public class AddConfigurationStory
    {
        private readonly GetApplicationWithConfigurationsByIdQuery _getApplicationWithConfigurationsByIdQuery;
        private readonly CreateConfigurationCommand _createConfigurationCommand;
        private readonly GetConfigurationByIdWithoutHierarchyQuery _getConfigurationByIdWithoutHierarchyQuery;

        public AddConfigurationStory(GetApplicationWithConfigurationsByIdQuery getApplicationWithConfigurationsByIdQuery,
            CreateConfigurationCommand createConfigurationCommand,
            GetConfigurationByIdWithoutHierarchyQuery getConfigurationByIdWithoutHierarchyQuery)
        {
            _getApplicationWithConfigurationsByIdQuery = getApplicationWithConfigurationsByIdQuery;
            _createConfigurationCommand = createConfigurationCommand;
            _getConfigurationByIdWithoutHierarchyQuery = getConfigurationByIdWithoutHierarchyQuery;
        }

        public async Task<ConfigurationEntity> ExecuteAsync(Guid applicationId, string configurationName)
        {
            var app = await _getApplicationWithConfigurationsByIdQuery.ExecuteAsync(applicationId);
            if (app == null)
            {
                throw new EntityNotFoundException("Application does not exist");
            }

            var id = await _createConfigurationCommand.ExecuteAsync(app, new ConfigurationName(configurationName));
            return await _getConfigurationByIdWithoutHierarchyQuery.ExecuteAsync(id);
        }
    }
}
