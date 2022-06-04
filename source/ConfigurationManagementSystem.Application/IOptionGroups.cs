using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Application
{
    public interface IOptionGroups
    {
        Task<IEnumerable<OptionGroupEntity>> Get(string name);
        Task<OptionGroupEntity> Get(Guid id);
        Task<OptionGroupEntity> Add(Guid parent, string name);
        Task Update(Guid id, string name);
        Task Remove(Guid id);
    }
}
