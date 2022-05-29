using ConfigurationManagementSystem.Framework.Bootstrap.ComponentScanning;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConfigurationManagementSystem.Framework.Tests
{
    public class TestTypeProvider : ITypeProvider
    {
        private readonly Type[] _types;

        public TestTypeProvider(IEnumerable<Type> types)
        {
            _types = types.ToArray();
        }

        public IEnumerable<Type> GetTypes()
        {
            return _types;
        }
    }
}
