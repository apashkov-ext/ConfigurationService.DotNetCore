﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using ConfigurationManagementSystem.Domain.Exceptions;

namespace ConfigurationManagementSystem.Domain.ValueObjects;

public class OptionGroupName : ValueObject
{
    private static readonly Regex _regex = new("^[a-zA-Z]+$");
    public string Value { get; }

    public OptionGroupName(string value)
    {
        if (value == null || value != string.Empty && !_regex.IsMatch(value))
        {
            throw new InconsistentDataStateException($"Incorrect option group name: {value}");
        }

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
