using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.GetOptionByIdStory
{
    [Component]
    public class GetOptionByIdStory
    {
        public Task<OptionDto> ExecuteAsync(Guid id)
        {
            return Task.FromResult<OptionDto>(null);
        }
    }
}
