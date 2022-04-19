using ConfigurationManagementSystem.Application.Stories.GetConfigurationByIdStory;
using ConfigurationManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Persistence.StoryImplementations.GetConfigurationByIdStory
{
    public class GetConfigurationByIdWithoutHierarchyQueryEF : GetConfigurationByIdWithoutHierarchyQuery
    {
        private readonly ConfigurationManagementSystemContext _context;

        public GetConfigurationByIdWithoutHierarchyQueryEF(ConfigurationManagementSystemContext context)
        {
            _context = context;
        }

        public override Task<ConfigurationEntity> ExecuteAsync(Guid id)
        {
            return _context.Configurations.Include(x => x.Application).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
