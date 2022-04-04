using System;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.Exceptions;
using ConfigurationManagementSystem.Domain.ValueObjects;
using ConfigurationManagementSystem.Tests.Presets;
using Xunit;

namespace ConfigurationManagementSystem.Domain.Tests.EntitiesTests
{
    public class ProjectTests
    {
        [Fact]
        public void Create_CorrectData_NewCorrectEntity()
        {
            ApplicationEntity.Create(new ApplicationName("TestApp"), new ApiKey(new Guid("d1509252-0769-4119-b6cb-7e7dff351384")));
        }

        [Theory]
        [ClassData(typeof(EmptyApplication))]
        public void AddConfig_NotExisted_ReturnsNewConfigEntity(ApplicationEntity p)
        {
            const string envName = "Dev";
            var env = p.AddConfiguration(new ConfigurationName(envName));
            Assert.Equal(envName, env.Name.Value);
        }

        [Theory]
        [ClassData(typeof(ApplicationWithConfiguration))]
        public void AddEnvironment_Existed_Exception(ApplicationEntity p, ConfigurationEntity e)
        {
            Assert.Throws<InconsistentDataStateException>(() => p.AddConfiguration(new ConfigurationName(e.Name.Value)));
        }
    }
}
