using ConfigurationService.Api.Dto;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Api.Extensions
{
    internal static class ConfigsExtensions
    {
        public static ConfigurationDto ToDto(this Configuration source)
        {
            return new ConfigurationDto
            {
                Environment = source.Environment.Value,
                Data = source.Content.Value
            };
        }
    }
}
