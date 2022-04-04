using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Application
{
    public interface IOptions
    {
        Task<IEnumerable<Option>> GetAsync(string name);
        Task<Option> GetAsync(Guid id);
        Task<Option> AddAsync(Guid optionGroup, string name, object value, OptionValueType type);
        Task UpdateAsync(Guid id, string name, object value);
        Task RemoveAsync(Guid id);
    }
}
