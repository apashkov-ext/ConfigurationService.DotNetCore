using ConfigurationManagementSystem.Application.Pagination;
using ConfigurationManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.FindOptionGroupsByNameStory;

public class FindOptionGroupsByNameStory
{
    public Task<PagedList<OptionGroupEntity>> ExecuteAsync(string name, PaginationOptions paginationOptions)
    {
        return Task.FromResult(PagedList<OptionGroupEntity>.Empty());
    }
}
