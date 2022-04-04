using System.Collections.Generic;
using System.Linq;
using ConfigurationManagementSystem.Domain.Exceptions;
using ConfigurationManagementSystem.Domain.ValueObjects;

namespace ConfigurationManagementSystem.Domain.Entities
{
    public class ApplicationEntity : DomainEntity
    {
        public ApplicationName Name { get; private set; }
        public ApiKey ApiKey { get; private set; }

        protected readonly List<ConfigurationEntity> _configurations = new List<ConfigurationEntity>();
        public IEnumerable<ConfigurationEntity> Configurations => _configurations;

        protected ApplicationEntity() { }

        protected ApplicationEntity(ApplicationName name, ApiKey apiKey)
        {
            Name = name;
            ApiKey = apiKey;
        }

        public static ApplicationEntity Create(ApplicationName name, ApiKey apiKey)
        {
            return new ApplicationEntity(name, apiKey);
        }

        public ConfigurationEntity AddConfiguration(ConfigurationName name)
        {
            if (_configurations.Any(x => x.Name == name))
            {
                throw new InconsistentDataStateException("The application already contains configuration with the same name");
            }

            var e = ConfigurationEntity.Create(name, this);
            _configurations.Add(e);
            return e;
        }
    }
}
