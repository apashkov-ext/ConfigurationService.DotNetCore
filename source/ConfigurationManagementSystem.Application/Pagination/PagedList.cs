using System;
using System.Collections.Generic;
using System.Linq;

namespace ConfigurationManagementSystem.Application.Pagination
{
    public class PagedList<T>
    {
        public T[] Data { get; }
        public int Offset { get; }
        public int Limit { get; }
        public int TotalElements { get; }

        public PagedList(IEnumerable<T> data, PaginationOptions paginationOptions, int total)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (total < 0) throw new ArgumentOutOfRangeException(nameof(total));

            Data = data.ToArray();
            Offset = paginationOptions.Offset;
            Limit = paginationOptions.Limit;
            TotalElements = total;
        }
    }
}
