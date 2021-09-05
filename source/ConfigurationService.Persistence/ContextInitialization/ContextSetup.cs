using Microsoft.EntityFrameworkCore;

namespace ConfigurationService.Persistence.ContextInitialization
{
    /// <summary>
    /// Setups context before data initialization.
    /// </summary>
    /// <typeparam name="TContext">Concrete type of the <see cref="DbContext"/>.</typeparam>
    public class ContextSetup<TContext> where TContext : DbContext
    {
        private readonly TContext _context;

        public ContextSetup(TContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Runs clean initialization for the DbContext.
        /// This action removes all data from the database.
        /// </summary>
        public ContextInitializer<TContext> Initialize()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            var c = new ContextInitializer<TContext>(_context);
            return c;
        }
    }
}
