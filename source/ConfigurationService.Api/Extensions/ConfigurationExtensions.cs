using Microsoft.Extensions.Configuration;
using System;

namespace ConfigurationService.Api.Extensions
{
    internal static class ConfigurationExtensions
    {
        public static string[] GetAllowedHosts(this IConfiguration config)
        {
            var section = config?["AllowedHosts"];
            var splitted = section?.Split(";", StringSplitOptions.RemoveEmptyEntries);
            var hosts = splitted ?? Array.Empty<string>();
            return hosts;
        }
    }
}
