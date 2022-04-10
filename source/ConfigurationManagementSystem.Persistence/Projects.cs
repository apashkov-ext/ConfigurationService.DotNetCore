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
    public class Applications : IApplications
    {
        private readonly ConfigurationManagementSystemContext _context;

        public Applications(ConfigurationManagementSystemContext context)
        {
            _context = context;
        }

        public Task<PagedList<ApplicationEntity>> GetAsync(string name, PaginationOptions paginationOptions)
        {
            if (paginationOptions == null) throw new ArgumentNullException(nameof(paginationOptions));

            var result = _context.Applications
                .ApplicationsWithIncludedEntities()
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

        public async Task<ApplicationEntity> GetAsync(Guid id)
        {
            var project = await _context.Applications
                .ApplicationsWithIncludedEntities()
                .FirstOrDefaultAsync(x => x.Id == id);

            return project ?? throw new EntityNotFoundException("Application does not exist");
        }

        public async Task<ApplicationEntity> AddAsync(string name)
        {
            var projName = new ApplicationName(name);
            var existed = await _context.Applications.FirstOrDefaultAsync(x => x.Name.Value == projName.Value);
            if (existed != null)
            {
                throw new AlreadyExistsException("Application with the same name already exists");
            }

            var newProj = Domain.Entities.ApplicationEntity.Create(new ApplicationName(name), new ApiKey(Guid.NewGuid()));
            await _context.Applications.AddAsync(newProj);
            await _context.SaveChangesAsync();

            return newProj;
        }

        public async Task RemoveAsync(Guid id)
        {
            var project = await _context.Applications.ApplicationsWithIncludedEntities().FirstOrDefaultAsync(x => x.Id == id);
            if (project == null)
            {
                throw new EntityNotFoundException("Application does not exist");
            }

            project.RemoveWithHierarchy(_context);
            await _context.SaveChangesAsync();
        }
    }
}
