using System;
using System.Collections.Generic;
using System.Text;
using ConfigurationService.Domain.ValueObjects;
using Xunit;

namespace ConfigurationService.Tests.DomainTests.ValueObjectsTests
{
    public class DescriptionTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("   ")]
        public void New_IncorrectValues_Fails(string val)
        {
            Assert.Throws<ApplicationException>(() => new Description(val));
        }

        [Theory]
        [InlineData("")]
        [InlineData("Description Описание 34 _-()[]{}")]
        public void New_CorrectValues_Success(string val)
        {
            Assert.NotNull(new Description(val));
        }
    }
}
