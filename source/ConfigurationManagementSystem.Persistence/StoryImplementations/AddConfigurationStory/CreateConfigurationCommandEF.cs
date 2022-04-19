using ConfigurationManagementSystem.Application.Stories.AddConfigurationStory;
using ConfigurationManagementSystem.Domain;
using ConfigurationManagementSystem.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Persistence.StoryImplementations.AddConfigurationStory
{
    public class CreateConfigurationCommandEF : CreateConfigurationCommand
    {
        private readonly ConfigurationManagementSystemContext _context;

        public CreateConfigurationCommandEF(ConfigurationManagementSystemContext context)
        {
            _context = context;
        }

        public override async Task<Guid> ExecuteAsync(ApplicationEntity app, ConfigurationName configurationName)
        {
            var config = app.AddConfiguration(configurationName);
            await _context.SaveChangesAsync();
            return config.Id;
        }
    }
}
