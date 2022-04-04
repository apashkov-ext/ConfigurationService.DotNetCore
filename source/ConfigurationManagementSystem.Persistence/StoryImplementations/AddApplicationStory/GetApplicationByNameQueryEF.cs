using ConfigurationManagementSystem.Application.Stories.AddApplicationStory;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Persistence.StoryImplementations.AddApplicationStory
{
    public class GetApplicationByNameQueryEF : GetApplicationByNameQuery
    {
        private readonly ConfigurationManagementSystemContext _context;

        public GetApplicationByNameQueryEF(ConfigurationManagementSystemContext context)
        {
            _context = context;
        }

        public override Task<ApplicationEntity> ExecuteAsync(ApplicationName name)
        {
            return _context.Applications.FirstOrDefaultAsync(x => x.Name.Value == name.Value);
        }
    }
}
