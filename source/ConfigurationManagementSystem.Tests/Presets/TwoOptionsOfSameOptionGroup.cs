using System;
using System.Collections;
using System.Collections.Generic;
using ConfigurationManagementSystem.Tests.Stubs;

namespace ConfigurationManagementSystem.Tests.Presets
{
    public class TwoOptionsOfSameOptionGroup : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var p = new TestableProject(Guid.NewGuid(), "TestProject", Guid.NewGuid());
            var env = new TestableEnvironment(Guid.NewGuid(), "Dev", p);
            p.AddEnv(env);

            var group = new TestableOptionGroup(Guid.NewGuid(),
                "Validation",
                "Option group description",
                env, env.GetRootOptionGroop());
            var option = new TestableOption(Guid.NewGuid(), "OptionName", "Option description", "Value", group);
            group.AddOption(option);
            var option2 = new TestableOption(Guid.NewGuid(), "SecondOptionName", "Option description", "Value", group);
            group.AddOption(option2);

            yield return new object[] { option, option2 };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}