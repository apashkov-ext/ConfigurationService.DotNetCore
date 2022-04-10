using ConfigurationManagementSystem.Domain.Exceptions;
using System.Collections.Generic;

namespace ConfigurationManagementSystem.Domain.ValueObjects
{
    public class HashedPassword : ValueObject
    {
        public string Value { get; }

        public HashedPassword(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InconsistentDataStateException($"Incorrect password hash");
            }

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
