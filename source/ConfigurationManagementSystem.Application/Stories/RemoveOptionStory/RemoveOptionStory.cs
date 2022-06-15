using ConfigurationManagementSystem.Framework.Attributes;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.RemoveOptionStory;

[Component]
public class RemoveOptionStory
{
    public Task ExecuteAsync(Guid id)
    {
        return Task.CompletedTask;
    }
}
