using System;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ConfigurationService.Tests.TestSetup
{
    /// <summary>
    /// Testable EF context.
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    internal class MockedContext<TContext> : Mock<TContext> where TContext : DbContext
    {
        /// <summary>
        /// Creates instance of the testable EF context.
        /// </summary>
        /// <param name="configure">Configures testable context.</param>
        public MockedContext(Action<TestContextConfigurer<TContext>> configure = null)
        {
            configure?.Invoke(new TestContextConfigurer<TContext>(this));
        }
    }
}