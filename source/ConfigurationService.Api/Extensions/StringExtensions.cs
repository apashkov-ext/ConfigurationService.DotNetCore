using System;
using System.Linq;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Api.Extensions
{
    internal static class StringExtensions
    {
        public static string ToLowerCamelCase(this string input)
        {
            return string.IsNullOrWhiteSpace(input) ? input : char.ToLower(input[0]) + input.Substring(1);
        }

        public static object ParseOptionValue(this string value, OptionValueType type)
        {
            return type switch
            {
                OptionValueType.String => value,
                OptionValueType.Number => int.Parse(value),
                OptionValueType.Boolean => bool.Parse(value),
                OptionValueType.StringArray => value.Split(','),
                OptionValueType.NumberArray => value.Split(',').Select(int.Parse),
                _ => throw new ApplicationException("Unsupported property type")
            };
        }
    }
}
