﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Application;
using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Domain;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationManagementSystem.Persistence
{
    public class Options : IOptions
    {
        private readonly ConfigurationManagementSystemContext _context;

        public Options(ConfigurationManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OptionEntity>> GetAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return await _context.Options.AsNoTracking().ToListAsync();
            }

            var list = await _context.Options
                .Where(x => x.Name.Value.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
                .AsNoTracking()
                .ToListAsync();

            return list;
        }

        public async Task<OptionEntity> GetAsync(Guid id)
        {
            var o = await _context.Options.FindAsync(id);
            return o ?? throw new EntityNotFoundException("Option does not exist");
        }

        public async Task<OptionEntity> AddAsync(Guid optionGroup, string name, object value, OptionValueType type)
        {
            var group = await _context.OptionGroups.Include(x => x.Options)
                .AsSingleQuery()
                .FirstOrDefaultAsync(x => x.Id == optionGroup);

            if (group == null)
            {
                throw new EntityNotFoundException("Option group does not exist");
            }

            var optionValue = TypeConversion.GetOptionValue(value, type);
            var option = group.AddOption(new OptionName(name), optionValue);

            await _context.SaveChangesAsync();

            return option;
        }

        public async Task UpdateAsync(Guid id, string name, object value)
        {
            var option = await _context.Options
                .Include(x => x.OptionGroup)
                .ThenInclude(x => x.Options)
                .AsSingleQuery()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (option == null)
            {
                throw new EntityNotFoundException("Option does not exist");
            }

            var newName = new OptionName(name);
            if (option.Name != newName)
            {
                if (option.OptionGroup.Options.Except(new[] { option }).Any(x => x.Name == newName))
                {
                    throw new ApplicationException("Option with the same name already exists");
                }

                option.UpdateName(newName);
            }

            option.UpdateValue(TypeConversion.GetOptionValue(value, option.Value.Type));

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var option = await _context.Options.Include(x => x.OptionGroup).AsSingleQuery().FirstOrDefaultAsync(x => x.Id == id);
            if (option == null)
            {
                throw new EntityNotFoundException("Option does not exist");
            }

            option.OptionGroup.RemoveOption(option);
            await _context.SaveChangesAsync();
        }
    }
}
