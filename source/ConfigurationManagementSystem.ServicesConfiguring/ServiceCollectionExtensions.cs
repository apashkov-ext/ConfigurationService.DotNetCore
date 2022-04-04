using ConfigurationManagementSystem.Application;
using ConfigurationManagementSystem.Application.AppConfiguration;
using ConfigurationManagementSystem.Application.Stories.Framework;
using ConfigurationManagementSystem.Persistence;
using ConfigurationManagementSystem.Persistence.StoryImplementations;
using ConfigurationManagementSystem.ServicesConfiguring.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ConfigurationManagementSystem.ServicesConfiguring
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IConfigurations, Configurations>();
            services.AddTransient<IEnvironments, Environments>();
            services.AddTransient<IOptionGroups, OptionGroups>();
            services.AddTransient<IOptions, Options>();
            services.AddDbContext<ConfigurationManagementSystemContext>(options =>
            {
                options.ConfigureSqlite();
            });

            services.AddConfig(configuration);
            services.AddStories();

            return services;
        }

        private static void AddStories(this IServiceCollection services)
        {
            var scanner = new TypeScanner(Assembly.GetAssembly(typeof(UserStoryAttribute)), Assembly.GetAssembly(typeof(ImplementationMarker)));
            foreach (var type in scanner.GetTypesByAttribute(typeof(UserStoryAttribute)))
            {
                services.AddTransient(type);
            }

            foreach (var baseType in scanner.GetTypesByAttribute(typeof(QueryAttribute), typeof(CommandAttribute)))
            {
                var impl = scanner.GetImplementationFor(baseType);
                services.AddTransient(baseType, impl);
            }
        }

        private static void AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var scanner = new TypeScanner(Assembly.GetAssembly(typeof(AppConfigurationAttribute)));
            foreach (var type in scanner.GetTypesByAttribute(typeof(AppConfigurationAttribute)))
            {
                var section = configuration.GetSection(type.Name);
                if (section == null)
                {
                    throw new ConfigurationBindingException($"Configuration section {type.Name} not found in the configuration source");
                }

                var config = section.Get(type);
                services.AddSingleton(config);
            }
        }
    }
}
