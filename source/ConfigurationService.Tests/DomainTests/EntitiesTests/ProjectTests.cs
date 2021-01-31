using System;
using ConfigurationService.Domain;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using ConfigurationService.Tests.TestSetup;
using ConfigurationService.Tests.TestSetup.Presets;
using Xunit;
using Environment = ConfigurationService.Domain.Entities.Environment;

namespace ConfigurationService.Tests.DomainTests.EntitiesTests
{
    public class ProjectTests
    {
        [Fact]
        public void Create_CorrectData_NewCorrectEntity()
        {
            Project.Create(new ProjectName(TestLiterals.Project.Name.Correct), new ApiKey(TestLiterals.Project.ApiKeys.Correct));
        }

        [Theory]
        [ClassData(typeof(EmptyProject))]
        public void AddEnvironment_NotExisted_ReturnsNewEnvEntity(Project p)
        {
            var env = p.AddEnvironment(new EnvironmentName(TestLiterals.Environment.Name.Correct));
            Assert.Equal(env.Name.Value, TestLiterals.Environment.Name.Correct);
        }

        [Theory]
        [ClassData(typeof(ProjectWithEnvironment))]
        public void AddEnvironment_Existed_Exception(Project p, Environment e)
        {
            Assert.Throws<ApplicationException>(() => p.AddEnvironment(new EnvironmentName(e.Name.Value)));
        }
    }
}
