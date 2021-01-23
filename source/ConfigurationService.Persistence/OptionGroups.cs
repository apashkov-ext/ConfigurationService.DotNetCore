using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigurationService.Application;
using ConfigurationService.Application.Exceptions;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using ConfigurationService.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationService.Persistence
{
    public class OptionGroups : IOptionGroups
    {
        private readonly ConfigurationServiceContext _context;

        public OptionGroups(ConfigurationServiceContext context)
        {
            _context = context;
        }

        public async Task<OptionGroup> Get(Guid id)
        {
            var group = await _context.OptionGroups.OptionGroupsWithIncludedEntities().FirstOrDefaultAsync(x => x.Id == id);
            return group ?? throw new NotFoundException("Project does not exist");
        }

        public async Task<OptionGroup> Add(Guid parent, string name, string description)
        {
            var parentGroup = await _context.OptionGroups.OptionGroupsWithIncludedEntities().FirstOrDefaultAsync(x => x.Id == parent);
            if (parentGroup == null)
            {
                throw new NotFoundException("Parent group does not exist");
            }

            var nestedGroup = OptionGroup.Create(new OptionGroupName(name), new Description(description), new List<Option>(), parentGroup, new List<OptionGroup>());
            parentGroup.AddNestedGroup(nestedGroup);
            await _context.OptionGroups.AddAsync(nestedGroup);
            await _context.SaveChangesAsync();

            return nestedGroup;
        }

        public async Task Update(Guid id, string name, string description)
        {
            var group = await _context.OptionGroups.FindAsync(id);
            if (group == null)
            {
                throw new NotFoundException("Option group does not exist");
            }

            group.UpdateName(new OptionGroupName(name));
            group.UpdateDescription(new Description(description));

            _context.OptionGroups.Update(group);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Guid id)
        {
            var group = await _context.OptionGroups.FindAsync(id);
            if (group == null)
            {
                throw new NotFoundException("Option group does not exist");
            }

            _context.OptionGroups.Remove(group);
            await _context.SaveChangesAsync();
        }
    }
}
