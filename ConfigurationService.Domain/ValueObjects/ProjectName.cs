﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConfigurationService.Domain.ValueObjects
{
    public class ProjectName : ValueObject
    {
        public string Value { get; }

        public ProjectName(string value)
        {
            if (!new Regex("^[a-zA-Z_]+$").IsMatch(value))
            {
                throw new ApplicationException("Invalid project name");
            }

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
