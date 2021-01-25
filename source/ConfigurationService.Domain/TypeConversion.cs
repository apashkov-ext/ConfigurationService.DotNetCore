using System;
using System.Linq;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using ConfigurationService.Domain.ValueObjects.OptionValueTypes;

namespace ConfigurationService.Domain
{
    public static class TypeConversion
    {
        public static object Parse(string value, OptionValueType type)
        {
            return type switch
            {
                OptionValueType.String => value,
                OptionValueType.Number => !string.IsNullOrWhiteSpace(value) ? int.Parse(value) : throw new ApplicationException("Invalid value of number"),
                OptionValueType.Boolean => bool.Parse(value),
                OptionValueType.StringArray => value.Split(','),
                OptionValueType.NumberArray => value.Split(',').Select(int.Parse),
                _ => throw new ApplicationException("Unsupported type")
            };
        }

        public static OptionValue GetOptionValue(object value, OptionValueType type)
        {
            return type switch
            {
                OptionValueType.String => new StringValue((string)value),
                OptionValueType.Number => new NumberValue((int)value),
                OptionValueType.Boolean => new BooleanValue((bool)value),
                OptionValueType.StringArray => new StringArrayValue((string[])value),
                OptionValueType.NumberArray => new NumberArrayValue((int[])value),
                _ => throw new ApplicationException("Unsupported type")
            };
        }
    }
}
