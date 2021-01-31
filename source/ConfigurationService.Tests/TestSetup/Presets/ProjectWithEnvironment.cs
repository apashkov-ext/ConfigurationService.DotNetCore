using System;
using System.Collections;
using System.Collections.Generic;
using ConfigurationService.Tests.TestSetup.Stubs;

namespace ConfigurationService.Tests.TestSetup.Presets
{
    public class ProjectWithEnvironment : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var p =  new TestableProject(Guid.NewGuid(), TestLiterals.Project.Name.Correct, TestLiterals.Project.ApiKeys.Correct);
            var env = new TestableEnvironment(Guid.NewGuid(), TestLiterals.Environment.Name.Correct, p);
            p.AddEnv(env);
            yield return new object[]{ p, env };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
