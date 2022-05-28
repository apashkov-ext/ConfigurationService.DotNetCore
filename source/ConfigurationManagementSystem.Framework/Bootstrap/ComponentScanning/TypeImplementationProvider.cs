using ConfigurationManagementSystem.Framework.Exceptions;
using System.Reflection;

namespace ConfigurationManagementSystem.Framework.Bootstrap.ComponentScanning
{
    internal class TypeImplementationProvider
    {
        private readonly ITypeProvider _typeProvider;

        public TypeImplementationProvider(ITypeProvider typeProvider)
        {
            _typeProvider = typeProvider ?? throw new ArgumentNullException(nameof(typeProvider));
        }

        /// <summary>
        /// Returns implementation for the specified base type.
        /// </summary>
        /// <param name="baseType">Base (parent) type. Must be a class or an interface</param>
        /// <returns>Type that implemets specified base type.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ImplementationNotFoundException"></exception>
        /// <exception cref="AmbiguousTypeImplementationException"></exception>
        public Type GetImplementation(Type baseType)
        {
            if (baseType is null) throw new ArgumentNullException(nameof(baseType));
            if (!baseType.IsClass && !baseType.IsInterface) throw new InvalidOperationException("Base type must be a class or an interface.");

            var implementations = _typeProvider.GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract)
                .Where(x => x.IsAssignableTo(baseType))
                .Select(x => x.GetTypeInfo())
                .ToArray();

            return GetTopOfHierarchy(implementations, baseType) ?? throw GetImplNotFoundEx(baseType);
        }

        private Type GetTopOfHierarchy(IEnumerable<TypeInfo> types, Type baseType)
        {
            if (!types.Any()) throw GetImplNotFoundEx(baseType);
            if (types.Count() == 1) return types.First();

            var typeArr = types.Where(x => x != baseType).ToArray();

            try
            {
                var derived = typeArr.SingleOrDefault(GetPredicate(baseType));
                if (derived == null) throw GetImplNotFoundEx(baseType);

                return GetTopOfHierarchy(typeArr, derived);
            }
            catch (InvalidOperationException ex)
            {
                throw new AmbiguousTypeImplementationException($"Too many implementations was found for the [{baseType.Name}] type", ex);
            }
        }

        private static Func<TypeInfo, bool> GetPredicate(Type baseType)
        {
            return baseType.IsClass
                ? t => t.BaseType == baseType
                : t => t.ImplementedInterfaces.Contains(baseType);
        }

        private static ImplementationNotFoundException GetImplNotFoundEx(Type baseType)
        {
            return new ImplementationNotFoundException($"No implementation was found for the [{baseType.Name}] type");
        }
    }
}
