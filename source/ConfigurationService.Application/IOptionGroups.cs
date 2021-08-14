using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Application
{
    public interface IOptionGroups
    {
        Task<IEnumerable<OptionGroup>> Get(string name);
        Task<OptionGroup> Get(Guid id);
        Task<OptionGroup> Add(Guid parent, string name, string description);
        Task Update(Guid id, string name, string description);
        Task Remove(Guid id);
    }
}
