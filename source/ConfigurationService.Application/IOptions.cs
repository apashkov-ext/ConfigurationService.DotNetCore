using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Application
{
    public interface IOptions
    {
        Task<IEnumerable<Option>> GetAsync(string name);
        Task<Option> GetAsync(Guid id);
        Task<Option> AddAsync(Guid optionGroup, string name, string description, object value, OptionValueType type);
        Task UpdateAsync(Guid id, string name, string description, object value);
        Task RemoveAsync(Guid id);
    }
}
