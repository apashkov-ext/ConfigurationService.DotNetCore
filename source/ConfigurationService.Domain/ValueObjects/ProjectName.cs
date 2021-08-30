using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConfigurationService.Domain.ValueObjects
{
    public class ProjectName : ValueObject
    {
        private static readonly Regex Regex = new Regex("^[a-zA-Z]+[\\w_-]*$");
        public string Value { get; }

        public ProjectName(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value))
            {
                throw new ApplicationException($"Incorrect project name: {value}");
            }

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
