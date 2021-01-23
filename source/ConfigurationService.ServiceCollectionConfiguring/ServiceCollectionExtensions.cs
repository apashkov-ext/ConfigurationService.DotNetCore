using ConfigurationService.Application;
using ConfigurationService.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationService.ServiceCollectionConfiguring
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
            services.AddDbContext<ConfigurationServiceContext>(options => options.UseInMemoryDatabase("ConfigurationStorage"));

            return services;
        }
    }
}
