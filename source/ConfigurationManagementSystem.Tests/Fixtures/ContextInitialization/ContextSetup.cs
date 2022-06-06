using ConfigurationManagementSystem.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationManagementSystem.Tests.Fixtures.ContextInitialization
{
    /// <summary>
    /// Setups context before data initialization.
    /// </summary>
    /// <typeparam name="TContext">Concrete type of the <see cref="DbContext"/>.</typeparam>
    public class ContextSetup<TContext> where TContext : DbContext, ICleanableDbContext
    {
        private readonly TContext _context;

        public ContextSetup(TContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Prepares clean DbContext.
        /// This action removes all data from the database.
        /// </summary>
        public ContextInitializer<TContext> Setup()
        {
            var c = new ContextInitializer<TContext>(_context);
            return c;
        }
    }
}
