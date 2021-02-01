using System;
using System.Collections;
using System.Collections.Generic;
using ConfigurationService.Tests.Stubs;

namespace ConfigurationService.Tests.Presets
{
    public class NonRootOptionGroupWithNested : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var p = new TestableProject(Guid.NewGuid(), TestLiterals.Project.Name.Correct, TestLiterals.Project.ApiKeys.Correct);
            var env = new TestableEnvironment(Guid.NewGuid(), TestLiterals.Environment.Name.Correct, p);
            p.AddEnv(env);

            var group = new TestableOptionGroup(Guid.NewGuid(),
                TestLiterals.OptionGroup.Name.Correct,
                TestLiterals.OptionGroup.Description.Correct,
                env, env.GetRootOptionGroop());

            var nested = new TestableOptionGroup(Guid.NewGuid(), 
                TestLiterals.OptionGroup.Name.Correct + "Nested", 
                TestLiterals.OptionGroup.Description.Correct,
                env, group);
            group.AddNested(nested);

            yield return new object[] { group, nested };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}