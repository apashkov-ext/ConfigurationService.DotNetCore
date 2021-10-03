using System.Linq;
using ConfigurationManagementSystem.Api.Dto;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Api.Extensions
{
    internal static class OptionGroupExtensions
    {
        public static OptionGroupDto ToDto(this OptionGroup source)
        {
            var options = source.Options.Select(x => x.ToDto());
            var nested = source.NestedGroups.Select(x => x.ToDto());
            return new OptionGroupDto
            {
                Id = source.Id.ToString(),
                Name = source.Name.Value,
                Description = source.Description.Value,
                Options = options,
                NestedGroups = nested,
                Root = source.Parent == null
            };
        }
    }
}
