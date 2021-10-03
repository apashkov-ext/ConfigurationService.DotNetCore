using System;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.Exceptions;
using ConfigurationManagementSystem.Domain.ValueObjects;
using ConfigurationManagementSystem.Tests.Presets;
using Xunit;
using Environment = ConfigurationManagementSystem.Domain.Entities.Environment;

namespace ConfigurationManagementSystem.Domain.Tests.EntitiesTests
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
