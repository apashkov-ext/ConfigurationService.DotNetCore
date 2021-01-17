using System.Collections.Generic;

namespace ConfigurationService.Domain.ValueObjects
{
    public class Description : ValueObject
    {
        public string Value { get; }

        public Description(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
