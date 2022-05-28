using ConfigurationManagementSystem.Framework.Attributes;
using System.Reflection;

namespace ConfigurationManagementSystem.Framework.Bootstrap.ComponentScanning
{
    internal class FrameworkComponentTypeProvider : IFrameworkComponentTypeProvider, ITypeProvider
    {
        private readonly Type[] _types;

        private FrameworkComponentTypeProvider(IEnumerable<Type> types)
        {
            _types = types.ToArray();
        }

        public static FrameworkComponentTypeProvider Create(IEnumerable<Assembly> assemblies)
        {
            if (assemblies is null)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }

            var types = assemblies.SelectMany(x => x.GetTypes()).Where(x => x.IsPublic);
            return new FrameworkComponentTypeProvider(types);
        }

        public IEnumerable<Type> GetComponentTypesByAttribute<T>() where T : FrameworkComponentAttribute
        {
            return _types.Where(x => x.CustomAttributes.Select(s => s.AttributeType).Any(y => y == typeof(T)));
        }

        public IEnumerable<Type> GetTypes()
        {
            return _types;
        }
    }
}
