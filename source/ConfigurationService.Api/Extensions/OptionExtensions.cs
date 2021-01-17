using ConfigurationService.Api.Dto;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Api.Extensions
{
    internal static class OptionExtensions
    {
        public static OptionDto ToDto(this Option source)
        {
            return new OptionDto
            {
                Name = source.Name.Value,
                Description = source.Description.Value,
                Value = source.Value.Value,
                Type = source.Type
            };
        }
    }
}
