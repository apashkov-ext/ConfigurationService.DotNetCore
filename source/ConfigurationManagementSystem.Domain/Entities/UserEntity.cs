using ConfigurationManagementSystem.Domain.ValueObjects;

namespace ConfigurationManagementSystem.Domain.Entities
{
    public class UserEntity : DomainEntity
    {
        public Username Username { get; }
        public Password PasswordHash { get; private set; }

        protected UserEntity() { }

        protected UserEntity(Username username, Password passwordHash)
        {
            Username = username;
            PasswordHash = passwordHash;
        }

        public static UserEntity Create(Username username, Password passwordHash)
        {
            return new UserEntity(username, passwordHash);
        }
    }
}
