using System;
using System.Collections.Generic;
using ConfigurationService.Domain.ValueObjects;

namespace ConfigurationService.Domain.Entities
{
    public class OptionGroup : Entity
    {
        public Name Name { get; private set; }
        public Description Description { get; private set; }
        public OptionGroup Parent { get; private set; }
        public Environment Environment { get; private set; }
        public Guid EnvironmentId { get; private set; }

        private readonly HashSet<OptionGroup> _children;
        public IEnumerable<OptionGroup> Children => _children;

        private readonly HashSet<Option> _options;
        public IEnumerable<Option> Options => _options;

        protected OptionGroup() { }

        private OptionGroup(Name name, Description description, IEnumerable<Option> options, OptionGroup parent, IEnumerable<OptionGroup> children, Environment environment)
        {
            Name = name;
            Description = description;
            _options = new HashSet<Option>(options);
            Parent = parent;
            _children = new HashSet<OptionGroup>(children);
            Environment = environment;
            EnvironmentId = environment?.Id ?? Guid.Empty;
        }

        public static OptionGroup Create(Name name, Description description, IEnumerable<Option> options, OptionGroup parent, 
            IEnumerable<OptionGroup> children, 
            Environment environment = null)
        {
            return new OptionGroup(name, description, options, parent, children, environment);
        }
    }
}
