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
        /// <summary>
        /// Runs framework initialization: scanning components, binding configurations, confuguring services. 
        /// </summary>
        /// <param name="services">Instance of <see cref="IServiceCollection"/></param>
        /// <param name="configuration">Instance of <see cref="IConfiguration"/></param>
        /// <exception cref="FrameworkInitializingException"></exception>
        public static void BootstrapFramework(this IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                var typeProv = GetComponentTypeProvider(configuration);
                var implProvider = new TypeImplementationProvider(typeProv);

                RegisterComponents(typeProv, implProvider, services);
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

        private static void RegisterComponents(IFrameworkComponentTypeProvider typeProvider, 
            TypeImplementationProvider implementationProvider, IServiceCollection services)
        {         
            foreach (var baseType in typeProvider.GetComponentTypesByAttribute<ComponentAttribute>())
            {
                var impl = implementationProvider.GetImplementation(baseType);
                services.AddTransient(baseType, impl);
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
