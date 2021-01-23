using System;
using System.Threading.Tasks;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Application
{
    public interface IOptionGroups
    {
        Task<OptionGroup> Get(Guid id);
        Task<OptionGroup> Add(Guid parent, string name, string description);
        Task Update(Guid id, string name, string description);
        Task Remove(Guid id);
    }
}
