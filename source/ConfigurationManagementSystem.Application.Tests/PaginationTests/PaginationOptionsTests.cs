using System;
using ConfigurationManagementSystem.Application.Pagination;
using Xunit;

namespace ConfigurationManagementSystem.Application.Tests.PaginationTests
{
    public class PaginationOptionsTests
    {
        [Fact]
        public void CreateInstance_MinimalCorrectParams_Success()
        {
            _ = new PaginationOptions(0, 1);
        }

        [Fact]
        public void CreateInstance_IncorrectOffset_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PaginationOptions(-1, 1));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CreateInstance_IncorrectLimit_ThrowsException(int limit)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PaginationOptions(0, limit));
        }
    }
}
