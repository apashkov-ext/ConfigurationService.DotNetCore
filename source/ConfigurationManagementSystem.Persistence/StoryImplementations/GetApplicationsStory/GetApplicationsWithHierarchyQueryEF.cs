using ConfigurationManagementSystem.Application.Stories.GetApplicationsStory;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Persistence.StoryImplementations
{
    public class GetApplicationsWithHierarchyQueryEF : GetApplicationsWithHierarchyQuery
    {
        private readonly ConfigurationManagementSystemContext _context;

        public GetApplicationsWithHierarchyQueryEF(ConfigurationManagementSystemContext context)
        {
            _context = context;
        }

        public override Task<IQueryable<ApplicationEntity>> ExecuteAsync()
        {
            var result = _context.Applications
                .ApplicationsWithIncludedEntities()
                .AsNoTrackingWithIdentityResolution();
            return Task.FromResult(result);
        }
    }
}
