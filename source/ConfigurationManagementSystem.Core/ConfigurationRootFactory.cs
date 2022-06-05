using Microsoft.Extensions.Configuration;

namespace ConfigurationManagementSystem.Core
{
    public static class ConfigurationRootFactory
    {
        public static IConfigurationRoot GetConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ConfigurationManagementSystem.Api"))
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile($"appsettings.{ApplicationConstants.EnvironmentName}.json", false)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
