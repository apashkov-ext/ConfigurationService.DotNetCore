using ConfigurationService.Api.Dto;
using ConfigurationService.Domain;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Api.Extensions
{
    internal static class OptionExtensions
    {
        public static OptionDto ToDto(this Option source)
        {
            return new OptionDto
            {
                Id = source.Id.ToString(),
                Name = source.Name.Value,
                Description = source.Description.Value,
                Value = TypeConversion.Parse(source.Value.Value, source.Value.Type),
                Type = source.Value.Type
            };
        }
    }
}
