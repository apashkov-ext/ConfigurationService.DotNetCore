using ConfigurationManagementSystem.Api.Dto;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Api.Extensions
{
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
}
