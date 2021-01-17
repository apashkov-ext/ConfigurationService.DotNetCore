using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationService.Domain.ValueObjects
{
    public class ApiKey : ValueObject
    {
        public Guid Value { get; }

        public ApiKey(Guid value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
