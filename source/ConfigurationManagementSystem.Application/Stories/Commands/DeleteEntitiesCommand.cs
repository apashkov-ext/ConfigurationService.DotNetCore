using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.Commands
{
    [Component]
    public abstract class DeleteEntitiesCommand
    {
        public abstract Task ExecuteAsync(IEnumerable<DomainEntity> entities);
    }
}
