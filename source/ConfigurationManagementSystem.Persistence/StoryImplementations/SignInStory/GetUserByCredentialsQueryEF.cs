using ConfigurationManagementSystem.Application.Stories.SignInStory;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Persistence.StoryImplementations.SignInStory
{
    public class GetUserByCredentialsQueryEF : GetUserByCredentialsQuery
    {
        private static readonly Username _username = new("Aleksey");
        private static readonly Password _password = new("Qwerty123!");

        public override Task<UserEntity> ExecuteAsync(Username username, Password password)
        {
            if (username == _username && password == _password)
            {
                return Task.FromResult(new UserEntity);
            }
        }
    }
}
