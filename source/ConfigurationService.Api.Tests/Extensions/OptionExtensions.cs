using ConfigurationService.Api.Dto;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain;

namespace ConfigurationService.Api.Tests.Extensions
{
    internal static class OptionExtensions
    {
        public static bool IsEqualToDto(this Option option, OptionDto dto)
        {
            if (option.Id.ToString() != dto.Id)
            {
                return false;
            }

            if (option.Name.Value != dto.Name)
            {
                return false;
            }

            if (option.Description.Value != dto.Description)
            {
                return false;
            }

            var val = new JsonValueParser(dto.Value, dto.Type).Parse();
            var optionValue = TypeConversion.GetOptionValue(val, dto.Type);
            if (option.Value != optionValue)
            {
                return false;
            }

            return true;
        }
    }
}
