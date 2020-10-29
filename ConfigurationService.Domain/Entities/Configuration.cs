using ConfigurationService.Domain.ValueObjects;

namespace ConfigurationService.Domain.Entities
{
    public class Configuration
    {
        public Env Environment { get; }
        public ConfigurationContent Content { get; }

        private Configuration(Env environment, ConfigurationContent content)
        {
            Environment = environment;
            Content = content;
        }

        public static Configuration Create(Env environment, ConfigurationContent content)
        {
            return new Configuration(environment, content);
        }
    }
}
