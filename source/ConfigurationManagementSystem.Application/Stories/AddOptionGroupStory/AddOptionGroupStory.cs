using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.AddOptionGroupStory
{
    [Component]
    public class AddOptionGroupStory
    {
        public Task<OptionGroupDto> ExecuteAsync(CreateOptionGroupDto dto)
        {
            return Task.FromResult<OptionGroupDto>(null);
        }
    }
}
