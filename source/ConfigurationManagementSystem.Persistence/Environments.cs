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
using ConfigurationEntity = ConfigurationManagementSystem.Domain.Entities.ConfigurationEntity;

namespace ConfigurationManagementSystem.Persistence
{
    public class Environments : IEnvironments
    {
        private readonly ConfigurationManagementSystemContext _context;

        public Environments(ConfigurationManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ConfigurationEntity>> GetAsync(string name)
        {
            var e = _context.Configurations.ToList();
            var p = _context.Applications.ToList();

            if (string.IsNullOrEmpty(name))
            {
                var all = await _context.Configurations
                    .ConfigurationsWithIncludedEntities()
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();

                return all;
            }

            var list = await _context.Configurations
                .ConfigurationsWithIncludedEntities()
                .Where(x => x.Name.Value.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();

            return list;
        }

        public async Task<ConfigurationEntity> GetAsync(Guid id)
        {
            var env = await _context.Configurations.ConfigurationsWithIncludedEntities().FirstOrDefaultAsync(x => x.Id == id);
            return env ?? throw new EntityNotFoundException("Environment does not exist");
        }

        public async Task<ConfigurationEntity> AddAsync(Guid projectId, string name)
        {
            var proj = await _context.Applications.Include(x => x.Configurations).AsSingleQuery().FirstOrDefaultAsync(x => x.Id == projectId);
            if (proj == null)
            {
                throw new EntityNotFoundException("Project does not exist");
            }

            var env = proj.AddConfiguration(new ConfigurationName(name));
            await _context.SaveChangesAsync();

            return env;
        }

        public async Task UpdateAsync(Guid id, string name)
        {
            var envName = new ConfigurationName(name);
            var env = await _context.Configurations
                .Include(x => x.Application)
                .ThenInclude(x => x.Configurations)
                .AsSingleQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (env == null)
            {
                throw new EntityNotFoundException("Environment does not exist");
            }

            if (env.Application.Configurations.Any(x => x.Name == envName))
            {
                throw new InconsistentDataStateException("Environment with the same name already exists in this project");
            }

            env.UpdateName(envName);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var env = await _context.Configurations
                .Include(x => x.Application)
                .AsSingleQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (env == null)
            {
                throw new EntityNotFoundException("Environment does not exist");
            }

            env.RemoveWithHierarchy(_context);

            await _context.SaveChangesAsync();
        }
    }
}
