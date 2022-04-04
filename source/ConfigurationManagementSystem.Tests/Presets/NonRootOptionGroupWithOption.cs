using System;
using System.Collections;
using System.Collections.Generic;
using ConfigurationManagementSystem.Tests.Stubs;

namespace ConfigurationManagementSystem.Tests.Presets
{
    public class NonRootOptionGroupWithOption : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var p = new TestableApplication(Guid.NewGuid(), "TestProject", new Guid("d1509252-0769-4119-b6cb-7e7dff351384"));
            var env = new TestableEnvironment(Guid.NewGuid(), "Dev", p);
            p.AddConfig(env);

            var group = new TestableOptionGroup(Guid.NewGuid(),
                "Validation",
                env, env.GetRootOptionGroop());
            var option = new TestableOption(Guid.NewGuid(), "Enabled", "Value", group);
            group.AddOption(option);

            yield return new object[] { group, option };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}