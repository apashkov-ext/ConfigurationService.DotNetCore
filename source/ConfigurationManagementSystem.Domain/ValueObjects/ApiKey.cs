﻿using System;
using System.Collections.Generic;
using ConfigurationManagementSystem.Domain.Exceptions;

namespace ConfigurationManagementSystem.Domain.ValueObjects
{
    public class ApiKey : ValueObject
    {
        public Guid Value { get; }

        public ApiKey(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InconsistentDataStateException("Incorrect api key");
            }
            Value = value;
        }

        public ApiKey(string value)
        {
            try
            {
                Value = Guid.Parse(value);
            }
            catch
            {
                throw new InconsistentDataStateException("Incorrect api key");
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
