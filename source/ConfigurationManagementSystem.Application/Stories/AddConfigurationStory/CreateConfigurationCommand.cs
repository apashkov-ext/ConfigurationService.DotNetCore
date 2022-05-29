using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Domain;
using ConfigurationManagementSystem.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.AddConfigurationStory
{
    [Component]
    public abstract class CreateConfigurationCommand
    {
        public abstract Task<Guid> ExecuteAsync(ApplicationEntity app, ConfigurationName configurationName);
    }
}