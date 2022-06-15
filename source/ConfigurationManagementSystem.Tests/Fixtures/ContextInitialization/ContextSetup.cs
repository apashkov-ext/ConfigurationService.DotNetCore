using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ConfigurationManagementSystem.Tests.Fixtures.ContextInitialization
{
    public interface IContextSetup<TContext> where TContext : DbContext, ICleanableDbContext
    {
        /// <summary>
        /// Adds some entities to the <see cref="DbSet{TEntity}"./>
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <param name="entities">Collection of entities.</param>
        IContextSetup<TContext> AddEntities<TEntity>(params TEntity[] entities) where TEntity : DomainEntity;
    }

    /// <summary>
    /// Contains methods for the database data initialization. 
    /// </summary>
    /// <typeparam name="TContext">Concrete type of the <see cref="DbContext"/>.</typeparam>
    public class ContextSetup<TContext> : IContextSetup<TContext> where TContext : DbContext, ICleanableDbContext
    {
        private readonly TContext _context;
        private readonly List<Action<TContext>> _detachActions = new();

        public ContextSetup(TContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds some entities to the <see cref="DbSet{TEntity}"./>
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <param name="entities">Collection of entities.</param>
        public IContextSetup<TContext> AddEntities<TEntity>(params TEntity[] entities)
            where TEntity : DomainEntity
        {
            _context.Set<TEntity>().AddRange(entities);
            _detachActions.Add(context =>
            {
                foreach (var entity in context.Set<TEntity>().Local) 
                {
                    context.Entry(entity).State = EntityState.Detached;
                }
            });
            return this;
        }

        /// <summary>
        /// Commits all changes.
        /// </summary>
        public void Initialize()
        {   
            _context.CleanData();
            _context.SaveChanges();
        }
    }
}
