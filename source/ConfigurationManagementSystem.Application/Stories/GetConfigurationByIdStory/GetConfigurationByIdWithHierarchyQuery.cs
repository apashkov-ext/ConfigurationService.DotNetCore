using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.GetConfigurationByIdStory
{
    [Component]
    public abstract class GetConfigurationByIdWithHierarchyQuery
    {
        public abstract Task<ConfigurationEntity> ExecuteAsync(Guid id);
    }
}
