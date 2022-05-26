using ConfigurationManagementSystem.Framework.Exceptions;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace ConfigurationManagementSystem.Framework.Bootstrap
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
            try
            {
                return GetAsmNames(_configuration).Select(Assembly.Load);
            }
            catch (Exception ex)
            {
                throw new TypesLoadingException("Error occured while loading types", ex);
            }
        }

        private string[] GetAsmNames(IConfiguration configuration)
        {
            var value = configuration.GetValue<string[]>("ApplicationFramework.Assemblies");
            return value ?? throw new Exception("Configuration property not defined: ApplicationFramework.Assemblies");
        }
    }
}
