using ConfigurationManagementSystem.Application.Stories.GetConfigurationsStory;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Persistence.StoryImplementations.GetConfigurationsStory
{
    public class GetConfigurationsWithHierarchyQueryEF : GetConfigurationsWithHierarchyQuery
    {
        private readonly ConfigurationManagementSystemContext _context;

        public GetConfigurationsWithHierarchyQueryEF(ConfigurationManagementSystemContext context)
        {
            _context = context;
        }

        public override Task<IQueryable<ConfigurationEntity>> ExecuteAsync()
        {
            var result = _context.Configurations
                .ConfigurationsWithIncludedEntities()
                .AsNoTrackingWithIdentityResolution();
            return Task.FromResult(result);
        }
    }
}
