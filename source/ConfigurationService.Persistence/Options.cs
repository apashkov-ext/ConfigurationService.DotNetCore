using System;
using System.Threading.Tasks;
using ConfigurationService.Application;
using ConfigurationService.Application.Exceptions;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using ConfigurationService.Domain.ValueObjects.OptionValueTypes;
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

            var option = Option.Create(new OptionName(name), new Description(description), new )
            group.AddOption();
        }

        public async Task Update(Guid id, string name, string description, object value, OptionValueType? type)
        {
            throw new NotImplementedException();
        }

        public async Task Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        private static OptionValue WrapValue(object value, OptionValueType type)
        {
            return null;
        }
    }
}
