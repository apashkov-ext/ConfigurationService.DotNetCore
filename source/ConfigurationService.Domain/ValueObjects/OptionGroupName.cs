using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConfigurationService.Domain.ValueObjects
{
    public class OptionGroupName : ValueObject
    {
        private static readonly Regex Regex = new Regex("^[a-zA-Z]+$");
        public string Value { get; }

        public OptionGroupName(string value)
        {
            if (value == null || value != string.Empty && !Regex.IsMatch(value))
            {
                throw new ApplicationException($"Incorrect option group name: {value}");
            }

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
