using System;
using System.Collections.Generic;
using System.Text;
using ConfigurationService.Domain.ValueObjects;
using Xunit;

namespace ConfigurationService.Tests.DomainTests.ValueObjectsTests
{
    public class ProjectNameTests
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
        public void New_IncorrectNames_Fails(string name)
        {
            Assert.Throws<ApplicationException>(() => new ProjectName(name));
        }

        [Theory]
        [InlineData("proj")]
        [InlineData("p")]
        [InlineData("Proj")]
        [InlineData("pRoj")]
        [InlineData("pRoj3")]
        [InlineData("pRoj_3--g")]
        public void New_CorrectNames_Success(string name)
        {
            Assert.NotNull(new ProjectName(name));
        }
    }
}
