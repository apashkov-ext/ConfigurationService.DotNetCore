using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Tests.Presets;
using Xunit;
using ConfigurationEntity = ConfigurationManagementSystem.Domain.Entities.ConfigurationEntity;

namespace ConfigurationManagementSystem.Domain.Tests.EntitiesTests
{
    public class EnvironmentTests
    {
        [Theory]
        [ClassData(typeof(EmptyApplication))]
        public void Create_CorrectData_RootGroupNotNull(Entities.ApplicationEntity p)
        {
            var env = ConfigurationEntity.Create(new ConfigurationName("TestProject"), p);
            Assert.NotNull(env.GetRootOptionGroop());
        }

        [Theory]
        [ClassData(typeof(EmptyApplication))]
        public void Create_CorrectData_RootGroupNameIsEmptyString(Entities.ApplicationEntity p)
        {
            var env = ConfigurationEntity.Create(new ConfigurationName("TestProject"), p);
            Assert.Equal("", env.GetRootOptionGroop().Name.Value);
        }

        [Theory]
        [ClassData(typeof(ValidEnvironment))]
        public void UpdateName_CorrectName_Success(ConfigurationEntity e)
        {
            const string name = "NewEnvName";
            e.UpdateName(new ConfigurationName(name));
            Assert.Equal(name, e.Name.Value);
        }
    }
}
