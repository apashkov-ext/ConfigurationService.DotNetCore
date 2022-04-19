using ConfigurationManagementSystem.Application.Stories.AddApplicationStory;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
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

        public override async Task<Guid> ExecuteAsync(ApplicationName name, ApiKey apiKey)
        {
            var app = ApplicationEntity.Create(name, new ApiKey(Guid.NewGuid()));
            await _context.Applications.AddAsync(app);
            await _context.SaveChangesAsync();

            return app.Id;
        }
    }
}
