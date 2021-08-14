using System;
using System.Collections;
using System.Collections.Generic;
using ConfigurationService.Tests.Stubs;

namespace ConfigurationService.Tests.Presets
{
    public class ValidEnvironment : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var p = new TestableProject(Guid.NewGuid(), "TestProject", TestLiterals.Project.ApiKeys.Correct);
            var env = new TestableEnvironment(Guid.NewGuid(), "Dev", p);
            p.AddEnv(env);
            yield return new object[] { env };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}