using System;
using System.Collections.Generic;

namespace ConfigurationService.Domain.ValueObjects
{
    public class ConfigurationContent : ValueObject
    {
        public string Value { get; } 

        public ConfigurationContent(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ApplicationException("Empty configuration");
            }

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
