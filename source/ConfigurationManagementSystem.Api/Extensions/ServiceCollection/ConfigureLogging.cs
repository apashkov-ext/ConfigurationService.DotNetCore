using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;

namespace ConfigurationManagementSystem.Api.Extensions.ServiceCollection
{
    internal static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureLogging(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
                logging.RequestHeaders.Add("My-Request-Header");
                logging.ResponseHeaders.Add("My-Response-Header");
                logging.MediaTypeOptions.AddText("application/javascript");
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
            }).AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddNLog(new NLogLoggingConfiguration(configuration.GetSection("NLog")));
            });
        }
    }
}
