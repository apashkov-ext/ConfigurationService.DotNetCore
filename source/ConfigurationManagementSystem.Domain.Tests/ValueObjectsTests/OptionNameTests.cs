using ConfigurationManagementSystem.Domain.Exceptions;
using ConfigurationManagementSystem.Domain.ValueObjects;
using Xunit;

namespace ConfigurationManagementSystem.Domain.Tests.ValueObjectsTests
{
    public class OptionNameTests
    {
        [Theory]
        [InlineData("_name")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("-name")]
        [InlineData("3")]
        [InlineData("3v")]
        [InlineData("v3")]
        [InlineData("na me")]
        public void New_IncorrectNames_Exception(string name)
        {
            Assert.Throws<InconsistentDataStateException>(() => new OptionName(name));
        }

        [Theory]
        [InlineData("val")]
        [InlineData("v")]
        [InlineData("VariAble")]
        public void New_CorrectNames_Success(string name)
        {
            _ = new OptionName(name);
        }
    }
}
