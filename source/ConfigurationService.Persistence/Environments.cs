using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationService.Application;
using ConfigurationService.Application.Exceptions;
using ConfigurationService.Domain;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
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

        public async Task<Environment> Get(Guid id)
        {
            var env = await _context.Environments.EnvironmentsWithIncludedEntities().FirstOrDefaultAsync(x => x.Id == id);
            return env ?? throw new NotFoundException("Environment does not exist");
        }

        public async Task<Environment> Add(Guid projectId, string name)
        {
            var envName = new EnvironmentName(name);
            var proj = await _context.Projects.Include(x => x.Environments).FirstOrDefaultAsync(x => x.Id == projectId);
            if (proj == null)
            {
                throw new NotFoundException("Project does not exist");
            }

            var envs = proj.Environments.ToList();
            var env = envs.FirstOrDefault(x => x.Name.Value == envName.Value);
            if (env != null)
            {
                throw new AlreadyExistsException("Environment with the same name already exists");
            }

            
            var newGroup = OptionGroup.Create(new OptionGroupName(""), new Description(""), new List<Option>(), null, new List<OptionGroup>());
            var newEnv = Environment.Create(envName, proj, !envs.Any(), newGroup);
            newGroup.SetEnvironment(newEnv);

            proj.AddEnvironment(newEnv);

            await _context.Environments.AddAsync(newEnv);
            await _context.OptionGroups.AddAsync(newGroup);
            await _context.SaveChangesAsync();

            return newEnv;
        }

        public async Task Update(Guid id, string name)
        {
            var envName = new EnvironmentName(name);
            var env = await _context.Environments.FirstOrDefaultAsync(x => x.Id == id);
            if (env == null)
            {
                throw new NotFoundException("Environment does not exist");
            }

            env.UpdateName(envName);
            _context.Environments.Update(env);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Guid id)
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
