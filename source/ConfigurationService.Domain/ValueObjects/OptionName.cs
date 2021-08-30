using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConfigurationService.Domain.ValueObjects
{
    public class OptionName : ValueObject
    {
        private static readonly Regex Regex = new Regex("^[a-zA-Z]+$");
        public string Value { get; }

        public OptionName(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value))
            {
                throw new ApplicationException($"Incorrect option name: {value}");
            }

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
