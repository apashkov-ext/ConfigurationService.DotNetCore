using ConfigurationManagementSystem.Application.Stories.Framework;
using ConfigurationManagementSystem.Domain;
using ConfigurationManagementSystem.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.AddConfigurationStory
{
    [Query]
    public abstract class CreateConfigurationCommand
    {
        public abstract Task<Guid> ExecuteAsync(ApplicationEntity app, ConfigurationName configurationName);
    }
}