using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Application;
using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Domain;
using ConfigurationManagementSystem.Domain.Exceptions;
using ConfigurationManagementSystem.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Configuration = ConfigurationManagementSystem.Domain.Entities.Configuration;

namespace ConfigurationManagementSystem.Persistence
{
    public class Environments : IEnvironments
    {
        private readonly ConfigurationManagementSystemContext _context;

        public Environments(ConfigurationManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Configuration>> GetAsync(string name)
        {
            var e = _context.Environments.ToList();
            var p = _context.Projects.ToList();

            if (string.IsNullOrEmpty(name))
            {
                var all = await _context.Environments
                    .EnvironmentsWithIncludedEntities()
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();

                return all;
            }

            var list = await _context.Environments
                .EnvironmentsWithIncludedEntities()
                .Where(x => x.Name.Value.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();

            return list;
        }

        public async Task<Configuration> GetAsync(Guid id)
        {
            var env = await _context.Environments.EnvironmentsWithIncludedEntities().FirstOrDefaultAsync(x => x.Id == id);
            return env ?? throw new NotFoundException("Environment does not exist");
        }

        public async Task<Configuration> AddAsync(Guid projectId, string name)
        {
            var proj = await _context.Projects.Include(x => x.Environments).AsSingleQuery().FirstOrDefaultAsync(x => x.Id == projectId);
            if (proj == null)
            {
                throw new NotFoundException("Project does not exist");
            }

            var env = proj.AddEnvironment(new EnvironmentName(name));
            await _context.SaveChangesAsync();

            return env;
        }

        public async Task UpdateAsync(Guid id, string name)
        {
            var envName = new EnvironmentName(name);
            var env = await _context.Environments
                .Include(x => x.Project)
                .ThenInclude(x => x.Environments)
                .AsSingleQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (env == null)
            {
                throw new NotFoundException("Environment does not exist");
            }

            if (env.Project.Environments.Any(x => x.Name == envName))
            {
                throw new InconsistentDataStateException("Environment with the same name already exists in this project");
            }

            env.UpdateName(envName);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var env = await _context.Environments
                .Include(x => x.Project)
                .AsSingleQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (env == null)
            {
                throw new NotFoundException("Environment does not exist");
            }

            env.RemoveWithHierarchy(_context);

            await _context.SaveChangesAsync();
        }
    }
}
