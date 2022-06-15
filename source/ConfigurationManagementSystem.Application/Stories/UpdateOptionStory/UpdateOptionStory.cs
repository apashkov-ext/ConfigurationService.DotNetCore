using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Framework.Attributes;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.UpdateOptionStory;

[Component]
public class UpdateOptionStory
{
    public Task ExecuteAsync(Guid id, UpdateOptionDto request)
    {
        return Task.CompletedTask;
    }
}
