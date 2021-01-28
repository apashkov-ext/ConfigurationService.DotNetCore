using System;
using ConfigurationService.Domain;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using ConfigurationService.Tests.TestSetup;
using Xunit;

namespace ConfigurationService.Tests.DomainTests.EntitiesTests
{
    public class ProjectTests
    {
        private const string CorrectProjectName = TestLiterals.Project.Name.Correct;
        private const string EnvName = TestLiterals.Environment.Name.Correct;
        private readonly Guid ApiKey = TestLiterals.Project.ApiKeys.Correct;

        [Fact]
        public void Create_NewCorrectEntity()
        {
            var project = Project.Create(new ProjectName(CorrectProjectName), new ApiKey(ApiKey));
        }

        [Fact]
        public void AddEnvironment_NotExisted_ReturnsNewEnvEntity()
        {
            var project = Project.Create(new ProjectName(CorrectProjectName), new ApiKey(ApiKey));
            var env = project.AddEnvironment(new EnvironmentName(EnvName));
            Assert.Equal(env.Name.Value, EnvName);
        }

        [Fact]
        public void AddEnvironment_Existed_Exception()
        {
            var project = Project.Create(new ProjectName(CorrectProjectName), new ApiKey(ApiKey));
            project.AddEnvironment(new EnvironmentName(EnvName));
            Assert.Throws<ApplicationException>(() => project.AddEnvironment(new EnvironmentName(EnvName)));
        }
    }
}
