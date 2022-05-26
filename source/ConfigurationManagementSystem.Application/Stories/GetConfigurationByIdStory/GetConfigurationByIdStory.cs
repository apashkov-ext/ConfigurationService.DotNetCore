using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.GetConfigurationByIdStory
{
    [UserStory]
    public class GetConfigurationByIdStory
    {
        private readonly GetConfigurationByIdWithHierarchyQuery _getConfigurationByIdWithHierarchyQuery;
        private readonly GetConfigurationByIdWithoutHierarchyQuery _getConfigurationByIdWithoutHierarchyQuery;

        public GetConfigurationByIdStory(GetConfigurationByIdWithHierarchyQuery getConfigurationByIdWithHierarchyQuery,
            GetConfigurationByIdWithoutHierarchyQuery getConfigurationByIdWithoutHierarchyQuery)
        {
            _getConfigurationByIdWithHierarchyQuery = getConfigurationByIdWithHierarchyQuery;
            _getConfigurationByIdWithoutHierarchyQuery = getConfigurationByIdWithoutHierarchyQuery;
        }

        public async Task<ConfigurationEntity> ExecuteAsync(Guid id, bool hierarchy)
        {
            var app = hierarchy
                ? await _getConfigurationByIdWithHierarchyQuery.ExecuteAsync(id)
                : await _getConfigurationByIdWithoutHierarchyQuery.ExecuteAsync(id);

            return app ?? throw new EntityNotFoundException("Configuration does not exist");
        }
    }
}
