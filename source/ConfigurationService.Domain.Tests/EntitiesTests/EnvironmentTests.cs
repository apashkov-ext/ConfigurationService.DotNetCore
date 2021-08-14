using ConfigurationService.Domain.Entities;
using ConfigurationService.Tests;
using ConfigurationService.Tests.Presets;
using Xunit;
using Environment = ConfigurationService.Domain.Entities.Environment;


namespace ConfigurationService.Domain.Tests.EntitiesTests
{
    public class EnvironmentTests
    {
        [Theory]
        [ClassData(typeof(EmptyProject))]
        public void Create_CorrectData_RootGroupNotNull(Project p)
        {
            var env = Environment.Create(new EnvironmentName(TestLiterals.Project.Name.Correct), p);
            Assert.NotNull(env.GetRootOptionGroop());
        }

        [Theory]
        [ClassData(typeof(EmptyProject))]
        public void Create_CorrectData_RootGroupNameIsEmptyString(Project p)
        {
            var env = Environment.Create(new EnvironmentName(TestLiterals.Project.Name.Correct), p);
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
