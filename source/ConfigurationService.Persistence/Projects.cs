using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ConfigurationService.Domain;
using ConfigurationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationService.Persistence
{
    public class Projects : IProjects
    {
        private readonly ConfigurationServiceContext _context;

        public Projects(ConfigurationServiceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetItems()
        {
            var projects = await _context.Projects
                .Include(x => x.Environments)
                .ThenInclude(x => x.OptionGroup)
                .ThenInclude(x => x.Children)
                .ThenInclude(x => x.Options)
                .ToListAsync();

            //var projects = await _context.Projects
            //    .ToListAsync();

            return projects;
        }
    }
}
