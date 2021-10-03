using ConfigurationManagementSystem.Application;
using ConfigurationManagementSystem.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationManagementSystem.ServicesConfiguring
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IProjects, Projects>();
            services.AddTransient<IConfigurations, Configurations>();
            services.AddTransient<IEnvironments, Environments>();
            services.AddTransient<IOptionGroups, OptionGroups>();
            services.AddTransient<IOptions, Options>();
            services.AddDbContext<ConfigurationServiceContext>(options =>
            {
                options.ConfigureSqlite();
            });

            return services;
        }
    }
}
