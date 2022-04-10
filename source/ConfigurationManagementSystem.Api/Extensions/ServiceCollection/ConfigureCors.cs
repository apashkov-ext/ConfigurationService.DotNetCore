using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationManagementSystem.Api.Extensions.ServiceCollection
{
    internal static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        var origins = configuration.GetOrigins();
                        builder.WithOrigins(origins).AllowAnyMethod().AllowAnyHeader();
                    });
            });
        }
    }
}
