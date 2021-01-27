using System;
using ConfigurationService.Domain.ValueObjects;
using Xunit;

namespace ConfigurationService.Tests.DomainTests.ValueObjectsTests
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
        public void New_IncorrectNames_Fails(string name)
        {
            Assert.Throws<ApplicationException>(() => new OptionGroupName(name));
        }

        [Theory]
        [InlineData("")]
        [InlineData("val")]
        [InlineData("v")]
        [InlineData("VariAble")]
        public void New_CorrectNames_Success(string name)
        {
            Assert.NotNull(new OptionGroupName(name));
        }
    }
}
