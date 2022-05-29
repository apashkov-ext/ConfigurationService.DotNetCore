using ConfigurationManagementSystem.Application.Stories.UpdateConfigurationStory;
using ConfigurationManagementSystem.Domain;
using ConfigurationManagementSystem.Domain.Entities;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Persistence.StoryImplementations.UpdateConfigurationStory
{
    public class UpdateConfigurationNameCommandEF : UpdateConfigurationNameCommand
    {
        private readonly ConfigurationManagementSystemContext _context;

        public UpdateConfigurationNameCommandEF(ConfigurationManagementSystemContext context)
        {
            _context = context;
        }

        public override Task ExecuteAsync(ConfigurationEntity config, ConfigurationName name)
        {
            config.UpdateName(name);
            return _context.SaveChangesAsync();
        }
    }
}
