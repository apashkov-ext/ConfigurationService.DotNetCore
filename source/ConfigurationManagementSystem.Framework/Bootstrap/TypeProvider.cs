using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Framework.Exceptions;
using System.Reflection;

namespace ConfigurationManagementSystem.Framework.Bootstrap
{
    internal class TypeProvider
    {
        private readonly Type[] _types;

        private TypeProvider(IEnumerable<Type> types)
        {
            _types = types.ToArray();
        }

        public static TypeProvider Create(IEnumerable<Assembly> assemblies)
        {
            if (assemblies is null)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }

            var types = assemblies.SelectMany(x => x.GetTypes()).Where(x => x.IsPublic);
            return new TypeProvider(types);
        }

        public IEnumerable<Type> FindByAttribute<T>() where T : FrameworkAttribute
        {
            return _types.Where(x => x.CustomAttributes.Select(s => s.AttributeType).Any(y => y == typeof(T)));
        }

        public Type GetImplementationFor(Type baseType)
        {
            try
            {
                return _types.Where(x => x.BaseType == baseType).SingleOrDefault()
                ?? throw new ImplementationNotFoundException($"No implementation was found for the [{baseType.Name}] type");
            }
            catch (InvalidOperationException)
            {
                throw new AmbiguousTypeImplementationException($"Too many implementations was found for the [{baseType.Name}] type");
            }
            catch
            {
                throw;
            }
        }
    }
}
