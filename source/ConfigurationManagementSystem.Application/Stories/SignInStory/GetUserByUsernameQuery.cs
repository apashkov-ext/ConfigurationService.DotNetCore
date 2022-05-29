using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.SignInStory
{
    [Component]
    public abstract class GetUserByUsernameQuery
    {
        public abstract Task<UserEntity> ExecuteAsync(Username username);
    }
}
