using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ConfigurationManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace ConfigurationManagementSystem.Tests
{
    /// <summary>
    /// Testable EF context configurator.
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class TestContextConfigurator<TContext> where TContext : DbContext
    {
        private readonly Mock<TContext> _contextMock;

        public TestContextConfigurator(Mock<TContext> contextMock)
        {
            _contextMock = contextMock;
        }

        /// <summary>
        /// Setups empty DbSet of <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">Type of DbSet entities.</typeparam>
        /// <param name="expression">Specifies a non-void member of a mocked context.</param>
        /// <param name="testData">Puts data into DbSet.</param>
        public TestContextConfigurator<TContext> WithSet<TEntity>(Expression<Func<TContext, DbSet<TEntity>>> expression, params TEntity[] testData) 
            where TEntity : DomainEntity
        {
            _contextMock.Setup(expression).Returns(ConstructDbSet(testData).Object);
            return this;
        }

        private static Mock<DbSet<TEntity>> ConstructDbSet<TEntity>(IEnumerable<TEntity> data) where TEntity : DomainEntity
        {
            return data.AsQueryable().BuildMockDbSet();
        }
    }
}