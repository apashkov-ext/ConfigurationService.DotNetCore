using ConfigurationManagementSystem.Framework.Attributes;
using System;

namespace ConfigurationManagementSystem.Application.AppConfiguration
{
    [ConfigurationSection]
    public class SecuritySection
    {
        public string Salt { get; private set; }
        public string SymmetricSecurityKey { get; private set; }
        public TimeSpan TokenExpiration { get; private set; }
        public string ValidIssuer { get; private set; }
        public string ValidAudience { get; private set; }
    }
}
