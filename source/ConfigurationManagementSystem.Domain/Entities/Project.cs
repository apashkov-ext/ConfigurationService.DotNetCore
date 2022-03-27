using System.Collections.Generic;
using System.Linq;
using ConfigurationManagementSystem.Domain.Exceptions;
using ConfigurationManagementSystem.Domain.ValueObjects;

namespace ConfigurationManagementSystem.Domain.Entities
{
    public class Application : DomainEntity
    {
        public ProjectName Name { get; private set; }
        public ApiKey ApiKey { get; private set; }

        protected readonly List<Configuration> _environments = new List<Configuration>();
        public IEnumerable<Configuration> Environments => _environments;

        protected Application() { }

        protected Application(ProjectName name, ApiKey apiKey)
        {
            Name = name;
            ApiKey = apiKey;
        }

        public static Application Create(ProjectName name, ApiKey apiKey)
        {
            return new Application(name, apiKey);
        }

        public Configuration AddEnvironment(EnvironmentName name)
        {
            if (_environments.Any(x => x.Name == name))
            {
                throw new InconsistentDataStateException("The project already contains environment with the same name");
            }

            var e = Configuration.Create(name, this);
            _environments.Add(e);
            return e;
        }
    }
}
