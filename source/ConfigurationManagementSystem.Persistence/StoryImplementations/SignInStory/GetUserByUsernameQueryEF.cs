using ConfigurationManagementSystem.Application.Stories.SignInStory;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Persistence.StoryImplementations.SignInStory
{
    public class GetUserByUsernameQueryEF : GetUserByUsernameQuery
    {
        private readonly ConfigurationManagementSystemContext _context;

        public GetUserByUsernameQueryEF(ConfigurationManagementSystemContext context)
        {
            _context = context;
        }

        public override Task<UserEntity> ExecuteAsync(Username username)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username.Value == username.Value);
            return Task.FromResult(user);
        }
    }
}
