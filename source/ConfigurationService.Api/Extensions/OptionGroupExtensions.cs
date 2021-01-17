using System;
using System.Collections.Generic;
using System.Linq;
using ConfigurationService.Api.Dto;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Api.Extensions
{
    internal static class OptionGroupExtensions
    {
        public static JsObject ToJsObject(this OptionGroup source)
        {
            var obj = new JsObject();

            foreach (var opt in source.Options)
            {
                obj[opt.Name.Value.ToLowerCamelCase()] = ParseValue(opt.Value.Value, opt.Type);
            }

            foreach (var nested in source.NestedGroups)
            {
                obj[nested.Name.Value.ToLowerCamelCase()] = nested.ToJsObject();
            }

            return obj;
        }

        private static object ParseValue(string value, OptionValueType type)
        {
            return type switch
            {
                OptionValueType.String => value,
                OptionValueType.Number => int.Parse(value),
                OptionValueType.Boolean => bool.Parse(value),
                _ => throw new ApplicationException("Unsupported option value type")
            };
        }

        public static IEnumerable<OptionGroupDto> ToAggregatedHierarchyDto(this OptionGroup root)
        {
            yield return root.ToDto();

            foreach (var optionGroup in root.NestedGroups)
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
                NestedGroups = source.NestedGroups.Select(x => x.ToDto())
            };
        }
    }
}
