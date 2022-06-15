using System;
using System.Linq;
using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Application.Pagination;

namespace ConfigurationManagementSystem.Application.Extensions;

internal static class PagedListExtensions
{
    public static PagedResponseDto<TDto> ToPagedResponseDto<T, TDto>(this PagedList<T> list, Func<T, TDto> converter)
    {
        if (list == null) throw new ArgumentNullException(nameof(list));
        if (converter == null) throw new ArgumentNullException(nameof(converter));

        var converted = list.Data.Select(converter);

        var result = new PagedResponseDto<TDto>
        {
            Data = converted,
            Offset = list.Offset,
            Limit = list.Limit,
            TotalElements = list.TotalElements
        };
        return result;
    }
}
