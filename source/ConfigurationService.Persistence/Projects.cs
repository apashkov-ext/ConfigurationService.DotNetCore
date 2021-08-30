using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationService.Application;
using ConfigurationService.Application.Exceptions;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using ConfigurationService.Persistence.Extensions;
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

        public async Task<IEnumerable<Project>> Get(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var all =  await _context.Projects
                    .ProjectsWithIncludedEntities()
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();

                return all;
            }

            var list = await _context.Projects
                .ProjectsWithIncludedEntities()
                .AsNoTrackingWithIdentityResolution()
                .Where(x => x.Name.Value.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
                .ToListAsync();

            return list;
        }

        public async Task<Project> Get(Guid id)
        {
            var project = await _context.Projects
                .ProjectsWithIncludedEntities()
                .FirstOrDefaultAsync(x => x.Id == id);

            return project ?? throw new NotFoundException("Project does not exist");
        }

        public async Task<Project> Add(string name)
        {
            var projName = new ProjectName(name);
            var existed = await _context.Projects.FirstOrDefaultAsync(x => x.Name.Value == projName.Value);
            if (existed != null)
            {
                throw new AlreadyExistsException("Projects with the same name already exists");
            }

            var newProj = Project.Create(new ProjectName(name), new ApiKey(Guid.NewGuid()));
            await _context.Projects.AddAsync(newProj);
            await _context.SaveChangesAsync();

            return newProj;
        }

        public async Task Remove(Guid id)
        {
            var project = await _context.Projects.ProjectsWithIncludedEntities().FirstOrDefaultAsync(x => x.Id == id);
            if (project == null)
            {
                throw new NotFoundException("Project does not exist");
            }

            project.RemoveWithHierarchy(_context);
            await _context.SaveChangesAsync();
        }
    }
}
