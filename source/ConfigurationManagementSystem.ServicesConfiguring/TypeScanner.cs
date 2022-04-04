using ConfigurationManagementSystem.ServicesConfiguring.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConfigurationManagementSystem.ServicesConfiguring
{
    internal class TypeScanner
    {
        private readonly Assembly[] _assemblies;
        private Type[] _types;
        private Type[] Types
        {
            get
            {
                return _types ??= _assemblies.SelectMany(x => x.GetTypes()).ToArray();
            }
        }

        public TypeScanner(params Assembly[] assemblies)
        {
            _assemblies = assemblies;
        }

        public IEnumerable<Type> GetTypesByAttribute(params Type[] attributes)
        {
            return Types.Where(x => x.CustomAttributes.Select(s => s.AttributeType).Any(y => attributes.Contains(y)));
        }

        public Type GetImplementationFor(Type baseType)
        {
            try
            {
                return Types.Where(x => x.BaseType == baseType).SingleOrDefault()
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
