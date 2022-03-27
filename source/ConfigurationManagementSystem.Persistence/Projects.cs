using System;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Application;
using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Application.Pagination;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using ConfigurationManagementSystem.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationManagementSystem.Persistence
{
    public class Projects : IProjects
    {
        private readonly ConfigurationManagementSystemContext _context;

        public Projects(ConfigurationManagementSystemContext context)
        {
            _context = context;
        }

        public Task<PagedList<Domain.Entities.Application>> GetAsync(string name, PaginationOptions paginationOptions)
        {
            if (paginationOptions == null) throw new ArgumentNullException(nameof(paginationOptions));

            var result = _context.Projects
                .ProjectsWithIncludedEntities()
                .AsNoTrackingWithIdentityResolution();

            if (string.IsNullOrEmpty(name))
            {
                var all = result.ToPagedList(paginationOptions);

                return Task.FromResult(all);
            }

            var filtered = result
                .Where(x => x.Name.Value.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
                .ToPagedList(paginationOptions);

            return Task.FromResult(filtered);
        }

        public async Task<Domain.Entities.Application> GetAsync(Guid id)
        {
            var project = await _context.Projects
                .ProjectsWithIncludedEntities()
                .FirstOrDefaultAsync(x => x.Id == id);

            return project ?? throw new NotFoundException("Project does not exist");
        }

        public async Task<Domain.Entities.Application> AddAsync(string name)
        {
            var projName = new ProjectName(name);
            var existed = await _context.Projects.FirstOrDefaultAsync(x => x.Name.Value == projName.Value);
            if (existed != null)
            {
                throw new AlreadyExistsException("Projects with the same name already exists");
            }

            var newProj = Domain.Entities.Application.Create(new ProjectName(name), new ApiKey(Guid.NewGuid()));
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
