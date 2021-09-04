using Microsoft.EntityFrameworkCore;

namespace ConfigurationService.Api.Tests
{
    internal class ContextSetup<TContext> where TContext : DbContext
    {
        private readonly TContext _context;

        public ContextSetup(TContext context)
        {
            _context = context;
        }

        public ContextInitializer<TContext> Initialize()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            var c = new ContextInitializer<TContext>(_context);
            return c;
        }
    }
}
