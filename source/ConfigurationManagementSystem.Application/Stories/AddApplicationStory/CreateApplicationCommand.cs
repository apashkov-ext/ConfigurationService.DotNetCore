using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.AddApplicationStory
{
    [Component]
    public abstract class CreateApplicationCommand
    {
        public abstract Task<Guid> ExecuteAsync(ApplicationName name, ApiKey apiKey);
    }
}
