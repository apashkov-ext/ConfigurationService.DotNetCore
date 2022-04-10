using ConfigurationManagementSystem.Application.Stories.Framework;
using ConfigurationManagementSystem.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.GetConfigurationsStory
{
    [Query]
    public abstract class GetConfigurationsWithoutHierarchyQuery
    {
        public abstract Task<IQueryable<ConfigurationEntity>> ExecuteAsync();
    }
}