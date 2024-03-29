﻿using System.Collections.Generic;
using System.Linq;
using ConfigurationManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ConfigurationManagementSystem.Persistence
{
    internal static class EntityEntryCollectionExtensions
    {
        public static IEnumerable<DomainEntity> SelectEntityInstances(this IEnumerable<EntityEntry> source, EntityState state)
        {
            return source.Where(x => x.State == state && x.Entity is DomainEntity).Select(x => x.Entity as DomainEntity);
        }
    }
}
