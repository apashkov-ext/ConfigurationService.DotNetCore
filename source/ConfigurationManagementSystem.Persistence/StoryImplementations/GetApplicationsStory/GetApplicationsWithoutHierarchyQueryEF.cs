using ConfigurationManagementSystem.Application.Stories.GetApplicationsStory;
using ConfigurationManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Persistence.StoryImplementations
{
    public class GetApplicationsWithoutHierarchyQueryEF : GetApplicationsWithoutHierarchyQuery
    {
        private readonly ConfigurationManagementSystemContext _context;

        public GetApplicationsWithoutHierarchyQueryEF(ConfigurationManagementSystemContext context)
        {
            _context = context;
        }

        public override Task<IQueryable<ApplicationEntity>> ExecuteAsync()
        {
            var result = _context.Applications
                .AsNoTrackingWithIdentityResolution();
            return Task.FromResult(result);
        }
    }
}
