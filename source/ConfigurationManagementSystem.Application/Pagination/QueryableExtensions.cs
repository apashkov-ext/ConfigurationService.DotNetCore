using System;
using System.Linq;

namespace ConfigurationManagementSystem.Application.Pagination
{
    public static class QueryableExtensions
    {
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, PaginationOptions paginationOptions)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (paginationOptions == null) throw new ArgumentNullException(nameof(paginationOptions));

            var total = source.Count();
            var paged = source.Skip(paginationOptions.Offset).Take(paginationOptions.Limit).ToList();
            var list = new PagedList<T>(paged, paginationOptions, total);

            return list;
        }
    }
}