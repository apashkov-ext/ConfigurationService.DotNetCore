using System;
using System.Collections.Generic;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using ConfigurationService.Persistence;

namespace ConfigurationService.Tests.TestSetup.Fixtures
{
    public class DbContextFixture
    {
        public ConfigurationServiceContext Db { get; }

        public DbContextFixture()
        {
            Db = Create().Object;
        }

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
    }
}