using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Application;
using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using ConfigurationManagementSystem.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationManagementSystem.Persistence
{
    public class Projects : IProjects
    {
        private readonly ConfigurationServiceContext _context;

        public Projects(ConfigurationServiceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAsync(string name)
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

        public async Task<Project> GetAsync(Guid id)
        {
            var project = await _context.Projects
                .ProjectsWithIncludedEntities()
                .FirstOrDefaultAsync(x => x.Id == id);

            return project ?? throw new NotFoundException("Project does not exist");
        }

        public async Task<Project> AddAsync(string name)
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

        public async Task RemoveAsync(Guid id)
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
