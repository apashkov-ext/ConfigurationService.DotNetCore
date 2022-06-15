using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.GetConfigurationByIdStory
{
    [Component]
    public interface GetConfigurationByIdWithoutHierarchyQuery
    {
        Task<ConfigurationEntity> ExecuteAsync(Guid id);
    }
}
