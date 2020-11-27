using ConfigurationService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConfigurationService.Domain.Entities
{
    public class Project
    {
        private readonly List<Configuration> _configs;

        public ProjectName Name { get; }
        public IEnumerable<Configuration> Configurations => _configs;

        public static Project Create(ProjectName name, IEnumerable<Configuration> configs)
        {
            return new Project(name, configs);
        }

        private Project(ProjectName name, IEnumerable<Configuration> configs)
        {
            Name = name;
            _configs = configs.ToList();
        }

        public void DeleteConfig()
        {

        }
    }
}
