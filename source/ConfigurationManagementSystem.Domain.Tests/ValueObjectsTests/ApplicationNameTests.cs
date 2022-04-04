using ConfigurationManagementSystem.Domain.Exceptions;
using ConfigurationManagementSystem.Domain.ValueObjects;
using Xunit;

namespace ConfigurationManagementSystem.Domain.Tests.ValueObjectsTests
{
    public class ApplicationNameTests
    {
        [Theory] 
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("12")]
        [InlineData("3proj")]
        [InlineData("_proj")]
        [InlineData("-proj")]
        [InlineData(null)]
        [InlineData("na me")]
        [InlineData("_Test_Project")]
        public void New_IncorrectNames_Exception(string name)
        {
            Assert.Throws<InconsistentDataStateException>(() => new ApplicationName(name));
        }

        [Theory]
        [InlineData("proj")]
        [InlineData("p")]
        [InlineData("Proj")]
        [InlineData("pRoj")]
        [InlineData("pRoj3")]
        [InlineData("pRoj_3--g")]
        [InlineData("TestProject")]
        public void New_CorrectNames_Success(string name)
        {
            var n = new ApplicationName(name);
        }
    }
}
