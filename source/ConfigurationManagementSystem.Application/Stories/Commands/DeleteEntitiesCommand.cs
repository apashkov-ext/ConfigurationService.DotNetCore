using ConfigurationManagementSystem.Application.Stories.Framework;
using ConfigurationManagementSystem.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.Commands
{
    [Command]
    public abstract class DeleteEntitiesCommand
    {
        public abstract Task ExecuteAsync(IEnumerable<DomainEntity> entities);
    }
}
