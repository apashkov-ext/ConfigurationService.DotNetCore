using ConfigurationService.Domain.ValueObjects;
using System.Collections.Generic;

namespace ConfigurationService.Domain.Entities
{
    public class Project : Entity
    {
        public ProjectName Name { get; private set; }
        public ApiKey ApiKey { get; private set; }

        private readonly HashSet<Environment> _environments = new HashSet<Environment>();
        public IEnumerable<Environment> Environments => _environments;

        protected Project() { }

        private Project(ProjectName name, ApiKey apiKey, IEnumerable<Environment> environments)
        {
            Name = name;
            ApiKey = apiKey;
            _environments = new HashSet<Environment>(environments);
        }

        public static Project Create(ProjectName name, ApiKey apiKey, IEnumerable<Environment> environments)
        {
            return new Project(name, apiKey, environments);
        }

        public void AddEnvironment(Environment env)
        {
            _environments.Add(env);
        }
    }
}
