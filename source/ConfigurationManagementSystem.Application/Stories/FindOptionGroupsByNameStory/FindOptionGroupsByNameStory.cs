using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Application.Extensions;
using ConfigurationManagementSystem.Application.Pagination;
using ConfigurationManagementSystem.Domain.ValueObjects;
using ConfigurationManagementSystem.Framework.Attributes;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.FindOptionGroupsByNameStory;

[Component]
public class FindOptionGroupsByNameStory
{
    private readonly IGetOptionGroupsByNameQuery _getOptionGroupsByNameQuery;

    public FindOptionGroupsByNameStory(IGetOptionGroupsByNameQuery getOptionGroupsByNameQuery)
    {
        _getOptionGroupsByNameQuery = getOptionGroupsByNameQuery;
    }

    public async Task<PagedResponseDto<OptionGroupDto>> ExecuteAsync(PagedRequest request)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        var oGroupName = string.IsNullOrEmpty(request.Name) ? null : new OptionGroupName(request.Name);
        var groups = await _getOptionGroupsByNameQuery.ExecuteAsync(oGroupName, PaginationOptions.Create(request.Offset, request.Limit));
        return groups.ToPagedResponseDto(OptionGroupExtensions.ToDto);
    }
}
