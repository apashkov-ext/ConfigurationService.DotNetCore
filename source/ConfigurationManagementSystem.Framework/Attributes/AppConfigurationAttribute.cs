using ConfigurationManagementSystem.Framework.Exceptions;
using System.Text.RegularExpressions;

namespace ConfigurationManagementSystem.Framework.Attributes
{
    public class ConfigurationSectionAttribute : FrameworkComponentAttribute
    {
        private static readonly Regex _regex = new(@"^[a-zA-Z]+\w+");
        public string SectionName { get; } = string.Empty;

        public ConfigurationSectionAttribute()
        {
        }

        /// <summary>
        /// Creates instance of <see cref="ConfigurationSectionAttribute" /> with specified section name value.
        /// <para/>
        /// Valid section names: section, sectionName, SectionName, section_name2.
        /// <para/>
        /// Invalid section names: 25, 25sectionName, _sectionName.
        /// </summary>
        /// <param name="sectionName"></param>
        /// <exception cref="InvalidConfigurationSectionNameException"></exception>
        public ConfigurationSectionAttribute(string sectionName)
        {
            if (!_regex.IsMatch(sectionName))
            {
                throw new InvalidConfigurationSectionNameException("Invalid section name.");
            }
            SectionName = sectionName;
        }
    }
}