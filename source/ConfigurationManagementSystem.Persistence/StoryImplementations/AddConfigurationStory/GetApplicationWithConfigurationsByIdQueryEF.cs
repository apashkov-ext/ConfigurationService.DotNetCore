using ConfigurationManagementSystem.Application.Stories.AddConfigurationStory;
using ConfigurationManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Persistence.StoryImplementations.AddConfigurationStory
{
    public class GetApplicationWithConfigurationsByIdQueryEF : GetApplicationWithConfigurationsByIdQuery
    {
        private readonly ConfigurationManagementSystemContext _context;

        public GetApplicationWithConfigurationsByIdQueryEF(ConfigurationManagementSystemContext context)
        {
            _context = context;
        }

        public override Task<ApplicationEntity> ExecuteAsync(Guid applicationId)
        {
            return _context.Applications.Include(x => x.Configurations).AsSingleQuery().FirstOrDefaultAsync(x => x.Id == applicationId);
        }
    }
}
