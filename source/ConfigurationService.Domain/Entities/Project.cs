using ConfigurationService.Domain.ValueObjects;
using System.Collections.Generic;

namespace ConfigurationService.Domain.Entities
{
    public class Project : Entity
    {
        public Name Name { get; private set; }
        public ApiKey ApiKey { get; private set; }

        private readonly HashSet<Environment> _environments;
        public IEnumerable<Environment> Environments => _environments;

        protected Project() { }

        private Project(Name name, ApiKey apiKey, IEnumerable<Environment> environments)
        {
            Name = name;
            ApiKey = apiKey;
            _environments = new HashSet<Environment>(environments);
        }

        public static Project Create(Name name, ApiKey apiKey, IEnumerable<Environment> environments)
        {
            return new Project(name, apiKey, environments);
        }

        public void AddEnvironment(Environment env)
        {

        }
    }
}
