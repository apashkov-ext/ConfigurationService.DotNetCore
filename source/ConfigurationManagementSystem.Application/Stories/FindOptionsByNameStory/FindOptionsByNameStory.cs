using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.FindOptionsByNameStory
{
    [Component]
    public class FindOptionsByNameStory
    {
        public Task<PagedResponseDto<OptionDto>> ExecuteAsync(PagedRequest request)
        {
            return Task.FromResult<PagedResponseDto<OptionDto>>(null);
        }
    }
}
