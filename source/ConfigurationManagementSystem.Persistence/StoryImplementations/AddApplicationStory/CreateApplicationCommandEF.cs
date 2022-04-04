using ConfigurationManagementSystem.Application.Stories.AddApplicationStory;
using ConfigurationManagementSystem.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Persistence.StoryImplementations.AddApplicationStory
{
    public class CreateApplicationCommandEF : CreateApplicationCommand
    {
        private readonly ConfigurationManagementSystemContext _context;

        public CreateApplicationCommandEF(ConfigurationManagementSystemContext context)
        {
            _context = context;
        }

        public override async Task<Guid> ExecuteAsync(ApplicationEntity application)
        {
            await _context.Applications.AddAsync(application);
            await _context.SaveChangesAsync();

            return application.Id;
        }
    }
}
