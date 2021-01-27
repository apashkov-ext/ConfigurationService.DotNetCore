using System;
using ConfigurationService.Domain.ValueObjects;
using Xunit;

namespace ConfigurationService.Tests.DomainTests.ValueObjectsTests
{
    public class ApiKeyTests
    {
        [Theory]
        [InlineData((string)null)]
        [InlineData("")]
        [InlineData("ere54rfg")]
        public void NewByString_IncorrectValues_Fails(string val)
        {
            Assert.Throws<ApplicationException>(() => new ApiKey(val));
        }

        [Fact]
        public void NewByGuid_Empty_Fails()
        {
            Assert.Throws<ApplicationException>(() => new ApiKey(Guid.Empty));
        }

        [Fact]
        public void NewByGuid_NotEmpty_Fails()
        {
            Assert.NotNull(new ApiKey(Guid.NewGuid()));
        }
    }
}
