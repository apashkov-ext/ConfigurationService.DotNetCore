using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Domain.ValueObjects;
using BCryptNet = BCrypt.Net.BCrypt;

namespace ConfigurationManagementSystem.Application.Stories.SignInStory
{
    public class PasswordVerifier
    {
        public static void VerifyPassword(string password, HashedPassword hashedPassword)
        {
            if (!BCryptNet.Verify(password, hashedPassword.Value))
            {
                throw new InvalidPasswordException();
            }
        }
    }
}
