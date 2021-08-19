using System.Collections.Generic;
using System.Linq;
using ConfigurationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ConfigurationService.Persistence
{
    internal static class EntityEntryCollectionExtensions
    {
        public static IEnumerable<Entity> SelectEntityInstances(this IEnumerable<EntityEntry> source, EntityState state)
        {
            return source.Where(x => x.State == state && x.Entity is Entity).Select(x => x.Entity as Entity);
        }
    }
}
