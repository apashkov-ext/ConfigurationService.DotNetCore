using System;

namespace ConfigurationManagementSystem.Application.Pagination
{
    public class PaginationOptions
    {
        public int Offset { get; }
        public int Limit { get; }

        public PaginationOptions(int offset, int limit)
        {
            if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
            if (limit <= 0) throw new ArgumentOutOfRangeException(nameof(limit));

            Offset = offset;
            Limit = limit;
        }

        public static PaginationOptions Create(int? offset, int? limit)
        {
            return new(offset ?? 0, limit ?? 20);
        }
    }
}
