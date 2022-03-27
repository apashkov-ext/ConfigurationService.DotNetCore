using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Tests.Presets;
using Xunit;
using Configuration = ConfigurationManagementSystem.Domain.Entities.Configuration;

namespace ConfigurationManagementSystem.Domain.Tests.EntitiesTests
{
    public class EnvironmentTests
    {
        [Theory]
        [ClassData(typeof(EmptyProject))]
        public void Create_CorrectData_RootGroupNotNull(Entities.Application p)
        {
            var env = Configuration.Create(new EnvironmentName("TestProject"), p);
            Assert.NotNull(env.GetRootOptionGroop());
        }

        [Theory]
        [ClassData(typeof(EmptyProject))]
        public void Create_CorrectData_RootGroupNameIsEmptyString(Entities.Application p)
        {
            var env = Configuration.Create(new EnvironmentName("TestProject"), p);
            Assert.Equal("", env.GetRootOptionGroop().Name.Value);
        }

        [Theory]
        [ClassData(typeof(ValidEnvironment))]
        public void UpdateName_CorrectName_Success(Configuration e)
        {
            const string name = "NewEnvName";
            e.UpdateName(new EnvironmentName(name));
            Assert.Equal(name, e.Name.Value);
        }
    }
}
