using ConfigurationService.Domain;
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
            services.AddDbContext<ConfigurationServiceContext>(options => options.UseInMemoryDatabase("ConfigurationStorage"));

            return services;
        }
    }
}
