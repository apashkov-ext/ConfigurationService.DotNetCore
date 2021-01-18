using System.Linq;
using ConfigurationService.Api.Dto;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Api.Extensions
{
    internal static class OptionGroupExtensions
    {
        public static OptionGroupDto ToDto(this OptionGroup source)
        {
            return new OptionGroupDto
            {
                Name = source.Name.Value,
                Description = source.Description.Value,
                Options = source.Options.Select(x => x.ToDto()),
                NestedGroups = source.NestedGroups.Select(x => x.ToDto())
            };
        }
    }
}
