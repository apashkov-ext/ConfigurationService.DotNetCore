using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Application
{
    public interface IOptions
    {
        Task<IEnumerable<OptionEntity>> GetAsync(string name);
        Task<OptionEntity> GetAsync(Guid id);
        Task<OptionEntity> AddAsync(Guid optionGroup, string name, object value, OptionValueType type);
        Task UpdateAsync(Guid id, string name, object value);
        Task RemoveAsync(Guid id);
    }
}
