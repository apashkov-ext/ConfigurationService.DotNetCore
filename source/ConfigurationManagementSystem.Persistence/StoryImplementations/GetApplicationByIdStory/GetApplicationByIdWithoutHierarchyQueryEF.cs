using ConfigurationManagementSystem.Application.Stories.GetApplicationByIdStory;
using ConfigurationManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Persistence.StoryImplementations.GetApplicationByIdStory
{
    public class GetApplicationByIdWithoutHierarchyQueryEF : GetApplicationByIdWithoutHierarchyQuery
    {
        private readonly ConfigurationManagementSystemContext _context;

        public GetApplicationByIdWithoutHierarchyQueryEF(ConfigurationManagementSystemContext context)
        {
            _context = context;
        }

        public override Task<ApplicationEntity> ExecuteAsync(Guid id)
        {
            return _context.Applications
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
