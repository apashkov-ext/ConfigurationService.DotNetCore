using ConfigurationManagementSystem.Application.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Persistence.Extensions
{
    internal static class QueryableExtensions
    {      
        public static async Task<PagedList<T>> AsPagedListAsync<T>(this IQueryable<T> source, PaginationOptions paginationOptions)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (paginationOptions == null) throw new ArgumentNullException(nameof(paginationOptions));

            var total = await source.CountAsync();
            var paged = await source.Skip(paginationOptions.Offset).Take(paginationOptions.Limit).ToListAsync();
            var list = new PagedList<T>(paged, paginationOptions, total);

            return list;
        }    
    }
}
