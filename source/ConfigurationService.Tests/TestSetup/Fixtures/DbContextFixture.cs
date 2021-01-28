using System.Collections.Generic;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Persistence;
using ConfigurationService.Tests.TestSetup.Stubs;

namespace ConfigurationService.Tests.TestSetup.Fixtures
{
    internal class DbContextFixture
    {
        public ConfigurationServiceContext Context => Create().Object;
        public ConfigurationServiceContext EmptyContext => CreateEmpty().Object;

        private static MockedContext<ConfigurationServiceContext> Create()
        {
            return new MockedContext<ConfigurationServiceContext>(b =>
            {
                var p = new TestableProject(TestLiterals.Guids.Id1, TestLiterals.Project.Name.Correct, TestLiterals.Project.ApiKeys.Correct);
                var env = new TestableEnvironment(TestLiterals.Guids.Id2, TestLiterals.Environment.Name.Correct, p);
                var group = new TestableOptionGroup(TestLiterals.Guids.Id3, TestLiterals.OptionGroup.Name.Correct, TestLiterals.OptionGroup.Description.Correct, env);
                var o = new TestableOption(TestLiterals.Guids.Id4, TestLiterals.Option.Name.Correct, 
                    TestLiterals.Option.Description.Correct, TestLiterals.Option.Value.StringValue, group);

                b.SetupDbSet(x => x.Projects, new List<Project> { p });
                b.SetupDbSet(x => x.Environments, new List<Environment>{ env });
                b.SetupDbSet(x => x.OptionGroups, new List<OptionGroup>{ group });
                b.SetupDbSet(x => x.Options, new List<Option>{ o });
            });
        }

        private static MockedContext<ConfigurationServiceContext> CreateEmpty()
        {
            return new MockedContext<ConfigurationServiceContext>(b =>
            {
                b.SetupDbSet(x => x.Projects);
                b.SetupDbSet(x => x.Environments);
                b.SetupDbSet(x => x.OptionGroups);
                b.SetupDbSet(x => x.Options);
            });
        }
    }
}