namespace ConfigurationManagementSystem.Framework.Bootstrap.ConfigurationSectionDefinitionReading
{
    internal class ConfigurationSectionDefinition
    {
        public string SectionName { get; }
        public Type TypeToBind { get; }

        public ConfigurationSectionDefinition(string sectionName, Type typeToBind)
        {
            SectionName = sectionName ?? throw new ArgumentNullException(nameof(sectionName));
            TypeToBind = typeToBind ?? throw new ArgumentNullException(nameof(typeToBind));
        }
    }
}
