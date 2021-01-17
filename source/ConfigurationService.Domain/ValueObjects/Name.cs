using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationService.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public string Value { get; }

        public Name(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
