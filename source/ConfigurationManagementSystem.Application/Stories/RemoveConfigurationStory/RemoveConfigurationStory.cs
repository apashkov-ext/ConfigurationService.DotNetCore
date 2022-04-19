using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Application.Stories.Commands;
using ConfigurationManagementSystem.Application.Stories.Framework;
using ConfigurationManagementSystem.Application.Stories.GetConfigurationByIdStory;
using ConfigurationManagementSystem.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.RemoveConfigurationStory
{
    [UserStory]
    public class RemoveConfigurationStory
    {
        private readonly GetConfigurationByIdWithHierarchyQuery _getConfigurationByIdWithHierarchyQuery;
        private readonly DeleteEntitiesCommand _deleteEntitiesCommand;

        public RemoveConfigurationStory(GetConfigurationByIdWithHierarchyQuery getConfigurationByIdWithHierarchyQuery,
            DeleteEntitiesCommand deleteEntitiesCommand)
        {
            _getConfigurationByIdWithHierarchyQuery = getConfigurationByIdWithHierarchyQuery;
            _deleteEntitiesCommand = deleteEntitiesCommand;
        }

        public async Task ExecuteAsync(Guid configId)
        {
            var config = await _getConfigurationByIdWithHierarchyQuery.ExecuteAsync(configId) 
                ?? throw new EntityNotFoundException("Configuration does not exist", configId);

            var groups = config.GetRootOptionGroop().GetOptionGroupsDeep();
            var options = groups.SelectMany(x => x.Options);
            var entities = new DomainEntity[] { config }.AsEnumerable()
                .Concat(groups)
                .Concat(options);

            await _deleteEntitiesCommand.ExecuteAsync(entities);
        }
    }
}
