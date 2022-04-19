using ConfigurationManagementSystem.Application.Stories.Framework;
using ConfigurationManagementSystem.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.AddApplicationStory
{
    [Command]
    public abstract class CreateApplicationCommand
    {
        public abstract Task<Guid> ExecuteAsync(ApplicationName name, ApiKey apiKey);
    }
}
