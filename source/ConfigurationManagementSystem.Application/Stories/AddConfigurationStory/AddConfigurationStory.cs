using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Application.Stories.GetConfigurationByIdStory;
using ConfigurationManagementSystem.Domain;
using System;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Application.Extensions;

namespace ConfigurationManagementSystem.Application.Stories.AddConfigurationStory
{
    [Component]
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

        public async Task<ConfigurationDto> ExecuteAsync(CreateConfigurationDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            var app = await _getApplicationWithConfigurationsByIdQuery.ExecuteAsync(dto.Application);
            if (app == null)
            {
                throw new EntityNotFoundException("Application does not exist");
            }

            var id = await _createConfigurationCommand.ExecuteAsync(app, new ConfigurationName(dto.Name));
            var created = await _getConfigurationByIdWithoutHierarchyQuery.ExecuteAsync(id);

            return created.ToDto();
        }
    }
}
