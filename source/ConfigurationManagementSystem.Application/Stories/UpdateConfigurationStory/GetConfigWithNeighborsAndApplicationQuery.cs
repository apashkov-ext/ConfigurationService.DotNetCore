using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.UpdateConfigurationStory
{
    [Query]
    public abstract class GetConfigWithNeighborsAndApplicationQuery
    {
        public abstract Task<ConfigurationEntity> ExecuteAsync(Guid configId);
    }
}