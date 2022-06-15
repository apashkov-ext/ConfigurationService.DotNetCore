using ConfigurationManagementSystem.Application.Pagination;
using ConfigurationManagementSystem.Application.Stories.FindOptionGroupsByNameStory;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Persistence.StoryImplementations.FindOptionGroupsByNameStory;

public class GetOptionGroupsByNameQueryEF : IGetOptionGroupsByNameQuery
{
    public Task<PagedList<OptionGroupEntity>> ExecuteAsync(OptionGroupName oGroupName, PaginationOptions paginationOptions)
    {
        return Task.FromResult(PagedList<OptionGroupEntity>.Empty());
        //if (string.IsNullOrEmpty(name))
        //{
        //    var all = await _context.OptionGroups.OptionGroupsWithIncludedEntities().AsNoTracking().ToListAsync();
        //    return all;
        //}

        //var list = await _context.OptionGroups
        //    .OptionGroupsWithIncludedEntities()
        //    .Where(x => x.Name.Value.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
        //    .AsNoTrackingWithIdentityResolution()
        //    .ToListAsync();

        //return list;
    }
}
