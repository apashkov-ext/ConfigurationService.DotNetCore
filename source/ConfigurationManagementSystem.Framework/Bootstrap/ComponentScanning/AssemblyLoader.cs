using ConfigurationManagementSystem.Framework.AppConfiguration;
using ConfigurationManagementSystem.Framework.Exceptions;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace ConfigurationManagementSystem.Framework.Bootstrap.ComponentScanning
{
    internal class AssemblyLoader
    {
        private readonly IConfiguration _configuration;

        public AssemblyLoader(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IEnumerable<Assembly> LoadAssemblies()
        {
            foreach (var asm in GetAsmNames(_configuration))
            {
                Assembly loaded;

                try
                {
                    loaded = Assembly.Load(asm);
                }
                catch (Exception ex)
                {
                    throw new AssemblyLoadingException($"Error occured while loading assembly {asm}", ex);
                }

                yield return loaded;
            }
        }

        private string[] GetAsmNames(IConfiguration configuration)
        {
            var section = configuration.GetSection(nameof(ApplicationFrameworkSection)).Get<ApplicationFrameworkSection>(o =>
            {
                o.BindNonPublicProperties = true;
            });
            return section?.Assemblies ?? throw new Exception("Configuration property not defined: ApplicationFramework.Assemblies");
        }
    }
}
