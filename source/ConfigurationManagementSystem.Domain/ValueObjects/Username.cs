using ConfigurationManagementSystem.Domain.Exceptions;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConfigurationManagementSystem.Domain.ValueObjects
{
    public class Username : ValueObject
    {
        private static readonly Regex Regex = new Regex("^[a-zA-Z][a-zA-Z_\\d]+$");
        public string Value { get; }

        public Username(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value))
            {
                throw new InconsistentDataStateException($"Incorrect username: {value}");
            }

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
