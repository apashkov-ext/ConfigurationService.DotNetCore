using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.AddApplicationStory
{
    [Query]
    public abstract class GetApplicationByNameQuery
    {
        public abstract Task<ApplicationEntity> ExecuteAsync(ApplicationName name);
    }
}
