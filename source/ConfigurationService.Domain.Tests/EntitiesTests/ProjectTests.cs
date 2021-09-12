using System;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.Exceptions;
using ConfigurationService.Domain.ValueObjects;
using ConfigurationService.Tests.Presets;
using Xunit;
using Environment = ConfigurationService.Domain.Entities.Environment;

namespace ConfigurationService.Domain.Tests.EntitiesTests
{
    public class ProjectTests
    {
        [Fact]
        public void Create_CorrectData_NewCorrectEntity()
        {
            Project.Create(new ProjectName("TestProject"), new ApiKey(new Guid("d1509252-0769-4119-b6cb-7e7dff351384")));
        }

        [Theory]
        [ClassData(typeof(EmptyProject))]
        public void AddEnvironment_NotExisted_ReturnsNewEnvEntity(Project p)
        {
            const string envName = "Dev";
            var env = p.AddEnvironment(new EnvironmentName(envName));
            Assert.Equal(envName, env.Name.Value);
        }

        [Theory]
        [ClassData(typeof(ProjectWithEnvironment))]
        public void AddEnvironment_Existed_Exception(Project p, Environment e)
        {
            Assert.Throws<InconsistentDataStateException>(() => p.AddEnvironment(new EnvironmentName(e.Name.Value)));
        }
    }
}
