﻿using System.Linq;
using ConfigurationManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationManagementSystem.Persistence.Extensions
{
    internal static class OptionGroupQueryableCollectionExtensions
    {
        public static IQueryable<OptionGroupEntity> OptionGroupsWithIncludedEntities(this IQueryable<OptionGroupEntity> source)
        {
            return source
                .Include(x => x.Parent)
                .Include(x => x.Configuration)
                .ThenInclude(x => x.OptionGroups)
                .ThenInclude(x => x.Options)
                .AsSingleQuery();
        }
    }
}
