namespace ConfigurationService.Domain.Entities
{
    public class Environment : Entity
    {
        public EnvironmentName Name { get; private set; }
        public bool IsDefault { get; private set; }
        public Project Project { get; private set; }
        public OptionGroup OptionGroup { get; private set; }

        protected Environment() {}

        private Environment(EnvironmentName name, Project project, bool isDefault, OptionGroup configuration)
        {
            Name = name;
            Project = project;
            IsDefault = isDefault;
            OptionGroup = configuration;
        }

        public static Environment Create(EnvironmentName name, Project project, bool isDefault, OptionGroup configuration)
        {
            return new Environment(name, project, isDefault, configuration);
        }

        public void SetOptionGroup(OptionGroup group)
        {
            OptionGroup = group;
        }
    }
}
