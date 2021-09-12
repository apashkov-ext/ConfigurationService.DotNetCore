using System;
using ConfigurationService.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using ConfigurationService.Domain.Exceptions;

namespace ConfigurationService.Domain.Entities
{
    public class Project : DomainEntity
    {
        public ProjectName Name { get; private set; }
        public ApiKey ApiKey { get; private set; }

        protected readonly List<Environment> _environments = new List<Environment>();
        public IEnumerable<Environment> Environments => _environments;

        protected Project() { }

        protected Project(ProjectName name, ApiKey apiKey)
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
                throw new InconsistentDataStateException("The project already contains environment with the same name");
            }

            var e = Environment.Create(name, this);
            _environments.Add(e);
            return e;
        }
    }
}
