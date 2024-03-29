﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using ConfigurationManagementSystem.Domain.Exceptions;

namespace ConfigurationManagementSystem.Domain.ValueObjects;

public class ApplicationName : ValueObject
{
    private static readonly Regex _regex = new("^[a-zA-Z]+[\\w_-]*$");
    public string Value { get; }

    public ApplicationName(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !_regex.IsMatch(value))
        {
            throw new InconsistentDataStateException($"Incorrect application name: {value}");
        }

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
