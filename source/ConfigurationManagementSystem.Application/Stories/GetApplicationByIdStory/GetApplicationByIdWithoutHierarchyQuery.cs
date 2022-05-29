using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.GetApplicationByIdStory
{
    [Component]
    public abstract class GetApplicationByIdWithoutHierarchyQuery
    {
        public abstract Task<ApplicationEntity> ExecuteAsync(Guid id);
    }
}
