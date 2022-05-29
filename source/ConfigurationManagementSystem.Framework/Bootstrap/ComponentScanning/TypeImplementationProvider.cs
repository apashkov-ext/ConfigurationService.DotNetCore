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

            TypeInfo[] implementations = GetAllImplementations(baseType).ToArray();
            Type derivedClass = GetDirectlyDerivedClassTypes(baseType, implementations);

            return GetTopOfHierarchy(implementations, derivedClass) ?? throw GetImplNotFoundEx(baseType);
        }

        private IEnumerable<TypeInfo> GetAllImplementations(Type baseType)
        {
            return _typeProvider.GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract)
                .Where(x => x.IsAssignableTo(baseType))
                .Select(x => x.GetTypeInfo())
                .ToArray();
        }

        private static Type GetDirectlyDerivedClassTypes(Type baseType, TypeInfo[] implementations)
        {
            return baseType.IsInterface
                ? GetSingleDerivedType(implementations, typeof(object))
                : baseType;
        }

        private Type GetTopOfHierarchy(IEnumerable<TypeInfo> types, Type baseType)
        {
            if (!types.Any()) throw GetImplNotFoundEx(baseType);
            if (types.Count() == 1) return types.First();

            var typeArr = types.Where(x => x != baseType).ToArray();
            var derived = GetSingleDerivedType(typeArr, baseType);

            return GetTopOfHierarchy(typeArr, derived);
        }

        private static Type GetSingleDerivedType(IEnumerable<Type> source, Type baseType)
        {
            var result = source.Where(x => x.BaseType == baseType).ToArray();

            if (!result.Any()) throw GetImplNotFoundEx(baseType);
            if (result.Length > 1) throw GetAmbigTypeImplEx(baseType);

            return result[0];
        }

        private static ImplementationNotFoundException GetImplNotFoundEx(Type baseType)
        {
            return new ImplementationNotFoundException($"No implementation was found for the [{baseType.Name}] type");
        }

        private static AmbiguousTypeImplementationException GetAmbigTypeImplEx(Type baseType)
        {
            return new AmbiguousTypeImplementationException($"Too many implementations was found for the [{baseType.Name}] type");
        }
    }
}
