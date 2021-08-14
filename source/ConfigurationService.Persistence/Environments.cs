using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationService.Application;
using ConfigurationService.Application.Exceptions;
using ConfigurationService.Domain;
using ConfigurationService.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Environment = ConfigurationService.Domain.Entities.Environment;

namespace ConfigurationService.Persistence
{
    public class Environments : IEnvironments
    {
        private readonly ConfigurationServiceContext _context;

        public Environments(ConfigurationServiceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Environment>> GetAsync()
        {
            return await _context.Environments.EnvironmentsWithIncludedEntities().ToListAsync();
        }

        public async Task<Environment> GetAsync(Guid id)
        {
            var env = await _context.Environments.EnvironmentsWithIncludedEntities().FirstOrDefaultAsync(x => x.Id == id);
            return env ?? throw new NotFoundException("Environment does not exist");
        }

        public async Task<Environment> AddAsync(Guid projectId, string name)
        {
            var proj = await _context.Projects.Include(x => x.Environments).FirstOrDefaultAsync(x => x.Id == projectId);
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
            var env = await _context.Environments.Include(x => x.Project).ThenInclude(x => x.Environments).FirstOrDefaultAsync(x => x.Id == id);
            if (env == null)
            {
                throw new NotFoundException("Environment does not exist");
            }

            if (env.Project.Environments.Any(x => x.Name == envName))
            {
                throw new InconsistentDataState("Environment with the same name already exists in this project");
            }

            env.UpdateName(envName);
            _context.Environments.Update(env);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var existed = await _context.Environments.FirstOrDefaultAsync(x => x.Id == id);
            if (existed == null)
            {
                throw new NotFoundException("Environment does not exist");
            }

            _context.Environments.Remove(existed);
            await _context.SaveChangesAsync();
        }
    }
}
