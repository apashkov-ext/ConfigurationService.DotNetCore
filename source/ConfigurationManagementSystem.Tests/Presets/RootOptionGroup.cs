using System;
using System.Collections;
using System.Collections.Generic;
using ConfigurationManagementSystem.Tests.Stubs;

namespace ConfigurationManagementSystem.Tests.Presets
{
    public class RootOptionGroup : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var p = new TestableProject(Guid.NewGuid(), "TestProject", new Guid("d1509252-0769-4119-b6cb-7e7dff351384"));
            var env = new TestableEnvironment(Guid.NewGuid(), "Dev", p);
            p.AddEnv(env);

            yield return new object[] { env.GetRootOptionGroop() };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}