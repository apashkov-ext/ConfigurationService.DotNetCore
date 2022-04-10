using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Application;
using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.Exceptions;
using ConfigurationManagementSystem.Domain.ValueObjects;
using ConfigurationManagementSystem.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationManagementSystem.Persistence
{
    public class OptionGroups : IOptionGroups
    {
        private readonly ConfigurationManagementSystemContext _context;

        public OptionGroups(ConfigurationManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OptionGroup>> Get(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var all = await _context.OptionGroups.OptionGroupsWithIncludedEntities().AsNoTracking().ToListAsync();
                return all;
            }

            var list = await _context.OptionGroups
                .OptionGroupsWithIncludedEntities()
                .Where(x => x.Name.Value.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();

            return list;
        }

        public async Task<OptionGroup> Get(Guid id)
        {
            var group = await _context.OptionGroups.OptionGroupsWithIncludedEntities().FirstOrDefaultAsync(x => x.Id == id);
            return group ?? throw new EntityNotFoundException("Option Group does not exist");
        }

        public async Task<OptionGroup> Add(Guid parent, string name)
        {
            var parentGroup = await _context.OptionGroups.OptionGroupsWithIncludedEntities().FirstOrDefaultAsync(x => x.Id == parent);
            if (parentGroup == null)
            {
                throw new EntityNotFoundException("Parent Option Group does not exist");
            }

            var nestedGroup = parentGroup.AddNestedGroup(new OptionGroupName(name));
            await _context.SaveChangesAsync();

            return nestedGroup;
        }

        public async Task Update(Guid id, string name)
        {
            var group = await _context.OptionGroups
                .Include(x => x.Configuration)
                .ThenInclude(x => x.OptionGroups)
                .AsSingleQuery()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (group == null)
            {
                throw new EntityNotFoundException("Option Group does not exist");
            }

            if (group.Configuration.GetRootOptionGroop() == group)
            {
                throw new InconsistentDataStateException("Root Option Group cannot be modified");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InconsistentDataStateException("Invalid Option Group name");
            }

            var newName = new OptionGroupName(name);
            var nestedExceptCurrent = group.Parent.NestedGroups.Except(new[] {group});
            if (nestedExceptCurrent.Any(x => x.Name == newName))
            {
                throw new ApplicationException("Option Group with the same name already exists");
            }

            group.UpdateName(newName);

            await _context.SaveChangesAsync();
        }

        public async Task Remove(Guid id)
        {
            var group = await _context.OptionGroups
                .Include(x => x.Configuration)
                .ThenInclude(x => x.OptionGroups)
                .ThenInclude(x => x.Options)
                .AsSingleQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (group == null)
            {
                throw new EntityNotFoundException("Option Group does not exist");
            }

            var root = group.Configuration.GetRootOptionGroop();
            if (group == root)
            {
                throw new InconsistentDataStateException("The root Option Group cannot be deleted");
            }

            group.RemoveWithHierarchy(_context);

            await _context.SaveChangesAsync();
        }
    }
}
