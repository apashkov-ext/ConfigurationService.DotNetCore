using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.GetConfigurationsStory
{
    [Component]
    public abstract class GetConfigurationsWithHierarchyQuery
    {
        public abstract Task<IQueryable<ConfigurationEntity>> ExecuteAsync();
    }
}