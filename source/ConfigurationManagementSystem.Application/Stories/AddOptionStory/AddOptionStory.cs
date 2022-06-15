using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Framework.Attributes;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.AddOptionStory;

[Component]
public class AddOptionStory
{
    public Task<OptionDto> ExecuteAsync(CreateOptionDto request)
    {
        return Task.FromResult<OptionDto>(null);
    }
}
