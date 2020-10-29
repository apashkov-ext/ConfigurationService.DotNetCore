using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConfigurationService.Domain.ValueObjects
{
    public class Env : ValueObject
    {
        public string Value { get; }

        public Env(string value)
        {
            if (!new Regex("^[a-zA-Z_]+$").IsMatch(value))
            {
                throw new ApplicationException("Invalid environment name");
            }

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
