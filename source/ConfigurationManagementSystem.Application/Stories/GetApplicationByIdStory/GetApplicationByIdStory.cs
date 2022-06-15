using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.GetApplicationByIdStory
{
    [Component]
    public class GetApplicationByIdStory
    {
        private readonly GetApplicationByIdWithoutHierarchyQuery _getApplicationByIdQuery;

        public GetApplicationByIdStory(GetApplicationByIdWithoutHierarchyQuery getApplicationByIdQuery)
        {
            _getApplicationByIdQuery = getApplicationByIdQuery;
        }

        public async Task<ApplicationEntity> ExecuteAsync(Guid id)
        {
            var app = await _getApplicationByIdQuery.ExecuteAsync(id);
            return app ?? throw new EntityNotFoundException("Application does not exist");
        }
    }
}
