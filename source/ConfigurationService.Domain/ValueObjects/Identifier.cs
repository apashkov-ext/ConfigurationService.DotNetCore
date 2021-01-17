using System;
using System.Collections.Generic;

namespace ConfigurationService.Domain.ValueObjects
{
    public class Identifier : ValueObject
    {
        public Guid Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
