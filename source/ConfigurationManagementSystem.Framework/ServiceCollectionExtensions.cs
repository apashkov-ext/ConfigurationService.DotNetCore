using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Framework.Bootstrap.ComponentScanning;
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
                ExecuteComponentRegistering(services, configuration);
            }
            catch (Exception ex)
            {
                throw new FrameworkInitializingException("Error occured while initializing framework", ex);
            }
        }

        private static void ExecuteComponentRegistering(IServiceCollection services, IConfiguration configuration)
        {         
            var asmLoader = new AssemblyLoader(configuration);
            var assemblies = asmLoader.LoadAssemblies();
            var typeProv = FrameworkComponentTypeProvider.Create(assemblies);
            var implProvider = new TypeImplementationProvider(typeProv);

            RegisterFrameworkComponents(() => typeProv.GetComponentTypesByAttribute<ComponentAttribute>(), 
                type => services.AddTransient(type));

            RegisterFrameworkComponents(() => typeProv.GetComponentTypesByAttribute<QueryAttribute>().Select(implProvider.GetImplementation), 
                type => services.AddTransient(type));

            RegisterFrameworkComponents(() => typeProv.GetComponentTypesByAttribute<CommandAttribute>().Select(implProvider.GetImplementation), 
                type => services.AddTransient(type));
        }

        private static void RegisterFrameworkComponents(Func<IEnumerable<Type>> getTypes, Action<Type> registrar)
        {
            foreach (var type in getTypes())
            {
                registrar(type);
            }
        }
    }
}
