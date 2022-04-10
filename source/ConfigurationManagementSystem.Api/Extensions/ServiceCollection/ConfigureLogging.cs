using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace ConfigurationManagementSystem.Api.Extensions.ServiceCollection
{
    internal static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureLogging(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
                loggingBuilder.AddNLog(configuration);
            });
        }
    }
}
