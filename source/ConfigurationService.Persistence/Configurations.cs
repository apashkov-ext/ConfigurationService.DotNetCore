using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationService.Application;
using ConfigurationService.Application.Exceptions;
using ConfigurationService.Domain;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationService.Persistence
{
    public class Configurations : IConfigurations
    {
        private readonly ConfigurationServiceContext _context;

        public Configurations(ConfigurationServiceContext context)
        {
            _context = context;
        }

        public async Task<OptionGroup> GetItem(string project, string environment, string apiKey)
        {
            var projName = new ProjectName(project);
            var envName = new EnvironmentName(environment);
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new NotFoundException("Project does not exist");
            }
            var key = new ApiKey(apiKey);

            var proj = await _context.Projects.ProjectsWithIncludedEntities().FirstOrDefaultAsync(x => x.Name.Value == projName.Value);
            if (proj == null || proj.ApiKey.Value != key.Value)
            {
                throw new NotFoundException("Project does not exist");
            }

            var env = proj.Environments.FirstOrDefault(x => x.Name.Value == envName.Value);
            if (env == null)
            {
                throw new NotFoundException("Configuration does not exist");
            }

            return env.OptionGroup;
        }

        public async Task<OptionGroup> Add(string project, string environment)
        {
            var projName = new ProjectName(project);
            var envName = new EnvironmentName(environment);
            var proj = await _context.Projects.ProjectsWithIncludedEntities().FirstOrDefaultAsync(x => x.Name.Value == projName.Value);
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

            var newEnv = Environment.Create(envName, proj, !envs.Any(), null);
            var newGroup = OptionGroup.Create(new OptionGroupName(""), new Description(""), new List<Option>(), null, new List<OptionGroup>(), newEnv);
            newEnv.SetOptionGroup(newGroup);
            proj.AddEnvironment(newEnv);

            await _context.Environments.AddAsync(newEnv);
            await _context.OptionGroups.AddAsync(newGroup);
            await _context.SaveChangesAsync();

            return newGroup;
        }
    }
}
