using ConfigurationManagementSystem.Application;
using ConfigurationManagementSystem.Application.Mappers;
using ConfigurationManagementSystem.Framework;
using ConfigurationManagementSystem.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationManagementSystem.ServicesConfiguring
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEnvironments, Environments>();
            services.AddTransient<IOptionGroups, OptionGroups>();
            services.AddTransient<IOptions, Options>();
            services.AddDbContext<ConfigurationManagementSystemContext>(options =>
            {
                options.ConfigureDatabaseConnection(configuration);
            });

            services.BootstrapFramework(configuration);

            services.AddAutoMapper(typeof(ApplicationMapperProfile));

            return services;
        } 
    }
}
