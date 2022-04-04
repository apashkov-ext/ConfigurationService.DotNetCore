using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConfigurationManagementSystem.Domain
{
    public class ConfigurationName : ValueObject
    {
        private static readonly Regex Regex = new Regex("^[a-zA-Z][\\w_-]+$");
        public string Value { get; }

        public ConfigurationName(string value)
        {
            if (!Regex.IsMatch(value))
            {
                throw new ApplicationException("Incorrect configuration name");
            }

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
