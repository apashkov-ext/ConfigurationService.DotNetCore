using System;
using ConfigurationService.Domain.ValueObjects;
using ConfigurationService.Tests;
using Xunit;

namespace ConfigurationService.Domain.Tests.ValueObjectsTests
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
        [InlineData(TestLiterals.Project.Name.Incorrect)]
        public void New_IncorrectNames_Exception(string name)
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
        [InlineData(TestLiterals.Project.Name.Correct)]
        public void New_CorrectNames_Success(string name)
        {
            var n = new ProjectName(name);
        }
    }
}
