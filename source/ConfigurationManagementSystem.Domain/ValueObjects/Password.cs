using ConfigurationManagementSystem.Domain.Exceptions;
using System.Collections.Generic;

namespace ConfigurationManagementSystem.Domain.ValueObjects
{
    public class Password : ValueObject
    {
        public string Value { get; }

        public Password(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InconsistentDataStateException($"Incorrect password: {value}");
            }

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
