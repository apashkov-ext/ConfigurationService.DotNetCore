using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.AddConfigurationStory
{
    [Component]
    public abstract class GetApplicationWithConfigurationsByIdQuery
    {
        public abstract Task<ApplicationEntity> ExecuteAsync(Guid applicationId);
    }
}