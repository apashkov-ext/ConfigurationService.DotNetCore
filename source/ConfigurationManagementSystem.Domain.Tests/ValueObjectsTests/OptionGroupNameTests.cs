using ConfigurationManagementSystem.Domain.Exceptions;
using ConfigurationManagementSystem.Domain.ValueObjects;
using Xunit;

namespace ConfigurationManagementSystem.Domain.Tests.ValueObjectsTests
{
    public class OptionGroupNameTests
    {
        [Theory]
        [InlineData("_name")]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("-name")]
        [InlineData("3")]
        [InlineData("3v")]
        [InlineData("v3")]
        [InlineData("na me")]
        public void New_IncorrectNames_Exception(string name)
        {
            Assert.Throws<InconsistentDataStateException>(() => new OptionGroupName(name));
        }

        [Theory]
        [InlineData("")]
        [InlineData("val")]
        [InlineData("v")]
        [InlineData("VariAble")]
        public void New_CorrectNames_Success(string name)
        {
            _ = new OptionGroupName(name);
        }
    }
}
