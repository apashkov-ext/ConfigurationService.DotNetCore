using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Framework.Bootstrap.ComponentScanning;
using ConfigurationManagementSystem.Framework.Bootstrap.ConfigurationRegistering;
using ConfigurationManagementSystem.Framework.Bootstrap.ConfigurationSectionDefinitionReading;
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
                var typeProv = GetComponentTypeProvider(configuration);
                RegisterComponents(typeProv, services);
                RegisterConfiguration(typeProv, services, configuration);
            }
            catch (Exception ex)
            {
                throw new FrameworkInitializingException("Error occured while initializing framework", ex);
            }
        }

        private static FrameworkComponentTypeProvider GetComponentTypeProvider(IConfiguration configuration)
        {
            var asmLoader = new AssemblyLoader(configuration);
            var assemblies = asmLoader.LoadAssemblies();
            return FrameworkComponentTypeProvider.Create(assemblies);
        }

        private static void RegisterComponents(FrameworkComponentTypeProvider typeProvider, IServiceCollection services)
        {         
            var implProvider = new TypeImplementationProvider(typeProvider);
            var typesToRegister = typeProvider.GetComponentTypesByAttribute<ComponentAttribute>().Select(implProvider.GetImplementation);
            foreach (var type in typesToRegister)
            {
                services.AddTransient(type);
            }
        }

        private static void RegisterConfiguration(IFrameworkComponentTypeProvider typeProvider, IServiceCollection services, IConfiguration configuration)
        {
            var configRegistrar = ApplicationConfigurationRegistrar.Create(services, configuration);
            var configDefs = new ConfigurationDefinitionProvider(typeProvider).GetDefinitions();
            foreach (var configDef in configDefs)
            {
                configRegistrar.RegisterConfigurationSection(configDef.SectionName, configDef.TypeToBind);
            }
        }
    }
}
