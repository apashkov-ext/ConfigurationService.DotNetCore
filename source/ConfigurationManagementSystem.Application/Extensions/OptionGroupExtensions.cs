using System.Linq;
using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Application.Extensions;

internal static class OptionGroupExtensions
{
    public static OptionGroupDto ToDto(this OptionGroupEntity source)
    {
        return new OptionGroupDto
        {
            Id = source.Id.ToString(),
            ParentId = source.Parent?.Id.ToString(),
            ConfigurationId = source.Configuration.Id.ToString(),
            Name = source.Name.Value,
            Root = source.Parent == null
        };
    }
}
