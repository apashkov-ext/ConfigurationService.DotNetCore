using System;
using ConfigurationService.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace ConfigurationService.Domain.Entities
{
    public class Project : Entity
    {
        public ProjectName Name { get; private set; }
        public ApiKey ApiKey { get; private set; }

        private readonly List<Environment> _environments = new List<Environment>();
        public IEnumerable<Environment> Environments => _environments;

        protected Project() { }

        private Project(ProjectName name, ApiKey apiKey)
        {
            Name = name;
            ApiKey = apiKey;
        }

        public static Project Create(ProjectName name, ApiKey apiKey)
        {
            return new Project(name, apiKey);
        }

        public Environment AddEnvironment(EnvironmentName name)
        {
            if (_environments.Any(x => x.Name == name))
            {
                throw new ApplicationException("The project already contains environment with the same name");
            }

            var e = Environment.Create(name, this);
            _environments.Add(e);
            return e;
        }
    }
}
