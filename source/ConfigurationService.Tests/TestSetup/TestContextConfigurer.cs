using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ConfigurationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace ConfigurationService.Tests.TestSetup
{
    /// <summary>
    /// Testable EF context configurer.
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    internal class TestContextConfigurer<TContext> where TContext : DbContext
    {
        private readonly Mock<TContext> _contextMock;

        public TestContextConfigurer(Mock<TContext> contextMock)
        {
            _contextMock = contextMock;
        }

        /// <summary>
        /// Setups empty DbSet of <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">Type of DbSet entities.</typeparam>
        /// <param name="expression">Specifies a non-void member of a mocked context.</param>
        /// <param name="testData">Puts data into DbSet.</param>
        public TestContextConfigurer<TContext> WithSet<TEntity>(Expression<Func<TContext, DbSet<TEntity>>> expression, params TEntity[] testData) 
            where TEntity : Entity
        {
            _contextMock.Setup(expression).Returns(ConstructDbSet(testData).Object);
            return this;
        }

        private static Mock<DbSet<TEntity>> ConstructDbSet<TEntity>(IEnumerable<TEntity> data) where TEntity : Entity
        {
            return data.AsQueryable().BuildMockDbSet();
        }
    }
}