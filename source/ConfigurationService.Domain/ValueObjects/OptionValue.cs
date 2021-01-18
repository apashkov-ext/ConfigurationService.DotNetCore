using System.Collections.Generic;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Domain.ValueObjects
{
    public class OptionValue : ValueObject
    {
        public string Value { get; private set; }
        public OptionValueType Type { get; }

        protected OptionValue() { }
        public OptionValue(object value, OptionValueType type)
        {
            SetValue(value);
            Type = type;
        }

        private void SetValue(object value)
        {
            Value = ConvertToString(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Type;
        }

        protected virtual string ConvertToString(object value)
        {
            return value.ToString();
        }
    }
}
