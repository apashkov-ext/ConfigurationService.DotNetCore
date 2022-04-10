using ConfigurationManagementSystem.Application.Stories.Framework;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.SignInStory
{
    [Query]
    public abstract class GetUserByUsernameQuery
    {
        public abstract Task<UserEntity> ExecuteAsync(Username username);
    }
}
