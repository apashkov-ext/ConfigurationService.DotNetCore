using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Domain.Entities;
using System;
using System.Threading.Tasks;

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

        public async Task<ConfigurationEntity> ExecuteAsync(Guid id)
        {
            var app = await _getConfigurationByIdWithoutHierarchyQuery.ExecuteAsync(id);
            return app ?? throw new EntityNotFoundException("Configuration does not exist");
        }
    }
}
