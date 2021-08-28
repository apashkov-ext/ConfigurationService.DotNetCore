using ConfigurationService.Application;
using ConfigurationService.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationService.ServiceCollectionConfiguring
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IProjects, Projects>();
            services.AddTransient<IConfigurations, Configurations>();
            services.AddTransient<IEnvironments, Persistence.Environments>();
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
