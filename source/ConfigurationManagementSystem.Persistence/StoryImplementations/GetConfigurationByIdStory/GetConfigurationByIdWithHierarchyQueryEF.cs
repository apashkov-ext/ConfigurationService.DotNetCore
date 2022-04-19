using ConfigurationManagementSystem.Application.Stories.GetConfigurationByIdStory;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Persistence.StoryImplementations.GetConfigurationByIdStory
{
    public class GetConfigurationByIdWithHierarchyQueryEF : GetConfigurationByIdWithHierarchyQuery
    {
        private readonly ConfigurationManagementSystemContext _context;

        public GetConfigurationByIdWithHierarchyQueryEF(ConfigurationManagementSystemContext context)
        {
            _context = context;
        }

        public override Task<ConfigurationEntity> ExecuteAsync(Guid id)
        {
            return _context.Configurations.ConfigurationsWithIncludedEntities().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
