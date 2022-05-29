using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Framework.Bootstrap.ComponentScanning;

namespace ConfigurationManagementSystem.Framework.Bootstrap.ConfigurationSectionDefinitionReading
{
    internal class ConfigurationDefinitionProvider
    {
        private readonly IFrameworkComponentTypeProvider _componentTypeProvider;

        public ConfigurationDefinitionProvider(IFrameworkComponentTypeProvider componentTypeProvider)
        {
            _componentTypeProvider = componentTypeProvider ?? throw new ArgumentNullException(nameof(componentTypeProvider));
        }

        public IEnumerable<ConfigurationSectionDefinition> GetDefinitions()
        {
            var types = _componentTypeProvider.GetComponentTypesByAttribute<ConfigurationSectionAttribute>();
            return types.Select(x =>
            {
                var attr = Attribute.GetCustomAttribute(x, typeof(ConfigurationSectionAttribute)) as ConfigurationSectionAttribute;
                var sectionName = !string.IsNullOrEmpty(attr?.SectionName) ? attr.SectionName : x.Name;
                return new ConfigurationSectionDefinition(sectionName, x);
            });
        }
    }
}
