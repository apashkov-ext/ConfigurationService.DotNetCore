using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigurationService.Application;
using ConfigurationService.Application.Exceptions;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
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

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _context.Projects.ProjectsWithIncludedEntities().ToListAsync();
        }

        public async Task<Project> GetProjectByName(string name)
        {
            var projName = new ProjectName(name);
            var project = await _context.Projects.ProjectsWithIncludedEntities().FirstOrDefaultAsync(x => x.Name == projName);
            return project ?? throw new NotFoundException("Project does not exist");
        }
    }
}
