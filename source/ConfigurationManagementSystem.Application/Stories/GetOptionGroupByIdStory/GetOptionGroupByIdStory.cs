using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.GetOptionGroupByIdStory
{
    [Component]
    public class GetOptionGroupByIdStory
    {
        public Task<OptionGroupDto> ExecuteAsync(Guid id)
        {
            return Task.FromResult<OptionGroupDto>(null);
        }
    }
}
