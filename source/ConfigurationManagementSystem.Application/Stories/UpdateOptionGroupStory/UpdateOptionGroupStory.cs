using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.UpdateOptionGroupStory;

[Component]
public class UpdateOptionGroupStory
{
    public Task ExecuteAsync(Guid id, UpdateOptionGroupDto dto)
    {
        return Task.CompletedTask;
    }
}
