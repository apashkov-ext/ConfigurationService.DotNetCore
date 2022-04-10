using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Application.Stories.Framework;
using ConfigurationManagementSystem.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.GetApplicationByIdStory
{
    [UserStory]
    public class GetApplicationByIdStory
    {
        private readonly GetApplicationByIdWithHierarchyQuery _getApplicationByIdWithHierarchyQuery;
        private readonly GetApplicationByIdWithoutHierarchyQuery _getApplicationByIdWithoutHierarchyQuery;

        public GetApplicationByIdStory(
            GetApplicationByIdWithHierarchyQuery getApplicationByIdWithHierarchyQuery,
            GetApplicationByIdWithoutHierarchyQuery getApplicationByIdWithoutHierarchyQuery)
        {
            _getApplicationByIdWithHierarchyQuery = getApplicationByIdWithHierarchyQuery;
            _getApplicationByIdWithoutHierarchyQuery = getApplicationByIdWithoutHierarchyQuery;
        }

        public async Task<ApplicationEntity> ExecuteAsync(Guid id, bool hierarchy)
        {
            var app = hierarchy
                ? await _getApplicationByIdWithHierarchyQuery.ExecuteAsync(id)
                : await _getApplicationByIdWithoutHierarchyQuery.ExecuteAsync(id);

            return app ?? throw new EntityNotFoundException("Project does not exist");
        }
    }
}
