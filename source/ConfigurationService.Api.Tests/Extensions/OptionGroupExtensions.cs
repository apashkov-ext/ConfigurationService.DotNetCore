using ConfigurationService.Api.Dto;
using ConfigurationService.Domain.Entities;
using System.Linq;

namespace ConfigurationService.Api.Tests.Extensions
{
    internal static class OptionGroupExtensions
    {
        public static bool IsEqualToDto(this OptionGroup group, OptionGroupDto dto)
        {
            if (group.Id.ToString() != dto.Id)
            {
                return false;
            }

            if (group.Name.Value != dto.Name)
            {
                return false;
            }

            if (group.Description.Value != dto.Description)
            {
                return false;
            }

            if (group.Parent == null && !dto.Root)
            {
                return false;
            }

            foreach (var option in group.Options)
            {
                var id = option.Id.ToString();
                var o = dto.Options.FirstOrDefault(x => x.Id == id);
                if (o == null || !option.IsEqualToDto(o))
                {
                    return false;
                }
            }

            foreach (var nested in group.NestedGroups)
            {
                var id = nested.Id.ToString();
                var g = dto.NestedGroups.FirstOrDefault(x => x.Id == id);
                if (g == null || !nested.IsEqualToDto(g))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
