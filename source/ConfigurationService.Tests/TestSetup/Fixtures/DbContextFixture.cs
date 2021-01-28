using System;
using System.Collections.Generic;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using ConfigurationService.Persistence;

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
                b.SetupDbSet(x => x.Projects, new List<Project>
                {
                    Project.Create(new ProjectName("TestProject"), new ApiKey(Guid.NewGuid()))
                });
                b.SetupDbSet(x => x.Environments);
                b.SetupDbSet(x => x.OptionGroups);
                b.SetupDbSet(x => x.Options);
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