using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Domain;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Application.Extensions;

internal static class OptionExtensions
{
    public static OptionDto ToDto(this OptionEntity source)
    {
        return new OptionDto
        {
            Id = source.Id.ToString(),
            Name = source.Name.Value,
            Value = TypeConversion.Parse(source.Value.Value, source.Value.Type),
            Type = source.Value.Type
        };
    }
}
