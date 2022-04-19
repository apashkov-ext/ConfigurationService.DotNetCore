using System;
using System.Collections;
using System.Collections.Generic;
using ConfigurationManagementSystem.Tests.Stubs;

namespace ConfigurationManagementSystem.Tests.Presets
{
    public class EmptyApplication : IEnumerable<object[]>
    {
        private static readonly Random Rand = new Random();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]{ new TestableApplication(Guid.NewGuid(), $"Application{ApplicationNamePostfix()}", Guid.NewGuid()) };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static string ApplicationNamePostfix()
        {
            return $"{(char) Rand.Next(65, 91)}{(char) Rand.Next(97, 123)}{(char)Rand.Next(97, 123)}";
        }
    }
}
