using System;
using System.Threading.Tasks;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Application
{
    public interface IOptions
    {
        Task<Option> Get(Guid id);
        Task<Option> Add(Guid optionGroup, string name, string description, object value, OptionValueType type);
        Task Update(Guid id, string name, string description, object value, OptionValueType? type);
        Task Remove(Guid id);
    }
}
