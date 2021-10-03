using ConfigurationManagementSystem.Domain.Exceptions;
using ConfigurationManagementSystem.Domain.ValueObjects;
using Xunit;

namespace ConfigurationManagementSystem.Domain.Tests.ValueObjectsTests
{
    public class DescriptionTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("   ")]
        public void New_IncorrectValues_Exception(string val)
        {
            Assert.Throws<InconsistentDataStateException>(() => new Description(val));
        }

        [Theory]
        [InlineData("")]
        [InlineData("Description Описание 34 _-()[]{}")]
        public void New_CorrectValues_Success(string val)
        {
            _ = new Description(val);
        }
    }
}
