﻿using System;
using System.Linq;
using System.Text.Json;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;

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
                OptionValueType.String => new OptionValue((string)value),
                OptionValueType.Number => new OptionValue((int)value),
                OptionValueType.Boolean => new OptionValue((bool)value),
                OptionValueType.StringArray => new OptionValue((string[])value),
                OptionValueType.NumberArray => new OptionValue((int[])value),
                _ => throw new ApplicationException("Unsupported type")
            };
        }
    }
}
