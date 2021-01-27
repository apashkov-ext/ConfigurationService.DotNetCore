using System;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ConfigurationService.Tests.TestSetup
{
    public class MockedContext<TContext> : Mock<TContext> where TContext : DbContext
    {
        public MockedContext(Action<TestContextBuilder<TContext>> builder = null)
        {
            builder?.Invoke(new TestContextBuilder<TContext>(this));
        }
    }
}