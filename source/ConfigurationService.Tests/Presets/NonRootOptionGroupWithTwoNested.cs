using System;
using System.Collections;
using System.Collections.Generic;
using ConfigurationService.Tests.Stubs;

namespace ConfigurationService.Tests.Presets
{
    public class NonRootOptionGroupWithTwoNested : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var p = new TestableProject(Guid.NewGuid(), "TestProject", new Guid("d1509252-0769-4119-b6cb-7e7dff351384"));
            var env = new TestableEnvironment(Guid.NewGuid(), "Dev", p);
            p.AddEnv(env);

            var group = new TestableOptionGroup(Guid.NewGuid(),
                "Validation",
                "Option group description",
                env, env.GetRootOptionGroop());

            var nested = new TestableOptionGroup(Guid.NewGuid(), 
                "Validation" + "Nested", 
                "Option group description",
                env, group);
            group.AddNested(nested);

            var nested2 = new TestableOptionGroup(Guid.NewGuid(),
                "Validation" + "SecondNested",
                "Option group description",
                env, group);
            group.AddNested(nested2);

            yield return new object[] { group, nested, nested2 };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}