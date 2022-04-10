using ConfigurationManagementSystem.Api.Dto;
using ConfigurationEntity = ConfigurationManagementSystem.Domain.Entities.ConfigurationEntity;

namespace ConfigurationManagementSystem.Api.Extensions
{
    internal static class ConfigurationEntityExtensions
    {
        public static ConfigurationDto ToDto(this ConfigurationEntity source)
        {
            var root = source.GetRootOptionGroop();
            return new ConfigurationDto
            {
                Id = source.Id.ToString(),
                ApplicationId = source.Application.Id.ToString(),
                Name = source.Name.Value,
                OptionGroup = root.ToDto()
            };
        }
    }
}
