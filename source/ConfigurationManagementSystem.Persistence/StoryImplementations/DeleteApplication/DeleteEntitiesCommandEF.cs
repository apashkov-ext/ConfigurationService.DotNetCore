﻿using ConfigurationManagementSystem.Application.Stories.RemoveApplicationStory;
using ConfigurationManagementSystem.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Persistence.StoryImplementations.DeleteApplication
{
    public class DeleteEntitiesCommandEF : DeleteEntitiesCommand
    {
        private readonly ConfigurationManagementSystemContext _context;

        public DeleteEntitiesCommandEF(ConfigurationManagementSystemContext context)
        {
            _context = context;
        }

        public override async Task ExecuteAsync(IEnumerable<DomainEntity> entities)
        {
            _context.RemoveRange(entities.ToList());
            await _context.SaveChangesAsync();
        }
    }
}
