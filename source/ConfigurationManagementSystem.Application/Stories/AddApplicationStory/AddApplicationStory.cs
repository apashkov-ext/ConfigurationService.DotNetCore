using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Application.Stories.GetApplicationByIdStory;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.AddApplicationStory
{
    [Component]
    public class AddApplicationStory
    {
        private readonly GetApplicationByNameQuery _getApplicationByNameQuery;
        private readonly CreateApplicationCommand _createApplicationCommand;
        private readonly GetApplicationByIdWithoutHierarchyQuery _getApplicationByIdWithoutHierarchyQuery;

        public AddApplicationStory(GetApplicationByNameQuery getApplicationByNameQuery,
            CreateApplicationCommand createApplicationCommand,
            GetApplicationByIdWithoutHierarchyQuery getApplicationByIdWithoutHierarchyQuery)
        {
            _getApplicationByNameQuery = getApplicationByNameQuery;
            _createApplicationCommand = createApplicationCommand;
            _getApplicationByIdWithoutHierarchyQuery = getApplicationByIdWithoutHierarchyQuery;
        }

        public async Task<ApplicationEntity> ExecuteAsync(string name)
        {
            var appName = new ApplicationName(name);
            var existed = await _getApplicationByNameQuery.ExecuteAsync(appName);
            if (existed != null)
            {
                throw new AlreadyExistsException("Application with the same name already exists");
            }

            var id = await _createApplicationCommand.ExecuteAsync(appName, new ApiKey(Guid.NewGuid()));
            return await _getApplicationByIdWithoutHierarchyQuery.ExecuteAsync(id);
        }
    }
}
