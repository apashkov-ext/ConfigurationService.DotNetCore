using ConfigurationManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationManagementSystem.Persistence.ContextInitialization
{
    /// <summary>
    /// Contains methods for the database data initialization. 
    /// </summary>
    /// <typeparam name="TContext">Concrete type of the <see cref="DbContext"/>.</typeparam>
    public class ContextInitializer<TContext> where TContext : DbContext
    {
        private readonly TContext _context;

        public ContextInitializer(TContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds some entities to the <see cref="DbSet{TEntity}"./>
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <param name="entities">Collection of entities.</param>
        public ContextInitializer<TContext> WithEntities<TEntity>(params TEntity[] entities)
            where TEntity : DomainEntity
        {
            var toRemove = _context.Set<TEntity>();
            _context.Set<TEntity>().RemoveRange(toRemove);
            _context.Set<TEntity>().AddRange(entities);
            return this;
        }

        /// <summary>
        /// Commits all changes.
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
