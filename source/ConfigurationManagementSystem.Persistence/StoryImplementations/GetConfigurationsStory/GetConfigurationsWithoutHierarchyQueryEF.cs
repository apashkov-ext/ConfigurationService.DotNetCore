using ConfigurationManagementSystem.Application.Stories.GetConfigurationsStory;
using ConfigurationManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Persistence.StoryImplementations.GetConfigurationsStory
{
    public class GetConfigurationsWithoutHierarchyQueryEF : GetConfigurationsWithoutHierarchyQuery
    {
        private readonly ConfigurationManagementSystemContext _context;

        public GetConfigurationsWithoutHierarchyQueryEF(ConfigurationManagementSystemContext context)
        {
            _context = context;
        }

        public override Task<IQueryable<ConfigurationEntity>> ExecuteAsync()
        {
            var result = _context.Configurations
                .Include(x => x.Application)
                .AsNoTrackingWithIdentityResolution();
            return Task.FromResult(result);
        }
    }
}
