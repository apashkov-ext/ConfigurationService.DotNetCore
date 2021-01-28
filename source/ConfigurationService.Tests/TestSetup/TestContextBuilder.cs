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
    internal class TestContextBuilder<TContext> where TContext : DbContext
    {
        private readonly Mock<TContext> _contextMock;

        public TestContextBuilder(Mock<TContext> contextMock)
        {
            _contextMock = contextMock;
        }

        /// <summary>
        /// Setups empty DbSet of <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">Type of DbSet entities.</typeparam>
        /// <param name="expression">Specifies a non-void member of a mocked context.</param>
        public void SetupDbSet<TEntity>(Expression<Func<TContext, DbSet<TEntity>>> expression) where TEntity : Entity
        {
            _contextMock.Setup(expression).Returns(ConstructDbSet(new List<TEntity>()).Object);
        }

        /// <summary>
        /// Setups DbSet of <typeparamref name="TEntity"/> with a test data.
        /// </summary>
        /// <typeparam name="TEntity">Type of DbSet entities.</typeparam>
        /// <param name="expression">Specifies a non-void member of a mocked context.</param>
        public void SetupDbSet<TEntity>(Expression<Func<TContext, DbSet<TEntity>>> expression, IEnumerable<TEntity> testData) where TEntity : Entity
        {
            _contextMock.Setup(expression).Returns(ConstructDbSet(testData ?? new List<TEntity>()).Object);
        }

        private static Mock<DbSet<TEntity>> ConstructDbSet<TEntity>(IEnumerable<TEntity> data) where TEntity : Entity
        {
            return data.AsQueryable().BuildMockDbSet();
        }
    }
}