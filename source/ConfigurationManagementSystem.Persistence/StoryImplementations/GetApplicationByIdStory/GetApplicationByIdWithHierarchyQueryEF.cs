using ConfigurationManagementSystem.Application.Stories.GetApplicationByIdStory;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Persistence.StoryImplementations.GetApplicationByIdStory
{
    public class GetApplicationByIdWithHierarchyQueryEF : GetApplicationByIdWithHierarchyQuery
    {
        private readonly ConfigurationManagementSystemContext _context;

        public GetApplicationByIdWithHierarchyQueryEF(ConfigurationManagementSystemContext context)
        {
            _context = context;
        }

        public override Task<ApplicationEntity> ExecuteAsync(Guid id)
        {
            return _context.Applications
                .ApplicationsWithIncludedEntities()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
