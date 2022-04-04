using System;

namespace ConfigurationManagementSystem.Application.AppConfiguration
{
    [AppConfiguration]
    public class SecuritySection
    {
        public string Salt { get; set; }
        public string SymmetricSecurityKey { get; set; }
        public TimeSpan TokenExpiration { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
    }
}
