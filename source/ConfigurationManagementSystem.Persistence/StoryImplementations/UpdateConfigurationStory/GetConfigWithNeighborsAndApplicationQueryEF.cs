using ConfigurationManagementSystem.Application.Stories.UpdateConfigurationStory;
using ConfigurationManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Persistence.StoryImplementations.UpdateConfigurationStory
{
    public class GetConfigWithNeighborsAndApplicationQueryEF : GetConfigWithNeighborsAndApplicationQuery
    {
        private readonly ConfigurationManagementSystemContext _context;

        public GetConfigWithNeighborsAndApplicationQueryEF(ConfigurationManagementSystemContext context)
        {
            _context = context;
        }

        public override Task<ConfigurationEntity> ExecuteAsync(Guid configId)
        {
            return _context.Configurations
                .Include(x => x.Application)
                .ThenInclude(x => x.Configurations)
                .AsSingleQuery()
                .FirstOrDefaultAsync(x => x.Id == configId);
        }
    }
}
