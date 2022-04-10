using ConfigurationManagementSystem.Domain.ValueObjects;

namespace ConfigurationManagementSystem.Domain.Entities
{
    public class UserEntity : DomainEntity
    {
        public Username Username { get; }
        public HashedPassword PasswordHash { get; private set; }

        protected UserEntity() { }

        protected UserEntity(Username username, HashedPassword passwordHash)
        {
            Username = username;
            PasswordHash = passwordHash;
        }

        public static UserEntity Create(Username username, HashedPassword passwordHash)
        {
            return new UserEntity(username, passwordHash);
        }
    }
}
