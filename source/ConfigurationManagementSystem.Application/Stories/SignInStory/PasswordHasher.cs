using ConfigurationManagementSystem.Domain.ValueObjects;
using BCryptNet = BCrypt.Net.BCrypt;

namespace ConfigurationManagementSystem.Application.Stories.SignInStory
{
    public class PasswordHasher
    {
        public static HashedPassword HashPassword(string password)
        {
            return new HashedPassword(BCryptNet.HashPassword(password));
        }
    }
}
