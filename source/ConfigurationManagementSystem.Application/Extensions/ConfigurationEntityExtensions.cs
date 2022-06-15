using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Application.Extensions;

internal static class ConfigurationEntityExtensions
{
    public static ConfigurationDto ToDto(this ConfigurationEntity source)
    {
        return new ConfigurationDto
        {
            Id = source.Id.ToString(),
            ApplicationId = source.Application.Id.ToString(),
            Name = source.Name.Value
        };
    }
}
