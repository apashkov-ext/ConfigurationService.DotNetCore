using System;
using System.Collections.Generic;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Domain.ValueObjects
{
    public class OptionValue : ValueObject
    {
        public string Value { get; }
        public OptionValueType Type { get; }

        protected OptionValue() { }

        public OptionValue(bool value)
        {
            Value = value.ToString().ToLower();
            Type = OptionValueType.Boolean;
        }

        public OptionValue(int value)
        {
            Value = value.ToString().ToLower();
            Type = OptionValueType.Number;
        }

        public OptionValue(string value)
        {
            Value = value;
            Type = OptionValueType.String;
        }

        public OptionValue(string[] value)
        {
            if (value == null) throw new ApplicationException("Invalid array value");
            Value = string.Join(',', value);
            Type = OptionValueType.StringArray;
        }

        public OptionValue(int[] value)
        {
            if (value == null) throw new ApplicationException("Invalid array value");
            Value = string.Join(',', value);
            Type = OptionValueType.NumberArray;
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Type;
        }
    }
}
