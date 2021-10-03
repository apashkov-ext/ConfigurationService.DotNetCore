using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Tests.Presets;
using Xunit;
using Environment = ConfigurationManagementSystem.Domain.Entities.Environment;

namespace ConfigurationManagementSystem.Domain.Tests.EntitiesTests
{
    public class EnvironmentTests
    {
        [Theory]
        [ClassData(typeof(EmptyProject))]
        public void Create_CorrectData_RootGroupNotNull(Project p)
        {
            var env = Environment.Create(new EnvironmentName("TestProject"), p);
            Assert.NotNull(env.GetRootOptionGroop());
        }

        [Theory]
        [ClassData(typeof(EmptyProject))]
        public void Create_CorrectData_RootGroupNameIsEmptyString(Project p)
        {
            var env = Environment.Create(new EnvironmentName("TestProject"), p);
            Assert.Equal("", env.GetRootOptionGroop().Name.Value);
        }

        [Theory]
        [ClassData(typeof(ValidEnvironment))]
        public void UpdateName_CorrectName_Success(Environment e)
        {
            const string name = "NewEnvName";
            e.UpdateName(new EnvironmentName(name));
            Assert.Equal(name, e.Name.Value);
        }
    }
}
