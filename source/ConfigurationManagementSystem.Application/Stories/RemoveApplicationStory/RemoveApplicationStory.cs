using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Application.Stories.Framework;
using ConfigurationManagementSystem.Application.Stories.GetApplicationByIdStory;
using ConfigurationManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.RemoveApplicationStory
{
    [UserStory]
    public class RemoveApplicationStory
    {
        private readonly GetApplicationByIdWithHierarchyQuery _getApplicationByIdWithHierarchyQuery;
        private readonly DeleteEntitiesCommand _deleteEntitiesCommand;

        public RemoveApplicationStory(
            GetApplicationByIdWithHierarchyQuery getApplicationByIdWithHierarchyQuery,
            DeleteEntitiesCommand deleteEntitiesCommand)
        {
            _getApplicationByIdWithHierarchyQuery = getApplicationByIdWithHierarchyQuery;
            _deleteEntitiesCommand = deleteEntitiesCommand;
        }

        public async Task ExecuteAsync(Guid id)
        {
            var application = await _getApplicationByIdWithHierarchyQuery.ExecuteAsync(id) ?? throw new NotFoundException("Project does not exist");

            var configs = application.Configurations;
            var groups = application.Configurations.SelectMany(x => GetOptionGroupsDeep(x.GetRootOptionGroop()));
            var options = groups.SelectMany(x => x.Options);
            var entities = new DomainEntity[] { application }.AsEnumerable()
                .Concat(configs)
                .Concat(groups)
                .Concat(options);

            await _deleteEntitiesCommand.ExecuteAsync(entities);
        }

        private IEnumerable<OptionGroup> GetOptionGroupsDeep(OptionGroup root)
        {
            yield return root;

            foreach(var group in root.NestedGroups)
            {
                var groups = GetOptionGroupsDeep(group);
                foreach(var g in groups)
                {
                    yield return g;
                }
            }
        }
    }
}
