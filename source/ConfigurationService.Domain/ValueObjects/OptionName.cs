using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConfigurationService.Domain.ValueObjects
{
    public class OptionName : ValueObject
    {
        private static readonly Regex Regex = new Regex("^[a-zA-Z][\\w_-]+$");
        public string Value { get; }

        public OptionName(string value)
        {
            if (!Regex.IsMatch(value))
            {
                throw new ApplicationException("Incorrect project name");
            }

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
