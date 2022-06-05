using ConfigurationManagementSystem.Api.Dto;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Api.Extensions;

internal static class ApplicationExtensions
{
    public static ApplicationDto ToDto(this ApplicationEntity source)
    {
        return new ApplicationDto
        {
            Id = source.Id.ToString(),
            Name = source.Name.Value
        };
    }

    public static CreatedApplicationDto ToCreatedApplicationDto(this ApplicationEntity source)
    {
        return new CreatedApplicationDto
        {
            Id = source.Id.ToString(),
            Name = source.Name.Value,
            ApiKey = source.ApiKey.Value.ToString()
        };
    }
}
