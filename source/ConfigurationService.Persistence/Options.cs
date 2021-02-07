﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationService.Application;
using ConfigurationService.Application.Exceptions;
using ConfigurationService.Domain;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationService.Persistence
{
    public class Options : IOptions
    {
        private readonly ConfigurationServiceContext _context;

        public Options(ConfigurationServiceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Option>> Get(string name)
        {
            return await _context.Options.Where(x => x.Name.Value.StartsWith(name, StringComparison.InvariantCultureIgnoreCase)).ToListAsync();
        }

        public async Task<Option> Get(Guid id)
        {
            var o = await _context.Options.FindAsync(id);
            return o ?? throw new NotFoundException("Option does not exist");
        }

        public async Task<Option> Add(Guid optionGroup, string name, string description, object value, OptionValueType type)
        {
            var group = await _context.OptionGroups.Include(x => x.Options).FirstOrDefaultAsync(x => x.Id == optionGroup);
            if (group == null)
            {
                throw new NotFoundException("Option group does not exist");
            }

            var optionValue = TypeConversion.GetOptionValue(value, type);
            var option = group.AddOption(new OptionName(name), new Description(description ?? ""), optionValue);

            await _context.SaveChangesAsync();

            return option;
        }

        public async Task Update(Guid id, string name, string description, object value, OptionValueType? type)
        {
            var option = await _context.Options.Include(x => x.OptionGroup).ThenInclude(x => x.Options).FirstOrDefaultAsync(x => x.Id == id);
            if (option == null)
            {
                throw new NotFoundException("Option does not exist");
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

            option.UpdateDescription(new Description(description));
            option.UpdateValue(TypeConversion.GetOptionValue(value, type ?? option.Value.Type));

            _context.Options.Update(option);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Guid id)
        {
            var option = await _context.Options.FindAsync(id);
            if (option == null)
            {
                throw new NotFoundException("Option does not exist");
            }

            _context.Options.Remove(option);
            await _context.SaveChangesAsync();
        }
    }
}
