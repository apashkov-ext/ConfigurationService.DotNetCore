using ConfigurationService.Domain.Exceptions;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConfigurationService.Domain.ValueObjects
{
    public class Description : ValueObject
    {
        private static readonly Regex Regex = new Regex("^[\\s]+$");
        public string Value { get; }

        public Description(string value)
        {
            if (value == null || Regex.IsMatch(value))
            {
                throw new InconsistentDataStateException("Incorrect description");
            }
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
