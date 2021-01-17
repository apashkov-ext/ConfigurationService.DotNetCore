using System;
using System.Collections.Generic;

namespace ConfigurationService.Domain.ValueObjects
{
    public class OptionValue : ValueObject
    {
        public string Value { get; }

        private OptionValue() {}

        public OptionValue(object value)
        {
            Value = ConvertToString(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        private static string ConvertToString(object value)
        {
            var t = value.GetType();
            if (t != typeof(string) && t != typeof(int) && t != typeof(bool))
            {
                throw new ApplicationException("Incorrect value");
            }

            return value.ToString();
        }
    }
}
