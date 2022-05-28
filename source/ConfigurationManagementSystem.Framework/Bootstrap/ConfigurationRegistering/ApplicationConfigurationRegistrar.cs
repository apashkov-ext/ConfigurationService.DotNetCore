using ConfigurationManagementSystem.Framework.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ConfigurationManagementSystem.Framework.Bootstrap.ConfigurationRegistering
{
    /// <summary>
    /// Configuration section registrar.
    /// Performs searching and binding specified configuration section.
    /// </summary>
    internal class ApplicationConfigurationRegistrar
    {
        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;
        private readonly MethodInfo _configureMethod;

        private ApplicationConfigurationRegistrar(IServiceCollection services, IConfiguration configuration, MethodInfo configureMethod)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _configureMethod = configureMethod ?? throw new ArgumentNullException(nameof(configureMethod));
        }

        /// <summary>
        /// Creates instance of <see cref="ApplicationConfigurationRegistrar"/>.
        /// Implementation of this method strongly depends on some framework features (see EXCEPTIONS section below).
        /// <para>Throws:</para>
        /// <list type="bullet">
        /// <item>
        /// <term><see cref="InvalidOperationException"/></term>
        /// <description>
        /// Throws if failed to find method <see cref="OptionsConfigurationServiceCollectionExtensions.Configure{TOptions}(IServiceCollection, IConfiguration)"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="services">Instance of <see cref="IServiceCollection"/>.</param>
        /// <param name="configuration">Instance of <see cref="IConfiguration"/> which represents the root of configuration sources.</param>
        /// <returns>Fully initialized instance of <see cref="ApplicationConfigurationRegistrar"/>.</returns>
        /// <exception cref="InvalidOperationException" />
        public static ApplicationConfigurationRegistrar Create(IServiceCollection services, IConfiguration configuration)
        {        
             var configureMethod = typeof(OptionsConfigurationServiceCollectionExtensions)
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(x => x.Name == nameof(OptionsConfigurationServiceCollectionExtensions.Configure) && x.IsGenericMethodDefinition)
                .Where(x => x.GetGenericArguments().Length == 1)
                .Where(x => x.GetParameters().Length == 2)
                .Single();

            return new ApplicationConfigurationRegistrar(services, configuration, configureMethod);
        }

        /// <summary>
        /// Registers configuration sections and binds them to the corresponding type.
        /// </summary>
        /// <param name="sectionInfos">Name of the configuration section to find it in the configuration sources.</param>
        /// <exception cref="ConfigurationBindingException">Throws if failed to find section by <paramref name="sectionName"/> value.
        /// Also throws if failed to bind configuration section to the <paramref name="typeToBind"/> type.</exception>
        public void RegisterConfigurationSection(string sectionName, Type typeToBind)
        {          
            var section = _configuration.GetSection(sectionName);
            if (section == null)
            {
                throw new ConfigurationBindingException($"Configuration section {sectionName} not found in the configuration sources");
            }

            try
            {
                Configure(typeToBind, section);
            }
            catch (Exception ex)
            {
                throw new ConfigurationBindingException($"Failed to bind configuration section {sectionName} to the {typeToBind.Name} type", ex);
            }         
        }

        private void Configure(Type typeToRegister, IConfigurationSection section)
        {
            var genericConfigureMethod = _configureMethod.MakeGenericMethod(typeToRegister);
            genericConfigureMethod.Invoke(null, new object[] { _services, section });
        }
    }
}
