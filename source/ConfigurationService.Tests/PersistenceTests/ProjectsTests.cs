using System;
using System.Collections.Generic;
using System.Linq;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using ConfigurationService.Persistence;
using ConfigurationService.Tests.TestSetup;
using ConfigurationService.Tests.TestSetup.Fixtures;
using Xunit;

namespace ConfigurationService.Tests.PersistenceTests
{
    [Collection("DbContextCollection")]
    public class ProjectsTests
    {
        private readonly DbContextFixture _contextFixture;

        public ProjectsTests(DbContextFixture contextFixture)
        {
            _contextFixture = contextFixture;
        }

        [Fact]
        public void Test()
        {


            Assert.True(true);
        }
    }
}
