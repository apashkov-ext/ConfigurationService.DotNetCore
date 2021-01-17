using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using ConfigurationService.Api.Dto;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Api.Extensions
{
    internal static class OptionGroupExtensions
    {
        public static ExpandoObject ToJsObject(this OptionGroup source)
        {
            dynamic obj = new ExpandoObject();

            foreach (var opt in source.Options)
            {
                obj[opt.Name.Value] = opt.Value.Value;
            }

            foreach (var nested in source.Children)
            {
                obj[nested.Name.Value] = nested.ToJsObject();
            }

            return obj;
        }

        public static IEnumerable<OptionGroupDto> ToAggregatedHierarchyDto(this OptionGroup root)
        {
            yield return root.ToDto();

            foreach (var optionGroup in root.Children)
            {
                yield return optionGroup.ToDto();
            }
        }

        public static OptionGroupDto ToDto(this OptionGroup source)
        {
            return new OptionGroupDto
            {
                Name = source.Name.Value,
                Description = source.Description.Value,
                Options = source.Options.Select(x => x.ToDto()),
                Children = source.Children.Select(x => x.ToDto())
            };
        }
    }
}
