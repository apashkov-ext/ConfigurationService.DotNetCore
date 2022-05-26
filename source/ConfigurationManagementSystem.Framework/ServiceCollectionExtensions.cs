using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Framework.Bootstrap;
using ConfigurationManagementSystem.Framework.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationManagementSystem.Framework
{
    public static class ServiceCollectionExtensions
    {
        public static void BootstrapFramework(this IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                ExecuteTypeRegistering(services, configuration);
            }
            catch (Exception ex)
            {
                throw new FrameworkInitializingException("Erro occured while initializing framework", ex);
            }
        }

        private static void ExecuteTypeRegistering(IServiceCollection services, IConfiguration configuration)
        {         
            var asmLoader = new AssemblyLoader(configuration);
            var assemblies = asmLoader.LoadAssemblies();

            var prov = TypeProvider.Create(assemblies);

            RegisterFrameworkTypes(() => prov.FindByAttribute<UserStoryAttribute>(), type => services.AddTransient(type));
            RegisterFrameworkTypes(() => prov.FindByAttribute<QueryAttribute>().Select(prov.GetImplementationFor), type => services.AddTransient(type));
            RegisterFrameworkTypes(() => prov.FindByAttribute<CommandAttribute>().Select(prov.GetImplementationFor), type => services.AddTransient(type));
        }

        private static void RegisterFrameworkTypes(Func<IEnumerable<Type>> getTypes, Action<Type> registrar)
        {
            foreach (var type in getTypes())
            {
                registrar(type);
            }
        }
    }

    internal static class ConfigurationLoader
    {
        public static void AddConfig(IServiceCollection services , IConfiguration configuration)
        {
            var scanner = new TypeImplementationFinder(Assembly.GetAssembly(typeof(AppConfigurationAttribute)));
            foreach (var type in scanner.GetTypesByAttribute(typeof(AppConfigurationAttribute)))
            {
                var section = configuration.GetSection(type.Name);
                if (section == null)
                {
                    throw new ConfigurationBindingException($"Configuration section {type.Name} not found in the configuration source");
                }

                var config = section.Get(type);
                services.AddSingleton(type, config);
            }
        }
    }
}
