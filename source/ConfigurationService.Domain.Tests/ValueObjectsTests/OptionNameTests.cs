using ConfigurationService.Domain.Exceptions;
using ConfigurationService.Domain.ValueObjects;
using Xunit;

namespace ConfigurationService.Domain.Tests.ValueObjectsTests
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
            var n = new OptionName(name);
        }
    }
}
