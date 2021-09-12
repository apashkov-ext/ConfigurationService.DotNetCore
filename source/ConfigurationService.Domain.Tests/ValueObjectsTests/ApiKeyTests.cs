﻿using System;
using ConfigurationService.Domain.Exceptions;
using ConfigurationService.Domain.ValueObjects;
using Xunit;

namespace ConfigurationService.Domain.Tests.ValueObjectsTests
{
    public class ApiKeyTests
    {
        [Theory]
        [InlineData((string)null)]
        [InlineData("")]
        [InlineData("ere54rfg")]
        public void NewByString_IncorrectValues_Exception(string val)
        {
            Assert.Throws<InconsistentDataStateException>(() => new ApiKey(val));
        }

        [Fact]
        public void NewByGuid_Empty_Exception()
        {
            Assert.Throws<InconsistentDataStateException>(() => new ApiKey(Guid.Empty));
        }

        [Fact]
        public void NewByGuid_NotEmpty_Success()
        {
            var apiKey = new ApiKey(Guid.NewGuid());
        }
    }
}
