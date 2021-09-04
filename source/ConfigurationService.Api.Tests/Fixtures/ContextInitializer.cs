using ConfigurationService.Domain.Entities;
using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationService.Api.Tests
{
    internal class ContextInitializer<TContext> where TContext : DbContext
    {
        private readonly TContext _context;

        public ContextInitializer(TContext context)
        {
            _context = context;
        }

        public ContextInitializer<TContext> WithEntities<TEntity>(Expression<Func<TContext, DbSet<TEntity>>> expression, params TEntity[] testData)
            where TEntity : Entity
        {
            _context.Set<TEntity>().AddRange(testData);
            return this;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
