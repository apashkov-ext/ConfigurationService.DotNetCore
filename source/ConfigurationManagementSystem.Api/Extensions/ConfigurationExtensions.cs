using System;
using Microsoft.Extensions.Configuration;

namespace ConfigurationManagementSystem.Api.Extensions
{
    internal static class ConfigurationExtensions
    {
        public static string[] GetOrigins(this IConfiguration config)
        {
            var value = config?["Web.Origins"];
            var splitted = value?.Split(";", StringSplitOptions.RemoveEmptyEntries);
            var hosts = splitted ?? Array.Empty<string>();
            return hosts;
        }
    }
}
