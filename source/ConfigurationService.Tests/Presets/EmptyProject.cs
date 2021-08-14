using System;
using System.Collections;
using System.Collections.Generic;
using ConfigurationService.Tests.Stubs;

namespace ConfigurationService.Tests.Presets
{
    public class EmptyProject : IEnumerable<object[]>
    {
        private static readonly Random Rand = new Random();
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]{ new TestableProject(Guid.NewGuid(), $"Project{ProjectNamePostfix()}", Guid.NewGuid()) };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static string ProjectNamePostfix()
        {
            return $"{(char) Rand.Next(65, 91)}{(char) Rand.Next(97, 123)}{(char)Rand.Next(97, 123)}";
        }
    }
}
