using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<OptionGroup>> Get(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return await _context.OptionGroups.OptionGroupsWithIncludedEntities().ToListAsync();
            }

            return await _context.OptionGroups.OptionGroupsWithIncludedEntities()
                .Where(x => x.Name.Value.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
                .ToListAsync();
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

            var nestedGroup = parentGroup.AddNestedGroup(new OptionGroupName(name), new Description(description ?? ""));
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
            var group = await _context.OptionGroups.Include(x => x.NestedGroups).FirstOrDefaultAsync(x => x.Id == id);
            if (group == null)
            {
                throw new NotFoundException("Option group does not exist");
            }

            _context.OptionGroups.RemoveRecursive(group);
            await _context.SaveChangesAsync();
        }
    }
}
