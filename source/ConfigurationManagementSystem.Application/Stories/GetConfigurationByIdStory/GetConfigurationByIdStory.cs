using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Framework.Attributes;
using System;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Application.Extensions;

namespace ConfigurationManagementSystem.Application.Stories.GetConfigurationByIdStory
{
    [Component]
    public class GetConfigurationByIdStory
    {
        private readonly GetConfigurationByIdWithoutHierarchyQuery _getConfigurationByIdWithoutHierarchyQuery;

        public GetConfigurationByIdStory(GetConfigurationByIdWithoutHierarchyQuery getConfigurationByIdWithoutHierarchyQuery)
        {
            _getConfigurationByIdWithoutHierarchyQuery = getConfigurationByIdWithoutHierarchyQuery;
        }

        public async Task<ConfigurationDto> ExecuteAsync(Guid id)
        {
            var config = await _getConfigurationByIdWithoutHierarchyQuery.ExecuteAsync(id);
            if (config is null) throw new EntityNotFoundException("Configuration does not exist");
            return config.ToDto();
        }
    }
}
