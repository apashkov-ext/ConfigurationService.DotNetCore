
using System.Collections.Generic;

namespace ConfigurationManagementSystem.Api.Dto
{
    public class PagedResponseDto<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int TotalElements { get; set; }
    }
}
