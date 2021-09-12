using ConfigurationService.Domain.Exceptions;
using ConfigurationService.Domain.ValueObjects;
using Xunit;

namespace ConfigurationService.Domain.Tests.ValueObjectsTests
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
            var d = new Description(val);
        }
    }
}
